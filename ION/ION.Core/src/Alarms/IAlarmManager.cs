using System;
using System.Collections.Generic;

using ION.Core.Alarms.Alerts;
using ION.Core.App;

namespace ION.Core.Alarms {

  /// <summary>
  /// A delegate factory function who is used to create IAlarmAlerts.
  /// </summary>
  public delegate IAlarmAlert AlarmAlertFactoryDelegate(IAlarmManager alarmManager, IAlarm alarm);

  public interface IAlarmManager : IIONManager {
    /// <summary>
    /// The factory that will create alarm alerts for alarms that do not
    /// have a custom alert set.
    /// </summary>
    /// <value>The alarm alert.</value>
    AlarmAlertFactoryDelegate alertFactory { get; set; }

    /// <summary>
    /// Queries a list of alarms that are registered to the given host.
    /// </summary>
    /// <returns>The alarms from host.</returns>
    /// <param name="host">Host.</param>
    List<IAlarm> GetAlarmsFromHost(object host);
    /// <summary>
    /// Queries an alarm of the given type from the host's alarm collection
    /// (if present).
    /// </summary>
    /// <returns>The alarm of type from host or null if the host or the alarm is not present.</returns>
    /// <param name="host">Host.</param>
    /// <typeparam name="T">The 1st type parameter.</typeparam>
    T GetAlarmOfTypeFromHost<T>(object host) where T : IAlarm;
    /// <summary>
    /// Applies a custom alarm alert for the given alarm.
    /// </summary>
    /// <param name="alarm">Alarm.</param>
    /// <param name="alertMode">alarm alert. Null allowed.</param>
    void SetAlertModeForAlarm(IAlarm alarm, IAlarmAlert alertMode);

    /// <summary>
    /// Registers the given alarm to the given host.
    /// </summary>
    /// <param name="host">Host.</param>
    /// <param name="alarm">Alarm.</param>
    /// <returns>True if the alarm was registered to the host.</returns>
    bool RegisterAlarmToHost(object host, IAlarm alarm);

    /// <summary>
    /// Unregisters the alarm from the host.
    /// </summary>
    /// <param name="host">Host.</param>
    void UnregisterAlarmFromHost(object host, IAlarm alarm);

    /// <summary>
    /// Unregisters all of the host's alarms (if any).
    /// </summary>
    /// <param name="host">Host.</param>
    void UnregisterAllAlarmsFromHost(object host);
  }
}

