namespace ION.Core.Alarms {

  using System;


  /// <summary>
  /// The delegate that is called when an alarm is throws an alarm event.
  /// </summary>
  public delegate void OnAlarmEvent(AlarmEvent alarmEvent);

  public class AlarmEvent {
    public IAlarm alarm;
    public EType type;

    public AlarmEvent(IAlarm alarm, EType type) {
      this.alarm = alarm;
      this.type = type;
    }

    public enum EType {
      /// <summary>
      /// Called when the alarm is triggered.
      /// </summary>
      Triggered,
      /// <summary>
      /// Called when the alarm is reset.
      /// </summary>
      Reset,
      /// <summary>
      /// Called when the alarm is cancelled and will stop firing (if it is).
      /// </summary>
      Cancelled,
      /// <summary>
      /// Called when properties such as the alarm's name or description change.
      /// </summary>
      Changed,
    }
  }

  public interface IAlarm : IDisposable {
    /// <summary>
    /// The event pool that is alerted when the alarm throws an alarm event.
    /// </summary>
    event OnAlarmEvent onAlarmEvent;

    /// <summary>
    /// The unique identifier for the alarm. Do not set this yourself; the alarm manager will resolve it for you.
    /// </summary>
    /// <value>The identifier.</value>
    int id { get; set; }
    /// <summary>
    /// The name of the alarm.
    /// </summary>
    string name { get; set; }

    /// <summary>
    /// The description of the alarm.
    /// </summary>
    /// <value>The description.</value>
    string description { get; set; }

    /// <summary>
    /// Whether or not the alarm is enabled. If the alarm is not enabled,
    /// it will not fire its trigger function. The only exception to this
    /// if Fire(force=true).
    /// </summary>
    /// <value><c>true</c> if is enabled; otherwise, <c>false</c>.</value>
    bool enabled { get; set; }

    /// <summary>
    /// Whether or not the alarm is pending a reset. A reset alarm is an alarm
    /// who is technically triggered, but will not fire until its state switches
    /// back to untriggered.
    /// </summary>
    /// <value><c>true</c> if is pending reset; otherwise, <c>false</c>.</value>
    bool pendingReset { get; }

    /// <summary>
    /// Fires the alarm's trigger function.
    /// </summary>
    /// <remarks>
    /// If force is true, then the trigger function will fire even if the alarm
    /// is pending reset.
    /// </remarks>
    /// <param name="force">If set to <c>true</c> force.</param>
    bool Fire(bool force=false);

    /// <summary>
    /// Queries whether or not the alarm is triggered.
    /// </summary>
    /// <returns><c>true</c> if this instance is triggered; otherwise, <c>false</c>.</returns>
    bool IsTriggered();

    /// <summary>
    /// Resets the alarm such that it will not be triggered, but is still enabeled. The alarm will trigger again once
    /// it leaves its trigger range.
    /// </summary>
    void Reset();

    /// <summary>
    /// Cancels the alarm.
    /// </summary>
    void Cancel();
  }
}

