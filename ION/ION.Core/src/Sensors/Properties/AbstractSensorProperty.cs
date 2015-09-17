using System;
using System.IO;
using System.Runtime.Serialization;

using ION.Core.App;
using ION.Core.Devices;
using ION.Core.IO;
using ION.Core.Measure;
using ION.Core.Sensors;
using ION.Core.Util;

namespace ION.Core.Sensors.Properties {
  /// <summary>
  /// A common implementation of a sensor property that will allow for quick
  /// implementation for a sensor property and provides common utility methods.
  /// </summary>
  /// <remarks>
  /// As part of the implementation of this class, the modifiedMeasurement will
  /// automatically be "set" when the sensors measurement is changed. Thus, if
  /// you wish to react to changes within the property sensor, simply resolve
  /// them in modifiedMeasurement.set.
  /// </remarks>
/*
  [DataContract(Name="AbstractSensorProperty")]
  [KnownType(typeof(AlternateUnitSensorProperty))]
  [KnownType(typeof(HoldSensorProperty))]
  [KnownType(typeof(MinSensorProperty))]
  [KnownType(typeof(MaxSensorProperty))]
*/
  public abstract class AbstractSensorProperty : ISensorProperty {
    // Overridden from ISensorProperty
    public event OnSensorPropertyChanged onSensorPropertyChanged;

    // Overridden from ISensorProperty
    public Sensor sensor { get; private set; }

    // Overridden from ISensorProperty
    public virtual Scalar modifiedMeasurement {
      get {
        return sensor.measurement;
      }
      protected set {
        // Nope
      }
    }

    /// <summary>
    /// This is EXCLUSSIVELY used for serialization. I hate it too, but it c# would
    /// have offered a custom fucking [de]serialization method for a property, then
    /// we wouldn't have to deal with this.
    /// </summary>
    /// <value>The serialized sensor.</value>
    private byte[] serializedSensor { get; set; }

    /// <summary>
    /// Used by the serialization system.
    /// </summary>
    public AbstractSensorProperty() {
      // Nope
    }

    public AbstractSensorProperty(Sensor sensor) {
      this.sensor = sensor;
      Reset();
      this.sensor.onSensorStateChangedEvent += OnSensorChangedDelegate;
    }

    // Overridden from ISensorProperty
    public void Dispose() {
      sensor.onSensorStateChangedEvent -= OnSensorChangedDelegate;
    }

    // Overridden from ISensorProperty
    public virtual void Reset() {
      modifiedMeasurement = sensor.measurement;
    }

    /// <summary>
    /// The callback that will set the sensor's modified measurement to the
    /// sensor's new reading.
    /// </summary>
    /// <param name="sensor">Sensor.</param>
    private void OnSensorChangedDelegate(Sensor sensor) {
      modifiedMeasurement = sensor.measurement;
      NotifyChanged();
    }

    /// <summary>
    /// Notifies the properties onSensorPropertyChanged event that the property
    /// has changed.
    /// </summary>
    protected void NotifyChanged() {
      if (onSensorPropertyChanged != null) {
        onSensorPropertyChanged(this);
      }
    }
/*

    [OnSerializing]
    protected virtual void OnPrepareForSerialization(StreamingContext context) {
      serializedSensor = __SerializeSensor(sensor);
    }

    [OnDeserialized]
    protected virtual void OnPostDeserialized() {
      // TODO ahodder@appioninc.com: I would MUCH rather have the context provide the ION
      // application object, but c# may just single handly be the worst interpreted language
      // of all history that 

      sensor = __DeserializeSensor(ion, serializedSensor);
    }

    protected byte[] __SerializeSensor(Sensor sensor) {
      var stream = new MemoryStream();
      var writer = new BinaryWriter(stream);

      if (sensor is GaugeDeviceSensor) {
        var ds = (GaugeDeviceSensor)sensor;
        writer.Write((int)EPersistedSensorType.GaugeDeviceSensor);
        writer.Write(ds.device.serialNumber.rawSerial);
        writer.Write(ds.index);
      } else if (sensor is Sensor) {
        var serializer = new DataContractSerializer(typeof(Scalar));

        writer.Write((int)EPersistedSensorType.ManualSensor);
        writer.Write((int)sensor.type);
        writer.Write(sensor.isRelative);
        writer.Write(sensor.isEditable);
        serializer.WriteObject(stream, sensor.measurement);
        serializer.WriteObject(stream, sensor.minMeasurement);
        serializer.WriteObject(stream, sensor.maxMeasurement);
        writer.Write(sensor.name);
      } else {
        writer.Write((int)EPersistedSensorType.NullSensor);
      }

      writer.Flush();
      return stream.ToArray();
    }

    protected Sensor __DeserializeSensor(IION ion, byte[] data) {
      try {
      var stream = new MemoryStream(data);
      var reader = new BinaryReader(stream);

      var type = (EPersistedSensorType)reader.ReadInt32();
        if (EPersistedSensorType.GaugeDeviceSensor == type) {
          var rawSerial = reader.ReadString();
          var index = reader.ReadInt32();
          var serial = GaugeSerialNumber.Parse(rawSerial);
          var device = ion.deviceManager[serial];

          return ((GaugeDevice)device)[index];
        } else if (EPersistedSensorType.GaugeDeviceSensor == type) {
          var serializer = new DataContractSerializer(typeof(Scalar));

          var sensorType = (ESensorType)reader.ReadInt32();
          var isRelative = reader.ReadBoolean();
          var isEditable = reader.ReadBoolean();
          Scalar measurement, minMeasurement, maxMeasurement;
          try {
            measurement = serializer.ReadObject(stream) as Scalar;
            minMeasurement = serializer.ReadObject(stream) as Scalar;
            maxMeasurement = serializer.ReadObject(stream) as Scalar;
          } catch (Exception e) {
            Log.E(this, "Failed to inflate measurement", e); 
          }
          var name = reader.ReadString();

          var ret = new Sensor(sensorType, isRelative, isEditable);
          ret.measurement = measurement;
          ret.minMeasurement = minMeasurement;
          ret.maxMeasurement = maxMeasurement;
          ret.name = name;

          return ret;
        } else {
          return null;
        }
      } catch (Exception e) {
        Log.E(this, "Failed to deserialize sensor");
      }
    }



    private enum EPersistedSensorType {
      GaugeDeviceSensor,
      ManualSensor,
      NullSensor,
    }
*/
  }
}

