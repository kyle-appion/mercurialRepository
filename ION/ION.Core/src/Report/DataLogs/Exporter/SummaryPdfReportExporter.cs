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
  using ION.Core.Devices.Certificates;
  using ION.Core.Sensors;

	public class SummaryPdfReportExporter : BaseFormattedFlexCelDataLogExporter {

    private IION ion;

    public SummaryPdfReportExporter(IION ion) {
      this.ion = ion;
    }

    public override async Task<bool> Export(DataLogReport dlr) {
      await Task.Delay(1);

			// TODO ahodder@appioninc.com: Not necessary
			var scaleReduction = 60; // TODO DEFINE

			var file = new XlsFile();
			file.AllowOverwritingFiles = true;
			file.ActiveSheet = 2;
			file.PrintScale = scaleReduction;
			// Note: ahodder@appioninc.com: Per kyle's original writing
			// SET A UNIFORM CELL WIDTH FOR COVER PAGE ITEMS. DEFAULT COLUMN WIDTH = 3189 AND SETTING IT TO 1.3x RESULTING IN 4317
			file.SetColWidth(1, 8, 4317); // TODO DEFINE

      InitializeToFile(file);

      DrawAppionLogo(file, dlr);

      return false;
		}

    /// <summary>
    /// Draws all of the sections into the file.
    /// </summary>
    /// <param name="file">File.</param>
    /// <param name="dlr">Dlr.</param>
    private void RenderFile(XlsFile file, DataLogReport dlr) {
      
    }

    /// <summary>
    /// Attempts to draw the Appion logo into the upper left of the file on the current sheet.
    /// </summary>
    /// <param name="file">File.</param>
    private void DrawAppionLogo(XlsFile file, DataLogReport dlr) {
      if (dlr.appionLogoPng != null) {
        // Add the image
        var image = new TImageProperties();
        image.Anchor = new TClientAnchor(TFlxAnchorType.MoveAndDontResize, 1, 0, 1, 0, 5, 255, 3, 1024);
        image.ShapeName = "Logo";
        file.AddImage(dlr.appionLogoPng, image);
        // Add the header
        file.MergeCells(4, 4, 5, 6);
        file.SetCellValue(4, 4, dlr.reportName, titleFormat);
      } else {
        Log.E(this, "Failed to draw appion logo: no bitmap bytes");
      }
    }

    /// <summary>
    /// Draws the used devices section to the file.
    /// </summary>
    /// <param name="x">The x coordinate to start drawing the used devices in cells.</param>
    /// <param name="y">The y coordinate to start drawing the used devices in cells.</param>
    /// <param name="dlr">The report that is being drawn.</param>
    /// <returns>A Tuple contining the width and height respectively of the drawn section.</returns>
    private Tuple<int, int> DrawUsedDevices(XlsFile file, DataLogReport dlr, int x, int y) {
			var l = dlr.localization;

      // Draw the header
      file.SetCellValue(x, y, l.serialNumber, sectionHeaderFormat);
      file.SetCellValue(x + 1, y, l.name, sectionHeaderFormat);
      file.SetCellValue(x + 2, y, l.certificationDate, sectionHeaderFormat);
      file.SetCellValue(x + 3, y, l.deviceModel, sectionHeaderFormat);

      // Draw the content
      var offset = 1;
      foreach (var device in dlr.devices) {
        var yoff = y + offset;
        file.SetCellValue(x, yoff, device.serialNumber, sectionContentFormat);
        file.SetCellValue(x + 1, yoff, device.name, sectionContentFormat);
        file.SetCellValue(x + 2, yoff, GetDeviceCalibrationTime(device), sectionContentFormat);
        file.SetCellValue(x + 3, yoff, l.GetDeviceModelString(device.serialNumber.deviceModel), sectionContentFormat);
        offset++;
      }

      return new Tuple<int, int>(4, offset);
		}

    /// <summary>
    /// Draws the reports device measurement statistics to the file.
    /// </summary>
    /// <returns>A Tuple containing the width and height respectively of the drawn section.</returns>
    /// <param name="file">The file that is drawn to.</param>
    /// <param name="dlr">The report being drawn.</param>
    /// <param name="x">The x coordinate to start drawing the section in cells.</param>
    /// <param name="y">The y coordinate to start drawing the section in cells.</param>
    private Tuple<int, int> DrawDeviceMeasurements(XlsFile file, DataLogReport dlr, int x, int y) {
      var l = dlr.localization;
      // Draw header
      file.SetCellValue(x, y, l.serialNumber, sectionHeaderFormat);
      file.SetCellValue(x, y + 1, l.minimum, sectionHeaderFormat);
      file.SetCellValue(x, y + 2, l.maximum, sectionHeaderFormat);
			file.SetCellValue(x, y + 3, l.average, sectionHeaderFormat);

      // Draw the content
      var offset = 1;
      foreach (var sensor in dlr.sensors) {
        var yoff = y + offset;
				var u = ion.preferences.units.DefaultUnitFor(sensor.type);

				ScalarSpan min, max, avg;
        dlr.CalculateDeviceMetrics(sensor, out min, out max, out avg);

        file.SetCellValue(x, yoff, sensor.device.serialNumber, sectionContentFormat);
        file.SetCellValue(x + 1, yoff, SensorUtils.ToFormattedString(min, true), sectionContentFormat);
        file.SetCellValue(x + 2, yoff, SensorUtils.ToFormattedString(max, true), sectionContentFormat);
        file.SetCellValue(x + 3, yoff, SensorUtils.ToFormattedString(avg, true), sectionContentFormat);

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
    /// <param name="x">The x coordinate.</param>
    /// <param name="y">The y coordinate.</param>
    private Tuple<int, int> DrawAllMeasurements(XlsFile file, DataLogReport dlr, int x, int y) {
      var l = dlr.localization;
      var sensors = dlr.sensors;
      var srs = dlr.sessionResults;
      srs.Sort();

      // Draw Header Padding
      file.SetCellValue(x, y, "", sectionHeaderFormat);
      file.SetCellValue(x, y + 1, "", sectionHeaderFormat);

      var sxoff = 2; // header sensor index x offset
      // Draw Header
      foreach (var sensor in sensors) {
        var yoff = y + sxoff;
        var u = ion.preferences.units.DefaultUnitFor(sensor.type);

        file.SetCellValue(sxoff, y, l.GetSensorTypeString(sensor.type) + "(" + u + ")", sectionHeaderFormat);
        file.SetCellValue(sxoff, y + 1, sensor.device.serialNumber, sectionContentFormat);
        sxoff++;
      }

      var index = 2;
      // Render SessionsResults measurement data
      foreach (var sr in srs) {
        
      }

      return null;
    }

    /// <summary>
    /// Draws the graphs for each of the devices.
    /// </summary>
    /// <returns>The graphs.</returns>
    /// <param name="file">File.</param>
    /// <param name="dlr">Dlr.</param>
    /// <param name="x">The x coordinate.</param>
    /// <param name="y">The y coordinate.</param>
    private Tuple<int, int> DrawGraphs(XlsFile file, DataLogReport dlr, int x, int y, bool stagger) {
      var l = dlr.localization;

      var index = 0;
      foreach (var sensor in dlr.sensors) {
        // The shift that is used to stagger the graphs down the pages.
        var xoff = (stagger && index % 2 == 0) ? 4 : 0;
        var yoff = y + (stagger ? index / 2 : index);

        var image = new TImageProperties();
        image.Anchor = new TClientAnchor(TFlxAnchorType.MoveAndDontResize, xoff, 0, yoff, 0, xoff + 5, 255, yoff + 2, 1024);
        file.AddImage(dlr.graphImages[sensor], image);

        file.SetCellValue(xoff, yoff, sensor.device.serialNumber + "\n" + l.GetSensorTypeString(sensor.type), sectionContentFormat);
        index++;
      }

      return new Tuple<int, int>(stagger ? 12 : 6, (stagger ? index / 2 : index ) * 3);
    }

		/// <summary>
		/// Attempts to query that last calibration time for the given device.
		/// </summary>
		/// <returns>The device calibration time.</returns>
		/// <param name="device">Device.</param>
		private string GetDeviceCalibrationTime(IDevice device) {
			try {
				var table = ion.database.Table<LoggingDeviceRow>();
				var row = table.Where(ldr => ldr.serialNumber.Equals(device.serialNumber.ToString())).FirstOrDefault();
				if (row == null) {
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
  }
}
