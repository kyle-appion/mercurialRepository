namespace Measurement {

  using NUnit.Framework;

  using Measure;

  [TestFixture]
  public class TestScalar : BaseTest {
    [Test]
    public void TestScalarAddition() {
      var c = Units.Temperature.CELSIUS.OfScalar(50);
      var f = Units.Temperature.FAHRENHEIT.OfSpan(5);
      AssertEquals(52.777777, (c + f).magnitude); 
    }

    [Test]
    public void TestScalarSubtraction() {
      var k = Units.Temperature.KELVIN.OfScalar(300);
      var f = Units.Temperature.FAHRENHEIT.OfSpan(16);
      AssertEquals(291.11111, (k - f).magnitude);
    }

    [Test]
    public void TestScalarMultiplication() {
      var f = Units.Temperature.FAHRENHEIT.OfScalar(45);
      var k = Units.Temperature.KELVIN.OfSpan(10);
      AssertEquals(810.0, (f * k).magnitude);
    }

    [Test]
    public void TestScalarDivision() {
      var k = Units.Temperature.KELVIN.OfScalar(210);
      var f = Units.Temperature.FAHRENHEIT.OfSpan(6);
      AssertEquals(63, (k / f).magnitude);
    }
  }
}

