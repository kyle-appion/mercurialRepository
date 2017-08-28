using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Appion.Commons.Util;

using ION.Core.Database;
using ION.Core.IO;

using ION.Droid.Activity;
using ION.Droid.Dialog;
using ION.Droid.Fragments;
using ION.Droid.IO;
using ION.Droid.Views;
using ION.Droid.Widgets.RecyclerViews;

namespace ION.Droid.Activity.Report {

  [Activity(Label = "@string/reporting", Icon="@drawable/ic_nav_reporting", Theme="@style/AppTheme", LaunchMode=LaunchMode.SingleTask, ScreenOrientation=ScreenOrientation.Portrait)]
  public class ReportActivity : IONActivity {

    public const string EXTRA_SHOW_ARCHIVE_VIEW = "ION.Droid.Activity.Report.extra.SHOW_ARCHIVE_VIEW";
		public const string SPREADSHEETS = "spreadsheet";
		public const string PDF = "pdf";
    
    public const int REQUEST_REPORT = 1;

		private const string MIME_EXCEL = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
		private const string MIME_PDF = "application/pdf";
    
    public readonly HashSet<int> checkedSessions = new HashSet<int>();
    
    private ReportByJobFragment byJob;
    private ReportByDateFragment byDate;
    private FileBrowserFragment spreadsheets;
    private FileBrowserFragment pdfs;

    private Fragment activeFragment;
    
    private View newReport;
    private View savedReports;
    
    private TextView header;
    
    private Button tab1;
    private Button tab2;
    
    protected override void OnCreate(Bundle savedInstanceState) {
      base.OnCreate(savedInstanceState);
      SetContentView(Resource.Layout.activity_report);

      ActionBar.SetIcon(GetColoredDrawable(Resource.Drawable.ic_nav_reporting, Resource.Color.gray));
      ActionBar.SetDisplayHomeAsUpEnabled(true);
      ActionBar.SetHomeButtonEnabled(true);
      
      newReport = FindViewById(Resource.Id.report_new);
      savedReports = FindViewById(Resource.Id.report_saved);
      
      header = FindViewById<TextView>(Resource.Id.title);
      tab1 = FindViewById<Button>(Resource.Id.tab_1);
      tab2 = FindViewById<Button>(Resource.Id.tab_2);
      
      newReport.Click += (sender, e) => {
        NavigateToByJob();
      };
      
      savedReports.Click += (sender, e) => {
        NavigateToSpreadsheets();
      };
      
      NavigateToByJob();
    }
    
    protected override void OnActivityResult(int requestCode, Result resultCode, Intent data) {
      if (Result.Ok != resultCode) {
        base.OnActivityResult(requestCode, resultCode, data);
      }
      
      switch (requestCode) {
        case REQUEST_REPORT:
          if (data != null) {
            var archive = data.GetStringExtra(EXTRA_SHOW_ARCHIVE_VIEW);
            if (SPREADSHEETS.Equals(archive)) {
              NavigateToSpreadsheets();
            } else if (PDF.Equals(archive)) {
              NavigateToPdf();
            }
          }
          break;
        default:
          base.OnActivityResult(requestCode, resultCode, data);
          break;
      }
    }
    
    public override bool OnMenuItemSelected(int featureId, IMenuItem item) {
      switch (item.ItemId) {
        case Android.Resource.Id.Home:
          SetResult(Result.Canceled);
          Finish();
          return true;
        default:
					return base.OnMenuItemSelected(featureId, item);
			}
    }
    
    private void NavigateToByJob() {
      if (byJob == null) {
        byJob = new ReportByJobFragment(this);
      }
    
      newReport.SetBackgroundResource(Resource.Drawable.xml_tab_gold_white);
      savedReports.SetBackgroundResource(Resource.Drawable.xml_tab_gray_black);
      
      header.SetText(Resource.String.report_session_selection);
      header.SetBackgroundColor(Resource.Color.green.AsResourceColor(this));
      
      tab1.SetBackgroundResource(Resource.Drawable.xml_tab_white_light_blue);
      tab1.SetText(Resource.String.report_by_job);
      
      tab2.SetBackgroundResource(Resource.Drawable.xml_tab_gray_black);
      tab2.SetText(Resource.String.report_by_date);
      
      tab1.SetOnClickListener(new ViewClickAction((view) => {
        NavigateToByJob();
      }));
      
      tab2.SetOnClickListener(new ViewClickAction((view) => {
        NavigateToByDate();
      }));
      
      NavigateToFragment(byJob);
    }
    
    private void NavigateToByDate() {
      if (byDate == null) {
        byDate = new ReportByDateFragment(this);
      }
      
      newReport.SetBackgroundResource(Resource.Drawable.xml_tab_gold_white);
      savedReports.SetBackgroundResource(Resource.Drawable.xml_tab_gray_black);
      
      header.SetText(Resource.String.report_session_selection);
      header.SetBackgroundColor(Resource.Color.green.AsResourceColor(this));
      
      tab1.SetBackgroundResource(Resource.Drawable.xml_tab_gray_black);
      tab1.SetText(Resource.String.report_by_job);
      
      tab2.SetBackgroundResource(Resource.Drawable.xml_tab_white_light_blue);
      tab2.SetText(Resource.String.report_by_date);
      
      tab1.SetOnClickListener(new ViewClickAction((view) => {
        NavigateToByJob();
      }));
      
      tab2.SetOnClickListener(new ViewClickAction((view) => {
        NavigateToByDate();
      }));
      
      NavigateToFragment(byDate);
    }
    
