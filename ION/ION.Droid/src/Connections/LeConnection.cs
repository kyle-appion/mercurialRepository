namespace ION.Droid.Connections {

	using System;

	using Android.Bluetooth;
	using Android.Content;
  using Android.OS;

	using Java.Util;

	using Appion.Commons.Util;

	// Using ION
	using Core.Connections;


  public class LeConnection : BluetoothGattCallback, IConnection, Handler.ICallback {
		/// <summary>
		/// A utility method used to create the UUIDs necessary for use for the
		/// bluetooth api.
		/// </summary>
		/// <param name="content">Content.</param>
		private static UUID Inflate(string content) {
			return UUID.FromString(BASE_UUID.Replace("****", content));
		}

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
		private static readonly UUID WRITE_CHARACTERISTIC = Inflate("ffe9");
    /// <summary>
    /// The delay from a disconnecto to a reconnect attempt.
    /// </summary>
    private const long PASSIVE_DELAY = 1000 * 30;
    /// <summary>
    /// The message that is used to start a passive connection attempt for the connection.
    /// </summary>
    private const int MSG_GO_PASSIVE = 1;

		public event OnConnectionStateChanged onStateChanged;

		public event OnDataReceived onDataReceived;

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

		public bool isConnected { get { return connectionState == EConnectionState.Connected; } }

		public string name { get { return device.Name; } }

		public string address { get { return device.Address; } }

		public ESignalStrength signalStrength { get; }

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

		public DateTime lastSeen { get; set; }

		public object nativeDevice { get { return device; } }

		public TimeSpan connectionTimeout { get; set; }

		/// <summary>
		/// The context that is needed to create modern bluetooth connections.
		/// </summary>
		private Context context;
		/// <summary>
		/// The manager that is necessary to glean details about a specific bluetooth device.
		/// </summary>
		private BluetoothManager manager;
		/// <summary>
		/// The android native bluetooth device that the connection is wrapping communications for.
		/// </summary>
		private BluetoothDevice device;
		/// <summary>
		/// The bluetooth gatt that the connection is using to interface with the bluetooth module.
		/// </summary>
		private BluetoothGatt gatt;
		/// <summary>
		/// The characteristic that is used to read from the bluetooth device.
		/// </summary>
		private BluetoothGattCharacteristic readCharacteristic;
		/// <summary>
		/// The characteristic that is used to write to the bluetooth device.
		/// </summary>
		private BluetoothGattCharacteristic writeCharacteristic;
    /// <summary>
    /// The object that is locked for multithread synchronization.
    /// </summary>
    private object locker = new object();
    /// <summary>
    /// The handler that is used to post delayed actions to.
    /// </summary>
    private Handler handler;

		public LeConnection(Context context, BluetoothManager manager, BluetoothDevice device) {
			this.context = context;
			this.manager = manager;
			this.device = device;
			connectionState = EConnectionState.Disconnected;
			lastSeen = DateTime.FromFileTimeUtc(0);
      handler = new Handler(Looper.MainLooper, this);
		}

    // Implemented from IConnection
    public bool Connect() {
      lock (locker) {
        if (gatt == null) {
          try {
            connectionState = EConnectionState.Connecting;
            gatt = device.ConnectGatt(context, false, this);
            handler.SendEmptyMessageDelayed(MSG_GO_PASSIVE, PASSIVE_DELAY);
            return true;
          } catch (Exception e) {
            Log.E(this, "Failed to start pending gatt connection.", e);
            return false;
          }
        } else {
          try {
            Disconnect(true);
            return true;
          } catch (Exception e) {
            Log.E(this, "Failed to attempt connect of pending gatt connection.", e);
            return false;
          }
        }
      }
    }

    // Implemented from IConnection
    public void Disconnect(bool reconnect=false) {
      lock (locker) {
        handler.RemoveMessages(MSG_GO_PASSIVE);

        readCharacteristic = null;
        writeCharacteristic = null;

        lastSeen = DateTime.Now;
        connectionState = EConnectionState.Disconnected;

        // Bounce out early as we have nothing to do.
        if (gatt == null) {
          return;
        }

        gatt.Disconnect();
        gatt.Close();
        gatt = null;

        if (reconnect) {
          handler.PostDelayed(() => Connect(), 1500);
        }
      }
    }

    // Implemented from IConnection
		public bool Write(byte[] packet) {
			if (!isConnected) {
				return false;
			}

			if (writeCharacteristic.SetValue(packet)) {
				return gatt.WriteCharacteristic(writeCharacteristic);
			} else {
				return false;
			}
		}

		public override void OnCharacteristicRead(BluetoothGatt gatt, BluetoothGattCharacteristic characteristic, GattStatus status) {
			if (characteristic.Equals(readCharacteristic)) {
				lastPacket = characteristic.GetValue();
			}
		}

		public override void OnCharacteristicChanged(BluetoothGatt gatt, BluetoothGattCharacteristic characteristic) {
			if (characteristic.Equals(readCharacteristic)) {
				lastPacket = characteristic.GetValue();
			}
		}

    public override void OnConnectionStateChange(BluetoothGatt gatt, GattStatus status, ProfileState newState) {
      Log.D(this, "The Rigado device is in state: " + newState);
      switch (newState) {
        case ProfileState.Connected: {
          handler.RemoveMessages(MSG_GO_PASSIVE);
          connectionState = EConnectionState.Resolving;
          lastSeen = DateTime.Now; // We at least know that the device is nearby at this point.

          if (!gatt.DiscoverServices()) {
            Log.E(this, "Failed to start service discovery for: " + device.Name);
            Disconnect();
          } else {
            connectionState = EConnectionState.Resolving;
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

		public override void OnServicesDiscovered(BluetoothGatt gatt, GattStatus status) {
			if (ValidateServices()) {
				if (!gatt.SetCharacteristicNotification(readCharacteristic, true)) {
					Log.E(this, "Failed to set rigado read characteristic to notify");
					Disconnect();
					return;
				}

				connectionState = EConnectionState.Connected;
			} else {
        Log.E(this, "Failed to discover services. Disconnecting device.");
        Disconnect();
/*
				connectionState = EConnectionState.Resolving;
				gatt.DiscoverServices();
*/
			}
		}

    // Implemented for Handler.ICallback
    public bool HandleMessage(Message msg) {
      switch (msg.What) {
        case MSG_GO_PASSIVE: {
          if (isConnected) {
            Disconnect(true);
          }

          return ConnectPassive();
        } // MSG_GO_PASSIVE

        default: {
          return false;
        }
      }
    }

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
          gatt = device.ConnectGatt(context, true, this);
          return true;
        } catch (Exception e) {
          Log.E(this, string.Format("Failed to start passive connect for device [{0}: {1}]", name, address), e);
          return false;
        }
      }
    }

		/// <summary>
		/// Checks the gatt object to see if we have successfully validated the services. If the gatt object is null, then
		/// obviously the services are not valid.
		/// </summary>
		/// <returns>The services.</returns>
		private bool ValidateServices() {
			if (gatt == null) {
				return false;
			}

			readCharacteristic = null;
			writeCharacteristic = null;

			var readService = gatt.GetService(READ_SERVICE);
			if (readService != null) {
				readCharacteristic = readService.GetCharacteristic(READ_CHARACTERISTIC);
			}

			var writeService = gatt.GetService(WRITE_SERVICE);
			if (writeService != null) {
				writeCharacteristic = writeService.GetCharacteristic(WRITE_CHARACTERISTIC);
			}

			return readCharacteristic != null && writeCharacteristic != null;
		}
	}
}

