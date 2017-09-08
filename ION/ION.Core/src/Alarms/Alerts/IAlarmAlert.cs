namespace ION.Core.Alarms.Alerts {

  using System;

  /// <summary>
  /// An alert mode is an interface that abstract the way an alarm might
  /// alert another entity (user, system, etc...)
  /// </summary>
  public interface IAlarmAlert {
    /// <summary>
    /// The alarm that this alert is for.
    /// </summary>
    /// <value>The alarm.</value>
    IAlarm alarm { get; }
    /// <summary>
    /// Whether or not the alert mode is started.
    /// </summary>
    /// <value><c>true</c> if is started; otherwise, <c>false</c>.</value>
    bool isStarted { get; }

    /// <summary>
    /// Starts the alert mode. Ie. if the mode is a UI element, this is where the
    /// UI changes are commited and displayed to the user.
    /// </summary>
    /// <returns>True if the alert was started, false otherwise.</returns>
    bool Start();

    /// <summary>
    /// Stops the alert mode. This is where the mode is torn down and wholly stopped.
    /// Ie. any UI elements are torn down and removed from the user's attention. After
    /// Stop is calles, it is an error to start again until the alarm alert is reset.
    /// </summary>
    void Stop();
  }
}

