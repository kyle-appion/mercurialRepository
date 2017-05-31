namespace ION.Core.Report.DataLogs.Exporter {

  using System;
	using System.IO;
	using System.Threading.Tasks;

	using FlexCel.Core;
	using FlexCel.Render;
	using FlexCel.XlsAdapter;

	using Appion.Commons.Measure;
	using Appion.Commons.Util;

	using ION.Core.App;
	using ION.Core.Database;
	using ION.Core.Devices;
	using ION.Core.Sensors;

	public class SummaryPdfReportExporter : BaseFormattedFlexCelDataLogExporter {

		private IION ion;
    private bool detailed;

		public SummaryPdfReportExporter(IION ion, bool detailed) {
			this.ion = ion;
      this.detailed = detailed;
		}

		public override async Task<bool> Export(Stream stream, DataLogReport dlr) {
			await Task.Delay(1);
			try {
				// TODO ahodder@appioninc.com: Not necessary
				var scaleReduction = 60; // TODO DEFINE

				var file = new XlsFile(2, TExcelFileFormat.v2013, true);
				file.AllowOverwritingFiles = true;
				file.ActiveSheet = 1;
				file.PrintScale = scaleReduction;
				// Note: ahodder@appioninc.com: Per kyle's original writing
				// SET A UNIFORM CELL WIDTH FOR COVER PAGE ITEMS. DEFAULT COLUMN WIDTH = 3189 AND SETTING IT TO 1.3x RESULTING IN 4317
				file.SetColWidth(1, 8, 4317); // TODO DEFINE

				InitializeToFile(file);
				RenderFile(file, dlr);

				// Commit file to stream
        using (var pdf = new FlexCelPdfExport(file, true)) {
					pdf.GetFontData += (sender, e) => {
						var ms = new MemoryStream(1024 * 64);
            using (var fontStream = dlr.localization.GetFontStream()) {
							var buffer = new byte[1024];
							var read = -1;
							while ((read = fontStream.Read(buffer, 0, buffer.Length)) > 0) {
								ms.Write(buffer, 0, buffer.Length);
							}              
            }
						e.FontData = ms.ToArray();
            ms.Dispose();
					};

          pdf.Export(stream);          
        }

				return true;
			} catch (Exception e) {
				Log.E(this, "Failed to export datalog report", e);
				return false;
			}
		}

		/// <summary>
		/// Draws all of the sections into the file.
		/// </summary>
		/// <param name="file">File.</param>
		/// <param name="dlr">Dlr.</param>
		private void RenderFile(XlsFile file, DataLogReport dlr) {
			// Cell coordinates
      var row = 1;
			Tuple<int, int> size;

			// Draw the report header
			size = DrawAppionLogo(file, dlr);
      row += size.Item2;
      row += 2;

			// Draw the used devices section
			size = DrawUsedDevices(file, dlr, row, 1);
			row += size.Item2;
      row += 2;

      // Draw the date section
      size = DrawReportDates(file, dlr, row, 1);
      row += size.Item2;
      row += 2;

			// Draw the device averages
			size = DrawDeviceAverages(file, dlr, row, 1);
			row += size.Item2;
			row += 2;

      // Draw the graph content
      size = DrawSmallGraphs(file, dlr, row, 1);
      row += size.Item2;
      row += 2;
		}

		/// <summary>
		/// Attempts to draw the Appion logo into the upper left of the file on the current sheet.
		/// </summary>
		/// <param name="file">File.</param>
		/// <returns>A Tuple containing the width and height respectively of the drawn section.</returns>
		private Tuple<int, int> DrawAppionLogo(XlsFile file, DataLogReport dlr) {
			if (dlr.appionLogoPng != null) {
				// Add the image
				var image = new TImageProperties();
				image.Anchor = new TClientAnchor(TFlxAnchorType.MoveAndDontResize, 1, 0, 1, 0, 5, 255, 3, 1024);
				image.ShapeName = "Logo";
				file.AddImage(dlr.appionLogoPng, image);
				// Add the header
				file.MergeCells(4, 4, 5, 6);
				file.SetCellValue(4, 4, dlr.reportName, titleFormat);
				return new Tuple<int, int>(6, 4);
			} else {
				Log.E(this, "Failed to draw appion logo: no bitmap bytes");
				return new Tuple<int, int>(0, 0);
			}
		}

		/// <summary>
		/// Draws the used devices section to the file.
		/// </summary>
		/// <param name="file">The file that is drawn to.</param>
		/// <param name="dlr">The report being drawn.</param>
		/// <param name="row">The x coordinate to start drawing the section in cells.</param>
		/// <param name="col">The y coordinate to start drawing the section in cells.</param>
		/// <returns>A Tuple contining the width and height respectively of the drawn section.</returns>
		private Tuple<int, int> DrawUsedDevices(XlsFile file, DataLogReport dlr, int row, int col) {
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
		private Tuple<int, int> DrawReportDates(XlsFile file, DataLogReport dlr, int row, int col) {
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

      var offset = 2;
      // Draw the dates
      foreach (var sr in dlr.sessionResults) {
        var rowOffset = row + offset;
        // Merge content cells
        file.MergeCells(rowOffset, col, rowOffset, col + 3);
        file.SetCellValue(rowOffset, col, sr.startTime.ToLongDateString() + "-" + sr.endTime.ToLongDateString(), sectionContentFormat);
        // Set the cell format for the merged cells (without this the merged cell doesn't have a format and is white)
        for (int i = 0; i < 4; i++) {
          file.SetCellFormat(rowOffset, col + i, sectionContentFormat);
        }
        offset++;
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
    private Tuple<int, int> DrawDeviceAverages(XlsFile file, DataLogReport dlr, int row, int col) {
			var l = dlr.localization;
			// Draw header
			file.SetCellValue(row, col, l.serialNumber, sectionHeaderFormat);
			file.SetCellValue(row, col + 1, l.minimum, sectionHeaderFormat);
			file.SetCellValue(row, col + 2, l.maximum, sectionHeaderFormat);
			file.SetCellValue(row, col + 3, l.average, sectionHeaderFormat);

			// Draw the content
			var offset = 1;
			foreach (var sensor in dlr.sensors) {
        var rowoff = row + offset;
				var u = ion.preferences.units.DefaultUnitFor(sensor.type);

				ScalarSpan min, max, avg;
				dlr.CalculateDeviceMetrics(sensor, out min, out max, out avg);

				file.SetCellValue(rowoff, col, sensor.device.serialNumber, sectionContentFormat);
				file.SetCellValue(rowoff, col + 1, SensorUtils.ToFormattedString(min, true), sectionContentFormat);
				file.SetCellValue(rowoff, col + 2, SensorUtils.ToFormattedString(max, true), sectionContentFormat);
				file.SetCellValue(rowoff, col + 3, SensorUtils.ToFormattedString(avg, true), sectionContentFormat);

				offset++;
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
    private Tuple<int, int> DrawAllMeasurements(XlsFile file, DataLogReport dlr, int row, int col) {
			var l = dlr.localization;
			var sensors = dlr.sensors;
			var srs = dlr.sessionResults;
			srs.Sort();

			// Draw Header Padding
			file.SetCellValue(row, col, "", sectionHeaderFormat);
			file.SetCellValue(row, col + 1, "", sectionHeaderFormat);

			var sxoff = 2; // header sensor index x offset
						   // Draw Header
			foreach (var sensor in sensors) {
				var yoff = col + sxoff;
				var u = ion.preferences.units.DefaultUnitFor(sensor.type);

				file.SetCellValue(sxoff, col, l.GetSensorTypeString(sensor.type) + "(" + u + ")", sectionHeaderFormat);
				file.SetCellValue(sxoff, col + 1, sensor.device.serialNumber, sectionContentFormat);
				sxoff++;
			}

			var index = 2;
			// Render SessionsResults measurement data
			foreach (var sr in srs) {

			}

      return new Tuple<int, int>(0, 0);
		}

		/// <summary>
		/// Draws the graphs for each of the devices.
		/// </summary>
		/// <returns>The graphs.</returns>
		/// <param name="file">File.</param>
		/// <param name="dlr">Dlr.</param>
		/// <param name="row">The x coordinate.</param>
		/// <param name="col">The y coordinate.</param>
    private Tuple<int, int> DrawSmallGraphs(XlsFile file, DataLogReport dlr, int row, int col) {
			var l = dlr.localization;

			var index = 0;
			foreach (var sensor in dlr.sensors) {
				// The shift that is used to stagger the graphs down the pages.
        var xoff = col + (index / 2);
				var yoff = index % 2 == 0 ? 4 : 0;

				var image = new TImageProperties();
				image.Anchor = new TClientAnchor(TFlxAnchorType.MoveAndDontResize, xoff, 0, yoff, 0, xoff + 5, 255, yoff + 2, 1024);
        file.AddImage(dlr.graphImages[sensor], image);

				file.SetCellValue(xoff, yoff, sensor.device.serialNumber + "\n" + l.GetSensorTypeString(sensor.type), sectionContentFormat);
				index++;
			}

      return new Tuple<int, int>(12, (int)Math.Ceiling(index / 2.0) * 3);
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
				if (row == null) {
					Log.D(this, "Device{" + device.serialNumber + "} does not have a calibration date");
					return "N/A";
				} else {
          if (string.IsNullOrEmpty(row.nistDate)) {
            return row.nistDate;
          } else {
            return "N/A";
          }
				}
			} catch (Exception e) {
				Log.E(this, "Failed to resolve device's last calibration time", e);
				return "N/A";
			}
		}
	}
}
