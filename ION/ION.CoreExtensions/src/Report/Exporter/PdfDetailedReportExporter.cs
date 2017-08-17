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

	public class PdfDetailedReportExporter : BaseFormattedFlexCelDataLogExporter {

    private bool showAllData;

		public PdfDetailedReportExporter(IION ion, bool showAllData) : base(ion) {
      this.showAllData = showAllData;
		}

		public override Task<bool> Export(Stream stream, DataLogReport dlr) {
      return Task.Factory.StartNew(() => {
        try {
          // TODO ahodder@appioninc.com: Not necessary
          var scaleReduction = 60; // TODO DEFINE

          var file = new XlsFile((showAllData ? 2 : 1) + dlr.graphImages.Count, TExcelFileFormat.v2013, true);
          file.AllowOverwritingFiles = true;
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

            pdf.BeginExport(stream);

            for (int i = 1; i <= file.SheetCount; i++) {
              file.ActiveSheet = i;
              pdf.ExportSheet();
            }

            pdf.EndExport();
          }

          return true;
        } catch (Exception e) {
          Log.E(this, "Failed to export datalog report", e);
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
      size = DrawGraphs(file, dlr, row, 1);
      row += size.Item2;
      row += 2;

      ////////////
      // SHEET 2
      ////////////
      row = 1;
      // Draw raw data
      if (showAllData) {
        file.ActiveSheet++;;
        size = DrawAllMeasurements(file, dlr, row, 1);
      }
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
        file.AddImage(dlr.appionLogoPng.data, image);
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
		/// Draws the graphs for each of the devices.
		/// </summary>
		/// <returns>The graphs.</returns>
		/// <param name="file">File.</param>
		/// <param name="dlr">Dlr.</param>
		/// <param name="row">The x coordinate.</param>
		/// <param name="col">The y coordinate.</param>
    private Tuple<int, int> DrawGraphs(XlsFile file, DataLogReport dlr, int row, int col) {
			var l = dlr.localization;
      var imageCellWidth = 8;
      var imageCellHeight = 4;

      var index = 0;
			foreach (var sensor in dlr.sensors) {
        if (!dlr.graphImages.ContainsKey(sensor)) {
          Log.E(this, "Failed to export sensor graph");
          continue;          
        }

				int xoff = 0, yoff = 0;

				if (index > 0) {
          xoff = 1;
					yoff = 1;
				} else {
          xoff = col;
					yoff = row;
        }

				var image = dlr.graphImages[sensor];
				file.MergeCells(yoff, xoff, yoff + imageCellHeight, xoff + imageCellWidth);

				file.SetRowHeight(yoff, (int)(image.height * FlxConsts.RowMult));
				TClientAnchor anchor = new TClientAnchor(TFlxAnchorType.MoveAndDontResize, yoff, 0, xoff, 0, yoff + imageCellHeight, 0, xoff + imageCellWidth, 0);
				double width = 0.0, height = 0.0;

				anchor.CalcImageCoords(ref height, ref width, file);
				// Width should always be greater than the height.
				anchor = new TClientAnchor(TFlxAnchorType.MoveAndDontResize, yoff, 0, xoff, 0, (int)height, (int)width, file);


				var imageProperties = new TImageProperties();
				imageProperties.Anchor = anchor;
				file.AddImage(image.data, TXlsImgType.Png, imageProperties);
				index++;
				file.ActiveSheet++;
			}

      return new Tuple<int, int>(imageCellWidth, index * (imageCellHeight + 1));
		}
	}
}
