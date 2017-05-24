namespace Measurement {

  using NUnit.Framework;

  using Measure;

  [TestFixture]
  public class TestPressure : BaseTest {
    [Test]
    public void TestPaToKpa() {
      var unit = Units.Pressure.PASCAL.OfScalar(1337);
      AssertEquals("Failed to convert Pa to kPa", 1.337, unit.ConvertTo(Units.Pressure.KILOPASCAL).magnitude);

      unit = Units.Pressure.KILOPASCAL.OfScalar(1337);
      AssertEquals("Failed to convert kPa to Pa", 1337000, unit.ConvertTo(Units.Pressure.PASCAL).magnitude);
    }

    [Test]
    public void TestPaToMPa() {
      var unit = Units.Pressure.PASCAL.OfScalar(1337);
      AssertEquals("Failed to convert Pa to MPa", .001337, unit.ConvertTo(Units.Pressure.MEGAPASCAL).magnitude);

      unit = Units.Pressure.MEGAPASCAL.OfScalar(1337);
      AssertEquals("Failed to convert MPa to Pa", 1337000000, unit.ConvertTo(Units.Pressure.PASCAL).magnitude);
    }

    [Test]
    public void TestPaToBar() {
      var unit = Units.Pressure.PASCAL.OfScalar(1337);
      AssertEquals("Failed to convert Pa to bar", 0.01337, unit.ConvertTo(Units.Pressure.BAR).magnitude);

      unit = Units.Pressure.BAR.OfScalar(0.125);
      AssertEquals("Failed to convert bar to Pa", 12500, unit.ConvertTo(Units.Pressure.PASCAL).magnitude);
    }


    [Test]
    public void TestPaToMillibar() {
      var unit = Units.Pressure.PASCAL.OfScalar(1337);
      AssertEquals("Failed to convert Pa to mBar", 13.37, unit.ConvertTo(Units.Pressure.MILLIBAR).magnitude);

      unit = Units.Pressure.MILLIBAR.OfScalar(0.125);
      AssertEquals("Failed to convert mBar to Pa", 12.5, unit.ConvertTo(Units.Pressure.PASCAL).magnitude);
    }


    [Test]
    public void TestPaToAtm() {
      var unit = Units.Pressure.PASCAL.OfScalar(1337);
      AssertEquals("Failed to convert Pa to atm", 0.01319516, unit.ConvertTo(Units.Pressure.ATMOSPHERE).magnitude);

      unit = Units.Pressure.ATMOSPHERE.OfScalar(.125);
      AssertEquals("Failed to convert atm to Pa", 12665.625, unit.ConvertTo(Units.Pressure.PASCAL).magnitude);
    }


    [Test]
    public void TestPaToInHg() {
      var unit = Units.Pressure.PASCAL.OfScalar(1337);
      AssertEquals("Failed to convert Pa to inhg", 0.3948158736648315, unit.ConvertTo(Units.Pressure.IN_HG).magnitude);

      unit = Units.Pressure.IN_HG.OfScalar(0.125);
      AssertEquals("Failed to convert inhg to Pa", 423.29858333375, unit.ConvertTo(Units.Pressure.PASCAL).magnitude);
    }

    [Test]
    public void TestPaToCmHg() {
      var unit = Units.Pressure.PASCAL.OfScalar(1337);
      AssertEquals("Failed to convert Pa to cmhg", 1.002832307461635, unit.ConvertTo(Units.Pressure.CM_HG).magnitude);

      unit = Units.Pressure.CM_HG.OfScalar(0.125);
      AssertEquals("Failed to convert cmhg to Pa", 166.6529875, unit.ConvertTo(Units.Pressure.PASCAL).magnitude);
    }

    [Test]
    public void TestPaToKgCm() {
      var unit = Units.Pressure.PASCAL.OfScalar(1337);
      AssertEquals("Failed to convert Pa to kgcm", 0.01363361, unit.ConvertTo(Units.Pressure.KG_CM).magnitude);

      unit = Units.Pressure.KG_CM.OfScalar(1.25);
      AssertEquals("Failed to convert kgcm to Pa", 122583.1, unit.ConvertTo(Units.Pressure.PASCAL).magnitude);
    }

    [Test]
    public void TestPaToPSI() {
      var unit = Units.Pressure.PASCAL.OfScalar(1337);
      AssertEquals("Failed to convert Pa to PSI", 0.1939155, unit.ConvertTo(Units.Pressure.PSIA).magnitude);

      unit = Units.Pressure.PSIA.OfScalar(0.125);
      AssertEquals("Failed to convert PSI to Pa", 861.8447, unit.ConvertTo(Units.Pressure.PASCAL).magnitude);
    }

    [Test]
    public void TestPascalDerivative() {
      AssertEquals(1, Units.Pressure.PASCAL.ToStandardUnit().Derivative().Convert(1));
    }

    [Test]
    public void TestKiloPascalDerivative() {
      AssertEquals(1000, Units.Pressure.KILOPASCAL.ToStandardUnit().Derivative().Convert(1));
    }

    [Test]
    public void TestMegaPascalDerivative() {
      AssertEquals(1000000, Units.Pressure.MEGAPASCAL.ToStandardUnit().Derivative().Convert(1));
    }

    [Test]
    public void TestBarDerivative() {
      AssertEquals(100000, Units.Pressure.BAR.ToStandardUnit().Derivative().Convert(1));
    }

    [Test]
    public void TestMilliBarDerivative() {
      AssertEquals(100, Units.Pressure.MILLIBAR.ToStandardUnit().Derivative().Convert(1));
    }

    [Test]
    public void TestAtmosphereDerivative() {
      AssertEquals(101325, Units.Pressure.ATMOSPHERE.ToStandardUnit().Derivative().Convert(1));
    }

    [Test]
    public void TestInHgDerivative() {
      AssertEquals(3386.388333, Units.Pressure.IN_HG.ToStandardUnit().Derivative().Convert(1));
    }

  }
}
