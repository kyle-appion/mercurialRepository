using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.IO;

using CoreGraphics;
using Foundation;
using UIKit;
using SQLite;

using ION.Core.App;

namespace ION.IOS.ViewController.Logging {
  public class LoggingJobSource : UITableViewSource {

    List<JobData> tableItems;
    ObservableCollection<int> usingSessions;
    //IION ion;
    nfloat cellHeight;

    public LoggingJobSource(List<JobData> allJobs, nfloat height, ObservableCollection<int> selectedSessions) {
      tableItems = allJobs;
      cellHeight = height;
      usingSessions = selectedSessions;
      //ion = AppState.context;
    }

    // Overridden from UITableViewSource
    public override nint NumberOfSections(UITableView tableView) {
      return tableItems.Count;
    }

    public override string TitleForHeader(UITableView tableView, nint section) {
      return tableItems[(int)section].jName;
    }

    // Overridden from UITableViewSource
    public override nint RowsInSection(UITableView tableview, nint section) {
      return tableItems[(int)section].jobSessions.Count;
    }

    // Overriden from UITableViewSource
    public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath) {
      return cellHeight;
    }
    // Overriden from UITableViewSource
    public override nfloat GetHeightForHeader(UITableView tableView, nint section) {
      return cellHeight;
    }

    // Overriden from UITableViewSource
    public override UIView GetViewForHeader(UITableView tableView, nint section) {
      return tableItems[(int)section].headerView;
    }
    // Overriden from UITableViewSource
    public override UIView GetViewForFooter (UITableView tableView, nint section)
    {
      return new UIView(new CGRect(0,0,0,0));
    }

    public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath) {

      var cell = tableView.DequeueReusableCell("jobCell") as SessionCell;

      if (cell == null)
        cell = new SessionCell ();   

      cell.makeCellData(tableItems[indexPath.Section].jobSessions[indexPath.Row].SID, tableItems[indexPath.Section].jobSessions[indexPath.Row].start,tableItems[indexPath.Section].jobSessions[indexPath.Row].finish ,tableView, cellHeight);
      cell.SelectionStyle = UITableViewCellSelectionStyle.None;

      if(usingSessions.Contains(tableItems[indexPath.Section].jobSessions[indexPath.Row].SID)){
        //cell.Accessory = UITableViewCellAccessory.Checkmark;
        cell.BackgroundColor = UIColor.LightGray;
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
          tableView.DeleteRows(new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Fade);
          break;
        case UITableViewCellEditingStyle.None:
          Console.WriteLine ("CommitEditingStyle:None called");
          break;
      }
    }

    public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
    {
      Console.WriteLine ("Clicked Job: " + tableItems[indexPath.Section].jName + "'s session " + tableItems[indexPath.Section].jobSessions[indexPath.Row].SID);
    

      if (usingSessions.Contains(tableItems[indexPath.Section].jobSessions[indexPath.Row].SID)) {
        usingSessions.Remove(tableItems[indexPath.Section].jobSessions[indexPath.Row].SID);
      } else {
        usingSessions.Add(tableItems[indexPath.Section].jobSessions[indexPath.Row].SID);
      }       

      tableView.ReloadData();
    }      
  }
}


