namespace ION.Core.Content {

  using System;
  using System.Collections.Generic;
  using System.Runtime.Serialization;

	using Appion.Commons.Util;

  using ION.Core.App;
  using ION.Core.Devices;
  using ION.Core.Fluids;
  using ION.Core.Sensors;

  public delegate void OnWorkbenchEvent(WorkbenchEvent workbenchEvent);

  /// <summary>
  /// The object that encapsulates the event data for a workbench changed event.
  /// </summary>
  public class WorkbenchEvent {
    /// <summary>
    /// The type of the event.
    /// </summary>
    /// <value>The type.</value>
    public EType type { get; private set; }
    /// <summary>
    /// The workbench that threw the event.
    /// </summary>
    /// <value>The workbench.</value>
    public Workbench workbench { get; private set; }
    /// <summary>
    /// The manifold that triggered the event. This is null on EType.Invalidated.
    /// </summary>
    /// <value>The manifold.</value>
    public Manifold manifold { get; private set; }
    /// <summary>
    /// The index of the manifold. Note: if the type is Swapped, then this index represents
    /// the manifold AFTER the swap occured.
    /// </summary>
    /// <value>The index.</value>
    public int index { get; private set; }
    /// <summary>
    /// The index of swapped manifold.
    /// </summary>
    /// <value>The index of the other.</value>
    public int otherIndex { get; private set; }
    /// <summary>
    /// The manifold event that was given for the WorkbenchEvent type.
    /// </summary>
    /// <value>The workbench event.</value>
    public ManifoldEvent manifoldEvent { get; private set; }

    public  WorkbenchEvent(EType type, Workbench workbench, Manifold manifold, int index, ManifoldEvent manifoldEvent = null) {
      this.type = type;
      this.workbench = workbench;
      this.manifold = manifold;
      this.index = index;
      this.manifoldEvent = manifoldEvent;
    }

    public WorkbenchEvent(EType type, Workbench workbench, Manifold manifold, int index, int otherIndex) {
      this.type = type;
      this.workbench = workbench;
      this.manifold = manifold;
      this.index = index;
      this.otherIndex = otherIndex;
    }

    /// <summary>
    /// The enumeration of the type of events that a workbench will throw.
    /// </summary>
    public enum EType {
      /// <summary>
      /// The event type used when a manifold is added to the workbench.
      /// </summary>
      Added,
      /// <summary>
      /// The event type used when two manifolds are swapped in the workbench.
      /// </summary>
      Swapped,
      /// <summary>
      /// The event type used when a manifold is removed from the workbench.
      /// </summary>
      Removed,
      /// <summary>
      /// The event type that is used when a manifold in the workbench throws an event.
      /// </summary>
      ManifoldEvent,
    }
  }
  /// <summary>
  /// A workbench is a simple container that will hold a collection of manifolds.
  /// </summary>
  [DataContract(Name="Workbench")]
  public class Workbench : IDisposable {
    public delegate void OnWorkbenchEvent(WorkbenchEvent workbenchEvent);

    public event OnWorkbenchEvent onWorkbenchEvent;

    /// <summary>
    /// The ion instance that the workbench lives in.
    /// </summary>
    /// <value>The ion.</value>
    public IION ion { get; set; }
    /// <summary>
    /// The number of manifolds that are present in the workbench.
    /// </summary>
    /// <value>The count.</value>
    public int count { get { return manifolds.Count; } }
    /// <summary>
    /// The indexer that will poll a manifold from the workbench.
    /// </summary>
    /// <param name="index">Index.</param>
    public Manifold this[int index] {
      get {
        return manifolds[index];
      }

      private set {
        manifolds[index] = value;
      }
    }

    /// <summary>
    /// The backing list of manifolds for the workbench.
    /// </summary>
    public readonly List<Manifold> manifolds = new List<Manifold>();
    
    /// <summary>
    /// Will store the original workbench instance for transitioning between remote viewing mode
    /// </summary>
    public  Workbench storedWorkbench;

    public Workbench(IION ion) {
      this.ion = ion;
      ion.deviceManager.onDeviceManagerEvent += OnDeviceManagerEvent;
    }

    // Overridden from IDisposable
    public void Dispose() {
      ion.deviceManager.onDeviceManagerEvent -= OnDeviceManagerEvent;
    }

