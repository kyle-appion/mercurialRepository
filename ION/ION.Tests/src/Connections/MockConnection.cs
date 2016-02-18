namespace ION.TestFixtures.Connections {

  using System;
  using System.Threading.Tasks;

  using ION.Core.Connections;

  public class MockConnection : IConnection {
    private static int INSTANCE_COUNT = 0;

    private readonly int id = ++INSTANCE_COUNT;

    // Overridden from IConnection
    public event OnConnectionStateChanged onStateChanged;
    // Overridden from IConnection
    public event OnDataReceived onDataReceived;

    // Overridden from IConnection
    public EConnectionState connectionState {
      get {
        return __connectionState;
      }
      set {
        __connectionState = value;
        if (onStateChanged != null) {
          onStateChanged(this, connectionState);
        }
      }
    } EConnectionState __connectionState;
    // Overridden from IConnection
    public string name { get { return "MockConnection: " + id; } }
    // Overridden from IConnection
    public string address { get { return string.Format("{0:0000}", id); } }
    public ESignalStrength signalStrength { get { return ESignalStrength.Fair; } }
    // Overridden from IConnection
    public int rssi { get { return 0; } }
    // Overridden from IConnection
    public bool isRssiReliable { get { return false; } }
    // Overridden from IConnection
    public byte[] lastPacket {
      get {
        return __lastPacket;
      }
      set {
        __lastPacket = value;
        if (onDataReceived != null) {
          onDataReceived(this, __lastPacket);
        }
      }
    } byte[] __lastPacket;
    // Overridden from IConnection
    public DateTime lastSeen { get; set; }
    // Overridden from IConnection
    public object nativeDevice { get { return null; } }
    // Overridden from IConnection
    public TimeSpan connectionTimeout { get; set; }

    // Overridden from IConnection
    public Task<bool> Connect() {
      return Task.Factory.StartNew(() => {
        connectionState = EConnectionState.Connected;
        return true;
      });
    }

    // Overridden from IConnection
    public void Disconnect() {
      connectionState = EConnectionState.Disconnected;
    }

    // Overridden from IConnection
    public Task<bool> Write(byte[] packet) {
      return Task.Factory.StartNew(() => {
        lastPacket = packet;
        return true;
      });
    }
  }
}
