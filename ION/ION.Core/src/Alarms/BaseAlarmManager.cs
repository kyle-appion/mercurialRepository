using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using ION.Core.Alarms.Alerts;
using ION.Core.App;
using ION.Core.Util;

namespace ION.Core.Alarms {
  /// <summary>
  /// A simple agnostic implementation of the alarm manager.
  /// </summary>
  // TODO ahodder@appioninc.com: A solid test needs to be done to see if object collision is common when registering
  public class BaseAlarmManager : IAlarmManager {
    private static IAlarmAlert EmptyFactory(IAlarmManager alarmManager, IAlarm alarm) {
      return new MockAlarmAlert(alarm);
    }


    // Overridden from IAlarmManager
    public AlarmAlertFactoryDelegate alertFactory { get; set; }

    /// <summary>
    /// The ion instance hosting the alarm manager.
    /// </summary>
    /// <value>The ion.</value>
    private IION ion { get; set; }
    /// <summary>
    /// The collection of alarms to their mapping objects (objects that
    /// they are linked to).
    /// </summary>
    private Dictionary<object, HashSet<IAlarm>> __alarmMapping = new Dictionary<object, HashSet<IAlarm>>();
    /// <summary>
    /// The collection of alarms to custom alerts.
    /// </summary>
    private Dictionary<IAlarm, IAlarmAlert> __alertMapping = new Dictionary<IAlarm, IAlarmAlert>();
    /// <summary>
    /// The list of alerts that are currently firing.
    /// </summary>
    private List<IAlarmAlert> __firingAlerts = new List<IAlarmAlert>();

    public BaseAlarmManager(IION ion) {
      this.ion = ion;
      this.alertFactory = EmptyFactory;
    }

    public BaseAlarmManager(IION ion, AlarmAlertFactoryDelegate alertFactory) {
      this.ion = ion;
      this.alertFactory = alertFactory;
    }

    // Overridden from IAlarmManager
    public async Task<InitializationResult> InitAsync() {
      return new InitializationResult() { success = true };
    }

    // Overridden from IAlarmManager
    public void Dispose() {
      foreach (var alert in __firingAlerts) {
        alert.Stop();
      }
    }

    // Overridden from IAlarmManager
    public List<IAlarm> GetAlarmsFromHost(object host) {
      List<IAlarm> ret = new List<IAlarm>();

      if (__alarmMapping.ContainsKey(host)) {
        ret.AddRange(__alarmMapping[host]);
      }

      return ret;
    }

    // Overridden from IAlarmManager
    public T GetAlarmOfTypeFromHost<T>(object host) where T : IAlarm {
      if (__alarmMapping.ContainsKey(host)) {
        foreach (var alarm in __alarmMapping[host]) {
          if (alarm is T) {
            return (T)alarm;
          }
        }
      }

      return default(T);
    }

    // Overridden from IAlarmManager
    public void SetAlertModeForAlarm(IAlarm alarm, IAlarmAlert alertMode) {
      __alertMapping[alarm] = alertMode;
    }

    // Overridden from IAlarmManager
    public bool RegisterAlarmToHost(object host, IAlarm alarm) {
      if (HostHasAlarmOfType(alarm.GetType(), host)) {
        Log.D(this, "Object " + host.GetHashCode() + " already has an alarm of type " + alarm.GetType().Name);
        return false;
      }
      HashSet<IAlarm> alarms = null;

      if (__alarmMapping.ContainsKey(host)) {
        alarms = __alarmMapping[host];
      } else {
        __alarmMapping[host] = alarms = new HashSet<IAlarm>();
      }

      alarms.Add(alarm);
      alarm.onAlarmTriggered += OnAlarmTriggered;

      return true;
    }

    // Overridden from IAlarmManager
    public void UnregisterAlarmFromHost(object host, IAlarm alarm) {
      if (__alarmMapping.ContainsKey(host)) {
        __alarmMapping[host].Remove(alarm);
      }
    }

    // Overridden from IAlarmManager
    public void UnregisterAllAlarmsFromHost(object host) {
      if (__alarmMapping.ContainsKey(host)) {
        foreach (var alarm in __alarmMapping[host]) {
          alarm.onAlarmTriggered -= OnAlarmTriggered;
        }
      }
      __alarmMapping[host] = new HashSet<IAlarm>();
    }

    /// <summary>
    /// Queries whether or not the host has an alarm of the given type.
    /// </summary>
    /// <returns><c>true</c>, if has alarm of type was hosted, <c>false</c> otherwise.</returns>
    private bool HostHasAlarmOfType(Type type, object host) {
      if (__alarmMapping.ContainsKey(host)) {
        foreach (var alarm in __alarmMapping[host]) {
          if (alarm.GetType() == type) {
            return true;
          }
        }
      }

      return false;
    }

    /// <summary>
    /// The function that is called when a registered alarm is triggered.
    /// </summary>
    /// <param name="alarm">Alarm.</param>
    private void OnAlarmTriggered(IAlarm alarm) {
      // Contension should never happen.
      lock (this) {
        IAlarmAlert alert = null;

        if (__alertMapping.ContainsKey(alarm)) {
          alert = __alertMapping[alarm];
        } else {
          alert = alertFactory(this, alarm);
        }

        // Prevent a double start of an alert. This should only occur if there is a bug in the
        // alarm implementation.
        if (!alert.isStarted) {
          __firingAlerts.Add(alert);

          alert.onAlarmAlertStopped += OnAlarmAlertStopped;
          alert.Start();
        } else {
          throw new Exception("Alarm was triggered, but alert cannot be started: the alert is already started.");
        }
      }
    }

    /// <summary>
    /// The function that is called when an alarm alert has been finished.
    /// </summary>
    /// <param name="alert">Alert.</param>
    private void OnAlarmAlertStopped(IAlarmAlert alert) {
      // Contension should never happen.
      lock (this) {
        __firingAlerts.Remove(alert);
        alert.onAlarmAlertStopped -= OnAlarmAlertStopped;
      }
    }
  }

  /// <summary>
  /// An AlarmAlarm that does nothing.
  /// </summary>
  internal class MockAlarmAlert : IAlarmAlert {
    // Overridden from IAlarmAlert
    public event OnAlarmAlertStopped onAlarmAlertStopped;

    // Overridden from IAlarmAlert
    public IAlarm alarm { get; private set; }
    // Overridden from IAlarmAlert
    public bool isStarted { get; private set; }
    // Overridden from IAlarmAlert
    public bool isFinished { get; private set; }

    public MockAlarmAlert(IAlarm alarm) {
      this.alarm = alarm;
    }

    // Overridden from IAlarmAlert
    public bool Start() {
      if (!isStarted) {
        isStarted = true;
        return true;
      } else {
        return false;
      }
    }

    // Overridden from IAlarmAlert
    public void Stop() {
      isFinished = true;
    }

    public void Reset() {
      isStarted = isFinished = false;
    }
  }
}

