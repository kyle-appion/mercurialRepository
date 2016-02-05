﻿using System;
using System.Collections.Generic;
using UIKit;
using Foundation;
using CoreGraphics;
using ION.Core.Sensors.Properties;

namespace ION.IOS.ViewController.Analyzer
{
	public class AnalyzerTableSource : UITableViewSource
	{
		//string cellIdentifier;
		List<string> tableItems;
    lowHighSensor tableSensors;

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
      return .521f * tableSensors.snapArea.Bounds.Height;
    }

    public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath) {
      return true;
    }

    public override void CommitEditingStyle (UITableView tableView, UITableViewCellEditingStyle editingStyle, Foundation.NSIndexPath indexPath)
    {
      switch (editingStyle) {
        case UITableViewCellEditingStyle.Delete:
          // remove the item from the underlying data source
          tableItems.RemoveAt(indexPath.Row);
          Console.WriteLine("There are " + tableItems.Count.ToString() + " subviews left");
          if (tableItems.Count.Equals(0)) {
            Console.WriteLine("All done with subviews");
            tableSensors.subviewHide.SetImage(null, UIControlState.Normal);
          }
          // delete the row from the table
          tableView.DeleteRows(new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Fade);
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
        return cell;
      } else if (tableItems[indexPath.Row].Contains("Minimum")) {
        var cell = tableView.DequeueReusableCell("Minimum") as minimumTableCell;
        if (cell == null)
          cell = new UITableViewCell(UITableViewCellStyle.Default, "Minimum") as minimumTableCell;
        
        cell.makeEvents(tableSensors,tableView.Bounds);
        cell.SelectionStyle = UITableViewCellSelectionStyle.None;
        return cell;
      } else if (tableItems[indexPath.Row].Contains("Hold")) {
        var cell = tableView.DequeueReusableCell("Hold") as holdTableCell;
        if (cell == null)
          cell = new UITableViewCell(UITableViewCellStyle.Default, "Hold") as holdTableCell;
        
        cell.makeEvents(tableSensors,tableView.Bounds);
        cell.SelectionStyle = UITableViewCellSelectionStyle.None;
        return cell;
      } else if (tableItems[indexPath.Row].Contains("Rate")) {
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
        return cell;
      } else if (tableItems[indexPath.Row].Contains("Pressure")) {
        var cell = tableView.DequeueReusableCell("Pressure") as PTTableCell;
        if (cell == null)
          cell = new UITableViewCell(UITableViewCellStyle.Default, "Pressure") as PTTableCell;

        cell.makeEvents(tableSensors,tableView.Bounds);
        cell.SelectionStyle = UITableViewCellSelectionStyle.None;
        return cell;
      } else {
        var cell = tableView.DequeueReusableCell("Alternate") as altTableCell;
        if (cell == null)
          cell = new UITableViewCell(UITableViewCellStyle.Default, "Alternate") as altTableCell;        
        
        cell.makeEvents(tableSensors,tableView.Bounds);
        cell.SelectionStyle = UITableViewCellSelectionStyle.None;
        return cell;
      }
			
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			Console.WriteLine ("Clicked: " + tableItems[indexPath.Row]);
		}

	}
}

