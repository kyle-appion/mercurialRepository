using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Android.Bluetooth;
using Android.Content;

using ION.Core.Connections;
using ION.Core.Devices;
using ION.Core.Devices.Protocols;
using ION.Core.Threading;
using ION.Core.Util;

using ION.Droid.Connections;

namespace ION.Droid.Devices {
  /// <summary>
  /// The android implementation of a DeviceManager. Uses bluetooth
  /// as its communication backend.
  /// </summary>
  public class AndroidDeviceManager : Java.Lang.Object, IDeviceManager, BluetoothAdapter.ILeScanCallback {
    /// <summary>
    /// The timeout that is applied to the device manager being enabled.
    /// </summary>
    private const long TIMEOUT_ENABLE = 10000;
    /// <summary>
    /// The timeout for a LE scan.
    /// </summary>
    private const long TIMEOUT_LE_ACTIVE_SCAN = 2500;
    /// <summary>
    /// The time for a passive le scan.
    /// </summary>
    private const long TIMEOUT_LE_PASSIVE_SCAN = 500;
    /// <summary>
    /// The timeout for a classic scan.
    /// </summary>
    private const long TIMEOUT_CLASSIC_ACTIVE_SCAN = 15000;
    /// <summary>
    /// The delay that occurs when touching the bluetooth adapter.
    /// </summary>
    private const long DELAY_TRANSITION = 1000;

    // Overridden from IDeviceManager
    public event OnDeviceFound onDeviceFound;
    // Overridden from IDeviceManager
    public event OnDeviceStateChanged onDeviceStateChanged;
    // Overridden from IDeviceManager
    public event OnDeviceManagerStateChanged onDeviceManagerStateChanged;

    // Overridden from IDeviceManager
    public IDevice this[ISerialNumber serialNumber] {
      get {
        IDevice ret = null;

        if (__knownDevices.ContainsKey(serialNumber)) {
          ret = __knownDevices[serialNumber];
        }

        if (ret == null) {
          if (__foundDevices.ContainsKey(serialNumber)) {
            ret = __foundDevices[serialNumber];
          }
        }

        return ret;
      }
    }
    // Overridden from IDeviceManager
    public List<IDevice> devices {
      get {
        List<IDevice> ret = new List<IDevice>();

        foreach (IDevice device in knownDevices) {
          ret.Add(device);
        }

        foreach (IDevice device in foundDevices) {
          ret.Add(device);
        }

        Log.D("AndroidDeviceManager", "Devices: " + ret.Count);

        return ret;
      }
    }
    // Overridden from IDeviceManager
    public List<IDevice> knownDevices { get { Log.D("AndroidDeviceManager", "KnownDevices: " + __knownDevices.Count); return __knownDevices.Values.ToList(); } }
    // Overridden from IDeviceManager
    public List<IDevice> foundDevices { get { Log.D("AndroidDeviceManager", "FoundDevices: " + __foundDevices.Count); return __foundDevices.Values.ToList(); } }
    // Overridden from IDeviceManager
    public EDeviceManagerState state {
      get {
        return __state;
      }
      private set {
        Log.D(this, "Setting state to: " + value);
        __state = value;
        if (onDeviceManagerStateChanged != null) {
          onDeviceManagerStateChanged(this, __state);
        } else {
          Log.D(this, this + " Cannot notify device manager state changed: event handler is dead");
        }
      }
    } EDeviceManagerState __state;

    /// <summary>
    /// The android context that the device manager is running in.
    /// </summary>
    private Context context { get; set; }
    /// <summary>
    /// The android backend bluetooth communication system.
    /// </summary>
    private BluetoothAdapter adapter { get; set; }

    /// <summary>
    /// The mapping of known devices.
    /// </summary>
    private Dictionary<ISerialNumber, IDevice> __knownDevices = new Dictionary<ISerialNumber, IDevice>();
    /// <summary>
    /// The mapping of unknown devices.
    /// </summary>
    private Dictionary<ISerialNumber, IDevice> __foundDevices = new Dictionary<ISerialNumber, IDevice>();
    /// <summary>
    /// The scheduler that will allow us to post our tasks in order.
    /// </summary>
    /// <value>The scheduler.</value>
//    private LimitedConcurrentTaskScheduler scheduler { get; set; }
    /// <summary>
    /// The factory that will create all of the thats that are need for
    /// the device manager.
    /// </summary>
    /// <value>The task factory.</value>
//    private TaskFactory taskFactory { get; set; }
    /// <summary>
    /// The source of stopping an active scan prematurely.
    /// </summary>
    private CancellationTokenSource activeScanStopper { get; set; }
    /// <summary>
    /// The source of stopping a passive scan prematurely.
    /// </summary>
    private CancellationTokenSource passiveScanStopper { get; set; }

