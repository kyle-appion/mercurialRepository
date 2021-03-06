﻿namespace ION.Core.Devices {

  using System;
  using System.Collections.Generic;

	using Appion.Commons.Util;

  using ION.Core.App;
  using ION.Core.Connections;
  using ION.Core.Devices.Protocols;
  using ION.Core.Sensors;

  /// <summary>
  /// A GaugeDevice is a device that contains 1 or more sensors.
  /// </summary>
  public class GaugeDevice : IDevice {
    /// <summary>
    /// The timespan that a device should have communicated within to be
    /// considered nearby.
    /// </summary>
    private static readonly TimeSpan TIMEOUT_NEARBY = TimeSpan.FromMilliseconds(5000);

		private static readonly TimeSpan MAX_UPDATE_DELAY = TimeSpan.FromSeconds(5);

    // Overridden from IDevice
    public event OnDeviceEvent onDeviceEvent;
    // Overridden from IDevice
    public ISerialNumber serialNumber { get { return __serialNumber; } } GaugeSerialNumber __serialNumber;
    // Overridden from IDevice
    public EDeviceType type { get { return __serialNumber.deviceType; } }
    // Overridden from IDevice
    public string name {
      get {
        return __name;
      }
      set {
        __name = value;
        NotifyOfDeviceEvent(DeviceEvent.EType.NameChanged);
      }
    } string __name;
    // Overridden from IDevice
    public int battery { get; private set; }
    // Overridden from IDevice
    public IConnection connection { get; private set; }
    // Overridden from IDevice
    public IProtocol protocol { get { return __protocol; } } IGaugeProtocol __protocol;
    // Overridden from IDevice
    public bool isConnected { get { return EConnectionState.Connected == connection.connectionState; } }
    // Overridden from IDevice
    public bool isNearby {
      get {
        return DateTime.Now - connection.lastSeen <= TIMEOUT_NEARBY;
      }
    }
    /// <summary>
    /// The sensors that are contained within the gauge.
    /// </summary>
    public GaugeDeviceSensor[] sensors {
      get {
        return __sensors;
      }
      internal set {
        __sensors = value;
        removedStates = new bool[__sensors.Length];
      }
    } GaugeDeviceSensor[] __sensors;
    /// <summary>
    /// Queries the number of sensors that are present in the gauge.
    /// </summary>
    public int sensorCount { get { return (sensors != null) ? sensors.Length : 0; } }
    /// <summary>
    /// Queries the sensor at the given index.
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public GaugeDeviceSensor this[int index] {
      get {
        return sensors[index];
      }
      private set {
        sensors[index] = value;
      }
    }
    /// <summary>
    /// Maintains a history of whether or not a sensor was removed during the last packet.
    /// </summary>
    /// <value>The removed states.</value>
    private bool[] removedStates { get ; set; }

		private DateTime lastNotify;

    public GaugeDevice(GaugeSerialNumber serialNumber, IConnection connection, IGaugeProtocol protocol) {
      __serialNumber = serialNumber;
      this.connection = connection;
      __protocol = protocol;
      name = serialNumber.ToString();

      connection.onStateChanged += ((IConnection conn, EConnectionState state) => {
        NotifyOfDeviceEvent(DeviceEvent.EType.ConnectionChange);
        foreach (GaugeDeviceSensor sensor in sensors) {
			    sensor.NotifyInvalidated();
        }
      });

      connection.onDataReceived += ((IConnection conn, byte[] packet) => {
        HandlePacket(packet);
      });

			lastNotify = DateTime.Now;
    }

		public override string ToString() {
			return string.Format("[GaugeDevice: serialNumber={0}, type={1}, name={2}, battery={3}, connection={4}, protocol={5}, isConnected={6}, isNearby={7}, sensors={8}, sensorCount={9}, this[]={10}]", serialNumber, type, name, battery, connection, protocol, isConnected, isNearby, sensors, sensorCount, this.sensors);
		}

    // Overridden from IDevice
    public void Dispose() {
      connection.Disconnect();
    }

    /// <Docs>To be added.</Docs>
    /// <para>Returns the sort order of the current instance compared to the specified object.</para>
    /// <summary>
    /// Compares to.
    /// </summary>
    /// <returns>The to.</returns>
    /// <param name="other">Other.</param>
    public int CompareTo(IDevice other) {
      return serialNumber.CompareTo(other.serialNumber);
    }

    // Overridden from IDevice
    public void HandlePacket(byte[] packet) {
      HandlePacketInternal(packet);
    }

		private DateTime last;
    private void HandlePacketInternal(byte[] packet) {
      if (packet == null) {
        return;
      }
      try {
				GaugePacket gp = __protocol.ParsePacket(packet);

        int oldBattery = battery;
        battery = gp.battery;

        var changed = oldBattery != battery;

        for (int i = 0; i < sensorCount; i++) {
          var reading = gp.gaugeReadings[i];    
          var sensor = this[i];

          if (reading.sensorType != sensor.type) {
            return;
          }

          if (sensor.measurement != gp.gaugeReadings[i].reading) {
            sensor.SetMeasurement(gp.gaugeReadings[i].reading);
            changed = true;
          }

          sensor.removed = reading.removed;
          var removedChanged = sensor.removed != removedStates[i];
          removedStates[i] = sensor.removed;

          if (removedChanged) {
            sensor.NotifyInvalidated();
          }
        }

				if (changed || DateTime.Now - lastNotify > MAX_UPDATE_DELAY) {
					last = DateTime.Now;
          NotifyOfDeviceEvent(DeviceEvent.EType.NewData);
					lastNotify = DateTime.Now;
        }
      } catch (Exception) {
				NotifyOfDeviceEvent(DeviceEvent.EType.NewData);
      }
    }

    /// <summary>
    /// Queries whether or not the device contians the given sensor.
    /// </summary>
    /// <returns><c>true</c>, if sensor was containsed, <c>false</c> otherwise.</returns>
    /// <param name="sensor">Sensor.</param>
    public bool ContainsSensor(Sensor sensor) {
      var gs = sensor as GaugeDeviceSensor;

      if (gs != null) {
        foreach (var s in sensors) {
          if (gs.Equals(s)) {
            return true;
          }
        }
      }

      return false;
    }

    /// <summary>
    /// Queries whether or not the gauge device has a sensor matching the given filter.
    /// </summary>
    /// <returns><c>true</c> if this instance hash sensor matching filter the specified filter; otherwise, <c>false</c>.</returns>
    /// <param name="filter">Filter.</param>
    public bool HasSensorMatchingFilter(IFilter<Sensor> filter) {
      foreach (var sensor in sensors) {
        if (filter.Matches(sensor)) {
          return true;
        }
      }

      return false;
    }

    /// <summary>
    /// Queries all of the sensors in the device that match the filter. Note: this may
    /// return an emtpy list.
    /// </summary>
    /// <returns>The sensors matching filter.</returns>
    /// <param name="filter">Filter.</param>
    public List<Sensor> GetSensorsMatchingFilter(IFilter<Sensor> filter) {
      var ret = new List<Sensor>();

      foreach (var sensor in sensors) {
        if (filter == null || filter.Matches(sensor)) {
          ret.Add(sensor);
        }
      }

      return ret;
    }

		/// <summary>
		/// Queries the index of the sensor or -1 if the sensor is not in the gauge.
		/// </summary>
		/// <returns>The of sensor.</returns>
		/// <param name="sensor">Sensor.</param>
		public int IndexOfSensor(GaugeDeviceSensor sensor) {
			for (int i = 0; i < sensorCount; i++) {
				if (sensors[i].Equals(sensor)) {
					return i;
				}
			}

			return -1;
		}

    /// <summary>
    /// Queries whether or not the device has a sensor of the given type.
    /// </summary>
    /// <returns><c>true</c> if this instance hash sensor of type the specified sensorType; otherwise, <c>false</c>.</returns>
    /// <param name="sensorType">Sensor type.</param>
		public bool HasSensorOfType(ESensorType sensorType) {
      foreach (var sensor in sensors) {
        if (sensorType == sensor.type) {
          return true;
        }
      }

      return false;
    }

    /// <summary>
    /// Notifies the device's onStateChange delegates that it has changed.
    /// </summary>
    private void NotifyOfDeviceEvent(DeviceEvent.EType type) {
      try {
        var ion = AppState.context;
        if (ion != null) {
          ion.PostToMain(() => {
            if (onDeviceEvent != null) {
              onDeviceEvent(new DeviceEvent(type, this));
            }
          });
        }
      } catch (Exception e) {
        Log.E(this, "FAILED TO POST DEVICE EVENT TO MAIN THREAD!!!!", e);
      }
    }
    
    public void SetBatteryRemoteDevice(int level){
			battery = level;
		}
  }
}

