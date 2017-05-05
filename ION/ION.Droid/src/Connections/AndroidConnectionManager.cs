namespace ION.Droid.Connections {

  using System;
  using System.Collections.Generic;
  using System.Threading;
  using System.Threading.Tasks;

  using Android.Bluetooth;
  using Android.Content;
  using Android.Content.PM;
  using Android.OS;
  using Android.Support.V4.Content;

  using Appion.Commons.Util;

  using ION.Core.Connections;
  using ION.Core.Devices;
  using ION.Core.Devices.Protocols;

  using ION.Droid.App;

  public class AndroidConnectionManager : Java.Lang.Object, IConnectionManager, ISharedPreferencesOnSharedPreferenceChangeListener {

    internal static TimeSpan SCAN_TIME = TimeSpan.FromMilliseconds(1500);
    internal static TimeSpan DOWN_TIME = TimeSpan.FromMilliseconds(3500);

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
    // Implemented for IConnectionManager
    public bool isBroadcastScanning { get { return broadcastTask != null; } }
    /// <summary>
    /// Queries whether or not the connection manager is performing a classic scan.
    /// </summary>
    /// <value><c>true</c> if is classic scanning; otherwise, <c>false</c>.</value>
    public bool isClassicScanning { get { return classicScanMethod.isScanning; } }

    /// <summary>
    /// The ion instance that create this manager.
    /// </summary>
    /// <value>The ion.</value>
    public BaseAndroidION ion { get; private set; }
    /// <summary>
    /// The application context.
    /// </summary>
    public  Context context { get { return ion.context; } }
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
    /// <summary>
    /// The token source that will cancel the broadcast task.
    /// </summary>
    private CancellationTokenSource broadcastTokenSource;
    /// <summary>
    /// The task that references a running broadcast action.
    /// </summary>
    private Task broadcastTask;

    public AndroidConnectionManager(BaseAndroidION ion) {
      this.ion = ion;
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

      ion.appPrefs.prefs.RegisterOnSharedPreferenceChangeListener(this);
    }

    // Implemented for IConnectionManager
    public void Dispose() {
      if (broadcastTask != null) {
        broadcastTokenSource.Cancel();
        broadcastTask.Wait();
      }

      broadcastTokenSource = null;
      broadcastTask = null;

      ion.appPrefs.prefs.UnregisterOnSharedPreferenceChangeListener(this);
      StopScan();
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
    public bool StartBroadcastScan() {
      lock (locker) {
        if (isBroadcastScanning) {
          return true;
        }

        if (isScanning) {
          StopScan();
        }

        handler.PostDelayed(() => {
          broadcastTokenSource = new CancellationTokenSource();
          broadcastTask = StartBroadcastTask(broadcastTokenSource, SCAN_TIME, DOWN_TIME);
        }, 1000);
        return true;
      }
    }

    // Implemented for IConnectionManager
    public void StopScan() {
      lock (locker) {
        if (broadcastTokenSource != null) {
          broadcastTokenSource.Cancel();
        }

        Monitor.PulseAll(locker);
        handler.RemoveCallbacksAndMessages(null);
        bleScanMethod.StopScan();
        classicScanMethod.StopScan();

        if (broadcastTask != null) {
          broadcastTask.Wait();
          broadcastTask = null;
          broadcastTokenSource.Dispose();
          broadcastTokenSource = null;
        }

        isScanning = false;
      }
    }

    // Implemented for OnSharedPreferenceChangeListener
    public void OnSharedPreferenceChanged(ISharedPreferences prefs, string key) {
      if (context.GetString(Resource.String.pkey_device_long_range).Equals(key)) {
        if (ion.preferences.device.allowLongRangeMode) {
          StartBroadcastScan();
        } else {
          StopScan();
        }
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
        return;
      }

      // Note: gauges that have a scan record come with an interesting issue. There are two radios on those devices. As
      // such, the radio that would actually provide a scan record is NOT the radio that accepts connections. So, we
      // must filter out the connections that are providing scan records and only pass empty scan record connections to
      // device manager.

      if (scanRecord == null) {
        // Notify the device manager a newly found device.
        var pv = ResolveProtocolVersion(device, sn, scanRecord);

        if (onDeviceFound != null) {
          onDeviceFound(this, sn, device.Address, scanRecord, pv);
        }
      } else {
        // We need to simply get the ACTUAL connection and pass it the scan record.
        var idevice = ion.deviceManager[sn];
        if (idevice == null) {
          // The device manager does not know about this device. This is actually an issue as we can't create the device
          // with the current bluetooth device that we were given. So we must ignore the device until we can get in
          // enough in range to discover the actual connectable bluetoothdevice.
          Log.E(this, "Received a broadcast packet for an unknown device. Ignoring device");
        } else {
          // The application is aware of the device, this is great. Just simply set the connection's last packet to
          // perform the broadcast update.
          idevice.connection.lastPacket = scanRecord;
        }
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
      } else if (scanRecord != null) {
        var ussn = System.Text.Encoding.UTF8.GetString(scanRecord, 0, 8);
        if (ussn.IsValidSerialNumber()) {
          sn = ussn.ParseSerialNumber();
          // We need to format the scan record.
          var buffer = new byte[20];
          Array.Copy(scanRecord, 8, buffer, 0, buffer.Length);
          Array.Clear(scanRecord, 0, scanRecord.Length);
          Array.Copy(buffer, scanRecord, buffer.Length);
          return true;
        } else {
          sn = null;
          return false;
        }
      } else {
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

    /// <summary>
    /// Toggles whether or not we are performing a scan for broadcasting.
    /// </summary>
    private Task StartBroadcastTask(CancellationTokenSource source, TimeSpan scanTime, TimeSpan downTime) {
      return Task.Factory.StartNew(async () => {
        try {
          var sleepTime = downTime;
          var lastTime = DateTime.Now;

          while (!source.Token.IsCancellationRequested) {
            if (!bleScanMethod.isScanning) {
              if (!bleScanMethod.StartScan()) {
                Log.E(this, "Failed to start ble scan for broadcasting");
              }
              await Task.Delay(scanTime, source.Token);
            } else {
              bleScanMethod.StopScan();
              await Task.Delay(downTime, source.Token);
            }
          }
        } catch (Exception e) {
          Log.E(this, "Unexpected failure in executing broadcast task.", e);
        } finally {
          StopScan();
        }
      });
    }
  }
}
