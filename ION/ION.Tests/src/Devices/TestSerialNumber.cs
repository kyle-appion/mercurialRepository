using System;

using NUnit.Framework;

using ION.Core.Devices;

namespace ION.TestFixtures.Devices {
  [TestFixture]
  public class TestSerialNumber {
    private static readonly GaugeSerialNumber VALID_GAUGE_SERIAL_NUMBER1= new GaugeSerialNumber(EDeviceModel.P300, "P315F0025", new DateTime(2015, 6, 1), 25);
    private static readonly GaugeSerialNumber VALID_GAUGE_SERIAL_NUMBER2 = new GaugeSerialNumber(EDeviceModel.P500, "P515F0025", new DateTime(2015, 6, 1), 25);
    private static readonly GaugeSerialNumber VALID_GAUGE_SERIAL_NUMBER3 = new GaugeSerialNumber(EDeviceModel.P800, "P815F0025", new DateTime(2015, 6, 1), 25);

    [Test]
    public void TestGaugeSerialNumber_AreEqual() {
      Assert.AreEqual(VALID_GAUGE_SERIAL_NUMBER1, VALID_GAUGE_SERIAL_NUMBER1);
    }

    [Test]
    public void TestGaugeSerialNumber_AreNotEqual() {
      Assert.AreNotEqual(VALID_GAUGE_SERIAL_NUMBER1, VALID_GAUGE_SERIAL_NUMBER3);
    }


    [Test]
    public void TestGaugeSerialNumber_Parse_Valid() {
      Assert.AreEqual(VALID_GAUGE_SERIAL_NUMBER1, GaugeSerialNumber.Parse("P315F0025"));
    }

    [Test]
    public void TestGaugeSerialNumber_Parse_FailNull() {
      var ex = Assert.Throws<ArgumentException>( () => { GaugeSerialNumber.Parse(null); });
      Assert.That(ex.Message, Is.EqualTo("Cannot parse serial: serial is null"));
    }

    /*
    Length 10 serial number now exists :(
    
    [Test]
    public void TestGaugeSerialNumber_Parse_FailTooLong() {
      var ex = Assert.Throws<ArgumentException>( () => { GaugeSerialNumber.Parse("P315F00025"); });
      Assert.That(ex.Message, Is.EqualTo("Cannot parse serial: expected serial of length 9, received length 10"));
    }
    */


    [Test]
    public void TestGaugeSerialNumber_BuildManufactureDate_Valid() {
      DateTime expected = new DateTime(2015, 6, 1);
      Assert.AreEqual(expected, GaugeSerialNumber.BuildManufactureDate("15", 'F'));
    }

    [Test]
    public void TestGaugeSerialNumber_BuildManufactureDate_ValidLowerMonthCode() {
      DateTime expected = new DateTime(2015, 6, 1);
      Assert.AreEqual(expected, GaugeSerialNumber.BuildManufactureDate("15", 'f'));
    }

    [Test]
    public void TestGaugeSerialNumber_BuildManufactureDate_FailsYearNull() {
      var ex = Assert.Throws<ArgumentException>( () => { GaugeSerialNumber.BuildManufactureDate(null, 'F'); });
      Assert.That(ex.Message, Is.EqualTo("Cannot build manufacture date: null year code"));
    }

    [Test]
    public void TestGaugeSerialNumber_BuildManufactureDate_FailsYearOutOfRange() {
      var ex = Assert.Throws<ArgumentException>( () => { GaugeSerialNumber.BuildManufactureDate("2015", 'F'); });
      Assert.That(ex.Message, Is.EqualTo("Cannot build manufacture date: year 2015 is out of range, expected [14, 99]"));
    }

    [Test]
    public void TestGaugeSerialNumber_BuildManufactureDate_FailsMonthOutOfRange() {
      var ex = Assert.Throws<ArgumentException>( () => { GaugeSerialNumber.BuildManufactureDate("15", 'M'); });
      Assert.That(ex.Message, Is.EqualTo("Cannot build manufacture date: month code M is out of range, expected [a|A, l|L]"));
    }

    [Test]
    public void TestGaugeSerialNumber_BuildManufactureDate_FailsInvalidYearCode() {
      var ex = Assert.Throws<FormatException>( () => { GaugeSerialNumber.BuildManufactureDate("Beer", 'f'); });
      Assert.IsNotNull(ex);
    }
  }
}