    /// <summary>
    /// Queries the index of the given manifold or -1 if the manifold is not present in the workbench.
    /// </summary>
    /// <returns>The of.</returns>
    /// <param name="manifold">Manifold.</param>
    public int IndexOf(Manifold manifold) {
      return manifolds.IndexOf(manifold);
    }

    /// <summary>
    /// Adds the given manifold to the workbench. A given manifold may only exist
    /// once in the workbench.
    /// </summary>
    /// <param name="manifold">Manifold.</param>
    /// <returns>True if the manifold was added, false otherwise.</returns>
    public bool Add(Manifold manifold) {
      if (manifolds.Contains(manifold)) {
        return false;
      } else {
        manifolds.Add(manifold);
        manifold.onManifoldEvent += OnManifoldEvent;
        NotifyOfEvent(WorkbenchEvent.EType.Added, manifold, manifolds.Count - 1);
        return true;
      } 
    }

    /// <summary>
    /// Adds the given sensor to the workbench. If the sensor is already present
    /// within the workbench, the sensor will not be added.
    /// </summary>
    /// <returns><c>true</c>, if sensor was added, <c>false</c> otherwise.</returns>
    /// <param name="sensor">Sensor.</param>
    public bool AddSensor(Sensor sensor) {
      if (ContainsSensor(sensor)) {
        return false;
      } else {
        var m = new Manifold(sensor);
        m.ptChart = PTChart.New(ion, Fluid.EState.Dew);
        return Add(m);
      }
    }

    /// <summary>
    /// Swaps two manifold at the given indices.
    /// </summary>
    /// <param name="first">First.</param>
    /// <param name="second">Second.</param>
    public void Swap(int first, int second) {
      if (first == second) {
        Log.D(this, "Not swapping manifolds");
        return;
      }
      var tmp = manifolds[first];
      manifolds[first] = manifolds[second];
      manifolds[second] = tmp;
      NotifyOfEvent(WorkbenchEvent.EType.Swapped, tmp, second, first);
    }

    /// <summary>
    /// Removes the manifold from the workbench.
    /// </summary>
    /// <param name="manifold">Manifold.</param>
    public void Remove(Manifold manifold) {
      var index = manifolds.IndexOf(manifold);
      manifolds.Remove(manifold);
      manifold.onManifoldEvent -= OnManifoldEvent;
      NotifyOfEvent(WorkbenchEvent.EType.Removed, manifold, index);
    }

    /// <summary>
    /// Removes all instances where the primary sensor is equal to the given the sensor from the workbench.
    /// </summary>
    /// <param name="sensor">Sensor.</param>
    public void Remove(Sensor sensor) {
      for (int i = count - 1; i >= 0; i--) {
        if (manifolds[i].primarySensor.Equals(sensor)) {
          var m = manifolds[i];
          manifolds.RemoveAt(i);
          this.NotifyOfEvent(WorkbenchEvent.EType.Removed, m, 1);
        }
      }
    }

    /// <summary>
    /// Removes the manifold who is using the given sensor as a primary sensor.
    /// </summary>
    /// <param name="sensor">Sensor.</param>
    public void RemovePrimarySensor(Sensor sensor) {
      foreach (var m in manifolds) {
        if (m.primarySensor.Equals(sensor)) {
          Remove(m);
        }
      }
    }

    /// <summary>
    /// Clears the workbench of all manifolds.
    /// </summary>
    public void Clear() {
      foreach (Manifold m in manifolds.ToArray()) {
        Remove(m);
      }
    }

    /// <summary>
    /// Queries whether or not the workbench contains any sensors from the
    /// given device.
    /// </summary>
    /// <returns><c>true</c>, if device was containsed, <c>false</c> otherwise.</returns>
    /// <param name="device">Device.</param>
    public bool ContainsDevice(IDevice device) {
      var gd = device as GaugeDevice;

      if (gd != null) {
        foreach (var m in manifolds) {
          var p = m.primarySensor as GaugeDeviceSensor;
          var s = m.secondarySensor as GaugeDeviceSensor;

          return gd.ContainsSensor(p) || gd.ContainsSensor(s);
        }
      }

      return false;
    }

