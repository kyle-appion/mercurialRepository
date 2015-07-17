using System;

using CoreGraphics;
using Foundation;
using ObjCRuntime;
using UIKit;

using ION.Core.Devices;

namespace ION.IOS.ViewController.DeviceManager {
  [Register("DeviceManagerDeviceCell")]
  public partial class DeviceManagerDeviceCell : UITableViewCell {

    /// <summary>
    /// The backing device for the cell.
    /// </summary>
    /// <value>The device.</value>
    private IDevice device { get; set; }

    /// <summary>
    /// Creates a new DeviceManagerDeviceCell nib.
    /// </summary>
    public static UINib CreateNib() {
      return UINib.FromName(typeof(DeviceManagerDeviceCell).Name, null);
    }

    public DeviceManagerDeviceCell(IntPtr handle) : base(handle) {
      // Nope
    }

    /// <summary>
    /// Applies the device to the cell as a content backing. Changes will
    /// take effect immediately.
    /// </summary>
    /// <param name="device">Device.</param>
    public void Apply(IDevice device) {
      this.device = device;
      iconDevice.Image = DeviceUtil.GetUIImageFromDeviceModel(device.serialNumber.deviceModel);
      labelDeviceType.Text = device.serialNumber.deviceModel.GetTypeString();
      labelDeviceName.Text = device.name;
      labelSerialNumber.Text = device.serialNumber.ToString();
    }
  }
}

