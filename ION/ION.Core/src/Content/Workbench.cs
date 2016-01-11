namespace ION.Core.Content {

  using System;
  using System.Collections.Generic;
  using System.Runtime.Serialization;

  using ION.Core.App;
  using ION.Core.Devices;
  using ION.Core.Sensors;

  public delegate void OnWorkbenchEvent(WorkbenchEvent workbenchEvent);

  /// <summary>
  /// The object that encapsulates the event data for a workbench changed event.
  /// </summary>
  public class WorkbenchEvent {
    public EType type { get; private set; }
    public Workbench workbench { get; private set; }
    /// <summary>
    /// The manifold that triggered the event. This is null on EType.Invalidated.
    /// </summary>
    /// <value>The manifold.</value>
    public Manifold manifold { get; private set; }

    public  WorkbenchEvent(EType type, Workbench workbench, Manifold manifold = null) {
      this.type = type;
      this.workbench = workbench;
      this.manifold = manifold;
    }

    public enum EType {
      Added,
      Removed,
      /// <summary>
      /// An event of this type will not contain a manifold.
      /// </summary>
      Invalidated,
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
    public int count { get { return __manifolds.Count; } }
    /// <summary>
    /// The indexer that will poll a manifold from the workbench.
    /// </summary>
    /// <param name="index">Index.</param>
    public Manifold this[int index] {
      get {
        return __manifolds[index];
      }
    }

    /// <summary>
    /// The backing list of manifolds for the workbench.
    /// </summary>
    public List<Manifold> manifolds {
      get {
        return new List<Manifold>(__manifolds);
      }
    } List<Manifold> __manifolds = new List<Manifold>();

    public Workbench(IION ion) {
      this.ion = ion;
      ion.deviceManager.onDeviceEvent += OnDeviceEvent;
    }

    // Overridden from IDisposable
    public void Dispose() {
      ion.deviceManager.onDeviceEvent -= OnDeviceEvent;
    }

    /// <summary>
    /// Adds the given manifold to the workbench. A given manifold may only exist
    /// once in the workbench.
    /// </summary>
    /// <param name="manifold">Manifold.</param>
    /// <returns>True if the manifold was added, false otherwise.</returns>
    public bool Add(Manifold manifold) {
      if (__manifolds.Contains(manifold)) {
        return false;
      } else {
        __manifolds.Add(manifold);
        NotifyWorkbenchEvent(WorkbenchEvent.EType.Added, manifold);
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
        var add = new Manifold(sensor);
        __manifolds.Add(add);
        NotifyWorkbenchEvent(WorkbenchEvent.EType.Added, add);

        return true;
      }
    }

    /// <summary>
    /// Removes the manifold from the workbench.
    /// </summary>
    /// <param name="manifold">Manifold.</param>
    public void Remove(Manifold manifold) {
      __manifolds.Remove(manifold);
      NotifyWorkbenchEvent(WorkbenchEvent.EType.Removed, manifold);
    }

    /// <summary>
    /// Clears the workbench of all manifolds.
    /// </summary>
    public void Clear() {
      foreach (Manifold m in __manifolds) {
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
        foreach (var m in __manifolds) {
          var p = m.primarySensor as GaugeDeviceSensor;
          var s = m.secondarySensor as GaugeDeviceSensor;

          return gd.ContainsSensor(p) || gd.ContainsSensor(s);
        }
      }

      return false;
    }

    /// <summary>
    /// Queries whether or not the workbench contains the given sensor as a primary
    /// sensor to a manifold that exists within the workbench.
    /// </summary>
    /// <returns><c>true</c>, if sensor was containsed, <c>false</c> otherwise.</returns>
    /// <param name="sensor">Sensor.</param>
    public bool ContainsSensor(Sensor sensor) {
      foreach (Manifold manifold in __manifolds) {
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
            m.secondarySensor = null;
          }
        }

        foreach (var m in toRemove) {
          Remove(m);
        }
      }
    }

    /// <summary>
    /// Notifies the manifold event handler of a new manifold event.
    /// </summary>
    /// <param name="type">Type.</param>
    private void NotifyWorkbenchEvent(WorkbenchEvent.EType type, Manifold manifold=null) {
      NotifyWorkbenchEvent(new WorkbenchEvent(type, this, manifold));
    }

    /// <summary>
    /// Notifies the manifold event handler of a new manifold event.
    /// </summary>
    /// <param name="manifoldEvent">Manifold event.</param>
    private void NotifyWorkbenchEvent(WorkbenchEvent manifoldEvent) {
      if (onWorkbenchEvent != null) {
        onWorkbenchEvent(manifoldEvent);
      }
    }

    /// <summary>
    /// Called when a device is deleted from the device manager.
    /// </summary>
    /// <param name="deviceEvent">Device event.</param>
    private void OnDeviceEvent(DeviceEvent deviceEvent) {
      switch (deviceEvent.type) {
        case DeviceEvent.EType.Deleted:
          if (ContainsDevice(deviceEvent.device)) {
            RemoveUsesOfDevice(deviceEvent.device);
            NotifyWorkbenchEvent(WorkbenchEvent.EType.Invalidated);
          }
          break;
      }
    }
  } // End Workbench
}

