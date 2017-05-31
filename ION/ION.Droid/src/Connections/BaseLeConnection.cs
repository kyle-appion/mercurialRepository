namespace ION.Droid.Connections {

  using System;

  using Android.Bluetooth;
  using Android.OS;

  using Appion.Commons.Util;

  // Using ION
  using Core.Connections;

  public abstract class BaseLeConnection : BluetoothGattCallback, IConnection, Handler.ICallback {
    /// <summary>
    /// The default time that is allowed for a rigado connection attempt.
    /// </summary>
    private static readonly TimeSpan DEFAULT_TIMEOUT = TimeSpan.FromSeconds(45);
    private static readonly TimeSpan LONG_RANGE_TIMEOUT = TimeSpan.FromMilliseconds(7500);
    /// <summary>
    /// The delay from a disconnecto to a reconnect attempt.
    /// </summary>
    private const long PASSIVE_DELAY = 1000 * 30;
    /// <summary>
    /// The delay before the connection disconnects because the service scan timed out.
    /// </summary>
    private const long SERVICES_DELAY = 1000 * 20;
    /// <summary>
    /// The message that is used to check whether or not the connection has successfully found its services.
    /// </summary>
    private const int MSG_CHECK_SERVICES = 2;
    /// <summary>
    /// The message that is used to check whether or not the connection is still in long range mode.
    /// </summary>
    private const int MSG_CHECK_LONG_RANGE = 3;
    /// <summary>
    /// The maximum attempts to resolve a connection.
    /// </summary>
    private const int MAX_CONNECTION_ATTEMPTS = 3;

    // Implemented for IConnection
    public event OnConnectionStateChanged onStateChanged;
    // Implmented for IConnection
    public event OnDataReceived onDataReceived;

    // Implemented for IConnection
    public EConnectionState connectionState {
      get {
        return __connectionState;
      }
      private set {
        var oldState = connectionState;
        __connectionState = value;
        if (onStateChanged != null) {
          onStateChanged(this, oldState);
        }
      }
    } EConnectionState __connectionState;


    // Implemented for IConnection
    public bool isConnected { get { return connectionState == EConnectionState.Connected; } }
    // Implemented for IConnection
    public string name { get { return device.Name; } }
    // Implemented for IConnection
    public string address { get { return device.Address; } }
    // Implemented for IConnection
    public byte[] lastPacket {
      get {
        return __lastPacket;
      }
      set {
        __lastPacket = value;
        lastSeen = DateTime.Now;
        lastPacketTime = DateTime.Now;
        if (onDataReceived != null) {
          onDataReceived(this, __lastPacket);
        }
      }
    }
    byte[] __lastPacket;

    // Implemented for IConnection
    public DateTime lastSeen { get; set; }
    // Implemented for IConnection
    public object nativeDevice { get { return device; } }
    // Implemented for IConnection
    public TimeSpan connectionTimeout { get; set; }

    /// <summary>
    /// The native device representing the connection.
    /// </summary>
    /// <value>The device.</value>
    public BluetoothDevice device { get; private set; }

    /// <summary>
    /// The connection manager that is used to manager connection operations and state.
    /// </summary>
    /// <value>The manager.</value>
    protected AndroidConnectionManager manager { get; private set; }
    /// <summary>
    /// The bluetooth gatt that the connection is using to interface with the bluetooth module.
    /// </summary>
    protected BluetoothGatt gatt {
      get {
        return __gatt;
      }
      set {
        __gatt = value;
      }
    }
    BluetoothGatt __gatt;
    /// <summary>
    /// Whether or not the connection should connect passively.
    /// </summary>
    private bool shouldPassiveConnect;

    /// <summary>
    /// The last time we received a packet.
    /// </summary>
    private DateTime lastPacketTime;
    /// <summary>
    /// The object that is locked for multithread synchronization.
    /// </summary>
    private object locker = new object();
    /// <summary>
    /// The handler that is used to post delayed actions to.
    /// </summary>
    private Handler handler;
    /// <summary>
    /// The number of connection attempts that have been made.
    /// </summary>
    private int attempts;

    public BaseLeConnection(AndroidConnectionManager manager, BluetoothDevice device) {
      this.manager = manager;
      this.device = device;
      connectionState = EConnectionState.Disconnected;
      connectionTimeout = DEFAULT_TIMEOUT;
      lastSeen = DateTime.FromFileTimeUtc(0);
      handler = new Handler(Looper.MainLooper, this);
    }

    // Implemented for IConnection
    public bool Connect(bool passive = false) {
      lock (locker) {
        if (isConnected) {
          return true;
        } else {
          // Ensure a clean connection attempt
          if (gatt != null || connectionState != EConnectionState.Disconnected) {
						Disconnect(false);
          }

          try {
						Log.D(this, "Device {" + name + "} is performing an " + (passive ? "passive" : "active") + " connection");
						if (passive) {
							gatt = device.ConnectGatt(manager.context, true, this);
						} else {
							connectionState = EConnectionState.Connecting;
							gatt = device.ConnectGatt(manager.context, false, this);
						}
            return true;
          } catch (Exception e) {
            Log.E(this, "Failed to start pending gatt connection.", e);
            return false;
          }
        }
      }
    }

    // Implemented for IConnection
    public void Disconnect(bool reconnect = false) {
      lock (locker) {
        Log.D(this, "Device {" + name + "} is disconnecting");
        handler.RemoveCallbacksAndMessages(null);

        OnDisconnect();

        lastSeen = DateTime.Now;
        lastPacketTime = DateTime.MaxValue;
        connectionState = EConnectionState.Disconnected;

        if (gatt != null) {
          gatt.Disconnect();
          gatt.Close();
          gatt = null;

          if (reconnect) {
            handler.PostDelayed(() => Connect(true), 1500);
          }
        }
      }
    }

    // Overridden from BluetoothGattCallback
    public sealed override void OnConnectionStateChange(BluetoothGatt gatt, GattStatus status, ProfileState newState) {
      lock (locker) {
        switch (newState) {
          case ProfileState.Connected: {
              handler.RemoveCallbacksAndMessages(null);
              connectionState = EConnectionState.Resolving;
              lastSeen = DateTime.Now; // We at least know that the device is nearby at this point.

              if (!gatt.DiscoverServices()) {
                Log.E(this, "Failed to start service discovery for: " + device.Name);
                Disconnect();
              } else {
                connectionState = EConnectionState.Resolving;
                handler.SendEmptyMessageDelayed(MSG_CHECK_SERVICES, SERVICES_DELAY);
              }
              break;
            } // ProfileState.Connected

          case ProfileState.Connecting: {
              break;
            } // ProfileState.Connecting

          case ProfileState.Disconnected: {
							// Only attempt a reconnect if the device disconnected unexpectedly
							bool reconnect = connectionState == EConnectionState.Connected && manager.ion.preferences.device.allowDeviceAutoConnect;
              Disconnect(reconnect);
              break;
            } // ProfileState.Disconnected

          case ProfileState.Disconnecting: {
              break;
            } // ProfileState.Disconnecting
        }
      }
    }

    // Overridden from BluetoothGattCallback
    public sealed override void OnServicesDiscovered(BluetoothGatt gatt, GattStatus status) {
      lock (locker) {
        if (ValidateServices()) {
          handler.RemoveCallbacksAndMessages(null);
          if (!OnConnectionSuccess()) {
            Log.E(this, "Failed to successfully handle post connection procedures for {" + name + "}");
            Disconnect();
            return;
          } else {
            connectionState = EConnectionState.Connected;
          }
        } else {
          Log.E(this, "Failed to discover services. Disconnecting device.");
          Disconnect();
        }
      }
    }

    // Implemented for Handler.ICallback
    public bool HandleMessage(Message msg) {
      lock (locker) {
        switch (msg.What) {
          case MSG_CHECK_SERVICES: {
              if (!ValidateServices()) {
                Log.E(this, "Failed to validate services; disconnecting with intention of an immediate reconnect");
                if (attempts <= MAX_CONNECTION_ATTEMPTS) {
                  Disconnect(true);
									attempts++;
                } else {
                  Disconnect(false);
                  attempts = 0;
                }
              } else {
                attempts = 0;
              }
              return true;
            } // MSG_CHECK_SERVICES
              /*
                        case MSG_CHECK_LONG_RANGE: { 
                          Log.D(this, "long range mode check");
                          if (DateTime.Now - lastPacketTime > LONG_RANGE_TIMEOUT) {
                            connectionState = EConnectionState.Disconnected;
                          }
                          return true;
                        } // MSG_CHECK_LONG_RANGE
              */

          default: {
              return false;
            }
        }
      }
    }

    // Implemented for IConnection
    public abstract bool Write(byte[] packet);

    /// <summary>
    /// Called after a stable connection is made and the services have been validated. This is were you want to register
    /// notifications and update any initial connection values. This is the final method that is called in the connection
    /// sequence that can terminate a connection. If you did not successfully perpare for a connection, return false.
    /// </summary>
    protected virtual bool OnConnectionSuccess() {
      return true;
    }

    /// <summary>
    /// Called during disconnect to handle implementation specific disconnect procedures.
    /// </summary>
    protected virtual void OnDisconnect() {
    }

    /// <summary>
    /// Called after a stable connection is made to the remote device and now we need to ensure that we have the
    /// correct services.
    /// </summary>
    /// <returns><c>true</c>, if services was validated, <c>false</c> otherwise.</returns>
    protected abstract bool ValidateServices();
  }
}
