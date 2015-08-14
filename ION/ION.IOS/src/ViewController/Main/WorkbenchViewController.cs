using System;
using System.Collections.Generic;

using Foundation;
using UIKit;

using ION.Core.App;
using ION.Core.Content;
using ION.Core.Devices;
using ION.Core.Measure;
using ION.Core.Sensors;
using ION.Core.Util;

using ION.IOS.Devices;
using ION.IOS.Sensors;
using ION.IOS.UI;
using ION.IOS.Util;

namespace ION.IOS.ViewController.Main {
	public partial class WorkbenchViewController : UIViewController {
    /// <summary>
    /// The current ion context.
    /// </summary>
    /// <value>The ion.</value>
    private IION ion { get; set; }
    /// <summary>
    /// The source that will provide Viewer views to the table view.
    /// </summary>
    /// <value>The source.</value>
    private ViewerSource source { get; set; }

		public WorkbenchViewController (IntPtr handle) : base (handle) {
      // Nope
		}

    // Overridden from UIViewController
    public override void ViewDidLoad() {
      base.ViewDidLoad();

      Title = Strings.Workbench.SELF.FromResources();
//      this.NavigationItem.LeftBarButtonItem = new UIBarButtonItem(UIBarButtonSystemItem.Action, delegate {
//        Toast.New(tableContent, "Huh?!");
//      });

      ion = AppState.context;

      source = new ViewerSource(this, ion.currentWorkbench);

      tableContent.Source = source;

      ion.currentWorkbench.onManifoldAdded += OnManifoldAdded;
      ion.currentWorkbench.onManifoldRemoved += OnManifoldRemoved;
    }

    // Overridden from UIViewController
    public override void ViewDidUnload() {
      ion.currentWorkbench.onManifoldAdded -= OnManifoldAdded;
      ion.currentWorkbench.onManifoldRemoved -= OnManifoldRemoved;
    }

    /// <summary>
    /// Called when the workbench adds a new manifold.
    /// </summary>
    /// <param name="workbench">Workbench.</param>
    /// <param name="manifold">Manifold.</param>
    private void OnManifoldAdded(Workbench workbench, Manifold manifold) {
      tableContent.ReloadData();
    }

    /// <summary>
    /// Called when the workbench removes a manifold.
    /// </summary>
    /// <param name="workbench">Workbench.</param>
    /// <param name="manifold">Manifold.</param>
    private void OnManifoldRemoved(Workbench workbench, Manifold manifold) {
      tableContent.ReloadData();
    }
	}

  /// <summary>
  /// The table source that will provide viewers to the table for display.
  /// </summary>
  internal class ViewerSource : UITableViewSource {
    private const string CELL_VIEWER = "cellViewer";
    private const string CELL_ADD = "cellAdd";

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

    // Overridden from UIViewController
    public override void RowSelected(UITableView tableView, NSIndexPath path) {
      
    }

    // Overridden from UITableViewSource
    public override nint NumberOfSections(UITableView tableView) {
      return __workbench.count + 1;
    }

    // Overridden from UITableViewSource
    public override nint RowsInSection(UITableView tableView, nint section) {
      return 0;
    }

    // Overridden from UITableViewSource
    public override nfloat GetHeightForHeader(UITableView tableView, nint section) {
      var secCount = NumberOfSections(tableView); 
      if (section <= secCount - 2) {
        return 138;//__cellHeights[CELL_VIEWER];
      } else if (section == secCount - 1) {
        return 32;//__cellHeights[CELL_ADD];
      } else {
        return 0;
      }
    }

    // Overridden from UITableViewSource
//    public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath) {
//    }

    // Overridden from UITableViewSource
    public override UIView GetViewForHeader(UITableView tableView, nint section) {
      if (section == NumberOfSections(tableView) - 1) {
        var add = (WorkbenchAddCell)tableView.DequeueReusableCell(CELL_ADD);
        add.clicked = (() => {
          Toast.New(tableView, "Clicked 'Add New Viewer'");
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
      return null;
    }
  }
}
