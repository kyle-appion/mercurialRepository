﻿using System;

namespace ION.Core.Devices {
  /// <summary>
  /// SerialNumber is the basic unit of identification for Appion products.
  /// </summary>
  public interface ISerialNumber : IComparable<ISerialNumber> {
    /// <summary>
    /// Queries the serial number as a raw string. This is useful when
    /// printing the serial number.
    /// </summary>
    string rawSerial { get; }

    /// <summary>
    /// Queries the date that the product (and implicitly the serial number)
    /// was created.
    /// </summary>
    DateTime manufactureDate { get; }

    /// <summary>
    /// Queries the batch id of the serial number (and implicitly the product).
    /// Used to identify where in the batch the product was as well as
    /// solidifying an enumeration in which products may be identified.
    /// </summary>
    ushort batchId { get; }
	}

  /// <summary>
  /// GaugeSerialNumber is a serial number that describes everything that is
  /// necessary to determine what a particular appion gauge is. As such, every
  /// appion gauge will and must have a valid GaugeSerialNumber.
  /// <para>
  /// As GaugeSerialNumbers are repsonsible for identifying and categorizing 
  /// gauges, they are rigidly structured. GaugeSerialNumbers will contain exactly
  /// 9 characters from which a gauge can be wholly identified. The following will
  /// desccribe the layout of a GaugeSerialNumber:
  /// </para>
  /// <code>
  /// CHARs   | Description
  /// --------+-------------------------------------------------------------------
  /// 0-1     | The device code. These two characters are used to define the exact
  ///         | gauge and is used to load the gauge parameters.
  /// --------+-------------------------------------------------------------------
  /// 2-3     | The year code. These two characters are used to determine the year
  ///         | that the device was manufactured. Paired with the following month
  ///         | code, we can deduce when the gauge was manufactured.
  /// --------+-------------------------------------------------------------------
  /// 4       | The month code. This character describes the month that the gauge
  ///         | was manufactured. Paired with the previous year code, we can
  ///         | deduce when the gauge was manufactured.
  /// --------+-------------------------------------------------------------------
  /// 5-8     | The batch id. These four digits are used to produce a 0-9999 batch
  ///         | id code that is used to indicate when in a batch the gauge was
  ///         | manufactured.
  /// --------+-------------------------------------------------------------------
  /// </code>
  /// </summary>
  public class GaugeSerialNumber : ISerialNumber {
    public string rawSerial { get; private set; }

    public DateTime manufactureDate { get; private set; }

    public ushort batchId { get; private set; }

    /// <summary>
    /// Queries the gauge type that the serial number is representative of.
    /// </summary>
    /// <value>The type of the gauge.</value>
    public EGaugeType gaugeType { get; private set; }


    public GaugeSerialNumber(string rawSerial, DateTime manufactureDate, ushort batchId, EGaugeType gaugeType) {
      this.rawSerial = rawSerial;
      this.manufactureDate = manufactureDate;
      this.batchId = batchId;
      this.gaugeType = gaugeType;
    }

    public int CompareTo(ISerialNumber other) {
      if (other is GaugeSerialNumber) {
      return manufactureDate.CompareTo(other.manufactureDate);
      } else {
        return 0;
      }
    }

    public override int GetHashCode() {
      return rawSerial.GetHashCode();
    }

    public override bool Equals(Object other) {
      if (this == other) {
        return true;
      } else if (other is GaugeSerialNumber) {
        return ((GaugeSerialNumber)other).rawSerial.Equals(rawSerial);
      } else {
        return false;
      }
    }

    /// <summary>
    /// Parses out a GaugeSerialNumber from the given serial. If the given serial
    /// is not a valid GaugeSerialNumber we will throw an ArguementException
    /// </summary>
    /// <param name="serial">Serial.</param>
    public static GaugeSerialNumber Parse(string serial) {
      if (serial == null) {
        throw new ArgumentException("Cannot parse serial: serial is null");
      }

      if (serial.Length != 9) {
        throw new ArgumentException("Cannot parse serial: expected serial of length 9, received length " + serial.Length);
      }

      string rawGaugeType = serial.Substring(0, 2);
      string rawYearCode = serial.Substring(2, 2);
      char rawMonthCode = serial[4];
      string rawBatchId = serial.Substring(5, 4);



      return new GaugeSerialNumber(serial, BuildManufactureDate(rawYearCode, rawMonthCode), Convert.ToUInt16(rawBatchId), GaugeTypeUtils.FromString(rawGaugeType));
    }

    /// <summary>
    /// Constructs a manufacture date from the given year and month codes. If the manufacture
    /// date cannot be constructed, then we will throw an ArgumentException.
    /// </summary>
    /// <returns>The manufacture date.</returns>
    public static DateTime BuildManufactureDate(string yearCode, char monthCode) {
      monthCode = Char.ToUpper(monthCode);

      if (yearCode == null) {
        throw new ArgumentException("Cannot build manufacture date: null year code");
      }

      int year = 2000 + Convert.ToInt32(yearCode); 

      if (year < 2014 || year > 2099) {
        throw new ArgumentException("Cannot build manufacture date: year " + yearCode + " is out of range, expected [14, 99]");
      }

      /*
      if (monthCode == null) {
        throw new ArgumentException("Cannot build manufacture date: null month code");
      }
      */

      int month = monthCode - 'A' + 1;

      if (month < 0 || month > 12) {
        throw new ArgumentException("Cannot build manufacture date: month code " + monthCode + " is out of range, expected [a|A, l|L]");
      }

      return new DateTime(year, month, 1);
    } 
  }
}

