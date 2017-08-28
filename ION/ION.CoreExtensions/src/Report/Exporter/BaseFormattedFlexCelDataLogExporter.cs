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
  using ION.Core.Database;
  using ION.Core.Devices;
  using ION.Core.Sensors;

  public abstract class BaseFormattedFlexCelDataLogExporter : IDataLogExporter {
    /// <summary>
    /// The ion instance used for this exporter.
    /// </summary>
    /// <value>The ion.</value>
    protected IION ion { get; private set; }
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

    public BaseFormattedFlexCelDataLogExporter(IION ion) {
      this.ion = ion;
    }

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
		/// Draws the used devices section to the file.
		/// </summary>
		/// <param name="file">The file that is drawn to.</param>
		/// <param name="dlr">The report being drawn.</param>
		/// <param name="row">The x coordinate to start drawing the section in cells.</param>
		/// <param name="col">The y coordinate to start drawing the section in cells.</param>
		/// <returns>A Tuple contining the width and height respectively of the drawn section.</returns>
		protected Tuple<int, int> DrawUsedDevices(XlsFile file, DataLogReport dlr, int row, int col) {
			var l = dlr.localization;

			// Draw the header
			file.SetCellValue(row, col, l.serialNumber, sectionHeaderFormat);
			file.SetCellValue(row, col + 1, l.name, sectionHeaderFormat);
			file.SetCellValue(row, col + 2, l.certificationDate, sectionHeaderFormat);
			file.SetCellValue(row, col + 3, l.deviceModel, sectionHeaderFormat);

			// Draw the content
			var offset = 1;
			foreach (var device in dlr.devices) {
				var rowoff = row + offset;
				file.SetCellValue(rowoff, col, device.serialNumber, sectionContentFormat);
				file.SetCellValue(rowoff, col + 1, device.name, sectionContentFormat);
				file.SetCellValue(rowoff, col + 2, GetDeviceCalibrationTime(device), sectionContentFormat);
				file.SetCellValue(rowoff, col + 3, l.GetDeviceModelString(device.serialNumber.deviceModel), sectionContentFormat);
				offset++;
			}

			return new Tuple<int, int>(4, offset);
		}

		/// <summary>
		/// Draws the important dates for the report.
		/// </summary>
		/// <returns>The report dates.</returns>
		/// <param name="file">The file that is drawn to.</param>
		/// <param name="dlr">The report being drawn.</param>
		/// <param name="row">The x coordinate to start drawing the section in cells.</param>
		/// <param name="col">The y coordinate to start drawing the section in cells.</param>
		protected Tuple<int, int> DrawReportDates(XlsFile file, DataLogReport dlr, int row, int col) {
			var l = dlr.localization;

			// Draw the header
			// Merge the header cells
			file.MergeCells(row, col + 1, row, col + 3);
			file.MergeCells(row + 1, col + 1, row + 1, col + 3);
			// Set the cell format for the merged cells (without this the merged cell doesn't have a format and is white)
			for (int i = 1; i < 4; i++) {
				file.SetCellFormat(row, col + i, sectionContentFormat);
			}

			file.SetCellValue(row, col, l.reportCreated, sectionHeaderFormat);
			file.SetCellValue(row, col + 1, DateTime.Now.ToLongDateString(), sectionContentFormat);
			file.SetCellValue(row + 1, col, l.reportDates, sectionHeaderFormat);
			file.SetCellValue(row + 1, col + 1, "", sectionHeaderFormat);
      
      var startEnds = dlr.GatherSessionStartEnds();
      var times = new List<Tuple<DateTime, DateTime>>(startEnds.Values);
      times.Sort();
      
			var offset = 2;
			// Draw the dates
      foreach (var time in times) {
        var ro = row + offset++; // row offset;
        
        file.MergeCells(ro, col, ro, col + 3);
        file.SetCellValue(ro, col, time.Item1.ToShortDateString() + " " + time.Item1.ToShortTimeString() + " - " + 
                                    time.Item2.ToShortDateString() + " " + time.Item2.ToShortTimeString(), sectionContentFormat);
        // Set the cell format for the merged cells (without this the merged cell doesn't have a format and is white)
        for (int i = 0; i < 4; i++) {
          file.SetCellFormat(ro, col + i, sectionContentFormat);
        }
      }

      return new Tuple<int, int>(2, offset);
		}

		/// <summary>
		/// Draws the reports device measurement statistics to the file.
		/// </summary>
		/// <returns>A Tuple containing the width and height respectively of the drawn section.</returns>
		/// <param name="file">The file that is drawn to.</param>
		/// <param name="dlr">The report being drawn.</param>
		/// <param name="row">The x coordinate to start drawing the section in cells.</param>
		/// <param name="col">The y coordinate to start drawing the section in cells.</param>
		protected Tuple<int, int> DrawDeviceAverages(XlsFile file, DataLogReport dlr, int row, int col) {
			var l = dlr.localization;
      
			// Draw header
			file.SetCellValue(row, col, l.serialNumber, sectionHeaderFormat);
			file.SetCellValue(row, col + 1, l.minimum, sectionHeaderFormat);
			file.SetCellValue(row, col + 2, l.maximum, sectionHeaderFormat);
			file.SetCellValue(row, col + 3, l.average, sectionHeaderFormat);

			// Draw the content
			var offset = 1;
			foreach (var pair in dlr.dataLogResults) {
				var ro = row + offset++;
        var sensorType = pair.Key.type;
				var u = ion.preferences.units.DefaultUnitFor(sensorType);

        var min = pair.Value.minimum.ConvertTo(u);
        var max = pair.Value.maximum.ConvertTo(u);
        var avg = pair.Value.average.ConvertTo(u);

				file.SetCellValue(ro, col, pair.Key.device.serialNumber, sectionContentFormat);
				file.SetCellValue(ro, col + 1, SensorUtils.ToFormattedString(min, true), sectionContentFormat);
				file.SetCellValue(ro, col + 2, SensorUtils.ToFormattedString(max, true), sectionContentFormat);
				file.SetCellValue(ro, col + 3, SensorUtils.ToFormattedString(avg, true), sectionContentFormat);
			}

			return new Tuple<int, int>(4, offset);
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
		protected Tuple<int, int> DrawAllMeasurements(XlsFile file, DataLogReport dlr, int row, int col) {
			var l = dlr.localization;
			var sensors = dlr.dataLogResults.Keys;

			// Coallate data
			var unsortedMasterDates = new HashSet<DateTime>();

			var masterDates = new List<DateTime>();
			var sessionBreaks = new Dictionary<int, DateTime>();
			var measurements = new Dictionary<GaugeDeviceSensor, Measurements>();
      
      foreach (var sdlr in dlr.dataLogResults) {
        foreach (var sid in sdlr.Value.sessionIds) {
          Measurements m;
          if (!measurements.TryGetValue(sdlr.Key, out m)) {
            measurements[sdlr.Key] = m = new Measurements() {
              sensor = sdlr.Key,
              measurements = new Dictionary<DateTime, Scalar>(),
            };
          }
        
          foreach (var dlm in sdlr.Value[sid]) {
            m.measurements[dlm.recordedDate] = dlm.measurement;
            unsortedMasterDates.Add(dlm.recordedDate);
          }
          
          var last = sdlr.Value[sid][sdlr.Value[sid].Count - 1].recordedDate;
          if (sessionBreaks.ContainsKey(sid)) {
            var dt = sessionBreaks[sid];
            if (last > dt) {
              sessionBreaks[sid] = last;
            }
          } else {
            sessionBreaks[sid] = last;
          }
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
        var format = sessionBreaks.ContainsValue(date) ? sessionBreakFormat : sectionContentFormat;
        file.SetCellValue(row + i + 1, col, date.ToShortDateString() + " " + date.ToLongTimeString(), format);
      }

      // Draw the content for the measurements
      var mi = 1;
      foreach (var m in measurements) {
        for (int y = 0; y < masterDates.Count; y++) {
          var dt = masterDates[y];
          if (m.Value.measurements.ContainsKey(dt)) {
            var scalar = ion.preferences.units.ToDefaultUnit(m.Value.measurements[dt]);
            var format = sessionBreaks.ContainsValue(dt) ? sessionBreakFormat : sectionContentFormat;
            file.SetCellValue(row + y + 1, row + mi, SensorUtils.ToFormattedString(scalar), format);
          }
        }
        mi++;
      }

      file.AutofitCol(col, col + mi, false, 1.0);

      return new Tuple<int, int>(1 + mi, 1 + measurements.Count);
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
		/// Attempts to query that last calibration time for the given device.
		/// </summary>
		/// <returns>The device calibration time.</returns>
		/// <param name="device">Device.</param>
		private string GetDeviceCalibrationTime(IDevice device) {
			try {
				var table = ion.database.Table<LoggingDeviceRow>();
				var sn = device.serialNumber.ToString();
				var row = table.Where(ldr => ldr.serialNumber.Equals(sn)).FirstOrDefault();
        if (row == null || row.nistDate == null || string.IsNullOrEmpty(row.nistDate.Trim())) {
					Log.D(this, "Device{" + device.serialNumber + "} does not have a calibration date");
					return "N/A";
				} else {
					return row.nistDate;
				}
			} catch (Exception e) {
				Log.E(this, "Failed to resolve device's last calibration time", e);
				return "N/A";
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
      public Dictionary<DateTime, Scalar> measurements = new Dictionary<DateTime, Scalar>();
    }
  }
}
