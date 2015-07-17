using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

using Foundation;
using UIKit;

using ION.Core.App;
using ION.Core.Devices;
using ION.Core.Util;

using ION.IOS.ViewController;

namespace ION.IOS.ViewController.DeviceManager {
	public partial class DeviceManagerViewController : UIViewController {

    /// <summary>
    /// The ion context for this view controller.
    /// </summary>
    /// <value>The ion.</value>
    private IION ion { get; set; }
    /// <summary>
    /// The device source for our device table.
    /// </summary>
    /// <value>The device source.</value>
    private DeviceSource deviceSource { get; set; }

		public DeviceManagerViewController (IntPtr handle) : base (handle) {
      ion = AppState.APP;
		}

    // Overridden from UIViewController
    public override void ViewDidLoad() {
      base.ViewDidLoad();

      buttonScan.TouchUpInside += (object sender, EventArgs e) => {
        ion.deviceManager.DoActiveScan();
      };

      tableDeviceList.Source = deviceSource = new DeviceSource(tableDeviceList, ion.deviceManager.devices);

      ion.deviceManager.onDeviceFound += HandleDeviceFound;
      ion.deviceManager.onDeviceManagerStateChanged += HandleDeviceManagerStateChanged;
      ion.deviceManager.onDeviceStateChanged += HandleDeviceStateChanged;
    }

    // Overridden from UIViewController
    public override void ViewDidUnload() {
      base.ViewDidUnload();

      ion.deviceManager.onDeviceFound -= HandleDeviceFound;
      ion.deviceManager.onDeviceManagerStateChanged -= HandleDeviceManagerStateChanged;
      ion.deviceManager.onDeviceStateChanged -= HandleDeviceStateChanged;
    }

    /// <summary>
    /// The callback used by the device manager when a new device is found.
    /// </summary>
    /// <param name="dm">Dm.</param>
    /// <param name="device">Device.</param>
    private void HandleDeviceFound(IDeviceManager dm, IDevice device) {
      Log.D(this, "Handling device found!");
      deviceSource.SetDevices(ion.deviceManager.devices);
    }

    /// <summary>
    /// The callback used by the device manager when its state changes.
    /// </summary>
    /// <param name="dm">Dm.</param>
    private void HandleDeviceManagerStateChanged(IDeviceManager dm, EDeviceManagerState state) {
      Log.D(this, "Handling device manager state changed!");
    }

    /// <summary>
    /// The callbacks used by the device manager when a device's state changes.
    /// </summary>
    /// <param name="device">Device.</param>
    private void HandleDeviceStateChanged(IDevice device) {
      Log.D(this, "Handling device state changed!");
    }
	} // End DeviceManagerViewController


  /// <summary>
  /// The table source that will provide the cell views for the device
  /// manager's table view 
  /// </summary>
  internal class DeviceSource : UITableViewSource {
    private const string CELL_DEVICE = "cell_device";


    /// <summary>
    /// The table view that this entity is a source to.
    /// </summary>
    /// <value>The table view.</value>
    private UITableView tableView { get; set; }
    /// <summary>
    /// The devices that are present within the source.
    /// </summary>
    private List<IDevice> __devices = new List<IDevice>();

    /*
    /// <summary>
    /// The states.
    /// </summary>
    private List<EDeviceState> __states = new List<EDeviceState>();
    /// <summary>
    /// The dictionary that is holding the content of the table source.
    /// </summary>
    private Dictionary<EDeviceState, List<IDevice>> __content = new Dictionary<EDeviceState, IDevice>();
    */

    public DeviceSource(UITableView tableView, List<IDevice> devices) {
      this.tableView = tableView;
      tableView.RegisterNibForCellReuse(DeviceManagerDeviceCell.CreateNib(), CELL_DEVICE);
      SetDevices(devices);
    }

    // Overridden from UITableViewSource
    public override nint NumberOfSections(UITableView tableView) {
      return 1;
    }

    // Overridden from UITableViewSource
    public override nint RowsInSection(UITableView tableview, nint section) {
      return __devices.Count;
    }

    /*
    public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath) {
      
    }
    */

    // Overridden from UITableViewSource
    public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath) {
      // Gets the next cell or creates one from the registered nib.
      UITableViewCell cell = tableView.DequeueReusableCell(CELL_DEVICE);

      IDevice device = __devices[(int)indexPath.Item];

      if (cell is DeviceManagerDeviceCell) {
        ((DeviceManagerDeviceCell)cell).Apply(device);
      }

      return cell;
    }

    /// <summary>
    /// Sets the content of the table source.
    /// </summary>
    /// <param name="devices">Devices.</param>
    public void SetDevices(List<IDevice> devices) {
      __devices.Clear();

      __devices.AddRange(devices);

      tableView.ReloadData();
    }
  } // End DeviceSource
}
