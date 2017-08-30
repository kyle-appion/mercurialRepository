namespace ION.Core.Devices {

  using System;

  public static class SerialNumberExtensions {
    /// <summary>
    /// Queries whether or not the given string is a valid serial number.
    /// </summary>
    /// <returns><c>true</c> if is valid serial number the specified serialNumber; otherwise, <c>false</c>.</returns>
    /// <param name="serialNumber">Serial number.</param>
    public static bool IsValidSerialNumber(this string serialNumber) {
			return GaugeSerialNumber.IsValid(serialNumber);
    }

    /// <summary>
    /// Attempts to parse the sting into a valid serial number.
    /// </summary>
    /// <param name="serialNumber">Serial number.</param>
    public static ISerialNumber ParseSerialNumber(this string serialNumber) {
      if (GaugeSerialNumber.IsValid(serialNumber)) {
        return GaugeSerialNumber.Parse(serialNumber);
      } else {
      	// do not throw exceptions
        //throw new Exception("Cannot parse serial number: invalid serial number: " + serialNumber);
        return null;
      }
    }

#if DEBUG
    private static int SERIAL_NUMBER_COUNTER = 0;
    /// <summary>
    /// Generates a serial number for the given model.
    /// </summary>
    public static ISerialNumber GenerateSerialNumber(this EDeviceModel model) {
      var date = DateTime.Now;
      var code = "";

      switch (model) {
        case EDeviceModel.P300: code = "P3"; break;
        case EDeviceModel.P500: code = "P5"; break;
        case EDeviceModel.P800: code = "P8"; break;
        case EDeviceModel.PT500: code = "S5"; break;
        case EDeviceModel.PT800: code = "S8"; break;
        case EDeviceModel.AV760: code = "V7"; break;
        default:
          throw new Exception(model + " is not implemented for random serial number. Please implement it...");
      }

      var str = code + ('A' + date.Month) + (date.Year - 2000).ToString("00") + (++SERIAL_NUMBER_COUNTER).ToString("000");
      return ParseSerialNumber(str);
    }
#endif
  }
}
