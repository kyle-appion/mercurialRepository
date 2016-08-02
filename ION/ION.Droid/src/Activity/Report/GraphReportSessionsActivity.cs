namespace ION.Droid.Activity.Report {

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	using Android.App;
	using Android.Content;
	using Android.Content.PM;
	using Android.Graphics;
	using Android.Graphics.Drawables;
	using Android.OS;
	using Android.Runtime;
	using Android.Support.V7.Widget;
	using Android.Views;
	using Android.Widget;

	using OxyPlot.Xamarin.Android;

	using ION.Core.Database;
	using ION.Core.Devices;
	using ION.Core.Report.DataLogs;
	using ION.Core.Util;

	using ION.Droid.Util;

	[Activity(Label="GRAPH REPORT SESSIONS", Theme="@style/AppTheme", LaunchMode=LaunchMode.SingleTask, ScreenOrientation=ScreenOrientation.Portrait)]
	public class GraphReportSessionsActivity : IONActivity {
		/// <summary>
		/// The extra that will retrieve an integer array from the starting Intent. This list is what is used to populate the
		/// activity's session list.
		/// </summary>
		public const string EXTRA_SESSIONS = "ION.Droid.extra.SESSIONS";

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
		/// The adapter that will shown the logs graphs.
		/// </summary>
		private GraphRecordAdapter adapter;
		/// <summary>
		/// The center x of the left slider.
		/// </summary>
		private float leftX;
		/// <summary>
		/// The drawable that shows the ignored content of the left selection.
		/// </summary>
		private SelectionDrawable leftOverlay;
		/// <summary>
		/// The center x of the right slider.
		/// </summary>
		private float rightX;
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

			if (!Intent.HasExtra(EXTRA_SESSIONS)) {
				Error(GetString(Resource.String.report_error_graph_no_sessions_provided));
				SetResult(Result.Canceled);
				Finish();
				return;
			}

			FindViewById(Resource.Id.reset).Click += (sender, e) => {
				Reset();
			};

			FindViewById(Resource.Id.export).Click += (sender, e) => {
				Export();
			};

			leftX = rightX = -1;

			sessions = new List<int>(Intent.GetIntArrayExtra(EXTRA_SESSIONS));

			InitializeGraphViews();
		}

		protected override void OnResume() {
			base.OnResume();

			InvalidateGraphViews();
			RefreshGraphList();
		}

		private void InitializeGraphViews() {
			dateRangeView = FindViewById<TextView>(Resource.Id.date);
			showSettingsView = FindViewById(Resource.Id.settings);
			graphList = FindViewById<RecyclerView>(Resource.Id.list);

			var left = FindViewById(Resource.Id.left);
			var right = FindViewById(Resource.Id.right);

			adapter = new GraphRecordAdapter();
			graphList.SetAdapter(adapter);

			var green = new Color(0xa0, 0xa0, 0xa0, 0xa0);
			leftOverlay = new SelectionDrawable(SelectionDrawable.EAlign.Left, green, 0);
			rightOverlay = new SelectionDrawable(SelectionDrawable.EAlign.Right, green, 0);

			graphList.Overlay.Add(leftOverlay);
			graphList.Overlay.Add(rightOverlay);

			// Initialize all of the views and their actions
			left.Touch += (sender, e) => {
				switch (e.Event.Action) {
					case MotionEventActions.Move:
						var x = e.Event.RawX;

						if (x < rect.Left) {
							x = rect.Left;
						} else if (x > (rightX - right.Width)) {
							x = rightX - right.Width;
						}

						leftX = x;
						left.SetX(leftX - left.Width);

						leftOverlay.width = (int)(x - rect.Left);
						Log.D(this, "Left: " + leftOverlay.width);
						graphList.Invalidate();
						UpdateGraphSelectionDatesFromMeasurements();
						break;
					case MotionEventActions.Up:
						break;
				}
			};

			right.Touch += (sender, e) => {
				switch (e.Event.Action) {
					case MotionEventActions.Move:
						var x = e.Event.RawX;

						if (x < leftX + left.Width) {
							x = leftX + left.Width;
						} else if (x > rect.Left + rect.Width()) {
							x = rect.Left + rect.Width();
						}

						rightX = x;
						right.SetX(rightX - right.Width);

						rightOverlay.width = (int)(rect.Left + rect.Width() - x);
						Log.D(this, "Right: " + rightOverlay.width);
						graphList.Invalidate();
						UpdateGraphSelectionDatesFromMeasurements();
						break;
					case MotionEventActions.Up:
						break;
				}
			};
		}

		private async void InvalidateGraphViews() {
			var left = FindViewById(Resource.Id.left);
			var right = FindViewById(Resource.Id.right);

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

			leftOverlay.plotWidth = rect.Width();
			rightOverlay.plotWidth = rect.Width();

			if (leftX == -1) {
				leftX = rect.Left;
			}

			if (rightX == -1) {
				rightX = rect.Left + rect.Width();
			}

			left.SetX(leftX - left.Width);
			right.SetX(rightX - right.Width);

			graphList.Invalidate();
			UpdateGraphSelectionDatesFromMeasurements();
		}

		private void UpdateGraphSelectionDatesFromMeasurements() {
			var start = adapter.FindDateTimeFromSelection(leftOverlay.width / (float)leftOverlay.plotWidth);
			var end = adapter.FindDateTimeFromSelection(1 - (rightOverlay.width / (float)rightOverlay.plotWidth));

			dateRangeView.Text = start.ToShortDateString() + " " + start.ToLongTimeString() + "\n" + 
				end.ToShortDateString() + " " + end.ToLongTimeString();
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
					var sessionData = await ion.dataLogManager.QuerySessionData(id);
					sessionResults.Add(sessionData);
				} catch (Exception e) {
					Log.E(this, "Failed to query session data", e);
				}
			}

			adapter.SetRecords(ion, sessionResults);

			dialog.Dismiss();
			InvalidateGraphViews();
		}

		private void Reset() {
			leftX = -1;
			rightX = -1;

			leftOverlay.width = 0;
			rightOverlay.width = 0;

			InvalidateGraphViews();
		}

		private void Export() {
			var results = adapter.GatherSelectedLogs(leftOverlay.width / (float)leftOverlay.plotWidth,
			                                         1 - (rightOverlay.width / (float)rightOverlay.plotWidth));
			;
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
}

