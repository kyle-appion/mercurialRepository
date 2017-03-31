namespace ION.Droid.Alarms.Alerts {

  using System;

  using Android.Media;

	using Appion.Commons.Util;

  using ION.Core.Alarms;
  using ION.Core.Alarms.Alerts;

  using ION.Droid.App;

  public class ToneAlarmAlert : AbstractAlarmAlert {

		private BaseAndroidION ion;
    private MediaPlayer mp;
    private Android.Net.Uri uri;

		public ToneAlarmAlert(IAlarm alarm, BaseAndroidION ion) : this(alarm, ion, RingtoneManager.GetDefaultUri(RingtoneType.Alarm)) {
    }

		public ToneAlarmAlert(IAlarm alarm, BaseAndroidION ion, Android.Net.Uri uri) : base(alarm) {
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
        mp.SetDataSource(ion.context, uri);
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

