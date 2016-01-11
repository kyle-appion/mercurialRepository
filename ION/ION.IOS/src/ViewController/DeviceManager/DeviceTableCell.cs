namespace ION.IOS.ViewController.DeviceManager {

  using System;

  using Foundation;
  using UIKit;

  using SWTableViewCell;

  using ION.Core.App;
  using ION.Core.Connections;
  using ION.Core.Devices;
  using ION.Core.Util;

  using ION.IOS.Devices;
  using ION.IOS.UI;

	public partial class DeviceTableCell : SWTableViewCell, IReleasable {

    /// <summary>
    /// The action that is called when the cell's background is clicked.
    /// </summary>
    /// <value>The background clicked.</value>
    public Action onBackgroundClicked { get; set; }

    /// <summary>
    /// The ion context. Necessary to properly connect a device.
    /// </summary>
    /// <value>The ion.</value>
    private IION ion { get; set; }
    /// <summary>
    /// The current device that the cell is displaying.
    /// </summary>
    /// <value>The device.</value>
    private IDevice device {
      get {
        return __device;
      }
      set {
        if (__device != null) {
          __device.onDeviceEvent -= OnDeviceEvent;
        }

        __device = value;

        if (__device != null) {
          __device.onDeviceEvent += OnDeviceEvent;
          UpdateLabels();
          UpdateActivityViews();
        }
      }
    } IDevice __device;
    
		public DeviceTableCell (IntPtr handle) : base (handle) {
		}

    // Overridden from UITableViewCell
    public override void AwakeFromNib() {
      base.AwakeFromNib();

      viewBackground.AddGestureRecognizer(new UITapGestureRecognizer(() => {
        Log.D(this, "Click, click");
        if (onBackgroundClicked != null) {
          onBackgroundClicked();
        }
      }));

      buttonConnect.SetBackgroundImage(UIImage.FromBundle("ButtonGold").AsNinePatch(), UIControlState.Normal);
      buttonConnect.SetBackgroundImage(UIImage.FromBundle("ButtonBlack").AsNinePatch(), UIControlState.Selected);
      buttonConnect.TouchUpInside += (object sender, EventArgs e) => {
        if (device != null) {
          if (EConnectionState.Disconnected == device.connection.connectionState) {
//            ion.deviceManager.ConnectDeviceAsync(device);
            device.connection.Connect();
          } else {
//            ion.deviceManager.DisconnectDevice(device);
            device.connection.Disconnect();
          }
        }
      };
    }

    // Overridden from UITableViewCell
    public override void PrepareForReuse() {
      base.PrepareForReuse();

      Release();
    }

    // Overridden from UITableViewCell
    public override void RemoveFromSuperview() {
      base.RemoveFromSuperview();

      Release();
    }

    // Overridden from IReleasable
    public void Release() {
      device = null;
    }

    /// <summary>
    /// Updates the device table cell to the given device.
    /// </summary>
    /// <param name="device">Device.</param>
    public void UpdateTo(IION ion, IDevice device, Action backgroundClickedResponder = null) {
      this.ion = ion;
      this.device = device;

      UpdateLabels();
      UpdateActivityViews();

      onBackgroundClicked = backgroundClickedResponder;
    }

    private void OnDeviceEvent(DeviceEvent deviceEvent) {
      var device = deviceEvent.device;

      switch (deviceEvent.type) {
        case DeviceEvent.EType.NewData: // TODO ahodder@appioninc.com: This may not be needed
        case DeviceEvent.EType.NameChanged:
          UpdateLabels();
          break;
        case DeviceEvent.EType.ConnectionChange:
          UpdateActivityViews();
          break;
      }
    }

    private void UpdateLabels() {
      imageDeviceIcon.Image = DeviceUtil.GetUIImageFromDeviceModel(device.serialNumber.deviceModel);

      labelDeviceType.Text = device.serialNumber.deviceModel.GetTypeString();
      labelDeviceName.Text = device.name;
    }

    private void UpdateActivityViews() {
      var state = device.connection.connectionState;
      if (EConnectionState.Connecting == state) {
        buttonConnect.SetImage(null, UIControlState.Normal);
        activityConnectStatus.Hidden = false;
      } else {
        if (device.isConnected) {
          buttonConnect.SetImage(UIImage.FromBundle("ic_bluetooth_connected"), UIControlState.Normal);
        } else {
          buttonConnect.SetImage(UIImage.FromBundle("ic_bluetooth_disconnected"), UIControlState.Normal);
        }
        activityConnectStatus.Hidden = true;
      }
    }
	}
}
