namespace ION.Core.Report.DataLogs.Exporter {

	using System;
	using System.Collections.Generic;
	using System.IO;
  using System.Text;
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

  public class CsvExporter : BaseFormattedFlexCelDataLogExporter {

    public CsvExporter(IION ion) : base(ion) {
    }

    public override Task<bool> Export(Stream stream, DataLogReport dlr) {
      return Task.Factory.StartNew(() => {
        try {
  			  var file = new XlsFile(1, TExcelFileFormat.v2013, true);
          InitializeToFile(file);
          RenderFile(file, dlr);

          file.Save(stream, TFileFormats.Text, '|', Encoding.UTF8);

          return true;
        } catch (Exception e) {
          Log.E(this, "Failed to export CSV report", e);
          return false;
        }
      });
    }

    /// <summary>
    /// Draws the content of the csv.
    /// </summary>
    /// <param name="file">File.</param>
    /// <param name="dlr">Dlr.</param>
    private void RenderFile(XlsFile file, DataLogReport dlr) {
      DrawAllMeasurements(file, dlr, 1, 1);
    }
  }
}
