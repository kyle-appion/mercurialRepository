using System;

using NUnit.Framework;

using ION.Core.Devices;

namespace ION.TestFixtures.Device {
  [TestFixture]
  public class TestGaugeType {
    [Test]
    public void TestDeviceModelUtils_FromString_Valid() {
      Assert.AreEqual(EDeviceModel.P300, DeviceModelUtils.GetDeviceModelFromCode("P3"));
    }
  }
}

