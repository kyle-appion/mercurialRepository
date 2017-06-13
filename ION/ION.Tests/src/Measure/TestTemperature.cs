namespace Measurement {
  
  using NUnit.Framework;

  using Measure;

  [TestFixture]
  public class TestTemperature : BaseTest {
    [Test]
    public void TestKelvinToCelsius() {
      var unit = Units.Temperature.KELVIN.OfScalar(200);
      AssertEquals("Failed to convert K to C", -73.15, unit.ConvertTo(Units.Temperature.CELSIUS).magnitude);

      unit = Units.Temperature.CELSIUS.OfScalar(75);
      AssertEquals("Failed to convert C to K", 348.15, unit.ConvertTo(Units.Temperature.KELVIN).magnitude);
    }

    [Test]
    public void TestKelvinToFahrenheit() {
      var unit = Units.Temperature.KELVIN.OfScalar(200);
      AssertEquals("Failed to convert K to F", -99.67, unit.ConvertTo(Units.Temperature.FAHRENHEIT).magnitude);

      unit = Units.Temperature.FAHRENHEIT.OfScalar(75);
      AssertEquals("Failed to convert F to K", 297.039, unit.ConvertTo(Units.Temperature.KELVIN).magnitude);
    }


    [Test]
    public void TestCelsiusDerivative() {
      var standardUnit = Units.Temperature.CELSIUS.ToStandardUnit();
      var der = standardUnit.Derivative();
      AssertEquals(1, der.Convert(1));
    }

    [Test]
    public void TestFahrenheitDerivative() {
      var standardUnit = Units.Temperature.FAHRENHEIT.ToStandardUnit();
      var der = standardUnit.Derivative();
      AssertEquals(5.0 / 9.0, der.Convert(1));
    }
  }
}

