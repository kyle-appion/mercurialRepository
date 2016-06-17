/*


using System;

using NUnit.Framework;

using ION.Core.Content;
using ION.Core.Content.Parsers;
using ION.Core.Measure;
using ION.Core.Sensors;

namespace ION.TestFixtures.Content {
  
  [TestFixture]
  public class TestManifoldSerialization {

    private const string SENSOR_NAME = "sensor";

    [Test]
    public void TestSerializeAndDeserializeManual() {
      Sensor sensor = new ManualSensor(ESensorType.Pressure, true);
      sensor.name = SENSOR_NAME;
      sensor.measurement = Units.Pressure.PSIG.OfScalar(100);
      sensor.minMeasurement = Units.Pressure.PSIG.OfScalar(10);
      sensor.maxMeasurement = Units.Pressure.PSIG.OfScalar(150);

      var manifold = new Manifold(sensor);
      var mp = new ManifoldParser();
    }
  }
}

*/