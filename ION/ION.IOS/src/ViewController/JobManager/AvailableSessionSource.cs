using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using CoreGraphics;
using Foundation;
using UIKit;
using SQLite;

using ION.Core.App;

namespace ION.IOS.ViewController.JobManager {
  public class AvailableSessionSource : UITableViewSource {

    List<ION.IOS.ViewController.Logging.SessionData> tableItems;
    List<int> sessionID;

    double cellHeight;
    double cellWidth;
     
    public AvailableSessionSource(List<ION.IOS.ViewController.Logging.SessionData> sessions,double height,double width,int frnJID, List<int> addList) {
      tableItems = sessions.OrderByDescending(x => x.SID).ToList();;
      cellHeight = height;
      cellWidth = width;
      sessionID = addList;
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
      return (nfloat)cellHeight;
    }

    // Overriden from UITableViewSource
    public override UIView GetViewForHeader(UITableView tableView, nint section) {
      return null;
    }
    // Overriden from UITableViewSource
    public override UIView GetViewForFooter (UITableView tableView, nint section)
    {
      return new UIView(new CGRect(0,0,0,0));
    }

    public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath) {

      var cell = tableView.DequeueReusableCell("availableCell") as AvailableSessionCell;

      if (cell == null) {
        cell = new UITableViewCell(UITableViewCellStyle.Default, "availableCell") as AvailableSessionCell;
      }
			Console.WriteLine("Session start date is " + tableItems[indexPath.Row].start);
      cell.SetupCell(tableItems[indexPath.Row],cellHeight,cellWidth);

      cell.SelectionStyle = UITableViewCellSelectionStyle.None;

      if(sessionID.Contains(tableItems[indexPath.Row].SID)){
        cell.Accessory = UITableViewCellAccessory.Checkmark;
      }

      return cell;
    }

    public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath) {
      return false;
    }

    public override void CommitEditingStyle (UITableView tableView, UITableViewCellEditingStyle editingStyle, Foundation.NSIndexPath indexPath)
    {
      switch (editingStyle) {
        case UITableViewCellEditingStyle.Delete:
          // remove that job as well as the sessions and their measurements that are associated with that job

          //          //reset any session's job association if it belongs to the job being removed
          //          ion.database.Query<ION.Core.Database.Session>("UPDATE Session SET frnJID = null WHERE frnJID = " + tableItems[indexPath.Section].JID);
          //          // delete the job chosen for removal
          //          ion.database.Query<ION.Core.Database.Job>("DELETE FROM Job WHERE JID = " + tableItems[indexPath.Section].JID);
          //          // remove the item from the underlying data source
          //          tableItems.RemoveAt(indexPath.Row);

          // delete the row from the table
          //tableView.DeleteRows(new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Fade);
          break;
        case UITableViewCellEditingStyle.None:
          Console.WriteLine ("CommitEditingStyle:None called");
          break;
      }
    }

    public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
    {
      if(sessionID.Contains(tableItems[indexPath.Row].SID)){
        sessionID.Remove(tableItems[indexPath.Row].SID);
      } else {
        sessionID.Add(tableItems[indexPath.Row].SID);
      }

      tableView.ReloadData();
    }

  }
}
