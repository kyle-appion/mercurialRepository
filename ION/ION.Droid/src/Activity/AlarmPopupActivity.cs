namespace ION.Droid.Activity {

  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;

  using Android.App;
  using Android.Content;
  using Android.OS;
  using Android.Runtime;
  using Android.Views;
  using Android.Widget;

  using ION.Core.Alarms;

  using ION.Droid.Views;

  [Activity(Label="@string/alarm", Theme="@style/DialogActivityTheme")]      
  public class AlarmPopupActivity : IONActivity {

    /// <summary>
    /// The extra that will retrieve an alarm id from an intent.
    /// </summary>
    public const string EXTRA_ALARM = "ION.Droid.Activity.extra.ALARM";

    /// <summary>
    /// Raises the create event.
    /// </summary>
    /// <param name="savedInstanceState">Saved instance state.</param>
    protected override void OnCreate(Bundle savedInstanceState) {
      base.OnCreate(savedInstanceState);
      SetContentView(Resource.Layout.activity_alarm_popup);

      var title = FindViewById<TextView>(Resource.Id.title);
      var text = FindViewById<TextView>(Resource.Id.text);

      var id = Intent.GetIntExtra(EXTRA_ALARM, 0);

      var am = ion.alarmManager;
      var alarm = ion.alarmManager.GetAlarm(id);

      title.Text = alarm.name;
      text.Text = alarm.description;

      FindViewById(Resource.Id.close).SetOnClickListener(new ViewClickAction((v) => {
        alarm.Cancel();
        Finish();
      }));

      FindViewById(Resource.Id.reenable).SetOnClickListener(new ViewClickAction((v) => {
        alarm.Cancel();
        alarm.enabled = true;
        Finish();
      }));
    }
  }
}

