using ION.Droid.Views;
namespace ION.Droid.Activity.Rss {

  using System;
  using System.Collections.Generic;

  using Android.App;
  using Android.Content;
	using Android.Content.PM;
	using Android.OS;
  using Android.Support.V7.Widget;
  using Android.Views;
  using Android.Widget;

  using ION.Core.App;
  using ION.Core.IO;

  using ION.Droid.App;
  using ION.Droid.Widgets.RecyclerViews;

  [Activity(Label="@string/rss_feed", Theme="@style/TerminalActivityTheme", ScreenOrientation=ScreenOrientation.Portrait)]
  public class RssActivity : IONActivity {

    private RecyclerView list;
    private RssAdapter adapter;
    private DownloadRssTask task;

    protected override void OnCreate(Bundle savedInstanceState) {
      base.OnCreate(savedInstanceState);
      SetContentView(Resource.Layout.activity_rss);

      ActionBar.SetDisplayHomeAsUpEnabled(true);

      list = FindViewById<RecyclerView>(Resource.Id.list);
      adapter = new RssAdapter((obj) => {
        var uri = Android.Net.Uri.Parse(obj.link);
        var i = new Intent();
        i.SetData(uri);
        i.AddFlags(ActivityFlags.NewTask);
        StartActivity(i);
      });
      list.SetAdapter(adapter); 
    }

    protected override void OnResume() {
      base.OnResume();
      task = new DownloadRssTask(this.ion, this, (obj) => {
        adapter.SetItems(obj.channelFeed.items);
      });
      task.Execute();
    }

    protected override void OnPause() {
      base.OnPause();
      if (task != null) {
        task.Cancel(true);
      }
    }

    public override bool OnMenuItemSelected(int featureId, IMenuItem item) {
      switch (item.ItemId) {
        case Android.Resource.Id.Home: {
          SetResult(Result.Canceled);
          Finish();
          return true;
        } // Android.Resource.Id.Home

        default: {
          return false;
        }        
      }
    }

    private class DownloadRssTask : AsyncTask {
      private BaseAndroidION ion;
      private Context context;
      private Action<Rss> onComplete;
      private ProgressDialog dialog;

      private Rss rss;

      public DownloadRssTask(BaseAndroidION ion, Context context, Action<Rss> onComplete) {
        this.ion = ion;
        this.context = context;
        this.onComplete = onComplete;
      }

      protected override void OnPreExecute() {
        dialog = new ProgressDialog(context);
        dialog.SetTitle(Resource.String.downloading);
        dialog.SetMessage(context.GetString(Resource.String.please_wait));
        dialog.SetCancelable(false);
        dialog.Show();
      }

      protected override Java.Lang.Object DoInBackground(params Java.Lang.Object[] native_parms) {
        rss = ion.portal.DownloadRssAsync().Result;
        return null;
      }

      protected override void OnCancelled() {
        base.OnCancelled();
        dialog.Dismiss();
      }

      protected override void OnPostExecute(Java.Lang.Object result) {
        dialog.Dismiss();
        onComplete(rss);
      }
    }
  }

  /// <summary>
  /// The adapter that will provide the views for the rss feed.
  /// </summary>
  internal class RssAdapter : RecordAdapter {

    private Action<FeedItem> onFeedItemClicked;

    public RssAdapter(Action<FeedItem> onFeedItemClicked) {
      this.onFeedItemClicked = onFeedItemClicked;
    }

    public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType) {
      var ret = new FeedItemViewHolder(parent);
      return ret;
    }

    public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position) {
      var vh = holder as FeedItemViewHolder;
      vh.record = this[position] as FeedItemRecord;
      vh.ItemView.SetOnClickListener(new ViewClickAction((view) => {
        onFeedItemClicked(((FeedItemRecord)this[position]).data);
      }));
    }

    public void SetItems(IEnumerable<FeedItem> feedItems) {
      records.Clear();

      foreach (var i in feedItems) {
        records.Add(new FeedItemRecord(i));
      }

      NotifyDataSetChanged();
    }
  }
}
