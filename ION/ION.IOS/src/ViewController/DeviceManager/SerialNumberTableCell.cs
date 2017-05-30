namespace ION.IOS.ViewController.DeviceManager {

  using System;

  using Foundation;
  using UIKit;

  using ION.Core.Devices;

  using ION.IOS.Util;

  public class SerialNumberRecord : IRecord {
    public EViewType viewType {
      get {
        return EViewType.SerialNumber;
      }
    }

    public bool isExpandable { 
      get {
        return false;
      }
    }
    public bool isExpanded { get; set; }

    public ISerialNumber serialNumber { get; set; }

    public SerialNumberRecord(ISerialNumber serialNumber) {
      this.serialNumber = serialNumber;
    }
  }

	public partial class SerialNumberTableCell : UITableViewCell {
		public SerialNumberTableCell (IntPtr handle) : base (handle) {
		}

    /// <summary>
    /// Updates the cell to the given device.
    /// </summary>
    /// <param name="device">Device.</param>
    public void UpdateTo(SerialNumberRecord record) {
      labelSerialNumber.Text = Strings.Device.SERIAL + " - " + record.serialNumber.ToString();
    }
	}
}
