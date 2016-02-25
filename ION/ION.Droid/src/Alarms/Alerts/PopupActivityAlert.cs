namespace ION.Droid {
  
  using System;

  using Android.Content;
  using Android.OS;

  using ION.Core.Alarms;
  using ION.Core.Alarms.Alerts;

  /// <summary>
  /// An implementation of IAlarmAlert that will display an activity describing the alarm that triggered.
  /// </summary>
  public class PopupActivityAlert : AbstractAlarmAlert {
    /// <summary>
    /// The context that is used to start the alarm activity.
    /// </summary>
    private Context context; 

    public PopupActivityAlert(IAlarm alarm, Context context) : base(alarm) {
      this.context = context;
    }

    /// <summary>
    /// Starts the alert mode. Ie. if the mode is a UI element, this is where the
    /// UI changes are commited and displayed to the user.
    /// </summary>
    /// <returns>True if the alert was started, false otherwise.</returns>
    protected override bool OnStart() {
      return true;
    }

    /// <summary>
    /// Called by the alert when it is stopped.
    /// </summary>
    protected override void OnStop() {
    }
  }
}

