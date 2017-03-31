namespace ION.Droid.Report {

	using System;
	using System.Collections.Generic;
	using System.Linq;

	using Android.Content;

	using FlexCel.Core;
	using FlexCel.XlsAdapter;

	using Appion.Commons.Measure;
	using Appion.Commons.Util;

	using ION.Core.App;
	using ION.Core.Database;
	using ION.Core.Devices;
	using ION.Core.Report.DataLogs;
	using ION.Core.Sensors;

	using ION.Droid.Util;

	public class DataLogExcelReportExporter {

		private const int SHEET_COUNT = 2;
		private const int SHEET_SESSION_RESULTS = 1;
		private const int SHEET_JOB_INFO = 2;

		/// <summary>
		/// The android context needed for writing the file.
		/// </summary>
		private Context context;
		/// <summary>
		/// The ion instance that is driving the background for the exporter.
		/// </summary>
		private IION ion;
		/// <summary>
		/// The filepath that we are exporting to.
		/// </summary>
		private string filepath;
		/// <summary>
		/// The report that we are exporting.
		/// </summary>
		private DataLogReport dlr;
		/// <summary>
		/// The excel file that we will write the data to.
		/// </summary>
		private XlsFile file;

		private int formatHeader;
		private int formatContent;
		private int formatBorder;

		public DataLogExcelReportExporter(Context context, IION ion, string filepath, DataLogReport dlr) {
			this.context = context;
			this.ion = ion;
			this.filepath = filepath;
			this.dlr = dlr;

			file = new XlsFile(SHEET_COUNT, TExcelFileFormat.v2013, true);
			file.AllowOverwritingFiles = true;

			// The format that is used to render the header cells.
			var headerFormat = file.GetDefaultFormat;
			headerFormat.VAlignment = TVFlxAlignment.top;
			headerFormat.HAlignment = THFlxAlignment.center;
			headerFormat.Font.Color = TExcelColor.FromIndex(2);
			headerFormat.FillPattern = new TFlxFillPattern {
				Pattern = TFlxPatternStyle.Solid,
				FgColor = TExcelColor.FromIndex(1),
			};

			// The format that is used to render the content cells.
			var contentFormat = file.GetDefaultFormat;
			contentFormat.VAlignment = TVFlxAlignment.top;
			contentFormat.HAlignment = THFlxAlignment.center;

			// The format that is applied fo the border of cells.
			var borderFormat = file.GetDefaultFormat;
			borderFormat.Borders.Bottom.Color = TUIColor.FromArgb(0xFF, 0x33, 0x33);
			borderFormat.Borders.Bottom.Style = TFlxBorderStyle.Medium;
			borderFormat.VAlignment = TVFlxAlignment.top;
			borderFormat.HAlignment = THFlxAlignment.center;

			// The order that the formats are added to the file is the 1-based index that is passed to SetCellValue as the cell
			// format.

			formatHeader = file.AddFormat(headerFormat);   // 1
			formatContent = file.AddFormat(contentFormat);  // 2
			formatBorder = file.AddFormat(borderFormat);   // 3
		}

		/// <summary>
		/// Commits the report to its file.
		/// </summary>
		public bool Commit() {
			try {
				RenderSessionResults();
				RenderCoverSheet();

				file.Save(filepath);
				file.Save(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "A Lovely Filename.xlsx"));

				return true;
			} catch (Exception e) {
				Log.E(this, "Failed to export data log to excel.", e);
				return false;
			}
		}

		private void RenderCoverSheet() {
			file.ActiveSheet = SHEET_JOB_INFO;

			// We will render the device details first
			var curRow = 1;

			// Write the block title
			file.SetCellValue(1, 2, context.GetString(Resource.String.report_devices_used), formatContent);
			// Move to the next row
			curRow++; 

			// Render the block headers
			file.SetCellValue(2, 1, context.GetString(Resource.String.device_serial_number), formatHeader);
			file.SetCellValue(2, 2, context.GetString(Resource.String.name), formatHeader);
			file.SetCellValue(2, 3, context.GetString(Resource.String.report_nist_date), formatHeader);
			// Move to the next row
			curRow++; 

			// Render the device details
			for (int i = 0; i < dlr.devices.Count; i++) {
				var device = dlr.devices[i];

				var sn = device.serialNumber.ToString();
				var ldr = ion.database.Table<LoggingDeviceRow>().Where(r => r.serialNumber.Equals(sn)).FirstOrDefault();
				string date = "N/A";
				DateTime dt;
				if (!DateTime.TryParse(ldr.nistDate, out dt)) {
					Log.D(this, "Failed to parse date time: '" + ldr.nistDate + "'");
				}

				file.SetCellValue(curRow, 1, device.serialNumber.ToString());
				file.SetCellValue(curRow, 2, device.name);
				file.SetCellValue(curRow, 3, date);
				// Move to the next row
				curRow++; 
			}

			// Add an row for padding
			curRow++; 

			// Add the footer
			file.SetCellValue(curRow, 1, context.GetString(Resource.String.report_created), formatHeader);
			file.SetCellValue(curRow, 2, DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());

      // Move to the next row
      curRow++;

			// Add the dates created
			file.SetCellValue(curRow, 1, "", formatHeader);
			file.SetCellValue(curRow, 2, context.GetString(Resource.String.report_dates), formatHeader);
			file.SetCellValue(curRow, 3, "", formatHeader);

			// Move to the next row
			curRow++; 

			file.SetCellValue(curRow, 2, dlr.start.ToFullShortString() + " - " + dlr.end.ToFullShortString());


			// Render the jobs details

			if (dlr.jobs.Count <= 0) {
				file.SetCellValue(1, 5, context.GetString(Resource.String.report_spans_no_jobs), formatHeader);
			} else if (dlr.jobs.Count == 1) {
				file.SetCellValue(1, 5, context.GetString(Resource.String.report_job_details));
				var job = dlr.jobs.First();

				file.SetCellValue(1, 6, context.GetString(Resource.String.report_job_details), formatHeader);
				file.SetCellValue(2, 5, context.GetString(Resource.String.job_name), formatHeader);
				file.SetCellValue(3, 5, context.GetString(Resource.String.job_customer_no), formatHeader);
				file.SetCellValue(4, 5, context.GetString(Resource.String.job_dispatch_no), formatHeader);
				file.SetCellValue(5, 5, context.GetString(Resource.String.job_purchase_no), formatHeader);

				file.SetCellValue(2, 5, job.jobName, formatContent);
				file.SetCellValue(3, 5, job.customerNumber, formatContent);
				file.SetCellValue(4, 5, job.dispatchNumber, formatContent);
				file.SetCellValue(5, 5, job.poNumber, formatContent);
			} else {
				file.SetCellValue(1, 5, context.GetString(Resource.String.report_spans_multiple_jobs), formatHeader);
			}

			// Add a couple of rows as padding, we are going to add some sensor meta data
			curRow += 2;

			// Render the serial number meta data
			// Add the header
			file.SetCellValue(curRow, 2, context.GetString(Resource.String.device_serial_number), formatHeader);
			file.SetCellValue(curRow, 3, context.GetString(Resource.String.minimum), formatHeader);
			file.SetCellValue(curRow, 4, context.GetString(Resource.String.maximum), formatHeader);
			file.SetCellValue(curRow, 5, context.GetString(Resource.String.average), formatHeader);

			// Move to next row
			curRow++;

			foreach (var device in dlr.devices) {
			}

			// Autofit
			file.AutofitCol(1, 6, false, 1.1);
		}

		/// <summary>
		/// Renders the session results to a new sheet in the excel.
		/// </summary>
		private void RenderSessionResults() {
			file.ActiveSheet = SHEET_SESSION_RESULTS;

			var unsortedMasterDates = new HashSet<DateTime>();

			var masterDates = new List<DateTime>();
			var sessionBreaks = new HashSet<DateTime>();
			var measurements = new Dictionary<string, Measurements>();

			foreach (var sr in dlr.sessionResults) {
				foreach (var dsl in sr.deviceSensorLogs) {
					var sensor = GetSensor(ion, dsl.deviceSerialNumber, dsl.index);

					Measurements m;

					if (!measurements.TryGetValue(dsl.deviceSerialNumber, out m)) {
						m = new Measurements() {
							sensor = sensor,
							serialNumber = dsl.deviceSerialNumber,
							sensorIndex = dsl.index,
						};
						measurements[dsl.deviceSerialNumber] = m;
					}

					foreach (var sl in dsl.logs) {
						unsortedMasterDates.Add(sl.recordedDate);
						m.measurements[sl.recordedDate] = sl.measurement;
					}
					sessionBreaks.Add(dsl.end);
				}
			}

			masterDates.AddRange(unsortedMasterDates);
			masterDates.Sort();

			// Draw header
			file.SetCellValue(1, 1, "" + context.GetString(Resource.String.time), 1);
			var i = 0;
			foreach (var key in measurements.Keys) {
				file.SetCellValue(1, 2 + i++, "" + GetHeaderStringFromSensor(ion, measurements[key].sensor), 1);
			}

			// Draw the master dates list to the file.
			for (i = 0; i < masterDates.Count; i++) {
				var date = masterDates[i];
				var format = sessionBreaks.Contains(date) ? 3 : 2;
				file.SetCellValue(2 + i, 1, date.ToShortDateString() + " " + date.ToLongTimeString(), format);
			}

			// Draw the content for the measurements.
			var x = 0;
			foreach (var key in measurements.Keys) {
				for (int y = 0; y < masterDates.Count; y++) {
					var m = measurements[key];
					var su = m.sensor.unit.standardUnit;
					var dt = masterDates[y];
					if (m.measurements.ContainsKey(dt)) {
						var scalar = su.OfScalar(m.measurements[dt]).ConvertTo(ion.defaultUnits.DefaultUnitFor(m.sensor.type));
						var format = sessionBreaks.Contains(dt) ? 3 : 2;
						file.SetCellValue(2 + y, 2 + x, "" + SensorUtils.ToFormattedString(m.sensor.type, scalar), format);
						Log.D(this, "X: " + x + " Y: " + y + " value: " + scalar);
					}
				}

				x++;
			}

			// Autofit
			file.AutofitCol(1, x + 1, false, 1.1);
		}

		/// <summary>
		/// Loads the sensor from the 
		/// </summary>
		/// <returns>The sensor.</returns>
		/// <param name="ion">Ion.</param>
		/// <param name="serialNumber">Serial number.</param>
		/// <param name="sensorIndex">Sensor index.</param>
		private GaugeDeviceSensor GetSensor(IION ion, string serialNumber, int sensorIndex) {
			if (SerialNumberExtensions.IsValidSerialNumber(serialNumber)) {
				var sn = SerialNumberExtensions.ParseSerialNumber(serialNumber);
				var device = ion.deviceManager[sn] as GaugeDevice;
				if (device != null) {
					return device[sensorIndex];
				} else {
					Log.E(this, "Failed to find gauge device with serial number: " + serialNumber);
				}
			} else {
				Log.E(this, "Failed to find gauge device with serial number: " + serialNumber);
			}

			return null;
		}

		/// <summary>
		/// Builds the header string that is used for the given sensor.
		/// </summary>
		/// <returns>The header string from sensor.</returns>
		/// <param name="ion">Ion.</param>
		/// <param name="sensor">Sensor.</param>
		private string GetHeaderStringFromSensor(IION ion, GaugeDeviceSensor sensor) {
			var device = sensor.device;
			var serialNumber = device.serialNumber;
			if (serialNumber.deviceModel == EDeviceModel.P500) {
				return serialNumber + " (" + Units.Pressure.IN_HG + "/" + Units.Pressure.PSIG + ")";
			} else {
				return serialNumber + " (" + ion.defaultUnits.DefaultUnitFor(sensor.type) + ")";
			}
		}

		/// <summary>
		/// Convenience method that is used to export the file.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="ion">Ion.</param>
		/// <param name="filepath">Filepath.</param>
		/// <param name="dlr">Dlr.</param>
		public static bool Export(Context context, IION ion, string filepath, DataLogReport dlr) {
			return new DataLogExcelReportExporter(context, ion, filepath, dlr).Commit();
		}


		private class Measurements {
			public GaugeDeviceSensor sensor;
			public string serialNumber;
			public int sensorIndex;
			public Dictionary<DateTime, double> measurements = new Dictionary<DateTime, double>();
		}
	}
}

