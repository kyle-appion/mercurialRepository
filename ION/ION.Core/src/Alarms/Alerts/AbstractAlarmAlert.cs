namespace ION.Core.Alarms.Alerts {

  using System;

  using ION.Core.Alarms;

  /// <summary>
  /// A simple implementation of an IAlarmAlert that implements the majority of the alert, leaving child classes to
  /// only have to worry about their alert effects.
  /// </summary>
  public abstract class AbstractAlarmAlert : IAlarmAlert {
    /// <summary>
    /// The alarm that this alert is for.
    /// </summary>
    /// <value>The alarm.</value>
    public IAlarm alarm { get; private set; }
    /// <summary>
    /// Whether or not the alert mode is started.
    /// </summary>
    /// <value>true</value>
    /// <c>false</c>
    public bool isStarted { get; private set; }

    public AbstractAlarmAlert(IAlarm alarm) {
      this.alarm = alarm;
    }

    /// <summary>
    /// Starts the alert mode. Ie. if the mode is a UI element, this is where the
    /// UI changes are commited and displayed to the user.
    /// </summary>
    /// <returns>True if the alert was started, false otherwise.</returns>
    public bool Start() {
      if (isStarted) {
        return false;
      }

      isStarted = OnStart();
      return isStarted;
    }

    /// <summary>
    /// Stops the alert mode. This is where the mode is torn down and wholly stopped.
    /// Ie. any UI elements are torn down and removed from the user's attention. After
    /// Stop is calles, it is an error to start again until the alarm alert is reset.
    /// </summary>
    public void Stop() {
      OnStop();
      isStarted = false;
    }

    /// <summary>
    /// Called by the alert when it is started.
    /// </summary>
    protected abstract bool OnStart();

    /// <summary>
    /// Called by the alert when it is stopped.
    /// </summary>
    protected abstract void OnStop();
  }
}

