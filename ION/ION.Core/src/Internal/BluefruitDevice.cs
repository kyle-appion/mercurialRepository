namespace ION.Core.Internal {

  using System;
  using System.IO;

  using ION.Core.App;
  using ION.Core.Connections;
  using ION.Core.Devices;
  using ION.Core.Devices.Protocols;
  using ION.Core.IO;
	using ION.Core.Measure;
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
    public IProtocol protocol { 
			get {
				return blueProtocol;
			}
		} 

		public BluefruitProtocol blueProtocol;
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
		/// Whether or not the device is resetting.
		/// </summary>
		/// <value>The is resetting.</value>
		public bool isResetting { get; private set; }

    /// <summary>
    /// The current degree angle of the bluefruit's stepper.
    /// </summary>
    /// <value>The current angle.</value>
    public float currentAngle { get; private set; }

    /// <summary>
    /// The stepper representtion of the remote stepper 1.
    /// </summary>
		public Stepper inputStepper { get; internal set; }

    /// <summary>
    /// Gets or sets the exhaust stepper.
    /// </summary>
    /// <value>The exhaust stepper.</value>
    public Stepper exhaustStepper { get; internal set; }
		public int lastVrcMeasurement { get; internal set; }
    /// <summary>
    /// The vrc micron measurement.
    /// </summary>
    /// <value>The vrc measurement.</value>
    public int currentVrcMeasurement {
			get {
				return __currentVrcMeasurement;
			}
			internal set {
				lastVrcMeasurement = __currentVrcMeasurement;
				__currentVrcMeasurement = value;
			}
		} int __currentVrcMeasurement;


		public BluefruitDevice(BluefruitSerialNumber serialNumber, IConnection connection, BluefruitProtocol protocol) {
      this.serialNumber = serialNumber;
      this.connection = connection;
			blueProtocol = protocol;
      this.name = serialNumber.ToString();

      connection.onStateChanged += ((IConnection conn, EConnectionState state) => {
        NotifyOfDeviceEvent(DeviceEvent.EType.ConnectionChange);
      });

      connection.onDataReceived += ((IConnection conn, byte[] packet) => {
        HandlePacket(packet);
      });

      inputStepper = new Stepper();
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
				var bp = blueProtocol.Parse(packet);
				isResetting = bp.isResetting;
				if (!isResetting) {
					inputStepper.currentAngle = Units.Angle.RADIAN.OfScalar(bp.currentInputAngle);
					inputStepper.targetAngle = Units.Angle.RADIAN.OfScalar(bp.targetInputAngle);
					inputStepper.rps = bp.inputRPS;
					inputStepper.atHome = bp.inputAtHome;
					inputStepper.atEnd = bp.inputAtEnd;

					exhaustStepper.currentAngle = Units.Angle.RADIAN.OfScalar(bp.currentExhaustAngle);
					exhaustStepper.targetAngle = Units.Angle.RADIAN.OfScalar(bp.targetExhaustAngle);
					exhaustStepper.rps = bp.exhaustRPS;
					exhaustStepper.atHome = bp.exhaustAtHome;
					exhaustStepper.atEnd = bp.exhaustAtEnd;

					currentVrcMeasurement = bp.vrc;
				}

        NotifyOfDeviceEvent(DeviceEvent.EType.NewData);
      } catch (Exception e) {
        Log.E(this, "Failed to resolve packet", e);
      }
    }

    /// <summary>
    /// Notifies the device's onStateChange delegates that it has changed.
    /// </summary>
    private void NotifyOfDeviceEvent(DeviceEvent.EType t) {
      try {
        var ion = AppState.context;
        if (ion != null) {
          ion.PostToMain(() => {
            if (onDeviceEvent != null) {
              onDeviceEvent(new DeviceEvent(t, this));
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
    /// The current rotation of the stepper.
    /// </summary>
    /// <value>The rotation.</value>
    public Scalar currentAngle { get; internal set; }
		/// <summary>
		/// The target angle of the stepper motor.
		/// </summary>
		/// <value>The target angle.</value>
		public Scalar targetAngle { get; internal set; }
		/// <summary>
		/// The current rotations per second for the stepper.
		/// </summary>
		/// <value>The rps.</value>
		public ESpeed rps { get; internal set; }
		/// <summary>
		/// Whether or not the start switch is pressed.
		/// </summary>
		/// <value>At home.</value>
		public bool atHome { get; internal set; }
		/// <summary>
		/// Whether or not the end switch is pressed.
		/// </summary>
		/// <value>At end.</value>
		public bool atEnd { get; internal set; }
    /// <summary>
    /// Whether or not the stepper is moving.
    /// </summary>
    /// <value><c>true</c> if is moving; otherwise, <c>false</c>.</value>
    public bool isMoving { 
      get {
				return currentAngle != targetAngle;
      }
    }
  }
}

