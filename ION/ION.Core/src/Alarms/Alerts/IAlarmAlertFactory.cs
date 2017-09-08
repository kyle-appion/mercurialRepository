using System;

namespace ION.Core.Alarms.Alerts {
  public interface IAlarmAlertFactory {
    /// <summary>
    /// Creates a new alarm alert;
    /// </summary>
    IAlarmAlert New();
  }
}

