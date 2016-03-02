namespace ION.Droid.Alarms.Alerts {

  using System;

  using Android.Content;
  using Android.OS;

  using ION.Core.Alarms;
  using ION.Core.Alarms.Alerts;
  using ION.Core.App;

  using ION.Droid.App;

  public class VibrateAlarmAlert : AbstractAlarmAlert {

    private const long PULSE_LENGTH = 350;
    private const long INTERVAL_LENGTH = 500;

    private AndroidION ion;
    private Vibrator vibrator;


    public VibrateAlarmAlert(IAlarm alarm, AndroidION ion) : base(alarm) {
      this.ion = ion;
    }

    /// <summary>
    /// Called by the alert when it is started.
    /// </summary>
    protected override bool OnStart() {
      if (!ion.preferences.alarm.allowsVibrate) {
        return false;
      }

      vibrator = ion.GetSystemService(Context.VibratorService) as Vibrator;

      var pl = PULSE_LENGTH;
      var il = INTERVAL_LENGTH;

      vibrator.Vibrate(new long[] { pl, il }, 0);

      return true;
    }

    /// <summary>
    /// Called by the alert when it is stopped.
    /// </summary>
    protected override void OnStop() {
      if (vibrator != null) {
        vibrator.Cancel();
        vibrator = null; 
      }
    }
  }
}

