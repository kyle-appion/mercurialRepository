namespace ION.Core.Devices {
  
  using System;

  public static class SerialNumberExtensions {
    /// <summary>
    /// Queries whether or not the given string is a valid serial number.
    /// </summary>
    /// <returns><c>true</c> if is valid serial number the specified serialNumber; otherwise, <c>false</c>.</returns>
    /// <param name="serialNumber">Serial number.</param>
    public static bool IsValidSerialNumber(this string serialNumber) {
      if (GaugeSerialNumber.IsValid(serialNumber)) {
        return true;
      } else {
        throw new Exception("Cannot parse serial number: invalid serial number: " + serialNumber);
      }
    }

    /// <summary>
    /// Attempts to parse the sting into a valid serial number.
    /// </summary>
    /// <param name="serialNumber">Serial number.</param>
    public static ISerialNumber ParseSerialNumber(this string serialNumber) {
      if (GaugeSerialNumber.IsValid(serialNumber)) {
        return GaugeSerialNumber.Parse(serialNumber);
      } else {
        throw new Exception("Cannot parse serial number: invalid serial number: " + serialNumber);
      }
    }
  }
}

