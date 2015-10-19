using System;

using Foundation;
using UIKit;

using ION.Core.Alarms;
using ION.Core.Alarms.Alerts;

using ION.IOS.UI;
using ION.IOS.Util;

namespace ION.IOS.Alarms.Alerts {
  public class PopupWindowAlarmAlert : IAlarmAlert {
    // Overridden from IAlarmAlert
    public event OnAlarmAlertStopped onAlarmAlertStopped;

    // Overridden from IAlarmAlert
    public IAlarm alarm { get; private set; }
    // Overridden from IAlarmAlert
    public bool isStarted { get; private set; }
    // Overridden from IAlarmAlert
    public bool isFinished { get; private set; }

    /// <summary>
    /// The alert view that will display the alarm information.
    /// </summary>
    /// <value>The alert view.</value>
    private UIAlertController alertView { get; set; }

    public PopupWindowAlarmAlert(IAlarm alarm) {
      this.alarm = alarm;
    }

    // Overridden from IAlarmAlert
    public bool Start() {
      if (isStarted) {
        return false;
      }

      isStarted = true;

      alertView = UIAlertController.Create("Alarm Title", "Alarm Message", UIAlertControllerStyle.Alert);

      alertView.AddAction(UIAlertAction.Create(Strings.CANCEL, UIAlertActionStyle.Cancel, (action) => {
        alarm.enabled = false;
        Stop();
      }));
      alertView.AddAction(UIAlertAction.Create(Strings.Alarms.REENABLE, UIAlertActionStyle.Default, (action) => {
        alarm.enabled = true;
        Stop();
      })); 

      alertView.Show();

      return true;
    }

    // Overridden from IAlarmAlert
    public void Stop() {
      alertView.Dismiss();
      alertView = null;
      if (onAlarmAlertStopped != null) {
        onAlarmAlertStopped(this);
      }
    }

    // Overridden from IAlarmAlert
    public void Reset() {
      isStarted = false;
      isFinished = false;
    }
  }
}

