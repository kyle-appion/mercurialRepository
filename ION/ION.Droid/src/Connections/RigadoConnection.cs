namespace ION.Droid.Connections {

	using System;
	using System.Collections.Generic;

	using Android.Bluetooth;
	using Android.Content;
  using Android.OS;

	using Java.Util;

	using Appion.Commons.Util;

	// Using ION
	using Core.Connections;

  public class RigadoConnection : BluetoothGattCallback, IConnection, Handler.ICallback {
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
		/// The default time that is allowed for a rigado connection attempt.
		/// </summary>
		private static readonly TimeSpan DEFAULT_TIMEOUT = TimeSpan.FromSeconds(45);
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
				Log.D(this, "Connection state is : " + value);
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
		/// The descriptor of the read characterisitic. This is necessary to set notify.
		/// </summary>
		private BluetoothGattDescriptor readCharacteristicDescriptor;
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

		public RigadoConnection(Context context, BluetoothManager manager, BluetoothDevice device) {
			this.context = context;
			this.manager = manager;
			this.device = device;
			connectionState = EConnectionState.Disconnected;
			connectionTimeout = DEFAULT_TIMEOUT;
			lastSeen = DateTime.FromFileTimeUtc(0);
      handler = new Handler(Looper.MainLooper, this);
		}

    // Implemented from IConnection
    public bool Connect() {
      lock (locker) {
        if (gatt == null) {
          try {
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

				if (!readCharacteristicDescriptor.SetValue(new List<byte>(BluetoothGattDescriptor.EnableNotificationValue).ToArray())) {
					Log.E(this, "Failed to set notification to read descriptor");
					Disconnect();
					return;
				}

				if (!gatt.WriteDescriptor(readCharacteristicDescriptor)) {
					Log.E(this, "Failed to write read notification descriptor");
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

			var baseService = gatt.GetService(BASE_SERVICE);
			if (baseService != null) {
				readCharacteristic = baseService.GetCharacteristic(READ_CHARACTERISTIC);

				if (readCharacteristic != null) {
					readCharacteristicDescriptor = readCharacteristic.GetDescriptor(READ_CHARACTERISTIC_DESCRIPTOR);
				}

				writeCharacteristic = baseService.GetCharacteristic(WRITE_CHARACTERISTIC);
			}

			return readCharacteristic != null && readCharacteristicDescriptor != null && writeCharacteristic != null;
		}
	}
}