		public int GetDeviceIndex(ISerialNumber serialNumber, int unitCode){
      if (serialNumber != null) {
        for(int i = 0; i < manifolds.Count; i++) {
          var p = manifolds[i].primarySensor as GaugeDeviceSensor;
          var s = manifolds[i].secondarySensor as GaugeDeviceSensor; 
					
					if(p != null){
	          if(serialNumber.rawSerial == p.device.serialNumber.rawSerial && p.type == UnitLookup.GetSensorTypeFromCode(unitCode)){
							return i;
						};
					}
					if(s != null){
	          if(serialNumber.rawSerial == s.device.serialNumber.rawSerial && s.type ==  UnitLookup.GetSensorTypeFromCode(unitCode)){
							return i;
						};
					}
        }
      }
			return 99;
		}
    /// <summary>
    /// Queries whether or not the workbench contains the given sensor as a primary
    /// sensor to a manifold that exists within the workbench.
    /// </summary>
    /// <returns><c>true</c>, if sensor was containsed, <c>false</c> otherwise.</returns>
    /// <param name="sensor">Sensor.</param>
    public bool ContainsSensor(Sensor sensor) {
      foreach (Manifold manifold in manifolds) {
        if (manifold.primarySensor.Equals(sensor)) {
          return true;
        }
      }

      return false;
    }

    /// <summary>
    /// Removes all usages of the given device. If the device (more accurately, its sensors)
    /// are being used as a secondary sensor in a manifold, then the secondary sensor is
    /// simply cleared. If the sensor is a primary sensor, then the manifold is removed.
    /// </summary>
    /// <param name="device">Device.</param>
    public void RemoveUsesOfDevice(IDevice device) {
      var gd = device as GaugeDevice;

      if (gd != null) {
        var toRemove = new List<Manifold>();

        foreach (var m in manifolds) {
          var p = m.primarySensor as GaugeDeviceSensor;
          var s = m.secondarySensor as GaugeDeviceSensor;

          if (gd.ContainsSensor(p)) {
            toRemove.Add(m);  
          } else if (gd.ContainsSensor(s)) {
            m.SetSecondarySensor(null);
          }
        }

        foreach (var m in toRemove) {
          Remove(m);
        }
      }
    }

    /// <summary>
    /// Notifies the onWorkbenchEvent handler of a new workbench event.
    /// </summary>
    /// <param name="type">Type.</param>
    /// <param name="manifold">Manifold.</param>
    private void NotifyOfEvent(WorkbenchEvent.EType type, Manifold manifold, int index, int otherIndex = -1) {
      if (onWorkbenchEvent != null) {
        onWorkbenchEvent(new WorkbenchEvent(type, this, manifold, index, otherIndex));
      }
    }

    private void NotifyOfEvent(WorkbenchEvent.EType type, ManifoldEvent manifoldEvent) {
      if (onWorkbenchEvent != null) {
        var m = manifoldEvent.manifold;
        onWorkbenchEvent(new WorkbenchEvent(type, this, m, manifolds.IndexOf(m), manifoldEvent));
      }
    }

    /// <summary>
    /// Called when a manifold event occurs.
    /// </summary>
    /// <param name="manifoldEvent">Manifold event.</param>
    private void OnManifoldEvent(ManifoldEvent manifoldEvent) {
      NotifyOfEvent(WorkbenchEvent.EType.ManifoldEvent, manifoldEvent);
    }

    /// <summary>
    /// Called on a device manager event.
    /// </summary>
    /// <param name="e">E.</param>
    private void OnDeviceManagerEvent(DeviceManagerEvent e) {
      if (DeviceManagerEvent.EType.DeviceEvent == e.type) {
        OnDeviceEvent(e.deviceEvent);
      }
    }

    /// <summary>
    /// Called when a device is deleted from the device manager.
    /// </summary>
    /// <param name="deviceEvent">Device event.</param>
    private void OnDeviceEvent(DeviceEvent deviceEvent) {
      // TODO ahodder@appioninc.com: Right now when a device is deleted from the application and the workbench
      // goes to reflect the change, the workbench will do a cascade delete that may leave the application in
      // a weird position. It may be worth it to pop-up a dialog that will inform the user that the app is about
      // to experiece a huge change as the device is removed from all sources.
      switch (deviceEvent.type) {
        case DeviceEvent.EType.Deleted:
          if (ContainsDevice(deviceEvent.device)) {
            RemoveUsesOfDevice(deviceEvent.device);
          }
          break;
      }
    }
  } // End Workbench
}

