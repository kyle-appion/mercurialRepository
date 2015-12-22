namespace ION.Droid.Sensors {

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

  /// <summary>
  /// An android specific construc that will allow a sensor to cross between activies as a result or
  /// intent data object.
  /// </summary>
  public class DefaultSensorParcelable : SensorParcelable {
    /// <summary>
    /// The sensor type of the sensor that was passed.
    /// </summary>
    /// <value>The type of the sensor.</value>
    public ESensorType sensorType { get; set; }
    /// <summary>
    /// Whether or not the sensor is relative.
    /// </summary>
    /// <value><c>true</c> if is relative; otherwise, <c>false</c>.</value>
    public bool isRelative { get; set; }
    /// <summary>
    /// Whether or not the sensor is editable.
    /// </summary>
    /// <value><c>true</c> if is editable; otherwise, <c>false</c>.</value>
    public bool isEditable { get; set; }
    /// <summary>
    /// The unit code for the unit that is being passed.
    /// </summary>
    /// <value>The unit code.</value>
    public int unitCode { get; set; }
    /// <summary>
    /// The quantity of the unit that is being passed.
    /// </summary>
    /// <value>The amount.</value>
    public double amount { get; set; }

    public DefaultSensorParcelable() {
    }

    protected DefaultSensorParcelable(Parcel source) {
      sensorType = (ESensorType)source.ReadInt();
      isRelative = source.ReadInt() == 1;
      isEditable = source.ReadInt() == 1;
      unitCode = source.ReadInt();
      amount = source.ReadDouble();
    }

    protected DefaultSensorParcelable(Sensor sensor) {
      if (sensor is GaugeDeviceSensor) {
        throw new InvalidOperationException("Do not use a DefaultSensorParcelable to pass a GaugeDeviceSensor. Please use a GaugeDeviceSensorParcelable instead.");
      }

      sensorType = sensor.type;
      unitCode = UnitLookup.GetCode(sensor.unit);
      amount = sensor.measurement.amount;
    }

    // Overridden from SensorParcelable
    public override void WriteToParcel(Parcel dest, ParcelableWriteFlags flags) {
      dest.WriteInt((ESensorType)sensorType);
      dest.WriteInt(isRelative ? 1 : 0);
      dest.WriteInt(isEditable ? 1 : 0);
      dest.WriteInt(unitCode);
      dest.WriteDouble(amount);
    }

    // Overridden from SensorParcelable
    public override Sensor Get() {
      var ret = new Sensor(sensorType, isRelative, isEditable);
      return null;
    }
  }

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

    // Overridden from SensorParcelable
    public override void WriteToParcel(Parcel dest, ParcelableWriteFlags flags) {
      dest.WriteString(serialNumber.ToString());
      dest.WriteInt(index);
    }

    // Overridden from SensorParcelable
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

