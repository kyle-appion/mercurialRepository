using System;

using NUnit.Framework;

using ION.Core.Measure;
using ION.Core.Sensors;

namespace ION.TestFixtures.Sensors {
  [TestFixture]
  class TestSensor {
    [Test]
    public void TestReadingEvent_UpdatesOnChange() {
      Sensor sensor = new ManualSensor(ESensorType.Temperature, false);
      sensor.measurement = Units.Temperature.FAHRENHEIT.OfScalar(10);
    }
  }
}
