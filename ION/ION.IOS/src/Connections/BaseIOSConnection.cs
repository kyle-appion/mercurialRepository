namespace ION.IOS.Connections {
  
  using System;
  using System.Threading.Tasks;

  using CoreBluetooth;
  using Foundation;

	using Appion.Commons.Util;

  using ION.Core.Connections;

  public abstract class BaseIOSConnection : CBPeripheralDelegate, IConnection {

    /// <summary>
    /// The event registry that will be notified when the connection's state changes.
    /// </summary>
    public event OnConnectionStateChanged onStateChanged;
    /// <summary>
    /// Occurs when on data recieved.
    /// </summary>
    public event OnDataReceived onDataReceived;

    /// <summary>
    /// The state of the connection.
    /// </summary>
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
    /// <summary>
    /// Queries the name of the connection. For almost every device, this will be the
    /// device's serial number.
    /// </summary>
    /// <value>The name.</value>
    public string name { get; set; }
    /// <summary>
    /// Queries the unique address of the connection.
    /// </summary>
    /// <value>The address.</value>
    public string address { 
      get {
        return __nativeDevice.Identifier.AsString();
      }
    }
    /// <summary>
    /// Queries the current received signal strength of the connection.
    /// </summary>
    /// <value>The signal strength.</value>
    public ESignalStrength signalStrength {
      get {
        return ESignalStrength.None;
      }
    }
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
          onDataReceived(this, __lastPacket);
        }
      }
    } byte[] __lastPacket;
    /// <summary>
    /// The last time that connection was seen- either through packet resolution or a scan.
    /// </summary>
    /// <value>The last seen.</value>
    public DateTime lastSeen { get; set; }
    /// <summary>
    /// Gets the native device.
    /// </summary>
    /// <value>The native device.</value>
    public object nativeDevice {
      get {
        return __nativeDevice;
      }
    } protected CBPeripheral __nativeDevice;
    /// <summary>
    /// The timeout that is applied when connecting to the remote terminal.
    /// </summary>
    /// <value>The connection timeout.</value>
    public TimeSpan connectionTimeout { get; set; }

    /// <summary>
    /// The ios central bluetooth manager that is managing this peripheral.
    /// </summary>
//    protected readonly CBCentralManager centralManager;

		/// <summary>
		/// The connection helper that created the connection.
		/// </summary>
		private LeConnectionHelper connectionHelper;

    public BaseIOSConnection(LeConnectionHelper connectionHelper, CBPeripheral peripheral) {
			this.connectionHelper = connectionHelper;
      __nativeDevice = peripheral;
      peripheral.Delegate = this;

      connectionState = EConnectionState.Disconnected;
      connectionTimeout = TimeSpan.FromMilliseconds(45 * 1000);
    }

    /// <summary>
    /// Attempts to connect the connection's remote terminus.
    /// </summary>
    /// <returns>The async.</returns>
    public async virtual Task<bool> ConnectAsync() {
      if (EConnectionState.Disconnected != connectionState) {
				Log.D(this, "connection state not disconnected. Returning false");
        return false;
      }
			try {
				Log.D(this, "BaseIOSConnection is attempting to connect");

	      connectionState = EConnectionState.Connecting;
	      DateTime start = DateTime.Now;

	      PeripheralConnectionOptions options = new PeripheralConnectionOptions();
	      options.NotifyOnConnection = true;
	      options.NotifyOnDisconnection = true;
	      options.NotifyOnNotification = true;

	      connectionHelper.onPeripheralDisconnected += OnCBPeripheralDisconnected;
	      connectionHelper.centralManager.ConnectPeripheral(__nativeDevice, options);

	      Log.D(this, "Awaiting physical connection...");
	      while (CBPeripheralState.Connected != __nativeDevice.State && EConnectionState.Disconnected != connectionState) {
	        if (DateTime.Now - start > TimeSpan.FromSeconds(5)) {
	          Log.D(this, "timeout: failed to connect");
	          Disconnect();
	          return false;
	        } else {
	          await Task.Delay(50);
	        }
	      }

	      await Task.Delay(100);

	      __nativeDevice.DiscoverServices((CBUUID[])null);  // DESIRED SERVICES ARRAY

	      Log.D(this, "Awaiting service discovery");
	      while (EConnectionState.Connected != connectionState && EConnectionState.Disconnected != connectionState && !AreServicesValid()) {
	        if (DateTime.Now - start > connectionTimeout) {
	          Log.D(this, "timeout: failed to validate services");
	          Disconnect();
	          return false;
	        } else {
	          await Task.Delay(50);
	        }
	      }

	      if (!AreServicesValid()) {
	        Log.E(this, "Failed to validate services");
	        Disconnect();
	        return false;
	      }

	      Log.D(this, "Connection success!");
	      OnConnected();
	      connectionState = EConnectionState.Connected;

	      return EConnectionState.Connected == connectionState;
			} catch (Exception e) {
				Log.E(this, "Failed to connect", e);
				Disconnect();
				return false;
			}
    }

    /// <summary>
    /// Disconnects the connection from the remote terminus.
    /// </summary>
    public void Disconnect() {
			if (connectionState == EConnectionState.Disconnected) {
				return;
			}
      Log.D(this, name + " disconnected");
      connectionHelper.onPeripheralDisconnected -= OnCBPeripheralDisconnected;
      connectionHelper.centralManager.CancelPeripheralConnection(__nativeDevice);
      connectionState = EConnectionState.Disconnected;
    }

    /// <summary>
    /// Writes the given packet out to the remote terminus.
    /// </summary>
    /// <returns></returns>
    /// <param name="packet">Packet.</param>
    public abstract bool Write(byte[] packet);

    /// <summary>
    /// Called when a service is discovered.
    /// </summary>
    /// <param name="peripheral">Peripheral.</param>
    /// <param name="error">Error.</param>
    public override void DiscoveredService(CBPeripheral peripheral, NSError error) {
      Log.D(this, "Discovered " + __nativeDevice.Services.Length + " Services for device" + name + "...");
      foreach (var service in __nativeDevice.Services) {
        Log.D(this, "Service is: " + service.UUID);
        __nativeDevice.DiscoverCharacteristics(service);
      }
    }

    /// <summary>
    /// Called when the a characteristic is discovered.
    /// </summary>
    /// <param name="peripheral">Peripheral.</param>
    /// <param name="service">Service.</param>
    /// <param name="error">Error.</param>
    public override void DiscoveredCharacteristic(CBPeripheral peripheral, CBService service, NSError error) {
      ValidateServices();
    }

    /// <summary>
    /// Called when the connection successfully connects.
    /// </summary>
    protected virtual void OnConnected() {
    }

    /// <summary>
    /// Called by the BaseIONConnection when new characteristics are discovered. This is where you should attempt to
    /// find all the characteristics that are necessary for your communication.
    /// </summary>
    protected virtual void ValidateServices() {
    }

    /// <summary>
    /// Queries whether or not the services are valid.
    /// </summary>
    /// <returns><c>true</c>, if services valid was ared, <c>false</c> otherwise.</returns>
    protected abstract bool AreServicesValid();

    /// <summary>
    /// Called when the peripheral disconnects.
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="e">E.</param>
    private void OnCBPeripheralDisconnected(object o, CBPeripheral peripheral) {
      if (peripheral.Equals(__nativeDevice)) {
        Disconnect();
      }
    }
  }
}

