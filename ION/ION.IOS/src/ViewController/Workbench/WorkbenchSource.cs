using System;
using System.Collections.Generic;

using Foundation;
using UIKit;

using ION.Core.Content;
using ION.Core.Devices;
using ION.Core.Sensors;
using ION.Core.Sensors.Properties;
using ION.Core.Util;

using ION.IOS.UI;
using ION.IOS.Util;
using ION.IOS.ViewController.Alarms;

namespace ION.IOS.ViewController.Workbench {
  /// <summary>
  /// The table source that will provide viewers to the table for display.
  /// </summary>
  internal class WorkbenchSource : UITableViewSource {

    /// <summary>
    /// The delegate that is used when the source wishes to request a new viewer.
    /// </summary>
    public delegate void OnRequestViewer();

    private const string CELL_VIEWER = "cellViewer";
    private const string CELL_ADD = "cellAdd";
    private const string CELL_MEASUREMENT_SUBVIEW = "cellMeasurementSubview";


    public OnRequestViewer onRequestViewerDelegate { get; set; }

    /// <summary>
    /// The workbench view controller that we are sourcing for.
    /// </summary>
    private WorkbenchViewController __workbenchController;
    /// <summary>
    /// The workbench controller's table view.
    /// </summary>
    private UITableView __table;
    /// <summary>
    /// The list of manifolds that we are displaying as viewers.
    /// </summary>
    private ION.Core.Content.Workbench __workbench;

    public WorkbenchSource(WorkbenchViewController workbenchViewController, ION.Core.Content.Workbench workbench) {
      __workbenchController = workbenchViewController;
      __workbench = workbench;
      __table = __workbenchController.tableContent;
    }

    // Overridden from UITableViewSource
    public override void CellDisplayingEnded(UITableView tableView, UITableViewCell cell, NSIndexPath indexPath) {
      var releasable = cell as IReleasable;
      if (releasable != null) {
        releasable.Release();
      }
    }

    // Overridden from UIViewController
    public override void RowSelected(UITableView tableView, NSIndexPath path) {

    }

    // Overridden from UITableViewSource
    public override nint NumberOfSections(UITableView tableView) {
      return __workbench.count + 1;
    }

    // Overridden from UITableViewSource
    public override nint RowsInSection(UITableView tableView, nint section) {
      if (section >= __workbench.count) {
        return 0;
      } else {
        return __workbench[(int)section].manifoldProperties.Count;
      }
    }

    // Overridden from UITableViewSource
    public override nfloat GetHeightForHeader(UITableView tableView, nint section) {
      var secCount = NumberOfSections(tableView); 
      if (section <= secCount - 2) {
        return 158;
      } else if (section == secCount - 1) {
        return 48;
      } else {
        return 0;
      }
    }

    public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath) {
      return 48;
    }

    // Overridden from UITableViewSource
    //    public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath) {
    //    }

    // Overridden from UITableViewSource
    public override UIView GetViewForHeader(UITableView tableView, nint section) {
      if (section == NumberOfSections(tableView) - 1) {
        var cell = tableView.DequeueReusableCell(CELL_ADD) as AddViewerTableCell;

        cell.UpdateTo(() => {
          if (onRequestViewerDelegate != null) {
            onRequestViewerDelegate();
          }
        });

        return cell;
      } else {
        var cell = tableView.DequeueReusableCell(CELL_VIEWER) as ViewerTableCell;
        var manifold = __workbench[(int)section];

        cell.UpdateTo(__workbenchController.ion, manifold, ShowManifoldContextMenu);

        return cell;
      }
    }

