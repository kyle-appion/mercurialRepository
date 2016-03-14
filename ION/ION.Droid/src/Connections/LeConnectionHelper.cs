namespace ION.Droid.Connections {

  using System;
  using System.Collections.Generic;
  using System.Threading;
  using System.Threading.Tasks;

  using Android.Bluetooth;
  using Android.Bluetooth.LE;
  using Android.Content;
  using Android.OS;

  using ION.Core.Connections;
  using ION.Core.Devices;
  using ION.Core.Devices.Connections;
  using ION.Core.Devices.Protocols;
  using ION.Core.Util;

  using ION.Droid.App;
  using ION.Droid.Connections;

  public class LeConnectionHelper : BaseConnectionHelper {
    // Overridden from BaseConnectionHelper
    public override bool isEnabled {
      get {
        return adapter.IsEnabled;
      }
    }

    private Context context;

    /// <summary>
    /// The bluetooth manager that holds the adapter.
    /// </summary>
    /// <value>The manager.</value>
    private BluetoothManager manager { get; set; }
    /// <summary>
    /// The native bluetooth adapter.
    /// </summary>
    /// <value>The adapter.</value>
    private BluetoothAdapter adapter { get; set; }

    /// <summary>
    /// The delegate that will perform the scan actions for the connection helper.
    /// </summary>
    /// <value>The scan delegate.</value>
    private IScanDelegate scanDelegate { get; set; }

    /// <summary>
    /// The dictionary of le connections.
    /// </summary>
    private Dictionary<string, LeConnection> __leConnections = new Dictionary<string, LeConnection>();

    public LeConnectionHelper(Context context, BluetoothManager manager) {
      this.context = context;
      this.manager = manager;
      this.adapter = manager.Adapter;
      if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop) {
        scanDelegate = new Api21ScanDelegate(adapter);
      } else if (Build.VERSION.SdkInt >= BuildVersionCodes.JellyBean) {
        scanDelegate = new Api18ScanDelegate(adapter);
      } else {
        // TODO ahodder@appioninc.com: Catch and display user message
        // No good, the user's device cannot support le connections.
        throw new Exception("Cannot create AndroidLeConnectionHelper: device version too old");
      }
    }

    // Overridden from BaseConnectionHelper
    protected async override Task OnScan(TimeSpan scanTime, CancellationToken token) {
      scanDelegate.Start(OnDeviceFound);
      await Task.Delay(scanTime);
    }

    // Overridden from BaseConnectionHelper
    protected override void OnStop() {
      scanDelegate.Stop();
    }

    // Overridden from BaseConnectionHelper
    public override Task<bool> Enable() {
      return Task.FromResult(adapter.Enable());
    }

    // Overridden from BaseConnectionHelper
    public override IConnection CreateConnectionFor(string address, EProtocolVersion protocolVersion) {
      var device = adapter.GetRemoteDevice(address);

      if (device == null) {
        throw new ArgumentException("Create connection for " + address + " failed: no device");
      } else if (BluetoothDeviceType.Le == device.Type) {
        var ret = new LeConnection(context, manager, device);
        __leConnections[address] = ret;
        return ret;
//        throw new ArgumentException("Create connection for " + address + " failed: device not le");
      } else {
        // TODO ahodder@appioninc.com: This is a test and should be removed
        var ret = new LeConnection(context, manager, device);
        __leConnections[address] = ret;
        return ret;
//        throw new ArgumentException("Create connection for " + address + " failed: can't handle device type: " + device.Type);
      }
    }

    /// <summary>
    /// Queries whether or not the connection helper can resolve the given protocol.
    /// </summary>
    /// <returns>true</returns>
    /// <c>false</c>
    /// <param name="protocol">Protocol.</param>
    public override bool CanResolveProtocol(EProtocolVersion protocol) {
      switch (protocol) {
        case EProtocolVersion.V1:
          return true;
        case EProtocolVersion.V2:
          return true;
        case EProtocolVersion.V3:
          return true;
        default:
          return false;
      }
    }

    /// <summary>
    /// Called when the scan delegate discovers a new device.
    /// </summary>
    /// <param name="device">Device.</param>
    /// <param name="scanRecord">Scan record.</param>
    private void OnDeviceFound(BluetoothDevice device, byte[] scanRecord) {
      try {
        if (!IsAppionDevice(device)) {
          return; // Break as we don't want to deal with none Appion devices
        }

        var serialNumber = GaugeSerialNumber.Parse(device.Name);
        int pv = (int)scanRecord?[0];
        var protocol = EProtocolVersion.V1;
        if (Enum.IsDefined(typeof(EProtocolVersion), pv)) {
          protocol = (EProtocolVersion)pv;
        }

        NotifyDeviceFound(serialNumber, device.Address, scanRecord, protocol);
      } catch (Exception e) {
        Log.E(this, "Failed to resolve found device " + device.Name, e);
      }
    }

    /// <summary>
    /// Queries whether or not the given device is a valid Appion device.
    /// </summary>
    /// <returns><c>true</c> if this instance is appion device the specified device; otherwise, <c>false</c>.</returns>
    /// <param name="device">Device.</param>
    private bool IsAppionDevice(BluetoothDevice device) {
      return GaugeSerialNumber.IsValid(device.Name);
    }
  }

  /// <summary>
  /// The delegate that is called when a device is found by the scan delegate.
  /// </summary>
  internal delegate void OnDeviceFound(BluetoothDevice device, byte[] scanRecord);

  /// <summary>
  /// A simple interface that will delegate the execution of le scans.
  /// </summary>
  internal interface IScanDelegate {
    void Start(OnDeviceFound onDeviceFound);
    void Stop();
  }

  /// <summary>
  /// The delegate that will resolve Apis 18-20 LeScanning.
  /// </summary>
  internal class Api18ScanDelegate : Java.Lang.Object, IScanDelegate, BluetoothAdapter.ILeScanCallback {
    private BluetoothAdapter adapter { get; set; }
    private OnDeviceFound onDeviceFound { get; set; }

    public Api18ScanDelegate(BluetoothAdapter adapter) {
      this.adapter = adapter;
    }

    // Overridden from ScanDelegate
    public void Start(OnDeviceFound onDeviceFound) {
      Log.D(this, "Starting scan");
      this.onDeviceFound = onDeviceFound;
      adapter.StartLeScan(this);
    }

    // Overridden from ScanDelegate
    public void Stop() {
      Log.D(this, "Stopping scan");
      onDeviceFound = null;
      adapter.StopLeScan(this);
    }

    // Overridden from BluetoothAdapter.ILeScanCallback
    public void OnLeScan(BluetoothDevice device, int rssi, byte[] scanRecord) {
      Log.D(this, "Found device");
      var payload = new byte[20];
      Array.Copy(scanRecord, 13, payload, 0, payload.Length);

      if (onDeviceFound != null) {
        onDeviceFound(device, payload);
      }
    }
  }

  /// <summary>
  /// The delegate that will resolve the Le Scanning for all devices at and above api level 21.
  /// </summary>
  internal class Api21ScanDelegate : ScanCallback, IScanDelegate {
    private BluetoothAdapter adapter { get; set; }
    private BluetoothLeScanner scanner { get; set; }
    private OnDeviceFound onDeviceFound { get; set; }

    public Api21ScanDelegate(BluetoothAdapter adapter) {
      this.adapter = adapter;
      this.scanner = adapter.BluetoothLeScanner;
    }

    // Overridden from ScanDelegate
    public void Start(OnDeviceFound onDeviceFound) {
      Log.D(this, "Starting scan");
      this.onDeviceFound = onDeviceFound;
      scanner.StartScan(this);
    }

    // Overridden from ScanDelegate
    public void Stop() {
      Log.D(this, "Stopping scan");
      scanner.StopScan(this);
    }

    // Overridden from ScanCallback
    public override void OnBatchScanResults(System.Collections.Generic.IList<ScanResult> results) {
      base.OnBatchScanResults(results);
    }

    // Overridden from ScanCallback
    public override void OnScanFailed(ScanFailure errorCode) {
      base.OnScanFailed(errorCode);
      Log.D(this, "Scan failed");
    }

    // Overridden from ScanCallback
    public override void OnScanResult(ScanCallbackType callbackType, ScanResult result) {
      Log.D(this, "Found device");

      if (onDeviceFound != null) {
        onDeviceFound(result.Device, result.ScanRecord.GetBytes());
      }
    }
  }
}

