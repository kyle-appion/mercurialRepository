using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

using Appion.Commons.Util;

using ION.Core.Database;
using ION.Core.Devices;
using ION.Core.IO;
using ION.Core.Report.DataLogs;
using ION.Core.Report.DataLogs.Exporter;
using ION.Core.Sensors;
using ION.Core.UI;

using ION.CoreExtensions.IO;

using ION.Droid.Activity;
using ION.Droid.IO;
using ION.Droid.Dialog;
using ION.Droid.Util;

namespace ION.Droid.Activity.Report {
	[Activity(Label = "@string/reporting", Icon="@drawable/ic_nav_reporting", Theme = "@style/AppTheme", LaunchMode = LaunchMode.SingleTask, ScreenOrientation = ScreenOrientation.Portrait)]
	public class ReportGraphActivity : IONActivity {
    /// <summary>
    /// The extra key used to retrieve an integer array of session ids.
    /// </summary>
    public const string EXTRA_SESSIONS = "ION.Droid.Activity.Report.New.extra.SESSIONS";

    /// <summary>
    /// The start time for exporting.
    /// </summary>
    private EditText _startTimeView;
    /// <summary>
    /// The end time for exporting.
    /// </summary>
    private EditText _endTimeView;
    /// <summary>
    /// The view that will toggle the adapter into displaying the graphs.
    /// </summary>
    private View _graphView;
    /// <summary>
    /// The view that will toggle the adapter into displaying the graph details.
    /// </summary>
    private View _numericView;
    /// <summary>
    /// The view that will display the size of the adapter (number of sensors).
    /// </summary>
    private TextView _sensorsCountView;
    /// <summary>
    /// The view that will display the number of sensors that are present in the adapter.
    /// </summary>
    private TextView _sensorsView;
    /// <summary>
    /// The list that will display the graphs and highlight data to the user. This view will also have an overlay
    /// rendered on top of it via the <code>_overlay</code> drawable.
    /// <summary>
    /// <seealso cref="_overlay"/>
    private RecyclerView _list;

		/// <summary>
		/// The list of session ids that are provided to the activity for display.
		/// </summary>
		private List<int> _sessionIds;
    /// <summary>
    /// The adapter that will display the list of report items.
    /// </summary>
    private ReportGraphAdapter _adapter;

    protected override void OnCreate(Bundle savedInstanceState) {
      base.OnCreate(savedInstanceState);

      SetContentView(Resource.Layout.activity_report_graph_sessions);

      ActionBar.SetIcon(GetColoredDrawable(Resource.Drawable.ic_nav_reporting, Resource.Color.gray));
			ActionBar.SetDisplayHomeAsUpEnabled(true);
			ActionBar.SetHomeButtonEnabled(true);

      InitViews();

      _sessionIds = new List<int>(Intent.GetIntArrayExtra(EXTRA_SESSIONS));
      if (_sessionIds.Count <= 0) {
        Error(Resource.String.report_error_graph_no_sessions_provided);
        SetResult(Result.Canceled);
        Finish();
      }
		}

