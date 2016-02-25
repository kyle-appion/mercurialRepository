using System;

namespace ION.Core.Alarms {
  public abstract class AbstractAlarm : IAlarm {
    /// <summary>
    /// The event pool that allows for notifications of the alarm trigger.
    /// </summary>
    public event OnAlarmTriggered onAlarmTriggered;
    /// <summary>
    /// The event pool that allows for notifications of when the alarm's data
    /// changes.
    /// </summary>
    public event OnAlarmChanged onAlarmChanged;

    public uint id { get; set; }
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
        NotifyAlarmChanged();
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
        NotifyAlarmChanged();
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

      if ((force) || (enabled && triggered && !pendingReset)) {
        Trigger();
        pendingReset = true;
        return true;
      } else {
        if (!triggered) {
          Reset();
        }
        return false;
      }
    }

    /// <summary>
    /// Resets the alarm. PendingReset will be set to false and any other alarm
    /// cleanup should happen here. This method should be called after a failed
    /// fire attempt (an attempt that didn't meet any of its fire criteria.
    /// </summary>
    public void Reset() {
      OnReset();
      pendingReset = false;
    }

    /// <summary>
    /// Queries whether or not the alarm is triggered.
    /// </summary>
    /// <returns>true</returns>
    /// <c>false</c>
    public abstract bool IsTriggered();

    /// <summary>
    /// A call that will allow the child to react to a Reset call.
    /// </summary>
    protected virtual void OnReset() {
      // Nope
    }

    /// <summary>
    /// Safely called OnAlarmChanged.
    /// </summary>
    protected void NotifyAlarmChanged() {
      if (onAlarmChanged != null) {
        onAlarmChanged(this);
      }
    }

    /// <summary>
    /// The trigger function that will be called when the alarm is triggered.
    /// </summary>
    private void Trigger() {
      if (onAlarmTriggered != null) {
        onAlarmTriggered(this);
      }
    }
  }
}

