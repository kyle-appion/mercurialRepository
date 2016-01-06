namespace ION.Droid {

  using System;

  using Android.OS;

  using ION.Core.App;
  using ION.Core.Devices;
  using ION.Core.Sensors;

  using ION.Droid.Util;

  /// <summary>
  /// An android specific construct that will allow a sensor to traverse the activity boundary.
  /// </summary>
  public abstract class SensorParcelable : GenericParcelable {
    public SensorParcelable() {
    }
    
    public abstract Sensor Get(IION ion);
  }
  /*
  public class GenericSensorParselable : SensorParcelable {
  }
  */

  /// <summary>
  /// An android specific construct that will allow a gauge device sensor to traverse the activity boundary.
  /// </summary>
  public class GaugeDeviceSensorParcelable : SensorParcelable {
    /// <summary>
    /// The serial number of the device that the sensor belongs to.
    /// </summary>
    /// <value>The serial number.</value>
    public ISerialNumber serialNumber { get; set; }
    /// <summary>
    /// The index where the sensor is within the gauge device.
    /// </summary>
    /// <value>The index.</value>
    public int index { get; set; }

    public GaugeDeviceSensorParcelable() {
    }

    public GaugeDeviceSensorParcelable(GaugeDeviceSensor sensor) {
      serialNumber = sensor.device.serialNumber;
      index = sensor.index;
    }

    protected GaugeDeviceSensorParcelable(Parcel source) {
      serialNumber = GaugeSerialNumber.Parse(source.ReadString());
      index = source.ReadInt();
    }

    // Overridden from GaugeDeviceSensorParcelable
    public override Sensor Get(IION ion) {
      var device = ion.deviceManager[serialNumber] as GaugeDevice;

      if (device != null) {
        return device[index];
      } else {
        return null;
      }
    }
  }
}

