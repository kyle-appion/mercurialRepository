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

namespace ION.IOS.ViewController.Main {
  /// <summary>
  /// The table source that will provide viewers to the table for display.
  /// </summary>
  internal class ViewerSource : UITableViewSource {

    /// <summary>
    /// The delegate that is used when the source wishes to request a new viewer.
    /// </summary>
    public delegate void OnRequestViewer();

    private const string CELL_VIEWER = "cellViewer";
    private const string CELL_ADD = "cellAdd";
    private const string CELL_DEFAULT = "cellDefaultSubview";


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
    private Workbench __workbench;
    /// <summary>
    /// A lookup table for heghts of all cells.
    /// </summary>
    private Dictionary<string, nfloat> __cellHeights = new Dictionary<string, nfloat>();

    public ViewerSource(WorkbenchViewController workbenchViewController, Workbench workbench) {
      __workbenchController = workbenchViewController;
      __workbench = workbench;
      __table = __workbenchController.tableContent;

      __cellHeights[CELL_VIEWER] = __table.DequeueReusableCell(CELL_VIEWER).Frame.Size.Height;
      __cellHeights[CELL_ADD] = __table.DequeueReusableCell(CELL_ADD).Frame.Size.Height;
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
        return 138;//__cellHeights[CELL_VIEWER];
      } else if (section == secCount - 1) {
        return 48;//__cellHeights[CELL_ADD];
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
        var add = (WorkbenchAddCell)tableView.DequeueReusableCell(CELL_ADD);
        add.clicked = (() => {
          if (onRequestViewerDelegate != null) {
            onRequestViewerDelegate();
          }
        });
        return add;
      } else {
        var viewer = (Viewer)tableView.DequeueReusableCell(CELL_VIEWER);
        var manifold = __workbench[(int)section];
        viewer.manifold = manifold;
        viewer.onViewerClicked = () => {
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

          dialog.AddAction(UIAlertAction.Create("BAD STRING Add Subview", UIAlertActionStyle.Default, (action) => {
            ShowAddSubviewDialog(tableView, manifold);
          }));

          dialog.AddAction(UIAlertAction.Create("BAD STRING Alarms", UIAlertActionStyle.Default, (action) => {
            Toast.New(__table, "Alarms coming soon!");
          }));

          dialog.AddAction(UIAlertAction.Create("BAD STRING Add Subview", UIAlertActionStyle.Default, (action) => {
            Toast.New(__table, "Subviews coming soon!");
          }));

          dialog.AddAction(UIAlertAction.Create(Strings.RENAME.FromResources(), UIAlertActionStyle.Default, (action) => {
            Toast.New(__table, "Rename coming soon!");
          }));

          dialog.AddAction(UIAlertAction.Create(Strings.Workbench.REMOVE.FromResources(), UIAlertActionStyle.Default, (action) => {
            __workbench.Remove(manifold);
            __table.ReloadData();
          }));

          dialog.AddAction(UIAlertAction.Create(Strings.CANCEL.FromResources(), UIAlertActionStyle.Cancel, null));

          // Requires for iPad- we must specify a source for the action sheet
          // since it is displayed as a popover
          var popover = dialog.PopoverPresentationController;
          if (popover != null) {
            popover.SourceView = tableView;
            popover.PermittedArrowDirections = UIPopoverArrowDirection.Up;
          }
          __workbenchController.PresentViewController(dialog, true, null);
        };
        return viewer;
      }
    }

    // Overridden from UITableViewSource
    public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath) {
      var manifold = __workbench[(int)indexPath.Section];
      var prop = manifold.manifoldProperties[(int)indexPath.Row];

      if (prop is MinSensorProperty || prop is MaxSensorProperty || prop is HoldSensorProperty || prop is AlternateUnitSensorProperty) {
        var cell = tableView.DequeueReusableCell(CELL_DEFAULT) as DefaultSubviewCell;

        cell.labelTitle.Text = GetLocalizedTitleString(prop);
        cell.labelMeasurement.Text = prop.modifiedMeasurement.ToString();
        cell.buttonIcon.SetImage(UIImage.FromBundle("ic_refresh"), UIControlState.Normal);
        cell.onIconClicked = () => {
          prop.Reset();
        };
        cell.sensorProperty = prop;

        return cell;
      } else {
        return null;
      }
    }

    private delegate void AddAction(string title, Action<UIAlertAction> action);
    // Overridden from UITableViewSource
    private void ShowAddSubviewDialog(UITableView tableView, Manifold manifold) {
      var dialog = UIAlertController.Create("BAD STRING Actions", "BAD STRING Add Subviewer", UIAlertControllerStyle.ActionSheet);

      AddAction addAction = (string title, Action<UIAlertAction> action) => {
        dialog.AddAction(UIAlertAction.Create(title, UIAlertActionStyle.Default, (UIAlertAction uia) => {
          action(uia);
          tableView.ReloadData();
        }));
      };

      var sensor = manifold.primarySensor;

      if (!manifold.HasSensorPropertyOfType(typeof(MinSensorProperty))) {
        addAction("BAD STRING Min", (UIAlertAction action) => {
          manifold.AddSensorProperty(new MinSensorProperty(sensor));
        });
      }
      if (!manifold.HasSensorPropertyOfType(typeof(MaxSensorProperty))) {
        addAction("BAD STRING Max", (UIAlertAction action) => {
          manifold.AddSensorProperty(new MaxSensorProperty(sensor));
        });
      }
      if (!manifold.HasSensorPropertyOfType(typeof(HoldSensorProperty))) { 
        addAction("BAD STRING Hold", (UIAlertAction action) => {
          manifold.AddSensorProperty(new HoldSensorProperty(sensor));
        });
      }
      if (!manifold.HasSensorPropertyOfType(typeof(AlternateUnitSensorProperty))) {
        addAction("BAD STRING Alternate Unit", (UIAlertAction action) => {
//          manifold.AddSensorProperty(new AlternateUnitSensorProperty(sensor, sensor.));
        });
      }

      var popover = dialog.PopoverPresentationController;
      if (popover != null) {
        popover.SourceView = tableView;
        popover.PermittedArrowDirections = UIPopoverArrowDirection.Up;
      }
      __workbenchController.PresentViewController(dialog, true, null);
    }

    private string GetLocalizedTitleString(ISensorProperty sensorProperty) {
      if (sensorProperty is MinSensorProperty) {
        return "BS MIN";
      } else if (sensorProperty is MaxSensorProperty) {
        return "BS MAX";
      } else if (sensorProperty is HoldSensorProperty) {
        return "BS HOLD";
      } else if (sensorProperty is AlternateUnitSensorProperty) {
        return "BS ALT";
      } else {
        throw new ArgumentException("Cannot identifiy sensor property: " + sensorProperty);
      }
    }
  }
}

