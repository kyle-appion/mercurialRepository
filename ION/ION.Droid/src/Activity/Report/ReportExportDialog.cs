namespace ION.Droid.Activity.Report {

  using Android.App;
  using Android.Content;
  using Android.Views;
  using Android.Widget;

  using ION.Droid.Dialog;

  public class ReportExportDialog {

    private Context context;

    public ReportExportDialog(Context context) {

    }

    public Dialog Show() {
      var view = LayoutInflater.From(context).Inflate(Resource.Layout.dialog_report_export_options, null, false);

      var tabHost = view.FindViewById<TabHost>(Android.Resource.Id.TabHost);
      var tab1 = view.FindViewById(Resource.Id.tab_1);
      var tab2 = view.FindViewById(Resource.Id.tab_2);

      var adb = new IONAlertDialog(context);
      adb.SetTitle(Resource.String.report_choose_export_format);
      adb.SetView(view);
      adb.SetNegativeButton(Resource.String.cancel, (sender, e) => {

      });
      adb.SetPositiveButton(Resource.String.export, (sender, e) => {
        Toast.MakeText(context, "EXPORT", ToastLength.Long).Show();
      });
      var ret = adb.Create();
      ret.Show();
      return ret;
    }


    private enum ExportType {
      PdfOnePage,
      PdfDetailedSummary,
      Xlsx,
      Csv,
    }
  }
}