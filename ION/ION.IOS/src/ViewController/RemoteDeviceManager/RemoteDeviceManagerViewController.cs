namespace ION.IOS.ViewController.RemoteDeviceManager {

  using System;

  using CoreGraphics;
  using Foundation;
  using UIKit;

	using Appion.Commons.Util;

  using ION.Core.App;
  using ION.Core.Devices;
  using ION.Core.Sensors;

  using ION.IOS.Util;

	public partial class RemoteDeviceManagerViewController : BaseIONViewController {
    /// <summary>
    /// The time in milliseconds that the view controller will perform a scan for.
    /// </summary>
    private const int DEFAULT_SCAN_TIME = 60000;
    /// <summary>
    /// The delegate that is used to pass a sensor back from the device manager.
    /// </summary>
    public delegate void OnSensorReturn(GaugeDeviceSensor sensor);

    /// <summary>
    /// The action that will be fired when the user selects a sensor for returning 
    /// within the device manager.
    /// </summary>
    public OnSensorReturn onSensorReturnDelegate { get; set; }

    /// <summary>
    /// The filter that is used to limit the devices that are displayed in the view controller.
    /// </summary>
    public IFilter<Sensor> displayFilter {
      get {
        return __displayFilter;
      }
      set {
        __displayFilter = value;

        if (deviceSource != null) {
          deviceSource.sensorFilter = __displayFilter;
        }
      }
		} IFilter<Sensor> __displayFilter = null;

    /// <summary>
    /// The ion context for this view controller.
    /// </summary>
    /// <value>The ion.</value>
    private IION ion { get; set; }
    /// <summary>
    /// The device source for our device table.
    /// </summary>
    /// <value>The device source.</value>
    private RemoteDeviceManagerTableSource deviceSource { get; set; }
    /// <summary>
    /// Whether or not the view controller is allowing a refresh to be preformed.
    /// </summary>
    /// <value><c>true</c> if allow refresh; otherwise, <c>false</c>.</value>
    private bool allowRefresh { get; set; }
    
    public UITableView tableContent;

		public RemoteDeviceManagerViewController (IntPtr handle) : base (handle) {
      ion = AppState.context;
		}

    // Overridden from UIViewController
    public override void ViewDidLoad() {
      base.ViewDidLoad();

      InitNavigationBar("ic_nav_device_manager", true);
      View.BackgroundColor = UIColor.FromPatternImage (UIImage.FromBundle ("CarbonBackground"));
      NavigationItem.Title = Strings.Device.Manager.SELF.FromResources();

      labelEmpty.Text = Strings.Device.Manager.EMPTY; 

      ion.deviceManager.onDeviceManagerEvent += OnDeviceManagerEvent;	

			tableContent = new UITableView(new CGRect(.05 * View.Bounds.Width, 50, .9 * View.Bounds.Width, View.Bounds.Height - 50));
			tableContent.BackgroundColor = UIColor.Clear;
			tableContent.SeparatorStyle = UITableViewCellSeparatorStyle.None;
      tableContent.Source = deviceSource = new RemoteDeviceManagerTableSource(ion, this, tableContent);
      deviceSource.sensorFilter = displayFilter;
      deviceSource.onSensorAddClicked = (GaugeDeviceSensor sensor, NSIndexPath indexPath) => {
        if (onSensorReturnDelegate != null) {
          onSensorReturnDelegate(sensor);
          NavigationController.PopViewController(true);
          return true;
        }
        return false;
      };
      View.AddSubview(tableContent);
    }

    public override void ViewWillAppear(bool animated) {
      base.ViewWillAppear(animated);
      allowRefresh = true;
      deviceSource.Reload();
    }

    public override void ViewWillDisappear(bool animated) {
      base.ViewWillDisappear(animated);
      allowRefresh = false;
      deviceSource.Release();
    }

    // Overridden from UIViewController
    public override void ViewDidUnload() {
      base.ViewDidUnload();

      ion.deviceManager.onDeviceManagerEvent -= OnDeviceManagerEvent;

      deviceSource?.Release();
      deviceSource = null;
    }

    /// <summary>
    /// A bouncing call that will keep posting itself to the message pump to update the view controller.
    /// </summary>
    private void PostUpdate() {
      if (IsViewLoaded) {
        deviceSource.Reload();
        ion.PostToMainDelayed(PostUpdate, TimeSpan.FromMilliseconds(5000));
      }
    }

    /// <summary>
    /// The callback used by the device manager when its state changes.
    /// </summary>
    /// <param name="dm">Dm.</param>
    private void OnDeviceManagerEvent(DeviceManagerEvent e) {
      if(ion.deviceManager.connectionManager.isEnabled) {

      }
    }
	} // End DeviceManagerViewController
}
