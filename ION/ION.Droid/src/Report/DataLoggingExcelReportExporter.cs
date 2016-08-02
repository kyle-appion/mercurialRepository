namespace ION.Droid.Report {

	using System;

	using FlexCel.Core;
	using FlexCel.Render;
	using FlexCel.XlsAdapter;

	using ION.Core.App;

	using ION.Droid.App;

	public class DataLoggingExcelReportExporter {
/*
		public static void Export(AndroidION ion, Dictionary<string, List<DeviceSensorLogs>> logs) {
		}
*/

		private static void DrawTable(AndroidION ion, XlsFile file) {
			var xls = new XlsFile(1, TExcelFileFormat.v2013, true);
			xls.AllowOverwritingFiles = true;

			var blackout = xls.GetDefaultFormat;
			blackout.VAlignment = TVFlxAlignment.top;
			blackout.HAlignment = THFlxAlignment.center;
			blackout.Font.Color = TExcelColor.FromIndex(2);
			blackout.FillPattern = new TFlxFillPattern {
				Pattern = TFlxPatternStyle.Solid, FgColor = TExcelColor.FromIndex(1),
			};

			var centerText = xls.GetDefaultFormat;
			centerText.VAlignment = TVFlxAlignment.top;
			centerText.HAlignment = THFlxAlignment.center;
			xls.AddFormat(centerText);

			var borderColor = xls.GetDefaultFormat;
			borderColor.Borders.Bottom.Color = TUIColor.FromArgb(0xff, 0x33, 0x33); // Red
			borderColor.Borders.Bottom.Style = TFlxBorderStyle.Medium;

			borderColor.VAlignment = TVFlxAlignment.top;
			borderColor.HAlignment = THFlxAlignment.center;
			xls.AddFormat(borderColor);


			// Draw Header
			xls.SetCellValue(1, 1, ion.GetString(Resource.String.time), 1);

//			for (int i = 0; i < 

		}
	}
}

