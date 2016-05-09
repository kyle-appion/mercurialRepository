using System;
using System.Collections.Generic;
using CoreGraphics;
using Foundation;
using UIKit;

using ION.IOS.Util;
using ION.Core.App;

namespace ION.IOS.ViewController.JobManager {
  public class CreatedJobSource : UITableViewSource   {
    public List<int> jobID;
    public IION ion;
    public JobViewController vc;
    public CreatedJobSource(List<int> jobs, JobViewController managerVC = null) {
      jobID = jobs;
      ion = AppState.context;
      vc = managerVC;
    }
    // Overriden from UITableViewSource
    public override UIView GetViewForHeader(UITableView tableView, nint section) {
      var headerView = new UIView(new CGRect(0,0,0,0));
      return headerView;
    }

    // Overriden from UITableViewSource
    public override UIView GetViewForFooter (UITableView tableView, nint section)
    {     
      return new UIView (new CGRect(0,0,0,0));
    }
    // Override from UITableViewSource
    public override void CommitEditingStyle(UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath) {
      switch (editingStyle) {
        case UITableViewCellEditingStyle.Delete:
          var index = (int)indexPath.Row;
          var Name = ion.database.Query<ION.Core.Database.JobRow>("SELECT jobName FROM JobRow WHERE JID = ?", jobID[indexPath.Row]);

          var xmlPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), Name[0].jobName + ".xml");
          if(System.IO.File.Exists(xmlPath)){
            System.IO.File.Delete(xmlPath);
          }
          ion.database.Query<ION.Core.Database.SessionRow>("UPDATE SessionRow SET frn_JID = 0 WHERE frn_JID = ?",jobID[index]);
          ion.database.Delete<ION.Core.Database.JobRow>(jobID[index]);

          jobID.RemoveAt(indexPath.Row);
          tableView.DeleteRows(new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Left);

          break;
      }
    }

    public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath) {
      return .3f * tableView.Bounds.Width;
    }

    // Overridden from UITableViewSource
    public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath) {
      return true;
    }

    // Overridden from UITableViewSource
    public override string TitleForDeleteConfirmation(UITableView tableView, NSIndexPath indexPath) {
      return Strings.DELETE_QUESTION;
    }

    // Overridden from UITableViewSource
    public override nint NumberOfSections(UITableView tableView) {
      return 1;
    }

    // Overridden from UITableViewSource
    public override nint RowsInSection(UITableView tableView, nint section) {
      return jobID.Count;
    }

    // Overridden from UITableViewSource
    public override void RowSelected(UITableView tableView, NSIndexPath indexPath) {      
      var jevc = vc.InflateViewController<JobEditViewController>(BaseIONViewController.VC_EDIT_JOB);
      jevc.frnJID = jobID[indexPath.Row];
      vc.NavigationController.PushViewController(jevc,true);
    }

    // Overridden from UITableViewSource
    public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath) {
      var cell = tableView.DequeueReusableCell("createdJobCell") as CreatedJobCell;
      if (cell == null) {
        cell = new UITableViewCell(UITableViewCellStyle.Default, "createdJobCell") as CreatedJobCell;
      }

      cell.makeCellData(jobID[indexPath.Row],tableView.Bounds.Width,.3 * tableView.Bounds.Width);
      cell.Layer.BorderWidth = 1f;
      cell.SelectionStyle = UITableViewCellSelectionStyle.None;
      return cell;
    }
  }
}

