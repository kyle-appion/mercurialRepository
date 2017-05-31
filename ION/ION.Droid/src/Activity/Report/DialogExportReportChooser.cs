namespace ION.Droid.Activity.Report {

  using System;
  using System.IO;
  using System.Threading.Tasks;

	using Android.App;
	using Android.Content;
  using Android.Graphics;
  using Android.Graphics.Drawables;
	using Android.Views;
	using Android.Widget;

  using Appion.Commons.Util;

  using ION.Core.App;
  using ION.Core.IO;
  using ION.Core.Report.DataLogs;
  using ION.Core.Report.DataLogs.Exporter;

	using ION.Droid.App;
	using ION.Droid.Dialog;
  using ION.Droid.Report;
  using ION.Droid.Util;

	public class DialogExportReportChooser {

		private const string FILE_NAME = "DataLogReport";
		private const string EXCEL_EXT = ".xlsx";
		private const string CSV_EXT = ".xlsx";
		private const string PDF_EXT = ".pdf";

    private BaseAndroidION ion;
		private Context context;
    private DataLogReport report;

		public DialogExportReportChooser(BaseAndroidION ion, Context context, DataLogReport report) {
      this.ion = ion;
      this.context = context;
      this.report = report;
		}

		public Dialog Show() {
      var view = LayoutInflater.From(context).Inflate(Resource.Layout.dialog_report_export_chooser, null, false);

      var tabBar = view.FindViewById(Resource.Id.title);
      var pdfBuffer = tabBar.FindViewById(Resource.Id._1);
      var spreadsheetBuffer = tabBar.FindViewById(Resource.Id._2);

      var tab1 = view.FindViewById(Resource.Id.tab_1);
			var tab2 = view.FindViewById(Resource.Id.tab_2);
			var showingPdf = true;

			pdfBuffer.Click += (sender, e) => {
        tab1.Visibility = ViewStates.Visible;
        tab2.Visibility = ViewStates.Invisible;
				pdfBuffer.SetBackgroundResource(Resource.Drawable.tab_selected_ion_action_bar);
				spreadsheetBuffer.SetBackgroundResource(Resource.Drawable.tab_unselected_ion_action_bar);
				showingPdf = true;
			};

      spreadsheetBuffer.Click += (sender, e) => {
        tab1.Visibility = ViewStates.Invisible;
        tab2.Visibility = ViewStates.Visible;
			  pdfBuffer.SetBackgroundResource(Resource.Drawable.tab_unselected_ion_action_bar);
  		  spreadsheetBuffer.SetBackgroundResource(Resource.Drawable.tab_selected_ion_action_bar);
        showingPdf = false;
	    };

      pdfBuffer.PerformClick();

			var adb = new IONAlertDialog(context);
			adb.SetTitle(Resource.String.report_choose_export_format);
			adb.SetView(view);
			adb.SetNegativeButton(Resource.String.cancel, (sender, e) => {

			});
			adb.SetPositiveButton(Resource.String.export, (sender, e) => {
        string filename = null;
        IDataLogExporter exporter = null;

				var dateString = DateTime.Now.ToFullShortString();
				dateString = dateString.Replace('\\', '-');
				dateString = dateString.Replace('/', '-');

        if (showingPdf) {
					filename = FILE_NAME + "_" + dateString + PDF_EXT;

          var radio = tab1.FindViewById<RadioGroup>(Resource.Id.content);
          var checkbox = tab1.FindViewById<CheckBox>(Resource.Id.checkbox);
          switch (radio.CheckedRadioButtonId) {
            case Resource.Id._1:
              exporter = new SummaryPdfReportExporter(ion, checkbox.Checked);
              break;
            case Resource.Id._2:
              break;
          }
        } else {
					var radio = tab2.FindViewById<RadioGroup>(Resource.Id.content);
					switch (radio.CheckedRadioButtonId) {
						case Resource.Id._1:
              filename = FILE_NAME + "_" + dateString + EXCEL_EXT;
							break;
						case Resource.Id._2:
							filename = FILE_NAME + "_" + dateString + CSV_EXT;
							break;
					}
				}

        Export(filename, exporter);
			});
			var ret = adb.Create();
			ret.Show();
			return ret;
		}

    private async Task<bool> Export(string filename, IDataLogExporter exporter) {
      var folder = ion.dataLogReportFolder;
      var file = folder.GetFile(filename, EFileAccessResponse.ReplaceIfExists);

      bool success = false;
      using (var stream = file.OpenForWriting()) {
				success = await exporter.Export(stream, report);
			}

      if (!success) {
        file.Delete();
      }

      return success;
		}

		private enum ExportType {
			PdfOnePage,
			PdfDetailedSummary,
			Xlsx,
			Csv,
		}
	}
}