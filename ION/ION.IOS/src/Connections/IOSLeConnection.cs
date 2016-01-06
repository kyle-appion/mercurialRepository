namespace ION.IOS.Connections {
  
  using System;
  using System.Threading;
  using System.Threading.Tasks;

  using CoreBluetooth;
  using Foundation;

  using ION.Core.Connections;
  using ION.Core.Util;

  public class IosLeConnection : IConnection {
    /// <summary>
    /// The base UUID that identifies services, charactertistics and descriptors
    /// within the BluetoothGatt API.
    /// </summary>
    private const string BASE_UUID = "0000****-0000-1000-8000-00805f9b34fb";
    /// <summary>
    /// The Rx characteristic.
    /// </summary>
    private static readonly ServiceCharacteristicPair READ_CHARACTERISTIC = new ServiceCharacteristicPair("ffe0", "ffe4");
    /// <summary>
    /// The Tx characteristic.
    /// </summary>
    private static readonly ServiceCharacteristicPair WRITE_CHARACTERISTIC = new ServiceCharacteristicPair("ffe5", "ffe9");
    /// <summary>
    /// The characteristic for the name.
    /// </summary>
    private static readonly ServiceCharacteristicPair NAME_CHARACTERISTIC = new ServiceCharacteristicPair("ff90", "ff91");
    /// <summary>
    /// The services to fetch on scan.
    /// </summary>
    private static readonly CBUUID[] DESIRED_SERVICES = new CBUUID[] { READ_CHARACTERISTIC.service, WRITE_CHARACTERISTIC.service, NAME_CHARACTERISTIC.service };

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
          ION.Core.App.AppState.context.PostToMain(() => {
            onStateChanged(this, __connectionState);
          });
        }
      }
    } EConnectionState __connectionState;
    // Overridden from IConnection
    // public string name { get { return __nativeDevice.Name; } }
    public string name { get; set; }
    // Overridden from IConnection
    public string address { get { return __nativeDevice.Identifier.AsString(); } }
    // Overridden from IConnection
    public ESignalStrength signalStrength {
      get {
        var r = (int)__nativeDevice.RSSI;
        // These values are largely made up.
        if (r > -35) {
          return ESignalStrength.Good;
        } else if (r > -50) {
          return ESignalStrength.Fair;
        } else if (EConnectionState.Disconnected == connectionState) {
          return ESignalStrength.None;
        } else {
          return ESignalStrength.Bad;
        }
      }
    }
    // Overridden from IConnection
    public byte[] lastPacket {
      get {
        return __lastPacket;
      }
      set {
        __lastPacket = value;
        lastSeen = DateTime.Now;
        if (onDataReceived != null) {
          onDataReceived(this, __lastPacket);
        }
      }
    } byte[] __lastPacket;
    // Overridden from IConnection
    public DateTime lastSeen { get; set; }
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
    private CBCentralManager centralManager { get; set; }
    /// <summary>
    /// The delegate that will received discovered service events.
    /// </summary>
    private EventHandler<NSErrorEventArgs> onServiceDiscoveredDelegate;
    /// <summary>
    /// The delegate that will receive discovered characteristic events.
    /// </summary>
    private EventHandler<CBServiceEventArgs> onCharacteristicDiscoveredDelegate;
    /// <summary>
    /// The delegate that will received updates when the connection's characteristic
    /// changes.
    /// </summary>
    private EventHandler<CBCharacteristicEventArgs> onCharacteristicChangedDelegate;
    /// <summary>
    /// The characteristic that we will read the device data from.
    /// </summary>
    private CBCharacteristic readCharacteristic { get; set; }
    /// <summary>
    /// The characteristic that we will write output packets to.
    /// </summary>
    private CBCharacteristic writeCharacteristic { get; set; }
    /// <summary>
    /// The characteristic that we will need to get the name of the device.
    /// </summary>
    /// <value>The name.</value>
    private CBCharacteristic nameCharacteristic { get; set; }


    /// <summary>
    /// Creates a new iOS connection wrapper.
    /// </summary>
    /// <param name="centeralManager">Centeral manager.</param>
    /// <param name="peripheral">Peripheral.</param>
    public IosLeConnection(CBCentralManager centralManager, CBPeripheral peripheral) {
      this.centralManager = centralManager;
      __nativeDevice = peripheral;
      name = __nativeDevice.Name;

      onServiceDiscoveredDelegate = ((object obj, NSErrorEventArgs args) => {
        foreach (CBService service in __nativeDevice.Services) {
          __nativeDevice.DiscoverCharacteristics(service);
        }
      });

      onCharacteristicDiscoveredDelegate = (object obj, CBServiceEventArgs args) => {
        if (EConnectionState.Connecting == connectionState) {
          if (ValidateServices()) {
            Log.D(this, name + " validated services");
            connectionState = EConnectionState.Connected;
          } else {
            Log.E(this, "Failed to resolve characteristics");
          }
        }
      };

      onCharacteristicChangedDelegate = (object obj, CBCharacteristicEventArgs args) => {
        if (args.Characteristic.Equals(nameCharacteristic)) {
          var bytes = nameCharacteristic?.Value?.ToArray();
          name = System.Text.Encoding.UTF8.GetString(bytes);
          Log.D(this, "Name characteristic read: " + name);
        } else if (args.Characteristic.Equals(readCharacteristic)) {
          lastPacket = readCharacteristic.Value.ToArray();
        } else {
          Log.D(this, "Received unknown characteristic value: " + args.Characteristic);
        }
      };

      __nativeDevice.DiscoveredService += onServiceDiscoveredDelegate;
      __nativeDevice.UpdatedCharacterteristicValue += onCharacteristicChangedDelegate;
      __nativeDevice.DiscoveredCharacteristic += onCharacteristicDiscoveredDelegate;
//      centralManager.DisconnectedPeripheral += OnPeripheralDisconnected;

      connectionState = EConnectionState.Disconnected;
      connectionTimeout = TimeSpan.FromMilliseconds(45 * 1000);
    }

    // Overridden from IConnection
    public void Dispose() {
      __nativeDevice.DiscoveredService -= onServiceDiscoveredDelegate;
      __nativeDevice.DiscoveredCharacteristic -= onCharacteristicDiscoveredDelegate;
      __nativeDevice.UpdatedCharacterteristicValue -= onCharacteristicChangedDelegate;
//      centralManager.DisconnectedPeripheral -= OnPeripheralDisconnected;
    }

    // Overridden from IConnection
    public async Task<bool> Connect() {
      if (EConnectionState.Disconnected != connectionState) {
        return false;
      }

      connectionState = EConnectionState.Connecting;

      DateTime start = DateTime.Now;

      PeripheralConnectionOptions options = new PeripheralConnectionOptions();
      options.NotifyOnConnection = true;
      options.NotifyOnDisconnection = true;
      options.NotifyOnNotification = true;

      centralManager.ConnectPeripheral(__nativeDevice, options);

      Log.D(this, "Awaiting physical connection....");
      while (CBPeripheralState.Connected != __nativeDevice.State) {
        if (DateTime.Now - start > connectionTimeout) {
          Log.D(this, "timeout: failed to connect");
          Disconnect();
          return false;
        } else {
          await Task.Delay(10);
        }
      }

      await Task.Delay(100);

      __nativeDevice.DiscoverServices(DESIRED_SERVICES);

      Log.D(this, "Awaiting service discovery");
      while (EConnectionState.Connected != connectionState) {
        if (DateTime.Now - start > connectionTimeout) {
          Log.D(this, "timeout: failed to validate services");
          Disconnect();
          return false;
        } else {
          await Task.Delay(10);
        }
      }

//      __nativeDevice.ReadValue(nameCharacteristic);
      /*
      while (nameCharacteristic.Value == null) {
        if (DateTime.Now - start > connectionTimeout) {
          Log.D(this, "timeout: failed to get device name");
          Disconnect();
          return false;
        } else {
          await Task.Delay(10);
        }
      }
      var bytes = nameCharacteristic?.Value?.ToArray();
      if (bytes != null) {
        name = System.Text.Encoding.UTF8.GetString(bytes);
      }
      Log.D(this, "name is: " + name);
      */

      __nativeDevice.SetNotifyValue(true, readCharacteristic);

      return EConnectionState.Connected == connectionState;
    }

    // Overridden from IConnection
    public void Disconnect() {
      Log.D(this, name + " disconnected");
      centralManager.CancelPeripheralConnection(__nativeDevice);
      connectionState = EConnectionState.Disconnected;
    }

    // Overridden from IConnection
    public Task<bool> Write(byte[] packet) {
      return Task.Run(() => {
        if (EConnectionState.Connected != connectionState) {
          return false;
        }

        NSData data = NSData.FromArray(packet);
        __nativeDevice.WriteValue(data, writeCharacteristic, CBCharacteristicWriteType.WithoutResponse);

        return true;
      });
    }


    public async Task<string> PullDeviceName() {
      Log.D(this, "Pulling device name");
      bool needsDisconnect = false;
      if (EConnectionState.Connected != connectionState) {
        needsDisconnect = true;
        await Connect();
      }

      try {
        __nativeDevice.ReadValue(nameCharacteristic);

        var start = DateTime.Now;
        var timeout = TimeSpan.FromSeconds(3);

        // TODO ahodder@appioninc.com: This can be improved. For not it works as a quick and dirty solution
        while (name == null && DateTime.Now - start < timeout) {
          await Task.Delay(10);
        }

        Log.D(this, "Name is: " + name);

        return name;
      } catch (Exception e) {
        Log.E(this, "wtf?", e);
        return null;
      } finally {
        if (needsDisconnect) {
          Disconnect();
        }
      }
    }

    /// <summary>
    /// Checks to make sure that the connection has valid serices and
    /// characteristics. If the method fails, then the connection cannot possibly
    /// support stable communication: all attempts will fail.
    /// </summary>
    /// <returns></returns>
    private bool ValidateServices() {
      readCharacteristic = null;
      writeCharacteristic = null;

      foreach (CBService service in __nativeDevice.Services) {
        if (service != null && service.Characteristics != null) {
          // Apparently services can be null after discovery?
          foreach (CBCharacteristic characteristic in service.Characteristics) {
            if (READ_CHARACTERISTIC.characteristic.Equals(characteristic.UUID)) {
              readCharacteristic = characteristic;
            } else if (WRITE_CHARACTERISTIC.characteristic.Equals(characteristic.UUID)) {
              writeCharacteristic = characteristic;
            } else if (NAME_CHARACTERISTIC.characteristic.Equals(characteristic.UUID)) {
              nameCharacteristic = characteristic;
            }
          }
        }
      }

      return readCharacteristic != null && writeCharacteristic != null && nameCharacteristic != null;
    }

    /// <summary>
    /// Called when the central manager receives a disconnected peripheral event. This
    /// will give the connection a chance to respond to its own disconnect.
    /// </summary>
    /// <param name="sensor">Sensor.</param>
    /// <param name="args">Arguments.</param>
    private void OnPeripheralDisconnected(object sensor, CBPeripheralErrorEventArgs args) {
      if (args.Peripheral.Equals(__nativeDevice)) {
        Disconnect();
      }
    }

    internal class ServiceCharacteristicPair {
      public CBUUID service { get; set; }
      public CBUUID characteristic { get; set; }

      public ServiceCharacteristicPair(string service, string characteristic) {
        this.service = CBUUID.FromString(service);
        this.characteristic = CBUUID.FromString(characteristic);
      }
    }
  } // End IOSLeConnection
}