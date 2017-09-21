﻿using System;
using System.Collections.Generic;
using UIKit;
using Foundation;
using CoreGraphics;
using ION.Core.Sensors.Properties;
using ION.Core.App;
using ION.Core.Content;
using ION.Core.Sensors;

namespace ION.IOS.ViewController.Analyzer
{
	public class AnalyzerTableSource : UITableViewSource
	{
		Sensor sourceSensor;
    lowHighSensor tableSensors;
		public IION ion = AppState.context;

		public AnalyzerTableSource (Sensor sensor, lowHighSensor lhSensor){
      sourceSensor = sensor;
      tableSensors = lhSensor;
		}
			
		public override nint RowsInSection (UITableView tableview, nint section)
		{
      if (sourceSensor != null) {
        return sourceSensor.sensorProperties.Count;
      } else {
        return 0;
      }
		}

		public override nint NumberOfSections (UITableView tableView)
		{
			return 1;
		} 

		public override UIView GetViewForFooter (UITableView tableView, nint section)
		{
			return null;
		}

    public override UIView GetViewForHeader(UITableView tableView, nint section) {
      return null;
    }

    public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath) {
      if (sourceSensor.sensorProperties[indexPath.Row] is RateOfChangeSensorProperty){
        return 144f;
      } else {
        return 72f;
			}
    }

    public override void CommitEditingStyle (UITableView tableView, UITableViewCellEditingStyle editingStyle, Foundation.NSIndexPath indexPath)
    {
      switch (editingStyle) {
        case UITableViewCellEditingStyle.Delete:
				  // remove the item from the underlying data source
          sourceSensor.RemoveSensorProperty(sourceSensor.sensorProperties[indexPath.Row]);
					if (sourceSensor.sensorProperties.Count.Equals(0)) {           
            tableSensors.subviewHide.SetImage(null, UIControlState.Normal);
          }
          // delete the row from the table
          tableView.DeleteRows(new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Fade);
          break;
				case UITableViewCellEditingStyle.Insert:
					//---- create a new item and add it to our underlying data
					//tableItems [indexPath.Section].Insert (indexPath.Row, new TableItem ("(inserted)"));
					//---- insert a new row in the table
					tableView.InsertRows (new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Fade);
					break;
        case UITableViewCellEditingStyle.None:
          Console.WriteLine ("CommitEditingStyle:None called");
          break;
      }
    }

