namespace ION.Tests {

  using System;

  using NUnit.Framework;

  using ION.Core.App;
  using ION.Core.Fluids;
  using ION.Core.Measure;

  using ION.TestFixtures.App;

  /// <summary>
  /// Tests common refrigerants and there conversions.
  /// </summary>
  [TestFixture]
  public class TestPTChart {

    private void TestAllPressure(PTChart pt, Unit inUnit, double[] tempAndExpected) {
      for (int i = 0; i < tempAndExpected.Length; i += 2) {
        var temp = pt.GetTemperature(inUnit.OfScalar(tempAndExpected[i]));
        Assert.AreEqual(tempAndExpected[i + 1], temp.amount, 0.5);
      }
    }

    private void TestAllTemperature(PTChart pt, Unit inUnit, double[] pressAndExpected) {
      for (int i = 0; i < pressAndExpected.Length; i += 2) {
        var temp = pt.GetTemperature(inUnit.OfScalar(pressAndExpected[i]));
        Assert.AreEqual(pressAndExpected[i + 1], temp.amount, 0.25);
      }
    }

    private IION ion;
    private IFluidManager fm;

    [TestFixtureSetUp]
    public void Setup() {
      ion = new MockION();
      fm = ion.fluidManager;
    }

    [TestFixtureTearDown]
    public void TearDown() {
      ion.Dispose();
    }

    [Test]
    public async void TestR22() {
      var f = await fm.GetFluidAsync("R22");
      var pt = PTChart.New(ion, Fluid.EState.Bubble, f);

      TestAllPressure(pt, Units.Pressure.PSIG, new double[] {
        // Pressure, Temperature
        0, -41,
        5, -30,
        10, -20,
        25, 1,
        50, 53,
        100, 59,
        150, 83,
        200, 101,
        250, 117,
        305, 132,
        400, 154,
      });

      TestAllTemperature(pt, Units.Temperature.FAHRENHEIT, new double[] {
        -41, 0,
        -30, 5,
        -20, 10,
        1, 25,
        53, 50,
        59, 100,
        83, 150,
        101, 200,
        117, 250,
        132, 305,
        154, 400,
      });
    }
  }
}

