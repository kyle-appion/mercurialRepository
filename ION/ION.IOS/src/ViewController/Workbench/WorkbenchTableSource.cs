namespace ION.IOS.ViewController.Workbench {

  using System;
  using System.Collections.ObjectModel;

  using Foundation;
  using UIKit;

  using ION.Core.App;
  using ION.Core.Content;
  using ION.Core.Util;

  using ION.IOS.Util;

  /// <summary>
  /// The class that will provide the cells for the workbench view controller.
  /// </summary>
  public class WorkbenchTableSource : UITableViewSource {


    private const string CELL_VIEWER = "cellViewer";
    private const string CELL_ADD = "cellAdd";
    private const string CELL_MEASUREMENT_SUBVIEW = "cellMeasurementSubview";
    private const string CELL_FLUID_SUBVIEW = "cellFluidSubview";
    private const string CELL_ROC_SUBVIEW = "cellRateOfChangeSubview";
    private const string CELL_TIMER_SUBVIEW = "cellTimerSubview";

    /// <summary>
    /// The action that is called when the add row is clicked.
    /// </summary>
    /// <value>The on add clicked.</value>
    public Action onAddClicked { get; set; }

    /// <summary>
    /// The current ion instance.
    /// </summary>
    /// <value>The ion.</value>
    private IION ion { get; set; }
    /// <summary>
    /// The workbench that will provide the backend for the cells in the source.
    /// </summary>
    /// <value>The workbench.</value>
    private Workbench workbench { get; set; }
    /// <summary>
    /// The collection that of records that the source is displaying.
    /// </summary>
    private ObservableCollection<WorkbenchSourceRecord> records = new ObservableCollection<WorkbenchSourceRecord>();

    public WorkbenchTableSource(IION ion, Workbench workbench) {
      this.ion = ion;
      this.workbench = workbench;
    }

    // Overridden from UITableViewSource
    public override nint NumberOfSections(UITableView tableView) {
      return 1;
    }

    // Overridden from UITableViewSource
    public override nint RowsInSection(UITableView tableview, nint section) {
      return records.Count;
    }

    // Overridden from UITableViewSource
    public override void CellDisplayingEnded(UITableView tableView, UITableViewCell cell, NSIndexPath path) {
      var releasable = cell as IReleasable;
      if (releasable != null) {
        releasable.Release();
      }
    }

    // Overridden from UITableViewSource
    public override void CommitEditingStyle(UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath path) {
      var record = records[path.Row];

      switch (editingStyle) {
        case UITableViewCellEditingStyle.Delete:
          var pos = path.Row;
          while (!(records[pos] is ViewerRecord)) {
            pos--;
          }

          break;
      }
    }

    // Overridden from UITableViewSource
    public override bool CanEditRow(UITableView tableView, NSIndexPath path) {
      var record = records[path.Row];
      return record is ViewerRecord || !((record is AddRecord));
    }

    // Overridden from UITableViewSource
    public override string TitleForDeleteConfirmation(UITableView tableView, NSIndexPath indexPath) {
      return Strings.DELETE_QUESTION;
    }

    // Overridden from UITableViewSource
    public override void RowSelected(UITableView tableView, NSIndexPath indexPath) {
    }

    // Overridden from UITableViewSource
    public override nfloat GetHeightForHeader(UITableView tableView, nint section) {
      return 0;
    }

    // Overridden from UITableViewSource
    public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath) {
      var record = records[indexPath.Row];

      if (record is ViewerRecord) {
        return 158;
      } else {
        return 50;
      }
    }

    // Overridden from UITableViewSource
    public override UIView GetViewForHeader(UITableView tableView, nint section) {
      return null;
    }

    // Overridden from UITableViewSource
    public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath) {
      var record = records[indexPath.Row];

      if (record is AddRecord) {
        var cell = tableView.DequeueReusableCell(CELL_ADD) as AddViewerTableCell;

        cell.UpdateTo(() => {
          if (onAddClicked != null) {
            onAddClicked();
          }
        });

        return;
      } else if (record is ViewerRecord) {
        var viewer = record as ViewerRecord;
        var cell = tableView.DequeueReusableCell(CELL_VIEWER) as ViewerTableCell;
        cell.UpdateTo(ion, viewer.manifold, ShowManifoldContext);
      } else {
        throw new Exception("Cannot get cell: " + record.viewType + " is not a supported record type.");
      }
    }

    private void ShowManifoldContext(object obj, Manifold manifold) {
      Log.D(this, "Clicky, clicky manifold context");
    }

    public enum ViewType {
      Add,
      Viewer,
      Min,
      Max,
      Hold,
      Alt,
      Roc,
      PtChart,
      SuperSub,
    }

    public interface WorkbenchSourceRecord {
      ViewType viewType { get; } 
    }

    public class AddRecord : WorkbenchSourceRecord {
      // Overridden from WorkbenchSourceRecord
      public ViewType viewType {
        get {
          return ViewType.Add;
        }
      }
    }

    public class ViewerRecord : WorkbenchSourceRecord {
      // Overridden from WorkbenchSourceRecord
      public ViewType viewType {
        get {
          return ViewType.Viewer;
        }
      }

      public Manifold manifold { get; set; }
    }
  }
}

