namespace ION.Core.Report.DataLogs.Exporter {

  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Threading.Tasks;

  using FlexCel.Core;
  using FlexCel.Render;
  using FlexCel.XlsAdapter;

  using Appion.Commons.Util;
  using Appion.Commons.Measure;

  using ION.Core.App;
  using ION.Core.Devices;
  using ION.Core.Sensors;

  public abstract class BaseFormattedFlexCelDataLogExporter : IDataLogExporter {
    /// <summary>
    /// The format that is used to render the title.
    /// </summary>
    protected int titleFormat;
    /// <summary>
    /// The format that is used to render the section header.
    /// </summary>
    protected int sectionHeaderFormat;
    /// <summary>
    /// The format that is used to render the section content.
    /// </summary>
    protected int sectionContentFormat;
    /// <summary>
    /// The format that is used to render session breaks.
    /// </summary>
    protected int sessionBreakFormat;

    // Implemented for IDataLogExporter
    public abstract Task<bool> Export(Stream stream, DataLogReport dlr);

    /// <summary>
    /// Initializes the formats for the given file.
    /// </summary>
    /// <param name="file">File.</param>
    protected void InitializeToFile(XlsFile file) {
      titleFormat = CreateTitleTextFormat(file);
      sectionHeaderFormat = CreateSectionHeaderFormat(file);
      sectionContentFormat = CreateSectionContentFormat(file);
      sessionBreakFormat = CreateSessionBreakFormat(file);
    }

		/// <summary>
		/// Draws all of the session's sensor measurement data. This is done in a fairly simple, yet tedious way. Across
		/// the section header, we will list all of the sensors in the report. The left-most column will show all of the
		/// sorted dates in all of the sessions. Then, for each sensor, it will list all of its session measurements. When
		/// a session is complete, we will the bottom of that session's measurement column with a red border.
		/// </summary>
		/// <returns>The all measurements.</returns>
		/// <param name="file">File.</param>
		/// <param name="dlr">Dlr.</param>
		/// <param name="row">The x coordinate.</param>
		/// <param name="col">The y coordinate.</param>
		protected Tuple<int, int> DrawAllMeasurements(XlsFile file, DataLogReport dlr, IION ion, int row, int col) {
			var l = dlr.localization;
			var sensors = dlr.sensors;
			var srs = dlr.sessionResults;
			srs.Sort();

			// Coallate data
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

			// Draw Header Padding
			file.SetCellValue(row, col, "", sectionHeaderFormat);
      var hi = 1;
      foreach (var key in measurements.Keys) {
        file.SetCellValue(row, col + hi++, GetHeaderStringFromSensor(ion, measurements[key].sensor), sectionHeaderFormat);
      }

      // Draw the master dates list to the file.
      for (int i = 0; i < masterDates.Count; i++) {
        var date = masterDates[i];
        var format = sessionBreaks.Contains(date) ? sessionBreakFormat : sectionContentFormat;
        file.SetCellValue(row + i + 1, col, date.ToShortDateString() + " " + date.ToLongTimeString(), format);
      }

      // Draw the content for the measurements
      var mi = 1;
      foreach (var key in measurements.Keys) {
        for (int y = 0; y < masterDates.Count; y++) {
          var m = measurements[key];
          var su = m.sensor.unit.standardUnit;
          var dt = masterDates[y];
          if (m.measurements.ContainsKey(dt)) {
            var scalar = su.OfScalar(m.measurements[dt]).ConvertTo(ion.preferences.units.DefaultUnitFor(m.sensor.type));
            var format = sessionBreaks.Contains(dt) ? sessionBreakFormat : sectionContentFormat;
            file.SetCellValue(row + y + 1, row + mi, SensorUtils.ToFormattedString(scalar), format);
          }
        }
        mi++;
      }

      file.AutofitCol(col, col + mi, false, 1.0);

      return new Tuple<int, int>(1 + mi, 1 + measurements.Count);
		}

		/// <summary>
		/// Loads the sensor from the 
		/// </summary>
		/// <returns>The sensor.</returns>
		/// <param name="ion">Ion.</param>
		/// <param name="serialNumber">Serial number.</param>
		/// <param name="sensorIndex">Sensor index.</param>
		protected GaugeDeviceSensor GetSensor(IION ion, string serialNumber, int sensorIndex) {
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
				return serialNumber + " (" + ion.preferences.units.DefaultUnitFor(sensor.type) + ")";
			}
		}

		/// <summary>
		/// Creates and registers the format that is used to draw title text.
		/// </summary>
		/// <returns>The bold text format.</returns>
		/// <param name="file">File.</param>
		private int CreateTitleTextFormat(XlsFile file) {
			var format = file.GetDefaultFormat;
			format.Font.Style = TFlxFontStyles.Bold;
			format.Font.Size20 = 400;
			return file.AddFormat(format);
		}

    /// <summary>
    /// Creates and registers the format that is used to draw the header
    /// </summary>
    /// <returns>The section header format.</returns>
    /// <param name="file">File.</param>
    private int CreateSectionHeaderFormat(XlsFile file) {
      var format = file.GetDefaultFormat;
      format.FillPattern = new TFlxFillPattern { Pattern = TFlxPatternStyle.Solid, FgColor = Colors.Black };
			format.VAlignment = TVFlxAlignment.top;
			format.HAlignment = THFlxAlignment.center;
      format.Font.Color = Colors.White;
      return file.AddFormat(format);
    }

    private int CreateSectionContentFormat(XlsFile file) {
      var format = file.GetDefaultFormat;
      format.Borders.Top.Color = Colors.Black;
			format.Borders.Top.Style = TFlxBorderStyle.Thin;
			format.Borders.Left.Color = Colors.Black;
			format.Borders.Left.Style = TFlxBorderStyle.Thin;
			format.Borders.Right.Color = Colors.Black;
			format.Borders.Right.Style = TFlxBorderStyle.Thin;
			format.Borders.Bottom.Color = Colors.Black;
			format.Borders.Bottom.Style = TFlxBorderStyle.Thin;
      format.WrapText = true;
			format.VAlignment = TVFlxAlignment.top;
			format.HAlignment = THFlxAlignment.center;
      return file.AddFormat(format);
    }

    private int CreateSessionBreakFormat(XlsFile file) {
      var format = file.GetDefaultFormat;
      format.Borders.Bottom.Color = Colors.Red;
			format.Borders.Bottom.Style = TFlxBorderStyle.Medium;
			format.Borders.Top.Color = Colors.Black;
			format.Borders.Top.Style = TFlxBorderStyle.Thin;
			format.Borders.Left.Color = Colors.Black;
			format.Borders.Left.Style = TFlxBorderStyle.Thin;
			format.Borders.Right.Color = Colors.Black;
			format.Borders.Right.Style = TFlxBorderStyle.Thin;
			format.VAlignment = TVFlxAlignment.top;
			format.HAlignment = THFlxAlignment.center;
      return file.AddFormat(format);
    }

    private class Measurements {
      public GaugeDeviceSensor sensor;
      public string serialNumber;
      public int sensorIndex;
      public Dictionary<DateTime, double> measurements = new Dictionary<DateTime, double>();
    }
  }
}
