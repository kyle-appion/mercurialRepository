using System;

using CoreGraphics;
using Foundation;
using ObjCRuntime;
using UIKit;

using ION.Core.Devices;
using ION.Core.Util;

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

      // TODO ahodder@appioninc.com: Huh?
      // We are doing this to make sure we don't spam the button event pool.
      // I should probably fix this, but I am le tired.
      buttonConnect.TouchUpInside -= HandleConnectButtonClick;
      buttonConnect.TouchUpInside += HandleConnectButtonClick;

      if (device is GaugeDevice) {
        ApplyGaugeDeviceContent((GaugeDevice)device);
      }

    }

    /// <summary>
    /// Called when the device cell is applied with a gauge device.
    /// </summary>
    /// <param name="gauge">Gauge.</param>
    private void ApplyGaugeDeviceContent(GaugeDevice gauge) {
      UIView[] subviews = containerSensor.Subviews;

      // Ensure we have the proper number of views in the container
      if (subviews.Length >= gauge.sensorCount) {
        for (int i = subviews.Length - 1; i >= gauge.sensorCount; i--) {
          subviews[i].RemoveFromSuperview();
        }
      }

      // Ensure we have all the sensor views we need
      while (containerSensor.Subviews.Length < gauge.sensorCount) {
        UIView view = DeviceManagerSensorView.CreateFromNib(containerSensor);        
        containerSensor.AddSubview(view);
      }

      for (int i = 0; i < containerSensor.Subviews.Length; i++) {
        ((DeviceManagerSensorView)containerSensor.Subviews[i]).Apply(gauge[i]);
      }
    }

    /// <summary>
    /// Called when the buttonConnect is clicked.
    /// </summary>
    /// <param name="obj">Object.</param>
    /// <param name="args">Arguments.</param>
    private void HandleConnectButtonClick(object obj, EventArgs args) {
      Log.D(this, "Handling button click");
      if (!device.isConnected) {
        Log.D(this, "Attempt connect");
        device.connection.Connect();
      } else {
        Log.D(this, "Disconnect");
        device.connection.Disconnect();
      }
    }
  }
}

