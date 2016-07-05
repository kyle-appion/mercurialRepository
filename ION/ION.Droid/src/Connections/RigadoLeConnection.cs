namespace ION.Droid {

  using System;
	using System.Threading;
  using System.Threading.Tasks;

  using Android.Bluetooth;
  using Android.Content;

  using Java.Util;

  using ION.Core.Connections;
  using ION.Core.IO;
  using ION.Core.Util;

  /// <summary>
  /// A BLE connection class that is used to communicate with the rigado bluetooth chipset.
  /// </summary>
  public class RigadoLeConnection : BluetoothGattCallback, IConnection {
    /// <summary>
    /// The UUID that is used to identify services and characteristics within the ridago BLE connection.
    /// </summary>
    private const string RIGADO_UUID = "6E40****-B5A3-F393-E0A9-E50E24DCCA9E";
    /// <summary>
    /// The uuid that is used to identify the read service for the rigado connection.
    /// </summary>
    private static readonly UUID BASE_SERVICE = UUID.FromString(RIGADO_UUID.Replace("****", "0001"));
    /// <summary>
    /// The uuid that is used to identify the read characterstistic for the rigado connection.
    /// </summary>
    private static readonly UUID READ_CHARACTERISTIC = UUID.FromString(RIGADO_UUID.Replace("****", "0003"));
    /// <summary>
    /// The descriptor for the read characteristic.
    /// </summary>
    private static readonly UUID READ_CHARACTERISTIC_DESCRIPTOR = UUID.FromString("00002902-0000-1000-8000-00805f9b34fb");
    /// <summary>
    /// The uuid that is used to identify the write characteristic for the rigado connection.
    /// </summary>
    private static readonly UUID WRITE_CHARACTERISTIC = UUID.FromString(RIGADO_UUID.Replace("****", "0002"));

    /// <summary>
    /// The event registry that will be notified when the connection's state changes.
    /// </summary>
    public event OnConnectionStateChanged onStateChanged;
    /// <summary>
    /// The event registrt that will be notified when the connection receives a new data packet.
    /// </summary>
    public event OnDataReceived onDataReceived;
    /// <summary>
    /// Queries the current state of the connection.
    /// </summary>
    /// <value>The state of the connection.</value>
    public EConnectionState connectionState {
      get {
        return __connectionState;
      }
      set {
        var oldState = connectionState;
        __connectionState = value;
        Log.D(this, "Device: " + device.Name + " went from state " + oldState + " to " + value);
        if (onStateChanged != null && oldState != connectionState) {
          onStateChanged(this, oldState);
        }
      }
    } EConnectionState __connectionState;
    /// <summary>
    /// Queries whether or not the connection is connected.
    /// </summary>
    /// <value>true</value>
    /// <c>false</c>
    public bool isConnected { get { return EConnectionState.Connected == connectionState; } }
    /// <summary>
    /// Queries the name of the connection. For almost every device, this will be the
    /// device's serial number.
    /// </summary>
    /// <value>The name.</value>
    public string name { get; set;}
    /// <summary>
    /// Queries the unique address of the connection.
    /// </summary>
    /// <value>The address.</value>
    public string address { get { return device.Address; } }
    /// <summary>
    /// Queries the current received signal strength of the connection.
    /// </summary>
    /// <value>The signal strength.</value>
    public ESignalStrength signalStrength { get; }
    /// <summary>
    /// Queries the last packet that the connection received.
    /// </summary>
    /// <value>The last packet.</value>
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
    /// <summary>
    /// Gets or sets the last seend.
    /// </summary>
    /// <value>The last seend.</value>
    public DateTime lastSeen { get; set; }
    /// <summary>
    /// Queries the native connection object that this connection is wrapping.
    /// </summary>
    /// <value>The native device.</value>
    public object nativeDevice { get { return device; } }
    /// <summary>
    /// The timeout that is applied when connecting to the remote terminal.
    /// </summary>
    /// <value>The connection timeout.</value>
    public TimeSpan connectionTimeout { get; set; }

    /// <summary>
    /// The context that is necessary for creating a connection.
    /// </summary>
    private Context context;
    /// <summary>
    /// The bluetooth manager that is managing the bluetooth device session.
    /// </summary>
    /// <value>The manager.</value>
    private BluetoothManager manager;
    /// <summary>
    /// The bluetooth device that the connection is wrapping and communicating with.
    /// </summary>
    /// <value>The device.</value>
    private BluetoothDevice device;
    /// <summary>
    /// The gatt that is set when the device is attempting to connect to a remote device.
    /// </summary>
    private BluetoothGatt gatt;
    /// <summary>
    /// The characteristic that the connection is using to read data from. This is also the notify characteristic.
    /// </summary>
    private BluetoothGattCharacteristic readCharacteristic;
    /// <summary>
    /// The characteristic that the connection is using to write commands to the device.
    /// </summary>
    private BluetoothGattCharacteristic writeCharacteristic;

    public RigadoLeConnection(Context context, BluetoothManager manager, BluetoothDevice device) {
      this.context = context;
      this.manager = manager;
      this.device = device;
      this.connectionTimeout = TimeSpan.FromSeconds(90);
    }

    /// <summary>
    /// Raises the characteristic read event.
    /// </summary>
    /// <param name="gatt">Gatt.</param>
    /// <param name="characteristic">Characteristic.</param>
    /// <param name="status">Status.</param>
    public override void OnCharacteristicRead(BluetoothGatt gatt, BluetoothGattCharacteristic characteristic, GattStatus status) {
      Log.D(this, "Read");
      if (characteristic.Equals(readCharacteristic)) {
        Log.D(this, "ReadCharacteristic.Value: " + characteristic.GetValue().ToByteString());
        lastPacket = characteristic.GetValue();
      }
    }

    /// <Docs>GATT client the characteristic is associated with</Docs>
    /// <summary>
    /// Callback triggered as a result of a remote characteristic notification.
    /// </summary>
    /// <para tool="javadoc-to-mdoc">Callback triggered as a result of a remote characteristic notification.</para>
    /// <format type="text/html">[Android Documentation]</format>
    /// <since version="Added in API level 18"></since>
    /// <param name="gatt">Gatt.</param>
    /// <param name="characteristic">Characteristic.</param>
    public override void OnCharacteristicChanged(BluetoothGatt gatt, BluetoothGattCharacteristic characteristic) {
      Log.D(this, "Changed");
      if (characteristic.Equals(readCharacteristic)) {
        Log.D(this, "ReadCharacteristic.Value: " + characteristic.GetValue().ToByteString());
        lastPacket = characteristic.GetValue();
      }
    }

    /// <Docs>GATT client</Docs>
    /// <summary>
    /// Raises the connection state change event.
    /// </summary>
    /// <param name="gatt">Gatt.</param>
    /// <param name="status">Status.</param>
    /// <param name="newState">New state.</param>
    public override void OnConnectionStateChange(BluetoothGatt gatt, GattStatus status, ProfileState newState) {
      switch (newState) {
        case ProfileState.Connected:
        Log.D(this, "Discovering services");
          gatt.DiscoverServices();
          connectionState = EConnectionState.Resolving;
          break;
        case ProfileState.Connecting:
          connectionState = EConnectionState.Connecting;
          break;
        case ProfileState.Disconnected:
          Log.D(this, "disconnected for why?");
          Disconnect();
          break;
        case ProfileState.Disconnecting:
          Log.D(this, "Disconnected for reasons");
          Disconnect();
          break;
      }
    }

    /// <summary>
    /// Raises the services discovered event.
    /// </summary>
    /// <param name="gatt">Gatt.</param>
    /// <param name="status">Status.</param>
    public override void OnServicesDiscovered(BluetoothGatt gatt, GattStatus status) {
      if (ValidateServices()) {
        if (EConnectionState.Resolving == connectionState) {
          Log.D(this, "Device successfully discovered characteristics");
          connectionState = EConnectionState.Connected;

          if (!gatt.SetCharacteristicNotification(readCharacteristic, true)) {
            Log.D(this, "Failed to set read to notify");
            Disconnect();
          }

          var descriptor = readCharacteristic.GetDescriptor(READ_CHARACTERISTIC_DESCRIPTOR);
          var enabled = new System.Collections.Generic.List<byte>(BluetoothGattDescriptor.EnableNotificationValue);
          if (descriptor == null || descriptor.SetValue(enabled.ToArray()) && !gatt.WriteDescriptor(descriptor)) {
            Log.D(this, "Failed to set read to notify");
            Disconnect();
          }
        } else {
          Log.D(this, "Device discovered services in a wierd state: " + GetGattState());
        }
      } else {
        Log.E(this, "Failed to resolve services for device: " + device.Name);
        connectionState = EConnectionState.Resolving;
        gatt.DiscoverServices();
      } 
    }

    /// <summary>
    /// Attempts to connect the connection's remote terminus.
    /// </summary>
    /// <returns>The async.</returns>
    public async Task<bool> ConnectAsync() {
      if (connectionState != EConnectionState.Disconnected) {
        return false;
      }

			try {
		    gatt = device.ConnectGatt(context, false, this);
		    connectionState = EConnectionState.Connecting;

		    var start = DateTime.Now;
		    Log.D(this, "Starting connection attempt at: " + start.ToLongTimeString() + " will timeout at: " + (start + TimeSpan.FromSeconds(45)));

				await Task.Delay(500);

		    while (!ValidateServices()) {
					if (DateTime.Now - start > connectionTimeout || connectionState == EConnectionState.Disconnected) {
		        Log.D(this, "discover timeout");
		        Disconnect();
		        return false;
		      }

					await Task.Delay(100);
		    }
    return true;
			} catch (Exception e) {
				Log.E(this, "Failed to connect to rigado device", e);
				Disconnect();
				return false;
			}
    }

    /// <summary>
    /// Disconnects the connection from the remote terminus.
    /// </summary>
    public void Disconnect() {
      lock (this) {
        Log.D(this, "Disconnecting....");
        if (gatt != null) {
          gatt.Disconnect();
          gatt.Close();
          gatt = null;
        }

        readCharacteristic = null;
        writeCharacteristic = null;

        connectionState = EConnectionState.Disconnected;
      }
    }

    /// <summary>
    /// Writes the given packet out to the remote terminus.
    /// </summary>
    /// <returns></returns>
    /// <param name="data">Data.</param>
    public bool Write(byte[] data) {
      if (isConnected && ValidateServices()) {
        writeCharacteristic.SetValue(data);
        return gatt.WriteCharacteristic(writeCharacteristic);
      } else {
        return false;
      }
    }

    /// <summary>
    /// A utility method that will query the gatt state of the device.
    /// </summary>
    /// <returns><c>true</c>, if gatt state was gotten, <c>false</c> otherwise.</returns>
    private ProfileState GetGattState() {
      return manager.GetConnectionState(device, ProfileType.Gatt);
    }

    /// <summary>
    /// Queries whether or not the device had properly discovered all of the necessary characteristics for proper use.
    /// </summary>
    /// <returns><c>true</c>, if services was validated, <c>false</c> otherwise.</returns>
    private bool ValidateServices() {
			Log.D(this, "Validating Services on thread: " + Thread.CurrentThread.ManagedThreadId);
      if (gatt == null) {
        return false;
      }

      readCharacteristic = null;
      writeCharacteristic = null;

      var baseService = gatt.GetService(BASE_SERVICE);
			if (baseService == null) {
				return false;
			}

      readCharacteristic = baseService.GetCharacteristic(READ_CHARACTERISTIC);
      writeCharacteristic = baseService.GetCharacteristic(WRITE_CHARACTERISTIC);

      return readCharacteristic != null && writeCharacteristic != null;
    }
  }
}
