namespace ION.Droid.Activity.Report {

	using System;
	using System.Collections.Generic;
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

	using OxyPlot.Xamarin.Android;

	using ION.Core.IO;
	using ION.Core.Measure;
	using ION.Core.Report.DataLogs;
	using ION.Core.Sensors;
	using ION.Core.Util;

	using ION.Droid.Dialog;
	using ION.Droid.Report;
	using ION.Droid.Util;
	using ION.Droid.Views;

	[Activity(Label="@string/report_select_export_data", Icon="@drawable/ic_nav_reporting", Theme="@style/AppTheme", LaunchMode=LaunchMode.SingleTask, ScreenOrientation=ScreenOrientation.Portrait)]
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
		private RecyclerView graphList;

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
		/// The plot rect;
		/// </summary>
		private Rect rect;

		/// <summary>
		/// The sessions that the activity is graphing.
		/// </summary>
		private List<int> sessions;

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
				var pos = e.Position;
				var len = (float)graphAdapter.dil.dateSpan;
				leftOverlay.width = (int)(pos / len * leftOverlay.plotWidth);

				InvalidateGraphViews();
			};

			endDateSpinner.ItemSelected += (sender, e) => {
				var len = (float)graphAdapter.dil.dateSpan;
				var pos = len - e.Position;
				leftOverlay.width = (int)(pos / len * leftOverlay.plotWidth);

				InvalidateGraphViews();
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
			graphList = FindViewById<RecyclerView>(Resource.Id.list);

			var left = FindViewById(Resource.Id.left);
			var right = FindViewById(Resource.Id.right);
			var container = FindViewById(Resource.Id.view);

			var icon = graphView.FindViewById(Resource.Id.icon);
			icon.SetOnClickListener(new ViewClickAction((view) => {
				AnimateToOptionsView();
			}));

			graphAdapter = new GraphRecordAdapter();
			graphList.SetAdapter(graphAdapter);

			var green = new Color(0xa0, 0xa0, 0xa0, 0xa0);
			leftOverlay = new SelectionDrawable(SelectionDrawable.EAlign.Left, green, 0);
			rightOverlay = new SelectionDrawable(SelectionDrawable.EAlign.Right, green, 0);

			graphList.Overlay.Add(leftOverlay);
			graphList.Overlay.Add(rightOverlay);

			// Initialize all of the views and their actions
			left.Touch += (sender, e) => {
				switch (e.Event.Action) {
					case MotionEventActions.Move:
						var r = new Rect();
						container.GetGlobalVisibleRect(r);

						// ModifiedStart is the actual position where the graph starts relative to the tabs "view" container.
						var modifiedStart = rect.Left - r.Left;

						var x = e.Event.RawX;
						var dw = left.Width / 2 + right.Width / 2;

						if (x < modifiedStart) {
							x = modifiedStart;
						} else if (x > modifiedStart + rect.Width() - rightOverlay.width - dw) {
							x = modifiedStart + rect.Width() - rightOverlay.width - dw;
						}

						leftOverlay.width = (int)(x - modifiedStart);
						left.SetX(x);

						graphList.Invalidate();
						UpdateDates();
						break;
					case MotionEventActions.Up:
						break;
				}
			};

			right.Touch += (sender, e) => {
				switch (e.Event.Action) {
					case MotionEventActions.Move:
						var r = new Rect();
						container.GetGlobalVisibleRect(r);

						// ModifiedStart is the actual position where the graph starts relative to the tabs "view" container.
						var modifiedStart = rect.Left - r.Left;

						var x = e.Event.RawX;
						var dw = left.Width / 2 + right.Width / 2;

						if (x < modifiedStart + leftOverlay.width + dw) {
							x = modifiedStart + leftOverlay.width + dw;
						} else if (x > modifiedStart + rect.Width()) {
							x = modifiedStart + rect.Width();
						}

						rightOverlay.width = (int)(modifiedStart + rect.Width() - x);

						right.SetX(x - right.Width);

						graphList.Invalidate();
						UpdateDates();
						break;
					case MotionEventActions.Up:
						break;
				}
			};
		}

		private void InitOptionsViews() {
			var table = FindViewById(Resource.Id.table);
			var pressure = table.FindViewById(Resource.Id.pressure);
			var temperature = table.FindViewById(Resource.Id.temperature);
			var vacuum = table.FindViewById(Resource.Id.vacuum);

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

			SetPressureReportUnit(ion.preferences.units.pressure);
			SetTemperatureReportUnit(ion.preferences.units.temperature);
			SetVacuumReportUnit(ion.preferences.units.vacuum);

			var icon = settingsView.FindViewById(Resource.Id.icon);
			icon.SetOnClickListener(new ViewClickAction((view) => {
				AnimateToGraphView();
			}));

			overviewAdapter = new OverviewAdapter();

			var list = settingsView.FindViewById<RecyclerView>(Resource.Id.list);
			list.SetAdapter(overviewAdapter);
		}

		private void SetPressureReportUnit(Unit unit) {
			if (!unit.IsCompatible(ion.preferences.units.pressure)) {
				Error(GetString(Resource.String.error_failed_to_set_unit));
			} else {
				pressureUnitButton.Text = unit.ToString();
				ion.preferences.units.pressure = unit;
			}
		}

		private void SetTemperatureReportUnit(Unit unit) {
			if (!unit.IsCompatible(ion.preferences.units.temperature)) {
				Error(GetString(Resource.String.error_failed_to_set_unit));
			} else {
				temperatureUnitButton.Text = unit.ToString();
				ion.preferences.units.temperature = unit;
			}
		}

		private void SetVacuumReportUnit(Unit unit) {
			if (!unit.IsCompatible(ion.preferences.units.vacuum)) {
				Error(GetString(Resource.String.error_failed_to_set_unit));
			} else {
				vacuumUnitButton.Text = unit.ToString();
				ion.preferences.units.vacuum = unit;
			}
		}

		private void AnimateToOptionsView() {
			FlipView(content, Resource.Id.graph, Resource.Id.settings);

		}

		private void AnimateToGraphView() {
			FlipView(content, Resource.Id.settings, Resource.Id.graph);
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
			var row = graphList.FindViewHolderForAdapterPosition(0)?.ItemView;
			do {
				await Task.Delay(50);
				tries++;
			} while ((row = graphList.FindViewHolderForAdapterPosition(0)?.ItemView) == null && tries < 5);

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

			graphList.Invalidate();


			UpdateDates();
		}

		/// <summary>
		/// Updates the adapter content and the settings view.
		/// </summary>
		/// <param name="startDate">Start date.</param>
		/// <param name="endDate">End date.</param>
		private void UpdateDates() {
			var startDate = graphAdapter.FindDateTimeFromSelection(leftOverlay.width / (float)leftOverlay.plotWidth);
			var endDate = graphAdapter.FindDateTimeFromSelection(1 - (rightOverlay.width / (float)rightOverlay.plotWidth));

			dateRangeView.Text = GetString(Resource.String.start) + ": " + startDate.ToShortDateString() + " " + startDate.ToLongTimeString() + "\n" + 
				GetString(Resource.String.finish) + ": " + endDate.ToShortDateString() + " " + endDate.ToLongTimeString();

			var date = settingsView.FindViewById(Resource.Id.date);
			date.Visibility = ViewStates.Gone;
			
			var startIndex = graphAdapter.IndexOfDateTime(startDate);
			var endIndex = graphAdapter.IndexOfDateTime(endDate);

			var startDates = graphAdapter.GetDatesInRange(0, endIndex);
			var endDates = graphAdapter.GetDatesInRange(startIndex, graphAdapter.dil.dateSpan - 1);

			startTimesAdapter = new DateTimeAdapter(this, startDates);
			endTimesAdapter = new DateTimeAdapter(this, endDates);

//			this.startTimesAdapter = new ArrayAdapter<string>(DatesToStr
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
					var sessionData = await ion.dataLogManager.QuerySessionDataAsync(id);
					sessionResults.Add(sessionData);
				} catch (Exception e) {
					Log.E(this, "Failed to query session data", e);
				}
			}

			graphAdapter.SetRecords(ion, sessionResults);
			overviewAdapter.SetLogs(ion, sessionResults);

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
		}

		private void Reset() {
			leftOverlay.width = 0;
			rightOverlay.width = 0;

			InvalidateGraphViews();
		}

		private void Export() {
			var dialog = new ListDialogBuilder(this);
			dialog.SetTitle(Resource.String.report_choose_export_format);

			dialog.AddItem(Resource.String.spreadsheet, () => {
				ExportExcel();
			});

			dialog.AddItem(Resource.String.pdf, () => {
				ExportPdf();
			});

			dialog.SetNegativeButton(Resource.String.cancel, (sender, e) => {
				var d = sender as Dialog;
				d.Dismiss();
			});

			dialog.Show();
		}

		private async Task ExportExcel() {
			var dialog = new ProgressDialog(this);
			dialog.SetTitle(Resource.String.please_wait);
			dialog.SetMessage(GetString(Resource.String.saving));
			dialog.Show();

			var task = Task.Factory.StartNew(() => {
				try {
					var leftSelection = leftOverlay.width / (float)leftOverlay.plotWidth;
					var rightSelection = 1 - (rightOverlay.width / (float)rightOverlay.plotWidth);
					var results = graphAdapter.GatherSelectedLogs(leftSelection, rightSelection);

					var start = graphAdapter.FindDateTimeFromSelection(leftSelection);
					var end = graphAdapter.FindDateTimeFromSelection(rightSelection);
					var dlr = DataLogReport.BuildFromSessionResults(ion, start, end, results);

					var dateString = DateTime.Now.ToFullShortString();
					dateString = dateString.Replace('\\', '-'); 
					dateString = dateString.Replace('/', '-');

					var folder = ion.dataLogReportFolder;
					var file = folder.GetFile(FILE_NAME + "_" + dateString + EXCEL_EXT, EFileAccessResponse.ReplaceIfExists);

					if (DataLogExcelReportExporter.Export(this, ion, file.fullPath, dlr)) {
						Log.D(this, "Succeeded in exporting the results");
					} else {
						Log.D(this, "Failed to export the results.");
					}
				} catch (Exception e) {
					Log.E(this, "Failed to export report", e);
				}
			});

			await task;

			var data = new Intent();
			data.PutExtra(ReportActivity.EXTRA_SHOW_SAVED_SPREADSHEETS, true);
			SetResult(Result.Ok, data);
			Finish();

			dialog.Dismiss();
		}

		private async Task ExportPdf() {
			var dialog = new ProgressDialog(this);
			dialog.SetTitle(Resource.String.please_wait);
			dialog.SetMessage(GetString(Resource.String.saving));
			dialog.Show();

			var task = Task.Factory.StartNew(() => {
				try {
					var leftSelection = leftOverlay.width / (float)leftOverlay.plotWidth;
					var rightSelection = 1 - (rightOverlay.width / (float)rightOverlay.plotWidth);
					var results = graphAdapter.GatherSelectedLogs(leftSelection, rightSelection);

					var start = graphAdapter.FindDateTimeFromSelection(leftSelection);
					var end = graphAdapter.FindDateTimeFromSelection(rightSelection);
					var dlr = DataLogReport.BuildFromSessionResults(ion, start, end, results);

					var dateString = DateTime.Now.ToFullShortString();
					dateString = dateString.Replace('\\', '-'); 
					dateString = dateString.Replace('/', '-');
					var filename = FILE_NAME + "_" + dateString + PDF_EXT;

					var folder = ion.dataLogReportFolder;
					var file = folder.GetFile(FILE_NAME + "_" + dateString + PDF_EXT, EFileAccessResponse.ReplaceIfExists);

					var success = DataLogPdfReportExporter.Export(this, ion, file.fullPath, dlr);

					if (success) {
						Log.D(this, "Succeeded in exporting the results");
					} else {
						Log.D(this, "Failed to export the results.");
					}
				} catch (Exception e) {
					Log.E(this, "Failed to export report", e);
				}
			});

			await task;

			var data = new Intent();
			data.PutExtra(ReportActivity.EXTRA_SHOW_SAVED_PDF, true);
			SetResult(Result.Ok, data);
			Finish();

			dialog.Dismiss();
		}
	}

	class SelectionDrawable : Drawable {
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

