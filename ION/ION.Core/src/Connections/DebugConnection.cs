#if DEBUG
namespace ION.Core.Connections {

  using System;

  /// <summary>
  /// A simple class that will simulate a connection for debug purposes.
  /// </summary>
  public class DebugConnection : IConnection {
    // Implemented for IConnection
    public event OnConnectionStateChanged onStateChanged;
    // Implemented for Connection
    public event OnDataReceived onDataReceived;

    // Implemented for Connection
    public EConnectionState connectionState {
      get {
        return _connectionState;
      }
      set {
        var old = _connectionState;
        _connectionState = value;
        if (onStateChanged != null) {
          onStateChanged(this, old);
        }
      }
    } EConnectionState _connectionState;

    // Implemented for Connection
    public bool isConnected { get { return connectionState == EConnectionState.Connected; } }
    // Implemented for Connection
    public string name { get; set; }
    // Implemented for Connection
    public string address { get; set; }
    // Implemented for Connection
    public byte[] lastPacket { get; set; }
    // Implemented for Connection
    public DateTime lastSeen { get; set; }
    // Implemented for Connection
    public object nativeDevice { get; set; }
    // Implemented for Connection
    public TimeSpan connectionTimeout { get; set; }

    public DebugConnection(string name, string address) {
      this.name = name;
      this.address = address;
      connectionState = EConnectionState.Disconnected;
      connectionTimeout = TimeSpan.FromSeconds(1);
    }

    // Implemented for Connection
    public bool Connect(bool passive=false) {
      lastSeen = DateTime.Now;
      connectionState = EConnectionState.Connected;
      return true;
    }

    // Implemented for Connection
    public void Disconnect(bool reconnect=false) {
      connectionState = EConnectionState.Disconnected;
    }

    // Implemented for Connection
    public bool Write(byte[] packet) {
      return false;
    }
  }
}
#endif
