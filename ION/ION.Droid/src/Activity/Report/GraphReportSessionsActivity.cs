namespace ION.Droid.Activity.Report {

	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Threading.Tasks;

	using Android.Animation;
	using Android.App;
	using Android.Content;
	using Android.Content.PM;
	using Android.Graphics;
	using Android.Graphics.Drawables;
	using Android.OS;
	using Android.Support.V7.Widget;
	using Android.Views;
	using Android.Widget;

	using OxyPlot;
	using OxyPlot.Axes;
	using OxyPlot.Series;
	using OxyPlot.Xamarin.Android;

	using Appion.Commons.Measure;
	using Appion.Commons.Util;

	using ION.Core.Devices;
	using ION.Core.IO;
	using ION.Core.Report.DataLogs;
	using ION.Core.Report.DataLogs.Exporter;
	using ION.Core.Sensors;
  using ION.Core.UI;

  using ION.Droid.App;
	using ION.Droid.Dialog;
  using ION.Droid.IO;
	using ION.Droid.Report;
	using ION.Droid.Util;
	using ION.Droid.Views;

	[Activity(Label="@string/report_select_export_data", Icon="@drawable/ic_nav_reporting", Theme="@style/AppTheme", ScreenOrientation=ScreenOrientation.Portrait)]
	public class GraphReportSessionsActivity : IONActivity {
		/// <summary>
		/// The extra that will retrieve an integer array from the starting Intent. This list is what is used to populate the
		/// activity's session list.
		/// </summary>
		public const string EXTRA_SESSIONS = "ION.Droid.extra.SESSIONS";

		private const string FILE_NAME = "DataLogReport";

		/// <summary>
		/// The container view that will hold all of the graphing views.
		/// </summary>
		private View graphView;
		/// <summary>
		/// The container view that will hold all of the settins views.
		/// </summary>
		private View settingsView;
		private View leftHandle;
		private View rightHandle;
		/// <summary>
		/// The text view that will show the date range of the graph.
		/// </summary>
		private TextView dateRangeView;

		/// <summary>
		/// The spinner that will display the start dates.
		/// </summary>
		private Spinner startDateSpinner;
		/// <summary>
		/// The spinner that will display the end dates.
		/// </summary>
		private Spinner endDateSpinner;

		/// <summary>
		/// The adapter that will shown the logs graphs.
		/// </summary>
		private GraphRecordAdapter graphAdapter;
		/// <summary>
		/// The adapter that will list the overview of the graphs.
		/// </summary>
		private OverviewAdapter overviewAdapter;
		/// <summary>
		/// The adapter that will list the available start times.
		/// </summary>
		private DateTimeAdapter startTimesAdapter;
		/// <summary>
		/// The adapter that will list the available end times.
		/// </summary>
		private DateTimeAdapter endTimesAdapter;
		/// <summary>
		/// The drawable that shows the ignored content of the handle selections.
		/// </summary>
		private ReportOverlay overlay;
		/// <summary>
		/// The base rect that is used to measure the plot sizes of the graphs
		/// </summary>
		private Rect rect;

		/// <summary>
		/// The sessions that the activity is graphing.
		/// </summary>
		private List<int> sessions;

		private DateTime startDate;
		private DateTime endDate;

		protected override void OnCreate(Bundle savedInstanceState) {
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.activity_graph_report_sessions);

			ActionBar.SetDisplayHomeAsUpEnabled(true);
			ActionBar.SetHomeButtonEnabled(true);
			ActionBar.SetIcon(GetColoredDrawable(Resource.Drawable.ic_nav_reporting, Resource.Color.gray));

			if (!Intent.HasExtra(EXTRA_SESSIONS)) {
				Error(GetString(Resource.String.report_error_graph_no_sessions_provided));
				SetResult(Result.Canceled);
				Finish();
				return;
			}

			graphView = FindViewById(Resource.Id.graph);
			settingsView = FindViewById(Resource.Id.settings);

			FindViewById(Resource.Id.reset).Click += (sender, e) => {
			};

			FindViewById(Resource.Id.export).Click += (sender, e) => {
				Export();
			};

			sessions = new List<int>(Intent.GetIntArrayExtra(EXTRA_SESSIONS));

			InitGraphViews();
			InitOptionsViews();
		}

		protected override void OnResume() {
			base.OnResume();
			ReloadGraphList();
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

    private void InitGraphViews() {
			dateRangeView = FindViewById<TextView>(Resource.Id.date);
			var showSettingsView = FindViewById(Resource.Id.settings);
			var list = FindViewById<RecyclerView>(Resource.Id.list);

			leftHandle = FindViewById(Resource.Id.left);
			rightHandle = FindViewById(Resource.Id.right);
			var container = FindViewById(Resource.Id.view);

			var icon = graphView.FindViewById(Resource.Id.icon);
			icon.SetOnClickListener(new ViewClickAction((view) => {
				AnimateToOptionsView();
			}));

			graphAdapter = new GraphRecordAdapter();
			list.SetAdapter(graphAdapter);

      var color = Android.Graphics.Color.Gray;
      color.A = (byte)(.3f * 0xff);
      overlay = new ReportOverlay(list, container, leftHandle, rightHandle, color);
      overlay.OnOverlayDragEvent += () => {
        UpdateDates();
      };

			list.Overlay.Add(overlay);
		}

		private void InitOptionsViews() {
			var table = FindViewById(Resource.Id.table);
			var pressure = table.FindViewById(Resource.Id.pressure);
			var temperature = table.FindViewById(Resource.Id.temperature);
			var vacuum = table.FindViewById(Resource.Id.vacuum);

			var pressureUnitButton = pressure.FindViewById<Button>(Resource.Id.unit);
			var temperatureUnitButton = temperature.FindViewById<Button>(Resource.Id.unit);
			var vacuumUnitButton = vacuum.FindViewById<Button>(Resource.Id.unit);

			var icon = settingsView.FindViewById(Resource.Id.icon);
			var list = settingsView.FindViewById<RecyclerView>(Resource.Id.list);

			startDateSpinner = settingsView.FindViewById<Spinner>(Resource.Id.start_times);
			endDateSpinner = settingsView.FindViewById<Spinner>(Resource.Id.end_times);

      // Initialize unit buttons
			pressureUnitButton.Text = ion.preferences.units.pressure.ToString();
			pressureUnitButton.SetOnClickListener(new ViewClickAction((view) => {
				UnitDialog.Create(this, SensorUtils.DEFAULT_PRESSURE_UNITS, (sender, e) => {
          ion.preferences.units.pressure = e;
				}).Show();
			}));

      temperatureUnitButton.Text = ion.preferences.units.temperature.ToString();
			temperatureUnitButton.SetOnClickListener(new ViewClickAction((view) => {
				UnitDialog.Create(this, SensorUtils.DEFAULT_TEMPERATURE_UNITS, (sender, e) => {
          ion.preferences.units.temperature = e;
				}).Show();
			}));

			vacuumUnitButton.Text = ion.preferences.units.vacuum.ToString();
			vacuumUnitButton.SetOnClickListener(new ViewClickAction((view) => {
				UnitDialog.Create(this, SensorUtils.DEFAULT_VACUUM_UNITS, (sender, e) => {
          ion.preferences.units.vacuum = e;
				}).Show();
			}));

      // Initialize state flip button
			icon.SetOnClickListener(new ViewClickAction((view) => {
				AnimateToGraphView();
			}));

      // Initialize adapter.
			overviewAdapter = new OverviewAdapter();
			list.SetAdapter(overviewAdapter);
		}

    /// <summary>
    /// Updates the date time text view and the adapters for the date spinner
    /// </summary>
    private void UpdateDates() {
      var dil = graphAdapter.dil;

      var startIndex = (int)Math.Max(overlay.leftPercent * (dil.dateSpan - 1), 0);
      var endIndex = (int)Math.Max((1 - overlay.rightPercent) * (dil.dateSpan - 1), 0);
      var noMansIndices = overlay.CalculateNoMansIndices(dil);

      startDate = dil.DateFromIndex(startIndex);
			endDate = dil.DateFromIndex(endIndex);

      // Update the dates for the date spinners
      var startDates = dil.Subset(0, endIndex - noMansIndices);
      var endDates = dil.Subset(startIndex + noMansIndices + 1, dil.dateSpan - 1);

			startDates.Sort();
      endDates.Sort();

      // Update the options spinners
      startDateSpinner.ItemSelected -= OnDateSpinnerChanged;
      endDateSpinner.ItemSelected -= OnDateSpinnerChanged;

      startDateSpinner.Adapter = startTimesAdapter = new DateTimeAdapter(this, startDates);
			endDateSpinner.Adapter = endTimesAdapter = new DateTimeAdapter(this, endDates);

      startDateSpinner.SetSelection(startDates.IndexOf(startDate));
			endDateSpinner.SetSelection(endDates.IndexOf(endDate));

			startDateSpinner.ItemSelected += OnDateSpinnerChanged;
			endDateSpinner.ItemSelected += OnDateSpinnerChanged;

      UpdateDateViews();
		}

    /// <summary>
    /// Updates the graphing date range view to the new start and end dates.
    /// </summary>
    private void UpdateDateViews() {
			var encaps = BuildSensorReportEncapsulations(graphAdapter.GatherSelectedLogs(startDate, endDate));
			overviewAdapter.SetLogs(encaps.Item2);

			dateRangeView.Text = GetString(Resource.String.start) + ": " + startDate.ToShortDateString() + " " + startDate.ToLongTimeString() + "\n" +
			  GetString(Resource.String.finish) + ": " + endDate.ToShortDateString() + " " + endDate.ToLongTimeString();
    }

    private void OnDateSpinnerChanged(object sender, AdapterView.ItemSelectedEventArgs args) {
      startDate = startTimesAdapter[startDateSpinner.SelectedItemPosition];
      endDate = endTimesAdapter[endDateSpinner.SelectedItemPosition];
      UpdateDateViews();
      // Update the handles to reflect the new start and end dates
      overlay.UpdateHandlesTo(graphAdapter.dil, startDate, endDate);
    }

    private async Task ReloadGraphList() {
			var dialog = new ProgressDialog(this);
			dialog.SetTitle(Resource.String.please_wait);
			dialog.SetMessage(GetString(Resource.String.loading));
			dialog.SetCancelable(false);
			dialog.Show();

			var sessionResults = new List<SessionResults>();

			foreach (var id in sessions) {
				try {
					sessionResults.Add(await ion.dataLogManager.QuerySessionDataAsync(id));
				} catch (Exception e) {
					Log.E(this, "Failed to query session data for id {" + id + "}", e);
				}
			}

      var tuple = BuildSensorReportEncapsulations(sessionResults);

      graphAdapter.SetRecords(sessionResults, tuple.Item1, tuple.Item2);
			overviewAdapter.SetLogs(tuple.Item2);

			var content = FindViewById(Resource.Id.content);
			var empty = FindViewById(Resource.Id.empty);
      if (graphAdapter.ItemCount > 0) {
        content.Visibility = ViewStates.Visible;
				empty.Visibility = ViewStates.Gone;
      } else {
				content.Visibility = ViewStates.Gone;
				empty.Visibility = ViewStates.Visible;
      }

			dialog.Dismiss();

			UpdateDates();
		}

		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // BELOW HERE IS PRIMARILY METHODS PERTAINING TO EXPORTING A REPORT.
		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Creates a complete representation of sensor graphs.
		/// </summary>
		/// <returns>The sensor report encapsulations.</returns>
		// TODO ahodder@appioninc.com: This needs to be optimized as we are allocating a truely fucktastic amount of memory to make this work.
		private Tuple<DateIndexLookup, List<SensorReportEncapsulation>> BuildSensorReportEncapsulations(List<SessionResults> sessionResults) {
			var dateLookupTable = new HashSet<DateTime>();
			var map = new Dictionary<GaugeDeviceSensor, List<PointSeries>>();

			foreach (var sr in sessionResults) {
				foreach (var dsl in sr.deviceSensorLogs) {
					// Find the gauge device sensor.
					if (!dsl.deviceSerialNumber.IsValidSerialNumber()) {
						Log.E(this, "Failed to parse serial number {" + dsl.deviceSerialNumber + "} for report");
						continue;
					}

					var sn = dsl.deviceSerialNumber.ParseSerialNumber();
					var device = ion.deviceManager[sn] as GaugeDevice;
					if (device == null) {
						Log.E(this, "Failed to find gauge device {" + sn + "} in device manager");
						continue;
					}

					var sensor = device[dsl.index];

					// Gather the dates and measurements
					for (int i = 0; i < dsl.logs.Count; i++) {
						dateLookupTable.Add(dsl.logs[i].recordedDate);
					}

					List<PointSeries> list;
					map.TryGetValue(sensor, out list);
					if (list == null) {
						list = new List<PointSeries>();
						map[sensor] = list;
					}

					list.Add(new PointSeries(dsl));
				}
			}

			var sortedDates = new List<DateTime>(dateLookupTable);
			sortedDates.Sort();
			var dil = new DateIndexLookup(sortedDates);
			var encaps = new List<SensorReportEncapsulation>();
			foreach (var sensor in map.Keys) {
				encaps.Add(new SensorReportEncapsulation(sensor, dil, map[sensor].ToArray()));
			}

			return new Tuple<DateIndexLookup, List<SensorReportEncapsulation>>(dil, encaps);
		}

		private void Export() {
      var results = graphAdapter.GatherSelectedLogs(startDate, endDate);
      var dlr = DataLogReport.BuildFromSessionResults(ion, new DataLogReportLocalization(this), startDate, endDate, results);
			dlr.reportName = GetString(Resource.String.report_data_logging_title);

			try {
				var drawable = Resources.GetDrawable(Resource.Drawable.img_logo_appionblack) as BitmapDrawable;

				using (var ms = new MemoryStream(512)) {
					drawable.Bitmap.Compress(Bitmap.CompressFormat.Png, 100, ms);
					dlr.appionLogoPng = ms.ToArray();
				}

				drawable.Bitmap.Recycle();
				drawable.Dispose();
			} catch (Exception e) {
				Log.E(this, "Failed to load application image for report", e);
			}

			ShowExportDialog(dlr);
		}

		/// <summary>
		/// Creates simple graphs for the the reports.
		/// </summary>
		/// <returns>The detailed graphs.</returns>
    private Dictionary<GaugeDeviceSensor, IonImage> CaptureGraphs(DataLogReport dlr, IGraphRenderer renderer) {
			var ret = new Dictionary<GaugeDeviceSensor, IonImage>();

			var tuple = BuildSensorReportEncapsulations(dlr.sessionResults);

			foreach (var encap in tuple.Item2) {
				var bitmap = Bitmap.CreateBitmap(800, 400, Bitmap.Config.Argb8888);
				try {
					var canvas = new Canvas(bitmap);
					renderer.Render(canvas, encap);

					using (var ms = new MemoryStream(128)) {
						bitmap.Compress(Bitmap.CompressFormat.Png, 100, ms);
						ret[encap.sensor] = new IonImage(IonImage.EType.Png, bitmap.Width, bitmap.Height, ms.ToArray());
					}
				} finally {
					bitmap.Recycle();
					bitmap.Dispose();
				}
			}

			return ret;
		}

    /// <summary>
    /// Shows the dialog that will allow the user to select the type of report that they are going to export.
    /// </summary>
    /// <param name="dlr">Dlr.</param>
    private void ShowExportDialog(DataLogReport dlr) {
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
			adb.SetNegativeButton(Resource.String.cancel, (sender, e) => {

			});

			adb.SetPositiveButton(Resource.String.export, async (sender, e) => {
				string filename = null;
				IDataLogExporter exporter = null;
				var successIntent = new Intent();

				var dateString = DateTime.Now.ToFullShortString();
				dateString = dateString.Replace('\\', '-');
				dateString = dateString.Replace('/', '-');

				var progress = new ProgressDialog(this);
				progress.SetTitle(Resource.String.please_wait);
				progress.SetMessage(GetString(Resource.String.saving));
				progress.Show();

        bool result = false;

        try {
          if (showingPdf) {
            filename = FILE_NAME + "_" + dateString + FileExtensions.EXT_PDF;

            var radio = tab1.FindViewById<RadioGroup>(Resource.Id.content);
            var checkbox = tab1.FindViewById<CheckBox>(Resource.Id.checkbox);
            switch (radio.CheckedRadioButtonId) {
              case Resource.Id._1:
                exporter = new PdfReportExporter(ion, checkbox.Checked);
                dlr.graphImages = CaptureGraphs(dlr, new SimpleGraphRenderer(this, ion));
                break;
              case Resource.Id._2:
                exporter = new PdfDetailedReportExporter(ion, checkbox.Checked);
								dlr.graphImages = CaptureGraphs(dlr, new DetailedGraphRenderer(this, ion));
								break;
            }
            successIntent.PutExtra(ReportActivity.EXTRA_SHOW_SAVED_PDF, true);
          } else {
            var radio = tab2.FindViewById<RadioGroup>(Resource.Id.content);
            switch (radio.CheckedRadioButtonId) {
              case Resource.Id._1:
                filename = FILE_NAME + "_" + dateString + FileExtensions.EXT_EXCEL;
                exporter = new ExcelReportExporter(ion);
                break;
              case Resource.Id._2:
                filename = FILE_NAME + "_" + dateString + FileExtensions.EXT_CSV;
                exporter = new CsvExporter(ion);
                break;
            }
            successIntent.PutExtra(ReportActivity.EXTRA_SHOW_SAVED_SPREADSHEETS, true);
          }

					result = await PerformDataLogExport(filename, dlr, exporter);
				} catch (Exception ex) {
					Log.E(this, "Failed to export", ex);
				}

				progress.Dismiss();

				if (result) {
          SetResult(Result.Ok, successIntent);
					Finish();
				} else {
					Toast.MakeText(this, Resource.String.report_screenshot_error_export_failed, ToastLength.Long).Show();
				}

			});
      adb.Show();
    }

    private async Task<bool> PerformDataLogExport(string filename, DataLogReport dlr, IDataLogExporter exporter) {
			var folder = ion.dataLogReportFolder;
			var file = folder.GetFile(filename, EFileAccessResponse.ReplaceIfExists);

			bool success = false;
			using (var stream = file.OpenForWriting()) {
				success = await exporter.Export(stream, dlr);
			}

			if (!success) {
				file.Delete();
			}

			return success;
		}

		private void AnimateToOptionsView() {
			FlipView(FindViewById(Resource.Id.content), Resource.Id.graph, Resource.Id.settings);
		}

		private void AnimateToGraphView() {
			FlipView(FindViewById(Resource.Id.content), Resource.Id.settings, Resource.Id.graph);
		}

		/// <summary>
		/// Flips a view, hiding one view and revealing another as the flip finished.
		/// </summary>
		/// <param name="view">View.</param>
		/// <param name="hideId">Hide identifier.</param>
		/// <param name="revealId">Reveal identifier.</param>
		private void FlipView(View view, int hideId, int revealId) {
			var fo = AnimatorInflater.LoadAnimator(this, Resource.Animation.card_flip_left_out);
			var fi = AnimatorInflater.LoadAnimator(this, Resource.Animation.card_flip_left_in);
			var fadeOut = AnimatorInflater.LoadAnimator(this, Resource.Animation.fade_out);
			var fadeIn = AnimatorInflater.LoadAnimator(this, Resource.Animation.fade_in);

			var hideView = view.FindViewById(hideId);
			var revealView = view.FindViewById(revealId);

			fo.SetTarget(view);
			fi.SetTarget(view);
			fadeOut.SetTarget(hideView);
			fadeIn.SetTarget(revealView);

			var animOut = new AnimatorSet();
			animOut.Play(fo);
			animOut.SetDuration(350);
			animOut.AnimationEnd += (obj, args) => {
				hideView.Visibility = ViewStates.Gone;
				revealView.Visibility = ViewStates.Visible;
			};

			var animIn = new AnimatorSet();
			animIn.Play(fi);
			animIn.SetDuration(350);

			var animationSet = new AnimatorSet();
			animationSet.AnimationEnd += (obj, args) => {
				hideView.Visibility = ViewStates.Gone;
				revealView.Visibility = ViewStates.Visible;
			};
			animationSet.Play(animOut)
						.Before(animIn);
			animationSet.Start();
		}
	}

  class DateTimeAdapter : ArrayAdapter<string> {
    public List<DateTime> dates;
    public DateTime this[int position] { get { return dates[position]; } }

    public DateTimeAdapter(Context context, List<DateTime> dates) : base(context, Android.Resource.Layout.SimpleSpinnerItem, DatesToStrings(dates)) {
			this.dates = dates;
		}

		private static List<string> DatesToStrings(List<DateTime> dates) {
			var ret = new List<string>();

			foreach (var date in dates) {
				ret.Add(date.ToShortDateString() + " " + date.ToLongTimeString());
			}

			return ret;
		}
	}
}
