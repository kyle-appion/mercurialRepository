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

    /// <summary>
    /// The constant that marks an alarm as not having and id.
    /// </summary>
    private const uint NO_ID = 0;

    // Overridden from IAlarmManager
    public AlarmAlertFactoryDelegate alertFactory { get; set; }

    /// <summary>
    /// The counter that will increment for new alarm ids when alarms are added to the manager.
    /// </summary>
    private uint alarmIdCounter;

    /// <summary>
    /// The ion instance hosting the alarm manager.
    /// </summary>
    /// <value>The ion.</value>
    private IION ion { get; set; }
    /// <summary>
    /// A mapping of alarm ids to alarms.
    /// </summary>
    private Dictionary<uint, IAlarm> __idAlarmMapping = new Dictionary<uint, IAlarm>();
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

    /// <summary>
    /// Initializes the IIONManager to the given IION.
    /// </summary>
    /// <returns>The async.</returns>
    public Task<InitializationResult> InitAsync() {
      return Task.FromResult(new InitializationResult() { success = true });
    }

    /// <summary>
    /// Releases all resource used by the <see cref="ION.Core.Alarms.BaseAlarmManager"/> object.
    /// </summary>
    /// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="ION.Core.Alarms.BaseAlarmManager"/>. The
    /// <see cref="Dispose"/> method leaves the <see cref="ION.Core.Alarms.BaseAlarmManager"/> in an unusable state.
    /// After calling <see cref="Dispose"/>, you must release all references to the
    /// <see cref="ION.Core.Alarms.BaseAlarmManager"/> so the garbage collector can reclaim the memory that the
    /// <see cref="ION.Core.Alarms.BaseAlarmManager"/> was occupying.</remarks>
    public void Dispose() {
      foreach (var alert in __firingAlerts) {
        alert.Stop();
      }
    }

    /// <summary>
    /// Queries the alarm by using the given id. If the alarm does not exist, then null will be returned.
    /// </summary>
    /// <returns>The alarm.</returns>
    /// <param name="id">Identifier.</param>
    public IAlarm GetAlarm(uint id) {
      if (__idAlarmMapping.ContainsKey(id)) {
        return __idAlarmMapping[id];
      } else {
        return null;
      }
    }

    /// <summary>
    /// Queries the alarm by using the given id. If the alarm does not exist, then null will be returned.
    /// </summary>
    /// <returns>The alarm.</returns>
    /// <param name="id">Identifier.</param>
    /// <typeparam name="T">The 1st type parameter.</typeparam>
    public T GetAlarm<T>(uint id) where T : IAlarm {
      var alarm = GetAlarm(id);

      if (alarm != null && alarm is T) {
        return (T)alarm;
      } else {
        return default(T);
      }
    }

    /// <summary>
    /// Queries a list of alarms that are registered to the given host.
    /// </summary>
    /// <returns>The alarms from host.</returns>
    /// <param name="host">Host.</param>
    public List<IAlarm> GetAlarmsFromHost(object host) {
      List<IAlarm> ret = new List<IAlarm>();

      if (__alarmMapping.ContainsKey(host)) {
        ret.AddRange(__alarmMapping[host]);
      }

      return ret;
    }

    /// <summary>
    /// Queries an alarm of the given type from the host's alarm collection
    /// (if present).
    /// </summary>
    /// <returns>The alarm of type from host or null if the host or the alarm is not present.</returns>
    /// <param name="host">Host.</param>
    /// <typeparam name="T">The 1st type parameter.</typeparam>
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

    /// <summary>
    /// Queries whether or not the host has one or more active alarms.
    /// </summary>
    /// <returns>true</returns>
    /// <c>false</c>
    /// <param name="host">Host.</param>
    public bool HostHasEnabledAlarms(object host) {
      foreach (IAlarm alarm in GetAlarmsFromHost(host)) {
        if (alarm.enabled) {
          return true;
        }
      }

      return false;
    }

    /// <summary>
    /// Applies a custom alarm alert for the given alarm.
    /// </summary>
    /// <param name="alarm">Alarm.</param>
    /// <param name="alertMode">alarm alert. Null allowed.</param>
    public void SetAlertModeForAlarm(IAlarm alarm, IAlarmAlert alertMode) {
      __alertMapping[alarm] = alertMode;
    }

    /// <summary>
    /// Registers the given alarm to the given host.
    /// </summary>
    /// <param name="host">Host.</param>
    /// <param name="alarm">Alarm.</param>
    /// <returns>True if the alarm was registered to the host.</returns>
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

      alarm.id = ++alarmIdCounter;
      __idAlarmMapping.Add(alarm.id, alarm);
      alarms.Add(alarm);
      alarm.onAlarmTriggered += OnAlarmTriggered;


      return true;
    }

    /// <summary>
    /// Unregisters the alarm from the host.
    /// </summary>
    /// <param name="host">Host.</param>
    /// <param name="alarm">Alarm.</param>
    public void UnregisterAlarmFromHost(object host, IAlarm alarm) {
      if (__alarmMapping.ContainsKey(host)) {
        __alarmMapping[host].Remove(alarm);
        alarm.onAlarmTriggered -= OnAlarmTriggered;

        if (__alarmMapping[host].Count == 0) {
          __alarmMapping[host] = null;
        }
      }
    }

    /// <summary>
    /// Unregisters all of the host's alarms (if any).
    /// </summary>
    /// <param name="host">Host.</param>
    public void UnregisterAllAlarmsFromHost(object host) {
      if (__alarmMapping.ContainsKey(host)) {
        foreach (var alarm in __alarmMapping[host]) {
          UnregisterAlarmFromHost(host, alarm);
        }
      }
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

