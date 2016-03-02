namespace ION.Droid.Alarms.Alerts {

  using System;

  using Android.Content;
  using Android.Media;
  using Android.OS;

  using ION.Core.Alarms;
  using ION.Core.Alarms.Alerts;
  using ION.Core.App;
  using ION.Core.Util;

  public class ToneAlarmAlert : AbstractAlarmAlert {

    private Context context;
    private MediaPlayer mp;
    private Android.Net.Uri uri;

    public ToneAlarmAlert(IAlarm alarm, Context context) : this(alarm, context, RingtoneManager.GetDefaultUri(RingtoneType.Alarm)) {
    }

    public ToneAlarmAlert(IAlarm alarm, Context context, Android.Net.Uri uri) : base(alarm) {
      this.context = context;
      this.uri = uri;
    }

    /// <summary>
    /// Called by the alert when it is started.
    /// </summary>
    protected override bool OnStart() {
      try {
        mp = new MediaPlayer();
        mp.SetDataSource(context, uri);
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

