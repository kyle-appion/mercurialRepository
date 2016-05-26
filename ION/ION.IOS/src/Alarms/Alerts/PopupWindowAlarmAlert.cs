using System;

using Foundation;
using UIKit;

using ION.Core.Alarms;
using ION.Core.Alarms.Alerts;

using ION.IOS.UI;
using ION.IOS.Util;

namespace ION.IOS.Alarms.Alerts {
  public class PopupWindowAlarmAlert : AbstractAlarmAlert {
    /// <summary>
    /// The alert view that will display the alarm information.
    /// </summary>
    /// <value>The alert view.</value>
    private UIAlertController alertView { get; set; }

    public PopupWindowAlarmAlert(IAlarm alarm) : base(alarm) {
    }

    /// <summary>
    /// Called by the alert when it is started.
    /// </summary>
    protected override bool OnStart() {
      alertView = UIAlertController.Create(alarm.name, alarm.description, UIAlertControllerStyle.Alert);

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

    /// <summary>
    /// Called by the alert when it is stopped.
    /// </summary>
    protected override void OnStop() {
      if (alertView != null) {
        alertView.Dismiss();
        alertView = null;
      }
    }
  }
}

