namespace ION.IOS.ViewController.Workbench {

  using System;
  using System.Collections.Generic;
  using System.Collections.ObjectModel;

  using Foundation;
  using UIKit;

	using Appion.Commons.Util;

  using ION.Core.App;
  using ION.Core.Content;
  using ION.Core.Devices;
	using ION.Core.Devices.Protocols;
  using ION.Core.Fluids;
  using ION.Core.Sensors;
  using ION.Core.Sensors.Filters;
  using ION.Core.Sensors.Properties;

  using ION.IOS.Util;
  using ION.IOS.App;
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
		private const string CELL_TARGET_SUBVIEW = "cellTargetSHSC";

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
    private IosION ion { get; set; }
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
    /// <summary>
    /// Flag to determine if subviews should be rendered or not
    /// </summary>
    private bool expanded = true;

    public TargetSHSCView setTargetView;

    public WorkbenchTableSource(WorkbenchViewController vc, IosION ion, UITableView tableView) {
      this.vc = vc;
      this.ion = ion;
      this.tableView = tableView;
      setTargetView = new TargetSHSCView(vc.View);
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
						//workbench.Remove(vr.manifold);
						workbench.Remove(vr.sensor);
          } else if (record is SensorPropertyRecord) {
            var spr = record as SensorPropertyRecord;
						//spr.manifold.RemoveSensorProperty(spr.sensorProperty);

						spr.sensor.RemoveSensorProperty(spr.sensorProperty);
            SetWorkbench(workbench);
          }
          break;
      }
    }

    // Overridden from UITableViewSource
    public override bool CanEditRow(UITableView tableView, NSIndexPath path) {
      var record = records[path.Row];
			if (record is AddRecord || record is SpaceRecord){
				return false;
			} else {
				return true;
			}
    }

    public override bool CanMoveRow(UITableView tableView, NSIndexPath indexPath) {
    	if(records[indexPath.Row] is ViewerRecord){
      	return true;
      } else {
      	return false;
      }
    }
    
    public override void MoveRow(UITableView tableView, NSIndexPath sourceIndexPath, NSIndexPath destinationIndexPath) {
			workbench.onWorkbenchEvent -= OnManifoldEvent;
			this.workbench.onWorkbenchEvent -= OnManifoldEvent;
			
			var item = records[sourceIndexPath.Row];
			//var manifold = workbench.manifolds[sourceIndexPath.Row];
			var sensor = workbench.sensors[sourceIndexPath.Row];
			//Console.WriteLine("Grabbed manifold " + manifold.primarySensor.name + "-" + manifold.primarySensor.measurement);
			Console.WriteLine("Grabbed manifold " + sensor.name + "-" + sensor.measurement);
      var deleteAt = sourceIndexPath.Row;
      var insertAt = destinationIndexPath.Row;

      // are we inserting
      Console.WriteLine("MoveRow manifold from index " + sourceIndexPath.Row + " going to index " + destinationIndexPath.Row + " total number of rows " + (records.Count - 1));

	      if(destinationIndexPath.Row < records.Count - 1){
		      tableView.SetEditing(false, true);
	      
		      if (destinationIndexPath.Row < sourceIndexPath.Row) {
		        // add one to where we delete, because we're increasing the index by inserting
		        deleteAt += 1;
		      } else {
		        // add one to where we insert, because we haven't deleted the original yet
		        insertAt += 1;
		      }
		      records.Insert (insertAt, item);
		      records.RemoveAt (deleteAt);
  				//workbench.manifolds.Insert(insertAt,manifold);
  				//workbench.manifolds.RemoveAt(deleteAt);
  				workbench.sensors.Insert(insertAt, sensor);
  				workbench.sensors.RemoveAt(deleteAt);

					expanded = true;
					this.workbench.onWorkbenchEvent -= OnManifoldEvent;
					SetWorkbench(workbench);				
	      } else {
		      tableView.SetEditing(false, true);
	      
		      records.Insert (records.Count - 2, item);
		      records.RemoveAt (deleteAt);

  				//workbench.manifolds.Insert(records.Count - 1,manifold);
  				//workbench.manifolds.RemoveAt(deleteAt);
  				workbench.sensors.Insert(records.Count - 1, sensor);
  				workbench.sensors.RemoveAt(deleteAt);

					expanded = true;
					this.workbench.onWorkbenchEvent -= OnManifoldEvent;
					SetWorkbench(workbench);		
				}
        tableView.ReloadData();
      Console.WriteLine("reloading data");
    }
    // Overridden from UITableViewSource
    public override string TitleForDeleteConfirmation(UITableView tableView, NSIndexPath indexPath) {
      return Strings.DELETE_QUESTION;
    }

    // Overridden from UITableViewSource
    public override void RowSelected(UITableView tableView, NSIndexPath indexPath) {
      var record = records[indexPath.Row];
    	Console.WriteLine("Clicked row " + indexPath.Row+ ". Record is a " + record.viewType);

      if (record is FluidRecord) {
        var fr = record as FluidRecord;

        if (fr.sensorProperty is PTChartSensorProperty) {
          var ptvc = vc.InflateViewController<PTChartViewController>(BaseIONViewController.VC_PT_CHART);
          ptvc.initialSensor = fr.sensor;
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

          vc.NavigationController.PushViewController(ptvc, true);
        } else if (fr.sensorProperty is SuperheatSubcoolSensorProperty) {
          var shvc = vc.InflateViewController<SuperheatSubcoolViewController>(BaseIONViewController.VC_SUPERHEAT_SUBCOOL);
					shvc.initialSensor = fr.sensor;
          vc.NavigationController.PushViewController(shvc, true);
        } else if (fr.sensorProperty is TargetSuperheatSubcoolProperty){
					setTargetView.setManifold(fr.sensor);
        }
      } else if (record is MeasurementRecord){
				var property = ((MeasurementRecord)record).sensorProperty as AlternateUnitSensorProperty;
				if(property != null){
					var dialog = UIAlertController.Create("Choose Unit", null, UIAlertControllerStyle.Alert);
					
					var unitList = property.sensor.supportedUnits;
					
					foreach(var unit in unitList){
	          dialog.AddAction(UIAlertAction.Create(unit.ToString(), UIAlertActionStyle.Default, (action) => {
	            property.unit = unit;
	          }));
					}
 					dialog.AddAction(UIAlertAction.Create(Strings.CANCEL, UIAlertActionStyle.Cancel, null));
					vc.PresentViewController(dialog, false, null);
				}
			} else if (record is RateOfChangeRecord){
				Console.WriteLine("Workbench table source clicked the rate of change cell. Sending record of " + ((RateOfChangeRecord)record).sensor.name);
          var rocvc = vc.InflateViewController<RateofChangeSettingsViewController>(BaseIONViewController.VC_RATEOFCHANGE);
          var passingRecord = record as RateOfChangeRecord;
          rocvc.initialRecord = passingRecord;
          vc.NavigationController.PushViewController(rocvc, true);
      } else if (record is ViewerRecord){

				ShowManifoldContext(null,((ViewerRecord)record).sensor);
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
        //return 158;
        return 100;
      } else if (record is SpaceRecord) {
        return 10;
      } else if (record is AddRecord){
			 	return 58;
			} else if (record is RateOfChangeRecord){
				return 96;
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

        var webIon = ion as IosION;

        if(webIon is RemoteIosION) {
					cell.Hidden = true;
				} else {
					cell.Hidden = false;
				}

        return cell;
      } else if (record is ViewerRecord) {
        var viewer = record as ViewerRecord;
        var cell = tableView.DequeueReusableCell(CELL_VIEWER) as ViewerTableCell;

				cell.UpdateTo(ion, viewer.sensor);

        var longPress = new UILongPressGestureRecognizer((obj) => {
					if(obj.State == UIGestureRecognizerState.Began){
						if(tableView.Editing){
							tableView.SetEditing(false,false);
							expanded = true;
							this.workbench.onWorkbenchEvent -= OnManifoldEvent;
							SetWorkbench(workbench);
						} else {
							tableView.SetEditing(true,false);
							expanded = false;
							this.workbench.onWorkbenchEvent -= OnManifoldEvent;
							SetWorkbench(workbench);							
						}	
					}	else if(obj.State == UIGestureRecognizerState.Cancelled){
						tableView.SetEditing(false,false);
						expanded = true;
						this.workbench.onWorkbenchEvent -= OnManifoldEvent;
						SetWorkbench(workbench);
					}
				});
				this.tableView.AddGestureRecognizer(longPress);
				cell.ContentView.BackgroundColor = UIColor.Clear;
        return cell;
      } else if (record is MeasurementRecord) {
        var meas = record as MeasurementRecord;
        var cell = tableView.DequeueReusableCell(CELL_MEASUREMENT_SUBVIEW) as MeasurementSensorPropertyTableCell;

        cell.UpdateTo(meas, GetLocalizedTitleString(meas.sensorProperty), "ic_refresh", (obj, sp) => {
          sp.Reset();
        });
   
        return cell;
      } else if (record is TimerRecord) {
        var timer = record as TimerRecord;
        var cell = tableView.DequeueReusableCell(CELL_TIMER_SUBVIEW) as TimerSensorPropertyCell;

        cell.UpdateTo(timer);

        return cell;
      } else if (record is RateOfChangeRecord) {
        var rr = record as RateOfChangeRecord;
        var cell = tableView.DequeueReusableCell(CELL_ROC_SUBVIEW) as RateOfChangeSensorPropertyCell;

        cell.UpdateTo(rr);

        return cell;
      } else if (record is FluidRecord) {
        var fr = record as FluidRecord;
        if (fr.sensorProperty is TargetSuperheatSubcoolProperty){
					var cell = tableView.DequeueReusableCell(CELL_TARGET_SUBVIEW) as TargetSHSCCell;
					cell.UpdateTo(fr);
					return cell;

				} else {
					var cell = tableView.DequeueReusableCell(CELL_FLUID_SUBVIEW) as FluidSubviewCell;

					cell.UpdateTo(fr);

					return cell;
        }

      } else if (record is SpaceRecord) {
        var cell = tableView.DequeueReusableCell(CELL_SPACE);

        cell.BackgroundColor = UIColor.Clear;
        cell.SelectionStyle = UITableViewCellSelectionStyle.None;

        return cell;
      } else if (record is SecondarySensorRecord) {
        var sr = record as SecondarySensorRecord;
        var cell = tableView.DequeueReusableCell(CELL_SECONDARY) as SecondarySensorCell;

        cell.UpdateTo(sr,tableView.Bounds.Width);
        cell.SelectionStyle = UITableViewCellSelectionStyle.None;

        return cell;
      } else {
        Log.E(this,"Cannot get cell: " + record.viewType + " is not a supported record type.");
        return null;
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
		public int IndexOfManifold(Sensor sensor) {
      for (var i = 0; i < records.Count; i++) {
        var record = records[i] as ViewerRecord;
				if (record != null && record.sensor == sensor) {
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
			foreach (var checkSensor in workbench.sensors) {
				records.Add(new ViewerRecord() {
					sensor = checkSensor,
					expanded = true,
				});
				
				if(expanded){
					foreach (var sp in checkSensor.sensorProperties) {
						records.Add(CreateRecordForSensorProperty(checkSensor, sp));
					}
				}
			}

			records.Add(new AddRecord());

			tableView.ReloadData();
      ion.SaveWorkbenchAsync();
		}

		/// <summary>
		/// Shows the context menu for the manifold.
		/// </summary>
		/// <param name="obj">Object.</param>
		/// <param name="sensor">Sensor.</param>
		private void ShowManifoldContext(object obj, Sensor sensor) {
      var window = UIApplication.SharedApplication.KeyWindow;
      var vc = window.RootViewController;
      while (vc.PresentedViewController != null) {
        vc = vc.PresentedViewController;
      }

			var dialog = UIAlertController.Create(sensor.name, Strings.Workbench.SELECT_VIEWER_ACTION.FromResources(), UIAlertControllerStyle.Alert);

			if (sensor is GaugeDeviceSensor) {

				var checkSensor = sensor as GaugeDeviceSensor;
        // Append gauge device sensor context items
        if (!checkSensor.device.isConnected) {
          dialog.AddAction(UIAlertAction.Create(Strings.Device.RECONNECT.FromResources(), UIAlertActionStyle.Default, (action) => {
            checkSensor.device.connection.Connect();
          }));
        }
				dialog.AddAction(UIAlertAction.Create("Remote change unit", UIAlertActionStyle.Default, (action) => {
					var d = UIAlertController.Create("Select a Sensor", "", UIAlertControllerStyle.Alert);

					var device = ((GaugeDeviceSensor)sensor).device;
					for (int i = 0; i < device.sensorCount; i++) {
						var s = device[i];

						d.AddAction(UIAlertAction.Create(s.type.ToString(), UIAlertActionStyle.Default, (e) => {
							ShowChangeUnitDialog(s);
						}));
					}
					d.AddAction(UIAlertAction.Create("Cancel", UIAlertActionStyle.Cancel,null));
					vc.PresentViewController(d, true, null);
				}));
      }

      dialog.AddAction(UIAlertAction.Create(Strings.Workbench.Viewer.ADD, UIAlertActionStyle.Default, (action) => {

		    ShowAddSubviewDialog(tableView, sensor);
      }));

      dialog.AddAction(UIAlertAction.Create(Strings.Alarms.SELF, UIAlertActionStyle.Default, (action) => {
        var ac = this.vc.InflateViewController<SensorAlarmViewController>(BaseIONViewController.VC_SENSOR_ALARMS);

  		  ac.sensor = sensor;
        this.vc.NavigationController.PushViewController(ac, true);
      }));

      dialog.AddAction(UIAlertAction.Create(Strings.Workbench.REMOVE, UIAlertActionStyle.Default, (action) => {
  		  workbench.Remove(sensor);
      }));

			if (sensor is GaugeDeviceSensor) {
				var checkSensor = sensor as GaugeDeviceSensor;
				// Append gauge device sensor context items
				if (checkSensor.device.isConnected) {
          dialog.AddAction(UIAlertAction.Create(Strings.Device.DISCONNECT.FromResources(), UIAlertActionStyle.Default, (action) => {
    			  checkSensor.device.connection.Disconnect();
          }));
        }
      }

      dialog.AddAction(UIAlertAction.Create(Strings.RENAME, UIAlertActionStyle.Default, (action) => {
		  UIAlertController textAlert = UIAlertController.Create(Strings.ENTER_NAME, sensor.name, UIAlertControllerStyle.Alert);
        textAlert.AddTextField(textField => {});
        textAlert.AddAction (UIAlertAction.Create (Strings.CANCEL, UIAlertActionStyle.Cancel, UIAlertAction => {}));
        textAlert.AddAction (UIAlertAction.Create (Strings.OK_SAVE, UIAlertActionStyle.Default, UIAlertAction => {
          //ion.database.Query<ION.Core.Database.LoggingDeviceRow>("UPDATE LoggingDeviceRow SET name = ? WHERE serialNumber = ?",textAlert.TextFields[0].Text,manifold.primarySensor.name);
          //ion.database.Query<ION.Core.Database.DeviceRow>("UPDATE DeviceRow SET name = ? WHERE serialNumber = ?",textAlert.TextFields[0].Text,manifold.primarySensor.name);
          //manifold.primarySensor.name = textAlert.TextFields[0].Text;
    			ion.database.Query<ION.Core.Database.LoggingDeviceRow>("UPDATE LoggingDeviceRow SET name = ? WHERE serialNumber = ?", textAlert.TextFields[0].Text, sensor.name);
    			ion.database.Query<ION.Core.Database.DeviceRow>("UPDATE DeviceRow SET name = ? WHERE serialNumber = ?", textAlert.TextFields[0].Text, sensor.name);
    			sensor.name = textAlert.TextFields[0].Text;
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
      if(!tableView.Editing){
      	vc.PresentViewController(dialog, true, null);
      }
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
			d.AddAction(UIAlertAction.Create("Cancel", UIAlertActionStyle.Cancel,null));
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
		/// <param name="sensor">Sensor.</param>
		private void ShowAddSubviewDialog(UITableView tableView, Sensor sensor) {
      var dialog = UIAlertController.Create(Strings.ACTIONS, Strings.Workbench.Viewer.ADD, UIAlertControllerStyle.Alert);

      AddAction addAction = (string title, Action<UIAlertAction> action) => {
        dialog.AddAction(UIAlertAction.Create(title, UIAlertActionStyle.Default, (UIAlertAction uia) => {
          action(uia);
          tableView.ReloadData();
        }));
      };

			var checkedSensor = sensor;

			if (sensor.linkedSensor != null) {
				if (!sensor.HasSensorPropertyOfType(typeof(SecondarySensorProperty))) { 
          addAction(Strings.Workbench.Viewer.SECONDARY, (UIAlertAction action) => {
    			  sensor.AddSensorProperty(new SecondarySensorProperty(sensor));
            SetWorkbench(workbench);
          });
        }
      }

      // The location of this block is kind of obnoxious, by pt chart is used by both of the below blocks.
      var ptChartFilter = new OrFilterCollection<Sensor>(new SensorOfTypeFilter(ESensorType.Pressure), new SensorOfTypeFilter(ESensorType.Temperature));

			if (!sensor.HasSensorPropertyOfType(typeof(PTChartSensorProperty)) && ptChartFilter.Matches(sensor)) {
          addAction(Strings.Workbench.Viewer.PT_CHART_DESC, (UIAlertAction action) => {
			    sensor.AddSensorProperty(new PTChartSensorProperty(sensor));
    			SetWorkbench(workbench);
    		});
      }

			// TODO Bug in checking sensor types
			// While the sensors are verified that they are pressure or temperature, they are not verified that they are exactly one
			// temperature and one pressure sensor. I let this be for the time being, in lieu expedience. This will bite you later,
			// mister maintainer. I am sorry. 
			if (!sensor.HasSensorPropertyOfType(typeof(SuperheatSubcoolSensorProperty)) && ptChartFilter.Matches(sensor) && (sensor.linkedSensor == null || ptChartFilter.Matches(sensor.linkedSensor))) {
        addAction(Strings.Workbench.Viewer.SHSC_DESC, (UIAlertAction action) => {
    			sensor.AddSensorProperty(new SuperheatSubcoolSensorProperty(sensor));
    			SetWorkbench(workbench);
    		});
      }

			if (!sensor.HasSensorPropertyOfType(typeof(MinSensorProperty))) {
        addAction(Strings.Workbench.Viewer.MIN_DESC, (UIAlertAction action) => {
    			sensor.AddSensorProperty(new MinSensorProperty(sensor));
    			SetWorkbench(workbench);
    		});
      }

			if (!sensor.HasSensorPropertyOfType(typeof(MaxSensorProperty))) {
        addAction(Strings.Workbench.Viewer.MAX_DESC, (UIAlertAction action) => {
    			sensor.AddSensorProperty(new MaxSensorProperty(sensor));
    			SetWorkbench(workbench);
    		});
      }

			if (!sensor.HasSensorPropertyOfType(typeof(HoldSensorProperty))) { 
        addAction(Strings.Workbench.Viewer.HOLD_DESC, (UIAlertAction action) => {
    			sensor.AddSensorProperty(new HoldSensorProperty(sensor));
    			SetWorkbench(workbench);
    		});
      }

			if (!sensor.HasSensorPropertyOfType(typeof(RateOfChangeSensorProperty))) {
        addAction(Strings.Workbench.Viewer.ROC_DESC, (UIAlertAction action) => {
    			sensor.AddSensorProperty(new RateOfChangeSensorProperty(sensor,ion.preferences.device.trendInterval));
    			SetWorkbench(workbench);
    		});
      }

		  if (!sensor.HasSensorPropertyOfType(typeof(AlternateUnitSensorProperty))) {
		    addAction(Strings.Workbench.Viewer.ALT_DESC, (UIAlertAction action) => {
					sensor.AddSensorProperty(new AlternateUnitSensorProperty(sensor, sensor.supportedUnits[0]));
					SetWorkbench(workbench);
				});
      }

			if (!sensor.HasSensorPropertyOfType(typeof(TimerSensorProperty))) {
        addAction(Strings.Workbench.Viewer.TIMER_DESC, (UIAlertAction action) => {
          sensor.AddSensorProperty(new TimerSensorProperty(sensor));
    			SetWorkbench(workbench);
    		});
      }

			if (!sensor.HasSensorPropertyOfType(typeof(TargetSuperheatSubcoolProperty))) {
        addAction(Strings.Workbench.Viewer.TARGET_SHSC, (UIAlertAction action) => {
    			sensor.AddSensorProperty(new TargetSuperheatSubcoolProperty(sensor));
    			SetWorkbench(workbench);
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
        //should not throw exceptions
        //throw new ArgumentException("Cannot identifiy sensor property: " + sensorProperty);
        return Strings.Workbench.Viewer.MIN;
      }
    }

    /// <summary>
    /// Called when the workbench changes.
    /// </summary>
    /// <param name="workbenchEvent">Workbench event.</param>
    private void OnManifoldEvent(WorkbenchEvent workbenchEvent) {
			var checkSensor = workbenchEvent.sensor;
			var recordIndex = IndexOfManifold(checkSensor);
      var indices = new List<int>();
      ViewerRecord vr;

      switch (workbenchEvent.type) {
        case WorkbenchEvent.EType.Added:
        Console.WriteLine("Added a Sensor");
          recordIndex = records.Count - 1;
          indices.Add(recordIndex);

          vr = new ViewerRecord() {
			      //manifold = manifold,
			      sensor = checkSensor,
            expanded = true,
          };

          records.Insert(recordIndex, vr);

          //indices.Add(recordIndex + 1);
          //records.Insert(recordIndex + 1, new SpaceRecord());

          tableView.InsertRows(ToNSIndexPath(indices.ToArray()), UITableViewRowAnimation.Top);
          tableView.ReloadData();
          Console.WriteLine("Should have reloaded table data");
          break;

        case WorkbenchEvent.EType.ManifoldEvent:
	        if (workbenchEvent.manifoldEvent.type != ManifoldEvent.EType.Invalidated) {
	          OnManifoldEvent(workbenchEvent.manifoldEvent);
	        }
          break;

        case WorkbenchEvent.EType.Removed:
          var start = recordIndex;
          var end = start;
					if (recordIndex == -1)
					{
						return;
					}
          vr = records[start] as ViewerRecord;

          if (vr.expanded) {
						end += vr.sensor.sensorPropertyCount;
          }
          vr.sensor.sensorProperties.Clear();
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
    	if(!expanded){
				return;
			}
			var checkSensor = e.sensor;
			var recordIndex = IndexOfManifold(checkSensor);
      var indices = new List<int>();
      ViewerRecord vr;
      int index;
			
      switch (e.type) {
        case ManifoldEvent.EType.SecondarySensorAdded:
        	goto case ManifoldEvent.EType.Invalidated;
        case ManifoldEvent.EType.SecondarySensorRemoved:
        	goto case ManifoldEvent.EType.Invalidated;
        case ManifoldEvent.EType.Invalidated:
          if (recordIndex > 0) {
            vr = records[recordIndex] as ViewerRecord;
            if (vr.expanded) {
							for (int i = checkSensor.sensorPropertyCount; i > 0; i--) {
                indices.Add(i + recordIndex);
              }
            }

            tableView.ReloadRows(ToNSIndexPath(indices.ToArray()), UITableViewRowAnimation.None);
          }
          break;

        case ManifoldEvent.EType.SensorPropertyAdded:
          index = recordIndex + e.index + 1;
					records.Insert(index, CreateRecordForSensorProperty(checkSensor, checkSensor[e.index]));
          tableView.InsertRows(ToNSIndexPath(new int[] { index }), UITableViewRowAnimation.Top);
          SetWorkbench(workbench);
          break;

        case ManifoldEvent.EType.SensorPropertyRemoved:
          index = recordIndex + e.index + 1;
          records.RemoveAt(index);
          tableView.DeleteRows(ToNSIndexPath(new int[] { index }), UITableViewRowAnimation.Top);
					SetWorkbench(workbench);
					break;

        case ManifoldEvent.EType.SensorPropertySwapped:
          break;
      }
    }

		public void collapseSubviews(){

		}
		
		public void expandSubviews(){

		}
		/// <summary>
		/// Creates a new WorkbenchSourceRecord from the given sensor property.
		/// </summary>
		/// <returns>The record for sensor property.</returns>
		/// <param name="sensorProperty">Sensor property.</param>
		private IWorkbenchSourceRecord CreateRecordForSensorProperty(Sensor sensor, ISensorProperty sensorProperty) {
      if (sensorProperty is MinSensorProperty || sensorProperty is MaxSensorProperty || sensorProperty is HoldSensorProperty) {
				return new MeasurementRecord(sensor, sensorProperty);
      } else if (sensorProperty is TimerSensorProperty) {
				return new TimerRecord(sensor, sensorProperty as TimerSensorProperty);
      } else if (sensorProperty is RateOfChangeSensorProperty) {
				return new RateOfChangeRecord(sensor, sensorProperty);
      } else if (sensorProperty is PTChartSensorProperty || sensorProperty is SuperheatSubcoolSensorProperty || sensorProperty is TargetSuperheatSubcoolProperty) {
				return new FluidRecord(sensor, sensorProperty);
      } else if (sensorProperty is SecondarySensorProperty){
				return new SecondarySensorRecord(sensor, sensorProperty as SecondarySensorProperty);
      } else if (sensorProperty is AlternateUnitSensorProperty){
				return new MeasurementRecord(sensor, sensorProperty as AlternateUnitSensorProperty);
      } else {
        //throw new Exception("Cannot create WorkbenchSourceRecord for sensor property: " + sensorProperty);
        Log.E(this, "Sensor property chosen that hasn't been implemented yet");
        return null;
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
      Target,
    }
  }

  public interface IWorkbenchSourceRecord {
    WorkbenchTableSource.ViewType viewType { get; } 
  }

  public abstract class SensorPropertyRecord : IWorkbenchSourceRecord {
    public abstract WorkbenchTableSource.ViewType viewType { get; }
		public Sensor sensor { get; set; }
    public ISensorProperty sensorProperty { get; set; }

		public SensorPropertyRecord(Sensor sensor, ISensorProperty sensorProperty) {
			this.sensor = sensor;
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

		//public Manifold manifold { get; set; }
		public Sensor sensor { get; set; }
    public bool expanded { get; set; }
  }
}

