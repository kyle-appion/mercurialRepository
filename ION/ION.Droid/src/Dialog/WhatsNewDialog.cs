namespace ION.Droid.Dialog {

  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;

  using Android.App;
  using Android.Content;
  using Android.OS;
  using Android.Runtime;
  using Android.Text;
  using Android.Views;
  using Android.Widget;

  using ION.Core.Content;

  using ION.Droid.App;

  /// <summary>
  /// A dialog that will show the user a list of what is new in the application.
  /// </summary>
  public class WhatsNewDialog {
    private AndroidION ion;
    private Context context; 
    private Handler handler;

    public WhatsNewDialog(AndroidION ion, Context context) {
      this.ion = ion;
      this.context = context;
      this.handler = new Handler();
    }

    /// <summary>
    /// Shows the dialog to the user.
    /// </summary>
    public Dialog Show() {
      var view = LayoutInflater.From(context).Inflate(Resource.Layout.dialog_whats_new, null, false);

      var content = view.FindViewById<TextView>(Resource.Id.content);
      var checkbox = view.FindViewById<CheckBox>(Resource.Id.checkbox);
      checkbox.CheckedChange += (sender, e) => {
        ion.preferences.showWhatsNew = e.IsChecked;
      };

      var adb = new IONAlertDialog(context);
      adb.SetTitle(string.Format(context.GetString(Resource.String.whats_new_in), ion.version));
      adb.SetView(view);

      adb.SetNegativeButton(Resource.String.ok_done, (obj, e) => {
        var dialog = obj as Dialog;
        dialog.Dismiss();
      });

      var ret = adb.Show();

      Task.Factory.StartNew(() => {
        var sb = new StringBuilder();

        var whatsNew = WhatsNew.ParseWithException(context.Resources.GetXml(Resource.Xml.whats_new));
        foreach (var wn in whatsNew) {
          sb.Append("<h2>").Append(wn.versionCode).Append("</h2>");

          sb.Append("<br><b>").Append(context.GetString(Resource.String._new)).Append("</b><br>");
          foreach (var n in wn.whatsNew) {
            sb.Append("•\t").Append(n).Append("<br>");
          }

          sb.Append("<br><b>").Append(context.GetString(Resource.String.updated)).Append("</b><br>");
          foreach (var u in wn.whatsUpdated) {
            sb.Append("•\t").Append(u).Append("<br>");
          }

          sb.Append("<br><b>").Append(context.GetString(Resource.String._fixed)).Append("</b><br>");
          foreach (var f in wn.whatsFixed) {
            sb.Append("•\t").Append(f).Append("<br>");
          }
        }

        handler.Post(() => {
          content.TextFormatted = Html.FromHtml(sb.ToString());
        });
      });

      return ret;
    }
  }
}

