namespace ION.Core.Devices {
  
  using System;

  /// <summary>
  /// Enumerates the ION device models that Appion has manufactured.
  /// </summary>
  public enum EDeviceModel {
    P300,
    P500,
    PT800,
    P800,
    AV760,
    _3XTM, // I kind of hate how greg names products some times
    HT,
  }

  /// <summary>
  /// Common extensions for EDeviceModel.
  /// </summary>
  public static class DeviceModelExtensions {
    /// <summary>
    /// Queries the model code of the EDeviceModel. If the model code cannot
    /// be extracted, we will throw an ArgumentException.
    /// </summary>
    /// <returns>The model code.</returns>
    /// <param name="deviceModel">Device model.</param>
    public static string GetModelCode(this EDeviceModel deviceModel) {
      switch (deviceModel) {
      case EDeviceModel.P300: { return "P3"; }
      case EDeviceModel.P500: { return "P5"; }
      case EDeviceModel.P800: { return "P8"; }
      case EDeviceModel.PT800: { return "PT8"; }
      case EDeviceModel.AV760: { return "V7"; }
      case EDeviceModel._3XTM: { return "T3"; }
      case EDeviceModel.HT: { return "HT"; }
      default: {
          throw new ArgumentException("Cannot get model code: unrecoginized device model " + deviceModel);
        }
      }
    }
  }

  /// <summary>
  /// A class that provides util functions for EDeviceModel.
  /// </summary>
  public partial class DeviceModelUtils {
    /// <summary>
    /// Converts the given model code to a DeviceModel. If the model code is not a valid
    /// code, we will throw an ArgumentException.
    /// </summary>
    /// <returns>The device model from string.</returns>
    /// <param name="modelCode">Model code.</param>
    public static EDeviceModel GetDeviceModelFromCode(string modelCode) {
      modelCode = modelCode.ToUpper();

      foreach (EDeviceModel model in Enum.GetValues(typeof(EDeviceModel))) {
        if (model.GetModelCode().Equals(modelCode)) {
          return model;
        }
      }

      throw new ArgumentException("Cannot get device model: unrecognized code " + modelCode);
    }
  }


  /// <summary>
  /// SerialNumber is the basic unit of identification for Appion products.
  /// </summary>
  public interface ISerialNumber : IComparable<ISerialNumber> {
    /// <summary>
    /// Queries the model of the device that this serial number represents.
    /// </summary>
    /// <value>The device model.</value>
    EDeviceModel deviceModel { get; }
    /// <summary>
    /// Queries the type of device that this serial number is representative of.
    /// </summary>
    EDeviceType deviceType { get; }
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


    // Overridden from ISerialNumber
    public EDeviceModel deviceModel { get; private set; }
    // Overridden from ISerialNumber
    public EDeviceType deviceType { get { return EDeviceType.Gauge; } }
    // Overridden from ISerialNumber
    public string rawSerial { get; private set; }
    // Overridden from ISerialNumber
    public DateTime manufactureDate { get; private set; }
    // Overridden from ISerialNumber
    public ushort batchId { get; private set; }

    public GaugeSerialNumber(EDeviceModel model, string rawSerial, DateTime manufactureDate, ushort batchId) {
      this.deviceModel = model;
      this.rawSerial = rawSerial;
      this.manufactureDate = manufactureDate;
      this.batchId = batchId;
    }

    public int CompareTo(ISerialNumber other) {
      if (other is GaugeSerialNumber) {
      return manufactureDate.CompareTo(other.manufactureDate);
      } else {
        return 0;
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
      } catch (ArgumentException) {
        ION.Core.Util.Log.D("SerialNumber", serial + " is not a valid serial");
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

      // This check is not ideal, but at the time of writing the serial numbers were not solidified. I hate
      // this project.
      if (serial.Length == 9) {
        string rawDeviceModel = serial.Substring(0, 2);
        string rawYearCode = serial.Substring(2, 2);
        char rawMonthCode = serial[4];
        string rawBatchId = serial.Substring(5, 4);
        EDeviceModel deviceModel = DeviceModelUtils.GetDeviceModelFromCode(rawDeviceModel);

        return new GaugeSerialNumber(deviceModel, serial, BuildManufactureDate(rawYearCode, rawMonthCode), Convert.ToUInt16(rawBatchId));
      } else if (serial.Length == 10) {
        string rawDeviceModel = serial.Substring(0, 3);
        string rawYearCode = serial.Substring(3, 2);
        char rawMonthCode = serial[5];
        string rawBatchId = serial.Substring(6, 4);
        EDeviceModel deviceModel = DeviceModelUtils.GetDeviceModelFromCode(rawDeviceModel);

        return new GaugeSerialNumber(deviceModel, serial, BuildManufactureDate(rawYearCode, rawMonthCode), Convert.ToUInt16(rawBatchId));
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

