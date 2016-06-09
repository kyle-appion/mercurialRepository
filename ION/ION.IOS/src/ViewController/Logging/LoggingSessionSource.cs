using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.IO;

using CoreGraphics;
using Foundation;
using UIKit;
using SQLite;

using ION.Core.Database;

using ION.Core.App;
namespace ION.IOS.ViewController.Logging {
  public class LoggingSessionSource : UITableViewSource {

    List<SessionData> tableItems;
    nfloat cellHeight;
    ObservableCollection<int> usingSessions;
    IION ion;
    public LoggingSessionSource(List<SessionData> allSessions, nfloat height, ObservableCollection<int> selectedSessions) {
      tableItems = allSessions;
      cellHeight = height;
      usingSessions = selectedSessions;
      ion = AppState.context;
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

      if(usingSessions.Contains(tableItems[indexPath.Row].SID)){
        cell.Accessory = UITableViewCellAccessory.Checkmark;
        //cell.BackgroundColor = UIColor.LightGray;
      }
      return cell;            
    }

    public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath) {
      return true;
    }

    public override void CommitEditingStyle (UITableView tableView, UITableViewCellEditingStyle editingStyle, Foundation.NSIndexPath indexPath)
    {
      switch (editingStyle) {
        case UITableViewCellEditingStyle.Delete:
          var sessionId = tableItems[indexPath.Row].SID;
          // remove that entry from the table and all the associated measurements     
          // delete the measurement associated with the session being removed
          ion.database.Table<SensorMeasurementRow>()
            .Delete(smr => smr.frn_SID == sessionId);
          // delete the session that was chosen for removal
          ion.database.Table<SessionRow>()
            .Delete(sr => sr.SID == sessionId);
          // remove the item from the list of selected sessions
          if (usingSessions.Contains(tableItems[indexPath.Row].SID)) {
            usingSessions.Remove(tableItems[indexPath.Row].SID);
          }
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
      Console.WriteLine ("Clicked: " + tableItems[indexPath.Row].SID);

      if (usingSessions.Contains(tableItems[indexPath.Row].SID)) {
        usingSessions.Remove(tableItems[indexPath.Row].SID);
        tableView.ReloadData();
      } else {
        usingSessions.Add(tableItems[indexPath.Row].SID);
        tableView.ReloadData();
      }

    }
  }
}

