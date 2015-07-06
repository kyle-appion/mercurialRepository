using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Android.Bluetooth;

using ION.Core.Connections;
using ION.Core.Devices;
using ION.Core.Util;

namespace ION.Droid.Devices {
  /// <summary>
  /// The android implementation of a DeviceManager. Uses bluetooth
  /// as its communication backend.
  /// </summary>
  public class AndroidDeviceManager : IDeviceManager, BluetoothAdapter.ILeScanCallback, IDisposable {
    /// <summary>
    /// The timeout that is applied to the device manager being enabled.
    /// </summary>
    private static readonly long TIMEOUT_ENABLE = 10000;
    /// <summary>
    /// The timeout for a LE scan.
    /// </summary>
    private static readonly long TIMEOUT_LE_ACTIVE_SCAN = 2500;
    /// <summary>
    /// The timeout for a classic scan.
    /// </summary>
    private static readonly long TIMEOUT_CLASSIC_ACTIVE_SCAN = 15000;
    /// <summary>
    /// The delay that occurs when touching the bluetooth adapter.
    /// </summary>
    private static readonly long DELAY_TRANSITION = 1000;

    // Overridden from IDeviceManager
    public event EventHandler<IDevice> onDeviceFound;
    // Overridden from IDeviceManager
    public event EventHandler<IDevice> onDeviceStateChanged;
    // Overridden from IDeviceManager
    public event EventHandler<ECommState> onDeviceManagerStateChanged;

    // Overridden from IDeviceManager
    public IDevice this[ISerialNumber serialNumber] {
      get {
        IDevice ret = __knownDevices[serialNumber];

        if (ret == null) {
          ret = __foundDevices[serialNumber];
        }

        return ret;
      }
    }

    // Overridden from IDeviceManager
    public List<IDevice> knownDevices { get { return __knownDevices.Values.ToList(); } }
    // Overridden from IDeviceManager
    public List<IDevice> foundDevices { get { return __knownDevices.Values.ToList(); } }

    // Overridden from IDeviceManager
    public ECommState state {
      get;
      private set {
        state = value;
        if (onDeviceStateChanged != null) {
          onDeviceManagerStateChanged(this, state);
        } else {
          Log.D(this, "Cannot notify device manager state changed: event handler is dead");
        }
      }
    }

    /// <summary>
    /// The mapping of known devices.
    /// </summary>
    private Dictionary<ISerialNumber, IDevice> __knownDevices = new Dictionary<ISerialNumber, IDevice>();
    /// <summary>
    /// The mapping of unknown devices.
    /// </summary>
    private Dictionary<ISerialNumber, IDevice> __foundDevices = new Dictionary<ISerialNumber, IDevice>();
    /// <summary>
    /// The android backend bluetooth communication system.
    /// </summary>
    private BluetoothAdapter __adapter;

    public AndroidDeviceManager() {
      __adapter = BluetoothAdapter.DefaultAdapter;
    }

    // Overridden from IDeviceManager
    public Task<bool> Enable() {
      return Task.Factory.StartNew(() => {
        Log.V(this, "Enabling bluetooth");
        if (!__adapter.Enable()) {
          return false;
        }

        DateTime start = DateTime.Now;
        while (!__adapter.IsEnabled) {
          if (DateTime.Now.Millisecond - start.Millisecond > TIMEOUT_ENABLE) {
            Log.E(this, "Failed to enable bluetooth");
            __adapter.Disable();
            return false;
          }
        }

        return true;
      });
    }

    // Overridden from IDeviceManager
    public Task<bool> DoActiveScan() {
      return Task.Factory.StartNew(() => {
        try {
          // Classic scan
          DoClassicScanAsync().Wait(TimeSpan.FromMilliseconds(TIMEOUT_CLASSIC_ACTIVE_SCAN));
          DoCancelClassicScanAsync().Wait();

          // LE scan
          DoLeScanAsync(TIMEOUT_LE_ACTIVE_SCAN).Wait(TimeSpan.FromMilliseconds(TIMEOUT_LE_ACTIVE_SCAN + 100));
          DoCancelLeScanAsync().Wait();

          return true;
        } catch (Exception e) {
          Log.E(this, "Cannot perform active scan: ", e);
          return false;
        }
      });
    }

    // Overridden from IDeviceManager
    public Task StopActiveScan() {
      return Task.Factory.StartNew(() => {
        DoCancelClassicScanAsync().Wait();
        DoCancelLeScanAsync().Wait();
      });
    }

    // Overridden from IDeviceManager
    public Task<bool> DoPassiveScan() {
      return Task.Factory.StartNew(() => {
        try {
          DoLeScanAsync(TIMEOUT_LE_ACTIVE_SCAN).Wait(TimeSpan.FromMilliseconds(TIMEOUT_LE_ACTIVE_SCAN + 100));
          DoCancelLeScanAsync().Wait();

          return true;
        } catch (Exception e) {
          Log.E(this, "Cannot perform passive scan: ", e);
          return false;
        }
      });
    }

