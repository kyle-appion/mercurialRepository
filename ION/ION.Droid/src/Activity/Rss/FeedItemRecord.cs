namespace ION.Droid.Activity.Rss {

  using System;

  using Android.Content;
  using Android.Text;
  using Android.Views;
  using Android.Widget;

  using ION.Core.IO;

  using ION.Droid.Widgets.RecyclerViews;
  using ION.Droid.Util;

  public class FeedItemRecord : RecordAdapter.Record<RssItem> {
    // Overridden from RecordAdapter.Record
    public override int viewType { get { return 0; } }

    public FeedItemRecord(RssItem rss) : base(rss) {
    }
  }

  public class FeedItemViewHolder : RecordAdapter.RecordViewHolder<FeedItemRecord> {
    public TextView title;
    public TextView date;
    public TextView content;

    public FeedItemViewHolder(ViewGroup parent) : base(parent, Resource.Layout.list_rss_item) {
      title = ItemView.FindViewById<TextView>(Resource.Id.title);
      date = ItemView.FindViewById<TextView>(Resource.Id.date);
      content = ItemView.FindViewById<TextView>(Resource.Id.text);
    }

    public override void Invalidate() {
      base.Invalidate();

      title.Text = record.data.title;
      date.Text = record.data.publishDate.ToFullShortString();
      content.Text = Html.FromHtml(record.data.description).ToString();
    }
  }
}
