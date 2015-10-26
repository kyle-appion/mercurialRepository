using System;

using ION.Core.Connections;
using ION.Core.Devices.Protocols;
using ION.Core.Sensors;
using ION.Core.Util;

namespace ION.Core.Devices {
  /// <summary>
  /// A GaugeDevice is a device that contains 1 or more sensors.
  /// </summary>
  public class GaugeDevice : IDevice {
    /// <summary>
    /// The timespan that a device should have communicated within to be
    /// considered nearby.
    /// </summary>
    private static readonly TimeSpan TIMEOUT_NEARBY = TimeSpan.FromMilliseconds(5000);

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
    // Overridden from IDevice
//    public bool isKnown { get { return deviceManager.knownDevices.Contains(this); } }

    /// <summary>
    /// The device manager who is managing this device.
    /// </summary>
//    private IDeviceManager deviceManager { get; set; }
    /// <summary>
    /// The sensors that are contained within the gauge.
    /// </summary>
    public GaugeDeviceSensor[] sensors { get; internal set; }
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

    public GaugeDevice(GaugeSerialNumber serialNumber, IConnection connection, IGaugeProtocol protocol) {
      __serialNumber = serialNumber;
      this.connection = connection;
      __protocol = protocol;
      name = serialNumber.ToString();

      connection.onStateChanged += ((IConnection conn, EConnectionState state) => {
        NotifyOfDeviceEvent(DeviceEvent.EType.ConnectionChange);
        foreach (GaugeDeviceSensor sensor in sensors) {
          sensor.NotifySensorStateChanged();
        }
      });

      connection.onDataReceived += ((IConnection conn, byte[] packet) => {
        HandlePacket(packet);
      });
    }

    // Overridden from IDevice
    public void Dispose() {
      // TODO ahodder@appioninc.com: Implement this and release all callbacks
    }

    // Overridden from IDevice
    public void HandlePacket(byte[] packet) {
      ION.Core.App.AppState.context.PostToMain(() => {
        try {
          GaugePacket gp = __protocol.ParsePacket(packet);

          if (sensorCount == gp.gaugeReadings.Length) {
            battery = gp.battery;

            var changed = false;

            for (int i = 0; i < sensorCount; i++) {
              GaugeReading reading = gp.gaugeReadings[i];
              if (reading.sensorType != this[i].type) {
                throw new ArgumentException("Cannot set device sensor measurement: Sensor at " + i + " of type " + 
                  this[i].type + " is not valid with received type " + reading.sensorType);
              }
              if (this[i].measurement != gp.gaugeReadings[i].reading) {
                this[i].SetMeasurement(gp.gaugeReadings[i].reading);
                changed = true;
              }
            }

            if (changed) {
              NotifyOfDeviceEvent(DeviceEvent.EType.NewData);
            }
          } else {
            throw new ArgumentException("Failed to resolve packet: Expected " + sensorCount + " sensor data input, received: " + gp.gaugeReadings.Length);
          }
        } catch (Exception e) {
          // TODO ahodder@appioninc.com: Consider exposing?
          //          Log.E(this, "Cannot resolve packet: unresolved exception {packet=> " + packet.ToByteString() + "}", e);
        }
      });
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
    /// Queries whether or not the device has a sensor of the given type.
    /// </summary>
    /// <returns><c>true</c> if this instance hash sensor of type the specified sensorType; otherwise, <c>false</c>.</returns>
    /// <param name="sensorType">Sensor type.</param>
    public bool HashSensorOfType(ESensorType sensorType) {
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
      // TODO ahodder@appioninc.com:  eeehhhhhhh. This no gud, make gudder
      ION.Core.App.AppState.context.PostToMain(() => {
        if (onDeviceEvent != null) {
          onDeviceEvent(new DeviceEvent(type, this));
        }
      });
    }
  }
}

