using System;

using NUnit.Framework;

using ION.Core.Devices;

namespace ION.TestFixtures.Device {
  [TestFixture]
  public class TestGaugeType {
    [Test]
    public void TestGaugeTypeUtils_FromString_Valid() {
      Assert.AreEqual(EGaugeType.P300, GaugeTypeUtils.FromString("P3"));
    }

    [Test]
    public void TestGaugeTypeUtils_FromString_FailsToShort() {
      var ex = Assert.Throws<ArgumentException>( () => { GaugeTypeUtils.FromString("P"); });
      Assert.That(ex.Message, Is.EqualTo("Cannot parse GaugeType: expected gauge type of length 2, received length " + 1));
    }

    [Test]
    public void TestGaugeTypeUtils_FromString_FailsToLong() {
      var ex = Assert.Throws<ArgumentException>( () => { GaugeTypeUtils.FromString("P300"); });
      Assert.That(ex.Message, Is.EqualTo("Cannot parse GaugeType: expected gauge type of length 2, received length " + 4));
    }

    [Test]
    public void TestGaugeTypeUtils_FromString_FailsNull() {
      var ex = Assert.Throws<ArgumentException>( () => { GaugeTypeUtils.FromString(null); });
      Assert.That(ex.Message, Is.EqualTo("Cannot parse GaugeType: string is null"));
    }
  }
}

