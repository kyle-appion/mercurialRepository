namespace ION.TestFixtures.Measure {

  using System;

  using Newtonsoft.Json;

  using NUnit.Framework;

  using ION.Core.Measure;
  using ION.Core.Util;

  [TestFixture]
  public class TestUnitSerialization {

    [Test]
    public void TestAllUnits() {
      var unit = Units.Pressure.MICRON;
      var json = JsonConvert.SerializeObject(unit, Formatting.Indented, new JsonSerializerSettings() {
        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
        TypeNameHandling = TypeNameHandling.All,
      });
      Console.WriteLine(json);
      var dunit = JsonConvert.DeserializeObject(json, unit.GetType(), new JsonSerializerSettings() {
        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
        TypeNameHandling = TypeNameHandling.All,
      });
      Console.WriteLine(unit + " equals " + dunit + " == " + (unit == dunit));
      Assert.AreEqual(unit.ToString(), dunit?.ToString() + "");//unit.Equals(dunit));
      /*
      foreach (var unit in Units.GetAllUnits()) {
        var json = JsonConvert.SerializeObject(unit);
        var dunit = JsonConvert.DeserializeObject(json, unit.GetType());
        Assert.Equals(unit, dunit);

      }
      */
    }
  }
}

