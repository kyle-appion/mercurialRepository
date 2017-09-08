using System;
using System.Collections.Generic;
using Foundation;
using CoreGraphics;
using UIKit;

namespace ION.IOS.ViewController.RssFeed {

	public class RssFeedDataSource : UITableViewSource {
	  
		List<Update> tableItems;
    nfloat cellHeight;
    
		public RssFeedDataSource(List<Update> feedItems) {
			tableItems = feedItems;
		}
		
		public override nint NumberOfSections(UITableView tableView) {
      return 1;
    }

    // Overridden from UITableViewSource
    public override nint RowsInSection(UITableView tableview, nint section) {
      return tableItems.Count;
    }

    // Overriden from UITableViewSource
    public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath) {
    	//Console.WriteLine("Cell height will be " + (tableItems[indexPath.Row].description.Count * 40) + " for update " + tableItems[indexPath.Row].title);
      //return tableItems[indexPath.Row].description.Count * 40;
      return 110;
    }
    // Overriden from UITableViewSource
    public override UIView GetViewForHeader(UITableView tableView, nint section) {
      return new UIView(new CGRect(0,0,0,0));
    }
    // Overriden from UITableViewSource
    public override UIView GetViewForFooter (UITableView tableView, nint section)
    {
      return new UIView(new CGRect(0,0,0,0));
    }

    public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath) {
      var cell = tableView.DequeueReusableCell("rssFeedCell") as RssFeedCell;

      if (cell == null)
        cell = new UITableViewCell(UITableViewCellStyle.Default, "rssFeedCell") as RssFeedCell;
        
    	cellHeight = 110;
      cell.makeCellData(tableItems[indexPath.Row],cellHeight, tableView);
      cell.SelectionStyle = UITableViewCellSelectionStyle.None;

      return cell;
    }

    public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath) {
      return false;
    }

    public override void CommitEditingStyle (UITableView tableView, UITableViewCellEditingStyle editingStyle, Foundation.NSIndexPath indexPath)
    {
      switch (editingStyle) {
        case UITableViewCellEditingStyle.Delete:

          // remove the item from the underlying data source
          tableItems.RemoveAt(indexPath.Row);
          // delete the row from the table
          tableView.DeleteRows(new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Fade);
          break;
        case UITableViewCellEditingStyle.None:
          Console.WriteLine ("CommitEditingStyle:None called");
          break;
        case UITableViewCellEditingStyle.Insert:
          Console.WriteLine("wants to insert");
          break;
      }
    }

    public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
    {
    	if(!String.IsNullOrEmpty(tableItems[indexPath.Row].link)){
      	UIApplication.SharedApplication.OpenUrl(new NSUrl(tableItems[indexPath.Row].link));
      }
    } 
	}
}

