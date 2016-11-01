namespace ION.Core.Report.DataLogs {

	using System;
	using System.Collections.Generic;

	using ION.Core.App;
	using ION.Core.Database;
	using ION.Core.Devices;
	using ION.Core.Measure;

	public class DeviceSensorGaugeReportData {
		/// <summary>
		/// The serial number of the device.
		/// </summary>
		/// <value>The sensor.</value>
		public ISerialNumber serialNumber { get; private set; }
		/// <summary>
		/// The index of the sensor within the device.
		/// </summary>
		/// <value>The index of the sensor.</value>
		public int sensorIndex { get; private set; }
		/// <summary>
		/// The date that the data starts at.
		/// </summary>
		/// <value>The start date.</value>
		public DateTime startDate { get; private set; }
		/// <summary>
		/// The date that the data ends at.
		/// </summary>
		public DateTime endDate { get; private set; }
		/// <summary>
		/// The list containing the lists of measurement sessions that make up the report.
		/// </summary>
		/// <value>The measurement data.</value>
		public List<SessionData> measurementData { get; private set; }

		public DeviceSensorGaugeReportData() {
		}


		/// <summary>
		/// </summary>
		/// <param name="ion">Ion.</param>
		public static DeviceSensorGaugeReportData QueryFromSessionOrThrow(IION ion, long sessionId, ISerialNumber serialNumber, int index) {
			var def = ion.deviceManager.deviceFactory.GetDeviceDefinition(serialNumber);
			if (def == null) {
				throw new Exception("Cannot query sensor data for " + serialNumber + ": failed to find device definition");
			}

			var gaugeDef = def as GaugeDeviceDefinition;
			if (gaugeDef == null) {
				throw new Exception("Cannot query sensor data for " + serialNumber + ": serial number returned an invalid device definition");
			}

			var sensorDef = gaugeDef[index];
			if (sensorDef == null) {
				throw new Exception("Cannot query sensor data for " + serialNumber + ": serial number returned a device definition with invalid sensor definition");
			}

			var table = ion.database.Table<SensorMeasurementRow>();
			var rawSerialNumber = serialNumber.ToString();
			var rows = table.Where(smr => smr.frn_SID == sessionId && smr.serialNumber.Equals(rawSerialNumber) && smr.sensorIndex == index)
			     .OrderBy(smr => smr.recordedDate);

			var measurements = new List<MeasurementData>();
			foreach (var row in rows) {
				measurements.Add(new MeasurementData() {
					measurement = sensorDef.minimumMeasurement.unit.standardUnit.OfScalar(row.measurement),
					recordedDate = row.recordedDate,
				});
			}

			return null;
		}

		public struct SessionData {
			/// <summary>
			/// The id of the session that this data was retrived from.
			/// </summary>
			public long sessionId;
			/// <summary>
			/// The list of measurements that the sensor recorded.
			/// </summary>
			public List<MeasurementData> measurement;
			/// <summary>
			/// The date that the data started recording.
			/// </summary>
			public DateTime startDate;
			/// <summary>
			/// The date that the data finished recording.
			/// </summary>
			public DateTime endDate;
		}

		public struct MeasurementData {
			public Scalar measurement;
			public DateTime recordedDate;
		}
	}
}
