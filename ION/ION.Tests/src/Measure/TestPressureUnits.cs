using System;

using NUnit.Framework;

using ION.Core.Measure;

namespace ION.TestFixtures.Measure {

  [TestFixture]
  public class PressureUnitTests {
    private static readonly Unit PA = Units.Pressure.PASCAL;
    private static readonly Unit KPA = Units.Pressure.KILOPASCAL;
    private static readonly Unit MPA = Units.Pressure.MEGAPASCAL;
    private static readonly Unit BAR = Units.Pressure.BAR;
    private static readonly Unit MBAR = Units.Pressure.MILLIBAR;
    private static readonly Unit ATMO = Units.Pressure.ATMOSPHERE;
    private static readonly Unit INHG = Units.Pressure.IN_HG;
    private static readonly Unit CMHG = Units.Pressure.CM_HG;
    private static readonly Unit KGCM = Units.Pressure.KG_CM;
    private static readonly Unit PSIG = Units.Pressure.PSIG;
    private static readonly Unit PSIA = Units.Pressure.PSIA;


    private static readonly double EPSILON = 0.01;
    private void AssertEquals(double expected, double received) {
      Assert.AreEqual(expected, received, EPSILON);
    }

    [Test]
    public void TestKPAToPascal() {
      AssertEquals(10000, KPA.OfScalar(10).ConvertTo(PA).amount);
    }

    [Test]
    public void TestPascalToKPA() {
      AssertEquals(10, PA.OfScalar(10000).ConvertTo(KPA).amount);
    }

    [Test]
    public void TestMPAToPascal() {
      AssertEquals(10000000, MPA.OfScalar(10).ConvertTo(PA).amount);
    }

    [Test]
    public void TestPascalToMPA() {
      AssertEquals(10, PA.OfScalar(10000000).ConvertTo(MPA).amount);
    }

    [Test]
    public void TestBarToPascal() {
      AssertEquals(1000000, BAR.OfScalar(10).ConvertTo(PA).amount);
    }

    [Test]
    public void TestPascalToBar() {
      AssertEquals(10, PA.OfScalar(1000000).ConvertTo(BAR).amount);
    }

    [Test]   
    public void TestMilliBarToPascal() {
      AssertEquals(1000, MBAR.OfScalar(10).ConvertTo(PA).amount);
    }

    [Test]
    public void TestPascalToMilliBar() {
      AssertEquals(10, PA.OfScalar(1000).ConvertTo(MBAR).amount);
    }

    [Test]
    public void TestAtmoToPascal() {
      AssertEquals(1013250, ATMO.OfScalar(10).ConvertTo(PA).amount);
    }

    [Test]
    public void TestPascalToAtmo() {
      AssertEquals(10, PA.OfScalar(1013250).ConvertTo(ATMO).amount);
    }

    [Test]
    public void TestINHGToPascal() {
      AssertEquals(33863.88666666, INHG.OfScalar(10).ConvertTo(PA).amount);
    }

    [Test]
    public void TestPascalToINHG() {
      AssertEquals(10, PA.OfScalar(33863.8866666).ConvertTo(INHG).amount);
    }

    [Test]
    public void TestCMHGToPascal() {
      AssertEquals(13332.239, CMHG.OfScalar(10).ConvertTo(PA).amount);
    }

    [Test]
    public void TestPascalToCMHG() {
      AssertEquals(10, PA.OfScalar(13332.239).ConvertTo(CMHG).amount);
    }

    [Test]
    public void TestKGCMToPascal() {
      AssertEquals(980665, KGCM.OfScalar(10).ConvertTo(PA).amount);
    }

    [Test]
    public void TestPascalToKGCM() {
      AssertEquals(10, PA.OfScalar(980665).ConvertTo(KGCM).amount);
    }

    [Test]
    public void TestPSIAToPascal() {
      AssertEquals(68947.5728, PSIA.OfScalar(10).ConvertTo(PA).amount);
    }

    [Test]
    public void TestPascalToPSIA() {
      AssertEquals(10, PA.OfScalar(68947.5728).ConvertTo(PSIA).amount);
    }

    [Test]
    public void TestPSIGToPascal() {
      AssertEquals(170272.573022, PSIG.OfScalar(10).ConvertTo(PA).amount);
    }

    [Test]
    public void TestPascalToPSIG() {
      AssertEquals(10, PA.OfScalar(170272.573022).ConvertTo(PSIG).amount);
    }

    [Test]
    public void TestPSIGToKPA() {
      AssertEquals(27.579, Units.Pressure.PSIG.OfScalar(4).ConvertTo(Units.Pressure.KILOPASCAL).amount);
    }

    [Test]
    public void TestPSIAToKPA() {
      AssertEquals(27.579, Units.Pressure.PSIA.OfScalar(4).ConvertTo(Units.Pressure.KILOPASCAL).amount);
    }

    [Test]
    public void TestKPAToPSIg() {
      AssertEquals(24, Units.Pressure.KILOPASCAL.OfScalar(265).ConvertTo(Units.Pressure.PSIG).amount);
    }

    [Test]
    public void TestKPAToPSIg2() {
      AssertEquals(24, Units.Pressure.KILOPASCAL.OfScalar(164).ConvertTo(Units.Pressure.PSIG).amount);
    }
  }
}

