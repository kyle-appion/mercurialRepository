using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

using ION.Core.App;
using ION.Core.Devices;
using ION.Core.Fluids;
using ION.Core.IO;
using ION.Core.Measure;
using ION.Core.Sensors;
using ION.Core.Sensors.Properties;

namespace ION.Core.Content {
  /// <summary>
  /// A Manifold is an abstract representation of a collection of sensors that
  /// come together to create and "explain" a certain context. For example: a
  /// manifold consiting of a pressure and a temperature sensor create a context
  /// that allows for a Pressure/Temperature lookup chart and a Superheat/subcool
  /// reference measurement.
  /// </summary>
  // TODO ahodder@appioninc.com: This is pretty much fucking stupid. Way to go me
  public class Manifold : IDisposable {
    private const int VERSION = 1;

    /// <summary>
    /// The delegate that is call when a manifold'a context is changed. This
    /// can be as simple as a reading update or an entirely new context has
    /// been added to the manifold.
    /// </summary>
    /// <param name="manifold"></param>
    /// <param name="context"></param>
    public delegate void OnManifoldChanged(Manifold manifold);
    /// <summary>
    /// The event handler that is notified when the manifold changes.
    /// </summary>
    public event OnManifoldChanged onManifoldChanged;

    /// <summary>
    /// The primary sensor for the manifold.
    /// </summary>
    public Sensor primarySensor {
      get {
        return __primarySensor;
      }
      private set {
        if (__primarySensor != null) {
          __primarySensor.onSensorStateChangedEvent -= OnManifoldSensorChanged;
        }

        __primarySensor = value;

        if (__primarySensor != null) {
          __primarySensor.onSensorStateChangedEvent += OnManifoldSensorChanged;
          OnManifoldSensorChanged(__primarySensor);
        }
      }
    } Sensor __primarySensor;

    /// <summary>
    /// The secondary sensor for the manifold.
    /// </summary>
    public Sensor secondarySensor {
      get {
        return __secondarySensor;
      }

      set {
        if (__secondarySensor != null) {
          __secondarySensor.onSensorStateChangedEvent -= OnManifoldSensorChanged;
        }

        __secondarySensor = value;

        if (__secondarySensor != null) {
          __secondarySensor.onSensorStateChangedEvent += OnManifoldSensorChanged;
          OnManifoldSensorChanged(__secondarySensor);
        }
        NotifyChanged();
      }
    } Sensor __secondarySensor;

    /// <summary>
    /// The sensor properties that are within the manifold.
    /// </summary>
    /// <value>The sensor properties.</value>
    public List<ISensorProperty> manifoldProperties { 
      get {
        return new List<ISensorProperty>(__sensorProperties);
      }
    } List<ISensorProperty> __sensorProperties = new List<ISensorProperty>();

    /// <summary>
    /// The fluid that the manifold is expected to work with.
    /// </summary>
    public Fluid fluid {
      get {
        return __fluid;
      }
      set {
        __fluid = fluid;
        NotifyChanged();
      }
    } Fluid __fluid;

    /// <summary>
    /// Used when inflated from serialization.
    /// </summary>
    public Manifold() {
      // Nope
    }

    public Manifold(Sensor primarySensor) {
      this.primarySensor = primarySensor;
    }
    /*
    // Overridden from ISerializable
    public void Serialize(SerializationContext context, BinaryWriter writer) {
      // Persist the manifold version
      writer.Write(VERSION);

      // Write primary sensor
      WriteSensor(primarySensor, writer);

      // Write secondary sensor
      WriteSensor(secondarySensor, writer);

      // Write fluid name
      if (fluid == null) {
        writer.Write("");
      } else {
        writer.Write(fluid.name);
      }

      // Write manifold properties
      writer.Write(manifoldProperties.Count);
      foreach (var mp in manifoldProperties) {
        writer.Write(mp);
      }
    }

    // Overridden from ISerializable
    public void Deserialize(SerializationContext context, BinaryReader reader) {
      // Ensure version
      var version = reader.ReadInt32();
      if (VERSION != version) {
        throw new IndexOutOfRangeException("Cannot read manifold from stream: expected version: " + VERSION + " received " + version);
      }

      var ion = (IION)context.context;

      // Read primary sensor
      primarySensor = ReadSensor(ion, reader);

      // Read secondary sensor
      secondarySensor = ReadSensor(ion, reader);

      // Read fluid
      fluid = ion.fluidManager.GetFluidAsync(reader.ReadString()).Result;

      // Read manifold propertied
      var count = reader.ReadInt32();
      for (int i = 0; i < count; i++) {
        AddSensorProperty((ISensorProperty)reader.ReadObject());
      }
    }
    */

    // Overridden from IDispose
    public void Dispose() {
      primarySensor.onSensorStateChangedEvent -= OnManifoldSensorChanged;
      if (__secondarySensor != null) {
        __secondarySensor.onSensorStateChangedEvent -= OnManifoldSensorChanged;
      }
    }

