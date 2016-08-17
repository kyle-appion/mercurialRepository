namespace ION.Droid.Dialog {

  using System;

  using Android.App;
  using Android.Content;
  using Android.OS;
  using Android.Util;
  using Android.Preferences;
  using Android.Views;
  using Android.Widget;

  using ION.Core.Util;

  public class DateTimePickerDialog {

    /// <summary>
    /// The title for the dialog.
    /// </summary>
    /// <value>The title.</value>
    public string title { get; set; }
    /// <summary>
    /// The handler that will receive the picked date time.
    /// </summary>
    public EventHandler<DateTime> handler;

    private DateTime start;
    private DateTime end;
    private DateTime now;

    public DateTimePickerDialog(string title, DateTime start, DateTime end, DateTime now, EventHandler<DateTime> handler) {
      this.title = title;
      this.handler = handler;
      this.start = start;
      this.end = end;
      this.now = now;
    }

    /// <summary>
    /// Shows the dialog.
    /// </summary>
    /// <param name="context">Context.</param>
    public AlertDialog Show(Context context) {
      var view = LayoutInflater.From(context).Inflate(Resource.Layout.dialog_date_time, null);

      var date = view.FindViewById<DatePicker>(Resource.Id.date);
      var time = view.FindViewById<TimePicker>(Resource.Id.time);

      var hours = 0;
      var minutes = 0;

      time.TimeChanged += (sender, e) => {
        hours = e.HourOfDay;
        minutes = e.Minute;
      };

      date.MinDate = start.ToUTCMilliseconds();
      date.MaxDate = end.ToUTCMilliseconds();
      date.CalendarViewShown = false;

      var adb = new IONAlertDialog(context);
      adb.SetTitle(title);
      adb.SetView(view);
      adb.SetNegativeButton(context.GetString(Resource.String.cancel), (obj, args) => {
        var dialog = obj as Android.App.Dialog;
        dialog.Dismiss();
      });

      adb.SetPositiveButton(context.GetString(Resource.String.ok_done), (obj, args) => {
        var dialog = obj as Android.App.Dialog;
        dialog.Dismiss();

        var dateTime = date.DateTime;
        dateTime = dateTime.AddHours(hours);
        dateTime = dateTime.AddMinutes(minutes);

        if (handler != null) {
          handler(obj, dateTime);
        }
      });

      return adb.Show();
    }
  }
}

