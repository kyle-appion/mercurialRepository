using System;

namespace ION.Core.Alarms.Alerts {
  /// <summary>
  /// An alarm alert comprised of multiple other alarm alerts.
  /// </summary>
  /// <remarks>
  /// Note: all the alarms in the compound alert MUST share the same alarm.
  /// </remarks>
  public class CompoundAlarmAlert : IAlarmAlert {
    // Overridden from IAlarmAlert
    public IAlarm alarm { get; private set; }
    // Overridden from IAlarmAlert
    public bool isStarted { get; private set; }

    /// <summary>
    /// The alerts that this alert is comprised of.
    /// </summary>
    /// <value>The alerts.</value>
    private IAlarmAlert[] alerts { get; set; }

    public CompoundAlarmAlert(IAlarm alarm, params IAlarmAlert[] alerts) {
      this.alarm = alarm;
      this.alerts = alerts;

      foreach (var alert in alerts) {
        if (alert.alarm != alarm) {
          throw new Exception("Cannot create CompountAlarmAlert: alarms don't match");
        }
      }
    }

    // Overridden from IAlarmAlert
    public bool Start() {
      if (isStarted) {
        return false;
      }

      foreach (var alert in alerts) {
        alert.Start();
      }

      return isStarted = true;
    }

    // Overridden from IAlarmAlert
    public void Stop() {
      foreach (var alert in alerts) {
        alert.Stop();
      }

      isStarted = false;
    }
  }
}

