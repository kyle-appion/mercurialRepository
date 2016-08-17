using System;
using System.Collections.Generic;
using UIKit;
using Foundation;
using CoreGraphics;

namespace ION.IOS.ViewController.Logging
{
	public class legendSource : UITableViewSource
	{
		List<deviceReadings> tableItems;
		UIView parentView;

		public legendSource (List<deviceReadings> items,UIView View)
		{
			tableItems = items;
			parentView = View;
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
			return .166f * parentView.Bounds.Height;
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
			var cell = tableView.DequeueReusableCell("legendCell") as legendCell;
			if (cell == null) {
				cell = new UITableViewCell(UITableViewCellStyle.Default, "legendCell") as legendCell;
			}

			cell.setupTable (parentView,tableItems [indexPath.Row],tableItems);
			cell.SelectionStyle = UITableViewCellSelectionStyle.None;
			return cell;
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			Console.WriteLine ("Clicked: " + tableItems[indexPath.Row].serialNumber);
		}
	}
}