    private OnDeviceStateChanged onDeviceStateChangedDelegate;
    
    public AndroidDeviceManager(Context context) {
      this.context = context;
      adapter = BluetoothAdapter.DefaultAdapter;
//      scheduler = new LimitedConcurrentTaskScheduler(1);
//      taskFactory = new TaskFactory(scheduler);

      if (adapter.IsEnabled) {
        state = EDeviceManagerState.Idle;
      } else {
        state = EDeviceManagerState.Disabled;
      }

      onDeviceStateChangedDelegate = ((IDevice device) => {
        onDeviceStateChanged(device);
      });
    }

    // Overridden from IDeviceManager
    public Task<bool> Enable() {
      return Task.Factory.StartNew(() => {
        state = EDeviceManagerState.Enabling;

        Log.V(this, "Enabling bluetooth");
        if (!adapter.Enable()) {
          return false;
        }

        DateTime start = DateTime.Now;
        while (!adapter.IsEnabled) {
          if (DateTime.Now.Millisecond - start.Millisecond > TIMEOUT_ENABLE) {
            Log.E(this, "Failed to enable bluetooth");
            adapter.Disable();
            state = EDeviceManagerState.Disabled;
            return false;
          }
        }

        state = EDeviceManagerState.Idle;

        return true;
      });
    }

    // Overridden from IDeviceManager
    public Task<bool> DoActiveScan() {
      lock (this) {
        if (activeScanStopper != null) {
          return Task.Factory.StartNew(() => { return true; });
        } else {
          activeScanStopper = new CancellationTokenSource();
          var token = activeScanStopper.Token;

          return Task.Factory.StartNew(() => {
            try {
              token.ThrowIfCancellationRequested();

              Log.D(this, "Start DoActiveScan");
              state = EDeviceManagerState.ActiveScanning;
              DateTime timer;

              if (passiveScanStopper != null) {
                passiveScanStopper.Cancel();
                passiveScanStopper = null;
              }

              Task leScan = Task.Factory.StartNew(() => {
                DoLeScan(TIMEOUT_LE_ACTIVE_SCAN);
              });

              timer = DateTime.Now;
              while (!leScan.IsCompleted && DateTime.Now - timer < TimeSpan.FromMilliseconds(TIMEOUT_LE_ACTIVE_SCAN * 2)) {
                token.ThrowIfCancellationRequested();

                Thread.Sleep(10);
              }

              if (!leScan.IsCompleted) {
                Log.D(this, "Stop DoActiveScan: Failed to complete le scan");
                return false;
              }

              Task classicScan = Task.Factory.StartNew(() => {
                DoClassicScan();
              });

              timer = DateTime.Now;
              while (!classicScan.IsCompleted && DateTime.Now - timer < TimeSpan.FromMilliseconds(TIMEOUT_CLASSIC_ACTIVE_SCAN * 2)) {
                token.ThrowIfCancellationRequested();

                Thread.Sleep(10);
              }

              if (!classicScan.IsCompleted) {
                Log.D(this, "Stop DoActiveScan: Failed to complete classic scan");
                return false;
              }


              Log.D(this, "Stop DoActiveScan: ok");
              return true;
            } finally {
              adapter.StopLeScan(this);
              adapter.CancelDiscovery();
              activeScanStopper = null;
              state = EDeviceManagerState.Idle;
            }
          }, activeScanStopper.Token);
        }
      }
    }

    // Overridden from IDeviceManager
    public void StopActiveScan() {
      lock (this) {
        if (activeScanStopper != null) {
          activeScanStopper.Cancel();
        }
      }
    }

    // Overridden from IDeviceManager
    public Task<bool> DoPassiveScan() {
      lock (this) {
        if (passiveScanStopper != null) {
          return Task.Factory.StartNew(() => { return true; });
        } else {
          passiveScanStopper = new CancellationTokenSource();
          var token = passiveScanStopper.Token;

          return Task.Factory.StartNew(() => {
            try {
              token.ThrowIfCancellationRequested();

              Log.D(this, "Start DoPassiveScan");
              state = EDeviceManagerState.PassiveScanning;

              if (activeScanStopper != null) {
                activeScanStopper.Cancel();
                activeScanStopper = null;
              }

              Task leScan = Task.Factory.StartNew(() => {
                DoLeScan(TIMEOUT_LE_PASSIVE_SCAN);
              });
              DateTime timer = DateTime.Now;
              while (!leScan.IsCompleted && DateTime.Now - timer < TimeSpan.FromMilliseconds(TIMEOUT_LE_ACTIVE_SCAN * 2)) {
                token.ThrowIfCancellationRequested();

                Thread.Sleep(10);
              }

              if (!leScan.IsCompleted) {
                Log.D(this, "Stop DoPassiveScan: Failed to complete le scan");
                return false;
              }

              Log.D(this, "Stop DoPassiveScan: ok");

              return true;
            } finally {
              adapter.StopLeScan(this);
              adapter.CancelDiscovery();
              passiveScanStopper = null;
              state = EDeviceManagerState.Idle;
            }
          }, passiveScanStopper.Token);
        }
      }
    }

