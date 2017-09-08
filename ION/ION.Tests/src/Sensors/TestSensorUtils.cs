/*

using System;

using NUnit.Framework;

using ION.Core.Measure;
using ION.Core.Sensors;

namespace ION.TestFixtures.Sensors {
  [TestFixture]
  public class TestSensorUtils {
    [Test]
    public void TestIsCompatibleWith_ValidUnit() {
      Assert.True(SensorUtils.IsCompatibleWith(ESensorType.Length, Units.Length.FOOT));
    }

    [Test]
    public void TestIsCompatibleWith_DefaultIsFalse() {
      Assert.False(SensorUtils.IsCompatibleWith(ESensorType.Unknown, Units.Force.NEWTON));
    }

    [Test]
    public void TestIsCompatibleWith_AllDefaultPressureUnits() {
      foreach (Unit unit in SensorUtils.DEFAULT_PRESSURE_UNITS) {
        Assert.True(SensorUtils.IsCompatibleWith(ESensorType.Pressure, unit));
      }
    }

    [Test]
    public void TestIsCompatibleWith_AllDefaultTemperatureUnits() {
      foreach (Unit unit in SensorUtils.DEFAULT_TEMPERATURE_UNITS) {
        Assert.True(SensorUtils.IsCompatibleWith(ESensorType.Temperature, unit));
      }
    }

    [Test]
    public void TestIsCompatibleWith_AllDefaultVacuumUnits() {
      foreach (Unit unit in SensorUtils.DEFAULT_VACUUM_UNITS) {
        Assert.True(SensorUtils.IsCompatibleWith(ESensorType.Vacuum, unit));
      }
    }
  }
}

*/