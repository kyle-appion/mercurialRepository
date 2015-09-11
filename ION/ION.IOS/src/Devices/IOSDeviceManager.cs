using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using CoreBluetooth;
using CoreFoundation;
using Foundation;

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
    /// The scan mode that will delegate out how the device manager performs a
    /// device scan.
    /// </summary>
    /// <value>The scan mode.</value>
    private IScanMode scanMode { get; set; }
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
    }

    // Overridden from IDeviceManager
    public async Task InitAsync() {
      centralManager = new CBCentralManager(DispatchQueue.CurrentQueue);
      centralManager.Init();

      scanMode = new LeScanMode(centralManager);

      onDeviceStateChangedDelegate = ((IDevice device) => {
        NotifyDeviceStateChanged(device);
      });

      if (CBCentralManagerState.PoweredOn == centralManager.State) {
        state = EDeviceManagerState.Idle;
      } else {
        state = EDeviceManagerState.Disabled;
      }

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

      centralManager.UpdatedState += OnCentralManagerStateUpdated;
      centralManager.DiscoveredPeripheral += OnCentralManagerPeripheralDiscovered;
      scanMode.onScanStateChanged += OnScanModeStateChanged;
    }

    // Overridden from IDeviceManager
    public void Dispose() {
      foreach (IDevice device in devices) {
        device.onStateChanged -= onDeviceStateChangedDelegate;
      }

      centralManager.UpdatedState -= OnCentralManagerStateUpdated;
      centralManager.DiscoveredPeripheral -= OnCentralManagerPeripheralDiscovered;
      scanMode.onScanStateChanged -= OnScanModeStateChanged;
    }

    // Overridden from IDeviceManager
    public Task<bool> EnableAsync() {
      return Task.Run(() => {
        Log.E(this, "Cannot enable hardware: not implemented");
        return false;
      });
    }

    // Overridden from IDeviceManager
    public Task<bool> DoActiveScanAsync() {
      if (scanMode != null) {
        scanMode.Scan(TimeSpan.FromMilliseconds(TIMEOUT_ACTIVE_SCAN));
      }
      return (Task<bool>)null;
    }

    // Overridden from IDeviceManager
    public void StopActiveScan() {
      if (scanMode != null) {
        scanMode.Stop();
      }
      /*
      StopScan();
      */
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
      var p = Protocol.FindProtocolFromVersion(protocol);
      if (p == null) {
        Log.D(this, "Found deprecated device with out broadcasting. Asserting protocol 1");
        p = Protocol.FindProtocolFromVersion(1);
      }
      return factory.Create(this, serialNumber, connection, p);
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
      ion.PostToMain(() => {
        if (onDeviceStateChanged != null) {
          onDeviceStateChanged(device);
        } else {
          Log.D(this, "Cannot notify device state change: event handler is dead");
        }
      });
    }

    /// <summary>
    /// Notifies the on device found event handler that a new device was found.
    /// </summary>
    /// <param name="device">Device.</param>
    private void NotifyDeviceFound(IDevice device) {
      ion.PostToMain(() => {
        if (onDeviceFound != null) {
          onDeviceFound(this, device);
        } else {
          Log.D(this, "Cannot notify device found: event handler is dead");
        }
      });
    }

    /// <summary>
    /// Notifies the on device manager state changed handler that the device manager's
    /// state has changed.
    /// </summary>
    private void NotifyDeviceManagerStateChanged() {
      // Post the update message to the main thread.
      ion.PostToMain(() => {
        if (onDeviceManagerStateChanged != null) {
          onDeviceManagerStateChanged(this, state);
        } else {
          Log.D(this, "Cannot notify device manager state changed: event handler is dead");
        }
      });
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

    /// <summary>
    /// Called when the central manager's state changes.
    /// </summary>
    /// <param name="obj">Object.</param>
    /// <param name="args">Arguments.</param>
    private void OnCentralManagerStateUpdated(object obj, EventArgs args) {
      switch (centralManager.State) {
        case CBCentralManagerState.PoweredOn:
          state = EDeviceManagerState.Idle;
          break;
        case CBCentralManagerState.Resetting:
          state = EDeviceManagerState.Enabling;
          break;
        case CBCentralManagerState.PoweredOff: // Fallthrough
        case CBCentralManagerState.Unauthorized: // Fallthrough
        case CBCentralManagerState.Unsupported: // Fallthrough
        case CBCentralManagerState.Unknown: // Fallthrough
        default:
          Log.D(this, "Unknown CBCenteralManagerState: " + centralManager.State);
          break;
      }
    }

    /// <summary>
    /// Called when the central manager discovers a new peripheral.
    /// </summary>
    /// <param name="obj">Object.</param>
    /// <param name="args">Arguments.</param>
    private void OnCentralManagerPeripheralDiscovered(object obj, CBDiscoveredPeripheralEventArgs args) {
      Log.D(this, "Discovered Peripheral");
      string name = args.Peripheral.Name;
      if (name == null) {
        if (args.AdvertisementData != null) {
          var data = args.AdvertisementData[CBAdvertisement.DataLocalNameKey] as NSString;
          if (data != null) {
            name = data.ToString();
          }
        }
      }
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

        var v = args.AdvertisementData[CBAdvertisement.DataManufacturerDataKey];
        byte[] scanRecord = new byte[20];
        if (v != null) {
          scanRecord = ((NSData)v).ToArray();
          Log.D(this, name + " scanRecord before: " + String.Join(", ", scanRecord));
          var bytes = new byte[20];
          Array.Copy(scanRecord, 2, bytes, 0, Math.Min(scanRecord.Length - 2, bytes.Length));
          scanRecord = bytes;
        }
        Log.D(this, name + " scanRecord after: " + String.Join(", ", scanRecord));

        IDevice ret = this[serialNumber];

        if (ret == null) {
          ret = CreateDevice(serialNumber, args.Peripheral.Identifier.AsString(), scanRecord[0]);
          FoundDevice(ret);
        }

        if (ret.protocol.supportsBroadcasting) {
          ret.HandlePacket(scanRecord);            
        }
      } catch (Exception e) {
        Log.E(this, "Failed to resolve newly found device", e);
      }

      Log.D(this, "Device discovered");
    }

    /// <summary>
    /// Called when the device manager's scan mode changes scan state.
    /// </summary>
    /// <param name="scanMode">Scan mode.</param>
    /// <param name="scanning">If set to <c>true</c> scanning.</param>
    public void OnScanModeStateChanged(IScanMode scanMode, bool scanning) {
      if (scanning) {
        state = EDeviceManagerState.ActiveScanning;
      } else {
        state = EDeviceManagerState.Idle;
      }
    }
  } // End IOSDeviceManager
}

