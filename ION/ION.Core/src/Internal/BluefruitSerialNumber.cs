namespace ION.Core.Internal {

  using System;

  using ION.Core.Devices;

  public class BluefruitSerialNumber : ISerialNumber {
    /// <summary>
    /// Gets the device model.
    /// </summary>
    /// <value>The device model.</value>
    public EDeviceModel deviceModel { 
      get {
        return EDeviceModel.InternalBluefruit;
      }
    }
    /// <summary>
    /// Queries the type of device that this serial number is representative of.
    /// </summary>
    /// <value>The type of the device.</value>
    public EDeviceType deviceType {
      get {
        return EDeviceType.InternalInterface;
      }
    }
    /// <summary>
    /// Queries the device code from the serial number. The device code is the raw string code for the device model.
    ///  Every serial number has at least a 2 character device code that identifies the device.
    /// </summary>
    /// <value>The device code.</value>
    public string deviceCode {
      get {
        return "Bf";
      }
    }
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
    public DateTime manufactureDate { 
      get {
        return DateTime.Now;
      }
    }
    /// <summary>
    /// Queries the batch id of the serial number (and implicitly the product).
    /// Used to identify where in the batch the product was as well as
    /// solidifying an enumeration in which products may be identified.
    /// </summary>
    /// <value>The batch identifier.</value>
    public ushort batchId {
      get {
        return 0;
      }
    }

    public BluefruitSerialNumber(string serialNumber) {
      rawSerial = serialNumber;
    }

    /// <Docs>To be added.</Docs>
    /// <para>Returns the sort order of the current instance compared to the specified object.</para>
    /// <summary>
    /// Compares to.
    /// </summary>
    /// <returns>The to.</returns>
    /// <param name="other">Other.</param>
    public int CompareTo(ISerialNumber other) {
      return 0;
    }

    /// <summary>
    /// Determines whether the specified <see cref="System.Object"/> is equal to the current <see cref="ION.Core.Devices.BluefruitSerialNumber"/>.
    /// </summary>
    /// <param name="obj">The <see cref="System.Object"/> to compare with the current <see cref="ION.Core.Devices.BluefruitSerialNumber"/>.</param>
    /// <returns><c>true</c> if the specified <see cref="System.Object"/> is equal to the current
    /// <see cref="ION.Core.Devices.BluefruitSerialNumber"/>; otherwise, <c>false</c>.</returns>
    public override bool Equals(object obj) {
      var other = obj as BluefruitSerialNumber;
      if (other != null) {
        return true;
      } else {
        return false;
      }
    }

    /// <summary>
    /// Serves as a hash function for a <see cref="ION.Core.Internal.BluefruitSerialNumber"/> object.
    /// </summary>
    /// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a hash table.</returns>
    public override int GetHashCode() {
      return rawSerial.GetHashCode();
    }

    /// <summary>
    /// Returns a <see cref="System.String"/> that represents the current <see cref="ION.Core.Devices.BluefruitSerialNumber"/>.
    /// </summary>
    /// <returns>A <see cref="System.String"/> that represents the current <see cref="ION.Core.Devices.BluefruitSerialNumber"/>.</returns>
    public override string ToString() {
      return rawSerial;
    }

    /// <summary>
    /// Attempts to determine if the given serial number is a valid bluefruit serial number.
    /// </summary>
    /// <returns><c>true</c> if is valid the specified serialNumber; otherwise, <c>false</c>.</returns>
    /// <param name="serialNumber">Serial number.</param>
    public static bool IsValid(string serialNumber) {
      try {
        return serialNumber.Equals("Appion_Internal_Bf") || "Adafruit Bluefruit LE".Equals(serialNumber);
      } catch (Exception e) {
        return false;
      }
    }

    /// <summary>
    /// Returns a parsed bluefruit serial number.
    /// </summary>
    /// <param name="serialNumber">Serial number.</param>
    public static BluefruitSerialNumber Parse(string serialNumber) {
      return new BluefruitSerialNumber(serialNumber);
    }
  }
}

