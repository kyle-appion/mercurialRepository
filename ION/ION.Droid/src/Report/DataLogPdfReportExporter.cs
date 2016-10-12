﻿namespace ION.Droid.Report {

	using System;
	using System.Collections.Generic;
	using System.IO;

	using Android.Content;

	using FlexCel.Core;
	using FlexCel.Render;
	using FlexCel.XlsAdapter;

	using ION.Core.App;
	using ION.Core.Devices;
	using ION.Core.Measure;
	using ION.Core.Report.DataLogs;
	using ION.Core.Sensors;
	using ION.Core.Util;

	using ION.Droid.App;
	using ION.Droid.Util;

	public class DataLogPdfReportExporter {

		/// <summary>
		/// The excel file that we will write the data to.
		/// </summary>
		private XlsFile file;

		public DataLogPdfReportExporter() {
			file = new XlsFile(1, TExcelFileFormat.v2013, true);
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
			contentFormat.Borders.Bottom.Style = TFlxBorderStyle.Thin;
			contentFormat.Borders.Bottom.Color = TUIColor.FromArgb(171, 171, 171);
			contentFormat.Borders.Top.Style = TFlxBorderStyle.Thin;
			contentFormat.Borders.Top.Color = TUIColor.FromArgb(171, 171, 171);
			contentFormat.Borders.Left.Style = TFlxBorderStyle.Thin;
			contentFormat.Borders.Left.Color = TUIColor.FromArgb(171, 171, 171);
			contentFormat.Borders.Right.Style = TFlxBorderStyle.Thin;
			contentFormat.Borders.Right.Color = TUIColor.FromArgb(171, 171, 171);
			contentFormat.VAlignment = TVFlxAlignment.top;
			contentFormat.HAlignment = THFlxAlignment.center;

			// The format that is applied fo the border of cells.
			var borderFormat = file.GetDefaultFormat;
			borderFormat.Borders.Bottom.Color = TUIColor.FromArgb(0xFF, 0x33, 0x33);
			borderFormat.Borders.Bottom.Style = TFlxBorderStyle.Medium;
/*
			borderFormat.Borders.Top.Style = TFlxBorderStyle.Thin;
			borderFormat.Borders.Top.Color = TUIColor.FromArgb(171, 171, 171);
*/
			borderFormat.Borders.Left.Style = TFlxBorderStyle.Thin;
			borderFormat.Borders.Left.Color = TUIColor.FromArgb(171, 171, 171);
			borderFormat.Borders.Right.Style = TFlxBorderStyle.Thin;
			borderFormat.Borders.Right.Color = TUIColor.FromArgb(171, 171, 171);
			borderFormat.VAlignment = TVFlxAlignment.top;
			borderFormat.HAlignment = THFlxAlignment.center;

			// The order that the formats are added to the file is the 1-based index that is passed to SetCellValue as the cell
			// format.

			file.AddFormat(headerFormat);   // 1
			file.AddFormat(contentFormat);  // 2
			file.AddFormat(borderFormat);   // 3
		}

		public bool Export(AndroidION ion, Context context, string filepath, List<SessionResults> sessionResults) {
			try {
				var unsortedMasterDates = new HashSet<DateTime>();

				var masterDates = new List<DateTime>();
				var sessionBreaks = new HashSet<DateTime>();
				var measurements = new Dictionary<string, Measurements>();

				foreach (var sr in sessionResults) {
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
				var x =0;
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

				for (int j = 0; j <= measurements.Count; j++) {
					file.AutofitCol(1 + j, false, 1.0);
				}

				using (var pdf = new FlexCelPdfExport(file, true)) {
					pdf.GetFontData += (sender, e) => {
						var ms = new MemoryStream(1024 * 64);
						var stream = ion.Assets.Open("fonts/RobotoCondensed.ttf");

						var buffer = new byte[1024];
						var read = -1;
						while ((read = stream.Read(buffer, 0, buffer.Length)) > 0) {
							ms.Write(buffer, 0, buffer.Length);
						}

						e.FontData = ms.ToArray();
					};

					pdf.Export(filepath);
				}

				return true;
			} catch (Exception e) {
				Log.E(this, "Failed to export data log to excel.", e);
				return false;
			}
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


		private class Measurements {
			public GaugeDeviceSensor sensor;
			public string serialNumber;
			public int sensorIndex;
			public Dictionary<DateTime, double> measurements = new Dictionary<DateTime, double>();
		}
	}
}
