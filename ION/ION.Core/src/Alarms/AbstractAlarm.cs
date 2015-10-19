using System;

namespace ION.Core.Alarms {
  public abstract class AbstractAlarm : IAlarm {
    // Overridden from IAlarm
    public event OnAlarmTriggered onAlarmTriggered;
    // Overridden from IAlarm
    public event OnAlarmChanged onAlarmChanged;

    // Overridden from IAlarm
    public string name { 
      get {
        return __name;
      }
      set {
        __name = value;
        NotifyAlarmChanged();
      }
    } string __name;
    // Overridden from IAlarm
    public string description {
      get {
        return __description;
      }
      set {
        __description = value;
        NotifyAlarmChanged();
      }
    } string __description;
    // Overridden from IAlarm
    public bool enabled { get; set; }
    // Overridden from IAlarm
    public bool pendingReset { get; private set; }

    public AbstractAlarm(string name, string description) {
      this.name = name;
      this.description = description;
    }

    // Overridden from IAlarm
    public virtual void Dispose() {
    }

    // Overridden from IAlarm
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

    // Overridden from IAlarm
    public void Reset() {
      OnReset();
      pendingReset = false;
    }

    // Overridden from IAlarm
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

