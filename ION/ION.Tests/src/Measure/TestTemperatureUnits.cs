using System;

using NUnit.Framework;

using ION.Core.Measure;

namespace ION.TestFixtures.Measure {
  [TestFixture]
  public class TestTemperatureUnits {
    private static readonly Unit KEL = Units.Temperature.KELVIN;
    private static readonly Unit CEL = Units.Temperature.CELSIUS;
    private static readonly Unit FAH = Units.Temperature.FAHRENHEIT;

    private static readonly double EPSILON = 0.01;
    private void AssertEquals(double expected, double received) {
      Assert.AreEqual(expected, received, EPSILON);
    }

    [Test]
    public void TestCelsiusToKelvin() {
      AssertEquals(283.15, CEL.OfScalar(10).ConvertTo(KEL).amount);
    }

    [Test]
    public void TestKelvinToCelsius() {
      AssertEquals(10, KEL.OfScalar(283.15).ConvertTo(CEL).amount);
    }

    [Test]
    public void TestFahrenheitToKelvin() {
      AssertEquals(260.928, FAH.OfScalar(10).ConvertTo(KEL).amount);
    }

    [Test]
    public void TestKelvinToFahrenheit() {
      AssertEquals(10, KEL.OfScalar(260.928).ConvertTo(FAH).amount);
    }
  }
}
