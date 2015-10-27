using System;
using System.Threading.Tasks;

using Android.Bluetooth;
using Android.Content;

using Java.Util;

using ION.Core.Connections;
using ION.Core.Util;


namespace ION.Droid.Connections {
  public class BLEConnection : BluetoothGattCallback, IConnection {
    /// <summary>
    /// The base UUID that identifies services, charactertistics and descriptors
    /// within the BluetoothGatt API.
    /// </summary>
    private const string BASE_UUID = "0000****-0000-1000-8000-00805f9b34fb";
    /// <summary>
    /// The Rx service that will contain the characteristic needed for reading.
    /// </summary>
    private UUID READ_SERVICE = Inflate("ffe0");
    /// <summary>
    /// The Rx characteristic.
    /// </summary>
    private UUID READ_CHARACTERISTIC = Inflate("ffe4");
    /// <summary>
    /// The Tx service that will contain the characteristic needed for writing.
    /// </summary>
    private UUID WRITE_SERVICE = Inflate("ffe5");
    /// <summary>
    /// The Tx characteristic.
    /// </summary>
    private UUID WRITE_CHARACTERISITIC = Inflate("ffe9");

    /// <summary>
    /// A utility method used to create the UUIDs necessary for full use of the
    /// BluetoothGatt API.
    /// </summary>
    /// <param name="content"></param>
    /// <returns></returns>
    private static UUID Inflate(string content) {
      return UUID.FromString(BASE_UUID.Replace("****", content));
    }


    // Overridden from IConnection
    public event OnConnectionStateChanged onStateChanged;
    // Overridden from IConnection
    public event OnDataReceived onDataReceived;
    // Overridden from IConnection
    public EConnectionState connectionState {
      get { return __connectionState; }
      private set {
        __connectionState = value;
        if (onStateChanged != null) {
          onStateChanged(this, __connectionState);
        }
      }
    } EConnectionState __connectionState;
    // Overridden from IConnection
    public string name { get { return __nativeDevice.Name; } }
    // Overridden from IConnection
    public int rssi { get; private set; }
    // Overridden from IConnection
    public bool isRssiReliable { get; private set; }
    // Overridden from IConnection
    public byte[] lastPacket {
      get { return __lastPacket; }
      private set {
        __lastPacket = value;
        timeLastPacketReceived = DateTime.Now;
        if (onDataReceived != null) {
          onDataReceived(this, __lastPacket);
        }
      }
    } byte[] __lastPacket;
    // Overridden from IConnection
    public DateTime timeLastPacketReceived { get; private set; }
    // Overridden from IConnection
    public object nativeDevice {
      get {
        return __nativeDevice;
      }
    } BluetoothDevice __nativeDevice;
    // Overridden from IConnection
    public TimeSpan connectionTimeout { get; set; }

    /// <summary>
    /// The android context necessary for creating BLE connections.
    /// </summary>
    private Context __context { get; set; }
    /// <summary>
    /// The gatt object that is used to manage the native connection state.
    /// </summary>
    private BluetoothGatt __gatt { get; set; }
    /// <summary>
    /// The characteristic that the connection will read incoming data from.
    /// </summary>
    private BluetoothGattCharacteristic __read { get; set; }
    /// <summary>
    /// The characteristic that the connection will write outgoing data to.
    /// </summary>
    private BluetoothGattCharacteristic __write { get; set; }

    public BLEConnection(BluetoothDevice device, Context context) {
      __nativeDevice = device;
      __context = context;
      connectionTimeout = TimeSpan.FromMilliseconds(45 * 1000);
    }

    // Overridden from IConnection
    public void Dispose() {
    }

    // Overridden from IConnection
    public Task<bool> Connect() {
      return Task.Factory.StartNew(() => {
        if (EConnectionState.Disconnected != connectionState) {
          return false;
        }

        DateTime start = DateTime.Now;

        __gatt = __nativeDevice.ConnectGatt(__context, false, this);

        while (EConnectionState.Connected != connectionState) {
          if (DateTime.Now - start > connectionTimeout) {
            Disconnect();
            break;
          }
        }

        return EConnectionState.Connected == connectionState;
      });
    }

    // Overridden from IConnection
    public void Disconnect() {
      if (__gatt != null) {
        __gatt.Disconnect();
        __gatt.Close();
      }

      connectionState = EConnectionState.Disconnected;
    }

    // Overridden from IConnection
    public Task<bool> Write(byte[] packet) {
      return Task.Factory.StartNew(() => {
        if (EConnectionState.Connected != connectionState) {
          return false;
        }

        __write.SetValue(packet);
        return __gatt.WriteCharacteristic(__write);
      });
    }

    // Overridden from BluetoothGattCallback
    public override void OnConnectionStateChange(BluetoothGatt gatt, GattStatus status, ProfileState newState) {
      Log.D(this, name + " has changed connection state to " + newState);

      switch (newState) {
        case ProfileState.Connecting: {
          connectionState = EConnectionState.Connecting;
          break;
        }
        case ProfileState.Connected: {
          connectionState = EConnectionState.Resolving;
          __gatt.DiscoverServices();
          break;
        }
        case ProfileState.Disconnecting:
          // Fallthrough
        case ProfileState.Disconnected: {
          connectionState = EConnectionState.Disconnected;
          break;
        }
        default: {
          // Nope
          break;
        }
      }
    }

    // Overridden from BluetoothGattCallback
    public override void OnServicesDiscovered(BluetoothGatt gatt, GattStatus status) {
      if (ValidateServices()) {
        __gatt.SetCharacteristicNotification(__read, true);
        connectionState = EConnectionState.Connected;
      } else {
        Disconnect();
      }
    }

    // Overridden from BluetoothGattCallbacks
    public override void OnCharacteristicChanged(BluetoothGatt gatt, BluetoothGattCharacteristic characteristic) {
      if (characteristic.Equals(__read)) {
        lastPacket = characteristic.GetValue();
      }
    }

    /// <summary>
    /// Checks to make sure that the connection has valid services and
    /// characteristics. If this method fails, then the connection cannot
    /// possibly support a stable connection: all communication will fail or
    /// not occur.
    /// </summary>
    /// <returns></returns>
    private bool ValidateServices() {
      __read = null;
      __write = null;

      BluetoothGattService readService = __gatt.GetService(READ_SERVICE);
      if (readService != null) {
        __read = readService.GetCharacteristic(READ_CHARACTERISTIC);
      }

      BluetoothGattService writeService = __gatt.GetService(WRITE_SERVICE);
      if (writeService != null) {
        __write = writeService.GetCharacteristic(WRITE_CHARACTERISITIC);
      }

      return __read != null && __write != null;
    }
  }
}