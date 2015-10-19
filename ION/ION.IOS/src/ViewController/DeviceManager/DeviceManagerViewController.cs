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
using ION.Core.Measure;
using ION.Core.Sensors;
using ION.Core.Util;

using ION.IOS.UI;
using ION.IOS.Util;

namespace ION.IOS.ViewController.DeviceManager {
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
          deviceSource.sensorFilter = __displayFilter;
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
    private DeviceSource deviceSource { get; set; }

		public DeviceManagerViewController (IntPtr handle) : base (handle) {
      ion = AppState.context;
		}

    // Overridden from UIViewController
    public override void ViewDidLoad() {
      base.ViewDidLoad();

      // TODO ahodder@appioninc.com: This asserts that the view controller is be opened
      // with the intent to return a sensor.
      InitNavigationBar("ic_nav_device_manager", true);

      NavigationItem.Title = Strings.Device.Manager.SELF.FromResources();
      NavigationItem.RightBarButtonItem = new UIBarButtonItem(Strings.Device.Manager.SCAN.FromResources(), UIBarButtonItemStyle.Plain, delegate {
        if (ion.deviceManager.connectionHelper.isScanning) {
          ion.deviceManager.connectionHelper.Stop();
        } else {
          if (!ion.deviceManager.connectionHelper.Scan(TimeSpan.FromMilliseconds(DEFAULT_SCAN_TIME))) {
            Toast.New(View, Strings.Errors.SCAN_INIT_FAIL);
          }
        }
      });

      ion.deviceManager.onDeviceFound += HandleDeviceFound;
      ion.deviceManager.onDeviceManagerStatesChanged += HandleDeviceManagerStatesChanged;
      ion.deviceManager.onDeviceStateChanged += HandleDeviceStateChanged;

      HandleDeviceManagerStatesChanged(ion.deviceManager);

      tableContent.Source = deviceSource = new DeviceSource(ion, tableContent);
      deviceSource.sensorFilter = displayFilter;
      deviceSource.onSensorAddClicked = (GaugeDeviceSensor sensor, NSIndexPath indexPath) => {
        if (onSensorReturnDelegate != null) {
          onSensorReturnDelegate(sensor);
          NavigationController.PopViewController(true);
          return true;
        }
        return false;
      };
      this.PostUpdate();
    }

    // Overridden from UIViewController
    public override void ViewDidUnload() {
      base.ViewDidUnload();

      ion.deviceManager.onDeviceFound -= HandleDeviceFound;
      ion.deviceManager.onDeviceManagerStatesChanged -= HandleDeviceManagerStatesChanged;
      ion.deviceManager.onDeviceStateChanged -= HandleDeviceStateChanged;

      tableContent.Source = null;
    }

    /// <summary>
    /// Updates the source with new device content.
    /// </summary>
    private void UpdateSourceContent() {
      var connected = new DeviceGroup(Strings.Device.CONNECTED.FromResources(), Colors.GREEN);
      var longRange = new DeviceGroup(Strings.Device.LONG_RANGE.FromResources(), Colors.LIGHT_BLUE);
      var newDevices = new DeviceGroup(Strings.Device.NEW_DEVICES.FromResources(), Colors.LIGHT_GRAY);
      var available = new DeviceGroup(Strings.Device.AVAILABLE.FromResources(), Colors.YELLOW);
      var disconnected = new DeviceGroup(Strings.Device.DISCONNECTED.FromResources(), Colors.RED);

      var groups = new List<DeviceGroup>();
      groups.AddRange(new DeviceGroup[] { connected, longRange, newDevices, available, disconnected });

      foreach (IDevice device in ion.deviceManager.devices) {
        if (device is GaugeDevice) {
          var gauge = (GaugeDevice)device;
          bool hasSensor = false;
          foreach (var sensor in gauge.sensors) {
            if (displayFilter.Matches(sensor)) {
              hasSensor = true;
              break;
            }
          }
          if (!hasSensor) {
            continue;
          }
        }

        if (EConnectionState.Connected == device.connection.connectionState) {
          connected.devices.Add(device);
        } else if (EConnectionState.Broadcasting == device.connection.connectionState) {
          longRange.devices.Add(device);
        } else if (!ion.deviceManager.IsDeviceKnown(device) && device.isNearby) {
          newDevices.devices.Add(device);
        } else if (ion.deviceManager.IsDeviceKnown(device) && device.isNearby) {
          available.devices.Add(device);
        } else {
          disconnected.devices.Add(device);
        }
      }

      var content = new List<DeviceGroup>();

      foreach (DeviceGroup group in groups) {
        if (group.devices.Count > 0) {
          content.Add(group);
        }
      }

      if (content.Count > 0) {
        deviceSource.SetContent(content);
        tableContent.Hidden = false;
        labelEmpty.Hidden = true;
      } else {
        tableContent.Hidden = true;
        labelEmpty.Hidden = false;
      }
    }

    /// <summary>
    /// A bouncing call that will keep posting itself to the message pump to update the view controller.
    /// </summary>
    private void PostUpdate() {
      if (IsViewLoaded) {
        UpdateSourceContent();
//        ion.PostToMainDelayed(PostUpdate, TimeSpan.FromMilliseconds(5000));
      }
    }

    /// <summary>
    /// The callback used by the device manager when a new device is found.
    /// </summary>
    /// <param name="dm">Dm.</param>
    /// <param name="device">Device.</param>
    private void HandleDeviceFound(IDeviceManager dm, IDevice device) {
      ion.PostToMain(UpdateSourceContent);
//      UpdateSourceContent();
    }

    /// <summary>
    /// The callback used by the device manager when its state changes.
    /// </summary>
    /// <param name="dm">Dm.</param>
    private void HandleDeviceManagerStatesChanged(IDeviceManager dm) {
      if (dm.connectionHelper.isScanning) {
        NavigationItem.RightBarButtonItem.Title = Strings.Device.Manager.SCANNING.FromResources();
      } else {
        NavigationItem.RightBarButtonItem.Title = Strings.Device.Manager.SCAN.FromResources();
      }
    }

    /// <summary>
    /// The callbacks used by the device manager when a device's state changes.
    /// </summary>
    /// <param name="device">Device.</param>
    private void HandleDeviceStateChanged(IDevice device) {
      Task.Factory.StartNew(() => {
        UpdateSourceContent();
      }, Task.Factory.CancellationToken, TaskCreationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());

      Task.Factory.StartNew(() => {
        if (device.isConnected) {
          deviceSource.ExpandDevice(device);
        }
      }, Task.Factory.CancellationToken, TaskCreationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());
    }
	} // End DeviceManagerViewController
}
