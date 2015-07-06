using System;

using NUnit.Framework;

using ION.Core.Measure;
using ION.Core.Sensors;

namespace ION.TestFixtures.Sensors {
  [TestFixture]
  class TestSensor {
    [Test]
    public void TestReadingEvent_UpdatesOnChange() {
      Sensor sensor = new Sensor(ESensorType.Temperature, false, Units.Temperature.FAHRENHEIT.OfScalar(10));
    }
  }
}
