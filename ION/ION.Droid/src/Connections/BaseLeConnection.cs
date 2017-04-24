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
    /// <summary>
    /// The delay from a disconnecto to a reconnect attempt.
    /// </summary>
    private const long PASSIVE_DELAY = 1000 * 30;
    /// <summary>
    /// The delay before the connection disconnects because the service scan timed out.
    /// </summary>
    private const long SERVICES_DELAY = 1000 * 20;
    /// <summary>
    /// The message that is used to start a passive connection attempt for the connection.
    /// </summary>
    private const int MSG_GO_PASSIVE = 1;
    /// <summary>
    /// The message that is used to check whether or not the connection has successfully found its services.
    /// </summary>
    private const int MSG_CHECK_SERVICES = 2;

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
        var oldState = __connectionState;
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
    protected BluetoothGatt gatt;

    /// <summary>
    /// The object that is locked for multithread synchronization.
    /// </summary>
    private object locker = new object();
    /// <summary>
    /// The handler that is used to post delayed actions to.
    /// </summary>
    private Handler handler;

    public BaseLeConnection(AndroidConnectionManager manager, BluetoothDevice device) {
      this.manager = manager;
      this.device = device;
      connectionState = EConnectionState.Disconnected;
      connectionTimeout = DEFAULT_TIMEOUT;
      lastSeen = DateTime.FromFileTimeUtc(0);
      handler = new Handler(Looper.MainLooper, this);
    }

    // Implemented for IConnection
    public bool Connect() {
      lock (locker) {
        if (isConnected) {
          return true;
        } else {
          if (connectionState != EConnectionState.Disconnected) {
            Disconnect(false);
          }
          try {
            Log.D(this, "Device {" + name + "} is performing an active connection");
            gatt = device.ConnectGatt(manager.context, false, this);
            handler.SendEmptyMessageDelayed(MSG_GO_PASSIVE, PASSIVE_DELAY);
            return true;
          } catch (Exception e) {
            Log.E(this, "Failed to start pending gatt connection.", e);
            return false;
          }
        }
      }
    }

    // Implemented for IConnection
    public void Disconnect(bool reconnect=false) {
      lock (locker) {
        handler.RemoveCallbacksAndMessages(null);

        OnDisconnect();

        lastSeen = DateTime.Now;
        connectionState = EConnectionState.Disconnected;

        if (gatt != null) {
          gatt.Disconnect();
          gatt.Close();
          gatt = null;
        }

        if (reconnect) {
          handler.PostDelayed(() => Connect(), 1500);
        }
      }
    }

    // Overridden from BluetoothGattCallback
    public sealed override void OnConnectionStateChange(BluetoothGatt gatt, GattStatus status, ProfileState newState) {
      lock (locker) {
        Log.D(this, "The Le device is in state: " + newState);
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
              Disconnect(true);
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
          case MSG_GO_PASSIVE: {
            if (isConnected) {
              Disconnect(false);
            }

            return ConnectPassive();
          } // MSG_GO_PASSIVE

          case MSG_CHECK_SERVICES: {
            if (!ValidateServices()) {
              Log.E(this, "Failed to validate services; disconnecting with intention of an immediate reconnect");
              Disconnect(false);
            }
            return true;
          } // MSG_CHECK_SERVICES

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

    /// <summary>
    /// Performs a passive connect - meaning that the connection will be established in the furture at an indeterminate
    /// time when the platform walks into range of the device.
    /// </summary>
    /// <returns><c>true</c>, if passive was connected, <c>false</c> otherwise.</returns>
    private bool ConnectPassive() {
      lock (locker) {
        if (isConnected) {
          return true;
        }

        if (gatt != null) {
          Disconnect();
        }

        try {
          Log.D(this, "Device {" + name + "} is performing a passive connection");
          gatt = device.ConnectGatt(manager.context, true, this);
          return true;
        } catch (Exception e) {
          Log.E(this, string.Format("Failed to start passive connect for device [{0}: {1}]", name, address), e);
          return false;
        }
      }
    }
  }
}
