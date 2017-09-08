namespace ION.TestFixtures.Measure {

  using System;

  using NUnit.Framework;

  using ION.Core.Measure;

  [TestFixture]
  public class TestVacuumUnits {

    private static readonly Unit PA = Units.Vacuum.PASCAL;
    private static readonly Unit KPA = Units.Vacuum.KILOPASCAL;
    private static readonly Unit BAR = Units.Vacuum.BAR;
    private static readonly Unit MBAR = Units.Vacuum.MILLIBAR;
    private static readonly Unit ATMO = Units.Vacuum.ATMOSPHERE;
    private static readonly Unit INHG = Units.Vacuum.IN_HG;
    private static readonly Unit CMHG = Units.Vacuum.CM_HG;
    private static readonly Unit KGCM = Units.Vacuum.KG_CM;
    private static readonly Unit PSIA = Units.Vacuum.PSIA;
    private static readonly Unit TORR = Units.Vacuum.TORR;
    private static readonly Unit MILLITORR = Units.Vacuum.MILLITORR;
    private static readonly Unit MICRON = Units.Vacuum.MICRON;

    private static readonly double EPSILON = 0.01;
    private void AssertEquals(double expected, double received) {
      Assert.AreEqual(expected, received, EPSILON);
    }

    [Test]
    public void TestTorrToPascal() {
      AssertEquals(1333.22368, TORR.OfScalar(10).ConvertTo(PA).amount);
    }

    [Test]
    public void TestPascalToTorr() {
      AssertEquals(10, PA.OfScalar(1333.22368).ConvertTo(TORR).amount);
    }

    [Test]
    public void TestMTorrToPascal() {
      AssertEquals(1.3332237, MILLITORR.OfScalar(10).ConvertTo(PA).amount);
    }

    [Test]
    public void TestPascalToMTorr() {
      AssertEquals(10, PA.OfScalar(1.3332237).ConvertTo(MILLITORR).amount);
    }

    [Test]
    public void TestMicronToPascal() {
      AssertEquals(1.3332237, MICRON.OfScalar(10).ConvertTo(PA).amount);
    }

    [Test]
    public void TestPascalToMicron() {
      AssertEquals(10, PA.OfScalar(1.3332237).ConvertTo(MICRON).amount);
    }
  }
}

