namespace ION.Droid.Dialog {

  using System;

  using Android.App;
  using Android.Content;
  using Android.Text;
  using Android.Views;
  using Android.Widget;

  using ION.Core.IO;

  public class RssDialog {


    public Context context { get; private set; }

    private Rss rss;

    public RssDialog(Context context, Rss rss) {
      this.context = context;
      this.rss = rss;
    }

    public Dialog Show() {
      var item = rss.channels[0].items[rss.channels[0].items.Count - 1];

      var view = LayoutInflater.From(context).Inflate(Resource.Layout.dialog_rss, null);

      var title = view.FindViewById<TextView>(Resource.Id.title);
			var content = view.FindViewById<TextView>(Resource.Id.content);

      title.Text = item.title;
      content.Text = Html.FromHtml(item.description).ToString();

			var adb = new IONAlertDialog(context);
      adb.SetPositiveButton(Resource.String.open, (sender, e) => {
        var uri = Android.Net.Uri.Parse(item.link);
        var intent = new Intent();
        intent.SetData(uri);
        intent.AddFlags(ActivityFlags.NewTask);
        context.StartActivity(intent);
      });
      adb.SetNegativeButton(Resource.String.close, (sender, e) => {
      });

      adb.SetTitle(Resource.String.rss);
      adb.SetView(view);
      var ret = adb.Create();
      ret.Show();
      return ret;
    }
  }
}
