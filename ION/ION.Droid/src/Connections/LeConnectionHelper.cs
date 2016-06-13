namespace ION.Droid.Connections {

  using System;
  using System.Collections.Generic;
  using System.Text;
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
    /// <summary>
    /// The device name of all rigado devices.
    /// </summary>
    private const string RIGDFU = "RigCom";

    // Overridden from BaseConnectionHelper
    public override bool isEnabled {
      get {
        return adapter.IsEnabled;
      }
    }

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

    public LeConnectionHelper(BluetoothManager manager) {
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
      Log.D(this, "LeScan started");
      scanDelegate.Start(OnDeviceFound);
      await Task.Delay(scanTime);
    }

    // Overridden from BaseConnectionHelper
    protected override void OnStop() {
      Log.D(this, "LeScan stopped");
      scanDelegate.Stop();
    }

    // Overridden from BaseConnectionHelper
    public override Task<bool> Enable() {
      return Task.FromResult(adapter.Enable());
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
          Log.D(this, "Ignoring device: " + device.Name);
          return; // Break as we don't want to deal with none Appion devices
        }

        ISerialNumber serialNumber = GaugeSerialNumber.Parse(device.Name);
        byte[] broadcastPacket = null;

        // Parse the scan record.
        var i = 0;
        while (i < scanRecord.Length) {
          var len = scanRecord[i++];
          var type = scanRecord[i++];
          if (type == 0xff) {
            var companyCode = (int)((scanRecord[i] << 8) & scanRecord[i + 1]);
            if (RIGDFU.Equals(device.Name)) {
              Log.D(this, "Resolving modern rigado BLE device: " + device.Name);
              var chars = Encoding.UTF8.GetString(scanRecord, 0, 8);
              serialNumber = SerialNumberExtensions.ParseSerialNumber(chars);
              broadcastPacket = new byte[19];
              Array.Copy(scanRecord, 9, broadcastPacket, 0, 19);
            } else {
              companyCode = (int)((scanRecord[i] << 8) & scanRecord[i + 1]);
              Log.D(this, "Resolving legacy BLE device: " + device.Name);
              serialNumber = GaugeSerialNumber.Parse(device.Name);
              broadcastPacket = new byte[19];
              Array.Copy(scanRecord, 2, broadcastPacket, 0, 19);
            }
          }
          Log.D(this, "Found advertisement record " + type.ToString("x2") + " with a length of " + len);
          i += len - 2; // We already incremented for the len and type
        }

        EProtocolVersion protocol = EProtocolVersion.V1;

        if (broadcastPacket != null) {
          int pv = (int)broadcastPacket[0];
          if (Enum.IsDefined(typeof(EProtocolVersion), pv)) {
            protocol = (EProtocolVersion)pv;
          }  
        } else if (serialNumber.rawSerial.StartsWith("S")) {
          protocol = EProtocolVersion.V4;
        }

        if (broadcastPacket != null) {
          Log.D(this, "Found device: " + serialNumber + " with the broadcast packet of: " + broadcastPacket.ToByteString());
        } else {
          Log.D(this, "Found device: " + serialNumber + " with a scan record of: " + scanRecord.ToByteString());
        }

        NotifyDeviceFound(serialNumber, device.Address, broadcastPacket, protocol);
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
      return RIGDFU.Equals(device.Name) || GaugeSerialNumber.IsValid(device.Name);
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
      if (onDeviceFound != null) {
        onDeviceFound(device, scanRecord);
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
      if (onDeviceFound != null) {
        onDeviceFound(result.Device, result.ScanRecord.GetBytes());
      }
    }
  }
}

