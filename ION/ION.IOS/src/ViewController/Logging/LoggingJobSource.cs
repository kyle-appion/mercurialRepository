/*

using System;
using System.Collections.Generic;
using CoreGraphics;
using Foundation;
using UIKit;
using SQLite;
using System.IO;

namespace ION.IOS.ViewController.Logging {
  public class LoggingJobSource : UITableViewSource {

    List<JobData> tableItems;
    nfloat cellHeight;

    public LoggingJobSource(List<JobData> jobList, nfloat height) {
      tableItems = jobList;
      cellHeight = height;;
    }

    // Overridden from UITableViewSource
    public override nint NumberOfSections(UITableView tableView) {
      return 1;
    }

    // Overridden from UITableViewSource
    public override nint RowsInSection(UITableView tableview, nint section) {
      return tableItems.Count;
    }

    // Overriden from UITableViewSource
    public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath) {
      return cellHeight;
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
      var cell = tableView.DequeueReusableCell("jobCell") as JobCell;

      if (cell == null)
        cell = new JobCell ();   

      cell.makeCellData(tableItems[indexPath.Row].JID, tableItems[indexPath.Row].jName, tableView, cellHeight);
      cell.SelectionStyle = UITableViewCellSelectionStyle.None;
      return cell;            
    }

    public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath) {
      return true;
    }

    public override void CommitEditingStyle (UITableView tableView, UITableViewCellEditingStyle editingStyle, Foundation.NSIndexPath indexPath)
    {
      switch (editingStyle) {
        case UITableViewCellEditingStyle.Delete:
          // remove that job as well as the sessions and their measurements that are associated with that job
          var documents = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
          var _pathToDatabase = Path.Combine(documents, "AppionJSO.db");
          var db = new SQLite.SQLiteConnection(_pathToDatabase);

          //reset any session's job association if it belongs to the job being removed
          db.Query<Session>("UPDATE Session SET frnJID = null WHERE frnJID = " + tableItems[indexPath.Row].JID);
          // delete the job chosen for removal
          db.Query<Job>("DELETE FROM Job WHERE JID = " + tableItems[indexPath.Row].JID);
          // remove the item from the underlying data source
          tableItems.RemoveAt(indexPath.Row);

          // delete the row from the table
          tableView.DeleteRows(new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Fade);
          break;
        case UITableViewCellEditingStyle.None:
          Console.WriteLine ("CommitEditingStyle:None called");
          break;
      }
    }

    public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
    {
      Console.WriteLine ("Clicked: " + tableItems[indexPath.Row].jName);
      UITableViewCell cellG = tableView.CellAt(indexPath);

      if (cellG.Accessory == UITableViewCellAccessory.Checkmark) {
        cellG.Accessory = UITableViewCellAccessory.None;
      } else {
        cellG.Accessory = UITableViewCellAccessory.Checkmark;
      }
    }
  }
}


*/