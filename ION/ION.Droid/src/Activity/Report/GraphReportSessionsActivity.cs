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
		private const string EXCEL_EXT = ".xlsx";
		private const string PDF_EXT = ".pdf";

		/// <summary>
		/// The view that will display the content of the activity.
		/// </summary>
		private View content;
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
		/// The view that, when clicked, will flip the view over to show the settings.
		/// </summary>
		private View showSettingsView;
		/// <summary>
		/// The recycler view that will list the graphing components.
		/// </summary>
		private RecyclerView list;

		/// <summary>
		/// The button that is used to select the pressure unit for the reports.
		/// </summary>
		private Button pressureUnitButton;
		/// <summary>
		/// The button taht is used to select the temperature unit for the reports.
		/// </summary>
		private Button temperatureUnitButton;
		/// <summary>
		/// The button that is used to select the vacuum unit for the reports.
		/// </summary>
		private Button vacuumUnitButton;
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
		/// The drawable that shows the ignored content of the left selection.
		/// </summary>
		private SelectionDrawable leftOverlay;
		/// <summary>
		/// The drawable that shows the ignored content of the right selection.
		/// </summary>
		private SelectionDrawable rightOverlay;
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

			content = FindViewById(Resource.Id.content);
			graphView = FindViewById(Resource.Id.graph);
			settingsView = FindViewById(Resource.Id.settings);

			FindViewById(Resource.Id.reset).Click += (sender, e) => {
				Reset();
			};

			FindViewById(Resource.Id.export).Click += (sender, e) => {
				Export();
			};

			sessions = new List<int>(Intent.GetIntArrayExtra(EXTRA_SESSIONS));

			InitializeGraphViews();
			InitOptionsViews();

			startDateSpinner = settingsView.FindViewById<Spinner>(Resource.Id.start_times);
			endDateSpinner = settingsView.FindViewById<Spinner>(Resource.Id.end_times);

			startDateSpinner.ItemSelected += (sender, e) => {
				startDate = startTimesAdapter.dates[e.Position];
				InvalidateOverlays();
				UpdateDates();
			};

			endDateSpinner.ItemSelected += (sender, e) => {
				endDate = endTimesAdapter.dates[e.Position];
				InvalidateOverlays();
				UpdateDates();
			};

			var empty = FindViewById(Resource.Id.empty);
			empty.Visibility = ViewStates.Gone;
			content.Visibility = ViewStates.Visible;
		}

		protected override void OnResume() {
			base.OnResume();
			RefreshGraphList();
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

		private void InitializeGraphViews() {
			dateRangeView = FindViewById<TextView>(Resource.Id.date);
			showSettingsView = FindViewById(Resource.Id.settings);
			list = FindViewById<RecyclerView>(Resource.Id.list);

			leftHandle = FindViewById(Resource.Id.left);
			rightHandle = FindViewById(Resource.Id.right);
			var container = FindViewById(Resource.Id.view);

			var icon = graphView.FindViewById(Resource.Id.icon);
			icon.SetOnClickListener(new ViewClickAction((view) => {
				AnimateToOptionsView();
			}));

			graphAdapter = new GraphRecordAdapter();
			list.SetAdapter(graphAdapter);

			var green = new Color(0xa0, 0xa0, 0xa0, 0xa0);
			leftOverlay = new SelectionDrawable(SelectionDrawable.EAlign.Left, green, 0);
			rightOverlay = new SelectionDrawable(SelectionDrawable.EAlign.Right, green, 0);

			list.Overlay.Add(leftOverlay);
			list.Overlay.Add(rightOverlay);

			// Initialize all of the views and their actions
			leftHandle.Touch += (sender, e) => {
				switch (e.Event.Action) {
					case MotionEventActions.Move:
						var r = new Rect();
						container.GetGlobalVisibleRect(r);

						// ModifiedStart is the actual position where the graph starts relative to the tabs "view" container.
						var modifiedStart = rect.Left - r.Left;

						var x = e.Event.RawX;
						var dw = leftHandle.Width + rightHandle.Width;

						if (x < modifiedStart) {
							x = modifiedStart;
						} else if (x > modifiedStart + rect.Width() - rightOverlay.width - rightHandle.Width - dw) {
							x = modifiedStart + rect.Width() - rightOverlay.width - dw;
						}

						leftOverlay.width = (int)(x - modifiedStart);
						leftHandle.SetX(x);

						startDate = FindDateFromPosition(x);
						list.Invalidate();
						UpdateDateAdapters();
						UpdateDates();
						break;
					case MotionEventActions.Up:
						break;
				}
			};

			rightHandle.Touch += (sender, e) => {
				switch (e.Event.Action) {
					case MotionEventActions.Move:
						var r = new Rect();
						container.GetGlobalVisibleRect(r);

						// ModifiedStart is the actual position where the graph starts relative to the tabs "view" container.
						var modifiedStart = rect.Left - r.Left;

						var x = e.Event.RawX;
						var dw = leftHandle.Width + rightHandle.Width;

						if (x < modifiedStart + leftOverlay.width + dw) {
							x = modifiedStart + leftOverlay.width + dw;
						} else if (x > modifiedStart + rect.Width()) {
							x = modifiedStart + rect.Width();
						}

						rightOverlay.width = (int)(modifiedStart + rect.Width() - x);

						rightHandle.SetX(x - rightHandle.Width);

						endDate = FindDateFromPosition(x);
						list.Invalidate();
						UpdateDateAdapters();
						UpdateDates();
						break;
					case MotionEventActions.Up:
						break;
				}
			};
		}

		private DateTime FindDateFromPosition(float x) {
			// Normalize the x to the rect.
			x = x - rect.Left;

			if (x > rect.Width()) {
				x = rect.Width();
			} else if (x < 0) {
				x = 0;
			}

			return graphAdapter.FindDateTimeFromSelection(x / (float)leftOverlay.plotWidth);
		}

		private void InvalidateOverlays() {
			leftOverlay.width = (int)(leftOverlay.plotWidth * graphAdapter.FindPercentFromDateTime(startDate));
			rightOverlay.width = (int)(rightOverlay.plotWidth * (1 - graphAdapter.FindPercentFromDateTime(endDate)));
			InvalidateHandles();
			list.Invalidate();
		}

		// TODO ahodder@appioninc.com: Currently, if the user selects dates that are too close to eachother, the handle will overlap
		private void InvalidateHandles() {
			var containerRect = new Rect();
			var container = FindViewById(Resource.Id.view);
			container.GetGlobalVisibleRect(containerRect);
			var ms = rect.Left - containerRect.Left; // The actual start for the container


			// Layout the left handle.
			leftHandle.SetX(ms + leftOverlay.width);

			// Layout the right handle.
			rightHandle.SetX(ms + (rightOverlay.plotWidth - rightOverlay.width) - rightHandle.Width);
		}

		private void InitOptionsViews() {
			var table = FindViewById(Resource.Id.table);
			var pressure = table.FindViewById(Resource.Id.pressure);
			var temperature = table.FindViewById(Resource.Id.temperature);
			var vacuum = table.FindViewById(Resource.Id.vacuum);

			overviewAdapter = new OverviewAdapter();

			pressureUnitButton = pressure.FindViewById<Button>(Resource.Id.unit);
			temperatureUnitButton = temperature.FindViewById<Button>(Resource.Id.unit);
			vacuumUnitButton = vacuum.FindViewById<Button>(Resource.Id.unit);

			pressureUnitButton.SetOnClickListener(new ViewClickAction((view) => {
				UnitDialog.Create(this, SensorUtils.DEFAULT_PRESSURE_UNITS, (sender, e) => {
					SetPressureReportUnit(e);
				}).Show();
			}));

			temperatureUnitButton.SetOnClickListener(new ViewClickAction((view) => {
				UnitDialog.Create(this, SensorUtils.DEFAULT_TEMPERATURE_UNITS, (sender, e) => {
					SetTemperatureReportUnit(e);
				}).Show();
			}));

			vacuumUnitButton.SetOnClickListener(new ViewClickAction((view) => {
				UnitDialog.Create(this, SensorUtils.DEFAULT_VACUUM_UNITS, (sender, e) => {
					SetVacuumReportUnit(e);
				}).Show();
			}));

			var icon = settingsView.FindViewById(Resource.Id.icon);
			icon.SetOnClickListener(new ViewClickAction((view) => {
				AnimateToGraphView();
			}));

			var list = settingsView.FindViewById<RecyclerView>(Resource.Id.list);
			list.SetAdapter(overviewAdapter);

			SetPressureReportUnit(ion.preferences.units.pressure);
			SetTemperatureReportUnit(ion.preferences.units.temperature);
			SetVacuumReportUnit(ion.preferences.units.vacuum);
		}

		private void SetPressureReportUnit(Unit unit) {
			if (!unit.IsCompatible(ion.preferences.units.pressure)) {
				Error(GetString(Resource.String.error_failed_to_set_unit));
			} else {
				pressureUnitButton.Text = unit.ToString();
				ion.preferences.units.pressure = unit;
			}
			if (graphAdapter.dil != null) {
        overviewAdapter.NotifyDataSetChanged();
			}
		}

		private void SetTemperatureReportUnit(Unit unit) {
			if (!unit.IsCompatible(ion.preferences.units.temperature)) {
				Error(GetString(Resource.String.error_failed_to_set_unit));
			} else {
				temperatureUnitButton.Text = unit.ToString();
				ion.preferences.units.temperature = unit;
			}
			if (graphAdapter.dil != null) {
				overviewAdapter.NotifyDataSetChanged();
			}
		}

		private void SetVacuumReportUnit(Unit unit) {
			if (!unit.IsCompatible(ion.preferences.units.vacuum)) {
				Error(GetString(Resource.String.error_failed_to_set_unit));
			} else {
				vacuumUnitButton.Text = unit.ToString();
				ion.preferences.units.vacuum = unit;
			}
			if (graphAdapter.dil != null) {
				overviewAdapter.NotifyDataSetChanged();
			}
		}

		private void AnimateToOptionsView() {
			FlipView(content, Resource.Id.graph, Resource.Id.settings);
		}

		private void AnimateToGraphView() {
			FlipView(content, Resource.Id.settings, Resource.Id.graph);
      InvalidateHandles();
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

		private async void InvalidateGraphViews() {
			var left = FindViewById(Resource.Id.left);
			var right = FindViewById(Resource.Id.right);
			var container = FindViewById(Resource.Id.view);

			var tries = 0;
			var row = list.FindViewHolderForAdapterPosition(0)?.ItemView;
			do {
				await Task.Delay(50);
				tries++;
			} while ((row = list.FindViewHolderForAdapterPosition(0)?.ItemView) == null && tries < 5);

			if (row == null) {
				Log.E(this, "Failed to get row from list.");
				return;
			}

			var plot = row.FindViewById<PlotView>(Resource.Id.graph);
			rect = new Rect();
			plot.GetGlobalVisibleRect(rect);

			var r = new Rect();
			container.GetGlobalVisibleRect(r);

			// ModifiedStart is the actual position where the graph starts relative to the tabs "view" container.
			var modifiedStart = rect.Left - r.Left;

			leftOverlay.plotWidth = rect.Width();
			rightOverlay.plotWidth = rect.Width();

			var leftx = Math.Max(modifiedStart, modifiedStart + leftOverlay.width - left.Width / 2);
			var rightx = Math.Min(modifiedStart + rect.Width() - right.Width, modifiedStart + rect.Width() - rightOverlay.width - right.Width / 2);

			left.SetX(leftx);
			right.SetX(rightx);

			startDate = FindDateFromPosition(leftx);
			endDate = FindDateFromPosition(rightx);

			list.Invalidate();

			UpdateDates();
		}

		private void UpdateDateAdapters() {
			var startIndex = graphAdapter.IndexOfDateTime(startDate);
			var endIndex = graphAdapter.IndexOfDateTime(endDate);

			var startDates = graphAdapter.GetDatesInRange(0, endIndex);
			var endDates = graphAdapter.GetDatesInRange(startIndex, graphAdapter.dil.dateSpan - 1);

			startTimesAdapter = new DateTimeAdapter(this, startDates);
			endTimesAdapter = new DateTimeAdapter(this, endDates);

			startDateSpinner.Adapter = startTimesAdapter;
			endDateSpinner.Adapter = endTimesAdapter;

			startDateSpinner.SetSelection(startTimesAdapter.dates.IndexOf(startDate));
			endDateSpinner.SetSelection(endTimesAdapter.dates.IndexOf(endDate));

			var tuple = BuildSensorReportEncapsulations(graphAdapter.GatherSelectedLogs(leftOverlay.percent, rightOverlay.percent));
			overviewAdapter.SetLogs(tuple.Item2);
		}

		/// <summary>
		/// Updates the adapter content and the settings view.
		/// </summary>
		/// <param name="startDate">Start date.</param>
		/// <param name="endDate">End date.</param>
		private void UpdateDates() {
      var tuple = BuildSensorReportEncapsulations(graphAdapter.GatherSelectedLogs(leftOverlay.percent, rightOverlay.percent));
      overviewAdapter.SetLogs(tuple.Item2);
			dateRangeView.Text = GetString(Resource.String.start) + ": " + startDate.ToShortDateString() + " " + startDate.ToLongTimeString() + "\n" +
				GetString(Resource.String.finish) + ": " + endDate.ToShortDateString() + " " + endDate.ToLongTimeString();
		}

    /// <summary>
    /// Creates a complete representation of sensor graphs.
    /// </summary>
    /// <returns>The sensor report encapsulations.</returns>
    // TODO ahodder@appioninc.com: This needs to be optimized as we are allocating a truely fucktastic amount of memory to make this work.
    private Tuple<DateIndexLookup, List<SensorReportEncapsulation>> BuildSensorReportEncapsulations(List<SessionResults> sessionResults) {
      var dateLookupTable = new List<DateTime>();
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
          for (int i = 0; i < dsl.logs.Length; i++) {
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

      var dil = new DateIndexLookup(dateLookupTable);
      var encaps = new List<SensorReportEncapsulation>();
      foreach (var sensor in map.Keys) {
        encaps.Add(new SensorReportEncapsulation(sensor, dil, map[sensor].ToArray()));
      }

      return new Tuple<DateIndexLookup, List<SensorReportEncapsulation>>(dil, encaps);
    }

		private async Task RefreshGraphList() {
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

			dialog.Dismiss();
			InvalidateGraphViews();

			var empty = FindViewById(Resource.Id.empty);
			if (graphAdapter.ItemCount <= 0) {
				empty.Visibility = ViewStates.Visible;
				content.Visibility = ViewStates.Gone;
			} else {
				empty.Visibility = ViewStates.Gone;
				content.Visibility = ViewStates.Visible;
			}

			startDate = graphAdapter.dil.DateFromIndex(0);
			endDate = graphAdapter.dil.DateFromIndex(graphAdapter.dil.dateSpan - 1);
			UpdateDateAdapters();
		}

		private void Reset() {
			leftOverlay.width = 0;
			rightOverlay.width = 0;

			InvalidateGraphViews();
		}

		private void Export() {
			var leftSelection = leftOverlay.width / (float)leftOverlay.plotWidth;
			var rightSelection = 1 - (rightOverlay.width / (float)rightOverlay.plotWidth);
			var results = graphAdapter.GatherSelectedLogs(leftSelection, rightSelection);

			var start = graphAdapter.FindDateTimeFromSelection(leftSelection);
			var end = graphAdapter.FindDateTimeFromSelection(rightSelection);

			var dlr = DataLogReport.BuildFromSessionResults(ion, new DataLogReportLocalization(this), start, end, results);
      dlr.reportName = GetString(Resource.String.report_data_logging_title);

			var drawable = Resources.GetDrawable(Resource.Drawable.img_logo_appionblack) as BitmapDrawable;

			using (var ms = new MemoryStream(512)) {
				drawable.Bitmap.Compress(Bitmap.CompressFormat.Png, 100, ms);
				dlr.appionLogoPng = ms.ToArray();
				dlr.graphImages = CaptureGraphs();
			}

      ShowExportDialog(dlr);
		}

		/// <summary>
		/// Captures all of the graph selected graph views and converts them into a png for exporting.
		/// </summary>
		private Dictionary<GaugeDeviceSensor, IonImage> CaptureGraphs() {
			var ret = new Dictionary<GaugeDeviceSensor, IonImage>();

      int templateIndex = -1;
			var lm = list.GetLayoutManager() as LinearLayoutManager;
      // Find the first visible graph view holder
      for (int i = lm.FindFirstVisibleItemPosition(); i < lm.FindLastVisibleItemPosition(); i++) {
        var tvh = list.FindViewHolderForAdapterPosition(i) as GraphViewHolder;
        if (tvh != null) {
          templateIndex = i;
          break;
        }
      }

      if (templateIndex == -1) {
        Log.E(this, "Failed to capture graphs for recycler view: no view holders found in range");
        return ret;
      }

			var vh = list.FindViewHolderForAdapterPosition(templateIndex) as GraphViewHolder;

			for (int i = 0; i < graphAdapter.ItemCount; i++) {
        var record = graphAdapter[i] as GraphRecord;
        if (record.isChecked) {
          var view = vh.contentContainer;
          graphAdapter.BindViewHolder(vh, i);
          var image = new IonImage(IonImage.EType.Png, view.Width, view.Height, view.ToPng());
          ret[vh.record.data] = image;
        }
      }

      graphAdapter.BindViewHolder(vh, templateIndex);

      return ret;
		}

    /// <summary>
    /// Creates detailed graphs for the the reports.
    /// </summary>
    /// <returns>The detailed graphs.</returns>
    private Dictionary<GaugeDeviceSensor, IonImage> CaptureDetailedGraphs(DataLogReport dlr) {
      var ret = new Dictionary<GaugeDeviceSensor, IonImage>();

      var tuple = BuildSensorReportEncapsulations(dlr.sessionResults);
      var renderer = new DetailedGraphGenerator(this, ion);


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
                dlr.graphImages = CaptureGraphs();
                break;
              case Resource.Id._2:
                exporter = new PdfDetailedReportExporter(ion, checkbox.Checked);
                dlr.graphImages = CaptureDetailedGraphs(dlr);
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
	}

	class SelectionDrawable : Drawable {
		public float percent {
			get {
				if (plotWidth == 0) {
					return (align == EAlign.Left) ? 0 : 1;
				} else {
					return (align == EAlign.Left) ? (width / (float)plotWidth) : 1 - (width / (float)plotWidth);
				}
			}
		}
		/// <summary>
		/// The width of the defining plot.
		/// </summary>
		/// <value>The width of the plot.</value>
		public int plotWidth { get; set; }
		/// <summary>
		/// The width of the overlay.
		/// </summary>
		/// <value>The width.</value>
		public int width { get; set; }
		/// <summary>
		/// The paint that the drawable will be painted with.
		/// </summary>
		/// <value>The paint.</value>
		public Paint paint { get; set; }
		/// <summary>
		/// How to align the drawable.
		/// </summary>
		private EAlign align;
		/// <summary>
		/// The bounds of the drawable.
		/// </summary>
		private Rect bounds;

		public SelectionDrawable(EAlign align, Color color, int width) {
			this.align = align;
			this.width = width;

			paint = new Paint();
			paint.Color = color;
			paint.SetStyle(Paint.Style.Fill);
			bounds = new Rect();
		}

		/// <Docs>The canvas to draw into</Docs>
		/// <remarks>Draw in its bounds (set via setBounds) respecting optional effects such
		///  as alpha (set via setAlpha) and color filter (set via setColorFilter).</remarks>
		/// <format type="text/html">[Android Documentation]</format>
		/// <since version="Added in API level 1"></since>
		/// <summary>
		/// Draw the specified canvas.
		/// </summary>
		/// <param name="canvas">Canvas.</param>
		public override void Draw(Canvas canvas) {
			switch (align) {
				case EAlign.Left:
					bounds.Left = 0;
					bounds.Right = bounds.Left + width;
				break;
				case EAlign.Right:
					bounds.Right = plotWidth;
					bounds.Left = plotWidth - width;
				break;
			}
			bounds.Top = 0;
			bounds.Bottom = canvas.Height;

			canvas.DrawRect(bounds, paint);
		}

		/// <Docs>To be added.</Docs>
		/// <remarks>Specify an alpha value for the drawable. 0 means fully transparent, and
		///  255 means fully opaque.</remarks>
		/// <format type="text/html">[Android Documentation]</format>
		/// <since version="Added in API level 1"></since>
		/// <summary>
		/// Sets the alpha.
		/// </summary>
		/// <param name="alpha">Alpha.</param>
		public override void SetAlpha(int alpha) {
			paint.Alpha = alpha;
		}

		/// <summary>
		/// Sets the color filter.
		/// </summary>
		/// <param name="colorFilter">Color filter.</param>
		public override void SetColorFilter(ColorFilter colorFilter) {
			paint.SetColorFilter(colorFilter);
		}

		/// <summary>
		/// Gets the opacity.
		/// </summary>
		/// <value>The opacity.</value>
		public override int Opacity {
			get {
				return (int)Format.Opaque;
			}
		}

		public enum EAlign {
			Left,
			Right,
		}
	}

	class DateTimeAdapter : ArrayAdapter<string> {
		public List<DateTime> dates;

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
