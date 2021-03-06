﻿namespace ION.Droid.Devices {

  using System;

  using Android.OS;

  using Java.Interop;

  using ION.Core.App;
  using ION.Core.Devices;

  using ION.Droid.Util;

  /// <summary>
  /// An android specific construct that will allow a device to traverse the Activity boundary.
  /// </summary>
  public class DeviceParcelable : GenericParcelable {
    /// <summary>
    /// The creator that will create the parcelable from a parcel source.
    /// </summary>
    [ExportField("CREATOR")]
    public static IParcelableCreator GetCreator() {
      return new GenericParcelableCreator<DeviceParcelable>();
    }

    public ISerialNumber serialNumber { get; set; }

    public DeviceParcelable() {
    }

    public DeviceParcelable(ISerialNumber serialNumber) {
      this.serialNumber = serialNumber;
    }

    public DeviceParcelable(IDevice device) {
      serialNumber = device.serialNumber;
    }

    protected DeviceParcelable(Parcel source) {
      serialNumber = GaugeSerialNumber.Parse(source.ReadString());
    }

    public IDevice Get(IION ion) {
      return ion.deviceManager[serialNumber];
    }

    public IDevice Get(IDeviceManager dm) {
      return dm[serialNumber];
    }

    // Overridden from GenericParcelable
    public override void WriteToParcel(Parcel dest, ParcelableWriteFlags flags) {
      dest.WriteString(serialNumber.ToString());
    }
  }
}

