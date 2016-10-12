namespace ION.Core.Devices {
  
  using System;

  using ION.Core.Util;

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
    /// <summary>
    /// Queries the model of the device that this serial number represents.
    /// </summary>
    /// <value>The device model.</value>
    public EDeviceModel deviceModel { get; private set; }
    /// <summary>
    /// Queries the type of device that this serial number is representative of.
    /// </summary>
    /// <value>The type of the device.</value>
    public EDeviceType deviceType { get { return EDeviceType.Gauge; } }
    /// <summary>
    /// Queries the device code from the serial number. The device code is the raw string code for the device model.
    ///  Every serial number has at least a 2 character device code that identifies the device.
    /// </summary>
    /// <value>The device code.</value>
    public string deviceCode { get; private set; }
    /// <summary>
    /// Queries the serial number as a raw string. This is useful when
    /// printing the serial number.
    /// </summary>
    /// <value>The raw serial.</value>
    public string rawSerial { get; private set; }
    /// <summary>
    /// Queries the date that the product (and implicitly the serial number)
    /// was created.
    /// </summary>
    /// <value>The manufacture date.</value>
    public DateTime manufactureDate { get; private set; }
    /// <summary>
    /// Queries the batch id of the serial number (and implicitly the product).
    /// Used to identify where in the batch the product was as well as
    /// solidifying an enumeration in which products may be identified.
    /// </summary>
    /// <value>The batch identifier.</value>
    public ushort batchId { get; private set; }

    public GaugeSerialNumber(EDeviceModel model, string deviceCode, string rawSerial, DateTime manufactureDate, ushort batchId) {
      this.deviceModel = model;
      this.deviceCode = deviceCode;
      this.rawSerial = rawSerial;
      this.manufactureDate = manufactureDate;
      this.batchId = batchId;
    }

    /// <Docs>To be added.</Docs>
    /// <para>Returns the sort order of the current instance compared to the specified object.</para>
    /// <summary>
    /// Compares to.
    /// </summary>
    /// <returns>The to.</returns>
    /// <param name="other">Other.</param>
    public int CompareTo(ISerialNumber other) {
      if (other is GaugeSerialNumber) {
        if (deviceModel == other.deviceModel) {
          return batchId.CompareTo(other.batchId);
        } else {
          return deviceModel.CompareTo(other.deviceModel);
        }
      } else {
        return deviceType.CompareTo(other.deviceType);
      }
    }

    // Overridden from object
    public override int GetHashCode() {
      return rawSerial.GetHashCode();
    }

    // Overridden from object
    public override bool Equals(Object other) {
      if (this == other) {
        return true;
      } else if (other is GaugeSerialNumber) {
        return ((GaugeSerialNumber)other).rawSerial.Equals(rawSerial);
      } else {
        return false;
      }
    }

    // Overridden from object
    public override string ToString() {
      return rawSerial;
    }

    /// <summary>
    /// Determines whether or not the given serial number is a valid GaugeSerialNumber.
    /// </summary>
    /// <param name="serial"></param>
    /// <returns></returns>
    public static bool IsValid(string serial) {
      try {
        Parse(serial);
        return true;
      } catch (ArgumentException e) {
//        ION.Core.Util.Log.D("SerialNumber", serial + " is not a valid GaugeDeviceSerial", e);
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

      // TODO ahodder@appioninc.com: remove this
/*
      if ("P516E003".Equals(serial) || "RigCom".Equals(serial)) {
        return new GaugeSerialNumber(EDeviceModel.PT500, "PT8", serial, BuildManufactureDate("16", 'E'), (ushort)3);
      }
*/
			// TODO ahodder@appioninc.com: The new short serial number format of TTYYM### needs to be implemented
      if (serial.StartsWith("S8")) {
        return new GaugeSerialNumber(EDeviceModel.PT800, "PT8", serial, BuildManufactureDate(serial.Substring(2, 2), serial.ToCharArray()[4]), ushort.Parse(serial.Substring(5)));
      } else if (serial.StartsWith("S5")) {
        return new GaugeSerialNumber(EDeviceModel.PT500, "PT5", serial, BuildManufactureDate(serial.Substring(2, 2), serial.ToCharArray()[4]), ushort.Parse(serial.Substring(5)));
			} else if (serial.StartsWith("T1")) {
				return new GaugeSerialNumber(EDeviceModel._1XTM, "T1", serial, BuildManufactureDate(serial.Substring(2, 2), serial.ToCharArray()[4]), ushort.Parse(serial.Substring(5)));
			}

      // This check is not ideal, but at the time of writing the serial numbers were not solidified. I hate
      // this project.
      if (serial.Length == 9) {
        string rawDeviceModel = serial.Substring(0, 2);
        string rawYearCode = serial.Substring(2, 2);
        char rawMonthCode = serial[4];
        string rawBatchId = serial.Substring(5, 4);
        EDeviceModel deviceModel = DeviceModelUtils.GetDeviceModelFromCode(rawDeviceModel);

        return new GaugeSerialNumber(deviceModel, rawDeviceModel, serial, BuildManufactureDate(rawYearCode, rawMonthCode), Convert.ToUInt16(rawBatchId));
      } else if (serial.Length == 10) {
        string rawDeviceModel = serial.Substring(0, 3);
        string rawYearCode = serial.Substring(3, 2);
        char rawMonthCode = serial[5];
        string rawBatchId = serial.Substring(6, 4);
        EDeviceModel deviceModel = DeviceModelUtils.GetDeviceModelFromCode(rawDeviceModel);

        return new GaugeSerialNumber(deviceModel, rawDeviceModel, serial, BuildManufactureDate(rawYearCode, rawMonthCode), Convert.ToUInt16(rawBatchId));
      } else {
        throw new ArgumentException("Cannot parse serial: expected serial of length 9 or 10, received length " + serial.Length);
      }
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

      if (year < 2013 || year > 2099) {
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

