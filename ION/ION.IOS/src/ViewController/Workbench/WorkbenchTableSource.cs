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
	using ION.Core.Devices.Protocols;
  using ION.Core.Fluids;
  using ION.Core.Sensors;
  using ION.Core.Sensors.Filters;
  using ION.Core.Sensors.Properties;
  using ION.Core.Util;

  using ION.IOS.Util;
  using ION.IOS.ViewController.Alarms;
  using ION.IOS.ViewController.PressureTemperatureChart;
  using ION.IOS.ViewController.SuperheatSubcool;

  /// <summary>
  /// The class that will provide the cells for the workbench view controller.
  /// </summary>
  public class WorkbenchTableSource : UITableViewSource, IReleasable {


    private const string CELL_VIEWER = "cellViewer";
    private const string CELL_ADD = "cellAdd";
    private const string CELL_MEASUREMENT_SUBVIEW = "cellMeasurementSubview";
    private const string CELL_FLUID_SUBVIEW = "cellFluidSubview";
    private const string CELL_ROC_SUBVIEW = "cellRateOfChangeSubview";
    private const string CELL_TIMER_SUBVIEW = "cellTimerSubview";
    private const string CELL_SPACE = "cellSpace";
    private const string CELL_SECONDARY = "cellLinked";

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
    private ObservableCollection<IWorkbenchSourceRecord> records = new ObservableCollection<IWorkbenchSourceRecord>();

    public WorkbenchTableSource(WorkbenchViewController vc, IION ion, UITableView tableView) {
      this.vc = vc;
      this.ion = ion;
      this.tableView = tableView;
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
          } else if (record is SensorPropertyRecord) {
            var spr = record as SensorPropertyRecord;
            spr.manifold.RemoveSensorProperty(spr.sensorProperty);
          }
          break;
      }
    }

    // Overridden from UITableViewSource
    public override bool CanEditRow(UITableView tableView, NSIndexPath path) {
      var record = records[path.Row];
      return record is ViewerRecord || !((record is AddRecord)) || !((record is SpaceRecord));
    }

    // Overridden from UITableViewSource
    public override string TitleForDeleteConfirmation(UITableView tableView, NSIndexPath indexPath) {
      return Strings.DELETE_QUESTION;
    }

    // Overridden from UITableViewSource
    public override void RowSelected(UITableView tableView, NSIndexPath indexPath) {
      var record = records[indexPath.Row];

      if (record is FluidRecord) {
        var fr = record as FluidRecord;

        if (fr.sensorProperty is PTChartSensorProperty) {
          var ptvc = vc.InflateViewController<PTChartViewController>(BaseIONViewController.VC_PT_CHART);
          switch (fr.pt.sensor.type) {
            case ESensorType.Pressure:
              ptvc.tUnitChanged += (changedUnit) => {
                //Log.D(this, "Changing unit to " + changedUnit);
                fr.pt.unit = changedUnit;
                tableView.ReloadData();
              };
              break;
            case ESensorType.Temperature:
              ptvc.pUnitChanged += (changedUnit) => {
                //Log.D(this, "Changing unit to " + changedUnit);
                fr.pt.unit = changedUnit;
                tableView.ReloadData();
              };
              break;
          }
          ptvc.initialManifold = fr.manifold;
          vc.NavigationController.PushViewController(ptvc, true);
        } else if (fr.sensorProperty is SuperheatSubcoolSensorProperty) {
          var shvc = vc.InflateViewController<SuperheatSubcoolViewController>(BaseIONViewController.VC_SUPERHEAT_SUBCOOL);
          shvc.initialManifold = fr.manifold;
          vc.NavigationController.PushViewController(shvc, true);
        }
      }
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
      } else if (record is SpaceRecord) {
        return 10;
      } else {
        return 48;
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
        cell.Layer.CornerRadius = 5;
        return cell;
      } else if (record is ViewerRecord) {
        var viewer = record as ViewerRecord;
        var cell = tableView.DequeueReusableCell(CELL_VIEWER) as ViewerTableCell;

        cell.UpdateTo(ion, viewer.manifold, ShowManifoldContext);
        cell.Layer.CornerRadius = 5;
        return cell;
      } else if (record is MeasurementRecord) {
        var meas = record as MeasurementRecord;
        var cell = tableView.DequeueReusableCell(CELL_MEASUREMENT_SUBVIEW) as MeasurementSensorPropertyTableCell;

        cell.UpdateTo(meas, GetLocalizedTitleString(meas.sensorProperty), "ic_refresh", (obj, sp) => {
          sp.Reset();
        });
        cell.Layer.CornerRadius = 5;
        return cell;
      } else if (record is TimerRecord) {
        var timer = record as TimerRecord;
        var cell = tableView.DequeueReusableCell(CELL_TIMER_SUBVIEW) as TimerSensorPropertyCell;

        cell.UpdateTo(timer);
        cell.Layer.CornerRadius = 5;
        return cell;
      } else if (record is RateOfChangeRecord) {
        var rr = record as RateOfChangeRecord;
        var cell = tableView.DequeueReusableCell(CELL_ROC_SUBVIEW) as RateOfChangeSensorPropertyCell;

        cell.UpdateTo(rr);
        cell.Layer.CornerRadius = 5;
        return cell;
      } else if (record is FluidRecord) {
        var fr = record as FluidRecord;
        var cell = tableView.DequeueReusableCell(CELL_FLUID_SUBVIEW) as FluidSubviewCell;

        cell.UpdateTo(fr);
        cell.Layer.CornerRadius = 5;
        return cell;
      } else if (record is SpaceRecord) {
        var cell = tableView.DequeueReusableCell(CELL_SPACE);

        cell.BackgroundColor = UIColor.Clear;
        cell.SelectionStyle = UITableViewCellSelectionStyle.None;
        cell.Layer.CornerRadius = 5;
        return cell;
      } else if (record is SecondarySensorRecord) {
        var sr = record as SecondarySensorRecord;
        var cell = tableView.DequeueReusableCell(CELL_SECONDARY) as SecondarySensorCell;

        cell.UpdateTo(sr,tableView.Bounds.Width);
        cell.SelectionStyle = UITableViewCellSelectionStyle.None;
        cell.Layer.CornerRadius = 5;
        return cell;
      }else {
        throw new Exception("Cannot get cell: " + record.viewType + " is not a supported record type.");
      }
    }

    // Overridden from IDisposable
    public void Release() {
      workbench.onWorkbenchEvent -= OnManifoldEvent;
    }

    /// <summary>
    /// Queries the index of the given manifold. Returns -1 if the manifold is not in the adapter as a record.
    /// </summary>
    /// <returns>The of manifold.</returns>
    /// <param name="manifold">Manifold.</param>
    public int IndexOfManifold(Manifold manifold) {
      for (var i = 0; i < records.Count; i++) {
        var record = records[i] as ViewerRecord;
        if (record?.manifold == manifold) {
          return i;
        }
      }

      return -1;
    }

		/// <summary>
		/// Sets the workbench for the source.
		/// </summary>
		/// <returns>The workbench.</returns>
		public void SetWorkbench(Workbench workbench) {
			this.workbench = workbench;
			this.workbench.onWorkbenchEvent += OnManifoldEvent;
			records.Clear();
			foreach (var manifold in workbench.manifolds) {
				records.Add(new ViewerRecord() {
					manifold = manifold,
					expanded = true,
				});

				foreach (var sp in manifold.sensorProperties) {
					records.Add(CreateRecordForSensorProperty(manifold, sp));
				}

				records.Add(new SpaceRecord());
			}

			records.Add(new AddRecord());

			tableView.ReloadData();
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
            sensor.device.connection.ConnectAsync();
          }));
        }
