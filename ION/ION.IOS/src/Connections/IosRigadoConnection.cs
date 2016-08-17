namespace ION.IOS.Connections {

  using System;
  using System.Threading.Tasks;

  using CoreBluetooth;
  using Foundation;

  using ION.Core.Connections;
  using ION.Core.Util;

  public class IosRigadoConnection : IConnection {
    /// <summary>
    /// The base UUID that identifies services, charactertistics and descriptors
    /// within the BluetoothGatt API.
    /// </summary>
		private const string BASE_UUID = "6e40****-b5a3-f393-e0a9-e50e24dcca9e";
    /// <summary>
    /// The Rx characteristic.
    /// </summary>
		private static readonly CBUUID READ_CHARACTERISTIC = CBUUID.FromString("6e400003-b5a3-f393-e0a9-e50e24dcca9e");
    /// <summary>
    /// The Tx characteristic.
    /// </summary>
		private static readonly CBUUID WRITE_CHARACTERISTIC = CBUUID.FromString("6e400002-b5a3-f393-e0a9-e50e24dcca9e");

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
        var oldState = __connectionState;
        __connectionState = value;
        if (onStateChanged != null) {
          ION.Core.App.AppState.context.PostToMain(() => {
            onStateChanged(this, oldState);
          });
        }
      }
    } EConnectionState __connectionState;
    /// <summary>
    /// Queries whether or not the connection is connected.
    /// </summary>
    /// <value>true</value>
    /// <c>false</c>
    public bool isConnected {
      get {
        return EConnectionState.Connected == connectionState;
      }
    }
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
    private LeConnectionHelper connectionHelper { get; set; }
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
    public IosRigadoConnection(LeConnectionHelper connectionHelper, CBPeripheral peripheral) {
			this.connectionHelper = connectionHelper;
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
        } else if (args.Characteristic.Equals(readCharacteristic)) {
          lastPacket = readCharacteristic.Value.ToArray();
        } else {
          //          Log.D(this, "Received unknown characteristic value: " + args.Characteristic);
        }
      };

      __nativeDevice.DiscoveredService += onServiceDiscoveredDelegate;
      __nativeDevice.UpdatedCharacterteristicValue += onCharacteristicChangedDelegate;
      __nativeDevice.DiscoveredCharacteristic += onCharacteristicDiscoveredDelegate;
			connectionHelper.onPeripheralDisconnected += OnPeripheralDisconnected;


      connectionState = EConnectionState.Disconnected;
      connectionTimeout = TimeSpan.FromMilliseconds(45 * 1000);
    }

    // Overridden from IConnection
    public void Dispose() {
      __nativeDevice.DiscoveredService -= onServiceDiscoveredDelegate;
      __nativeDevice.DiscoveredCharacteristic -= onCharacteristicDiscoveredDelegate;
      __nativeDevice.UpdatedCharacterteristicValue -= onCharacteristicChangedDelegate;
			connectionHelper.onPeripheralDisconnected -= OnPeripheralDisconnected;
    }

    // Overridden from IConnection
    public async Task<bool> ConnectAsync() {
      if (EConnectionState.Disconnected != connectionState) {
        return false;
      }

      connectionState = EConnectionState.Connecting;

      DateTime start = DateTime.Now;

      PeripheralConnectionOptions options = new PeripheralConnectionOptions();
      options.NotifyOnConnection = true;
      options.NotifyOnDisconnection = true;
      options.NotifyOnNotification = true;

			connectionHelper.centralManager.ConnectPeripheral(__nativeDevice, options);

      while (CBPeripheralState.Connected != __nativeDevice.State && EConnectionState.Disconnected != connectionState) {
        if (DateTime.Now - start > TimeSpan.FromSeconds(5)/*connectionTimeout*/) {
          Disconnect();
          return false;
        } else {
          await Task.Delay(10);
        }
      }

      await Task.Delay(100);

      __nativeDevice.DiscoverServices((CBUUID[])null/*DESIRED_SERVICES*/);

      while (EConnectionState.Connected != connectionState && EConnectionState.Disconnected != connectionState) {
        if (DateTime.Now - start > connectionTimeout) {
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
      connectionHelper.centralManager.CancelPeripheralConnection(__nativeDevice);
      connectionState = EConnectionState.Disconnected;
    }

    // Overridden from IConnection
    public bool Write(byte[] packet) {
      if (EConnectionState.Connected != connectionState) {
        return false;
      }

      NSData data = NSData.FromArray(packet);
      __nativeDevice.WriteValue(data, writeCharacteristic, CBCharacteristicWriteType.WithoutResponse);

      return true;
    }


    public async Task<string> PullDeviceName() {
      bool needsDisconnect = false;
      if (EConnectionState.Connected != connectionState) {
        needsDisconnect = true;
        await ConnectAsync();
      }

      try {
        __nativeDevice.ReadValue(nameCharacteristic);

        var start = DateTime.Now;
        var timeout = TimeSpan.FromSeconds(3);

        // TODO ahodder@appioninc.com: This can be improved. For not it works as a quick and dirty solution
        while (name == null && DateTime.Now - start < timeout) {
          await Task.Delay(10);
        }

        return name;
      } catch (Exception e) {
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
        Log.D(this, "Service: " + service.UUID);
        if (service != null && service.Characteristics != null) {
          // Apparently services can be null after discovery?
          foreach (CBCharacteristic characteristic in service.Characteristics) {
            Log.D(this, "Characteristic: " + characteristic.UUID);
            if (READ_CHARACTERISTIC.Equals(characteristic.UUID)) {
              readCharacteristic = characteristic;
            } else if (WRITE_CHARACTERISTIC.Equals(characteristic.UUID)) {
              writeCharacteristic = characteristic;
            }
          }
        }
      }

			return readCharacteristic != null && writeCharacteristic != null;
    }

    /// <summary>
    /// Called when the central manager receives a disconnected peripheral event. This
    /// will give the connection a chance to respond to its own disconnect.
    /// </summary>
    /// <param name="sensor">Sensor.</param>
    /// <param name="args">Arguments.</param>
    private void OnPeripheralDisconnected(object sensor, CBPeripheral peripheral) {
      if (peripheral.Equals(__nativeDevice)) {
        Disconnect();
      }
    }
  }
}

