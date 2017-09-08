﻿using System;
using System.Collections.Generic;
using UIKit;
using Foundation;
using CoreGraphics;
using ION.Core.Sensors.Properties;
using ION.Core.App;
using ION.IOS.ViewController.Workbench;

namespace ION.IOS.ViewController.Analyzer
{
	public class AnalyzerTableSource : UITableViewSource
	{
		//string cellIdentifier;
		List<string> tableItems;
    lowHighSensor tableSensors;
		public IION ion = AppState.context;

    public AnalyzerTableSource (List<string> items, lowHighSensor lhSensor){
			tableItems = items;
      tableSensors = lhSensor;
		}
			
		public override nint RowsInSection (UITableView tableview, nint section)
		{
				return tableItems.Count;
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
      if (tableItems[indexPath.Row].Contains("Trending")){
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
          tableItems.RemoveAt(indexPath.Row);

          if (tableItems.Count.Equals(0)) {           
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
		
      if (tableItems[indexPath.Row].Contains("Maximum")) {
        var cell = tableView.DequeueReusableCell("Maximum") as maximumTableCell;
        if (cell == null) {
          cell = new UITableViewCell(UITableViewCellStyle.Default, "Maximum") as maximumTableCell; 
        }
        cell.makeEvents(tableSensors,tableView.Bounds);
        cell.SelectionStyle = UITableViewCellSelectionStyle.None;
        cell.Layer.BorderWidth = 1f;
        return cell;
      } else if (tableItems[indexPath.Row].Contains("Minimum")) {
        var cell = tableView.DequeueReusableCell("Minimum") as minimumTableCell;
        if (cell == null)
          cell = new UITableViewCell(UITableViewCellStyle.Default, "Minimum") as minimumTableCell;
        
        cell.makeEvents(tableSensors,tableView.Bounds);
        cell.SelectionStyle = UITableViewCellSelectionStyle.None;
        cell.Layer.BorderWidth = 1f;
        return cell;
      } else if (tableItems[indexPath.Row].Contains("Hold")) {
        var cell = tableView.DequeueReusableCell("Hold") as holdTableCell;
        if (cell == null)
          cell = new UITableViewCell(UITableViewCellStyle.Default, "Hold") as holdTableCell;
        
        cell.makeEvents(tableSensors,tableView.Bounds);
        cell.SelectionStyle = UITableViewCellSelectionStyle.None;
        cell.Layer.BorderWidth = 1f;
        return cell;
      } else if (tableItems[indexPath.Row].Contains("Trending") || tableItems[indexPath.Row].Contains("Rate")) {
        var cell = tableView.DequeueReusableCell("Rate") as RoCTableCell;
        if (cell == null)
          cell = new UITableViewCell(UITableViewCellStyle.Default, "Rate") as RoCTableCell;

        cell.makeEvents(tableSensors,tableView.Bounds);
        cell.Layer.BorderColor = UIColor.Black.CGColor;
        cell.Layer.BorderWidth = 1f;
        cell.SelectionStyle = UITableViewCellSelectionStyle.None;
        return cell;
      } else if (tableItems[indexPath.Row].Contains("Superheat")) {
        var cell = tableView.DequeueReusableCell("Superheat") as SHSCTableCell;
        if (cell == null)
          cell = new UITableViewCell(UITableViewCellStyle.Default, "Superheat") as SHSCTableCell;

        cell.makeEvents(tableSensors,tableView.Bounds);
        cell.SelectionStyle = UITableViewCellSelectionStyle.None;
        cell.Layer.BorderWidth = 1f;
        return cell;
      } else if (tableItems[indexPath.Row].Contains("Pressure")) {
        var cell = tableView.DequeueReusableCell("Pressure") as PTTableCell;
        if (cell == null)
          cell = new UITableViewCell(UITableViewCellStyle.Default, "Pressure") as PTTableCell;

        cell.makeEvents(tableSensors,tableView.Bounds);
        cell.SelectionStyle = UITableViewCellSelectionStyle.None;
        cell.Layer.BorderWidth = 1f;
        return cell;
      } else if (tableItems[indexPath.Row].Contains("Linked")){
        var cell = tableView.DequeueReusableCell("Linked") as secondarySensorCell;
        if (cell == null)
          cell = new UITableViewCell(UITableViewCellStyle.Default, "Linked") as secondarySensorCell;

        cell.makeEvents(tableSensors,tableView.Bounds);
        cell.SelectionStyle = UITableViewCellSelectionStyle.None;
        cell.Layer.BorderWidth = 1f;
        return cell;
      }else {
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
      var item = tableItems[sourceIndexPath.Row];
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
      tableItems.Insert (insertAt, item);
      tableItems.RemoveAt (deleteAt);

      tableView.SetEditing(false, true);
    }

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			Console.WriteLine ("Clicked: " + tableItems[indexPath.Row]);
      if(tableItems[indexPath.Row].Contains("Trending")){
        var clickedRecord = new RateOfChangeRecord(tableSensors.manifold,tableSensors.roc);

				var rocvc = tableSensors.__analyzerviewcontroller.InflateViewController<RateofChangeSettingsViewController>(BaseIONViewController.VC_RATEOFCHANGE);
				var passingRecord = clickedRecord;
				rocvc.initialRecord = passingRecord;

				tableSensors.__analyzerviewcontroller.NavigationController.PushViewController(rocvc, true);       
      }
		}

//    public override UITableViewCellEditingStyle EditingStyleForRow(UITableView tableView, NSIndexPath indexPath) {
//      return UITableViewCellEditingStyle.Delete;
//    }

	}
}

