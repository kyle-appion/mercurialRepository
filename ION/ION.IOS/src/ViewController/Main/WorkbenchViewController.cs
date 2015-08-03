using System;
using System.Collections.Generic;

using Foundation;
using UIKit;

using ION.Core.App;
using ION.Core.Content;
using ION.Core.Measure;
using ION.Core.Sensors;
using ION.Core.Util;

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

      ion = AppState.context;

      source = new ViewerSource(tableContent, ion.currentWorkbench);

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
    /// The table view that this source is providing to.
    /// </summary>
    private UITableView __tableView;
    /// <summary>
    /// The list of manifolds that we are displaying as viewers.
    /// </summary>
    private Workbench __workbench;
    /// <summary>
    /// A lookup table for heghts of all cells.
    /// </summary>
    private Dictionary<string, nfloat> __cellHeights = new Dictionary<string, nfloat>();

    public ViewerSource(UITableView tableView, Workbench workbench) {
      __tableView = tableView;
      __workbench = workbench;

      __cellHeights[CELL_VIEWER] = tableView.DequeueReusableCell(CELL_VIEWER).Frame.Size.Height;
      __cellHeights[CELL_ADD] = tableView.DequeueReusableCell(CELL_ADD).Frame.Size.Height;

      var sensor = new Sensor(ESensorType.Pressure, true, Units.Pressure.PSIA.OfScalar(100));
      __workbench.Add(new Manifold(sensor));
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
        Log.D(this, __cellHeights[CELL_VIEWER] + "");
        return 142;//__cellHeights[CELL_VIEWER];
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
        return add;
      } else {
        var viewer = (WorkbenchViewer)tableView.DequeueReusableCell(CELL_VIEWER);
        var manifold = __workbench[(int)section];

        viewer.labelHeader.Text = "Type" + ": " + "Sensor name";

        var measurement = manifold.primarySensor.measurement;

        viewer.labelMeasurement.Text = measurement.amount + "";

        viewer.labelUnit.Text = measurement.unit.ToString();
        viewer.labelSerialNumber.Text = "Sensor serial number goes here";

        return viewer;
      }
    }

    // Overridden from UITableViewSource
    public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath) {
      return null;
    }
  }
}
