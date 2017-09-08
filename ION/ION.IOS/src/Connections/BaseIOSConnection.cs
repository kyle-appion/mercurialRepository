namespace ION.IOS.Connections {
  
  using System;

  using CoreBluetooth;
  using CoreFoundation;
  using Foundation;

	using Appion.Commons.Util;

  using ION.Core.Connections;

  public abstract class BaseIOSConnection : CBPeripheralDelegate, IConnection {

    // Implemented for IConnection
    public event OnConnectionStateChanged onStateChanged;
    // Implemented for IConnection
    public event OnDataReceived onDataReceived;

    /// Implemented for IConnection
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
    // Implemented for IConnection
    public bool isConnected { 
      get {
        return EConnectionState.Connected == connectionState;
      } 
    }
    // Implemented for IConnection
    public string name { get; set; }
    // Implemented for IConnection
    public string address { 
      get {
        return device.Identifier.AsString();
      }
    }
    // Implemented for IConnection
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
    // Implemented for IConnection
    public DateTime lastSeen { get; set; }
    // Implemented for IConnection
    public object nativeDevice { get { return device; } }
    // Implemented for IConnection
    public TimeSpan connectionTimeout { get; set; }

    /// <summary>
    /// The native device.
    /// </summary>
    /// <value>The device.</value>
    protected CBPeripheral device { get; private set; }

    /// <summary>
    /// The object used for synchronization (because the bluetooth stack is multithreaded).
    /// </summary>
    /// <value>The locker.</value>
    protected object locker { get; private set; }

		/// <summary>
		/// The connection helper that created the connection.
		/// </summary>
		private IonCBCentralManagerDelegate centralDelegate;
    /// <summary>
    /// The dispatch queue that will allow us to post actions to the main thread message pump.
    /// </summary>
    private DispatchQueue handler;

    public BaseIOSConnection(IonCBCentralManagerDelegate centralDelegate, CBPeripheral peripheral) {
      this.centralDelegate = centralDelegate;
      device = peripheral;
      name = peripheral.Name;
      peripheral.Delegate = this;

      connectionState = EConnectionState.Disconnected;
      connectionTimeout = TimeSpan.FromMilliseconds(45 * 1000);

      centralDelegate.onDeviceConnected += (obj) => {
        OnConnected();
      };
      handler = DispatchQueue.MainQueue;
      locker = new object();
    }

    /// <summary>
    /// Attempts to connect the connection's remote terminus.
    /// </summary>
    /// <returns>The async.</returns>
    public bool Connect(bool passive = false) {
      lock (locker) {
        if (EConnectionState.Disconnected != connectionState) {
          return false;
        }
  			try {
  	      connectionState = EConnectionState.Connecting;
  	      DateTime start = DateTime.Now;

  	      PeripheralConnectionOptions options = new PeripheralConnectionOptions();
  	      options.NotifyOnConnection = true;
  	      options.NotifyOnDisconnection = true;
  	      options.NotifyOnNotification = true;

  	      centralDelegate.centralManager.ConnectPeripheral(device, options);
          return true;
  			} catch (Exception e) {
  				Log.E(this, "Failed to connect", e);
  				Disconnect();
  				return false;
  			}
      }
    }

    /// <summary>
    /// Disconnects the connection from the remote terminus.
    /// </summary>
    public void Disconnect(bool reconnect=false) {
      lock (locker) {
  			if (connectionState == EConnectionState.Disconnected) {
  				return;
  			}
        connectionState = EConnectionState.Disconnected;
        centralDelegate.centralManager.CancelPeripheralConnection(device);

        OnDisconnect();

        if (reconnect) {
          handler.DispatchAfter(TimeSpan.FromMilliseconds(1500), () => Connect());
        }
      }
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
    public override sealed void DiscoveredService(CBPeripheral peripheral, NSError error) {
      //      Log.D(this, "Discovered " + __nativeDevice.Services.Length + " Services for device" + name + "...");
      if (device.Services != null) {
        foreach (var service in device.Services) {
          //        Log.D(this, "Service is: " + service.UUID);
          device.DiscoverCharacteristics(service);
        }
      }
    }

    /// <summary>
    /// Called when the a characteristic is discovered.
    /// </summary>
    /// <param name="peripheral">Peripheral.</param>
    /// <param name="service">Service.</param>
    /// <param name="error">Error.</param>
    public override sealed void DiscoveredCharacteristic(CBPeripheral peripheral, CBService service, NSError error) {
      if (!AreServicesValid()) {
        if (ValidateServices()) {
          if (device.State == CBPeripheralState.Connected && connectionState == EConnectionState.Resolving) {
            connectionState = EConnectionState.Connected;
          }
        }
      }
    }

    /// <summary>
    /// Called when the disconnects. This is where you do any clean up that may need to happen.
    /// </summary>
    protected virtual void OnDisconnect() {
    }

    /// <summary>
    /// Checks to make sure that the connection has valid serices and characteristics. If the method fails, then the
    /// connection cannot possibly support stable communication: all attempts will fail.
    /// </summary>
    protected abstract bool ValidateServices();

    /// <summary>
    /// Whether or not the services are valid for the connection.
    /// </summary>
    /// <returns><c>true</c>, if services valid was ared, <c>false</c> otherwise.</returns>
    protected abstract bool AreServicesValid();

    /// <summary>
    /// Called when we get confirmation that the peripheral has connected.
    /// </summary>
    private void OnConnected() {
      if (device.State != CBPeripheralState.Connected) {
        return;
      }

      if (connectionState != EConnectionState.Connected) {
        connectionState = EConnectionState.Resolving;
        device.DiscoverServices();
        DispatchQueue.MainQueue.DispatchAfter(TimeSpan.FromSeconds(45), () => {
          if (!AreServicesValid()) {
              Disconnect(true);
          }
        });
      }
    }

  }
}
