using System;
using System.Threading.Tasks;

using CoreBluetooth;
using Foundation;

using ION.Core.Connections;

namespace ION.IOS.Connections {
  public class IOSLeConnection : IConnection {
    /// <summary>
    /// The base UUID that identifies services, charactertistics and descriptors
    /// within the BluetoothGatt API.
    /// </summary>
    private const string BASE_UUID = "0000****-0000-1000-8000-00805f9b34fb";
    /// <summary>
    /// The Rx service that will contain the characteristic needed for reading.
    /// </summary>
    private CBUUID READ_SERVICE = Inflate("ffe0");
    /// <summary>
    /// The Rx characteristic.
    /// </summary>
    private CBUUID READ_CHARACTERISTIC = Inflate("ffe4");
    /// <summary>
    /// The Tx service that will contain the characteristic needed for writing.
    /// </summary>
    private CBUUID WRITE_SERVICE = Inflate("ffe5");
    /// <summary>
    /// The Tx characteristic.
    /// </summary>
    private CBUUID WRITE_CHARACTERISITIC = Inflate("ffe9");

    /// <summary>
    /// A utility method used to create the UUIDs necessary for use of the
    /// BluetoothApi.
    /// </summary>
    /// <param name="content"></param>
    /// <returns></returns>
    private static CBUUID Inflate(string content) {
      return CBUUID.FromString(BASE_UUID.Replace("****", content));
    }

    // Overridden from IConnection
    public event OnConnectionStateChanged onStateChanged;
    // Overridden from IConnection
    public event OnDataReceived onDataReceived;
    // Overridden from IConnection
    public EConnectionState connectionState {
      get {
        return __connectionState;
      }
      set {
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
      get {
        return __lastPacket;
      }
      set {
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
    } CBPeripheral __nativeDevice;
    // Overridden from IConnection
    public TimeSpan connectionTimeout { get; set; }

    /// <summary>
    /// The central manager that is being used to host this connection.
    /// </summary>
    private CBCentralManager centeralManager { get; set; }
    /// <summary>
    /// The delegate that will receive discovered characteristic events.
    /// </summary>
    private EventHandler<CBServiceEventArgs> onCharacteristicDiscoveredDelegate;
    /// <summary>
    /// The delegate that will received updates when the connection's characteristic
    /// changes.
    /// </summary>
    private EventHandler<CBDescriptorEventArgs> onCharacteristicChangedDelegate;
    /// <summary>
    /// The characteristic that we will read the device data from.
    /// </summary>
    private CBCharacteristic read { get; set; }
    /// <summary>
    /// The characteristic that we will write output packets to.
    /// </summary>
    private CBCharacteristic write { get; set; }

    public IOSLeConnection(CBCentralManager centeralManager, CBPeripheral peripheral) {
      this.centeralManager = centeralManager;
      __nativeDevice = peripheral;

      onCharacteristicDiscoveredDelegate = (object obj, CBServiceEventArgs args) => {
        if (EConnectionState.Connecting == connectionState) {
          if (ValidateServices()) {
            connectionState = EConnectionState.Connected;
          }
        }
      };

      onCharacteristicChangedDelegate = (object obj, CBDescriptorEventArgs args) => {
        if (args.Descriptor.Characteristic.Equals(read)) { 
          lastPacket = read.Value.ToArray();
        }
      };

      __nativeDevice.DiscoveredCharacteristic += onCharacteristicDiscoveredDelegate;
      __nativeDevice.UpdatedValue += onCharacteristicChangedDelegate;

      connectionTimeout = TimeSpan.FromMilliseconds(45 * 1000);
    }

    // Overridden from IConnection
    public void Dispose() {
      __nativeDevice.DiscoveredCharacteristic -= onCharacteristicDiscoveredDelegate;
      __nativeDevice.UpdatedValue -= onCharacteristicChangedDelegate;
    }

    // Overridden from IConnection
    public Task<bool> Connect() {
      return Task.Factory.StartNew(() => {
        if (EConnectionState.Disconnected != connectionState) {
          return false;
        }

        connectionState = EConnectionState.Connecting;

        DateTime start = DateTime.Now;

        PeripheralConnectionOptions options = new PeripheralConnectionOptions();
        options.NotifyOnConnection = true;
        options.NotifyOnDisconnection = true;
        options.NotifyOnNotification = true;

        centeralManager.ConnectPeripheral(__nativeDevice, options);

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
      centeralManager.CancelPeripheralConnection(__nativeDevice);
      connectionState = EConnectionState.Disconnected;
    }

    // Overridden from IConnection
    public Task<bool> Write(byte[] packet) {
      return Task.Factory.StartNew(() => {
        if (EConnectionState.Connected != connectionState) {
          return false;
        }

        NSData data = NSData.FromArray(packet);
        __nativeDevice.WriteValue(data, write, CBCharacteristicWriteType.WithoutResponse);

        return true;
      });
    }

    /// <summary>
    /// Checks to make sure that the connection has valid serices and
    /// characteristics. If the method fails, then the connection cannot possibly
    /// support stable communication: all attempts will fail.
    /// </summary>
    /// <returns></returns>
    private bool ValidateServices() {
      read = null;
      write = null;

      foreach (CBService service in __nativeDevice.Services) {
        foreach (CBCharacteristic characteristic in service.Characteristics) {
          if (READ_CHARACTERISTIC.Equals(characteristic.UUID)) {
            read = characteristic;
          } else if (WRITE_CHARACTERISITIC.Equals(characteristic.UUID)) {
            write = characteristic;
          }
        }
      }

      return read != null && write != null;
    }
  } // End IOSLeConnection
}