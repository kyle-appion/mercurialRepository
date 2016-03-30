namespace ION.Droid.Activity.DataLogging {

  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;

  using Android.Animation;
  using Android.App;
  using Android.Content;
  using Android.Graphics;
  using Android.Graphics.Drawables;
  using Android.Graphics.Drawables.Shapes;
  using Android.OS;
  using Android.Runtime;
  using Android.Support.V7.Widget;
  using Android.Views;
  using Android.Widget;

  using OxyPlot.Xamarin.Android;

  using ION.Core.Database;
  using ION.Core.Report.DataLogs;
  using ION.Core.Util;

  using ION.Droid.Activity;
  using ION.Droid.Widgets.Adapters.DataLogging;
  using ION.Droid.Views;
  using ION.Droid.Widgets.RecyclerViews;

  [Activity(Label = "DataLoggingReportActivity", Theme="@style/TerminalActivityTheme")]      
  public class DataLoggingReportActivity : IONActivity {

    /// <summary>
    /// The card view that holds the new/saved options.
    /// </summary>
    private CardView newSavedCard;
    /// <summary>
    /// The card view that holds the selection content.
    /// </summary>
    private CardView selectionCard;
    /// <summary>
    /// The card view that holds the graph content.
    /// </summary>
    private CardView graphCard;
    /// <summary>
    /// The card view that is currently focused.
    /// </summary>
    private CardView focusedCard;

    /// <summary>
    /// Id the selection card viewing jobs. False means the list is set to sessions.
    /// </summary>
    private bool viewJobs;
    /// <summary>
    /// The adapter that will allow for the selection of sessions.
    /// </summary>
    private SessionSelectionAdapter sessionAdapter;
    /// <summary>
    /// The adapter that will display the session content graphs. 
    /// </summary>
    private GraphSelectionAdapter graphAdapter;
    /// <summary>
    /// The view that will perform the left bounded selection for the graphs.
    /// </summary>
    private SelectionDrawable leftOverlay;
    /// <summary>
    /// The x coordinate for the center x of the left tab.
    /// </summary>
    private int leftTabX;
    /// <summary>
    /// The view that will perform the right bounded selection for the graphs.
    /// </summary>
    private SelectionDrawable rightOverlay;
    /// <summary>
    /// The x coordinate for the center x of the right tab.
    /// </summary>
    private int rightTabX;
    /// <summary>
    /// The last known width of the plot.
    /// </summary>
    private int plotWidth;
    /// <summary>
    /// The data log report that
    /// </summary>
    private DataLogReport report;
    /// <summary>
    /// The start time for the data logs to include in the report.
    /// </summary>
    private DateTime absoluteStart;
    /// <summary>
    /// The relative selected start.
    /// </summary>
    private DateTime localStart;
    /// <summary>
    /// The end time for the data logs to include in the report.
    /// </summary>
    private DateTime absoluteEnd;
    /// <summary>
    /// The relative selected end.
    /// </summary>
    private DateTime localEnd;


    /// <summary>
    /// Raises the create event.
    /// </summary>
    /// <param name="savedInstanceState">Saved instance state.</param>
    protected override void OnCreate(Bundle savedInstanceState) {
      base.OnCreate(savedInstanceState);

      ActionBar.SetDisplayHomeAsUpEnabled(true);
      ActionBar.SetHomeButtonEnabled(true);

      SetContentView(Resource.Layout.activity_data_logging_report);

      newSavedCard = FindViewById<CardView>(Resource.Id.report_pick);
      selectionCard = FindViewById<CardView>(Resource.Id.report_selection);
      graphCard = FindViewById<CardView>(Resource.Id.report_graph);

      InitNewSavedViews();
      InitSelectionViews();
      InitGraphViews();

      report = new DataLogReport();
      MarkCardViewAsFocused(newSavedCard);
    }

    /// <Docs>The options menu in which you place your items.</Docs>
    /// <returns>To be added.</returns>
    /// <summary>
    /// Raises the create options menu event.
    /// </summary>
    /// <param name="menu">Menu.</param>
    public override bool OnCreateOptionsMenu(Android.Views.IMenu menu) {
      base.OnCreateOptionsMenu(menu);

      return true;
    }

    /// <Docs>The panel that the menu is in.</Docs>
    /// <summary>
    /// Raises the menu item selected event.
    /// </summary>
    /// <param name="featureId">Feature identifier.</param>
    /// <param name="item">Item.</param>
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

    /// <summary>
    /// Initializes the views for the new saved card view.
    /// </summary>
    /// <param name="view">View.</param>
    private void InitNewSavedViews() {
      var view = newSavedCard;

      SetCardViewTitleVisible(newSavedCard, true);
      newSavedCard.SetOnClickListener(new ViewClickAction((v) => {
        MarkCardViewAsFocused(newSavedCard);
      }));

      view.FindViewById(Resource.Id.report_saved).SetOnClickListener(new ViewClickAction((v) => {
        Alert("Load a saved report");
      }));

      view.FindViewById(Resource.Id.report_new).SetOnClickListener(new ViewClickAction((v) => {
        report = new DataLogReport();
//        FlipView(view, Resource.Id.content, Resource.Id.status);
        InvalidateNewSavedViews();
        MarkCardViewAsFocused(selectionCard);
      }));
    }

    /// <summary>
    /// Initializes the views for the selection card view.
    /// </summary>
    private async void InitSelectionViews() {
      var view = selectionCard;

      var list = view.FindViewById<RecyclerView>(Resource.Id.list);
      list.SetLayoutManager(new LinearLayoutManager(this));

      SetCardViewTitleVisible(view, true);

      view.FindViewById(Resource.Id.next).SetOnClickListener(new ViewClickAction((v) => {
        InvalidateGraphViews();
        MarkCardViewAsFocused(graphCard);
      }));

      sessionAdapter = new SessionSelectionAdapter();
      sessionAdapter.AddSessions(ion, ion.database.Table<SessionRow>().Where(sr => sr.jobId == 0).AsEnumerable());
      sessionAdapter.AddJobs(ion, await ion.database.QueryForAllAsync<JobRow>());
      list.SetAdapter(sessionAdapter);
    }

    /// <summary>
    /// Initializes the graphing views.
    /// </summary>
    private void InitGraphViews() {
      var view = graphCard;

      var list = view.FindViewById<RecyclerView>(Resource.Id.list);
      list.SetLayoutManager(new LinearLayoutManager(this));
      var up = view.FindViewById<ImageView>(Resource.Id.up);
      var down = view.FindViewById<ImageView>(Resource.Id.down);
      var left = view.FindViewById(Resource.Id.left);
      var right = view.FindViewById(Resource.Id.right);
      var time = view.FindViewById<TextView>(Resource.Id.time);

      var lb = new Rect(); // List bounds

      left.Touch += (sender, e) => {
        switch (e.Event.Action) {
          case MotionEventActions.Move:
            time.SetTextColor(Color.Red);

            list.GetGlobalVisibleRect(lb);

            // The event x in local to the list view.
            var x = (int)e.Event.RawX;

            if (x < lb.Left) {
              x = lb.Left;
            } else if (x > (rightTabX - right.Width)) {
              x = rightTabX - right.Width;
            }

            leftTabX = x;
            left.SetX(leftTabX - left.Width);
            if (lb.Width() != 0) {
              leftOverlay.width = x - lb.Left;
              list.Invalidate();
              UpdateGraphSelection();
            }
            break;
          case MotionEventActions.Up:
            time.SetTextColor(Color.Black);
            break;
        }
      };

      right.Touch += (sender, e) => {
        switch (e.Event.Action) {
          case MotionEventActions.Move:
            time.SetTextColor(Color.Red);

            list.GetGlobalVisibleRect(lb);

            // The event x in local to the list view.
            var x = (int)e.Event.RawX;

            if (x < leftTabX + left.Width) {
              x = leftTabX + left.Width;
            } else if (x > (lb.Left + plotWidth)) {
              x = lb.Left + plotWidth;
            }

            rightTabX = x;
            right.SetX(rightTabX - right.Width);
            if (lb.Width() != 0) {
              rightOverlay.width = (lb.Left + plotWidth) - x;
              list.Invalidate();
              UpdateGraphSelection();
            }
            break;
          case MotionEventActions.Up:
            time.SetTextColor(Color.Black);
            break;
        }
      };

      SetCardViewTitleVisible(view, true);

      graphAdapter = new GraphSelectionAdapter(ion);
      list.SetAdapter(graphAdapter);

      var green = new Android.Graphics.Color(Resource.Color.green);
      leftOverlay = new SelectionDrawable(SelectionDrawable.EAlign.Left, green, 0);
      rightOverlay = new SelectionDrawable(SelectionDrawable.EAlign.Right, green, 0);

      list.Overlay.Add(leftOverlay);
      list.Overlay.Add(rightOverlay);
    }

    /// <summary>
    /// Invalidates the content of the new saved cards view.
    /// </summary>
    private void InvalidateNewSavedViews() {
      var view = newSavedCard;
      var title = view.FindViewById(Resource.Id.title);
      var text = view.FindViewById(Resource.Id.text);
      var check = view.FindViewById<CheckBox>(Resource.Id.state);

      check.Checked = report != null;
    }

    /// <summary>
    /// Invalidates all of the views that are present within the selection card.
    /// </summary>
    private void InvalidateSelectionViews() {
      var view = selectionCard;
      var title = view.FindViewById(Resource.Id.title);
      var text = view.FindViewById(Resource.Id.text);
      var check = view.FindViewById<CheckBox>(Resource.Id.state);
      var list = view.FindViewById<RecyclerView>(Resource.Id.list);

      check.Checked = report != null && report.results != null && report.results.Count > 0;
    }

    /// <summary>
    /// Invalidates all of the views that are present witin the selection card.
    /// </summary>
    private async void InvalidateGraphViews() {
      var view = graphCard;
      var title = view.FindViewById(Resource.Id.title);
      var text = view.FindViewById(Resource.Id.text);
      var check = view.FindViewById<CheckBox>(Resource.Id.state);
      var list = view.FindViewById<RecyclerView>(Resource.Id.list);
      var left = view.FindViewById(Resource.Id.left);
      var right = view.FindViewById(Resource.Id.right);

      // Build the logs that will be included in the graph selection.
      var logs = new List<DeviceSensorLogs>();
      foreach (var s in sessionAdapter.GetCheckedSessions()) {
        var sessionData = await ion.dataLogManager.QuerySessionData(s.id);
        logs.AddRange(sessionData.deviceSensorLogs);
      }

      absoluteStart = DateTime.Now;

      foreach (var log in logs) {
        if (log.start < absoluteStart) {
          absoluteStart = localStart = log.start;
        }

        if (log.end > absoluteEnd) {
          absoluteEnd = localEnd = log.end;
        }
      }

      graphAdapter.SetLogs(logs);

      // Bail if we don't have anything to graph.
      if (graphAdapter.ItemCount <= 0) {
        return;
      }

      UpdateGraphSelection();

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

      var plot = row.FindViewById<PlotView>(Resource.Id.content);
      var w = plotWidth = plot.Width;
      leftOverlay.plotWidth = w;
      rightOverlay.plotWidth = w;

      var lb = new Rect(); // List bounds
      list.GetGlobalVisibleRect(lb);

      if (leftTabX == 0) {
        leftTabX = lb.Left;
      }

      if (rightTabX == 0) {
        rightTabX = lb.Left + plotWidth;
      }

      left.SetX(leftTabX - left.Width / 2);
      right.SetX(rightTabX - right.Width / 2);

      list.Invalidate();
    }

    /// <summary>
    /// Updates the graph selection based on the overlays.
    /// </summary>
    private void UpdateGraphSelection() {
      var view = graphCard;
      var time = view.FindViewById<TextView>(Resource.Id.time);

      var interval = absoluteEnd - absoluteStart;
      var sd = TimeSpan.FromTicks((long)(interval.Ticks * (leftOverlay.width / (double)leftOverlay.plotWidth)));
      var ed = TimeSpan.FromTicks((long)(interval.Ticks * (1 - ((plotWidth - rightOverlay.width) / (double)rightOverlay.plotWidth))));

      localStart = absoluteStart + sd;
      localEnd = absoluteEnd - ed;

      time.Text = localStart.ToShortDateString() + " " + localStart.ToShortTimeString() + "\t\t" +
        localEnd.ToShortDateString() + " " + localEnd.ToShortTimeString(); 
    }

    /// <summary>
    /// Marks the given card view as focused. It will epand the card view.
    /// </summary>
    /// <param name="cardView">Card view.</param>
    private void MarkCardViewAsFocused(CardView cardView) {
      if (focusedCard != null) {
        SetCardViewTitleVisible(focusedCard, true);
      }

      SetCardViewTitleVisible(cardView, false);
      focusedCard = cardView;
    }

    /// <summary>
    /// Toggles the card view's content
    /// </summary>
    /// <param name="cardView">Card view.</param>
    private void ToggleCardViewContent(CardView cardView) {
      if (ViewStates.Visible == cardView.FindViewById(Resource.Id.title).Visibility) {
        SetCardViewTitleVisible(cardView, false);
      } else {
        SetCardViewTitleVisible(cardView, true);
      }
    }

    /// <summary>
    /// Sets whether or not the card view's title view is visible.
    /// </summary>
    /// <param name="visible">If set to <c>true</c> visible.</param>
    private void SetCardViewTitleVisible(CardView cardView, bool visible) {
      var title = cardView.FindViewById(Resource.Id.status);
      var content = cardView.FindViewById(Resource.Id.content);

      if (visible) {
        title.Visibility = ViewStates.Visible;
        content.Visibility = ViewStates.Gone;
      } else {
        title.Visibility = ViewStates.Gone;
        content.Visibility = ViewStates.Visible;
      }
    }

    /// <summary>
    /// Flips a view, hiding one view and revealing another as the flip finished.
    /// </summary>
    /// <param name="view">View.</param>
    /// <param name="hideId">Hide identifier.</param>
    /// <param name="revealId">Reveal identifier.</param>
    private void FlipView(View card, int hideId, int revealId) {
      var fo = AnimatorInflater.LoadAnimator(this, Resource.Animation.card_flip_left_out);
      fo.AnimationEnd += (obj, args) => {
        Log.D(this, "Animation end");
        card.FindViewById(hideId).Visibility = ViewStates.Gone;
        card.FindViewById(revealId).Visibility = ViewStates.Visible;
      };

      var fi = AnimatorInflater.LoadAnimator(this, Resource.Animation.card_flip_left_in);

      var animationSet = new AnimatorSet();
      animationSet.Play(fo)
        .Before(fi);
      animationSet.SetTarget(card);
      animationSet.Start();
    }
  }

  class SelectionDrawable : Drawable {
    /// <summary>
    /// The width of the plot.
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
          bounds.Left = bounds.Right - width;
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