    /// <summary>
    /// Adds the sensor property to the manifold if the manifold does not already have a
    /// sensor property of the given type.
    /// </summary>
    /// <param name="sensorProperty">Sensor property.</param>
    /// <returns>True if the property was added, false if the manifold already has the property.</returns>
    public bool AddSensorProperty(ISensorProperty sensorProperty) {
      if (HasSensorPropertyOfType(sensorProperty.GetType())) {
        return false;
      } else {
        __sensorProperties.Add(sensorProperty);
        return true;
      }
    }

    /// <summary>
    /// Removes the given sensor property from the manifold.
    /// </summary>
    /// <param name="sensorProperty">Sensor property.</param>
    public void RemoveSensorProperty(ISensorProperty sensorProperty) {
      __sensorProperties.Remove(sensorProperty);
    }

    /// <summary>
    /// Queries whether or not the manifold has a sensor property of the given type.
    /// </summary>
    /// <returns><c>true</c> if this instance has sensor property of type; otherwise, <c>false</c>.</returns>
    /// <typeparam name="T">The 1st type parameter.</typeparam>
    public bool HasSensorPropertyOfType(Type type) {
      foreach (var prop in manifoldProperties) {
        if (prop.GetType().Equals(type)) {
          return true;
        }
      }

      return false;
    }

    /// <summary>
    /// Called when a manifold's sensor's reading changes.
    /// </summary>
    /// <param name="sensor"></param>
    /// <param name="reading"></param>
    private void OnManifoldSensorChanged(Sensor sensor) {
      NotifyChanged();
    }

    /// <summary>
    /// Call to notify all known OnManifoldChanged delegates of a change to the
    /// manifold.
    /// </summary>
    private void NotifyChanged() {
      if (onManifoldChanged != null) {
        onManifoldChanged(this);
      }
    }
    /*
    /// <summary>
    /// Writes the sensor to stream.
    /// </summary>
    /// <param name="sensor">Sensor.</param>
    /// <param name="stream">Stream.</param>
    private void WriteSensor(Sensor sensor, BinaryWriter writer) {
      if (sensor is GaugeDeviceSensor) {
        var ds = (GaugeDeviceSensor)sensor;
        writer.Write((int)EPersistSensorType.GaugeDeviceSensor);
        writer.Write(ds.device.serialNumber.rawSerial);
        writer.Write(ds.index);
      } else if (sensor is Sensor) {
        var stream = writer.baseStream;
        var serializer = new DataContractSerializer(typeof(Scalar));

        writer.Write((int)EPersistSensorType.ManualSensor);
        writer.Write((int)sensor.type);
        writer.Write(sensor.isRelative);
        serializer.WriteObject(stream, sensor.measurement);
        serializer.WriteObject(stream, sensor.minMeasurement);
        serializer.WriteObject(stream, sensor.maxMeasurement);
        writer.Write(sensor.name);
      } else {
        writer.Write((int)EPersistSensorType.NullSensor);
      }
    }

    /// <summary>
    /// Reads out a Sensor from the given binary reader.
    /// </summary>
    /// <param name="ion">Ion.</param>
    /// <param name="reader">Reader.</param>
    private Sensor ReadSensor(IION ion, BinaryReader reader) {
      var type = (EPersistSensorType)reader.ReadInt32();

      if (EPersistSensorType.GaugeDeviceSensor == type) {
        var rawSerialNumber = reader.ReadString();
        var serialNumber = GaugeSerialNumber.Parse(rawSerialNumber);
        var index = reader.ReadInt32();
        var device = (GaugeDevice)ion.deviceManager[serialNumber];

        return device[index];
      } else if (EPersistSensorType.ManualSensor == type) {
        var serializer = new DataContractSerializer(typeof(Scalar));

        var sensorType = (ESensorType)reader.ReadInt32();
        var isRelative = reader.ReadBool();
        var measurement = (Scalar)serializer.ReadObject(reader.baseStream);
        var minMeasurement = (Scalar)serializer.ReadObject(reader.baseStream);
        var maxMeasurement = (Scalar)serializer.ReadObject(reader.baseStream);
        var name = reader.ReadString();

        var ret = new Sensor(sensorType, isRelative);
        ret.measurement = measurement;
        ret.minMeasurement = minMeasurement;
        ret.maxMeasurement = maxMeasurement;
        ret.name = name;

        return ret;
      } else {
        return null;
      }
    }

    /// <summary>
    /// An enumeration of the sensor types that the parser supports.
    /// </summary>
    private enum EPersistSensorType {
      /// <summary>
      /// Enumerates a null reference to a sensor. This is only allowed for a
      /// manifold's second sensor.
      /// </summary>
      NullSensor,
      ManualSensor,
      GaugeDeviceSensor,
    }
    */
  }
}

