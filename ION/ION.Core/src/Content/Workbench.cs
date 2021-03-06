﻿namespace ION.Core.Content {

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
		//public Manifold manifold { get; private set; }
		public Sensor sensor { get; private set; }
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

		//public WorkbenchEvent(EType type, Workbench workbench, Manifold manifold, int index, ManifoldEvent manifoldEvent = null){
		public  WorkbenchEvent(EType type, Workbench workbench, Sensor sensor, int index, ManifoldEvent manifoldEvent = null) {
      this.type = type;
      this.workbench = workbench;
      //this.manifold = manifold;
      this.sensor = sensor;
      this.index = index;
      this.manifoldEvent = manifoldEvent;
    }

		//public WorkbenchEvent(EType type, Workbench workbench, Manifold manifold, int index, int otherIndex){
		public WorkbenchEvent(EType type, Workbench workbench, Sensor sensor, int index, int otherIndex) {
      this.type = type;
      this.workbench = workbench;
			//this.manifold = manifold;
			this.sensor = sensor;
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
		//public int count { get { return manifolds.Count; } }
		public int count { get { return sensors.Count; } }
		/// <summary>
		/// The indexer that will poll a manifold from the workbench.
		/// </summary>
		/// <param name="index">Index.</param>
		//public Manifold this[int index] {
		//  get {
		//    return manifolds[index];
		//  }

		//  private set {
		//    manifolds[index] = value;
		//  }
		//}
		public Sensor this[int index]
		{
			get
			{
				return sensors[index];
			}

			private set
			{
				sensors[index] = value;
			}
		}
		/// <summary>
		/// The backing list of manifolds for the workbench.
		/// </summary>
		//public readonly List<Manifold> manifolds = new List<Manifold>();
		public readonly List<Sensor> sensors = new List<Sensor>();
		/// <summary>
		/// Whether or not the workbench is editable.
		/// </summary>
		/// <value><c>true</c> if is editable; otherwise, <c>false</c>.</value>
		public bool isEditable { get; set; }
		/// <summary>
		/// Whether or not the workbench is empty.
		/// </summary>
		/// <value><c>true</c> if is empty; otherwise, <c>false</c>.</value>
		//public bool isEmpty { get { return manifolds.Count <= 0; } }
		public bool isEmpty { get { return sensors.Count <= 0; } }

    public Workbench(IION ion) {
      this.ion = ion;
      ion.deviceManager.onDeviceManagerEvent += OnDeviceManagerEvent;
			isEditable = true;
    }

    // Overridden from IDisposable
    public void Dispose() {
      ion.deviceManager.onDeviceManagerEvent -= OnDeviceManagerEvent;
    }

		/// <summary>
		/// Queries the index of the manifold whose primary sensor is the given sensor.
		/// </summary>
		/// <returns>The of.</returns>
		/// <param name="sensor">Sensor.</param>
		//public int IndexOf(Sensor sensor) {
		//	for (int i = 0; i < manifolds.Count; i++) {
		//		if (manifolds[i].primarySensor == sensor) {
		//			return i;
		//		}
		//	}

		//	return -1;
		//}

		/// <summary>
		/// Queries the index of the given manifold or -1 if the manifold is not present in the workbench.
		/// </summary>
		/// <returns>The of.</returns>
		/// <param name="manifold">Manifold.</param>
		//public int IndexOf(Manifold manifold){
		public int IndexOf(Sensor sensor) {
			//return manifolds.IndexOf(manifold);
			return sensors.IndexOf(sensor);
    }

		/// <summary>
		/// Adds the given manifold to the workbench. A given manifold may only exist
		/// once in the workbench.
		/// </summary>
		/// <param name="manifold">Manifold.</param>
		/// <returns>True if the manifold was added, false otherwise.</returns>
		//public bool Add(Manifold manifold){
		public bool Add(Sensor sensor) {
			//if (manifolds.Contains(manifold)){
		  if (sensors.Contains(sensor)) {
        return false;
      } else {
    //    manifolds.Add(manifold);
    //    manifold.onManifoldEvent += OnManifoldEvent;
				//var GaugeSensor = manifold.primarySensor as GaugeDeviceSensor;

				//NotifyOfEvent(WorkbenchEvent.EType.Added, manifold, manifolds.Count - 1);

				//if (GaugeSensor.device.serialNumber.deviceModel == EDeviceModel.AV760) {
				//	manifold.AddSensorProperty(new Sensors.Properties.RateOfChangeSensorProperty(manifold, TimeSpan.FromSeconds(1)));
				//} else if (GaugeSensor.device.serialNumber.deviceModel == EDeviceModel.PT500 || GaugeSensor.device.serialNumber.deviceModel == EDeviceModel.PT800) {
				//	manifold.AddSensorProperty(new Sensors.Properties.SuperheatSubcoolSensorProperty(manifold));
				//	manifold.AddSensorProperty(new Sensors.Properties.SecondarySensorProperty(manifold));
				//}
        sensors.Add(sensor);
        if (sensor is GaugeDeviceSensor)
        {
          var GaugeSensor = sensor as GaugeDeviceSensor;

          if (GaugeSensor.device.serialNumber.deviceModel == EDeviceModel.AV760)
          {
            sensor.AddSensorProperty(new Sensors.Properties.RateOfChangeSensorProperty(sensor, TimeSpan.FromSeconds(1)));
          }
          else if (GaugeSensor.device.serialNumber.deviceModel == EDeviceModel.PT500 || GaugeSensor.device.serialNumber.deviceModel == EDeviceModel.PT800)
          {
            sensor.AddSensorProperty(new Sensors.Properties.SuperheatSubcoolSensorProperty(sensor));
            sensor.AddSensorProperty(new Sensors.Properties.SecondarySensorProperty(sensor));
          }
        }
        onWorkbenchEvent(new WorkbenchEvent(WorkbenchEvent.EType.Added, this, sensor, this.sensors.Count));
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
				return Add(sensor);
      }
    }

		/// <summary>
		/// Inserts the manifold into the given index of the workbench.
		/// </summary>
		/// <param name="manifold">Manifold.</param>
		/// <param name="index">Index.</param>
		//public bool Insert(Manifold manifold, int index){
		public bool Insert(Sensor sensor, int index) {
      //if (manifolds.Contains(manifold)) {
      //	return false;
      //} else {
      //	manifolds.Insert(index, manifold);
      //	manifold.onManifoldEvent += OnManifoldEvent;
      //	NotifyOfEvent(WorkbenchEvent.EType.Added, manifold, index);
      //	return true;
      //}
      if(sensors.Contains(sensor)){
        return false;
      }else {
        sensors.Insert(index,sensor);
        return true;
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
      //var tmp = manifolds[first];
      //manifolds[first] = manifolds[second];
      //manifolds[second] = tmp;
      var tmp = sensors[first];
      sensors[first] = sensors[second];
      sensors[second] = tmp;
      NotifyOfEvent(WorkbenchEvent.EType.Swapped, tmp, second, first);
    }

		/// <summary>
		/// Removes the manifold from the workbench.
		/// </summary>
		/// <param name="manifold">Manifold.</param>
		//public void Remove(Manifold manifold){
		public void Remove(Sensor sensor) {
			//var index = manifolds.IndexOf(manifold);
			var index = sensors.IndexOf(sensor);
      //manifolds.Remove(manifold);
      //manifold.onManifoldEvent -= OnManifoldEvent;
      //NotifyOfEvent(WorkbenchEvent.EType.Removed, manifold, index);
      if (index != -1)
      {
        sensors.Remove(sensor);
        NotifyOfEvent(WorkbenchEvent.EType.Removed, sensor, index);
      }
    }

    /// <summary>
    /// Removes the manifold who is using the given sensor as a primary sensor.
    /// </summary>
    /// <param name="sensor">Sensor.</param>
    public void RemovePrimarySensor(Sensor sensor) {
			//foreach (var m in manifolds) {
			//  if (m.primarySensor.Equals(sensor)) {
			//    Remove(m);
			//  }
			//}
			foreach (var m in sensors)
			{
				if (m.Equals(sensor))
				{
					Remove(m);
				}
			}
    }

    /// <summary>
    /// Clears the workbench of all manifolds.
    /// </summary>
    public void Clear() {
			//foreach (Manifold m in manifolds.ToArray())	{
			foreach (Sensor s in sensors.ToArray()) {
        Remove(s);
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
				//foreach (var m in manifolds) {
				//  var p = m.primarySensor as GaugeDeviceSensor;
				//  var s = m.secondarySensor as GaugeDeviceSensor;

				//  return gd.ContainsSensor(p) || gd.ContainsSensor(s);
				//}
				foreach (var s in sensors)
				{
					var p = s as GaugeDeviceSensor;
					var l = s.linkedSensor as GaugeDeviceSensor;

					return gd.ContainsSensor(p) || gd.ContainsSensor(s);
				}
      }

      return false;
    }
		/// <summary>
		/// Gets the index of the device.
		/// </summary>
		/// <returns>The device index.</returns>
		/// <param name="serialNumber">Serial number.</param>
		/// <param name="unitCode">Unit code.</param>
		public int GetDeviceIndex(ISerialNumber serialNumber, int sensorIndex){
      if (serialNumber != null) {
				//for (int i = 0; i < manifolds.Count; i++) {
				for(int i = 0; i < sensors.Count; i++) {
					//var p = manifolds[i].primarySensor as GaugeDeviceSensor;
					var p = sensors[i] as GaugeDeviceSensor;
					
					if(p != null){
	          if(serialNumber.rawSerial == p.device.serialNumber.rawSerial && p.index == sensorIndex){
							return i;
						};
					}

        }
      }
			return -1;
		}
    /// <summary>
    /// Queries whether or not the workbench contains the given sensor as a primary
    /// sensor to a manifold that exists within the workbench.
    /// </summary>
    /// <returns><c>true</c>, if sensor was containsed, <c>false</c> otherwise.</returns>
    /// <param name="sensor">Sensor.</param>
    public bool ContainsSensor(Sensor sensor) {
			//foreach (Manifold manifold in manifolds) {
			foreach (Sensor checkSensor in sensors) {
        if (checkSensor.Equals(sensor)) {
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
				//var toRemove = new List<Manifold>();
				var toRemove = new List<Sensor>();

				//foreach (var m in manifolds) {
				//var p = m.primarySensor as GaugeDeviceSensor;
				//var s = m.secondarySensor as GaugeDeviceSensor;
				foreach (var s in sensors){
					var p = s as GaugeDeviceSensor;
					var l = s.linkedSensor as GaugeDeviceSensor;
          if (gd.ContainsSensor(p)) {
            toRemove.Add(s);  
          } else if (gd.ContainsSensor(s)) {
            s.SetLinkedSensor(null);
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
		//private void NotifyOfEvent(WorkbenchEvent.EType type, Manifold manifold, int index, int otherIndex = -1){
		private void NotifyOfEvent(WorkbenchEvent.EType type, Sensor sensor, int index, int otherIndex = -1) {
      if (onWorkbenchEvent != null) {
				//onWorkbenchEvent(new WorkbenchEvent(type, this, manifold, index, otherIndex));
				onWorkbenchEvent(new WorkbenchEvent(type, this, sensor, index, otherIndex));
      }
    }

		//private void NotifyOfEvent(WorkbenchEvent.EType type, ManifoldEvent manifoldEvent){
	  private void NotifyOfEvent(WorkbenchEvent.EType type, ManifoldEvent manifoldEvent) {
      if (onWorkbenchEvent != null) {
				//var m = manifoldEvent.manifold;
				var m = manifoldEvent.sensor;
        //onWorkbenchEvent(new WorkbenchEvent(type, this, m, sensors.IndexOf(m), manifoldEvent));
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

