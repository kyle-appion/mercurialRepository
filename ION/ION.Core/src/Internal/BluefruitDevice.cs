namespace ION.Core.Internal {

  using System;
  using System.IO;

  using ION.Core.App;
  using ION.Core.Connections;
  using ION.Core.Devices;
  using ION.Core.Devices.Protocols;
  using ION.Core.IO;
  using ION.Core.Util;

  public class BluefruitDevice : IDevice {
    /// <summary>
    /// The event registery that will be notified when the device's
    /// connection state changes
    /// </summary>
    public event OnDeviceEvent onDeviceEvent;

    /// <summary>
    /// Queries the serial number of the device.
    /// </summary>
    /// <value>The serial number.</value>
    public ISerialNumber serialNumber { get; private set; }
    /// <summary>
    /// Queries the device type for this device.
    /// </summary>
    /// <value>The type.</value>
    public EDeviceType type {
      get {
        return EDeviceType.InternalInterface;
      }
    }
    /// <summary>
    /// Queries the name of the device.
    /// </summary>
    /// <value>The name.</value>
    public string name { get; set; }
    /// <summary>
    /// The current battery level (0-100] for the device.
    /// </summary>
    /// <value>The battery.</value>
    public int battery { get; set; }
    /// <summary>
    /// Queries the connection responsible for communicating with the device's
    /// remote terminus.
    /// </summary>
    /// <value>The connection.</value>
    public IConnection connection { get; private set; }
    /// <summary>
    /// The protocol that is used by the device.
    /// </summary>
    /// <value>The protocol.</value>
    public IProtocol protocol { get; private set; }
    /// <summary>
    /// Queries whether or not the device is connected.
    /// </summary>
    /// <value><c>true</c> if is connected; otherwise, <c>false</c>.</value>
    public bool isConnected { 
      get {
        return EConnectionState.Connected == connection.connectionState;
      }
    }
    /// <summary>
    /// Queries whether or not the device is considered nearby.
    /// </summary>
    /// <value><c>true</c> if is nearby; otherwise, <c>false</c>.</value>
    public bool isNearby {
      get {
        return EConnectionState.Broadcasting == connection.connectionState;
      }
    }

    /// <summary>
    /// The current degree angle of the bluefruit's stepper.
    /// </summary>
    /// <value>The current angle.</value>
    public float currentAngle { get; private set; }

    /// <summary>
    /// The stepper representtion of the remote stepper 1.
    /// </summary>
    public Stepper controlStepper { get; internal set; }

    /// <summary>
    /// Gets or sets the exhaust stepper.
    /// </summary>
    /// <value>The exhaust stepper.</value>
    public Stepper exhaustStepper { get; internal set; }
    /// <summary>
    /// The vrc micron measurement.
    /// </summary>
    /// <value>The vrc measurement.</value>
    public float vrcMeasurement { get; internal set; }


    public BluefruitDevice(BluefruitSerialNumber serialNumber, IConnection connection, BluefruitProtocol protcol) {
      this.serialNumber = serialNumber;
      this.connection = connection;
      this.protocol = new BluefruitProtocol();
      this.name = serialNumber.ToString();

      connection.onStateChanged += ((IConnection conn, EConnectionState state) => {
        NotifyOfDeviceEvent(DeviceEvent.EType.ConnectionChange);
      });

      connection.onDataReceived += ((IConnection conn, byte[] packet) => {
        HandlePacket(packet);
      });

      controlStepper = new Stepper();
      exhaustStepper = new Stepper();
    }

    /// <summary>
    /// Releases all resource used by the <see cref="ION.Core.Devices.GaugeTestInterfaceDevice"/> object.
    /// </summary>
    /// <remarks>Call <see cref="Dispose"/> when you are finished using the
    /// <see cref="ION.Core.Devices.GaugeTestInterfaceDevice"/>. The <see cref="Dispose"/> method leaves the
    /// <see cref="ION.Core.Devices.GaugeTestInterfaceDevice"/> in an unusable state. After calling
    /// <see cref="Dispose"/>, you must release all references to the
    /// <see cref="ION.Core.Devices.GaugeTestInterfaceDevice"/> so the garbage collector can reclaim the memory that the
    /// <see cref="ION.Core.Devices.GaugeTestInterfaceDevice"/> was occupying.</remarks>
    public void Dispose() {
      connection.Disconnect();
    }

    /// <Docs>To be added.</Docs>
    /// <para>Returns the sort order of the current instance compared to the specified object.</para>
    /// <summary>
    /// Compares to.
    /// </summary>
    /// <returns>The to.</returns>
    /// <param name="other">Other.</param>
    public int CompareTo(IDevice other) {
      return serialNumber.CompareTo(other.serialNumber);
    }

    /// <summary>
    /// Informs the device that is should hanldle the given packet.
    /// </summary>
    /// <param name="packet">Packet.</param>
    public void HandlePacket(byte[] packet) {
      try {
        packet = CharacterBytesToRawBytes(packet);
        Log.D(this, "Packet is " + packet.ToByteString());
        using (var reader = new BinaryReader(new MemoryStream(packet))) {
          controlStepper.state = (Stepper.EState)reader.ReadByte();
          controlStepper.rotation = reader.ReadSingle();

          exhaustStepper.state = (Stepper.EState)reader.ReadByte();
          exhaustStepper.rotation = reader.ReadSingle();

          this.vrcMeasurement = reader.ReadUInt32();

        }

        NotifyOfDeviceEvent(DeviceEvent.EType.NewData);
      } catch (Exception e) {
        Log.E(this, "Failed to resolve packet", e);
      }
    }

    /// <summary>
    /// Converts a character byte array into a raw 0 indexed byte array.
    /// </summary>
    /// <returns>The bytes to raw bytes.</returns>
    /// <param name="chars">Chars.</param>
    private byte[] CharacterBytesToRawBytes(byte[] chars) {
      var ret = new byte[chars.Length / 2];

      for (int i = 0, k = 0; i < chars.Length; i += 2, k++) {
        byte highNibble = CharToNibble((char)chars[i]);
        byte lowNibble = CharToNibble((char)chars[i + 1]);

        if (highNibble == 0xff) {
          throw new Exception("Could not parse high nibble: " + chars[i]);
        }

        if (lowNibble == 0xff) {
          throw new Exception("Could not parse low nibble: " + chars[i + 1]);
        }

        ret[k] = (byte)(highNibble * 16 + lowNibble);
      }

      return ret;
    }

    /// <summary>
    /// Reads the given character as a nibble.
    /// </summary>
    /// <returns>The to nibble.</returns>
    /// <param name="ch">Ch.</param>
    private byte CharToNibble(char ch) {
      if (ch >= '0' && ch <= '9') {
        return (byte)(ch - '0');
      } else if (ch >= 'a' && ch <= 'f') {
        return (byte)(ch - 'a');
      } else if (ch >= 'A' && ch <= 'F') {
        return (byte)(ch - 'A');
      } else {
        return 0xff;
      }
    }

    /// <summary>
    /// Notifies the device's onStateChange delegates that it has changed.
    /// </summary>
    private void NotifyOfDeviceEvent(DeviceEvent.EType type) {
      try {
        var ion = AppState.context;
        if (ion != null) {
          ion.PostToMain(() => {
            if (onDeviceEvent != null) {
              onDeviceEvent(new DeviceEvent(type, this));
            }
          });
        }
      } catch (Exception e) {
        Log.E(this, "FAILED TO POST DEVICE EVENT TO MAIN THREAD!!!!", e);
      }
    }
  }

  public class Stepper {
    /// <summary>
    /// The curret state of the stepper.
    /// </summary>
    public EState state { get; internal set; }
    /// <summary>
    /// The current rotation of the stepper.
    /// </summary>
    /// <value>The rotation.</value>
    public float rotation { get; internal set; }
    /// <summary>
    /// Whether or not the stepper is moving.
    /// </summary>
    /// <value><c>true</c> if is moving; otherwise, <c>false</c>.</value>
    public bool isMoving { 
      get {
        return (state & EState.Moving) == EState.Moving;
      }
    }

    /// <summary>
    /// The enumeration of the states that the bluefruit can be in.
    /// </summary>
    [Flags]
    public enum EState {
      Home = 1,
      Moving = 2,
    }    
  }
}

