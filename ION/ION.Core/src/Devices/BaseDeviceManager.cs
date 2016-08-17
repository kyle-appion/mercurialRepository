namespace ION.Core.Devices {

  using System;
  using System.Collections.Generic;
  using System.Threading.Tasks;

  using ION.Core.App;
  using ION.Core.Connections;
  using ION.Core.Database;
  using ION.Core.Devices.Connections;
  using ION.Core.Devices.Protocols;
  using ION.Core.IO;
  using ION.Core.Sensors;
  using ION.Core.Util;

  /// <summary>
  /// Provides a standard implementation for the device manager. This device manager
  /// is meant to be as useful to as many platforms as possible.
  /// </summary>
  public class BaseDeviceManager : IDeviceManager {
    // Overridden from IDeviceManager
    public event OnDeviceManagerEvent onDeviceManagerEvent;

    /// <summary>
    /// The embedded resource name that holds appion's list of devices.
    /// </summary>
    private const string DEVICES_XML = "Devices.xml";

    public bool isInitialized { get { return __isInitialized; } } bool __isInitialized;

    // Overridden form IDeviceManager
    public IDevice this[ISerialNumber serialNumber] {
      get {
        IDevice ret = null;

        if (__knownDevices.ContainsKey(serialNumber)) {
          ret = __knownDevices[serialNumber];
        }

        if (ret == null && __foundDevices.ContainsKey(serialNumber)) {
          ret = __foundDevices[serialNumber];
        }

        return ret;
      }
    }

    // Overridden from IDeviceManager
    public List<IDevice> devices {
      get {
        var ret = new List<IDevice>();

        ret.AddRange(__knownDevices.Values);
        ret.AddRange(__foundDevices.Values);

        return ret;
      }
    }
    // Overridden from IDeviceManager
    public List<IDevice> knownDevices {
      get {
        var ret = new List<IDevice>();
        ret.AddRange(__knownDevices.Values);
        return ret;
      }
    }
    // Overridden from IDeviceManager
    public List<IDevice> foundDevices {
      get {
        var ret = new List<IDevice>();
        ret.AddRange(__foundDevices.Values);
        return ret;
      }
    }
    // Overridden from IDeviceManager
    public IConnectionFactory connectionFactory { get; set; }
    // Overridden from IDeviceManager
    public IConnectionHelper connectionHelper {
      get {
        return __connectionHelper;
      }
      set {
        if (__connectionHelper != null) {
          __connectionHelper.onDeviceFound -= OnDeviceFound;
          __connectionHelper.onScanStateChanged -= OnScanStateChanged;
        }

        __connectionHelper = value;

        if (__connectionHelper != null) {
          __connectionHelper.onDeviceFound += OnDeviceFound;
          __connectionHelper.onScanStateChanged += OnScanStateChanged;
        } else {
          throw new ArgumentException(this + " cannot accept a null IScanMode");
        }
      }
    } IConnectionHelper __connectionHelper;

    /// <summary>
    /// The ION instance for the device manager.
    /// </summary>
    /// <value>The ion.</value>
    private IION ion { get; set; }

    /// <summary>
    /// The dictionary of all the devices that are known (persisted) by the device manager.
    /// </summary>
    private Dictionary<ISerialNumber, IDevice> __knownDevices = new Dictionary<ISerialNumber, IDevice>();
    /// <summary>
    /// The dictionary of all the devices that have been found by the device manager's scanner.
    /// </summary>
    private Dictionary<ISerialNumber, IDevice> __foundDevices = new Dictionary<ISerialNumber, IDevice>();
    /// <summary>
    /// The factory that is used to create devices.
    /// </summary>
    public DeviceFactory __deviceFactory { get; internal set; }

    public BaseDeviceManager(IION ion, IConnectionFactory connectionFactory, IConnectionHelper connectionHelper) {
      this.ion = ion;
      this.connectionFactory = connectionFactory;
      this.connectionHelper = connectionHelper;
    }

    // Overridden from IDeviceManager
    public async Task<InitializationResult> InitAsync() {
    	Log.D(this,"regular device factory init");
      if (!connectionHelper.isEnabled) {
        if (!await connectionHelper.Enable()) {
          return new InitializationResult() {
            success = __isInitialized = false,
            errorMessage = "Failed to init device manager: failed to enable connection helper."
          };
        }
      }

      __deviceFactory = DeviceFactory.CreateFromStream(EmbeddedResource.Load(DEVICES_XML));
      if (__deviceFactory == null) {
        return new InitializationResult() {
          success = false,
          errorMessage = "Failed to init device manager: could not load device's database."
        };
      }

      try {
      	
        var devices = await ion.database.QueryForAllDevicesAsync();
        foreach (IDevice device in devices) {
          Register(device);
        } 
      } catch (Exception e) {
        Log.E(this, "Failed to load previous devices", e);
      }

      return new InitializationResult() { success = __isInitialized = true };
    }

    // Overridden from IDeviceManager
    public void Dispose() {
      __connectionHelper.onDeviceFound -= OnDeviceFound;
      __connectionHelper.onScanStateChanged -= OnScanStateChanged;

      foreach (var device in devices) {
        device.connection.Disconnect();
        Unregister(device);
      }

      __knownDevices.Clear();
      __foundDevices.Clear();
    }

    // Overridden from IDeviceManager
    public void ForgetFoundDevices() {
      lock (this) {
        foreach (var device in foundDevices) {
          device.Dispose();
          Unregister(device);
					device.onDeviceEvent -= OnDeviceEvent;
        }
      }
    }

    // Overridden from IDeviceManager
    public IDevice CreateDevice(ISerialNumber serialNumber, string connectionAddress, EProtocolVersion protocolVersion) {
      var ret = CreateDeviceInternal(serialNumber, connectionAddress, protocolVersion);
      // The register proved superfluous. Consider merging this functions with the internal one.
//      Register(ret);
      return ret;
    }

    /// <summary>
    /// Queries all of the device that are of the given type.
    /// </summary>
    /// <returns>The all devices of type.</returns>
    /// <param name="deviceType">Device type.</param>
    public List<IDevice> GetAllDevicesOfType(EDeviceType deviceType) {
      var ret = new List<IDevice>();

      foreach (var device in devices) {
        if (device.type == deviceType) {
          ret.Add(device);
        }
      }

      return ret;
    }

    /// <summary>
    /// Gets the type of the all gauge device sensors of.
    /// </summary>
    /// <returns>The all gauge devices of type.</returns>
    /// <param name="sensorType">Sensor type.</param>
    public List<GaugeDeviceSensor> GetAllGaugeDeviceSensorsOfType(ESensorType sensorType) {
      var ret = new List<GaugeDeviceSensor>();

      foreach (var device in devices) {
        var gd = device as GaugeDevice;
        if (gd != null) {
          foreach (var sensor in gd.sensors) {
            if (sensor.type == sensorType) {
              ret.Add(sensor);
            }
          }
        }
      }

      return ret;
    }

    /// <summary>
    /// Saves the given device to the database.
    /// </summary>
    /// <returns>The device.</returns>
    /// <param name="device">Device.</param>
    public async Task<bool> SaveDevice(IDevice device) {
      Log.D(this, "Attempting to save device");
      var d = await ion.database.DeconstructDevice(device);
      var ret = await ion.database.SaveAsync<DeviceRow>(d);
      Log.D(this, "Save device with id: " + d.DID);
      return ret;
    }

    // Overridden from IDeviceManager
    public async Task<bool> DeleteDevice(ISerialNumber serialNumber) {
      var device = this[serialNumber];
      if (device != null) {
        device.Dispose();
        Unregister(device);
        var db = ion.database;
        var deleted = await db.DeleteAsync<DeviceRow>(await db.DeconstructDevice(device));
        NotifyOfDeviceEvent(DeviceEvent.EType.Deleted, device);
        return true;
      }
      return false;
    }

    // Overridden from IDeviceManager
    public bool IsDeviceKnown(IDevice device) {
      var sb = new System.Text.StringBuilder();
      foreach (IDevice d in knownDevices) {
        sb.Append(d.serialNumber).Append(",");
      }
      return knownDevices.Contains(device);
    }

    /// <summary>
    /// Registers the device to the known device's mapping.
    /// </summary>
    /// <param name="device">Device.</param>
    public void Register(IDevice device) {
      __foundDevices.Remove(device.serialNumber);
      __knownDevices.Add(device.serialNumber, device);
			device.onDeviceEvent += OnDeviceEvent;
    }

    /// <summary>
    /// Unregisters the device from the device manager.
    /// </summary>
    /// <param name="device">Device.</param>
    public void Unregister(IDevice device) {
			__foundDevices[device.serialNumber] = device;
      __knownDevices.Remove(device.serialNumber);
    }

    private IDevice CreateDeviceInternal(ISerialNumber serialNumber, string connectionAddress, EProtocolVersion protocolVersion) {
      IDevice ret = this[serialNumber];

      if (ret == null) {
        var connection = connectionFactory.CreateConnection(connectionAddress, protocolVersion);

        var protocol = Protocol.FindProtocolFromVersion(protocolVersion);
        if (protocol == null) {
          protocol = Protocol.FindProtocolFromVersion(EProtocolVersion.V1);
        }
        var definition = __deviceFactory.GetDeviceDefinition(serialNumber);
        ret = definition.CreateDevice(serialNumber, connection, protocol);
        ret.onDeviceEvent += OnDeviceEvent;
      } else {
        if (!ret.connection.address.Equals(connectionAddress)) {
          var msg = BuildErrorHeader(serialNumber, protocolVersion) + ": a device already exists with address " +
            ret.connection.address + " but a new device creation request was made for address " + connectionAddress;
          Log.C(this, msg);
          throw new Exception(msg);
        }
      }

      if (ret == null) {
        var msg = BuildErrorHeader(serialNumber, protocolVersion) +
          ": Please ensure that the serial number is resgistered in ION.Core.Devices.Devices.xml";
        Log.C(this, msg);
        throw new Exception(msg);
      }

      return ret;
    }

    /// <summary>
    /// Posts a new DeviceManagerEvent to the onDeviceManagerEvent handler. This is posted to the main
    /// thread.
    /// </summary>
    /// <param name="type">Type.</param>
    /// <param name="dtype">Dtype.</param>
    /// <param name="device">Device.</param>
    private void NotifyOfDeviceManagerEvent(DeviceManagerEvent.EType type) {
      NotifyOfDeviceManagerEvent(new DeviceManagerEvent(type, this));
    }

    /// <summary>
    /// Posts a new DeviceManagerEvent of type DeviceEvent to the onDeviceManager handler. This is posted
    /// to the main thread.
    /// </summary>
    /// <param name="type">Type.</param>
    /// <param name="device">Device.</param>
    private void NotifyOfDeviceEvent(DeviceEvent.EType type, IDevice device) {
      NotifyOfDeviceEvent(new DeviceEvent(type, device));
    }

    /// <summary>
    /// Posts a new DeviceManagerEvent of type DeviceEvent to the onDeviceManager handler. This is posted
    /// to the main thread.
    /// </summary>
    /// <param name="type">Type.</param>
    /// <param name="device">Device.</param>
    private void NotifyOfDeviceEvent(DeviceEvent deviceEvent) {
      NotifyOfDeviceManagerEvent(new DeviceManagerEvent(this, deviceEvent));
    }

    /// <summary>
    /// Posts a new DeviceManager event to the device manager handler.
    /// </summary>
    /// <param name="dme">Dme.</param>
    private void NotifyOfDeviceManagerEvent(DeviceManagerEvent dme) {
      if (onDeviceManagerEvent != null) {
        ion.PostToMain(() => {
          onDeviceManagerEvent(dme);
        });
      }
    }

    /// <summary>
    /// The delegate that is called when a device is found by the device manager's scan mode.
    /// </summary>
    /// <param name="device">Device.</param>
    private void OnDeviceFound(IConnectionHelper scanner, ISerialNumber serialNumber, string address, byte[] packet, EProtocolVersion protocol) {
      var device = this[serialNumber];

      if (device == null) {
        device = CreateDeviceInternal(serialNumber, address, protocol);
        __foundDevices[serialNumber] = device;
      }

      if (device.protocol is IGaugeProtocol) {
        var gp = device.protocol as IGaugeProtocol;
        if (gp.supportsBroadcasting) {
          device.HandlePacket(packet);
        }
      }
      device.connection.lastSeen = DateTime.Now;

      NotifyOfDeviceEvent(DeviceEvent.EType.Found, device);
    }

    /// <summary>
    /// Called when a device known by the device manager posts a device event.
    /// </summary>
    /// <param name="deviceEvent">Device event.</param>
    private async void OnDeviceEvent(DeviceEvent deviceEvent) {
      var device = deviceEvent.device;

      switch (deviceEvent.type) {
        case DeviceEvent.EType.ConnectionChange:
          if (!IsDeviceKnown(device)) {
            Register(device);
          }

          if (device.isConnected) {
            await SaveDevice(device);
          }
          break;
      }

      NotifyOfDeviceEvent(deviceEvent);
    }

    /// <summary>
    /// Called when the connection helper changes state.
    /// </summary>
    /// <param name="connectionHelper">Connection helper.</param>
    private void OnScanStateChanged(IConnectionHelper connectionHelper) {
      if (connectionHelper.isScanning) {
        NotifyOfDeviceManagerEvent(DeviceManagerEvent.EType.ScanStarted);
      } else {
        NotifyOfDeviceManagerEvent(DeviceManagerEvent.EType.ScanStopped);
      }
    }

    /// <summary>
    /// Builds a string that can be used as an error header for the logger.
    /// </summary>
    /// <returns>The error header.</returns>
    /// <param name="serialNumber">Serial number.</param>
    /// <param name="protocol">Protocol.</param>
    private string BuildErrorHeader(ISerialNumber serialNumber, EProtocolVersion protocol) {
      return "Failed to create device from serial number: " + serialNumber + ", Protocol: " + protocol;
    }
  }
}

