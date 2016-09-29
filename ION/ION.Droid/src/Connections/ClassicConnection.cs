namespace ION.Droid.Connections {

  using System;
  using System.IO;
  using System.Threading.Tasks;

  using Android.Bluetooth;
	using Android.OS;

  using Java.Util;

  using ION.Core.Connections;
  using ION.Core.Devices;
  using ION.Core.Devices.Protocols;
  using ION.Core.Util;

	public class ClassicConnection : Java.Lang.Object, IConnection, Handler.ICallback {
    /// <summary>
    /// The bytes that make up the request packet.
    /// </summary>
    private static byte[] DPT = System.Text.Encoding.UTF8.GetBytes("1 REQUEST DPT\r\n");
    /// <summary>
    /// The profile id that we are using for bluetooth communications.
    /// </summary>
    private static UUID SPP = UUID.FromString("00001101-0000-1000-8000-00805F9B34FB");

		private const int RECONNECT_DELAY = 750;

		private const int MSG_REQUEST_PACKET = 1;
		private const int MSG_RECONNECT = 2;

    /// <summary>
    /// The event registry that will be notified when the connection's state changes.
    /// </summary>
    public event OnConnectionStateChanged onStateChanged;
    /// <summary>
    /// The event registrt that will be notified when the connection receives a new
    /// data packet.
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
    public string address { get { return device.Address; } }
    /// <summary>
    /// Queries the current received signal strength of the connection.
    /// </summary>
    /// <value>The signal strength.</value>
    public ESignalStrength signalStrength {
      get {
        if (EConnectionState.Connected == connectionState) {
          return ESignalStrength.Fair;
        } else {
          return ESignalStrength.None;
        }
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
          onDataReceived(this, value);
        }
      }
    } byte[] __lastPacket;
    /// <summary>
    /// The last time that connection was seen- either through packet resolution or a scan.
    /// </summary>
    /// <value>The last seen.</value>
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
    /// The android native device.
    /// </summary>
    private BluetoothDevice device;
    /// <summary>
    /// The socket that represents our connection.
    /// </summary>
    private BluetoothSocket socket;
    /// <summary>
    /// The input stream for the connection.
    /// </summary>
    private StreamReader input;
    /// <summary>
    /// The output stream for the connection.
    /// </summary>
    private System.IO.Stream output;
		/// <summary>
		/// The handler that the connection will place its pending connection actions to.
		/// </summary>
		private Handler handler;

    public ClassicConnection(BluetoothDevice device) {
      this.device = device;
      connectionTimeout = TimeSpan.FromSeconds(45);
			handler = new Handler(Looper.MainLooper, this);
    }

    /// <summary>
    /// Attempts to connect the connection's remote terminus.
    /// </summary>
    /// <returns>The async.</returns>
    public Task<bool> ConnectAsync() {
      return Task.Factory.StartNew(() => {
        if (EConnectionState.Connected == connectionState) {
          return true;
        } else if (EConnectionState.Disconnected != connectionState) {
          Log.D(this, "Connection not in a disconnected state: returing as failed.");
          return false;
        }

        connectionState = EConnectionState.Connecting;

				if (!ConnectInternal()) {
					Task.Delay(5000).Wait();

					if (!ConnectInternal()) {
	          Disconnect();
	          return false;
	        }
				}

        connectionState = EConnectionState.Connected;

        DoRequestPacket();

        return true;
      });
    }

    /// <summary>
    /// Disconnects the connection from the remote terminus.
    /// </summary>
    public void Disconnect() {
			handler.RemoveCallbacksAndMessages(null);

			Log.D(this, "Disconnected");
      if (input != null) {
        input.Close();
        input.Dispose();
        input = null;
      }

      if (output != null) {
        output.Close();
        output.Dispose();
        output = null;
      }

      if (socket != null) {
        socket.Close();
        socket.Dispose();
        socket = null;
      }

      connectionState = EConnectionState.Disconnected;
    }

    /// <summary>
    /// Writes the given packet out to the remote terminus.
    /// </summary>
    /// <returns></returns>
    /// <param name="data">Data.</param>
    public bool Write(byte[] data) {
      return false;
    }

    /// <summary>
    /// Attempts to resolve the serial number of the connection.
    /// </summary>
    /// <returns>The serial number.</returns>
    public static Task<GaugeSerialNumber> ResolveSerialNumber(BluetoothDevice device) {
			return Task.Factory.StartNew(() => {
				var connection = new ClassicConnection(device);
	      try {
	        if (connection.ConnectInternal()) {
	          GaugeSerialNumber ret = null;
	          int tries = 5;

	          while (ret == null && tries-- > 0) {
	            var packet = connection.RequestPacket().Result;

	            ClassicProtocol.ParseSerialNumber(System.Text.Encoding.UTF8.GetBytes(packet), out ret);
	          }

	          return ret;
	        } else {
	          Log.E(connection, "Failed to resolve serial number: failed to connect.");
	          return null;
	        }
	      } finally {
	        connection.Disconnect();
	      }
			});
    }

		public bool HandleMessage(Message msg) {
			switch (msg.What) {
				case MSG_REQUEST_PACKET:
					DoRequestPacket();
					return true;
				case MSG_RECONNECT:
					ConnectAsync();
					return true;
			}

			return false;
		}

    /// <summary>
    /// Connects to the device's bluetooth socket.
    /// </summary>
    /// <returns>The to socket.</returns>
    private bool ConnectInternal() {
			Disconnect();
      try {
        socket = device.CreateInsecureRfcommSocketToServiceRecord(SPP);
        socket.Connect();
        input = new StreamReader(socket.InputStream);
        output = socket.OutputStream;
        return true;
      } catch (Exception e) {
        Log.E(this, "Failed to connect to the class device", e);
        return false;
      }
    }

    /// <summary>
    /// Requests a single packet from the remote device.
    /// </summary>
    /// <returns>The packet.</returns>
    private Task<string> RequestPacket() {
      return Task.Factory.StartNew(() => {
        try {
          output.Write(DPT, 0, DPT.Length);
          output.Flush();

          var ret = input.ReadLine();
					Log.D(this, "Ret: " + ret);
					return ret;
        } catch (Exception e) {
          Log.E(this, "Classic connection crash on write.", e);
          return null;
        }
      });
    }

    /// <summary>
    /// Performs a timer event that will request a new packet.
    /// </summary>
    private async void DoRequestPacket() {
      if (EConnectionState.Connected == connectionState) {
        var requestTask = RequestPacket();
        if (await Task.WhenAny(requestTask, Task.Delay(TimeSpan.FromMilliseconds(5000))) == requestTask) {
					handler.SendMessageDelayed(handler.ObtainMessage(MSG_REQUEST_PACKET), 100);
          if (requestTask.Result != null) {
            lastPacket = System.Text.Encoding.UTF8.GetBytes(requestTask.Result);
					} else {
						if (requestTask.IsFaulted) {
							if (socket.IsConnected) {
								Log.D(this, "Classic connection failed to resolve packet. Disconnecting");
								Disconnect();
							} else {
								Disconnect();
								handler.SendEmptyMessageDelayed(MSG_RECONNECT, 750);
							}
						}
          }
        } else {
          // We failed to get a packet in the timeout timespan
          Log.D(this, "Timeout on packet read. Classic connection disconnected");
          Disconnect();
        }
      }
    }
  }
}