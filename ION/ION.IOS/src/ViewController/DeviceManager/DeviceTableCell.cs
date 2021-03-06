namespace ION.IOS.ViewController.DeviceManager {

  using System;

  using UIKit;

  using SWTableViewCell;

	using Appion.Commons.Util;

  using ION.Core.App;
  using ION.Core.Connections;
  using ION.Core.Devices;

  using ION.IOS.Devices;
  using ION.IOS.UI;

  public class DeviceRecord : IRecord {
    /// <summary>
    /// The view type of the record.
    /// </summary>
    /// <value>The type of the view.</value>
    public EViewType viewType {
      get {
        return EViewType.Device;
      }
    }

    public IDevice device { get; set; }
    public bool isExpandable { 
      get {
        return device is GaugeDevice;
      }
    }
    public bool isExpanded { get; set; }

    public DeviceRecord(IDevice device, bool expanded = false) {
      this.device = device;
      this.isExpanded = expanded;
    }
  }

	public partial class DeviceTableCell : SWTableViewCell, IReleasable {
    /// <summary>
    /// The ion context. Necessary to properly connect a device.
    /// </summary>
    /// <value>The ion.</value>
    private IION ion { get; set; }
    /// <summary>
    /// The current device that the cell is displaying.
    /// </summary>
    /// <value>The device.</value>
    private DeviceRecord record {
      get {
        return __record;
      }
      set {
        if (__record != null) {
          __record.device.onDeviceEvent -= OnDeviceEvent;
        }

        __record = value;

        if (__record != null) {
          __record.device.onDeviceEvent += OnDeviceEvent;
          UpdateLabels();
          UpdateActivityViews();
        }
      }
    } DeviceRecord __record;
    
		public DeviceTableCell (IntPtr handle) : base (handle) {
		}

    // Overridden from UITableViewCell
    public override void AwakeFromNib() {
      base.AwakeFromNib();

      buttonConnect.SetBackgroundImage(UIImage.FromBundle("ButtonGold").AsNinePatch(), UIControlState.Normal);
      buttonConnect.SetBackgroundImage(UIImage.FromBundle("ButtonBlack").AsNinePatch(), UIControlState.Selected);
      buttonConnect.TouchUpInside += (object sender, EventArgs e) => {
        if (record != null) {
          // TODO ahodder@appioninc.com: Unify this connection process.
          if (EConnectionState.Disconnected == record.device.connection.connectionState) {
            record.device.connection.Connect();
          } else {
            record.device.connection.Disconnect();
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
      record = null;
    }

    /// <summary>
    /// Updates the device table cell to the given device.
    /// </summary>
    /// <param name="device">Device.</param>
    public void UpdateTo(IION ion, DeviceRecord record) {
      this.ion = ion;
      this.record = record;

      UpdateLabels();
      UpdateActivityViews();
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
      var device = record.device;

      imageDeviceIcon.Image = DeviceUtil.GetUIImageFromDeviceModel(device.serialNumber.deviceModel);
      labelDeviceType.Text = device.serialNumber.deviceModel.GetTypeString();
      labelDeviceName.Text = device.name;
    }

    private void UpdateActivityViews() {
      var device = record.device;
      var state = device.connection.connectionState;

      if (EConnectionState.Resolving == state) {
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