    // Overridden from IDeviceManager
    public Task StopPassiveScan() {
      return Task.Factory.StartNew(() => {
        DoCancelLeScanAsync().Wait();
      });
    }

    // Overridden from IDeviceManager
    public void ForgetFoundDevices() {
      __foundDevices.Clear();
      state = state; // Posts a refresh event to the observers
    }

    // Overridden from IDeviceManager
    public Task<bool> ConnectDevice(IDevice device) {
      return Task.Factory.StartNew(() => {
        bool connected = device.connection.Connect().Result;

        if (connected) {
          if (__foundDevices.ContainsKey(device.serialNumber)) {
            __foundDevices.Remove(device.serialNumber);
          }

          if (!__knownDevices.ContainsKey(device.serialNumber)) {
            __knownDevices[device.serialNumber] = device;
          }
        }

        NotifyDeviceStateChanged(device);

        return connected;
      });
    }

    // Overridden from IDeviceManager
    public void DisconnectDevice(IDevice device) {
      Task.Factory.StartNew(() => {
        device.connection.Disconnect();

        NotifyDeviceStateChanged(device);
      });
    } 
       
    // Overridden from ILeScanCallback
    public void OnLeScan(BluetoothDevice device, int rssi, byte[] scanRecord) {
      // TODO ahodder@appioninc.com: Allocate found devices, determine protocols and register them
    }

    // Overridden from IDisposable
    public void Dispose() {
    }

    /// <summary>
    /// An asynchrous bluetooth classic scan call. This procedure will perform a
    /// classic scan that lasts as long as android deems necessary.
    /// </summary>
    /// <returns></returns>
    private Task DoClassicScanAsync() {
      // TODO ahodder@appioninc.com: Determine whether or not we are actually classic scanning.
      return Task.Factory.StartNew(() => {
        Log.V(this, "Starting classic scan");
        if (!__adapter.StartDiscovery()) {
          throw new Exception("Failed to start classic discovery");
        }

        while (__adapter.IsDiscovering) {
          Thread.Sleep(50);
        }
      });
    }

    /// <summary>
    /// Cancels a classic scan in a clean way that [hopefully] won't rape android's
    /// bluetooth stack. It was noticed on the original java based application that
    /// repreated touching of the bluetooth adapter in a quick fachion would ruin the
    /// adapter's state, making future communication unreliable.
    /// </summary>
    /// <returns></returns>
    private Task DoCancelClassicScanAsync() {
      // TODO ahodder@appioninc.com: Determine whether or not we are actually classic scanning.
      return Task.Factory.StartNew(() => {
        Log.V(this, "Cancelling classic scan");
        if (__adapter.IsDiscovering) {
          __adapter.CancelDiscovery();
        }

        DateTime start = DateTime.Now;

        while (DateTime.Now.Millisecond - start.Millisecond < DELAY_TRANSITION) {
          Thread.Sleep(50);
        }
      });
    }

    /// <summary>
    /// An asynchronous bluetooth le scan call. This procedure will perform a BLE
    /// scan operation for the given duration.
    /// </summary>
    /// <param name="durationMillis"></param>
    /// <returns></returns>
    private Task DoLeScanAsync(long durationMillis) {
      // TODO ahodder@appioninc.com: Determine whether or not we are actually le scanning.
      return Task.Factory.StartNew(() => {
        Log.V(this, "Starting LE scan");
        if (!__adapter.StartLeScan(this)) {
          throw new Exception("Failed to start LE scan");
        }

        DateTime start = DateTime.Now;

        while (DateTime.Now.Millisecond - start.Millisecond < durationMillis) {
          Thread.Sleep(50);
        }
      });
    }

    /// <summary>
    /// Cancels a LE scan in a clean way that [hopefully] won't rape android's
    /// bluetooth stack. As with a classic scan, but much more pronounced with LE,
    /// android's bluetooth stack would become immensely unstable with repeated,
    /// frequent access to the adapter state.
    /// </summary>
    /// <returns></returns>
    private Task DoCancelLeScanAsync() {
      // TODO ahodder@appioninc.com: Determine whether or not we are actually le scanning.
      return Task.Factory.StartNew(() => {
        Log.V(this, "Stopping LE scan");
        __adapter.StopLeScan(this);

        DateTime start = DateTime.Now;

        while (DateTime.Now.Millisecond - start.Millisecond < DELAY_TRANSITION) {
          Thread.Sleep(50);
        }
      });
    }

    /*
    // TODO ahodder@appioninc.com: What is going on here?
    private void NotifyDeviceStateChangedAsync(IDevice device) {
      Task.Factory.StartNew(() => NotifyDeviceStateChanged(device));
    }
    */

    /// <summary>
    /// Notifies the on device state changed event handler that the given device
    /// has had a state change.
    /// </summary>
    /// <param name="device"></param>
    private void NotifyDeviceStateChanged(IDevice device) {
      if (onDeviceStateChanged != null) {
        onDeviceStateChanged(this, device);
      } else {
        Log.D(this, "Cannot notify device state change: event handler is dead");
      }
    }
  }
}