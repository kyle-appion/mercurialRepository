namespace ION.IOS.ViewController.Workbench {

  using System;
  using System.Collections.Generic;
  using System.Collections.ObjectModel;
  using System.Linq;

  using Foundation;
  using UIKit;

  using ION.Core.App;
  using ION.Core.Content;
  using ION.Core.Devices;
  using ION.Core.Util;

  using ION.IOS.Util;
  using ION.IOS.ViewController.Alarms;

  /// <summary>
  /// The class that will provide the cells for the workbench view controller.
  /// </summary>
  public class WorkbenchTableSource : UITableViewSource, IDisposable {


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
    /// The view controller that is hosting this source.
    /// </summary>
    /// <value>The vc.</value>
    private WorkbenchViewController vc { get; set; }
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
    /// The table view that this adapter is working with.
    /// </summary>
    /// <value>The table view.</value>
    private UITableView tableView { get; set; }
    /// <summary>
    /// The collection that of records that the source is displaying.
    /// </summary>
    private ObservableCollection<WorkbenchSourceRecord> records = new ObservableCollection<WorkbenchSourceRecord>();

    public WorkbenchTableSource(WorkbenchViewController vc, IION ion, Workbench workbench, UITableView tableView) {
      this.vc = vc;
      this.ion = ion;
      this.workbench = workbench;
      this.workbench.onWorkbenchEvent += OnManifoldChanged;
      this.tableView = tableView;

      foreach (var manifold in workbench.manifolds) {
        records.Add(new ViewerRecord() {
          manifold = manifold,
          expanded = true,
        });

        foreach (var sp in manifold.manifoldProperties) {
        }
      }

      records.Add(new AddRecord());
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
          if (record is ViewerRecord) {
            var vr = record as ViewerRecord;

            workbench.Remove(vr.manifold);
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

        return cell;
      } else if (record is ViewerRecord) {
        var viewer = record as ViewerRecord;
        var cell = tableView.DequeueReusableCell(CELL_VIEWER) as ViewerTableCell;
        cell.UpdateTo(ion, viewer.manifold, ShowManifoldContext);
        return cell;
      } else {
        throw new Exception("Cannot get cell: " + record.viewType + " is not a supported record type.");
      }
    }

    // Overridden from IDisposable
    public void Dispose() {
      workbench.onWorkbenchEvent -= OnManifoldChanged;
    }

    /// <summary>
    /// Queries the index of the given manifold. Returns -1 if the manifold is not in the adapter as a record.
    /// </summary>
    /// <returns>The of manifold.</returns>
    /// <param name="manifold">Manifold.</param>
    public int IndexOfManifold(Manifold manifold) {
      for (var i = 0; i < records.Count; i++) {
        var record = records[i] as ViewerRecord;
        if (record != null && record.manifold == manifold) {
          return i;
        }
      }

      return -1;
    }

    /// <summary>
    /// Shows the context menu for the manifold.
    /// </summary>
    /// <param name="obj">Object.</param>
    /// <param name="manifold">Manifold.</param>
    private void ShowManifoldContext(object obj, Manifold manifold) {
      var window = UIApplication.SharedApplication.KeyWindow;
      var vc = window.RootViewController;
      while (vc.PresentedViewController != null) {
        vc = vc.PresentedViewController;
      }
      var dialog = UIAlertController.Create(manifold.primarySensor.name, Strings.Workbench.SELECT_VIEWER_ACTION.FromResources(), UIAlertControllerStyle.Alert);


      if (manifold.primarySensor is GaugeDeviceSensor) {
        var sensor = manifold.primarySensor as GaugeDeviceSensor;
        // Append gauge device sensor context items
        if (!sensor.device.isConnected) {
          dialog.AddAction(UIAlertAction.Create(Strings.Device.RECONNECT.FromResources(), UIAlertActionStyle.Default, (action) => {
            sensor.device.connection.Connect();
          }));
        }
      }

      dialog.AddAction(UIAlertAction.Create(Strings.RENAME, UIAlertActionStyle.Default, (action) => {
        UIAlertController textAlert = UIAlertController.Create(Strings.ENTER_NAME, manifold.primarySensor.name, UIAlertControllerStyle.Alert);
        textAlert.AddTextField(textField => {});
        textAlert.AddAction (UIAlertAction.Create (Strings.CANCEL, UIAlertActionStyle.Cancel, UIAlertAction => {}));
        textAlert.AddAction (UIAlertAction.Create (Strings.OK_SAVE, UIAlertActionStyle.Default, UIAlertAction => {
          manifold.primarySensor.name = textAlert.TextFields[0].Text;
        }));

        vc.PresentViewController(textAlert, true, null);

      }));

      dialog.AddAction(UIAlertAction.Create(Strings.Workbench.Viewer.ADD, UIAlertActionStyle.Default, (action) => {
        ShowAddSubviewDialog(tableView, manifold);
      }));

      dialog.AddAction(UIAlertAction.Create(Strings.Alarms.SELF, UIAlertActionStyle.Default, (action) => {
        var ac = this.vc.InflateViewController<SensorAlarmViewController>(BaseIONViewController.VC_SENSOR_ALARMS);
        ac.sensor = manifold.primarySensor;
        this.vc.NavigationController.PushViewController(ac, true);
      }));

      if (manifold.primarySensor is GaugeDeviceSensor) {
        var sensor = manifold.primarySensor as GaugeDeviceSensor;
        // Append gauge device sensor context items
        if (sensor.device.isConnected) {
          dialog.AddAction(UIAlertAction.Create(Strings.Device.DISCONNECT.FromResources(), UIAlertActionStyle.Default, (action) => {
            sensor.device.connection.Disconnect();
          }));
        }
      }

      dialog.AddAction(UIAlertAction.Create(Strings.Workbench.REMOVE, UIAlertActionStyle.Default, (action) => {
        workbench.Remove(manifold);
      }));

      dialog.AddAction(UIAlertAction.Create(Strings.CANCEL, UIAlertActionStyle.Cancel, null));

      // Requires for iPad- we must specify a source for the action sheet
      // since it is displayed as a popover
      var popover = dialog.PopoverPresentationController;
      if (popover != null) {
        popover.SourceView = tableView;
        popover.PermittedArrowDirections = UIPopoverArrowDirection.Up;
      }
      vc.PresentViewController(dialog, true, null);
    }

    /// <summary>
    /// Shows an action sheet that will allow subviews to be added to the table source.
    /// </summary>
    /// <param name="tableView">Table view.</param>
    /// <param name="manifold">Manifold.</param>
    private void ShowAddSubviewDialog(UITableView tableView, Manifold manifold) {
      Log.D(this, "Showy, showy");
    }

    /// <summary>
    /// Called when the workbench changes.
    /// </summary>
    /// <param name="workbenchEvent">Workbench event.</param>
    private void OnManifoldChanged(WorkbenchEvent workbenchEvent) {
      Log.D(this, "event: " + workbenchEvent.type);
      var manifold = workbenchEvent.manifold;
      var index = IndexOfManifold(manifold);
      var indices = new List<int>();
      ViewerRecord vr;

      switch (workbenchEvent.type) {
        case WorkbenchEvent.EType.Added:
          index = records.Count - 1;
          indices.Add(index);

          vr = new ViewerRecord() {
            manifold = manifold,
            expanded = true,
          };

          records.Insert(index, vr);

/*
          for (int i = 0; i < manifold.sensorPropertyCount; i++) {
            indices.Add(index + i + 1);
          }
*/

          tableView.InsertRows(ToNSIndexPath(indices.ToArray()), UITableViewRowAnimation.Top);

          break;    
        case WorkbenchEvent.EType.Invalidated:
          if (index > 0) {
            vr = records[index] as ViewerRecord;
            if (vr.expanded) {
              for (int i = manifold.sensorPropertyCount; i > 0; i--) {
                indices.Add(i + index);
              }
            }

            tableView.ReloadRows(ToNSIndexPath(indices.ToArray()), UITableViewRowAnimation.None);
          }
          break;
        case WorkbenchEvent.EType.Removed:
          var start = index;
          var end = start;

          vr = records[start] as ViewerRecord;

          if (vr.expanded) {
            end += vr.manifold.sensorPropertyCount;
          }

          for (int i = end; i >= start; i--) {
            records.RemoveAt(i);
          }

          tableView.DeleteRows(ToNSIndexPath(Arrays.Range(start, end)), UITableViewRowAnimation.Top);
          break;
      }
    }

    /// <summary>
    /// Converts the given ints to a NSIndexPath array.
    /// </summary>
    /// <returns>The NS index path.</returns>
    /// <param name="ints">Ints.</param>
    private NSIndexPath[] ToNSIndexPath(int[] ints) {
      var ret = new NSIndexPath[ints.Length];

      for (int i = 0; i < ret.Length; i++) {
        ret[i] = NSIndexPath.FromRowSection((nint)i, (nint)0);
      }

      return ret;
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
      public bool expanded { get; set; }
    }
  }
}

