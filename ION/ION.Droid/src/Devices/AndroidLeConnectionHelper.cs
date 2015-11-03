namespace ION.Droid.Devices {

  using System;
  using System.Collections.Generic;

  using Android.Bluetooth;
  using Android.Bluetooth.LE;
  using Android.OS;

  using ION.Core.Connections;
  using ION.Core.Devices;
  using ION.Core.Devices.Connections;
  using ION.Core.Util;

  using ION.Droid.App;
  using ION.Droid.Connections;

  public class AndroidLeConnectionHelper : BaseConnectionHelper {
    // Overridden from BaseConnectionHelper
    public override bool isEnabled {
      get {
        return adapter.IsEnabled;
      }
    }

    private AndroidION ion { get; set; }
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

    public AndroidLeConnectionHelper(AndroidION ion, BluetoothAdapter adapter) {
      this.ion = ion;
      this.adapter = adapter;

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
    protected override void DoStartScan() {
      scanDelegate.Start(OnDeviceFound);
    }

    // Overridden from BaseConnectionHelper
    protected override void DoStopScan() {
      scanDelegate.Stop();
    }

    // Overridden from BaseConnectionHelper
    protected override void OnDispose() {
      base.OnDispose();

      scanDelegate.Stop();
    }

    // Overridden from BaseConnectionHelper
    public override bool Enable() {
      return adapter.Enable();
    }

    // Overridden from BaseConnectionHelper
    public override IConnection CreateConnectionFor(string address) {
      var device = adapter.GetRemoteDevice(address);

      if (device == null) {
        throw new ArgumentException("Create connection for " + address + " failed: no device");
      } else if (BluetoothDeviceType.Le == device.Type) {
        throw new ArgumentException("Create connection for " + address + " failed: device not le");
      }

      var ret = new LeConnection(ion.context, device);
      __leConnections[address] = ret;
      return ret;
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
        int protocol = (int)scanRecord?[0];

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
      this.onDeviceFound = onDeviceFound;
      adapter.StartLeScan(this);
    }

    // Overridden from ScanDelegate
    public void Stop() {
      onDeviceFound = null;
      adapter.StopLeScan(this);
    }

    // Overridden from BluetoothAdapter.ILeScanCallback
    public void OnLeScan(BluetoothDevice device, int rssi, byte[] scanRecord) {
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
      this.onDeviceFound = onDeviceFound;
      scanner.StartScan(this);
    }

    // Overridden from ScanDelegate
    public void Stop() {
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
      if (onDeviceFound != null) {
        onDeviceFound(result.Device, result.ScanRecord.GetBytes());
      }
    }
  }
}