    private void NavigateToSpreadsheets() {
      if (spreadsheets == null) {
        spreadsheets = new FileBrowserFragment(GetString(Resource.String.report_archive_empty));
        spreadsheets.folder = ion.dataLogReportFolder;
        spreadsheets.filter = new FileExtensionFilter(true, new string[] { FileExtensions.EXT_EXCEL, FileExtensions.EXT_CSV });
        spreadsheets.onFileClicked = (file) => {
          try {
            var i = new Intent(Intent.ActionView);
            i.SetDataAndType(Android.Net.Uri.FromFile(new Java.IO.File(file.fullPath)), MIME_EXCEL);
            i.SetFlags(ActivityFlags.NoHistory);
            StartActivity(Intent.CreateChooser(i, GetString(Resource.String.open_with)));
          } catch (Exception e) {
            Log.E(this, "Failed to start Excel activity chooser", e);
            var adb = new IONAlertDialog(this);
            adb.SetTitle(Resource.String.error_failed_to_open_file);
            adb.SetMessage(Resource.String.error_excel_viewer_missing);
            adb.SetNegativeButton(Resource.String.cancel, (sender, ex) => {
            });
            adb.Show();
          }
        };
      }
      newReport.SetBackgroundResource(Resource.Drawable.xml_tab_gray_black);
      savedReports.SetBackgroundResource(Resource.Drawable.xml_tab_gold_white);
      
      header.SetText(Resource.String.report_archive);
      header.SetBackgroundColor(Resource.Color.orange.AsResourceColor(this));
      
      tab1.SetBackgroundResource(Resource.Drawable.xml_tab_white_light_blue);
      tab1.SetText(Resource.String.spreadsheet);
      
      tab2.SetBackgroundResource(Resource.Drawable.xml_tab_gray_black);
      tab2.SetText(Resource.String.pdf);
      
      tab1.SetOnClickListener(new ViewClickAction((view) => {
        NavigateToSpreadsheets();
      }));
      
      tab2.SetOnClickListener(new ViewClickAction((view) => {
        NavigateToPdf();
      }));
      
      NavigateToFragment(spreadsheets);
    }
    
    private void NavigateToPdf() {
      if (pdfs == null) {
        pdfs = new FileBrowserFragment(GetString(Resource.String.report_archive_empty));
        pdfs.folder = ion.dataLogReportFolder;
        pdfs.filter = new FileExtensionFilter(true, new string[] { FileExtensions.EXT_PDF });
        pdfs.onFileClicked = (file) => {
          try {
            var i = new Intent(Intent.ActionView);
            i.SetDataAndType(Android.Net.Uri.FromFile(new Java.IO.File(file.fullPath)), MIME_PDF);
            i.SetFlags(ActivityFlags.NoHistory);
            StartActivity(Intent.CreateChooser(i, GetString(Resource.String.open_with)));
          } catch (Exception e) {
            Log.E(this, "Failed to start pdf activity chooser", e);
            var adb = new IONAlertDialog(this);
            adb.SetTitle(Resource.String.error_failed_to_open_file);
            adb.SetMessage(Resource.String.error_pdf_viewer_missing);
            adb.SetNegativeButton(Resource.String.cancel, (sender, ex) => {
            });
            adb.Show();
          }
        };
      }
      newReport.SetBackgroundResource(Resource.Drawable.xml_tab_gray_black);
      savedReports.SetBackgroundResource(Resource.Drawable.xml_tab_gold_white);
      
      header.SetText(Resource.String.report_archive);
      header.SetBackgroundColor(Resource.Color.orange.AsResourceColor(this));
      
      tab1.SetBackgroundResource(Resource.Drawable.xml_tab_gray_black);
      tab1.SetText(Resource.String.spreadsheet);
      
      tab2.SetBackgroundResource(Resource.Drawable.xml_tab_white_light_blue);
      tab2.SetText(Resource.String.pdf);
      
      tab1.SetOnClickListener(new ViewClickAction((view) => {
        NavigateToSpreadsheets();
      }));
      
      tab2.SetOnClickListener(new ViewClickAction((view) => {
        NavigateToPdf();
      }));
      
      NavigateToFragment(pdfs);
    }
    
    private void NavigateToFragment(Fragment frag) {
      var ft = FragmentManager.BeginTransaction();

      ft.Replace(Resource.Id.content, frag);
      ft.SetCustomAnimations(Resource.Animation.card_flip_left_in, Resource.Animation.card_flip_left_out);
      activeFragment = frag;

      ft.Commit();
    }
  }
}
