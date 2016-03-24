namespace ION.Droid.Activity.DataLogging {

  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;

  using Android.Animation;
  using Android.App;
  using Android.Content;
  using Android.OS;
  using Android.Runtime;
  using Android.Support.V7.Widget;
  using Android.Views;
  using Android.Widget;

  using ION.Core.Database;
  using ION.Core.Report.DataLogs;
  using ION.Core.Util;

  using ION.Droid.Activity;
  using ION.Droid.Widgets.Adapters.DataLogging;
  using ION.Droid.Views;

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
    private RecyclerView.Adapter currentAdapter;
    /// <summary>
    /// The adapter that will allow for the selection of jobs.
    /// </summary>
    private JobsSelectionAdapter jobAdapter;
    /// <summary>
    /// The adapter that will allow for the selection of sessions.
    /// </summary>
    private SessionSelectionAdapter sessionAdapter;
    /// <summary>
    /// The adapter that will display the session content graphs. 
    /// </summary>
    private GraphSelectionAdapter graphAdapter;
    /// <summary>
    /// The set that will contain all the device sensor logs that will be displayed in the graph list.
    /// </summary>
    private HashSet<DeviceSensorLogs> logs = new HashSet<DeviceSensorLogs>();
    /// <summary>
    /// The data log report that
    /// </summary>
    private DataLogReport report;


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

/*
      SetCardViewTitleVisible(selectionCard, true);
      selectionCard.SetOnClickListener(new ViewClickAction((v) => {
        MarkCardViewAsFocused(selectionCard);
      }));

      SetCardViewTitleVisible(graphCard, true);
      graphCard.SetOnClickListener(new ViewClickAction((v) => {
        MarkCardViewAsFocused(graphCard);
      }));
*/

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
        Alert("Create a new view and progress to next step");
        report = new DataLogReport();
//        FlipView(view, Resource.Id.content, Resource.Id.status);
        InvalidateNewSavedViews();
        MarkCardViewAsFocused(selectionCard);
      }));
    }

    /// <summary>
    /// Initializes the views for the selection card view.
    /// </summary>
    private void InitSelectionViews() {
      var view = selectionCard;

      view.FindViewById<RecyclerView>(Resource.Id.list).SetLayoutManager(new LinearLayoutManager(this));

      SetCardViewTitleVisible(view, true);
      view.FindViewById(Resource.Id.report_job).SetOnClickListener(new ViewClickAction((v) => {
        Alert("Clicked job");
        viewJobs = true;
        InvalidateSelectionViews();
      }));

      view.FindViewById(Resource.Id.report_sessions).SetOnClickListener(new ViewClickAction((v) => {
        Alert("Clicked selection");
        viewJobs = false;
        InvalidateSelectionViews();
      }));

      view.FindViewById(Resource.Id.next).SetOnClickListener(new ViewClickAction(async (v) => {
        Alert("Graph clicked");
        var sessions = sessionAdapter.GetCheckedSessions();
        logs.Clear();
        foreach (var s in sessions) {
          foreach (var d in ion.dataLogManager.QuerySessionData(s).Result.deviceSensorLogs) {
            logs.Add(d);
          }
        }
        graphAdapter.SetLogs(logs);

        InvalidateGraphViews();
        MarkCardViewAsFocused(graphCard);
      }));

      jobAdapter = new JobsSelectionAdapter();
      sessionAdapter = new SessionSelectionAdapter();
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

      SetCardViewTitleVisible(view, true);

      graphAdapter = new GraphSelectionAdapter(ion);
      list.SetAdapter(graphAdapter);
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
    private async void InvalidateSelectionViews() {
      var view = selectionCard;
      var title = view.FindViewById(Resource.Id.title);
      var text = view.FindViewById(Resource.Id.text);
      var check = view.FindViewById<CheckBox>(Resource.Id.state);
      var list = view.FindViewById<RecyclerView>(Resource.Id.list);

      if (viewJobs) {
        var jobs = await ion.database.QueryForAllAsync<JobRow>();
        jobAdapter.SetJobs(jobs);
        list.SetAdapter(jobAdapter);
      } else {
        var sessions = await ion.database.QueryForAllAsync<SessionRow>();
        sessionAdapter.SetSessions(sessions);
        list.SetAdapter(sessionAdapter);
      }

      check.Checked = report != null && report.results != null && report.results.Count > 0;
    }

    /// <summary>
    /// Invalidates all of the views that are present witin the selection card.
    /// </summary>
    private void InvalidateGraphViews() {
      var view = graphCard;
      var title = view.FindViewById(Resource.Id.title);
      var text = view.FindViewById(Resource.Id.text);
      var check = view.FindViewById<CheckBox>(Resource.Id.state);
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
}

