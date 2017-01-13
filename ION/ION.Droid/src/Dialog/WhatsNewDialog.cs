namespace ION.Droid.Dialog {

  using System.Collections.Generic;
  using System.Text;
  using System.Threading.Tasks;

  using Android.App;
  using Android.Content;
  using Android.OS;
  using Android.Text;
  using Android.Views;
  using Android.Widget;

  using ION.Core.Content;

	using ION.Droid.Preferences;

  /// <summary>
  /// A dialog that will show the user a list of what is new in the application.
  /// </summary>
  public class WhatsNewDialog {
    private Context context; 
		private AppPrefs prefs;
		private AppVersion oldVersion;
		private AppVersion currentVersion;
    private Handler handler;

    public WhatsNewDialog(Context context, AppPrefs prefs, AppVersion oldVersion, AppVersion currentVersion) {
      this.context = context;
			this.prefs = prefs;
			this.oldVersion = oldVersion;
			this.currentVersion = currentVersion;
      this.handler = new Handler();
    }

    /// <summary>
    /// Shows the dialog to the user.
    /// </summary>
    public Dialog Show(bool showVersion=false) {
      var view = LayoutInflater.From(context).Inflate(Resource.Layout.dialog_whats_new, null, false);

      var content = view.FindViewById<TextView>(Resource.Id.content);
      var checkbox = view.FindViewById<CheckBox>(Resource.Id.checkbox);
      checkbox.CheckedChange += (sender, e) => {
        prefs.showWhatsNew = e.IsChecked;
      };

      var adb = new IONAlertDialog(context);
      adb.SetTitle(string.Format(context.GetString(Resource.String.whats_new_in), currentVersion));
      adb.SetView(view);

      adb.SetNegativeButton(Resource.String.ok_done, (obj, e) => {
        var dialog = obj as Dialog;
        dialog.Dismiss();
      });

      var ret = adb.Show();

      Task.Factory.StartNew(() => {
        var sb = new StringBuilder();

				var news = new List<WhatsNew>();
				news.AddRange(WhatsNew.ParseWithException(context.Resources.GetXml(Resource.Xml.whats_new)));
				news.AddRange(WhatsNew.ParseWithException(context.Resources.GetXml(Resource.Xml.whats_new_history)));

				// Trim the unnecessary versions.
				for (int i = news.Count - 1; i >= 0; i--) {
					if (news[i].versionCode.CompareTo(oldVersion) < 0 || news[i].versionCode.CompareTo(currentVersion) > 0) {
						news.RemoveAt(i);
					}
				}

				if (showVersion) {
					PrintIndividualUpdates(news, sb);
				} else {
					PrintCollapsedUpdates(news, sb);
				}
   

        handler.Post(() => {
					Appion.Commons.Util.Log.D(this, "Posting: " + sb.ToString());
          content.TextFormatted = Html.FromHtml(sb.ToString());
        });
      });

      return ret;
    }

		private void PrintCollapsedUpdates(List<WhatsNew> news, StringBuilder sb) {
			if (news.Count <= 0) {
				return;
			}

			sb.Append("<br><b>").Append(context.GetString(Resource.String._new)).Append("</b><br>");
			foreach (var wn in news) {
				foreach (var n in wn.whatsNew) {
					sb.Append("•\t").Append(n).Append("<br>");
				}
			}

			sb.Append("<br><b>").Append(context.GetString(Resource.String.updated)).Append("</b><br>");
			foreach (var wn in news) {
				foreach (var u in wn.whatsUpdated) {
					sb.Append("•\t").Append(u).Append("<br>");
				}
			}

				sb.Append("<br><b>").Append(context.GetString(Resource.String._fixed)).Append("</b><br>");
			foreach (var wn in news) {
				foreach (var f in wn.whatsFixed) {
					sb.Append("•\t").Append(f).Append("<br>");
				}
			}
		}

		private void PrintIndividualUpdates(List<WhatsNew> news, StringBuilder sb) {
			foreach (var wn in news) {
				sb.Append("<h2>").Append(wn.versionCode).Append("</h2>");

				if (wn.whatsNew.Count > 0) {
					sb.Append("<br><b>").Append(context.GetString(Resource.String._new)).Append("</b><br>");
					foreach (var n in wn.whatsNew) {
						sb.Append("•\t").Append(n).Append("<br>");
					}
				}

				if (wn.whatsUpdated.Count > 0) {
					sb.Append("<br><b>").Append(context.GetString(Resource.String.updated)).Append("</b><br>");
					foreach (var u in wn.whatsUpdated) {
						sb.Append("•\t").Append(u).Append("<br>");
					}
				}

				if (wn.whatsFixed.Count > 0) {
					sb.Append("<br><b>").Append(context.GetString(Resource.String._fixed)).Append("</b><br>");
					foreach (var f in wn.whatsFixed) {
						sb.Append("•\t").Append(f).Append("<br>");
					}
				}
			}
		}
  }
}

