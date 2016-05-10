namespace ION.Core.Devices {
  
  using System;

  /// <summary>
  /// Enumerates the ION device models that Appion has manufactured.
  /// </summary>
  public enum EDeviceModel {
    InternalBluefruit,
    P300,
    PT300,
    P500,
    PT500,
    P800,
    PT800,
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
        case EDeviceModel.P300: return "P3";
        case EDeviceModel.P500: return "P5";
        case EDeviceModel.P800: return "P8";
        case EDeviceModel.PT300: return "PT3";
        case EDeviceModel.PT500: return "PT5";
        case EDeviceModel.PT800: return "PT8";
        case EDeviceModel.AV760: return "V7";
        case EDeviceModel._3XTM: return "T3";
        case EDeviceModel.HT: return "HT";
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
    /// Queries the device code from the serial number. The device code is the raw string code for the device model.
    ///  Every serial number has at least a 2 character device code that identifies the device.
    /// </summary>
    /// <value>The device code.</value>
    string deviceCode { get; }
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

}

