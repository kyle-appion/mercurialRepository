namespace ION.IOS.Alarms.Alerts {

  using System;

  using AudioToolbox;
  using Foundation;

  using ION.Core.Alarms;
  using ION.Core.Alarms.Alerts;

  using ION.IOS.App;

  public class VibrateAlarmAlert : AbstractAlarmAlert {

//    private const long PULSE_LENGTH = 350;
//    private const long INTERVAL_LENGTH = 500;

    private IosION ion;

    public VibrateAlarmAlert(IAlarm alarm, IosION ion) : base(alarm) {
      this.ion = ion;
    }

    /// <summary>
    /// Called by the alert when it is started.
    /// </summary>
    protected override bool OnStart() {
      if (!ion.settings.alarm.haptic) {
        return false;
      }

      ion.PostToMain(PostVibrateTask);

      return true;
    }

    /// <summary>
    /// Called by the alert when it is stopped.
    /// </summary>
    protected override void OnStop() {
    }

    private void PostVibrateTask() {
      SystemSound.Vibrate.PlaySystemSound();
      if (isStarted) {
        ion.PostToMainDelayed(PostVibrateTask, TimeSpan.FromMilliseconds(500));
      }
    }
  }
}