		public override UITableViewCell GetCell (UITableView tableView, Foundation.NSIndexPath indexPath)
		{
		  Console.WriteLine("Get Cell is call for analyzer");
      if (sourceSensor.sensorProperties[indexPath.Row] is MaxSensorProperty) {
        var cell = tableView.DequeueReusableCell("Maximum") as maximumTableCell;
        if (cell == null) {
          cell = new UITableViewCell(UITableViewCellStyle.Default, "Maximum") as maximumTableCell; 
        }
        cell.makeEvents(tableSensors,tableView.Bounds);
        cell.SelectionStyle = UITableViewCellSelectionStyle.None;
        cell.Layer.BorderWidth = 1f;
        return cell;
      } else if (sourceSensor.sensorProperties[indexPath.Row] is MinSensorProperty) {
        var cell = tableView.DequeueReusableCell("Minimum") as minimumTableCell;
        if (cell == null)
          cell = new UITableViewCell(UITableViewCellStyle.Default, "Minimum") as minimumTableCell;
        
        cell.makeEvents(tableSensors,tableView.Bounds);
        cell.SelectionStyle = UITableViewCellSelectionStyle.None;
        cell.Layer.BorderWidth = 1f;
        return cell;
      } else if (sourceSensor.sensorProperties[indexPath.Row] is HoldSensorProperty) {
        var cell = tableView.DequeueReusableCell("Hold") as holdTableCell;
        if (cell == null)
          cell = new UITableViewCell(UITableViewCellStyle.Default, "Hold") as holdTableCell;
        
        cell.makeEvents(tableSensors,tableView.Bounds);
        cell.SelectionStyle = UITableViewCellSelectionStyle.None;
        cell.Layer.BorderWidth = 1f;
        return cell;
      } else if (sourceSensor.sensorProperties[indexPath.Row] is RateOfChangeSensorProperty) {
        var cell = tableView.DequeueReusableCell("Rate") as RoCTableCell;
        if (cell == null)
          cell = new UITableViewCell(UITableViewCellStyle.Default, "Rate") as RoCTableCell;

        cell.makeEvents(tableSensors,tableView.Bounds);
        cell.Layer.BorderColor = UIColor.Black.CGColor;
        cell.Layer.BorderWidth = 1f;
        cell.SelectionStyle = UITableViewCellSelectionStyle.None;
        return cell;
      } else if (sourceSensor.sensorProperties[indexPath.Row] is SuperheatSubcoolSensorProperty) {
        var cell = tableView.DequeueReusableCell("Superheat") as SHSCTableCell;
        if (cell == null)
          cell = new UITableViewCell(UITableViewCellStyle.Default, "Superheat") as SHSCTableCell;

        cell.makeEvents(tableSensors,tableView.Bounds);
        cell.SelectionStyle = UITableViewCellSelectionStyle.None;
        cell.Layer.BorderWidth = 1f;
        return cell;
      } else if (sourceSensor.sensorProperties[indexPath.Row] is PTChartSensorProperty) {
        var cell = tableView.DequeueReusableCell("Pressure") as PTTableCell;
        if (cell == null)
          cell = new UITableViewCell(UITableViewCellStyle.Default, "Pressure") as PTTableCell;

        cell.makeEvents(tableSensors,tableView.Bounds);
        cell.SelectionStyle = UITableViewCellSelectionStyle.None;
        cell.Layer.BorderWidth = 1f;
        return cell;
      } else if (sourceSensor.sensorProperties[indexPath.Row] is SecondarySensorProperty){
        var cell = tableView.DequeueReusableCell("Linked") as secondarySensorCell;
        if (cell == null)
          cell = new UITableViewCell(UITableViewCellStyle.Default, "Linked") as secondarySensorCell;

        cell.makeEvents(tableSensors,tableView.Bounds);
        cell.SelectionStyle = UITableViewCellSelectionStyle.None;
        cell.Layer.BorderWidth = 1f;
        return cell;
      } else {
        var cell = tableView.DequeueReusableCell("Alternate") as altTableCell;
        if (cell == null)
          cell = new UITableViewCell(UITableViewCellStyle.Default, "Alternate") as altTableCell;        
        
        cell.makeEvents(tableSensors,tableView.Bounds);
        cell.SelectionStyle = UITableViewCellSelectionStyle.None;
        cell.Layer.BorderWidth = 1f;

        return cell;
      }
			
		}
		
    public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath) {
      return true;
    }
    
    public override bool CanMoveRow(UITableView tableView, NSIndexPath indexPath) {
      return true;
    }

		public override UITableViewCellEditingStyle EditingStyleForRow (UITableView tableView, NSIndexPath indexPath)
		{
			// WARNING: SPECIAL HANDLING HERE FOR THE SECOND ROW
			// ALSO MEANS SWIPE-TO-DELETE DOESN'T WORK ON THAT ROW
			//if (indexPath.Section == 0 && indexPath.Row == 1) {
			//	return UITableViewCellEditingStyle.Insert;
			//} else {
				return UITableViewCellEditingStyle.Delete;
			//}
		}
		
    public override void MoveRow(UITableView tableView, NSIndexPath sourceIndexPath, NSIndexPath destinationIndexPath) {
      var item = sourceSensor.sensorProperties[sourceIndexPath.Row];
      var deleteAt = sourceIndexPath.Row;
      var insertAt = destinationIndexPath.Row;

      // are we inserting 
      if (destinationIndexPath.Row < sourceIndexPath.Row) {
        // add one to where we delete, because we're increasing the index by inserting
        deleteAt += 1;
      } else {
        // add one to where we insert, because we haven't deleted the original yet
        insertAt += 1;
      }
      sourceSensor.sensorProperties.Insert (insertAt, item);
      sourceSensor.sensorProperties.RemoveAt (deleteAt);

      tableView.SetEditing(false, true);
    }

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			Console.WriteLine ("Clicked: " + sourceSensor.sensorProperties[indexPath.Row]);
      if(sourceSensor.sensorProperties[indexPath.Row] is RateOfChangeSensorProperty){
				//var clickedRecord = new RateOfChangeRecord(tableManifold, tableManifold.sensorProperties[indexPath.Row]);
				//var clickedRecord = new RateOfChangeRecord(tableManifold,tableManifold.sensorProperties[indexPath.Row]);

				//var rocvc = tableSensors.__analyzerviewcontroller.InflateViewController<RateofChangeSettingsViewController>(BaseIONViewController.VC_RATEOFCHANGE);
				//var passingRecord = clickedRecord;
				//rocvc.initialRecord = passingRecord;

				//tableSensors.__analyzerviewcontroller.NavigationController.PushViewController(rocvc, true);       
      }
		}
	}
}

