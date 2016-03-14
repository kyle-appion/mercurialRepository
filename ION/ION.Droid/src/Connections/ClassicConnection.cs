namespace ION.Droid.Connections {

  using System;
  using System.IO;
  using System.Threading;
  using System.Threading.Tasks;
  using System.Timers;

  using Android.Bluetooth;
  using Android.Content;
  using Android.Media;

  using Java.Util;

  using ION.Core.Connections;
  using ION.Core.Devices;
  using ION.Core.Devices.Protocols;
  using ION.Core.Util;


  public class ClassicConnection : IConnection {

    /// <summary>
    /// The bytes that make up the request packet.
    /// </summary>
    private static byte[] REQUEST_DPT = System.Text.Encoding.UTF8.GetBytes("1 REQUEST DPT\r\n");
    /// <summary>
    /// The profile id that we are using for bluetooth communications.
    /// </summary>
    private static UUID SPP = UUID.FromString("00001101-0000-1000-8000-00805F9B34FB");

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
        Log.D(this, "Last Packet: " + System.Text.UTF8Encoding.UTF8.GetString(value));
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
    public object nativeDevice { get { return device as BluetoothDevice; } }
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
    /// The timer that will request new packets periodically while connected.
    /// </summary>
    private System.Timers.Timer timer;
    /// <summary>
    /// The thread that the actual socket connection runs on.
    /// </summary>
    private Thread connectThread;

    public ClassicConnection(BluetoothDevice device) {
      this.device = device;
      connectionTimeout = TimeSpan.FromSeconds(45);
    }

    /// <summary>
    /// Releases all resource used by the <see cref="ION.Droid.Connections.ClassicConnection"/> object.
    /// </summary>
    /// <remarks>Call <see cref="Dispose"/> when you are finished using the
    /// <see cref="ION.Droid.Connections.ClassicConnection"/>. The <see cref="Dispose"/> method leaves the
    /// <see cref="ION.Droid.Connections.ClassicConnection"/> in an unusable state. After calling <see cref="Dispose"/>,
    /// you must release all references to the <see cref="ION.Droid.Connections.ClassicConnection"/> so the garbage
    /// collector can reclaim the memory that the <see cref="ION.Droid.Connections.ClassicConnection"/> was occupying.</remarks>
    public void Dispose() {
      if (EConnectionState.Disconnected != connectionState) {
        Disconnect();
      }
    }

    /// <summary>
    /// Attempts to connect the connection's remote terminus.
    /// </summary>
    public async Task<bool> Connect() {
      Log.D(this, "connect");

      if (EConnectionState.Connected == connectionState) {
        return true;
      } else if (EConnectionState.Disconnected != connectionState) {
        Log.D(this, "Connection not in a disconnected state: returing as failed.");
        return false;
      }

      connectionState = EConnectionState.Connecting;

      if (!await ConnectInternal()) {
        Disconnect();
        return false;
      }

      timer = new System.Timers.Timer(150);
      timer.Elapsed += DoTimerRequest;
      timer.Start();

      connectionState = EConnectionState.Connected;

      return true;
    }

    /// <summary>
    /// Disconnects the connection from the remote terminus.
    /// </summary>
    public void Disconnect() {
      Log.D(this, "Disconnect");

      if (connectThread != null) {
        connectThread.Abort();
      }
      connectThread = null;

      if (input != null) {
        input.Close();
      }
      input = null;


      if (output != null) {
        output.Close();
      }
      output = null;

      if (timer != null) {
        timer.Stop();
        timer = null;
      }

      if (socket != null) {
        socket.Close();
      }
      socket = null;

      connectionState = EConnectionState.Disconnected;
    }

    /// <summary>
    /// Attempts to get the serial number from the connection.
    /// </summary>
    /// <returns>The serial number.</returns>
    public async Task<ISerialNumber> RequestSerialNumber() {
      if (__lastPacket != null) {
        Log.D(this, "REQUEST SERIAL NUMBER: " + System.Text.UTF8Encoding.UTF8.GetString(__lastPacket));
      }
      GaugeSerialNumber serialNumber = null;

      if (__lastPacket != null) {
        if (ClassicProtocol.ParseSerialNumber(lastPacket, out serialNumber)) {
          return serialNumber;
        }
      }

      if (await Connect()) {
        if (await RequestAsync()) {
          Disconnect();
          if (ClassicProtocol.ParseSerialNumber(lastPacket, out serialNumber)) {
            return serialNumber;
          } else {
            throw new Exception("Failed to parse serial number");
          }
        } else {
          throw new Exception("Failed to request serial number");
        }
      } else {
        throw new Exception("Failed to connect and request serial number");
      }
    }

    /// <summary>
    /// Writes the given packet out to the remote terminus.
    /// </summary>
    /// <returns></returns>
    /// <param name="data">Data.</param>
    public async Task<bool> Write(byte[] data) {
      try {
        if (EConnectionState.Connected != connectionState) {
          return false;
        }

        output.Write(data, 0, data.Length);
        output.Flush();

        return true;
      } catch (Exception e) {
        Log.E(this, "Failed to write data: " + data, e);
        return false;
      }
    }

    /// <summary>
    /// The internal connect method used by the backend.
    /// </summary>
    private async Task<bool> ConnectInternal() {
      Log.D(this, "connect internal");
      DateTime start = DateTime.Now;

      socket = device.CreateInsecureRfcommSocketToServiceRecord(SPP);

      connectThread = new Thread(() => {
        socket.Connect();
      });
      connectThread.Start();

      while (!socket.IsConnected) {
        if (DateTime.Now - start > connectionTimeout) {
          Log.E(this, "Failed to connect: timedout");
          Disconnect();
          return false;
        } else {
          await Task.Delay(50);
        }
      }

      input = new StreamReader(socket.InputStream);
      output = socket.OutputStream;

      return true;
    }

    private async void DoTimerRequest(object obj, System.Timers.ElapsedEventArgs args) {
      if (EConnectionState.Connected == connectionState) {
        await RequestAsync();
      } else {
        timer.Elapsed -= DoTimerRequest;
      }
    }

    /// <summary>
    /// Writes a request dpt packet out to the remote device and awaits a response. If the packet is valid, the last
    /// packet will be set.
    /// </summary>
    /// <returns><c>true</c>, if async was writed, <c>false</c> otherwise.</returns>
    /// <param name="data">Data.</param>
    private async Task<bool> RequestAsync() {
      try {
        if (EConnectionState.Connected != connectionState) {
          return false;
        }

        if (await Write(REQUEST_DPT)) {

          var packet = input.ReadLine();

          lastPacket = System.Text.UTF8Encoding.UTF8.GetBytes(packet);

          return true;
        } else {
          return false;
        }
      } catch (Exception e) {
        //        Log.E(this, "Failed to request async", e);
        return false;
      }
    }
  }
}

