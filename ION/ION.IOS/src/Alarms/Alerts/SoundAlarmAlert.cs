namespace ION.IOS.Alarms.Alerts {

  using System;

  using AudioToolbox;

  using ION.Core.Alarms;
  using ION.Core.Alarms.Alerts;

  using ION.IOS.App;

  public class SoundAlarmAlert : AbstractAlarmAlert {
    private IosION ion;
    private SystemSound sound;

    public SoundAlarmAlert(IAlarm alarm, IosION ion) : base(alarm) {
      this.ion = ion;
    }

    /// <summary>
    /// Called by the alert when it is started.
    /// </summary>
    protected override bool OnStart() {
      if (!ion.settings._alarm.allowsSounds || sound != null) {
        return false;
      }

      sound = new SystemSound(1005);

      ion.PostToMain(PostNotification);

      return true;
    }

    /// <summary>
    /// Called by the alert when it is stopped.
    /// </summary>
    protected override void OnStop() {
      if (sound != null) {
        sound.Close();
      }
      sound = null;
    }

    private void PostNotification() {
      if (isStarted && sound != null) {
        sound.PlaySystemSound();
        ion.PostToMainDelayed(PostNotification, TimeSpan.FromSeconds(3));
      }
    }
  }
}

