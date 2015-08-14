using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using CoreBluetooth;
using CoreFoundation;

using ION.Core.App;
using ION.Core.Connections;
using ION.Core.Devices;
using ION.Core.Devices.Protocols;
using ION.Core.Util;

using ION.IOS.Connections;

namespace ION.IOS.Devices {
  /// <summary>
  /// The iOS implementation for a device manager.
  /// </summary>
  public class IOSDeviceManager : IDeviceManager {

    private const long TIMEOUT_ACTIVE_SCAN = 3000;


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

        return ret;
      }
    }
    // Overridden from IDeviceManager
    public List<IDevice> knownDevices {
      get {
        return __knownDevices.Values.ToList();
      }
    } Dictionary<ISerialNumber, IDevice> __knownDevices = new Dictionary<ISerialNumber, IDevice>();
    // Overridden from IDeviceManager
    public List<IDevice> foundDevices {
      get {
        return __foundDevices.Values.ToList();
      }
    } Dictionary<ISerialNumber, IDevice> __foundDevices = new Dictionary<ISerialNumber, IDevice>();

    // Overridden from IDeviceManager
    public EDeviceManagerState state {
      get {
        return __state;
      }
      private set {
        Log.D(this, "Setting state to: " + value);
        __state = value;
        NotifyDeviceManagerStateChanged();
      }
    } EDeviceManagerState __state;


    /// <summary>
    /// The ION context for the device manager.
    /// </summary>
    /// <value>The ion.</value>
    private IION ion { get; set; }
    /// <summary>
    /// The iOS specific entity who is responsible for managing the bluetooth
    /// adapter for the device.
    /// </summary>
    private CBCentralManager centralManager { get; set; }
    /// <summary>
    /// The source of stopping a scan prematurely.
    /// </summary>
    private CancellationTokenSource scanStopper { get; set; }

    /// <summary>
    /// The delegate that will forward device state events.
    /// </summary>
    private OnDeviceStateChanged onDeviceStateChangedDelegate;


    public IOSDeviceManager(IION ion) {
      this.ion = ion;

      centralManager = new CBCentralManager(DispatchQueue.CurrentQueue);
      centralManager.Init();

      if (CBCentralManagerState.PoweredOn == centralManager.State) {
        state = EDeviceManagerState.Idle;
      } else {
        state = EDeviceManagerState.Disabled;
      }

      // Note ahodder@appioninc.com: We don't need to retain the delegate as the central manager will
      // die when the device manager does.
      centralManager.UpdatedState += (object obj, EventArgs args) => {
        switch (centralManager.State) {
        case CBCentralManagerState.PoweredOn:
          {
            state = EDeviceManagerState.Idle;
            break;
          }
        case CBCentralManagerState.Resetting:
          {
            state = EDeviceManagerState.Enabling;
            break;
          }
        case CBCentralManagerState.PoweredOff: // Fallthrough
        case CBCentralManagerState.Unauthorized: // Fallthrough
        case CBCentralManagerState.Unsupported: // Fallthrough
        case CBCentralManagerState.Unknown: // Fallthrough
        default:
          {
            Log.D(this, "Unknown CBCenteralManagerState: " + centralManager.State);
            break;
          }
        }
      };
      centralManager.DiscoveredPeripheral += (object obj, CBDiscoveredPeripheralEventArgs args) => {
        Log.D(this, "Discovered Peripheral");
        string name = args.Peripheral.Name;
        Log.D(this, "Found device: " + name);

        try {
          if (!IsAppionDevice(args.Peripheral)) {
            Log.D(this, "Ignoring non-ION device: " + name);
            return;
          }

          GaugeSerialNumber serialNumber;
          try {
            serialNumber = GaugeSerialNumber.Parse(name);
          } catch (ArgumentException) {
            Log.E(this, "Invalid GaugeSerialNumber: " + name);
            return;
          }

          IDevice ret = this[serialNumber];

          if (ret == null) {
            ret = CreateDevice(serialNumber, args.Peripheral.Identifier.AsString(), 0);
            FoundDevice(ret);
          }
        } catch (Exception e) {
          Log.E(this, "Failed to resolve newly found device", e);
        }

        Log.D(this, "Device discovered");
      };

      onDeviceStateChangedDelegate = ((IDevice device) => {
        NotifyDeviceStateChanged(device);
      });
    }

    // Overridden from IDeviceManager
    public void Dispose() {
      foreach (IDevice device in devices) {
        device.onStateChanged -= onDeviceStateChangedDelegate;
      }
    }

    // Overridden from IDeviceManager
    public async Task Init() {
      Log.D(this, "Querying for all known devices");
      try {
        var devices = await ion.database.deviceDao.QueryForAllAsync();
        foreach (IDevice device in devices) {
          Log.D(this, "loading device " + device);
          RegisterDevice(device);
        } 
      } catch (Exception e) {
        Log.E(this, "Failed to init", e);
      }
    }

    // Overridden from IDeviceManager
    public Task<bool> EnableAsync() {
      return Task.Run(() => {
        Log.E(this, "Cannot enable hardware: not implemented");
        return false;
      });
    }

    // Overridden from IDeviceManager
    public async Task<bool> DoActiveScanAsync() {
      if (EDeviceManagerState.Idle != state) {
        Log.D(this, "Cannot perform active scan: device manager not idle");
        return false;
      } else {
        StopScan(); // TODO ahodder@appioninc.com: Ensure this doesn't cause a deadlock

        scanStopper = new CancellationTokenSource();
        var token = scanStopper.Token;

        Log.D(this, "Start DoActiveScan");
        state = EDeviceManagerState.ActiveScanning;

        bool ret = await Task.Run(() => {
          try {
            token.ThrowIfCancellationRequested();

            Task scan = Task.Run(() => {
              DoLeScan(TIMEOUT_ACTIVE_SCAN);
            });

            DateTime timer = DateTime.Now;
            while (!scan.IsCompleted && DateTime.Now - timer < TimeSpan.FromMilliseconds(TIMEOUT_ACTIVE_SCAN * 2)) {
              token.ThrowIfCancellationRequested();

              Thread.Sleep(10);
            }

            if (!scan.IsCompleted) {
              Log.D(this, "Stop DoActiveScan: Failed to complete le scan");
              return false;
            }

            Log.D(this, "Stop DoActiveScan: ok");

            return true;
          } catch (Exception e) {
            Log.E(this, "Failed to perform active scan", e);
            return false;
          } finally {
            centralManager.StopScan();
          }
        }, scanStopper.Token);

        Log.D(this, "Stop DoActiveScan with result " + ret);
        StopScan();
        state = EDeviceManagerState.Idle;
        return ret;
      }
    }

    // Overridden from IDeviceManager
    public void StopActiveScan() {
      StopScan();
    }

    // Overridden from IDeviceManager
    public async Task<bool> DoPassiveScanAsync() {
      if (EDeviceManagerState.Idle != state) {
        Log.D(this, "Cannot perform passive scan: device manager not idle");
        return false;
      } else {
        StopScan(); // TODO ahodder@appioninc.com: Ensure this doesn't cause a deadlock

        scanStopper = new CancellationTokenSource();
        var token = scanStopper.Token;

        Log.D(this, "Start DoActiveScan");
        state = EDeviceManagerState.PassiveScanning;

        bool ret = await Task.Run(() => {
          try {
            token.ThrowIfCancellationRequested();

            Task scan = Task.Run(() => {
              DoLeScan(TIMEOUT_ACTIVE_SCAN);
            });

            DateTime timer = DateTime.Now;
            while (!scan.IsCompleted && DateTime.Now - timer < TimeSpan.FromMilliseconds(TIMEOUT_ACTIVE_SCAN * 2)) {
              token.ThrowIfCancellationRequested();

              Thread.Sleep(10);
            }

            if (!scan.IsCompleted) {
              Log.D(this, "Stop DoPassiveScan: Failed to complete le scan");
              return false;
            }

            Log.D(this, "Stop DoPassiveScan: ok");

            return true;
          } catch (Exception e) {
            Log.E(this, "Faild to perform passive scan", e);
            return false;
          } finally {
            centralManager.StopScan();
          }
        }, scanStopper.Token);


        Log.D(this, "Stop DoPassiveScanAsync with result " + ret);
        StopScan();
        state = EDeviceManagerState.Idle;
        return ret;
      }
    }

    // Overridden from IDeviceManager
    public void StopPassiveScan() {
      StopScan();
    }

    // Overridden from IDeviceManager
    public void ForgetFoundDevices() {
      foreach (IDevice device in __foundDevices.Values.ToArray()) {
        UnregisterDevice(device);
      }
    }

    // Overridden from IDeviceManager
    public IDevice CreateDevice(ISerialNumber serialNumber, string connectionIdentifier, int protocol) {
      var peripheral = centralManager.RetrievePeripheralsWithIdentifiers(new Foundation.NSUuid(connectionIdentifier))[0];
      if (peripheral == null) {
        throw new ArgumentException("Cannot create device: " + connectionIdentifier + " is not a valid connection identifier");
      }
      IConnection connection = new IosLeConnection(centralManager, peripheral);
      DeviceFactory factory = DeviceFactory.FindFactoryFor(serialNumber);
      return factory.Create(this, serialNumber, connection, ProtocolUtil.BLE_PROTOCOLS[0]);
    }

    // Overridden from IDeviceManager
    public async Task<bool> ConnectDeviceAsync(IDevice device) {
      Log.D(this, "Starting to connect device " + device.serialNumber);
      bool connected = await device.connection.Connect();
      Log.D(this, "Connection resolved with value " + connected);


      if (connected) {
        if (__foundDevices.ContainsKey(device.serialNumber)) {
          __foundDevices.Remove(device.serialNumber);
        }

        if (!__knownDevices.ContainsKey(device.serialNumber)) {
          __knownDevices[device.serialNumber] = device;
        }
      }

      NotifyDeviceStateChanged(device);
      await ion.database.deviceDao.SaveAsync(device);

      return connected;
    }

    // Overridden from IDeviceManager
    public void DisconnectDevice(IDevice device) {
      Task.Factory.StartNew(() => {
        device.connection.Disconnect();

        NotifyDeviceStateChanged(device);
      });
    }

    /// <summary>
    /// Registers the device to the found devices mapping.
    /// </summary>
    /// <param name="device"></param>
    private void FoundDevice(IDevice device) {
      device.onStateChanged += onDeviceStateChangedDelegate;
      __foundDevices.Add(device.serialNumber, device);
      NotifyDeviceFound(device);
    }

    /// <summary>
    /// Registers the device to the known devices mapping.
    /// </summary>
    /// <param name="device"></param>
    private void RegisterDevice(IDevice device) {
      device.onStateChanged += onDeviceStateChangedDelegate;
      __foundDevices.Remove(device.serialNumber);
      __knownDevices.Add(device.serialNumber, device);
      NotifyDeviceFound(device);
    }

    /// <summary>
    /// Fully unregisters the device from the device manager.
    /// </summary>
    /// <param name="device"></param>
    private void UnregisterDevice(IDevice device) {
      device.onStateChanged -= onDeviceStateChangedDelegate;
      __foundDevices.Remove(device.serialNumber);
      __knownDevices.Remove(device.serialNumber);
      NotifyDeviceStateChanged(device);
    }

    /// <summary>
    /// Attempts to deduce whether or not the given bluetooth device is a valid
    /// ION device.
    /// </summary>
    /// <param name="peripheral"></param>
    /// <returns></returns>
    private bool IsAppionDevice(CBPeripheral peripheral) {
      return GaugeSerialNumber.IsValid(peripheral.Name);
    }

    /// <summary>
    /// A blocking bluetooth le scan call. This procedure will perform a BLE scan
    /// operation for the given duration.
    /// </summary>
    /// <param name="durationMillis">Duration millis.</param>
    private void DoLeScan(long durationMillis) {
      
      try {
        PeripheralScanningOptions options = new PeripheralScanningOptions();
        options.AllowDuplicatesKey = false;

        centralManager.ScanForPeripherals((CBUUID[])null, options);

        Thread.Sleep(TimeSpan.FromMilliseconds(durationMillis));
      } catch (Exception e) {
        Log.E(this, "Failed to perform le scan", e);
      } finally {
        centralManager.StopScan();
      }
    }

    /// <summary>
    /// Notifies the on device state changed event handler that the given device has
    /// had a state change.
    /// </summary>
    /// <param name="device"></param>
    private void NotifyDeviceStateChanged(IDevice device) {
      if (onDeviceStateChanged != null) {
        onDeviceStateChanged(device);
      } else {
        Log.D(this, "Cannot notify device state change: event handler is dead");
      }
    }

    /// <summary>
    /// Notifies the on device found event handler that a new device was found.
    /// </summary>
    /// <param name="device">Device.</param>
    private void NotifyDeviceFound(IDevice device) {
      if (onDeviceFound != null) {
        onDeviceFound(this, device);
      } else {
        Log.D(this, "Cannot notify device found: event handler is dead");
      }
    }

    /// <summary>
    /// Notifies the on device manager state changed handler that the device manager's
    /// state has changed.
    /// </summary>
    private void NotifyDeviceManagerStateChanged() {
        if (onDeviceManagerStateChanged != null) {
        onDeviceManagerStateChanged(this, state);
      } else {
        Log.D(this, "Cannot notify device manager state changed: event handler is dead");
      }
    }

    /// <summary>
    /// Stops a scan.
    /// </summary>
    private void StopScan() {
      lock (this) {
        if (scanStopper != null) {
          scanStopper.Cancel();
        }
        scanStopper = null;
      }
    }
  } // End IOSDeviceManager
}

