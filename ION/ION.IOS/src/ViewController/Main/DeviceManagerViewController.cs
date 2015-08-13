using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Foundation;
using UIKit;

using ION.Core.App;
using ION.Core.Connections;
using ION.Core.Devices;
using ION.Core.Measure;
using ION.Core.Util;

using ION.IOS.Util;

namespace ION.IOS.ViewController.Main {
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
      ion = AppState.context;
		}

    // Overridden from UIViewController
    public override void ViewDidLoad() {
      base.ViewDidLoad();

      buttonScan.TouchUpInside += (object sender, EventArgs e) => {
        ion.deviceManager.DoActiveScanAsync();
      };

      ion.deviceManager.onDeviceFound += HandleDeviceFound;
      ion.deviceManager.onDeviceManagerStateChanged += HandleDeviceManagerStateChanged;
      ion.deviceManager.onDeviceStateChanged += HandleDeviceStateChanged;

      HandleDeviceManagerStateChanged(ion.deviceManager, ion.deviceManager.state);

      tableDeviceList.Source = deviceSource = new DeviceSource(ion, tableDeviceList);
      UpdateSourceContent();
    }

    // Overridden from UIViewController
    public override void ViewDidUnload() {
      base.ViewDidUnload();

      ion.deviceManager.onDeviceFound -= HandleDeviceFound;
      ion.deviceManager.onDeviceManagerStateChanged -= HandleDeviceManagerStateChanged;
      ion.deviceManager.onDeviceStateChanged -= HandleDeviceStateChanged;
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
        if (EConnectionState.Connected == device.connection.connectionState) {
          connected.devices.Add(device);
        } else if (EConnectionState.Broadcasting == device.connection.connectionState) {
          longRange.devices.Add(device);
        } else if (!device.isKnown && device.isNearby) {
          newDevices.devices.Add(device);
        } else if (device.isKnown && device.isNearby) {
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

      deviceSource.SetContent(content);
    }

    /// <summary>
    /// The callback used by the device manager when a new device is found.
    /// </summary>
    /// <param name="dm">Dm.</param>
    /// <param name="device">Device.</param>
    private void HandleDeviceFound(IDeviceManager dm, IDevice device) {
      UpdateSourceContent();
    }

    /// <summary>
    /// The callback used by the device manager when its state changes.
    /// </summary>
    /// <param name="dm">Dm.</param>
    private void HandleDeviceManagerStateChanged(IDeviceManager dm, EDeviceManagerState state) {
      switch (state) {
        case EDeviceManagerState.ActiveScanning: {
          buttonScan.TitleLabel.Text = Strings.Device.Manager.SCANNING.FromResources();
          break;
        }
        default: {
          buttonScan.TitleLabel.Text = Strings.Device.Manager.SCAN.FromResources();
          break;
        }
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
    }
	} // End DeviceManagerViewController
}
