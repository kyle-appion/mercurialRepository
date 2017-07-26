namespace ION.Droid.Activity.Grid {

  using System;
  using System.Collections.Generic;

  using Android.Support.V7.Widget;
  using Android.OS;
  using Android.Views;

  using ION.Core.App;
  using ION.Core.Content;
  using ION.Core.Devices;
  using ION.Core.Devices.Sorters;
  using ION.Core.Sensors;

  using ION.Droid.Widgets.RecyclerViews;
  using ION.Droid.Views;

  public class DeviceGridAdapter : RecyclerView.Adapter {

    public delegate bool AcceptDeviceFilterDelegate(GaugeDevice device);
    public delegate void OnSensorClicked(GaugeDeviceSensor sensor, int index);

    public override int ItemCount {
      get {
        return sensors.Count;
      }
    }

    public GaugeDeviceSensor this[int index] {
      get {
        return sensors[index];
      }
    }

    public IION ion { get; private set; }
    public int colSize { get; private set; }
    public IDeviceManager dm { get; private set; }

    public OnSensorClicked onSensorClicked { get; set; }

    private HashSet<GaugeDevice> knownDevices = new HashSet<GaugeDevice>();
    private List<GaugeDeviceSensor> sensors = new List<GaugeDeviceSensor>();

    private LinkDecorator links;
    private AcceptDeviceFilterDelegate filter;

    private readonly Handler handler = new Handler();
    private readonly object locker = new object();

    public DeviceGridAdapter(IION ion, int colSize, AcceptDeviceFilterDelegate filter) {
      this.ion = ion;
      this.colSize = colSize;
      this.filter = filter;
      dm = ion.deviceManager;
    }

    public override void OnAttachedToRecyclerView(RecyclerView recyclerView) {
      base.OnAttachedToRecyclerView(recyclerView);
      links = new LinkDecorator(recyclerView.Context, this);
      recyclerView.AddItemDecoration(links);
      recyclerView.GetItemAnimator().ChangeDuration = 0;

      RefreshContent();
      dm.onDeviceManagerEvent += OnDeviceManagerEvent;
      ion.currentWorkbench.onWorkbenchEvent += OnWorkbenchEvent;
      ion.currentAnalyzer.onAnalyzerEvent += OnAnalyzerEvent;
    }

    public override void OnDetachedFromRecyclerView(RecyclerView recyclerView) {
      base.OnDetachedFromRecyclerView(recyclerView);
      recyclerView.RemoveItemDecoration(links);
      links.Release();

      sensors.Clear();
      dm.onDeviceManagerEvent -= OnDeviceManagerEvent;
      ion.currentWorkbench.onWorkbenchEvent -= OnWorkbenchEvent;
      ion.currentAnalyzer.onAnalyzerEvent -= OnAnalyzerEvent;
    }

    public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType) {
      return new DeviceViewHolder(parent);
    }

    public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position) {
      var vh = holder as DeviceViewHolder;
      vh.Bind(ion, this[position]);
      vh.ItemView.SetOnClickListener(new ViewClickAction((v) => {
        if (onSensorClicked != null) {
          onSensorClicked(sensors[position], position);
        }
      }));
    }

    /// <summary>
    /// Queried whether or not the given indices are linked.
    /// </summary>
    /// <returns><c>true</c>, if indices linked was ared, <c>false</c> otherwise.</returns>
    /// <param name="i1">I1.</param>
    /// <param name="i2">I2.</param>
    public bool AreIndicesLinked(int i1, int i2) {
      return sensors[i1] != null && sensors[i2] != null &&
             sensors[i1].device.Equals(sensors[i2].device) &&
             (sensors[i1].device.isConnected || sensors[i1].device.isNearby);
    }

    public bool Contains(Sensor sensor) {
      var gds = sensor as GaugeDeviceSensor;
      if (gds != null) {
        return sensors.Contains(gds);
      } else {
        return false;
      }
    }

    public bool Contains(GaugeDevice gd) {
      return knownDevices.Contains(gd);
    }

    public int IndexOfDevice(GaugeDevice gd) {
      var ret = 0;
      for (int i = 0; i < sensors.Count; i++) {
        if (sensors[i] == null) {
          continue;
        } else if (!sensors[i].device.Equals(gd)) {
          i += sensors[i].device.sensorCount;
        } else {
          ret = i;
          break;
        }
      }

      return ret;
    }

    public int IndexOfSensor(Sensor sensor) {
      if (sensor == null) {
        return -1;
      }

      for (int i = 0; i < sensors.Count; i++) {
        if (sensor.Equals(sensors[i])) {
          return i;
        }
      }

      return -1;
    }

    private void RemoveDevice(GaugeDevice gd) {
      lock (locker) {
        if (!knownDevices.Contains(gd)) {
          return;
        }

        knownDevices.Remove(gd);

        var i = IndexOfDevice(gd);

        for (int j = 0; j < gd.sensorCount; j++) {
          sensors.RemoveAt(i);
        }

        NotifyItemRangeRemoved(i, gd.sensorCount);

        CrushContent();
      }
    }

    private void RefreshContent() {
      sensors.Clear();

      foreach (var device in dm.devices) {
        var gd = device as GaugeDevice;
        if (gd != null && filter(gd)) {
          InsertGaugeDevice(gd);
        }
      }

      NotifyDataSetChanged();
    }

		/// <summary>
		/// Inserts the device's sensor into the backing array. 
		/// </summary>
		/// <returns>The insertion index.</returns>
		/// <param name="device">Device.</param>
		private void InsertGaugeDevice(GaugeDevice device) {
      lock (locker) {
        if (knownDevices.Contains(device)) {
          return;
        } else {
          knownDevices.Add(device);
        }

        var comparer = new GeneralSensorSorter();
        var size = device.sensorCount;

        var index = 0;
        var k = 0;

        for (; index < sensors.Count; index += colSize) {
          if (sensors[index] == null) {
            // If the sensor is null, then we have a problem, the adapter was not crushed before a new device was added.
            // Try crushing the content in hopes that we can fix whatever happened.
            CrushContent();
            if (sensors[index] == null) {
              // The sensor is still null. We have an issue with the algorithm and we need to resolve it. Hopefully this
              // will happen in test code, otherwise, we will just rape to adapter to prevent any bad things from happening.
#if DEBUG
              throw new Exception("Found null index {" + index + "}: expected an item");
#else
	          Log.E(this, "Unexpected issue with inserting sensor into DeviceGridAdapter: found a null item at first index");
	          // At this point, the adapter is broken, and we can't really fix it without a shit ton of effort. Just append
	          // the sensor to the end of the list and wait until the user exits activity and comes back.
	          return sensors.Count;
#endif
            }
          }

          for (k = 0; k < colSize - device.sensorCount + 1 && index + k < sensors.Count; k++) {
            if (sensors[index + k] != null) {
              var dir = comparer.Compare(device.sensors[0], sensors[index + k]);
              if (dir >= 0) {
                continue;
              }

              // We found the insert index. Let's figure out what needs to happen here.
              goto __FOUND_INDEX__;
            }
          }
        }

__FOUND_INDEX__:
        if (index + k > sensors.Count) {
          var oldCnt = sensors.Count;
          // If our insertion index is past the sensors count, then we can simply add the items to the end of the list
          // First though, check if we fit on the row.
          if (device.sensorCount + k > colSize) {
            // Fill out the row with empties, and then add the device
            for (int j = 0; j < colSize - k; j++) {
              sensors.Add(null);
            }
          }
          // Now we can add the gauges.
          var i = sensors.Count;
          foreach (var sensor in device.sensors) {
            sensors.Add(sensor);
          }

          NotifyItemRangeInserted(i, sensors.Count - oldCnt);
					return;
        } else {
          // Otherwise, we need to insert the item.
          if (index + k + device.sensorCount > index + colSize) {
            // Adding the sensor to the given index will split rows. So, add the sensors to the next row
            // However, we do need to fill this row with empties
            for (int i = colSize - 1; i >= k; i--) {
              sensors.Insert(index + k, null);
            }
            index += colSize;
            k = 0;
          }

          // Insert the device into the array.
          for (int i = device.sensorCount - 1; i >= 0; i--) {
            sensors.Insert(index + k, device.sensors[i]);
          }

          if (FixRows(index)) {
            NotifyDataSetChanged();
          } else {
            NotifyItemRangeInserted(index + k, device.sensorCount);
          }
        }
      }
    }

    private bool FixRows(int index) {
      var @fixed = false;

      // Trim the trailing empties
      for (int i = sensors.Count - 1; i >= 0 && sensors[i] == null; i--) {
        sensors.RemoveAt(i);
      }

      for (int row = index / colSize; row < sensors.Count; row += colSize) {
        // Eat the leadings empties
        for (int i = 0; i < colSize && sensors[row] == null; i++) {
          sensors.Insert(row - 1, null);
          sensors.RemoveAt(row);
        }

        // Walk through the row seeing if any device's are cut into two rows
        for (int col = 0; col < colSize; col++) {
          var i = row * colSize + col;
          if (i >= sensors.Count) {
            break;
          } else if (sensors[i] == null) {
            continue;
          } else if (col + sensors[i].device.sensorCount > colSize) {
            // We found a sensor that needs to be pushed to the next row. Add empties until the row is full.
            for (int j = col; j < colSize; j++) {
              sensors.Insert(i, null);
            }

            @fixed = true;
          } else {
            col += sensors[i].device.sensorCount - 1;
          }
        }
      }

      return @fixed;
    }


    /// <summary>
    /// Attempts to ensure that the content is as compressed as possible. This should be called whenever an item is
    /// removed from the adapter. This method will call NotifyDataSetChanged().
    /// </summary>
    private void CrushContent() {
      for (var index = 0; index < sensors.Count; index += colSize) {
        // Eat up the row until we find a null item (if at all).
        var k = 0;
        while (index + k < sensors.Count && k < colSize && sensors[index + k] != null) k++;

        if (k >= colSize) {
          // The row is valid so we don't need to do anything
          continue;
        }

        // Our current index;
        var i = index + k;

        // We have a null in the row, so a couple of things can happen here.
        // Note: option 1 is optional (may or may not be true), however, regardless, option 2 will always occur.
        //    1) the row has a hole (null followed by an item) making it fragmented, and we need to collapse it
        //    2) there are emtpies at the end of the row. to fix this, we must attempt to bring down items from the next
        //        row into this row (if able).

        // Let's see if the row is fragmented. To be considered fragmented, the number of consecutive empties must be
        // less than the remaining slots in the row. If the number of empties is equal to the number of remaining slots,
        // then we can simply pull down the next row's sensors.
        var empties = CountConsecutiveEmpties(i);
        var isFragmented = empties > colSize - k; // Account for how far into the row we are.

        if (isFragmented) {
          // We are fragmented, so collapse the row.
          for (int j = 0; j < empties; j++) {
            sensors[i] = sensors[i + empties];
            sensors[i + empties] = null;
          }
          i += empties; // Advance i by the number of sensors we just moved over.
        }

        // Now we need to determine if we can even try to bring anything from the next row down into this row.
        if (index + colSize > sensors.Count) {
          // Nope, this was the last row.
          continue;
        }

        // TODO ahodder@appioninc.com: I think this algorithm will leave null items at the end of the adapter.
        // This will need to be discovered and resolved at some point

				// Attempt to bring some things from the next row down.


				// Find the index first sensor on the next row.
				var nri = index + colSize;
        while (nri < sensors.Count && nri < index + 2 * colSize && sensors[nri] == null) nri++;

        var ds = sensors[nri].device.sensorCount; // The number of sensors that the device has. 

        // Check if we can fit the device into this row. If not, then we are done and this row is finished.
        if (i - index + ds < colSize) {
          // We can add the device into this row
          for (int j = 0; j < ds; j++) {
            sensors[i] = sensors[nri + j];
            sensors[nri + j] = null;
          }
          i += ds; // Advance i by the number of sensors we just moved down
        }

        // At this point we have collapsed all of the sensor to the left and pulled down any sensors that we can for
        // this row. We are good to move on to the next row.
      }

      NotifyDataSetChanged();
    }

    /// <summary>
    /// Counts the number of empty indices until the edge of the adapter row.
    /// </summary>
    /// <returns>The consecutive empties.</returns>
    /// <param name="index">Index.</param>
    private int CountConsecutiveEmpties(int index) {
      // Find the index that completes the row, and compare it to the number of items in the sensor list.
      var endIndex = Math.Min(((index / colSize) + 1) * colSize, sensors.Count);
      var ret = 0;

      // While our index is less than our end index, let's see how many consecutive empties there are.
      for (; index < endIndex && sensors[index] == null; index++) ret++;

      return ret;
    }

    private void InvalidateDevice(GaugeDevice gd) {
      foreach (var sensor in gd.sensors) {
        var index = IndexOfSensor(sensor);
        if (index >= 0) {
          NotifyItemChanged(index);
        }
      }
    }

		private void OnDeviceManagerEvent(DeviceManagerEvent e) {
			switch (e.type) {
				case DeviceManagerEvent.EType.DeviceEvent:
					OnDeviceEvent(e.deviceEvent);
					break;
			}
		}

		private void OnDeviceEvent(DeviceEvent e) {
      var gd = e.device as GaugeDevice;
      if (gd == null) {
        return;
      }

      switch (e.type) {
        case DeviceEvent.EType.Found:
        case DeviceEvent.EType.ConnectionChange:
          handler.Post(() => {
            if (filter(gd)) {
              if (knownDevices.Contains(gd)) {
                InvalidateDevice(gd);
              } else {
                InsertGaugeDevice(gd);
              }
            } else {
              RemoveDevice(gd);
            }
          });
          break;

        case DeviceEvent.EType.Deleted:
          handler.Post(() => {
            RemoveDevice(gd);
          });
          break;

        case DeviceEvent.EType.NewData:
        case DeviceEvent.EType.NameChanged:
          handler.Post(() => {
            foreach (var sensor in gd.sensors) {
              var index = IndexOfSensor(sensor);
              if (index >= 0) {
                NotifyItemChanged(index);
              }
            }
          });
          break;
			}
		}

    private void OnWorkbenchEvent(WorkbenchEvent e) {
      switch (e.type) {
        case WorkbenchEvent.EType.Added:
        case WorkbenchEvent.EType.Removed:
          handler.Post(() => {
            var pi = IndexOfSensor(e.manifold.primarySensor);
            var si = IndexOfSensor(e.manifold.secondarySensor);

            if (pi >= 0) {
              NotifyItemChanged(pi);
            }

            if (si >= 0) {
              NotifyItemChanged(si);
            }
          });
          break;
      }
    }

    private void OnAnalyzerEvent(AnalyzerEvent e) {
      switch (e.type) {
        case AnalyzerEvent.EType.Added:
        case AnalyzerEvent.EType.Removed:
					handler.Post(() => {
            var index = IndexOfSensor(e.sensor);

            if (index >= 0) {
              NotifyItemChanged(index);
            }
					});
					break;
      }
    }
  }
}
