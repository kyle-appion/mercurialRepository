namespace ION.IOS.ViewController.RemoteDeviceManager {

  using System;
	using CoreGraphics;
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

	public partial class RemoteDeviceTableCell : UITableViewCell, IReleasable {
		public UIView viewBackground;
		public UIButton buttonConnect;
		public UIImageView imageDeviceIcon;
		public UILabel labelDeviceType;
		public UILabel labelDeviceName;

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
          if(imageDeviceIcon != null){
	          UpdateLabels();
	          UpdateActivityViews();
          }
        }
      }
    } DeviceRecord __record;
    
		public RemoteDeviceTableCell (IntPtr handle) {
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
    public void UpdateTo(IION ion, DeviceRecord record, double cellWidth) {
      this.ion = ion;
      this.record = record;
      this.BackgroundColor = UIColor.White;
      this.Layer.BorderWidth = 1f;
      
      var cellHeight = 48;
      viewBackground = new UIView(new CGRect(0,0,cellWidth, cellHeight));
      viewBackground.BackgroundColor = UIColor.White;
      
			buttonConnect = new UIButton(new CGRect(cellWidth - cellHeight,0, cellHeight, cellHeight));
      buttonConnect.SetBackgroundImage(UIImage.FromBundle("ButtonGold").AsNinePatch(), UIControlState.Normal);
      buttonConnect.SetBackgroundImage(UIImage.FromBundle("ButtonBlack").AsNinePatch(), UIControlState.Selected);

      imageDeviceIcon = new UIImageView(new CGRect(0,0,cellHeight,cellHeight));

      labelDeviceType = new UILabel(new CGRect(cellHeight, 0, cellWidth - cellHeight, .5 * cellHeight));
      labelDeviceName = new UILabel(new CGRect(cellHeight, .5 * cellHeight, cellWidth - cellHeight, .5 * cellHeight));

      viewBackground.AddSubview(buttonConnect);
      viewBackground.AddSubview(imageDeviceIcon);
      viewBackground.AddSubview(labelDeviceName);
      viewBackground.AddSubview(labelDeviceType);
      this.AddSubview(viewBackground);

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

      if (EConnectionState.Connecting == state) {
        buttonConnect.SetImage(null, UIControlState.Normal);
      } else {
        if (device.isConnected) {
          buttonConnect.SetImage(UIImage.FromBundle("ic_bluetooth_connected"), UIControlState.Normal);
        } else {
          buttonConnect.SetImage(UIImage.FromBundle("ic_bluetooth_disconnected"), UIControlState.Normal);
        }
      }
    }
	}
}
