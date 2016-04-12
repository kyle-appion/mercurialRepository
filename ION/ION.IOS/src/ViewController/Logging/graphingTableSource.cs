using System;
using System.Linq;
using System.Collections.Generic;
using UIKit;
using Foundation;
using CoreGraphics;

namespace ION.IOS.ViewController.Logging
{
	public class graphingTableSource : UITableViewSource
	{


		public graphingTableSource ()
		{
		}

		//string cellIdentifier;
		List<deviceReadings> tableItems;
		UIView parentView;
		public double trackerHeight;

		public graphingTableSource (List<deviceReadings> items,UIView View, double tHeight){
			tableItems = items;
			parentView = View;
			trackerHeight = tHeight;
		}

		public override nint RowsInSection (UITableView tableview, nint section)
		{
      return ChosenDates.includeList.Count;
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
			return .175f * parentView.Bounds.Height;
		}

		public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath) {
			return false;
		}

//		public override void CommitEditingStyle (UITableView tableView, UITableViewCellEditingStyle editingStyle, Foundation.NSIndexPath indexPath)
//		{
//			switch (editingStyle) {
//			case UITableViewCellEditingStyle.Delete:
//				// remove the item from the underlying data source
//				tableItems.RemoveAt(indexPath.Row);
//
//				// delete the row from the table
//				tableView.DeleteRows(new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Fade);
//				break;
//			case UITableViewCellEditingStyle.None:
//				Console.WriteLine ("CommitEditingStyle:None called");
//				break;
//			}
//		}

		public override UITableViewCell GetCell (UITableView tableView, Foundation.NSIndexPath indexPath)
		{

			var cell = tableView.DequeueReusableCell("graphingCell") as graphCell;
			if (cell == null) {
				cell = new UITableViewCell(UITableViewCellStyle.Default, "graphingCell") as graphCell;
			}
			
      cell.setupGraph (tableItems [indexPath.Row], tableItems,tableView.Bounds.Width, .15 * parentView.Bounds.Height, trackerHeight, parentView, tableView);
			cell.SelectionStyle = UITableViewCellSelectionStyle.None;
			return cell;
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
      Console.WriteLine ("Clicked: " + tableItems[indexPath.Row].name);

//			var cell = (graphCell)tableView.CellAt (indexPath);
//			Console.WriteLine ("Plotview bounds " + cell.plotView.Bounds);
//			foreach (var reading in tableItems[indexPath.Row].readings) {
//				Console.WriteLine ("\t" + reading);
//			}
//			Console.WriteLine (Environment.NewLine);
//			foreach (var time in tableItems[indexPath.Row].times) {
//				Console.WriteLine ("\t" + time);
//			}
		}
	}
}

