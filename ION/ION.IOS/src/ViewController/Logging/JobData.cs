using System;
using System.Collections.ObjectModel;
using UIKit;
using CoreGraphics;

using SQLite;
using ION.Core.App;

namespace ION.IOS.ViewController.Logging {
  public class JobData {

    public int JID;
    public string jName; 
    public ObservableCollection<SessionData> jobSessions;
    public UIView headerView;
    public UILabel headerName;
    public UITapGestureRecognizer jobSelect;
    public IION ion;
    public bool selected;

    public JobData(int job, string name,UITableView jobTable, nfloat cellHeight, ObservableCollection<int> selectedSessions) {
      ion = AppState.context;
      selected = false;
      JID = job;
      jName = name;
      jobSessions = new ObservableCollection<SessionData>();

      headerView = new UIView(new CGRect(0,0,jobTable.Bounds.Width,cellHeight)){
        BackgroundColor = UIColor.Black,
      };

      headerName = new UILabel(new CGRect(0,0,jobTable.Bounds.Width, cellHeight)){
        Text = jName,
        TextAlignment = UITextAlignment.Center,
        AdjustsFontSizeToFitWidth = true,
        BackgroundColor = UIColor.Clear,
        TextColor = UIColor.White,
      };

      jobSelect = new UITapGestureRecognizer(() => {
        var sessions = ion.database.Query<ION.Core.Database.Session>("SELECT * FROM Session WHERE frnJID = " + JID);
        if(selected.Equals(false)){
          selected = true;
          foreach(var sesh in sessions){
            if(!selectedSessions.Contains(sesh.SID)){
              selectedSessions.Add(sesh.SID);
            }
          }
        } else {
          selected = false;
          foreach(var sesh in sessions){
            if(selectedSessions.Contains(sesh.SID)){
              selectedSessions.Remove(sesh.SID);
            }
          }
        }
        jobTable.ReloadData();
      });

      headerView.AddSubview(headerName);
      headerView.AddGestureRecognizer(jobSelect);
    }

  }
}

