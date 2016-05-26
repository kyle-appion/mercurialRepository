namespace ION.Droid.Connections {

  using System;
  using System.Threading.Tasks;

  using Android.Bluetooth;
  using Android.Content;
  using Android.Media;

  using Java.Util;

  using ION.Core.Connections;
  using ION.Core.Util;

  public class LeConnection : BluetoothGattCallback, IConnection {
    /// <summary>
    /// The base uuid that identifies services, characteristics and descriptors
    /// within the bluetooth gatt api.
    /// </summary>
    private const string BASE_UUID = "0000****-0000-1000-8000-00805f9b34fb";
    /// <summary>
    /// The UUID for the read service.
    /// </summary>
    private static readonly UUID READ_SERVICE = Inflate("ffe0");
    /// <summary>
    /// The UUID for the read characteristic.
    /// </summary>
    private static readonly UUID READ_CHARACTERISTIC = Inflate("ffe4");
    /// <summary>
    /// The UUID for the write service.
    /// </summary>
    private static readonly UUID WRITE_SERVICE = Inflate("ffe5");
    /// <summary>
    /// The UUID for the write characteristic.
    /// </summary>
    /// <param name="content">Content.</param>
    private static readonly UUID WRITE_CHARACTERISTIC = Inflate("ffe9");

    /// <summary>
    /// A utility method used to create the UUIDs necessary for use for the
    /// bluetooth api.
    /// </summary>
    /// <param name="content">Content.</param>
    private static UUID Inflate(string content) {
      return UUID.FromString(BASE_UUID.Replace("****", content));
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
          onStateChanged(this, oldState);
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
    public string name { get; set; }
    // Overridden from IConnection
    public string address { get { return device.Address; } }
    // Overridden from IConnection
    public ESignalStrength signalStrength {
      get {
        // These values are largely made up.
        if (__rssi > -35) {
          return ESignalStrength.Good;
        } else if (__rssi > -50) {
          return ESignalStrength.Fair;
        } else if (EConnectionState.Disconnected == connectionState) {
          return ESignalStrength.None;
        } else {
          return ESignalStrength.Bad;
        }
      }
    } int __rssi;
    // Overridden from IConnection
    public byte[] lastPacket { 
      get {
        return __lastPacket;
      }
      set {
        __lastPacket = value;
        lastSeen = DateTime.Now;
        if (onDataReceived != null) {
          onDataReceived(this, lastPacket);
        }
      }
    } byte[] __lastPacket;
    // Overridden from IConnection
    public DateTime lastSeen { get; set; }
    // Overridden from IConnection
    public object nativeDevice { get { return device as BluetoothDevice; } }
    // Overridden from IConnection
    public TimeSpan connectionTimeout { get; set; }

    /// <summary>
    /// The android context necessary for creating a connection.
    /// </summary>
    /// <value>The context.</value>
    private Context context { get; set; }
    /// <summary>
    /// The bluetooth manager that is NECESSARY FOR USE TO CHECK THE FUCKING CONNECTION STATE
    /// OF THE BLUETOOTH DEVICE?! Way to go Android, way to go.
    /// </summary>
    /// <value>The manager.</value>
    private BluetoothManager manager { get; set; }
    /// <summary>
    /// The android native device.
    /// </summary>
    /// <value>The native device.</value>
    private BluetoothDevice device { get; set; }
    /// <summary>
    /// The BluetoothGatt representing the device's connection.
    /// </summary>
    /// <value>The gatt.</value>
    private BluetoothGatt gatt { get; set; }
    /// <summary>
    /// The characteristic that the connection will read from.
    /// </summary>
    /// <value>The read.</value>
    private BluetoothGattCharacteristic read { get; set; }
    /// <summary>
    /// The characteristic that the connection will write form.
    /// </summary>
    /// <value>The write.</value>
    private BluetoothGattCharacteristic write { get; set; }

    public LeConnection(Context context, BluetoothManager manager, BluetoothDevice device) {
      this.context = context;
      this.manager = manager;
      this.device = device;
      this.name = device.Name;
      connectionTimeout = TimeSpan.FromSeconds(15);
    }

    // Overridden from BluetoothGattCallback
    public override void OnCharacteristicRead(BluetoothGatt bg, BluetoothGattCharacteristic characteristic, GattStatus status) {
      if (characteristic.Equals(read)) {
        lastPacket = characteristic.GetValue();
      }
    }

    // Overridden from BluetoothGattCallback
    public override void OnCharacteristicChanged(BluetoothGatt bg, BluetoothGattCharacteristic characteristic) {
      if (characteristic.Equals(read)) {
        lastPacket = characteristic.GetValue();
      }
    }

    // Overridden from BluetoothGattCallback
    public override void OnCharacteristicWrite(BluetoothGatt bg, BluetoothGattCharacteristic characteristic, GattStatus status) {
      Log.D(this, characteristic.Uuid + " wrote stuff");
    }

    // Overridden from BluetoothGattCallback
    public override void OnConnectionStateChange(BluetoothGatt bg, GattStatus status, ProfileState state) {
      switch (state) {
        case ProfileState.Connected:
          Log.D(this, device.Name + " connected");
          break; // Not needed right now.
        case ProfileState.Connecting:
          Log.D(this, device.Name + " connecting");
          break;
        case ProfileState.Disconnected:
          Log.D(this, device.Name + " disconnected");
          if (EConnectionState.Disconnected != connectionState) {
            Disconnect();
          }
          break;
        case ProfileState.Disconnecting:
          Log.D(this, device.Name + " disconnecting");
          if (EConnectionState.Disconnected != connectionState) {
            Disconnect();
          }
          break;
        default:
          Log.D(this, device.Name + " unknown action");
          break;
      }
    }

    // Overridden from BluetoothGattCallback
    public override void OnServicesDiscovered(BluetoothGatt bg, GattStatus status) {
      Log.D(this, "Services discovered");
/*
      if (EConnectionState.Resolving == connectionState) {
        if (ValidateServices()) {
          connectionState = EConnectionState.Connected;
        } else {
          Log.D(this, "Failed to validate services");
          Disconnect();
        }
      }
*/
    }

    /// <summary>
    /// Dispose the specified disposing.
    /// </summary>
    /// <param name="disposing">If set to <c>true</c> disposing.</param>
    protected override void Dispose(bool disposing) {
      base.Dispose(disposing);
      this.Dispose();
    }

    // Overridden from IConnection
    public new void Dispose() {
      if (EConnectionState.Disconnected != connectionState) {
        Disconnect();
      }
    }

    // Overridden from IConnection
    public async Task<bool> ConnectAsync() {
      if (EConnectionState.Disconnected != connectionState) {
        Log.D(this, "Connection not in a disconnected state: returning attempt as failed.");
        return false;
      }

      connectionState = EConnectionState.Connecting;

      DateTime start = DateTime.Now;
      gatt = device.ConnectGatt(context, false, this);

      await Task.Delay(100);

      // Wait for the connection to be established
      while (ProfileState.Connected != manager.GetConnectionState(device, ProfileType.Gatt) && DateTime.Now - start < connectionTimeout) {
        await Task.Delay(50);
      }
      Log.D(this, "Done waiting for connect spool");

      if (ProfileState.Disconnected == manager.GetConnectionState(device, ProfileType.Gatt)) {
        Log.D(this, "Failed to connect the remote device");
        Disconnect();
        return false;
      }

      connectionState = EConnectionState.Resolving;
      // Attempt to discover the device's services
      if (!gatt.DiscoverServices()) {
        Log.E(this, "Failed to discover services");
        Disconnect();
        return false;
      }

      while (!ValidateServices() && DateTime.Now - start < connectionTimeout) {
        await Task.Delay(50);
      }

      if (ValidateServices()) {
        Log.D(this, "Connection successful");
        connectionState = EConnectionState.Connected;
        gatt.SetCharacteristicNotification(read, true);
        return true;
      } else {
        Log.D(this, "Failed to discover services");
        Disconnect();
        return false;
      }
    }

    // Overridden from IConnection
    public void Disconnect() {
      lock (this) {
        if (gatt != null) {
          gatt.Disconnect();
          gatt.Close();
          gatt = null;
        }
        connectionState = EConnectionState.Disconnected;
      }
    }

    // Overridden from IConnection
    public bool Write(byte[] data) {
      if (EConnectionState.Connected == connectionState) {
        write.SetValue(data);
        return gatt.WriteCharacteristic(write);
      } else {
        return false;
      }
    }

    /// <summary>
    /// Validates the bluetooth gatt's current known services to such that the connection
    /// knows that the it has a stable connection capable of reading and writing.
    /// </summary>
    private bool ValidateServices() {
      if (gatt == null) {
        return false;
      }

      read = null;
      write = null;

      var readService = gatt.GetService(READ_SERVICE);
      if (readService != null) {
        read = readService.GetCharacteristic(READ_CHARACTERISTIC);
      }

      var writeService = gatt.GetService(WRITE_SERVICE);
      if (writeService != null) {
        write = writeService.GetCharacteristic(WRITE_CHARACTERISTIC);
      }

      return read != null && write != null;
    }
  }
}

