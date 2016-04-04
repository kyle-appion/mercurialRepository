namespace ION.Core.Connections {

  using System;
  using System.Collections.Generic;
  using System.Threading.Tasks;

  using Android.Bluetooth;
  using Android.Content;
  using Android.OS;

  using ION.Core.Connections;
  using ION.Core.Devices;
  using ION.Core.Devices.Connections;
  using ION.Core.Devices.Protocols;
  using ION.Core.Util;

  using ION.Droid.App;
  using ION.Droid.Connections;


  public class ClassicConnectionHelper : IConnectionHelper {
    /// <summary>
    /// The event pool that is notified when the connection helper state changes.
    /// </summary>
    public event OnScanStateChanged onScanStateChanged;
    /// <summary>
    /// The event pool that is nofied when the connection helper discovers a device.
    /// </summary>
    public event ION.Core.Devices.Connections.OnDeviceFound onDeviceFound;

    /// <summary>
    /// The device name that signifies the device as an appion device.
    /// </summary>
    private const string APPION_GAUGE = "APPION Gauge";

    /// <summary>
    /// Whether or not the connection helper's backend is enabled.
    /// </summary>
    /// <value>true</value>
    /// <c>false</c>
    public bool isEnabled {
      get {
        return adapter.IsEnabled;
      }
    }
    /// <summary>
    /// Whether or not the connection helper is currently scanning.
    /// </summary>
    /// <value><c>true</c> if is scanning; otherwise, <c>false</c>.</value>
    public bool isScanning {
      get {
        return __isScanning;
      }
      set {
        __isScanning = value;
        NotifyScanStateChanged();
      }
    } bool __isScanning;

    /// <summary>
    /// The android context.
    /// </summary>
    /// <value>The context.</value>
    private Context context;
    /// <summary>
    /// The bluetooth manager that holds the adapter.
    /// </summary>
    /// <value>The manager.</value>
    private BluetoothManager manager;
    /// <summary>
    /// The native bluetooth adapter.
    /// </summary>
    /// <value>The adapter.</value>
    private BluetoothAdapter adapter;
    /// <summary>
    /// The receiver that will listen for bluetooth events.
    /// </summary>
    private ClassicReceiver receiver;

    /// <summary>
    /// The dictionary of classic connections.
    /// </summary>
    private Dictionary<string, ClassicConnection> __connections = new Dictionary<string, ClassicConnection>();

    public ClassicConnectionHelper(Context context, BluetoothManager manager) {
      this.context = context;
      this.manager = manager;
      this.adapter = manager.Adapter;
    }

    /// <summary>
    /// Determines whether this instance can resolve protocol the specified protocolVersion.
    /// </summary>
    /// <returns><c>true</c> if this instance can resolve protocol the specified protocolVersion; otherwise, <c>false</c>.</returns>
    /// <param name="protocolVersion">Protocol version.</param>
    public bool CanResolveProtocol(EProtocolVersion protocolVersion) {
      return EProtocolVersion.Classic == protocolVersion;
    }

    /// <summary>
    /// Enables the connection helper's backend.
    /// </summary>
    public async Task<bool> Enable() {
      if (!isEnabled) {
        adapter.Enable();

        var start = DateTime.Now;

        while (DateTime.Now - start < TimeSpan.FromMilliseconds(15000)) {
          await Task.Delay(100);
        }
      }

      return isEnabled;
    }

    /// <summary>
    /// Platform code for starting a scan.
    /// </summary>
    /// <returns>The scan async.</returns>
    public async Task<bool> Scan(TimeSpan scanTime) {
      var filter = new IntentFilter();
      filter.AddAction(BluetoothAdapter.ActionDiscoveryFinished);
      filter.AddAction(BluetoothDevice.ActionFound);
      context.RegisterReceiver(receiver = new ClassicReceiver(OnBroadcast), filter);
      isScanning = true;
      if (!adapter.StartDiscovery()) {
        return false;
      }

      await Task.Delay(500);

      while (adapter.IsDiscovering) {
        await Task.Delay(500);
      }

      return true;
    }
    /// <summary>
    /// Platform code for stopping a scan.
    /// </summary>
    public void Stop() {
      try {
        if (isScanning) {
          context.UnregisterReceiver(receiver);
        }
      } catch (Exception e) {
        Log.E(this, "We are unregistering the classic receiver too many times. Please stawp.", e);
      }
      isScanning = false;
    }

    /// <summary>
    /// Releases all resource used by the <see cref="ION.Droid.Connections.ClassicConnectionHelper"/> object.
    /// </summary>
    /// <remarks>Call <see cref="Dispose"/> when you are finished using the
    /// <see cref="ION.Droid.Connections.ClassicConnectionHelper"/>. The <see cref="Dispose"/> method leaves the
    /// <see cref="ION.Droid.Connections.ClassicConnectionHelper"/> in an unusable state. After calling
    /// <see cref="Dispose"/>, you must release all references to the
    /// <see cref="ION.Droid.Connections.ClassicConnectionHelper"/> so the garbage collector can reclaim the memory that
    /// the <see cref="ION.Droid.Connections.ClassicConnectionHelper"/> was occupying.</remarks>
    public void Dispose() {
      Stop();
    }

    /// <summary>
    /// Notifies all of the OnScanStateChanged delegates that the scan mode's state changed.
    /// </summary>
    protected void NotifyScanStateChanged() {
      if (onScanStateChanged != null) {
        onScanStateChanged(this);
      }
    }

    /// <summary>
    /// Notified all of the OnDeviceFound delegate that the scan mode found a new device.
    /// </summary>
    /// <param name="device">SerialNumber.</param>
    /// <param name="packet">Packet.</param>
    /// <param name="protocol">Protocol version.</param>
    protected void NotifyDeviceFound(ISerialNumber serialNumber, string address, byte[] packet, EProtocolVersion protocolVersion) {
      if (onDeviceFound != null) {
        onDeviceFound(this, serialNumber, address, packet, protocolVersion);
      }
    }

    /// <summary>
    /// Called when the helper's broadcast receiver gets a new message.
    /// </summary>
    /// <param name="context">Context.</param>
    /// <param name="intent">Intent.</param>
    private async void OnBroadcast(Context context, Intent intent) {
      var action = intent.Action;

      switch (intent.Action) {
        case BluetoothAdapter.ActionDiscoveryFinished:
          Stop();
          break;
        case BluetoothDevice.ActionFound:
          var device = (BluetoothDevice)intent.GetParcelableExtra(BluetoothDevice.ExtraDevice);
          if (IsAppionDevice(device) && BluetoothDeviceType.Classic == device.Type) {
            if (!__connections.ContainsKey(device.Address)) {
              var connection = new ClassicConnection(device);
              __connections.Add(device.Address, connection);
              for (int i = 5; i > 0; i--) {
                try {
                  var serialNumber = await connection.RequestSerialNumber();
                  NotifyDeviceFound(serialNumber, device.Address, null, EProtocolVersion.Classic);
                  Log.D(this, "Got a valid serial number: " + serialNumber);
                  break;
                } catch (Exception e) {
                  Log.E(this, "Failed to get classic serial number", e);
                }
              }
            }
          }
          break;
      }
    }

    /// <summary>
    /// Called to check if the device is a valid appion device.
    /// </summary>
    /// <returns><c>true</c> if this instance is appion device the specified device; otherwise, <c>false</c>.</returns>
    /// <param name="device">Device.</param>
    private bool IsAppionDevice(BluetoothDevice device) {
      Log.D(this, "Checking " + device.Name);
      return device.Name == null || APPION_GAUGE.Equals(device.Name) || GaugeSerialNumber.IsValid(device.Name);
    }

    /// <summary>
    /// Creates the connection for the given address.
    /// </summary>
    /// <returns>The connection for.</returns>
    /// <param name="identifier">Address.</param>
    /// <param name="address">Address.</param>
    public IConnection CreateConnectionFor(string address, EProtocolVersion protocolVersion) {
      return new ClassicConnection(adapter.GetRemoteDevice(address));
    }
  }

  class ClassicReceiver : BroadcastReceiver {
    /// <summary>
    /// The receiver delegate.
    /// </summary>
    public delegate void OnBroadcast(Context context, Intent intent);

    private OnBroadcast onBroadcast;

    public ClassicReceiver(OnBroadcast onBroadcast) {
      this.onBroadcast = onBroadcast;
    }
    /// <Docs>The Context in which the receiver is running.</Docs>
    /// <summary>
    /// This method is called when the BroadcastReceiver is receiving an Intent
    ///  broadcast.
    /// </summary>
    /// <param name="context">Context.</param>
    /// <param name="intent">Intent.</param>
    public override void OnReceive(Context context, Intent intent) {
      onBroadcast(context, intent);
    }
  }
}

