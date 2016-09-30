using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.IO;

using CoreGraphics;
using Foundation;
using UIKit;
using SQLite;

using ION.Core.App;

namespace ION.IOS.ViewController.JobManager {
  public class AssociatedSessionSource : UITableViewSource {

    List<ION.IOS.ViewController.Logging.SessionData> tableItems;
    List<int> sessionID;
//    IION ion;
    double cellHeight;
    double cellWidth;
    int JID;

    public AssociatedSessionSource(List<ION.IOS.ViewController.Logging.SessionData> sessions, double height,double width,int frnJID, List<int> removeList) {
      tableItems = sessions;
      cellHeight = height;
      cellWidth = width;
//      ion = AppState.context;
      JID = frnJID;
      sessionID = removeList;
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

      var cell = tableView.DequeueReusableCell("associatedCell") as AssociatedSessionCell;

      if (cell == null) {
        cell = new UITableViewCell(UITableViewCellStyle.Default, "associatedCell") as AssociatedSessionCell;
      }

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
//          //reset any session's job association if it belongs to the job being removed
//          ion.database.Query<ION.Core.Database.SessionRow>("UPDATE SessionRow SET frn_JID = 0 WHERE frn_JID = ? AND SID = ?",JID,tableItems[indexPath.Row].SID);
//          // remove the item from the underlying data source
//          tableItems.RemoveAt(indexPath.Row);
//          // delete the row from the table
//          tableView.DeleteRows(new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Fade);
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
