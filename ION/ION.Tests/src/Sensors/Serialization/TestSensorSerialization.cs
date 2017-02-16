/*

using System;

using Newtonsoft.Json;

using NUnit.Framework;

using ION.Core.Measure;
using ION.Core.Sensors;
using ION.Core.Sensors.Serialization;

namespace ION.TestFixwtures.Sensors.Serialization {
  [TestFixture]
  public class TestSensorSerialization {

    [Test]
    public void TestSensorFullSerialization() {
      var sensor = new ManualSensor(ESensorType.Pressure, false);
      sensor.name = "Duh, sensor";
      sensor.measurement = Units.Pressure.PSIA.OfScalar(100);
      sensor.minMeasurement = Units.Pressure.PSIA.OfScalar(10);
      sensor.maxMeasurement = Units.Pressure.PSIA.OfScalar(1000);

      var converter = new DefaultSensorJsonConverter();

      var json = JsonConvert.SerializeObject(sensor, converter);

      var uSensor = JsonConvert.DeserializeObject<Sensor>(json, converter); 

      Assert.Equals(sensor.measurement, uSensor.measurement);
      Assert.Equals(sensor.isRelative, uSensor.isRelative);
      Assert.Equals(sensor.isEditable, uSensor.isEditable);
      Assert.Equals(sensor.name, uSensor.name);
      Assert.Equals(sensor.minMeasurement, uSensor.minMeasurement);
      Assert.Equals(sensor.maxMeasurement, uSensor.maxMeasurement);
    }

    [Test]
    public void TestSensorPartialSerialization() {
      var sensor = new ManualSensor(ESensorType.Pressure, false);
      sensor.name = "Duh, sensor";
      sensor.measurement = Units.Pressure.PSIA.OfScalar(100);

      var converter = new DefaultSensorJsonConverter();

      var json = JsonConvert.SerializeObject(sensor, converter);

      var uSensor = JsonConvert.DeserializeObject<Sensor>(json, converter); 

      Assert.Equals(sensor.measurement, uSensor.measurement);
      Assert.Equals(sensor.isRelative, uSensor.isRelative);
      Assert.Equals(sensor.isEditable, uSensor.isEditable);
      Assert.Equals(sensor.name, uSensor.name);
      Assert.Equals(null, uSensor.minMeasurement);
      Assert.Equals(null, uSensor.maxMeasurement);
    }
  }
}

*/