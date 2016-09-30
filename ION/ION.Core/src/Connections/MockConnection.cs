namespace ION.Core.Connections {

  using System;
  using System.Threading.Tasks;

  /// <summary>
  /// A connection that does not do anything.
  /// </summary>
  public class MockConnection : IConnection {
    /// <summary>
    /// The address that is used to identify a mock address.
    /// </summary>
    public const string MOCK_ADDRESS = "MOCK_ADDRESS";
    /// <summary>
    /// Initializes a new instance of the <see cref="ION.Core.Connections.MockConnection"/> class.
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
			protected set {
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
				return connectionState == EConnectionState.Connected;
      }
    }
    /// <summary>
    /// Queries the name of the connection. For almost every device, this will be the
    /// device's serial number.
    /// </summary>
    /// <value>The name.</value>
		public string name { get; protected set; }
    /// <summary>
    /// Queries the unique address of the connection.
    /// </summary>
    /// <value>The address.</value>
    public string address {
      get {
        return MOCK_ADDRESS;
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
			protected set {
				__lastPacket = value;
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
    /// Queries the native connection object that this connection is wrapping.
    /// </summary>
    /// <value>The native device.</value>
    public object nativeDevice { get; private set; }
    /// <summary>
    /// The timeout that is applied when connecting to the remote terminal.
    /// </summary>
    /// <value>The connection timeout.</value>
    public TimeSpan connectionTimeout { get; set; }

		public MockConnection() : this(MOCK_ADDRESS) {
		}

		public MockConnection(string name) {
			this.name = name;
			connectionState = EConnectionState.Disconnected;
		}

    /// <summary>
    /// Attempts to connect the connection's remote terminus.
    /// </summary>
		public virtual Task<bool> ConnectAsync() {
			return Task.FromResult(false);
		}

    /// <summary>
    /// Disconnects the connection from the remote terminus.
    /// </summary>
		public virtual void Disconnect() {
		}
    /// <summary>
    /// Writes the given packet out to the remote terminus.
    /// </summary>
    /// <returns></returns>
    /// <param name="packet">Packet.</param>
		public virtual bool Write(byte[] packet) {
			return false;
		}
  }
}

