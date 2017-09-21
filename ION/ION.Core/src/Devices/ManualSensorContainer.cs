using System.Collections.Generic;

using ION.Core.App;
using ION.Core.Connections;
using ION.Core.Devices.Protocols;
using ION.Core.Sensors;

namespace ION.Core.Devices
{

	/// <summary>
	/// A MaualDevice is a device that is intended to store manual sensors such that they will persist in a predictable
	/// way.
	/// </summary>
	public class ManualSensorContainer
	{
		/*
			public event OnDeviceEvent onDeviceEvent;

			// Implemented for IDevice
			public ISerialNumber serialNumber { get { return null; } }
			// Implemented for IDevice
			public EDeviceType type { get { return EDeviceType.Unknown; } }
			// Implemented for IDevice
			public string name { get { return "ManualDevice"; } set {} }
			// Implemented for IDevice
			public int battery { get { return -1; } }
			// Implemented for IDevice
			public IConnection connection { get { return null; } }
			// Implemented for IDevice
			public IProtocol protocol { get { return null; } }
			// Implemented for IDevice
			public bool isConnected { get { return false; } }
			// Implemented for IDevice
			public bool isNearby { get { return false; } }
		*/

		/// <summary>
		/// Gets the <see cref="T:ION.Core.Devices.ManualSensorDevice"/> at the specified index.
		/// </summary>
		/// <param name="index">Index.</param>
		public ManualSensor this[int index]
		{
			get
			{
				return _sensors[index];
			}
		}

		/// <summary>
		/// The number of sensors that are present in the container.
		/// </summary>
		public int sensorCount { get { return _sensors.Count; } }

		/// <summary>
		/// The list of manual sensors that are present in the device.
		/// </summary>
		private List<ManualSensor> _sensors = new List<ManualSensor>();

		public ManualSensorContainer()
		{
		}

		public void Dispose()
		{
		}

		/// <summary>
		/// Creates a new manual sensor within this device. The sensor will last until explicitly destroyed.
		/// </summary>
		/// <param name="ion"></param>
		/// <param name="sensorType"></param>
		/// <returns></returns>
		public ManualSensor CreateNewSensor(IION ion, ESensorType sensorType, bool isRelative = true)
		{
      //var u = ion.preferences.units.DefaultUnitFor(sensorType);
      //var ret = new ManualSensor(this, sensorType, u.OfScalar(0), isRelative);
      //return ret;
      return null;
		}

		/// <summary>
		/// Removes the sensor from this device.
		/// </summary>
		/// <param name="sensor"></param>
		public void DestroySensor(Sensor sensor)
		{
			_sensors.Remove(sensor as ManualSensor);
		}
		/*
			// Implemented for IDevice
			public void HandlePacket(byte[] packet) {
			}

			public int CompareTo(IDevice device) {
			  return 0;
			}
		*/
	}
}
