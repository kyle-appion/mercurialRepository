namespace ION.Droid.Alarms.Alerts {

  using System;

  using Android.Content;
  using Android.Media;
  using Android.OS;

  using ION.Core.Alarms;
  using ION.Core.Alarms.Alerts;
  using ION.Core.App;
  using ION.Core.Util;

  using ION.Droid.App;

  public class ToneAlarmAlert : AbstractAlarmAlert {

    private AndroidION ion;
    private MediaPlayer mp;
    private Android.Net.Uri uri;

    public ToneAlarmAlert(IAlarm alarm, AndroidION ion) : this(alarm, ion, RingtoneManager.GetDefaultUri(RingtoneType.Alarm)) {
    }

    public ToneAlarmAlert(IAlarm alarm, AndroidION ion, Android.Net.Uri uri) : base(alarm) {
      this.ion = ion;
      this.uri = uri;
    }

    /// <summary>
    /// Called by the alert when it is started.
    /// </summary>
    protected override bool OnStart() {
      if (!ion.preferences.alarm.allowsSounds) {
        return false;
      }

      try {
        mp = new MediaPlayer();
        mp.SetDataSource(ion, uri);
        mp.SetAudioStreamType(Stream.Alarm);
        mp.Looping = true;
        mp.SetVolume(1.0f, 1.0f);
        mp.Prepare();
        mp.Start();
        return true;
      } catch (Exception e) {
        Log.E(this, "Failed to start tone alarm alert", e);
        OnStop();
        return false;
      }
    }

    /// <summary>
    /// Called by the alert when it is stopped.
    /// </summary>
    protected override void OnStop() {
      if (mp != null) {
        mp.Stop();
        mp.Release();
        mp = null;
      }
    }
  }
}

