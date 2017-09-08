namespace ION.Core.Report.DataLogs.Exporter {

	using System;
	using System.Collections.Generic;
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
	using ION.Core.UI;

  public class ExcelReportExporter : BaseFormattedFlexCelDataLogExporter {
    public ExcelReportExporter(IION ion) : base(ion) {
    }

    public override Task<bool> Export(Stream stream, DataLogReport dlr) {
      return Task.Factory.StartNew(() => {
        try {
				  var file = new XlsFile(2, TExcelFileFormat.v2013, true);
				  file.AllowOverwritingFiles = true;

				  InitializeToFile(file);
				  RenderFile(file, dlr);

          file.Save(stream);

          return true;
        } catch (Exception e) {
          Log.E(this, "Failed to export excel report", e);
          return false;
        }
      });
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
			file.ActiveSheet = 1;

			////////////
			// SHEET 1
			////////////
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

      file.AutofitCol(1, size.Item1, false, 1);

			////////////
			// SHEET 2
			////////////
			row = 1;
			// Draw raw data
			file.ActiveSheet = 2;
			size = DrawAllMeasurements(file, dlr, row, 1);
		}
  }
}