    protected override void OnResume() {
      base.OnResume();

      InflateAdapter();
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

    private async Task InflateAdapter() {
			var dialog = new ProgressDialog(this);
			dialog.SetTitle(Resource.String.loading);
			dialog.SetMessage(GetString(Resource.String.please_wait));
			dialog.Show();

			try {
				var dil = DateIndexLookup.CreateFromSessionsAsync(ion, _sessionIds);
				var results = ion.dataLogManager.QuerySensorResultsAsync(_sessionIds);
				_adapter = new ReportGraphAdapter(ion,
                                          await dil,
                                          await results,
                                          FindViewById(Resource.Id.report_handles),
                                          FindViewById(Resource.Id.left),
                                          FindViewById(Resource.Id.right));
				_adapter.onOverlayDragEvent += OnOverlayDragEvent;
				_sensorsCountView.Text = _adapter.ItemCount + "";
				_list.SetAdapter(_adapter);
        SetDetailMode(false);
        Reset();
			} catch (Exception e) {
				Error(Resource.String.report_error_failed_to_load_sessions, e);
				SetResult(Result.Canceled);
				Finish();
			} finally {
				dialog.Dismiss();
			}
    }

    private void InitViews() {
      _startTimeView = FindViewById<EditText>(Resource.Id.start_times);
			_endTimeView = FindViewById<EditText>(Resource.Id.end_times);
      _sensorsCountView = FindViewById<TextView>(Resource.Id.count);

			_graphView = FindViewById(Resource.Id.report_graph);
      _numericView = FindViewById(Resource.Id.report_numeric);

      // Initialize list and overlay
      _list = FindViewById<RecyclerView>(Resource.Id.list);

      // Initialize Reset
      FindViewById(Resource.Id.reset).Click += (sender, arg) => Reset();

      // Initialize Export
      FindViewById(Resource.Id.export).Click += (sender, arg) => PrepareExport();

      _graphView.Click += (sender, e) => SetDetailMode(false);
			_numericView.Click += (sender, e) => SetDetailMode(true);

      FindViewById(Resource.Id.settings).Click += (sender, e) => ShowSettings();
      FindViewById(Resource.Id.reset).Click += (sender, e) => Reset();
      FindViewById(Resource.Id.export).Click += (sender, e) => PrepareExport();
    }

    /// <summary>
    /// Sets whether or not the adapter will display the graph details. Also changing the tabs background to reflect the
    /// current adapter state.
    /// </summary>
    /// <param name="showDetails">If set to <c>true</c> show details.</param>
    private void SetDetailMode(bool showDetails) {
      _adapter.showDetails = showDetails;

      if (showDetails) {
        _numericView.SetBackgroundResource(Resource.Drawable.xml_tab_gold_white);
				_graphView.SetBackgroundResource(Resource.Drawable.xml_tab_gray_black);
      } else {
				_graphView.SetBackgroundResource(Resource.Drawable.xml_tab_gold_white);
				_numericView.SetBackgroundResource(Resource.Drawable.xml_tab_gray_black);
      }
    }

    private void OnOverlayDragEvent() {
			var dil = _adapter.dil;
      var cnt = dil.count;
      var startDate = dil.GetDateTimeFromIndex(Math.Min(cnt - 1, (int)(cnt * _adapter.leftPercent)));
      var endDate = dil.GetDateTimeFromIndex(Math.Min(cnt - 1, (int)(dil.count * _adapter.rightPercent)));

      _startTimeView.Text = startDate.ToShortDateString() + " " + startDate.ToLongTimeString();
      _endTimeView.Text = endDate.ToShortDateString() + " " + endDate.ToLongTimeString();
      _adapter.SetStartEndDates(startDate, endDate);
    }

    private void ShowSettings() {
      var adb = new IONAlertDialog(this);
      adb.SetTitle(Resource.String.report_settings);

      var view = LayoutInflater.From(this).Inflate(Resource.Layout.dialog_report_settings, null, false);
      var pressure = view.FindViewById<Button>(Resource.Id.pressure);
      pressure.Text = prefs.units.pressure.ToString();
      pressure.Click += (sender, e) => {
        UnitDialog.Create(this, SensorUtils.DEFAULT_PRESSURE_UNITS, (s, u) => {
          prefs.units.pressure = u;
          pressure.Text = prefs.units.pressure.ToString();
        }).Show();
      };

      var temperature = view.FindViewById<Button>(Resource.Id.temperature);
      temperature.Text = prefs.units.temperature.ToString();
      temperature.Click += (sender, e) => {
        UnitDialog.Create(this, SensorUtils.DEFAULT_TEMPERATURE_UNITS, (s, u) => {
          prefs.units.temperature = u;
          temperature.Text = prefs.units.temperature.ToString();
        }).Show();
      };

      var vacuum = view.FindViewById<Button>(Resource.Id.vacuum);
      vacuum.Text = prefs.units.vacuum.ToString();
      vacuum.Click += (sender, e) => {
        UnitDialog.Create(this, SensorUtils.DEFAULT_VACUUM_UNITS, (s, u) => {
          prefs.units.pressure = u;
          vacuum.Text = prefs.units.vacuum.ToString();
        }).Show();
      };

      adb.SetView(view);
      adb.SetCancelable(true);
      adb.Show();
    }

		/// <summary>
		/// Resets the state of the activity. The overlay will go back to the edges, the times will go back to full start
		/// and end.
		/// </summary>
		private void Reset() {
      _adapter.Reset();
		}

    private void PrepareExport() {
			var view = LayoutInflater.From(this).Inflate(Resource.Layout.dialog_report_export_chooser, null, false);

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

			var adb = new IONAlertDialog(this);
			adb.SetTitle(Resource.String.report_choose_export_format);
			adb.SetView(view);
			adb.SetNegativeButton(Resource.String.cancel, (sender, e) => { });

      adb.SetPositiveButton(Resource.String.export, (sender, e) => {
        string ext = "";
        IDataLogExporter exporter = null;
        Intent successIntent = new Intent();

        if (showingPdf) {
          ext = FileExtensions.EXT_PDF;
          var isChecked = tab1.FindViewById<CheckBox>(Resource.Id.checkbox).Checked;
          switch (tab1.FindViewById<RadioGroup>(Resource.Id.content).CheckedRadioButtonId) {
            case Resource.Id._1:
              exporter = new PdfReportExporter(ion, isChecked);
              break;
				    case Resource.Id._2:
              exporter = new PdfDetailedReportExporter(ion, isChecked);
              break;
			    }
          successIntent.PutExtra(ReportActivity.EXTRA_SHOW_SAVED_PDF, true);
        } else {
          var radio = tab2.FindViewById<RadioGroup>(Resource.Id.content);
          switch (radio.CheckedRadioButtonId) {
            case Resource.Id._1:
              ext = FileExtensions.EXT_EXCEL;
              exporter = new ExcelReportExporter(ion);
              break;

            case Resource.Id._2:
  					  ext = FileExtensions.EXT_EXCEL;
              exporter = new CsvExporter(ion);
  					  break;
			    }
  			  successIntent.PutExtra(ReportActivity.EXTRA_SHOW_SAVED_SPREADSHEETS, true);
  		  }

        Export(ext, exporter, successIntent);
  	  });

      adb.Show();
		}

    private Task Export(string fileExtension, IDataLogExporter exporter, Intent successIntent) {
      Alert("Not completed");
      return Task.FromResult(new Object());
/*
      var progress = new ProgressDialog(this);
      progress.SetTitle(Resource.String.please_wait);
      progress.SetMessage(GetString(Resource.String.saving));
      progress.Show();

      return Task.Factory.StartNew(async () => {
        // Query the jobs that are included in this report.
        var sessionIds = new HashSet<int>();
        foreach (var )

        ion.database.Table<JobRow>().

        // Build the data log report proper
        var dlr = new DataLogReport();



  		  // Build the file name for the report
  		  var dateString = DateTime.Now.ToFullShortString().Replace('\\', '-').Replace('/', '_');
  		  var filename = GetString(Resource.String.report_data_logging_title) + " " + dateString + " " + fileExtension;
  		  var success = false;
        IFile file = null;

		    try {
          var folder = ion.dataLogReportFolder;
          file = folder.GetFile(filename, EFileAccessResponse.ReplaceIfExists);

          using (var stream = file.OpenForReading()) {
            success = await exporter.Export(stream, dlr);
          }
        } finally {
          if (!success) {
            try {
              file.Delete();
            } catch (Exception e) {
              Log.E(this, "Failed to delete file after data log export failed", e);
            }
          }
        }
      }).ContinueWith((arg) => {
        progress.Dismiss();
        if (arg.Result.Exception == null) {
          SetResult(Result.Result.Ok, successIntent);
          Finish();
        } else {
          Error(GetString(Resource.String.report_error), arg.Result.Exception);
        }
      }, TaskScheduler.FromCurrentSynchronizationContext());
*/
    }
  }
}
