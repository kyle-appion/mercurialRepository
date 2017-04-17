namespace ION.Droid.Connections {

  using System;
  using System.Collections.Generic;
  using System.Threading.Tasks;

  using Android.Bluetooth;
  using Android.Content;
  using Android.OS;

  using Appion.Commons.Util;

  using ION.Core.Devices.Protocols;

  using ION.Droid.App;

  public class ClassicScanMethod : BroadcastReceiver, IScanMethod {
    // Implemented for IScanMethod 
    public bool isScanning { get; private set; }

    /// <summary>
    /// The manager that will allow us access to the current connection and bluetooth states.
    /// </summary>
    private AndroidConnectionManager manager; 

    private Dictionary<string, BluetoothDevice> classicDevices = new Dictionary<string, BluetoothDevice>();

    public ClassicScanMethod(AndroidConnectionManager manager) {
      this.manager = manager;
    }

    // Overridden from BroadcastReceiver
    public override void OnReceive(Context context, Intent intent) {
      switch (intent.Action) {
        case BluetoothDevice.ActionFound: {
          VerifyDevice(intent.GetParcelableExtra(BluetoothDevice.ExtraDevice) as BluetoothDevice);
          break;
        } // BluetoothDevice.ActionFound

        case BluetoothAdapter.ActionDiscoveryFinished: {
          StopScan();
          break;
        } // BluetoothAdapter.ActionDiscoveryFinished
      }
    }

    // Implemented for IScanMethod
    public bool StartScan() {
      Log.D(this, "Starting Scan!");
      lock (manager.locker) {
        if (isScanning) {
          return true;
        }

        var filter = new IntentFilter();
        filter.AddAction(BluetoothDevice.ActionFound);
        filter.AddAction(BluetoothAdapter.ActionDiscoveryFinished);
        manager.context.RegisterReceiver(this, filter);

        isScanning = manager.bm.Adapter.StartDiscovery();
        return isScanning;
      }
    }

    // Implemented for IScanMethod
    public void StopScan() {
      Log.D(this, "Stopping Scan!");
      lock (manager.locker) {
        if (!isScanning) {
          return;
        }

        isScanning = false;
        try {
          // This throws an exception is we call it after we have already unregistered; what a terrible design
          manager.context.UnregisterReceiver(this);
        } catch (Exception e) {
          Log.E(this, "Attempted to unregister classic receiver too many times", e);
        }
      }
    }

    /// <summary>
    /// Attempts to resolve the device if it is an appion device.
    /// </summary>
    /// <param name="device">Device.</param>
    private void VerifyDevice(BluetoothDevice device) {
      lock (manager.locker) {
        if (!manager.HasConnectionForAddress(device.Address)) {
          if (Protocol.APPION_CLASSIC_DEVICE_NAME.Equals(device.Name)) {
            Task.Factory.StartNew(async () => {
              var sn = await ClassicConnection.ResolveSerialNumber(device);
              if (sn != null) {
                manager.OnClassicDeviceFound(sn, device);
              }
            });
          }
        }
      }
    }
  }
}
