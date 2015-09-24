using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using ION.Core.App;
using ION.Core.Devices.Connections;
using ION.Core.Devices.Protocols;
using ION.Core.IO;
using ION.Core.Util;

namespace ION.Core.Devices {
  /// <summary>
  /// Provides a standard implementation for the device manager. This device manager
  /// is meant to be as useful to as many platforms as possible.
  /// </summary>
  public class BaseDeviceManager : IDeviceManager {
    // Overridden from IDeviceManager
    public event OnDeviceFound onDeviceFound;
    // Overridden from IDeviceManager
    public event OnDeviceStateChanged onDeviceStateChanged;
    // Overridden from IDeviceManager
    public event OnDeviceManagerStatesChanged onDeviceManagerStatesChanged;

    /// <summary>
    /// The embedded resource name that holds appion's list of devices.
    /// </summary>
    private const string DEVICES_XML = "Devices.xml";

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
    public EDeviceManagerStates states {
      get {
        EDeviceManagerStates ret = EDeviceManagerStates.None;

        if (connectionHelper.isEnabled) {
          ret |= EDeviceManagerStates.Enabled;
        }

        if (connectionHelper.isScanning) {
          ret |= EDeviceManagerStates.Scanning;
        }


        return ret;
      }
    }
    // Overridden from IDeviceManager
    public DeviceFactoryDelegate deviceFactoryDelegate { get; protected set; }
    // Overridden from IDeviceManager
    public IConnectionHelper connectionHelper {
      get {
        return __connectionHelper;
      }
      protected set {
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
    private DeviceFactory __deviceFactory;

    public BaseDeviceManager(IION ion, IConnectionHelper connectionHelper) {
      this.ion = ion;
      this.connectionHelper = connectionHelper;
    }

    // Overridden from IDeviceManager
    public async Task<InitializationResult> InitAsync() {
      if (!connectionHelper.isEnabled) {
        if (!connectionHelper.Enable()) {
          return new InitializationResult() {
            success = false,
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
        var devices = await ion.database.deviceDao.QueryForAllAsync();
        foreach (IDevice device in devices) {
          Register(device);
        } 
      } catch (Exception e) {
        Log.E(this, "Failed to load previous devices", e);
      }

      return new InitializationResult() { success = true };
    }

    // Overridden from IDeviceManager
    public void Dispose() {
      __connectionHelper.onDeviceFound -= OnDeviceFound;

      foreach (var device in devices) {
        Unregister(device);
      }

      __knownDevices.Clear();
      __foundDevices.Clear();
    }

    // Overridden from IDeviceManager
    public void ForgetFoundDevices() {
      lock (this) {
        foreach (var device in foundDevices) {
          Unregister(device);
        }
      }
    }

    // Overridden from IDeviceManager
    public IDevice CreateDevice(ISerialNumber serialNumber, string connectionAddress, int protocolVersion) {
      var ret = CreateDeviceInternal(serialNumber, connectionAddress, protocolVersion);
      // The register proved superfluous. Consider merging this functions with the internal one.
//      Register(ret);
      return ret;
    }

    // Overridden from IDeviceManager
    public bool IsDeviceKnown(IDevice device) {
      var sb = new System.Text.StringBuilder();
      foreach (IDevice d in knownDevices) {
        sb.Append(d.serialNumber).Append(",");
      }
      Log.D(this, "Looking for: " + device.serialNumber + " in known devices: " + sb);
      return knownDevices.Contains(device);
    }

    private IDevice CreateDeviceInternal(ISerialNumber serialNumber, string connectionAddress, int protocolVersion) {
      IDevice ret = this[serialNumber];

      if (ret == null) {
        var connection = connectionHelper.CreateConnectionFor(connectionAddress);
        var protocol = Protocol.FindProtocolFromVersion(protocolVersion);
        if (protocol == null) {
          protocol = Protocol.PROTOCOLS[0];
        }
        ret = __deviceFactory.CreateDeviceFromSerialNumber(serialNumber, connection, protocol);
        ret.onStateChanged += OnDeviceStateChanged;
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
    /// Registers the device to the known device's mapping.
    /// </summary>
    /// <param name="device">Device.</param>
    private void Register(IDevice device) {
      __foundDevices.Remove(device.serialNumber);
      __knownDevices.Add(device.serialNumber, device);
    }

    /// <summary>
    /// Unregisters the device from the device manager.
    /// </summary>
    /// <param name="device">Device.</param>
    private void Unregister(IDevice device) {
      __foundDevices.Remove(device.serialNumber);
      __knownDevices.Remove(device.serialNumber);
      device.onStateChanged -= OnDeviceStateChanged;
    }

    /// <summary>
    /// The delegate that is called when a device is found by the device manager's scan mode.
    /// </summary>
    /// <param name="device">Device.</param>
    private void OnDeviceFound(IConnectionHelper scanner, ISerialNumber serialNumber, string address, byte[] packet, int protocol) {
      var device = this[serialNumber];

      if (device == null) {
        device = CreateDeviceInternal(serialNumber, address, protocol);
        __foundDevices[serialNumber] = device;
      }

      if (device.protocol.supportsBroadcasting) {
        device.HandlePacket(packet);
      }
      device.connection.lastSeen = DateTime.Now;

      if (onDeviceFound != null) {
        onDeviceFound(this, device);
      }
    }

    /// <summary>
    /// Called when a device created by this device manager's state changes.
    /// </summary>
    /// <param name="device">Device.</param>
    private async void OnDeviceStateChanged(IDevice device) {
      if (!IsDeviceKnown(device)) {
        Register(device);
      }
      await ion.database.deviceDao.SaveAsync(device);
      ion.PostToMain(() => {
        if (onDeviceStateChanged != null) {
          onDeviceStateChanged(device);
        }
      });
    }

    /// <summary>
    /// Called when the connection helper changes state.
    /// </summary>
    /// <param name="connectionHelper">Connection helper.</param>
    private void OnScanStateChanged(IConnectionHelper connectionHelper) {
      ion.PostToMain(() => {
        if (onDeviceManagerStatesChanged != null) {
          onDeviceManagerStatesChanged(this);
        }
      });
    }

    /// <summary>
    /// Builds a string that can be used as an error header for the logger.
    /// </summary>
    /// <returns>The error header.</returns>
    /// <param name="serialNumber">Serial number.</param>
    /// <param name="protocol">Protocol.</param>
    private string BuildErrorHeader(ISerialNumber serialNumber, int protocol) {
      return "Failed to create device from serial number: " + serialNumber + ", Protocol: " + protocol;
    }
  }
}