#if DEBUG
				dialog.AddAction(UIAlertAction.Create("Remote change unit", UIAlertActionStyle.Default, (action) => {
					var d = UIAlertController.Create("Select a Sensor", "", UIAlertControllerStyle.Alert);
					var device = ((GaugeDeviceSensor)manifold.primarySensor).device;
					for (int i = 0; i < device.sensorCount; i++) {
						var s = device[i];
						d.AddAction(UIAlertAction.Create(i + ": " + s.GetType(), UIAlertActionStyle.Default, (e) => {
							ShowChangeUnitDialog(s);
						}));
					}
					vc.PresentViewController(d, true, null);
				}));
#endif
      }

      dialog.AddAction(UIAlertAction.Create(Strings.Workbench.Viewer.ADD, UIAlertActionStyle.Default, (action) => {
        ShowAddSubviewDialog(tableView, manifold);
      }));

      dialog.AddAction(UIAlertAction.Create(Strings.Alarms.SELF, UIAlertActionStyle.Default, (action) => {
        var ac = this.vc.InflateViewController<SensorAlarmViewController>(BaseIONViewController.VC_SENSOR_ALARMS);
        ac.sensor = manifold.primarySensor;
        this.vc.NavigationController.PushViewController(ac, true);
      }));

      dialog.AddAction(UIAlertAction.Create(Strings.Workbench.REMOVE, UIAlertActionStyle.Default, (action) => {
        workbench.Remove(manifold);
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

      dialog.AddAction(UIAlertAction.Create(Strings.RENAME, UIAlertActionStyle.Default, (action) => {
        UIAlertController textAlert = UIAlertController.Create(Strings.ENTER_NAME, manifold.primarySensor.name, UIAlertControllerStyle.Alert);
        textAlert.AddTextField(textField => {});
        textAlert.AddAction (UIAlertAction.Create (Strings.CANCEL, UIAlertActionStyle.Cancel, UIAlertAction => {}));
        textAlert.AddAction (UIAlertAction.Create (Strings.OK_SAVE, UIAlertActionStyle.Default, UIAlertAction => {
          ion.database.Query<ION.Core.Database.LoggingDeviceRow>("UPDATE LoggingDeviceRow SET name = ? WHERE serialNumber = ?",textAlert.TextFields[0].Text,manifold.primarySensor.name);
          ion.database.Query<ION.Core.Database.DeviceRow>("UPDATE DeviceRow SET name = ? WHERE serialNumber = ?",textAlert.TextFields[0].Text,manifold.primarySensor.name);
          manifold.primarySensor.name = textAlert.TextFields[0].Text;
        }));

        vc.PresentViewController(textAlert, true, null);
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

		private void ShowChangeUnitDialog(GaugeDeviceSensor sensor) {
			var d = UIAlertController.Create("Select a Sensor", "", UIAlertControllerStyle.Alert);
			foreach (var unit in sensor.supportedUnits) {
				d.AddAction(UIAlertAction.Create(unit.ToString(), UIAlertActionStyle.Default, (e) => {
					var device = sensor.device;
					var p = device.protocol as IGaugeProtocol;
					device.connection.Write(p.CreateSetUnitCommand(device.IndexOfSensor(sensor) + 1, sensor.type, unit));
				}));
			}

			var window = UIApplication.SharedApplication.KeyWindow;
			var vc = window.RootViewController;
			vc.PresentViewController(d, true, null);
		}

    /// <summary>
    /// Throw-away 
    /// </summary>
    private delegate void AddAction(string title, Action<UIAlertAction> action);
    /// <summary>
    /// Shows an action sheet that will allow subviews to be added to the table source.
    /// </summary>
    /// <param name="tableView">Table view.</param>
    /// <param name="manifold">Manifold.</param>
    private void ShowAddSubviewDialog(UITableView tableView, Manifold manifold) {
      var dialog = UIAlertController.Create(Strings.ACTIONS, Strings.Workbench.Viewer.ADD, UIAlertControllerStyle.Alert);

      AddAction addAction = (string title, Action<UIAlertAction> action) => {
        dialog.AddAction(UIAlertAction.Create(title, UIAlertActionStyle.Default, (UIAlertAction uia) => {
          action(uia);
          tableView.ReloadData();
        }));
      };

      var sensor = manifold.primarySensor;

      if (manifold.secondarySensor != null) {
        if (!manifold.HasSensorPropertyOfType(typeof(SecondarySensorProperty))) { 
          addAction(Strings.Workbench.Viewer.SECONDARY, (UIAlertAction action) => {
            manifold.AddSensorProperty(new SecondarySensorProperty(manifold));
          });
        }
      }

      // The location of this block is kind of obnoxious, by pt chart is used by both of the below blocks.
      var ptChartFilter = new OrFilterCollection<Sensor>(new SensorOfTypeFilter(ESensorType.Pressure), new SensorOfTypeFilter(ESensorType.Temperature));
      var ptChart = manifold.ptChart;
      if (ptChart == null) {
        ptChart = PTChart.New(ion, Fluid.EState.Dew);
      }

      if (!manifold.HasSensorPropertyOfType(typeof(PTChartSensorProperty)) && ptChartFilter.Matches(sensor)) {
        manifold.ptChart = ptChart;
        addAction(Strings.Workbench.Viewer.PT_CHART_DESC, (UIAlertAction action) => {
          manifold.AddSensorProperty(new PTChartSensorProperty(manifold));
        });
      }

      // TODO Bug in checking sensor types
      // While the sensors are verified that they are pressure or temperature, they are not verified that they are exactly one
      // temperature and one pressure sensor. I let this be for the time being, in lieu expedience. This will bite you later,
      // mister maintainer. I am sorry. 
      if (!manifold.HasSensorPropertyOfType(typeof(SuperheatSubcoolSensorProperty)) &&
        ptChartFilter.Matches(sensor) && (manifold.secondarySensor == null || ptChartFilter.Matches(manifold.secondarySensor))) {
        manifold.ptChart = ptChart;
        addAction(Strings.Workbench.Viewer.SHSC_DESC, (UIAlertAction action) => {
          manifold.AddSensorProperty(new SuperheatSubcoolSensorProperty(manifold));
        });
      }

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

      if (!manifold.HasSensorPropertyOfType(typeof(RateOfChangeSensorProperty))) {
        addAction(Strings.Workbench.Viewer.ROC_DESC, (UIAlertAction action) => {
          manifold.AddSensorProperty(new RateOfChangeSensorProperty(sensor));
        });
      }

      /*
      if (!manifold.HasSensorPropertyOfType(typeof(AlternateUnitSensorProperty))) {
        addAction(Strings.Workbench.Viewer.ALT_DESC, (UIAlertAction action) => {
          manifold.AddSensorProperty(new AlternateUnitSensorProperty(sensor, sensor.supportedUnits[0]));
        });
      }
      */

      if (!manifold.HasSensorPropertyOfType(typeof(TimerSensorProperty))) {
        addAction(Strings.Workbench.Viewer.TIMER_DESC, (UIAlertAction action) => {
          manifold.AddSensorProperty(new TimerSensorProperty(sensor));
        });
      }

      dialog.AddAction(UIAlertAction.Create(Strings.CANCEL, UIAlertActionStyle.Cancel, null));

      var popover = dialog.PopoverPresentationController;
      if (popover != null) {
        popover.SourceView = tableView;
        popover.PermittedArrowDirections = UIPopoverArrowDirection.Up;
      }

      vc.PresentViewController(dialog, true, null);
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

    /// <summary>
    /// Called when the workbench changes.
    /// </summary>
    /// <param name="workbenchEvent">Workbench event.</param>
    private void OnManifoldEvent(WorkbenchEvent workbenchEvent) {
      var manifold = workbenchEvent.manifold;
      var recordIndex = IndexOfManifold(manifold);
      var indices = new List<int>();
      ViewerRecord vr;

      switch (workbenchEvent.type) {
        case WorkbenchEvent.EType.Added:
          recordIndex = records.Count - 1;
          indices.Add(recordIndex);

          vr = new ViewerRecord() {
            manifold = manifold,
            expanded = true,
          };

          records.Insert(recordIndex, vr);

          indices.Add(recordIndex + 1);
          records.Insert(recordIndex + 1, new SpaceRecord());

          tableView.InsertRows(ToNSIndexPath(indices.ToArray()), UITableViewRowAnimation.Top);
          break;    

        case WorkbenchEvent.EType.ManifoldEvent:
          OnManifoldEvent(workbenchEvent.manifoldEvent);
          break;

        case WorkbenchEvent.EType.Removed:
          var start = recordIndex;
          var end = start + 1;

          vr = records[start] as ViewerRecord;

          if (vr.expanded) {
            end += vr.manifold.sensorPropertyCount;
          }

          for (int i = end; i >= start; i--) {
            records.RemoveAt(i);
          }

          tableView.DeleteRows(ToNSIndexPath(Arrays.Range(start, end)), UITableViewRowAnimation.Top);
          break;

        case WorkbenchEvent.EType.Swapped:
          tableView.BeginUpdates();

          // TODO ahodder@appioninc.com: I think  this is a singular move and not a swap.
//          tableView.MoveRow(NSIndexPath.FromRowSection((nint)workbenchEvent.index, (nint)workbenchEvent.otherIndex));

          tableView.EndUpdates();
          break;
      }
    }

    /// <summary>
    /// Called when the table source received a new manifold event.
    /// </summary>
    /// <param name="e">E.</param>
    /// <param name="manifold">Manifold.</param>
    private void OnManifoldEvent(ManifoldEvent e) {
      var manifold = e.manifold;
      var recordIndex = IndexOfManifold(manifold);
      var indices = new List<int>();
      ViewerRecord vr;
      int index;

      switch (e.type) {
        case ManifoldEvent.EType.Invalidated:
          if (recordIndex > 0) {
            vr = records[recordIndex] as ViewerRecord;
            if (vr.expanded) {
              for (int i = manifold.sensorPropertyCount; i > 0; i--) {
                indices.Add(i + recordIndex);
              }
            }

            tableView.ReloadRows(ToNSIndexPath(indices.ToArray()), UITableViewRowAnimation.None);
          }
          break;

        case ManifoldEvent.EType.SensorPropertyAdded:
          index = recordIndex + e.index + 1;
          records.Insert(index, CreateRecordForSensorProperty(manifold, manifold[e.index]));
          tableView.InsertRows(ToNSIndexPath(new int[] { index }), UITableViewRowAnimation.Top);
          break;

        case ManifoldEvent.EType.SensorPropertyRemoved:
          index = recordIndex + e.index + 1;
          records.RemoveAt(index);
          tableView.DeleteRows(ToNSIndexPath(new int[] { index }), UITableViewRowAnimation.Top);
          break;

        case ManifoldEvent.EType.SensorPropertySwapped:
          break;
      }
    }

    /// <summary>
    /// Creates a new WorkbenchSourceRecord from the given sensor property.
    /// </summary>
    /// <returns>The record for sensor property.</returns>
    /// <param name="sensorProperty">Sensor property.</param>
    private IWorkbenchSourceRecord CreateRecordForSensorProperty(Manifold manifold, ISensorProperty sensorProperty) {
      if (sensorProperty is MinSensorProperty || sensorProperty is MaxSensorProperty || sensorProperty is HoldSensorProperty) {
        return new MeasurementRecord(manifold, sensorProperty);
      } else if (sensorProperty is TimerSensorProperty) {
        return new TimerRecord(manifold, sensorProperty as TimerSensorProperty);
      } else if (sensorProperty is RateOfChangeSensorProperty) {
        return new RateOfChangeRecord(manifold, sensorProperty);
      } else if (sensorProperty is PTChartSensorProperty || sensorProperty is SuperheatSubcoolSensorProperty) {
        return new FluidRecord(manifold, sensorProperty);
      } else if (sensorProperty is SecondarySensorProperty){
        return new SecondarySensorRecord(manifold, sensorProperty as SecondarySensorProperty);
      } else {
        throw new Exception("Cannot create WorkbenchSourceRecord for sensor property: " + sensorProperty);
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
        ret[i] = NSIndexPath.FromRowSection((nint)ints[i], (nint)0);
      }

      return ret;
    }

    public enum ViewType {
      Add,
      Space,
      Viewer,
      Measurement,
      Timer,
      Fluid,
      RateOfChange,
      Secondary,
    }
  }

  public interface IWorkbenchSourceRecord {
    WorkbenchTableSource.ViewType viewType { get; } 
  }

  public abstract class SensorPropertyRecord : IWorkbenchSourceRecord {
    public abstract WorkbenchTableSource.ViewType viewType { get; }
    public Manifold manifold { get; set; }
    public ISensorProperty sensorProperty { get; set; }

    public SensorPropertyRecord(Manifold manifold, ISensorProperty sensorProperty) {
      this.manifold = manifold;
      this.sensorProperty = sensorProperty;
    }
  }

  public class AddRecord : IWorkbenchSourceRecord {
    // Overridden from WorkbenchSourceRecord
    public WorkbenchTableSource.ViewType viewType {
      get {
        return WorkbenchTableSource.ViewType.Add;
      }
    }
  }

  public class SpaceRecord : IWorkbenchSourceRecord {
    public WorkbenchTableSource.ViewType viewType {
      get {
        return WorkbenchTableSource.ViewType.Space;
      }
    }
  }

  public class ViewerRecord : IWorkbenchSourceRecord {
    // Overridden from WorkbenchSourceRecord
    public WorkbenchTableSource.ViewType viewType {
      get {
        return WorkbenchTableSource.ViewType.Viewer;
      }
    }

    public Manifold manifold { get; set; }
    public bool expanded { get; set; }
  }
}