    // Overridden from IDeviceManager
    public void StopPassiveScan() {
      lock (this) {
        if (passiveScanStopper != null) {
          passiveScanStopper.Cancel();
        }
      }
    }

    // Overridden from IDeviceManager
    public void ForgetFoundDevices() {
      foreach (IDevice device in __foundDevices.Values.ToArray()) {
        UnregisterDevice(device);
      }
      // Needed?
//      state = state; // Posts a refresh event to the observers
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
      Log.D(this, "Found device: " + device.Name);
      try {
        if (!IsAppionDevice(device)) {
          Log.D(this, "Ignoring non-ION device: " + device.Name);
          return;
        }

        GaugeSerialNumber serialNumber;
        try {
          serialNumber = GaugeSerialNumber.Parse(device.Name);
        } catch (ArgumentException) {
          Log.E(this, "Invalid GaugeSerialNumber: device.Name");
          return;
        }

        IDevice ret = this[serialNumber];

        if (ret == null) {
          IConnection connection = new BLEConnection(device, context);
          DeviceFactory factory = DeviceFactory.FindFactoryFor(serialNumber);
          ret = factory.Create(this, serialNumber, connection, ProtocolUtil.BLE_PROTOCOLS[0]);
          FoundDevice(ret);
        }
      } catch (Exception e) {
        Log.E(this, "Failed to resolve newly found device", e);
      }
    }

    /// <summary>
    /// Registers the device to the found devices mapping.
    /// </summary>
    /// <param name="device"></param>
    private void FoundDevice(IDevice device) {
      device.onStateChanged += onDeviceStateChangedDelegate;
      __foundDevices.Add(device.serialNumber, device);
      onDeviceFound(this, device);
    }

    /// <summary>
    /// Registers the device to the known devices mapping.
    /// </summary>
    /// <param name="device"></param>
    private void RegisterDevice(IDevice device) {
      device.onStateChanged += onDeviceStateChangedDelegate;
      __foundDevices.Remove(device.serialNumber);
      __knownDevices.Add(device.serialNumber, device);
      onDeviceFound(this, device);
    }

    /// <summary>
    /// Fully unregisters the device from the device manager.
    /// </summary>
    /// <param name="device">Device.</param>
    private void UnregisterDevice(IDevice device) {
      device.onStateChanged -= onDeviceStateChangedDelegate;
      __foundDevices.Remove(device.serialNumber);
      __knownDevices.Remove(device.serialNumber);
    }

    /// <summary>
    /// Attempts to deduce whether or not the given bluetooth device is a valid
    /// ION device.
    /// </summary>
    /// <param name="device"></param>
    /// <returns></returns>
    private bool IsAppionDevice(BluetoothDevice device) {
      // TODO ahodder@appioninc.com: This must be updated per supported device type.
      return GaugeSerialNumber.IsValid(device.Name);
    }

    /// <summary>
    /// A blocking bluetooth classic scan call. This procedure will perform a
    /// classic scan that lasts as long as android deems necessary.
    /// </summary>
    /// <returns></returns>
    private void DoClassicScan() {
      // TODO ahodder@appioninc.com: Determine whether or not we are actually classic scanning.
      Log.V(this, "Starting classic scan");
      if (!adapter.StartDiscovery()) {
        throw new Exception("Failed to start classic discovery");
      }

      while (adapter.IsDiscovering) {
        Thread.Sleep(50);
      }

      adapter.CancelDiscovery();

      Thread.Sleep(250);
    }

    /// <summary>
    /// A blocking bluetooth le scan call. This procedure will perform a BLE
    /// scan operation for the given duration.
    /// </summary>
    /// <param name="durationMillis"></param>
    /// <returns></returns>
    private void DoLeScan(long durationMillis) {
      try {
        // TODO ahodder@appioninc.com: Determine whether or not we are actually le scanning.
        Log.V(this, "Starting LE scan");
        if (!adapter.StartLeScan(this)) {
          throw new Exception("Failed to start LE scan");
        }

        Thread.Sleep(TimeSpan.FromMilliseconds(durationMillis));
      } finally {
        adapter.StopLeScan(this);
      }
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
        onDeviceStateChanged(device);
      } else {
        Log.D(this, "Cannot notify device state change: event handler is dead");
      }
    }
  }
}