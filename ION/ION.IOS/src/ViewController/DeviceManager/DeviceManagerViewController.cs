namespace ION.IOS.ViewController.DeviceManager {

  using System;
  using System.Collections.Generic;
  using System.Threading;
  using System.Threading.Tasks;

  using CoreGraphics;
  using Foundation;
  using UIKit;

  using ION.Core.App;
  using ION.Core.Connections;
  using ION.Core.Devices;
  using ION.Core.Devices.Connections;
  using ION.Core.Measure;
  using ION.Core.Sensors;
  using ION.Core.Util;

  using ION.IOS.UI;
  using ION.IOS.Util;

	public partial class DeviceManagerViewController : BaseIONViewController {
    /// <summary>
    /// The time in milliseconds that the view controller will perform a scan for.
    /// </summary>
    private const int DEFAULT_SCAN_TIME = 5000;
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
        if (value == null) {
          value = new YesFilter<Sensor>();
        }

        __displayFilter = value;

        if (deviceSource != null) {
          deviceSource.SetSensorFilter(__displayFilter);
        }
      }
    } IFilter<Sensor> __displayFilter = new YesFilter<Sensor>();

    /// <summary>
    /// The ion context for this view controller.
    /// </summary>
    /// <value>The ion.</value>
    private IION ion { get; set; }
    /// <summary>
    /// The device source for our device table.
    /// </summary>
    /// <value>The device source.</value>
    private DeviceTableSource deviceSource { get; set; }
    /// <summary>
    /// Whether or not the view controller is allowing a refresh to be preformed.
    /// </summary>
    /// <value><c>true</c> if allow refresh; otherwise, <c>false</c>.</value>
    private bool allowRefresh { get; set; }

		public DeviceManagerViewController (IntPtr handle) : base (handle) {
      ion = AppState.context;
		}

    // Overridden from UIViewController
    public override void ViewDidLoad() {
      base.ViewDidLoad();

      InitNavigationBar("ic_nav_device_manager", true);

      NavigationItem.Title = Strings.Device.Manager.SELF.FromResources();
      NavigationItem.RightBarButtonItem = new UIBarButtonItem(Strings.Device.Manager.SCAN.FromResources(), UIBarButtonItemStyle.Plain, delegate {
        if (ion.deviceManager.connectionHelper.isScanning) {
          ion.deviceManager.connectionHelper.Stop();
        } else {
//          var opts = new ScanRepeatOptions(ScanRepeatOptions.REPEAT_FOREVER, TimeSpan.FromMilliseconds(5000));
          if (!ion.deviceManager.connectionHelper.Scan(TimeSpan.FromMilliseconds(DEFAULT_SCAN_TIME))) {
            Toast.New(View, Strings.Errors.SCAN_INIT_FAIL);
          } else {
            ion.deviceManager.connectionHelper.Scan(TimeSpan.FromMilliseconds(DEFAULT_SCAN_TIME));
          }
        }
      });

      ion.deviceManager.onDeviceManagerEvent += OnDeviceManagerEvent;

      UpdateScanState();

      tableContent.Source = deviceSource = new DeviceTableSource(ion, this, tableContent);
      deviceSource.SetSensorFilter(displayFilter);
      deviceSource.onSensorAddClicked = (GaugeDeviceSensor sensor, NSIndexPath indexPath) => {
        if (onSensorReturnDelegate != null) {
          onSensorReturnDelegate(sensor);
          NavigationController.PopViewController(true);
          return true;
        }
        return false;
      };

//      deviceSource.onDeleteDevice = (IDevice device) => {
//        ion.deviceManager.DeleteDevice(device.serialNumber);
//        return true;
//      };
//      this.PostUpdate();
    }

    public override void ViewWillAppear(bool animated) {
      base.ViewWillAppear(animated);
      allowRefresh = true;
      ion.deviceManager.connectionHelper.Scan(TimeSpan.FromMilliseconds(DEFAULT_SCAN_TIME));
//      PostUpdate();
    }

    public override void ViewWillDisappear(bool animated) {
      base.ViewWillDisappear(animated);
      allowRefresh = false;
      ion.deviceManager.connectionHelper.Stop();
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
        deviceSource.RefreshContent();
        ion.PostToMainDelayed(PostUpdate, TimeSpan.FromMilliseconds(5000));
      }
    }


    /// <summary>
    /// The callback used by the device manager when its state changes.
    /// </summary>
    /// <param name="dm">Dm.</param>
    private void OnDeviceManagerEvent(DeviceManagerEvent e) {
      UpdateScanState();
    }

    private void UpdateScanState() {
      if (ion.deviceManager.connectionHelper.isScanning) {
        NavigationItem.RightBarButtonItem.Title = Strings.Device.Manager.SCANNING.FromResources();
      } else {
        NavigationItem.RightBarButtonItem.Title = Strings.Device.Manager.SCAN.FromResources();
      }
    }
	} // End DeviceManagerViewController
}
