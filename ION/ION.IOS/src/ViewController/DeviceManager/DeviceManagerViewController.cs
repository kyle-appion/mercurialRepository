namespace ION.IOS.ViewController.DeviceManager {

  using System;

  using CoreFoundation;
  using Foundation;
  using UIKit;

  using Appion.Commons.Measure;
  using Appion.Commons.Util;

  using ION.Core.App;
  using ION.Core.Devices;
  using ION.Core.Sensors;

  using ION.IOS.Connections;
  using ION.IOS.UI;
  using ION.IOS.Util;

  public partial class DeviceManagerViewController : BaseIONViewController {
    /// <summary>
    /// The time in milliseconds that the view controller will perform a scan for.
    /// </summary>
    private const int DEFAULT_SCAN_TIME = 7500;
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
    }
    IFilter<Sensor> __displayFilter = null;

    /// <summary>
    /// The ion context for this view controller.
    /// </summary>
    /// <value>The ion.</value>
    private IION ion { get; set; }
    /// <summary>
    /// The device source for our device table.
    /// </summary>
    /// <value>The device source.</value>
    private DeviceManagerTableSource deviceSource { get; set; }
    /// <summary>
    /// Whether or not the view controller is allowing a refresh to be preformed.
    /// </summary>
    /// <value><c>true</c> if allow refresh; otherwise, <c>false</c>.</value>
    private bool allowRefresh { get; set; }

    public DeviceManagerViewController(IntPtr handle) : base(handle) {
      ion = AppState.context;
    }

    // Overridden from UIViewController
    public override void ViewDidLoad() {
      base.ViewDidLoad();

      InitNavigationBar("ic_nav_device_manager", true);
      View.BackgroundColor = UIColor.FromPatternImage(UIImage.FromBundle("CarbonBackground"));
      NavigationItem.Title = Strings.Device.Manager.SELF.FromResources();
      NavigationItem.RightBarButtonItem = new UIBarButtonItem(Strings.Device.Manager.SCAN.FromResources(), UIBarButtonItemStyle.Plain, delegate {
        if (!ion.deviceManager.connectionManager.isEnabled) {
          UIAlertView bluetoothWarning = new UIAlertView("Bluetooth Disconnected", "Bluetooth needs to be connected to discover peripherals", null, "Close", "Settings");
          bluetoothWarning.Clicked += (sender, e) => {
            if (e.ButtonIndex.Equals(1)) {
              UIApplication.SharedApplication.OpenUrl(new NSUrl(UIApplication.OpenSettingsUrlString));
            }
          };
          bluetoothWarning.Show();
        } else {
          if (ion.deviceManager.connectionManager.isScanning) {
            StopScan();
          } else {
            StartScan();
          }
        }
      });

      labelEmpty.Text = Strings.Device.Manager.EMPTY;

      ion.deviceManager.onDeviceManagerEvent += OnDeviceManagerEvent;
      if (ion.deviceManager.connectionManager.isEnabled) {
        UpdateScanState();
      }
      tableContent.Source = deviceSource = new DeviceManagerTableSource(ion, this, tableContent);
      deviceSource.sensorFilter = displayFilter;
      deviceSource.onSensorAddClicked = (GaugeDeviceSensor sensor, NSIndexPath indexPath) => {
        if (onSensorReturnDelegate != null) {
          onSensorReturnDelegate(sensor);
          NavigationController.PopViewController(true);
          return true;
        }
        return false;
      };
    }

    public override void ViewWillAppear(bool animated) {
      base.ViewWillAppear(animated);
      allowRefresh = true;
      StartScan();
      deviceSource.Reload();
//      PostUpdate();
    }

    public override void ViewWillDisappear(bool animated) {
      base.ViewWillDisappear(animated);
      allowRefresh = false;
      ion.deviceManager.connectionManager.StopScan();
      deviceSource.Release();
      var cm = ion.deviceManager.connectionManager as IonCBCentralManagerDelegate;
      cm.ClearUnusedConnections();
      ion.deviceManager.ForgetFoundDevices();
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

    private void StartScan() {
      Log.D(this, "Starting scan");
      if (!ion.deviceManager.connectionManager.StartScan()) {
        Toast.New(View, Strings.Errors.SCAN_INIT_FAIL);
      } else {
        DispatchQueue.MainQueue.DispatchAfter(TimeSpan.FromMilliseconds(DEFAULT_SCAN_TIME), () => StopScan());
      }
    }

    private void StopScan() {
      Log.D(this, "Stopping scan");
      ion.deviceManager.connectionManager.StopScan();
    }

    /// <summary>
    /// The callback used by the device manager when its state changes.
    /// </summary>
    /// <param name="dm">Dm.</param>
    private void OnDeviceManagerEvent(DeviceManagerEvent e) {
      switch (e.type) {
        case DeviceManagerEvent.EType.ScanStarted: // Fallthrough
        case DeviceManagerEvent.EType.ScanStopped: {
          UpdateScanState();
          break;
        }
        default: {
          break;
        }
      }
      if (ion.deviceManager.connectionManager.isEnabled) {
        UpdateScanState();
      }
    }

    private void UpdateScanState() {
      //Log.D(this, "Updating scan state");
      if (ion.deviceManager.connectionManager.isScanning) {
        NavigationItem.RightBarButtonItem.Title = Strings.Device.Manager.SCANNING.FromResources();
      } else {
        NavigationItem.RightBarButtonItem.Title = Strings.Device.Manager.SCAN.FromResources();
      }
    }
  } // End DeviceManagerViewController
}
