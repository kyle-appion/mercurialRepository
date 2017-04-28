namespace ION.Droid.Connections {

  using System;
  using System.Collections.Generic;

  using Android.Bluetooth;
  using Android.Content;
  using Android.Content.PM;
  using Android.OS;
  using Android.Support.V4.Content;

  using Appion.Commons.Util;

  using ION.Core.Connections;
  using ION.Core.Devices;
  using ION.Core.Devices.Protocols;

  public class AndroidConnectionManager : IConnectionManager {
    // Implemented for IConnectionManager
    public event OnScanStateChanged onScanStateChanged;
    // Implemented for IConnectionManager
    public event OnDeviceFound onDeviceFound;

    // Implemented for IConnectionManager
    public bool isEnabled { get { return bm.Adapter.IsEnabled; } }
    // Implemented for IConnectionManager
    public bool isScanning { 
      get {
        return __isScanning;
      }
      private set {
        __isScanning = value;
        if (onScanStateChanged != null) {
          onScanStateChanged(this);
        }
      }
    } bool __isScanning;
    /// <summary>
    /// Queries whether or not the connection manager is performing a classic scan.
    /// </summary>
    /// <value><c>true</c> if is classic scanning; otherwise, <c>false</c>.</value>
    public bool isClassicScanning { get { return classicScanMethod.isScanning; } }

    /// <summary>
    /// The application context.
    /// </summary>
    public  Context context { get; private set; }
    /// <summary>
    /// The android native bluetooth manager that gives us access to the bluetooth stack.
    /// </summary>
    public BluetoothManager bm { get; private set; }

    /// <summary>
    /// The object that is used to synchronize bluetooth actions.
    /// </summary>
    /// <value>The locker.</value>
    // NOTE: You made the locker return handler so that implementation didn't need to worry about what object was the lock.
    internal object locker { get { return handler; } }
    /// <summary>
    /// The handler that will post actions to the main action pump.
    /// </summary>
    private Handler handler { get; set; }

    /// <summary>
    /// The collection of every found appion device.
    /// Note: this does not necessarily need to match up with the device manager and may, indeed, contain more devices
    /// than the device manager maintains.
    /// </summary>
    private Dictionary<string, BluetoothDevice> connectionLookup = new Dictionary<string, BluetoothDevice>();
    /// <summary>
    /// The scan method used to perform ble scans.
    /// </summary>
    private IScanMethod bleScanMethod;
    /// <summary>
    /// The scan method used to perform classic scans.
    /// </summary>
    private IScanMethod classicScanMethod;

    public AndroidConnectionManager(Context context) {
      this.context = context;
      bm = context.GetSystemService(Context.BluetoothService) as BluetoothManager;

      handler = new Handler(Looper.MainLooper);

      // Resolve the scan methods that we will need to perform scanning.
      if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop) {
        if (Permission.Granted == ContextCompat.CheckSelfPermission(context, Android.Manifest.Permission.AccessFineLocation)) {
          bleScanMethod = new Api21BleScanMethod(this);
        } else {
          bleScanMethod = new Api18BleScanMethod(this);
        }
      } else {
        bleScanMethod = new Api18BleScanMethod(this);
      }

      // Create classic scan method.
      classicScanMethod = new ClassicScanMethod(this);
    }

    // Implemented for IConnectionManager
    public bool StartScan() {
      lock (locker) {
        if (bleScanMethod.StartScan()) {
          isScanning = true;
          return true;
        } else {
          return false;
        }
      }
    }

    // Implemented for IConnectionManager
    public void StopScan() {
      lock (locker) {
        bleScanMethod.StopScan();
        classicScanMethod.StopScan();
        isScanning = false;
      }
    }

    /// <summary>
    /// Starts a classic scan. If we are currently doing a ble scan, we will stop it and start a classic scan.
    /// </summary>
    /// <returns><c>true</c>, if classic scan was started, <c>false</c> otherwise.</returns>
    public bool StartClassicScan() {
      if (bleScanMethod.isScanning) {
        bleScanMethod.StopScan();
      }

      if (classicScanMethod.StartScan()) {
        isScanning = true;
        return true;
      } else {
        return false;
      }
    }


    // Implemented for IConnectionManager
    public IConnection CreateConnection(string address, EProtocolVersion protocolVersion) {
      var device = bm.Adapter.GetRemoteDevice(address);

      if (device == null) {
        var msg = "Cannot create connection: Failed to find device for address";
        Log.E(this, msg);
        throw new Exception(msg);
      }

      switch (protocolVersion) {
        case EProtocolVersion.Classic: {
          return new ClassicConnection(device);
        } // EProtocolVersion.Classic

        case EProtocolVersion.V1: {
          return new LeConnection(this, device);
        } // EProtocolVersion.V1

        case EProtocolVersion.V4: {
          return new RigadoConnection(this, device);
        } // EProtocolVersion.V4

        default: {
          throw new Exception("Cannot create connection for protocol version: " + protocolVersion);
        }
      }
    }

    /// <summary>
    /// Queries whether or not the connection manager has a connection for the given address.
    /// </summary>
    /// <returns><c>true</c>, if connection for address was hased, <c>false</c> otherwise.</returns>
    /// <param name="address">Address.</param>
    public bool HasConnectionForAddress(string address) {
      return connectionLookup.ContainsKey(address);
    }

    internal void OnDeviceFound(BluetoothDevice device, byte[] scanRecord) {
      ISerialNumber sn = null;

      if (!ResolveSerialNumber(device, scanRecord, out sn)) {
        // We could not resolve the device (or it is not ours)
        Log.V(this, "Ignoring device: " + device.Name);
        return;
      }

      var pv = ResolveProtocolVersion(device, sn, scanRecord);

      if (onDeviceFound != null) {
        onDeviceFound(this, sn, device.Address, scanRecord, pv);
      }
    }

    internal void OnClassicDeviceFound(ISerialNumber sn, BluetoothDevice device) {
      if (onDeviceFound != null) {
        onDeviceFound(this, sn, device.Address, null, EProtocolVersion.Classic);
      }
    }

    /// <summary>
    /// Attempts to resolve the serial number from the device and its scan record.
    /// </summary>
    /// <returns><c>true</c>, if serial number was resolved, <c>false</c> otherwise.</returns>
    /// <param name="device">Device.</param>
    /// <param name="scanRecord">Scan record.</param>
    /// <param name="sn">Sn.</param>
    private bool ResolveSerialNumber(BluetoothDevice device, byte[] scanRecord, out ISerialNumber sn) {
      if (device.Name.IsValidSerialNumber()) {
        sn = device.Name.ParseSerialNumber();
        return true;
      } else {
        // TODO ahodder@appioninc.com:
        sn = null;
        return false;
      }
    }

    /// <summary>
    /// Attempts to resolve the protocol version for the device.
    /// </summary>
    /// <returns>The protocol version.</returns>
    /// <param name="device">Device.</param>
    /// <param name="sn">Sn.</param>
    /// <param name="scanRecord">Scan record.</param>
    private EProtocolVersion ResolveProtocolVersion(BluetoothDevice device, ISerialNumber sn, byte[] scanRecord) {
      if (scanRecord != null && scanRecord[0] == 4) {
        return EProtocolVersion.V4;
      } else {
        // TODO ahodder@appioninc.com: We need a better way to determine a device's protocol.
        if (sn.rawSerial.Length == 8) {
          return EProtocolVersion.V4;
        } else if (device.Type == BluetoothDeviceType.Classic) {
          return EProtocolVersion.Classic;
        } else {
          return EProtocolVersion.V1;
        }
      }
    }
  }
}
