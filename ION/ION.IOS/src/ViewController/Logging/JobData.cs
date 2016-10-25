using System;
using System.Collections.ObjectModel;
using UIKit;
using CoreGraphics;

using SQLite;
using ION.Core.App;
using ION.IOS.ViewController;
using ION.IOS.ViewController.JobManager;

namespace ION.IOS.ViewController.Logging {
  public class JobData {

    public int JID;
    public string jName; 
    public ObservableCollection<SessionData> jobSessions;
    public UIView headerView;
    public UILabel headerName;
    public UIButton moreInfo;
    public UITapGestureRecognizer jobSelect;
    public IION ion;
    public bool selected;

    public JobData(int job, string name,UITableView jobTable, nfloat cellHeight, ObservableCollection<int> selectedSessions, LoggingViewController vc) {
      ion = AppState.context;
      selected = false;
      JID = job;
      jName = name;
      jobSessions = new ObservableCollection<SessionData>(); 

      headerView = new UIView(new CGRect(0,0,jobTable.Bounds.Width, .8 * cellHeight)){
        BackgroundColor = UIColor.Black,
      };

      headerName = new UILabel(new CGRect(0,0,.9 * jobTable.Bounds.Width, .8 * cellHeight)){
        Text = jName,
        TextAlignment = UITextAlignment.Center,
        AdjustsFontSizeToFitWidth = true,
        BackgroundColor = UIColor.Black,
        TextColor = UIColor.White,
      };

      moreInfo = new UIButton(new CGRect(.9 * headerView.Bounds.Width,0,.1 * headerView.Bounds.Width, .8 * cellHeight));
      moreInfo.Layer.BorderWidth = 1f;
      moreInfo.SetImage(UIImage.FromBundle("ic_more_info"), UIControlState.Normal);
      moreInfo.TouchUpInside += (sender, e) => {
        moreInfoPopup(selectedSessions, jobTable, vc);
      };

      jobSelect = new UITapGestureRecognizer(() => {
        var sessions = ion.database.Query<ION.Core.Database.SessionRow>("SELECT * FROM SessionRow WHERE frn_JID = " + JID);
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
      headerView.AddSubview(moreInfo);
      headerView.BringSubviewToFront(moreInfo);
      headerView.AddGestureRecognizer(jobSelect);
    }

    public void moreInfoPopup(ObservableCollection<int> selectedSessions,UITableView jobTable, LoggingViewController vc){

      UIAlertController moreInfoSheet;

      moreInfoSheet = UIAlertController.Create ("Job Options", "", UIAlertControllerStyle.Alert);

      moreInfoSheet.AddAction (UIAlertAction.Create ("Job Info", UIAlertActionStyle.Default, (action) => {
        var jevc = vc.InflateViewController<JobEditViewController>(BaseIONViewController.VC_EDIT_JOB);
        jevc.frnJID = JID;
        vc.NavigationController.PushViewController(jevc,true);
      }));

      moreInfoSheet.AddAction (UIAlertAction.Create ("Select All", UIAlertActionStyle.Default, (action) => {
        var sessions = ion.database.Query<ION.Core.Database.SessionRow>("SELECT * FROM SessionRow WHERE frn_JID = " + JID);
        foreach(var sesh in sessions){
          if(!selectedSessions.Contains(sesh.SID)){
            selectedSessions.Add(sesh.SID);
          }
        }
        jobTable.ReloadData();
      }));

      moreInfoSheet.AddAction (UIAlertAction.Create ("Deselect All", UIAlertActionStyle.Default, (action) => {
        var sessions = ion.database.Query<ION.Core.Database.SessionRow>("SELECT * FROM SessionRow WHERE frn_JID = " + JID);
        foreach(var sesh in sessions){
          if(selectedSessions.Contains(sesh.SID)){
            selectedSessions.Remove(sesh.SID);
          }
        }
        jobTable.ReloadData();
      }));

      moreInfoSheet.AddAction (UIAlertAction.Create (Util.Strings.CANCEL, UIAlertActionStyle.Cancel, (action) => Console.WriteLine ("Cancel Action")));
      vc.PresentViewController (moreInfoSheet, true, null);
    }
  }
}

