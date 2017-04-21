namespace ION.Core.Devices {

	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	using Appion.Commons.Util;

	using ION.Core.App;
	using ION.Core.IO;

	using ION.Core.Connections;
	using ION.Core.Devices.Protocols;
	using ION.Core.Sensors;

	public class RemoteDeviceManager : IDeviceManager {
		// Implemented from IDeviceManager
		public event OnDeviceManagerEvent onDeviceManagerEvent;

		/// <summary>
		/// The embedded resource name that holds appion's list of devices.
		/// </summary>
		private const string DEVICES_XML = "Devices.xml";

		// Implemented from IDeviceManager
		public IDevice this[ISerialNumber serialNumber] {
			get {
				if (serialNumber != null && __devices.ContainsKey(serialNumber)) {
					return __devices[serialNumber];
				} else {
					return null;
				}
			}
		} 

		// Implemented from IDeviceManager
		public List<IDevice> devices {
			get {
				return new List<IDevice>(__devices.Values);
			}
		} Dictionary<ISerialNumber, IDevice> __devices = new Dictionary<ISerialNumber, IDevice>();

		// Implemented from IDeviceManager
		public List<IDevice> knownDevices { get { return devices; } }
		// Implemented from IDeviceManager
		public List<IDevice> foundDevices { get { return new List<IDevice>(); } }
		// Implemented from IDeviceManager

		// Implemented from IDeviceManager
		public IConnectionManager connectionManager { get; set; }

		// Implemented from IDeviceManager
		public DeviceFactory deviceFactory { get; set; }

		public bool isInitialized { get { return true; } }

		private IION ion;

		public RemoteDeviceManager(IION ion) {
			this.ion = ion;
			this.connectionManager = new RemoteConnectionManager();
		}

		// Implemented from IDeviceManager
		public Task<InitializationResult> InitAsync() {
			Log.D(this, "loading the base device manager");
			try {
				deviceFactory = DeviceFactory.CreateFromStream(EmbeddedResource.Load(DEVICES_XML));
			} catch (Exception e) {
				Log.E(this, "Failed from device factor", e);
			}
			Log.D(this, "device factory set");
			if (deviceFactory == null) {
				return Task.FromResult(new InitializationResult() {
					success = false,
					errorMessage = "Failed to init device manager: could not load device database.",
				});
			}

			return Task.FromResult(new InitializationResult() { success = true });
		}

    // Implemented from IDeviceManager
    public void PostInit() {
    }

		// Implemented from IDeviceManager
		public void Dispose() {
		}

		// Implemented from IDeviceManager
		public void ForgetFoundDevices() {
		}

		// Implemented from IDeviceManager
		public IDevice CreateDevice(ISerialNumber serialNumber, string connectionAddress, EProtocolVersion protocol) {
			var p = Protocol.FindProtocolFromVersion(protocol);
			return deviceFactory.GetDeviceDefinition(serialNumber).CreateDevice(serialNumber, new RemoteConnection(serialNumber.ToString()), p);
		}

		// Implemented from IDeviceManager
		public List<IDevice> GetAllDevicesOfType(EDeviceType deviceType) {
			var ret = new List<IDevice>();

			foreach (var device in __devices.Values) {
				if (device.type == deviceType) {
					ret.Add(device);
				}
			}

			return ret;
		}

		// Implemented from IDeviceManager
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

		// Implemented from IDeviceManager
		public Task<bool> SaveDevice(IDevice device) {
			return Task.FromResult(false);
		}

		// Implemented from IDeviceManager
		public Task<bool> DeleteDevice(ISerialNumber serialNumber) {
			return Task.FromResult(false);
		}

		// Implemented from IDeviceManager
		public bool IsDeviceKnown(IDevice device) {
			return __devices.ContainsValue(device);
		}

		// Implemented from IDeviceManager
		public void Register(IDevice device) {
			if (!IsDeviceKnown(device)) {
				__devices[device.serialNumber] = device;
				device.onDeviceEvent += OnDeviceEvent;
				NotifyOfDeviceEvent(DeviceEvent.EType.Found, device);
			}
		}

		// Implemented from IDeviceManager
		public void Unregister(IDevice device) {
			device.onDeviceEvent -= OnDeviceEvent;
			__devices.Remove(device.serialNumber);
			NotifyOfDeviceEvent(DeviceEvent.EType.Deleted, device);
		}

		/// <summary>
		/// Called when a device known by the device manager posts a device event.
		/// </summary>
		/// <param name="deviceEvent">Device event.</param>
		private void OnDeviceEvent(DeviceEvent deviceEvent) {
			var device = deviceEvent.device;
			NotifyOfDeviceEvent(deviceEvent);
		}

		/// Posts a new DeviceManagerEvent to the onDeviceManagerEvent handler. This is posted to the main
		/// thread.
		/// </summary>
		/// <param name="type">Type.</param>
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
					try {
						onDeviceManagerEvent(dme);
					} catch (Exception e) {
						Log.E(this, "Failed to post device manager event", e);
					}
				});
			}
		}
	}
}
