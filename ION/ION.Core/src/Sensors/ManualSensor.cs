
using ION.Core.App;
using ION.Core.Devices;

namespace ION.Core.Sensors
{

	using System;
	using Appion.Commons.Measure;

	/// <summary>
	/// A sensor that reprents a manually entered sensor value.
	/// </summary>
	public class ManualSensor : Sensor
	{
		/// <summary>
		/// Whether or not te sensor's reading is editable.
		/// </summary>
		/// <value>true</value>
		/// <c>false</c>
		public override bool isEditable
		{
			get
			{
				return true;
			}
		}

		/// <summary>
		/// The manual sensor device that own this sensor.
		/// </summary>
		public ManualSensorContainer container { get; private set; }

		public ManualSensor(ManualSensorContainer container, ESensorType sensorType, Scalar manualScalar, bool isRelative = true) : base(sensorType, manualScalar, isRelative)
		{
      this.container = container;
		}

		/// <summary>
		/// Creates a new ManualSensor bound to the given ion instance.
		/// </summary>
		/// <returns>The create.</returns>
		/// <param name="ion">Ion.</param>
		/// <param name="sensorType">Sensor type.</param>
		/// <param name="isRelative">If set to <c>true</c> is relative.</param>
		public static ManualSensor Create(IION ion, ESensorType sensorType, bool isRelative = true)
		{
			return ion.manualSensorContainer.CreateNewSensor(ion, sensorType, isRelative);
		}

		public void SetMeasurement(Scalar measurement)
		{
			this.measurement = measurement;
		}

		public void SetSupportedUnits(Unit[] supportedUnits)
		{
			this.supportedUnits = supportedUnits;
		}
	}
}
