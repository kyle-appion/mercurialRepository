namespace ION.Core.Connections {

  using System;

  /// <summary>
  /// The delegate that is notified when a connection's state is changed.
  /// </summary>
  /// <param name="connection"></param>
  /// <param name="state">The old state of the connection.</param>
  /// <returns></returns>
  public delegate void OnConnectionStateChanged(IConnection connection, EConnectionState oldState);
  /// <summary>
  /// The delegate that is notified when a connection received a new packet.
  /// </summary>
  /// <param name="connection"></param>
  /// <param name="packet"></param>
  /// <returns></returns>
  public delegate void OnDataReceived(IConnection connection, byte[] packet);
  /// <summary>
  /// IConnection is the contract that will wrap the physical connection between the
  /// application and a remote terminus.
  /// </summary>
  public interface IConnection {
    /// <summary>
    /// The event registry that will be notified when the connection's state changes.
    /// </summary>
    event OnConnectionStateChanged onStateChanged;
    /// <summary>
    /// The event registrt that will be notified when the connection receives a new
    /// data packet.
    /// </summary>
    event OnDataReceived onDataReceived;
    /// <summary>
    /// Queries the current state of the connection.
    /// </summary>
    EConnectionState connectionState { get; }
    /// <summary>
    /// Queries whether or not the connection is connected.
    /// </summary>
    /// <value><c>true</c> if is connected; otherwise, <c>false</c>.</value>
    bool isConnected { get; }
    /// <summary>
    /// Queries the name of the connection. For almost every device, this will be the
    /// device's serial number.
    /// </summary>
    string name { get; }
    /// <summary>
    /// Queries the unique address of the connection.
    /// </summary>
    /// <value>The address.</value>
    string address { get; }
    /// <summary>
    /// Queries the last packet that the connection received.
    /// </summary>
		byte[] lastPacket { get; set; }
    /// <summary>
    /// The last time that connection was seen- either through packet resolution or a scan.
    /// </summary>
    DateTime lastSeen { get; set; }
    /// <summary>
    /// Queries the native connection object that this connection is wrapping.
    /// </summary>
    object nativeDevice { get; }
    /// <summary>
    /// The timeout that is applied when connecting to the remote terminal.
    /// </summary>
    TimeSpan connectionTimeout { get; set; }

    /// <summary>
    /// Attempts to connect the connection's remote terminus.
    /// </summary>
    bool Connect();
    /// <summary>
    /// Disconnects the connection from the remote terminus.
    /// </summary>
    void Disconnect(bool reconnect=false);
    /// <summary>
    /// Writes the given packet out to the remote terminus.
    /// </summary>
    /// <returns></returns>
    bool Write(byte[] packet);
  }

  /// <summary>
  /// Enumerates the possible states that a connection can be in. The life cycle for EConnectionState is as follows:
  /// 
  /// Disconnected -> Connecting -> Resolving -> Connected
  ///     ^               |             |             |
  ///     |               |             |             |
  ///     +- Disconnect <-+-------------+-------------+
  /// </summary>
  public enum EConnectionState {
    /// <summary>
    /// The connection is not connected to the remote terminus. No communication is
    /// occurring in this state.
    /// </summary>
    Disconnected,
    /// <summary>
    /// The connection is not connected to the remote terminus. However, it is still
    /// receiving new data packets from the remote.
    /// </summary>
    Broadcasting,
    /// <summary>
    /// The connection is attempting to establish a connection with the remote terminus.
    /// Note: this is a pending action (meaning that it will happen in the indeterminate future).
    /// </summary>
    Connecting,
    /// <summary>
    /// A connection has been made, not the host and device are resolving any necessary details before communication
    /// goes live.
    /// </summary>
    Resolving,
    /// <summary>
    /// The connection is stable and communication may occur.
    /// </summary>
    Connected,
  }

  /// <summary>
  /// Enumerates the type of signal strength that a connection would have.
  /// </summary>
  public enum ESignalStrength {
    /// <summary>
    /// The signal strength from the connection is such that sponteneous disconnect is
    /// unlikely.
    /// </summary>
    Reliable,
    /// <summary>
    /// The recieved signal strength from the connection is good.
    /// </summary>
    Good,
    /// <summary>
    /// The received signal strength from the connection is moderate. Too much noise could
    /// cause the connection to terminate.
    /// </summary>
    Fair,
    /// <summary>
    /// The received signal strenth from the connection is such that it is expected that 
    /// the connection will terminate at any moment.
    /// </summary>
    Bad,
    /// <summary>
    /// The connection does not have a signal strength.
    /// </summary>
    None,
  }
}