    // Overridden from UITableViewSource
    public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath) {
      var manifold = __workbench[(int)indexPath.Section];
      var prop = manifold.manifoldProperties[(int)indexPath.Row];

      if (prop is MinSensorProperty || prop is MaxSensorProperty || prop is HoldSensorProperty || prop is AlternateUnitSensorProperty) {
        var cell = tableView.DequeueReusableCell(CELL_MEASUREMENT_SUBVIEW) as MeasurementSensorPropertyTableCell;

        cell.UpdateTo(prop, GetLocalizedTitleString(prop), "ic_refresh", (object obj, ISensorProperty sensorProperty) => {
          sensorProperty.Reset();
        });

        return cell;
      } else {
        return null;
      }
    }

    private void ShowManifoldContextMenu(object obj, Manifold manifold) {
      var dialog = UIAlertController.Create(manifold.primarySensor.name, Strings.Workbench.SELECT_VIEWER_ACTION.FromResources(), UIAlertControllerStyle.ActionSheet);

      if (manifold.primarySensor is GaugeDeviceSensor) {
        var sensor = manifold.primarySensor as GaugeDeviceSensor;
        // Append gauge device sensor context items
        if (sensor.device.isConnected) {
          dialog.AddAction(UIAlertAction.Create(Strings.Device.DISCONNECT.FromResources(), UIAlertActionStyle.Default, (action) => {
            sensor.device.connection.Disconnect();
          }));
        } else {
          dialog.AddAction(UIAlertAction.Create(Strings.Device.RECONNECT.FromResources(), UIAlertActionStyle.Default, (action) => {
            sensor.device.connection.Connect();
          }));
        }
      }

      dialog.AddAction(UIAlertAction.Create(Strings.Workbench.Viewer.ADD, UIAlertActionStyle.Default, (action) => {
        ShowAddSubviewDialog(__table, manifold);
      }));

      dialog.AddAction(UIAlertAction.Create(Strings.Alarms.SELF, UIAlertActionStyle.Default, (action) => {
        var ac = this.__workbenchController.InflateViewController<SensorAlarmViewController>(BaseIONViewController.VC_SENSOR_ALARMS);
        ac.sensor = manifold.primarySensor;
        __workbenchController.NavigationController.PushViewController(ac, true);
      }));
/*

          dialog.AddAction(UIAlertAction.Create(Strings.RENAME, UIAlertActionStyle.Default, (action) => {
            Toast.New(__table, "Rename coming soon!");
          }));
*/

      dialog.AddAction(UIAlertAction.Create(Strings.Workbench.REMOVE, UIAlertActionStyle.Default, (action) => {
        __workbench.Remove(manifold);
        __table.ReloadData();
      }));

      dialog.AddAction(UIAlertAction.Create(Strings.CANCEL, UIAlertActionStyle.Cancel, null));

      // Requires for iPad- we must specify a source for the action sheet
      // since it is displayed as a popover
      var popover = dialog.PopoverPresentationController;
      if (popover != null) {
        popover.SourceView = __table;
        popover.PermittedArrowDirections = UIPopoverArrowDirection.Up;
      }
      __workbenchController.PresentViewController(dialog, true, null);
    }

    private delegate void AddAction(string title, Action<UIAlertAction> action);
    // Overridden from UITableViewSource
    private void ShowAddSubviewDialog(UITableView tableView, Manifold manifold) {
      var dialog = UIAlertController.Create(Strings.ACTIONS, Strings.Workbench.Viewer.ADD, UIAlertControllerStyle.ActionSheet);

      AddAction addAction = (string title, Action<UIAlertAction> action) => {
        dialog.AddAction(UIAlertAction.Create(title, UIAlertActionStyle.Default, (UIAlertAction uia) => {
          action(uia);
          tableView.ReloadData();
        }));
      };

      var sensor = manifold.primarySensor;

      if (!manifold.HasSensorPropertyOfType(typeof(MinSensorProperty))) {
        addAction(Strings.Workbench.Viewer.MIN_DESC, (UIAlertAction action) => {
          manifold.AddSensorProperty(new MinSensorProperty(sensor));
        });
      }
      if (!manifold.HasSensorPropertyOfType(typeof(MaxSensorProperty))) {
        addAction(Strings.Workbench.Viewer.MAX_DESC, (UIAlertAction action) => {
          manifold.AddSensorProperty(new MaxSensorProperty(sensor));
        });
      }
      if (!manifold.HasSensorPropertyOfType(typeof(HoldSensorProperty))) { 
        addAction(Strings.Workbench.Viewer.HOLD_DESC, (UIAlertAction action) => {
          manifold.AddSensorProperty(new HoldSensorProperty(sensor));
        });
      }
      if (!manifold.HasSensorPropertyOfType(typeof(AlternateUnitSensorProperty))) {
        addAction(Strings.Workbench.Viewer.ALT_DESC, (UIAlertAction action) => {
//          manifold.AddSensorProperty(new AlternateUnitSensorProperty(sensor, sensor.));
        });
      }

      dialog.AddAction(UIAlertAction.Create(Strings.CANCEL, UIAlertActionStyle.Cancel, null));

      var popover = dialog.PopoverPresentationController;
      if (popover != null) {
        popover.SourceView = tableView;
        popover.PermittedArrowDirections = UIPopoverArrowDirection.Up;
      }
      __workbenchController.PresentViewController(dialog, true, null);
    }

    private string GetLocalizedTitleString(ISensorProperty sensorProperty) {
      if (sensorProperty is MinSensorProperty) {
        return Strings.Workbench.Viewer.MIN;
      } else if (sensorProperty is MaxSensorProperty) {
        return Strings.Workbench.Viewer.MAX;
      } else if (sensorProperty is HoldSensorProperty) {
        return Strings.Workbench.Viewer.HOLD;
      } else if (sensorProperty is AlternateUnitSensorProperty) {
        return Strings.Workbench.Viewer.ALT;
      } else {
        throw new ArgumentException("Cannot identifiy sensor property: " + sensorProperty);
      }
    }
  }
}

