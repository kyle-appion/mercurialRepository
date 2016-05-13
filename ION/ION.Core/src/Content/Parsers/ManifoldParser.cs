namespace ION.Core.Content.Parsers {

  using System;
  using System.IO;
  using System.Runtime.Serialization;

  using ION.Core.App;
  using ION.Core.Content;
  using ION.Core.Devices;
  using ION.Core.Fluids;
  using ION.Core.IO;
  using ION.Core.Measure;
  using ION.Core.Sensors;
  using ION.Core.Sensors.Properties;
  using ION.Core.Util;

  /// <summary>
  /// The first version of the manifold parser.
  /// </summary>
  public class ManifoldParser : IParser<Manifold> {

    // Overridden from IParser
    public int version { get { return 1; } }

    // Overridden from IParser
    public void WriteToStream(IION ion, Manifold manifold, Stream stream) {
      var writer = new BinaryWriter(stream);
      try {
        // Persist the version of the parser
        writer.Write(version);
        // Write primary sensor
        WriteSensor(manifold.primarySensor, writer);
        // Write secondary sensor
        WriteSensor(manifold.secondarySensor, writer);

        // Write fluid name
        if (manifold.ptChart == null) {
          writer.Write(false); // Doens't have a pt chart.
        } else {
          writer.Write(true); // Does have a pt chart
          writer.Write(Enum.GetName(typeof(Fluid.EState), manifold.ptChart.state));
          if (manifold.ptChart.fluid == null) {
            writer.Write("");
          } else {
            writer.Write(manifold.ptChart.fluid.name);
          }
        }

        writer.Write(manifold.sensorProperties.Count);
        // Write sensor properties
        foreach (var sp in manifold.sensorProperties) {
          WriteSensorProperty(sp, writer);
        }
      } catch (Exception e) {
        Log.E(this, "Failed to write manifold to stream.", e);
      } finally {
        if (writer != null) {
          writer.Flush();
        }
      }
    }

    // Overridden from IParser
    public Manifold ReadFromStream(IION ion, Stream stream) {
      var reader = new BinaryReader(stream);
      try {
        var version = reader.ReadInt32();

        // TODO ahodder@appioninc.com: Implement version checking
        // Throw or delegate on version difference
        if (this.version != version) {
          throw new IOException("Cannot read manifold from stream: expected version " + this.version + " received " + version);
        }

        // Read the primary sensor
        Sensor primary = ReadSensor(ion, reader);
        // Assert that the primary sensor is not null;
        if (primary == null) {
          throw new IOException("Cannot load manifold: primary sensor not read");
        }

        // Read the secondary sensor
        Sensor secondary = ReadSensor(ion, reader);

        // Create the inflated manifold
        var ret = new Manifold(primary);
        ret.SetSecondarySensor(secondary);

        if (reader.ReadBoolean()) {
          var state = (Fluid.EState)Enum.Parse(typeof(Fluid.EState), reader.ReadString());
          // Read the fluid name for the manifold
          var fluidName = reader.ReadString();
          Fluid fluid = null;

          if (fluidName != null && !fluidName.Equals("")) {
            fluid = ion.fluidManager.GetFluidAsync(fluidName).Result;
          }

          ret.ptChart = PTChart.New(ion, state, fluid);
        }

        // Read sensor properties
        var propCount = reader.ReadInt32();

        for(int i = 0; i < propCount; i++){
          ReadSensorProperty(ret, reader);
        }
        return ret;
      } catch (Exception e) {
        Log.E(this, "Failed to read manifold from file.", e);
        return null;
      } finally {
      }
    }

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
        var stream = writer.BaseStream;

        writer.Write((int)EPersistSensorType.ManualSensor);

        writer.Write((int)sensor.type);

        writer.Write(sensor.isRelative);

        writer.Write(UnitLookup.GetCode(sensor.measurement.unit));
        writer.Write(sensor.measurement.amount);

        if (sensor.minMeasurement.unit == null) {
          writer.Write(0);
        } else {
          writer.Write(UnitLookup.GetCode(sensor.minMeasurement.unit));
          writer.Write(sensor.minMeasurement.amount);
        }

        if (sensor.maxMeasurement.unit == null) {
          writer.Write(0);
        } else {
          writer.Write(UnitLookup.GetCode(sensor.maxMeasurement.unit));
          writer.Write(sensor.maxMeasurement.amount);
        }

        if (String.IsNullOrEmpty(sensor.name)) {
          sensor.name = "Manual";
          writer.Write(sensor.name);
        } else {
          writer.Write(sensor.name);
        }
      } else {
        Log.D(this, "sensor is a null sensor");
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
        Log.D(this, "Rawserial: " + rawSerialNumber);
        var serialNumber = GaugeSerialNumber.Parse(rawSerialNumber);
        var index = reader.ReadInt32();
        var device = (GaugeDevice)ion.deviceManager[serialNumber];

        return device[index];
      } else if (EPersistSensorType.ManualSensor == type) {
        var sensorType = (ESensorType)reader.ReadInt32();
        var isRelative = reader.ReadBoolean();
        var measurementUnit = reader.ReadInt32();
        var measurement = reader.ReadDouble();
        var ret = new ManualSensor(sensorType, isRelative);
        var measurementScalar = new Scalar(UnitLookup.GetUnit(measurementUnit),measurement);       
        ret.measurement = measurementScalar;

        var minMeasurementUnit = reader.ReadInt32();
        if (minMeasurementUnit != 0) {
          var minMeasurement = reader.ReadDouble();
          var minMeasurementScalar = new Scalar(UnitLookup.GetUnit(minMeasurementUnit),minMeasurement);
          ret.minMeasurement = minMeasurementScalar;
        } else {
          ret.minMeasurement = new Scalar(UnitLookup.GetUnit(measurementUnit), 0.0);
        }
        var maxMeasurementUnit = reader.ReadInt32();

        if (maxMeasurementUnit != 0) {
          var maxMeasurement = reader.ReadDouble();
          var maxMeasurementScalar = new Scalar(UnitLookup.GetUnit(maxMeasurementUnit),maxMeasurement);
          ret.maxMeasurement = maxMeasurementScalar;
        } else {
          ret.maxMeasurement = new Scalar(UnitLookup.GetUnit(measurementUnit), 0.0);
        }

        var name = reader.ReadString();

        ret.name = name;
        return ret;
      } else {
        return null;
      }
    }

    private void WriteSensorProperty(ISensorProperty property, BinaryWriter writer) {      
      var name = property.GetType();
        writer.Write(name.Name);
    }

    private void ReadSensorProperty(Manifold manifold, BinaryReader reader) {
      var name = reader.ReadString();
     
      if (name != null) {
        if (name.Equals("MinSensorProperty")) {
          manifold.AddSensorProperty(new MinSensorProperty(manifold.primarySensor));
        } else if (name.Equals("MaxSensorProperty")) {
          manifold.AddSensorProperty(new MaxSensorProperty(manifold.primarySensor));
        } else if (name.Equals("PTChartSensorProperty")) {
          manifold.AddSensorProperty(new PTChartSensorProperty(manifold));
        } else if (name.Equals("RateOfChangeSensorProperty")) {
          manifold.AddSensorProperty(new RateOfChangeSensorProperty(manifold.primarySensor));
        } else if (name.Equals("SecondarySensorProperty")) {
          manifold.AddSensorProperty(new SecondarySensorProperty(manifold));
        } else if (name.Equals("SuperheatSubcoolSensorProperty")) {
          manifold.AddSensorProperty(new SuperheatSubcoolSensorProperty(manifold));
          //Log.D(this,"");
        } else if (name.Equals("TimerSensorProperty")) {
          manifold.AddSensorProperty(new TimerSensorProperty(manifold.primarySensor));
        } else if (name.Equals("HoldSensorProperty")) {
          manifold.AddSensorProperty(new HoldSensorProperty(manifold.primarySensor));
        } else if (name.Equals("AlternateUnitSensorProperty")) {
          //TODO setup alternate sensor writing with name and unit code 
          //This will be reader.ReadString() with reader.ReadInt32() right after
          Log.E(this, "Trying to add alternate sensor property. Not implemented yet");
        } else {
          Log.E(this, "Name for sensor property doesn't match an ISensorProperty");
        }
      } else {
        Log.E(this, "Couldn't read the name from the binary file for the sensor property");
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
  }
}

