namespace ION.Droid.Activity.Grid {

  using System;
  using System.Collections.Generic;

  using Android.App;
  using Android.Content;
  using Android.Support.V7.Widget;
  using Android.OS;
  using Android.Views;
  using Android.Widget;

  using ION.Core.App;
  using ION.Core.Content;
  using ION.Core.Devices;
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

        CleanLayout(i);
      }
    }

    private void RefreshContent() {
      sensors.Clear();

			foreach (var device in dm.devices) {
				var gd = device as GaugeDevice;
        if (gd != null && filter(gd)) {
          AddDevice(gd);          
				}
			}

      NotifyDataSetChanged();
    }

    private void AddDevice(GaugeDevice device) {
      lock (locker) {
			  if (knownDevices.Contains(device)) {
				  return;
			  } else {
				  knownDevices.Add(device);
			  }

			  var index = FindInsertionIndex(device);

			  if (index > sensors.Count) {
				  for (int i = 0; i < device.sensorCount; i++) {
					  sensors.Add(device.sensors[i]);
				  }
			  } else {
				  for (int i = device.sensorCount - 1; i >= 0; i--) {
					  sensors.Insert(index, device.sensors[i]);
				  }
			  }

			  NotifyItemRangeInserted(index, device.sensorCount);

			  CleanLayout(index + device.sensorCount);
		  }
    }

    /// <summary>
    /// Finds the index were the device should be inserted
    /// </summary>
    /// <returns>The insertion index.</returns>
    /// <param name="device">Device.</param>
    private int FindInsertionIndex(GaugeDevice device) {
      int start = 0, end = sensors.Count;
      int i = 0;

      var type = device.serialNumber.deviceModel;
      while (start < end) {
				i = (end + start) / 2;

        if (sensors[i] == null) {
          // Check if we fit in the row. 
          if ((i + device.sensorCount) / colSize >= colSize) {
            // We don't and need to shift down by a row.
            start = i + (colSize - i % colSize);
            continue;
          } else {
            return i;
          }
          // Normal binary search increments
        } else if (device.serialNumber.deviceModel.CompareTo(sensors[i].device.serialNumber.deviceModel) < 0) {
					end = i;
				} else {
          start = i + device.sensorCount;
				}
			}

      return end;
    }

    /// <summary>
    /// This is called after we insert a gauge to clean the layout of any hanging sensors. Because the sensor links are
    /// not all the same length, whenever we insert a new device, we will have to ensure that the insertion did not
    /// screw with the layout 
    /// </summary>
    private void CleanLayout(int startIndex) {
      var i = startIndex;
      while (i < sensors.Count) {
        var row = i / colSize;
        var col = i % colSize;

        if (sensors[i] == null) {
          var j = i;
          while (sensors[j] == null && j < sensors.Count) j++;
          sensors.RemoveRange(i, j - i);
          NotifyItemRangeRemoved(i, j - i);
        } else if (col + (sensors[i].device.sensorCount) > colSize) {
          var cnt = (sensors[i].device.sensorCount);
          // We need to shift the devices down or the will split the row
          for (int j = colSize - col; j > 0; j--) {
            // Insert some dead space.
            sensors.Insert(i, null);
          }
          i += (colSize - col) + cnt;
        } else {
          i += sensors[i].device.sensorCount;
        }
      }

      NotifyItemRangeChanged(startIndex, sensors.Count - startIndex);
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
                AddDevice(gd);
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
