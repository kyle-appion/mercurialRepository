/*

using System;
using System.Collections.Generic;
using CoreGraphics;
using Foundation;
using UIKit;
using SQLite;
using System.IO;
namespace ION.IOS.ViewController.Logging {
  public class LoggingSessionSource : UITableViewSource {

    List<SessionData> tableItems;
    nfloat cellHeight;
    AssociateJob ajView;

    public LoggingSessionSource(List<SessionData> sessionList, nfloat height, AssociateJob ChooseJob) {
      tableItems = sessionList;
      cellHeight = height;
      ajView = ChooseJob;
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
      var cell = tableView.DequeueReusableCell("sessionCell") as SessionCell;

      if (cell == null)
        cell = new SessionCell();
    
      cell.makeCellData(tableItems[indexPath.Row].SID, tableItems[indexPath.Row].start, tableItems[indexPath.Row].finish, tableView, cellHeight);
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
          // remove that entry from the table and all the associated measurements
          var documents = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
          var _pathToDatabase = Path.Combine(documents, "AppionJSO.db");
          var db = new SQLite.SQLiteConnection(_pathToDatabase);        
          // delete the measurement associated with the session being removed
          db.Query<SessionMeasurement>("DELETE FROM SessionMeasurement WHERE frnSID = " + tableItems[indexPath.Row].SID);
          // delete the session that was chosen for removal
          db.Query<Session>("DELETE FROM Session WHERE SID = " + tableItems[indexPath.Row].SID);
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
      Console.WriteLine ("Clicked: " + tableItems[indexPath.Row]);

      //tableItems[indexPath.Row];
      if (ajView.chooseJob.Hidden == true)
        associateSessionToJob(tableItems[indexPath.Row].SID, cellHeight, ajView);
      else
        Console.WriteLine("Job list was already being shown");
    }

    public void associateSessionToJob(int SID, nfloat cellHeight, AssociateJob ajView) {
      var documents = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
      var _pathToDatabase = Path.Combine(documents, "AppionJSO.db");
      var db = new SQLite.SQLiteConnection(_pathToDatabase);  

      var result = db.Query<Job>("SELECT * FROM Job");
      var queriedJobs = new List<JobData>();

      foreach (var job in result) {
        queriedJobs.Add(new JobData(job.JID, job.jobName));
      }

      ajView.jobList.Source = new LoggingJobSource(queriedJobs, cellHeight);
      ajView.jobList.ReloadData();

      ajView.frnSID = SID;
      ajView.chooseJob.Hidden = false;
    }

  }
}

*/