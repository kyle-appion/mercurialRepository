namespace ION.Core.Alarms {

  using System;

  public abstract class AbstractAlarm : IAlarm {
    /// <summary>
    /// The event that is notified when an alarm event is thrown.
    /// </summary>
    public event OnAlarmEvent onAlarmEvent;

    /// <summary>
    /// The unique identifier for the alarm. Do not set this yourself; the alarm manager will resolve it for you.
    /// </summary>
    /// <value>The identifier.</value>
    public int id { get; set; }
    /// <summary>
    /// The name of the alarm.
    /// </summary>
    /// <value>The name.</value>
    public string name { 
      get {
        return __name;
      }
      set {
        __name = value;
        NotifyAlarmEvent(AlarmEvent.EType.Changed);
      }
    } string __name;
    /// <summary>
    /// The description of the alarm.
    /// </summary>
    /// <value>The description.</value>
    public string description {
      get {
        return __description;
      }
      set {
        __description = value;
        NotifyAlarmEvent(AlarmEvent.EType.Changed);
      }
    } string __description;
    /// <summary>
    /// Whether or not the alarm is enabled. If the alarm is not enabled,
    /// it will not fire its trigger function. The only exception to this
    /// if Fire(force=true).
    /// </summary>
    /// <value>true</value>
    /// <c>false</c>
    public bool enabled { get; set; }
    /// <summary>
    /// Whether or not the alarm is pending a reset. A reset alarm is an alarm
    /// who is technically triggered, but will not fire until its state switches
    /// back to untriggered.
    /// </summary>
    /// <value>true</value>
    /// <c>false</c>
    public bool pendingReset { get; private set; }

    public AbstractAlarm(string name, string description) {
      this.name = name;
      this.description = description;
    }

    /// <summary>
    /// Releases all resource used by the <see cref="ION.Core.Alarms.AbstractAlarm"/> object.
    /// </summary>
    /// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="ION.Core.Alarms.AbstractAlarm"/>. The
    /// <see cref="Dispose"/> method leaves the <see cref="ION.Core.Alarms.AbstractAlarm"/> in an unusable state. After
    /// calling <see cref="Dispose"/>, you must release all references to the
    /// <see cref="ION.Core.Alarms.AbstractAlarm"/> so the garbage collector can reclaim the memory that the
    /// <see cref="ION.Core.Alarms.AbstractAlarm"/> was occupying.</remarks>
    public virtual void Dispose() {
    }

    /// <summary>
    /// Fires the alarm's trigger function.
    /// </summary>
    /// <remarks>If force is true, then the trigger function will fire even if the alarm
    /// is pending reset.</remarks>
    /// <param name="force">If set to <c>true</c> force.</param>
    public bool Fire(bool force=false) {
      bool triggered = IsTriggered();

      ION.Core.Util.Log.D(this, "Force: " + force + " enabled: " + enabled + " triggered:" + triggered + " pendingReset:" + pendingReset);

      if ((force) || (enabled && triggered && !pendingReset)) {
        NotifyAlarmEvent(AlarmEvent.EType.Triggered);
        pendingReset = true;
        return true;
      } else {
        if (!triggered) {
          pendingReset = false;
        }
        return false;
      }
    }

    /// <summary>
    /// Resets the alarm such that it will not be triggered, but is still enabeled. The alarm will trigger again once
    /// it leaves its trigger range.
    /// </summary>
    public void Reset() {
      enabled = true;
      pendingReset = true;
      NotifyAlarmEvent(AlarmEvent.EType.Reset);
    }

    /// <summary>
    /// Cancels the alarm.
    /// </summary>
    public void Cancel() {
      enabled = false;
      pendingReset = false;
      NotifyAlarmEvent(AlarmEvent.EType.Cancelled);
    }

    /// <summary>
    /// Queries whether or not the alarm is triggered.
    /// </summary>
    /// <returns>true</returns>
    /// <c>false</c>
    public abstract bool IsTriggered();

    /// <summary>
    /// Called when the alarm wishes to thrown an alarm event.
    /// </summary>
    protected void NotifyAlarmEvent(AlarmEvent.EType type) {
      if (onAlarmEvent != null) {
        onAlarmEvent(new AlarmEvent(this, type));
      }
    }
  }
}

