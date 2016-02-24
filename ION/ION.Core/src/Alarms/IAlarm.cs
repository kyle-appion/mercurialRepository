using System;

namespace ION.Core.Alarms {

  /// <summary>
  /// The delegate that is called when an alarm is triggered.
  /// </summary>
  public delegate void OnAlarmTriggered(IAlarm alarm);
  /// <summary>
  /// The delegate that is called when the alarm's data is changed.
  /// </summary>
  public delegate void OnAlarmChanged(IAlarm alarm);

  public interface IAlarm : IDisposable {
    /// <summary>
    /// The event pool that allows for notifications of the alarm trigger.
    /// </summary>
    event OnAlarmTriggered onAlarmTriggered;
    /// <summary>
    /// The event pool that allows for notifications of when the alarm's data
    /// changes.
    /// </summary>
    event OnAlarmChanged onAlarmChanged;

    /// <summary>
    /// The unique identifier for the alarm. Do not set this yourself; the alarm manager will resolve it for you.
    /// </summary>
    /// <value>The identifier.</value>
    uint id { get; set; }
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
    /// Resets the alarm. PendingReset will be set to false and any other alarm
    /// cleanup should happen here. This method should be called after a failed
    /// fire attempt (an attempt that didn't meet any of its fire criteria.
    /// </summary>
    void Reset();
  }
}

