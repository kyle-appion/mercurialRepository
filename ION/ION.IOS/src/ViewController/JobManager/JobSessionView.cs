using System;
using System.Collections.Generic;
using System.IO;
using UIKit;
using Foundation;
using CoreGraphics;

using ION.Core.App;

namespace ION.IOS.ViewController.JobManager {
  public class JobSessionView {
    IION ion;
    public UIView sessionView;
    public UILabel NoneAttached;
    public UILabel attachedHeader;
    public UITableView attachedSessions;
    public UILabel NoneAvailable;
    public UILabel availableHeader;
    public UILabel availableWarning;
    public UITableView availableSessions;
    public UIButton addSession;
    public UIButton removeSession;
    public UILabel sessionDivider;
    public List<int> addList;
    public List<int> removeList;

    public int frnJID;

    public JobSessionView(UIView parentView,int JID = 0) {
      ion = AppState.context;
      frnJID = JID;
      addList = new List<int>();
      removeList = new List<int>();

      var queryAll = ion.database.Query<ION.Core.Database.SessionRow>("SELECT SID, sessionStart, sessionEnd, frn_JID FROM SessionRow WHERE frn_JID <> ? OR frn_JID IS NULL",frnJID);
      var queryAttached = ion.database.Query<ION.Core.Database.SessionRow>("SELECT SID, sessionStart, sessionEnd FROM SessionRow WHERE frn_JID = ?", frnJID);

      sessionView = new UIView(new CGRect(0,0,parentView.Bounds.Width,parentView.Bounds.Height));
      sessionView.Hidden = true;

      var availableData = new List<ION.IOS.ViewController.Logging.SessionData>();


      if (queryAll.Count.Equals(0)) {
        availableSessions.Hidden = true;
      }
      foreach (var session in queryAll) {
        var data = new ION.IOS.ViewController.Logging.SessionData(session.SID,session.sessionStart,session.sessionEnd,session.frn_JID);
        availableData.Add(data);
      }
      
      //////////////////////////////////////////////////////////////////////////////////////////
      attachedHeader = new UILabel(new CGRect(.25 * sessionView.Bounds.Width, .02 * (sessionView.Bounds.Height - 60), .5 * sessionView.Bounds.Width, .05 * (sessionView.Bounds.Height - 60) ));
      attachedHeader.Font = UIFont.BoldSystemFontOfSize(20);
      attachedHeader.TextAlignment = UITextAlignment.Center;
      attachedHeader.AdjustsFontSizeToFitWidth = true;     
      attachedHeader.Text = "Current Sessions";

      NoneAttached = new UILabel(new CGRect(.1 * sessionView.Bounds.Width,.07 * (sessionView.Bounds.Height - 60),.8 * sessionView.Bounds.Width,.08 * (sessionView.Bounds.Height - 60)));
      NoneAttached.Text = "No Sessions Currently Associated";
      NoneAttached.AdjustsFontSizeToFitWidth = true;
      NoneAttached.TextAlignment = UITextAlignment.Center;
      if (!queryAttached.Count.Equals(0)) {
        NoneAttached.Hidden = true;
      } 

      attachedSessions = new UITableView(new CGRect(.025 * sessionView.Bounds.Width, .07 * (sessionView.Bounds.Height - 60), .95 * sessionView.Bounds.Width, .3 * (sessionView.Bounds.Height - 60)));
      attachedSessions.Bounces = false;
      attachedSessions.Layer.BorderWidth = 1f;
      attachedSessions.RegisterClassForCellReuse(typeof(AssociatedSessionCell), "associatedCell");
      attachedSessions.SeparatorStyle = UITableViewCellSeparatorStyle.None;
      if (queryAttached.Count.Equals(0)) {
        attachedSessions.Hidden = true;
      } 
      var attachedData = new List<ION.IOS.ViewController.Logging.SessionData>();
      foreach (var session in queryAttached) {
        var data = new ION.IOS.ViewController.Logging.SessionData(session.SID,session.sessionStart,session.sessionEnd);
        attachedData.Add(data);
      }
      //attachedSessions.Source = new AssociatedSessionSource(attachedData,.05 * sessionView.Bounds.Height,sessionView.Bounds.Width, frnJID,removeList);
      attachedSessions.Source = new AssociatedSessionSource(attachedData,.1 * (sessionView.Bounds.Height - 60),sessionView.Bounds.Width, frnJID,removeList);
      attachedSessions.ReloadData();
      //////////////////////////////////////////////////////////////////////////////////////////

      removeSession = new UIButton(new CGRect(.25 * sessionView.Bounds.Width,.38 * (sessionView.Bounds.Height - 60),.5 * sessionView.Bounds.Width,.05 * (sessionView.Bounds.Height - 60)));
      removeSession.SetTitle("Remove Selected", UIControlState.Normal);
      removeSession.SetTitleColor(UIColor.Black, UIControlState.Normal);
      removeSession.BackgroundColor = UIColor.FromRGB(255, 215, 101);
      removeSession.Layer.BorderWidth = 1f;

      removeSession.TouchDown += (sender, e) => {removeSession.BackgroundColor = UIColor.Blue;};
      removeSession.TouchUpOutside += (sender, e) => {removeSession.BackgroundColor = UIColor.FromRGB(255, 215, 101);};
      removeSession.TouchUpInside += (sender, e) => {
        removeSession.BackgroundColor = UIColor.FromRGB(255, 215, 101);
        if(removeList.Count > 0){
          var window = UIApplication.SharedApplication.KeyWindow;
          var vc = window.RootViewController;
          while (vc.PresentedViewController != null) {
            vc = vc.PresentedViewController;
          }
          UIAlertController moreInfoSheet;

          moreInfoSheet = UIAlertController.Create ("Removing Sessions", "Are you sure you want to remove these sessions from the current job?", UIAlertControllerStyle.Alert);

          moreInfoSheet.AddAction (UIAlertAction.Create ("Confirm Removal", UIAlertActionStyle.Default, (action) => {


            NoneAvailable.Hidden = true;
            availableSessions.Hidden = false;

            foreach(var id in removeList){
              ion.database.Query<ION.Core.Database.SessionRow>("UPDATE SessionRow SET frn_JID = 0 WHERE SID = ?",id);
            } 

            removeList = new List<int>();
            attachedData = new List<ION.IOS.ViewController.Logging.SessionData>();
            var redoAttached = ion.database.Query<ION.Core.Database.SessionRow>("SELECT SID, sessionStart, sessionEnd FROM SessionRow WHERE frn_JID = ?", frnJID);

            foreach (var session in redoAttached) {
              var data = new ION.IOS.ViewController.Logging.SessionData(session.SID,session.sessionStart,session.sessionEnd);
              attachedData.Add(data);
            } 

            availableData = new List<ION.IOS.ViewController.Logging.SessionData>();
            var redoAll = ion.database.Query<ION.Core.Database.SessionRow>("SELECT SID, sessionStart, sessionEnd, frn_JID FROM SessionRow WHERE frn_JID <> ? OR frn_JID IS NULL",frnJID);

            foreach (var session in redoAll) {
              var data = new ION.IOS.ViewController.Logging.SessionData(session.SID,session.sessionStart,session.sessionEnd, session.frn_JID );
              availableData.Add(data);
            }

            //attachedSessions.Source = new AssociatedSessionSource(attachedData,.05 * sessionView.Bounds.Height,sessionView.Bounds.Width, frnJID,removeList);
            attachedSessions.Source = new AssociatedSessionSource(attachedData,.1 * (sessionView.Bounds.Height - 60),sessionView.Bounds.Width, frnJID,removeList);
            attachedSessions.ReloadData();   

            //availableSessions.Source = new AvailableSessionSource(availableData, .05 * sessionView.Bounds.Height,sessionView.Bounds.Width, frnJID, addList);
            availableSessions.Source = new AvailableSessionSource(availableData, .1 * (sessionView.Bounds.Height - 60),sessionView.Bounds.Width, frnJID, addList);
            availableSessions.ReloadData();
          }));

          moreInfoSheet.AddAction (UIAlertAction.Create (Util.Strings.CANCEL, UIAlertActionStyle.Cancel, (action) => Console.WriteLine ("Cancel Action")));
          vc.PresentViewController (moreInfoSheet, true, null);
        } 
      };
      ////////////////////////////////////////////////////////////////////////////////////////// 
      
      sessionDivider = new UILabel(new CGRect(0,.44 * (sessionView.Bounds.Height - 60),sessionView.Bounds.Width,5));
      sessionDivider.BackgroundColor = UIColor.Black;
      
      //////////////////////////////////////////////////////////////////////////////////////////
      availableHeader = new UILabel(new CGRect(.25 * sessionView.Bounds.Width,.45 * (sessionView.Bounds.Height - 60),.5 * sessionView.Bounds.Width, .05 * (sessionView.Bounds.Height - 60)));
      availableHeader.Font = UIFont.BoldSystemFontOfSize(20);
      availableHeader.TextAlignment = UITextAlignment.Center;
      availableHeader.AdjustsFontSizeToFitWidth = true;
      availableHeader.Text = "Available Sessions";

      availableWarning = new UILabel(new CGRect(.025 * sessionView.Bounds.Width, .5 * (sessionView.Bounds.Height - 60), .95 * sessionView.Bounds.Width,.07 * (sessionView.Bounds.Height - 60)));
      availableWarning.Font = UIFont.ItalicSystemFontOfSize(14);
      availableWarning.AdjustsFontSizeToFitWidth = true;
      availableWarning.TextAlignment = UITextAlignment.Center;
      availableWarning.Text = "(Adding a session in red will remove the association to it's current job)";
      availableWarning.Lines = 0;
      availableWarning.LineBreakMode = UILineBreakMode.WordWrap;

      NoneAvailable = new UILabel(new CGRect(.1 * sessionView.Bounds.Width,.57 * (sessionView.Bounds.Height - 60),.8 * sessionView.Bounds.Width,.08 * (sessionView.Bounds.Height - 60)));
      NoneAvailable.Text = "No Sessions Available";
      NoneAvailable.AdjustsFontSizeToFitWidth = true;
      NoneAvailable.TextAlignment = UITextAlignment.Center;
      if (!queryAll.Count.Equals(0)) { 
        NoneAvailable.Hidden = true;
      }
      availableSessions = new UITableView(new CGRect(.025 * sessionView.Bounds.Width,.57 * (sessionView.Bounds.Height - 60),.95 * sessionView.Bounds.Width,.3 * (sessionView.Bounds.Height - 60)));
      availableSessions.Bounces = false;
      availableSessions.RegisterClassForCellReuse(typeof(AvailableSessionCell), "availableCell");
      availableSessions.Layer.BorderWidth = 1f;
      availableSessions.SeparatorStyle = UITableViewCellSeparatorStyle.None;
      
      //availableSessions.Source = new AvailableSessionSource(availableData, .05 * sessionView.Bounds.Height,sessionView.Bounds.Width, frnJID, addList);
      availableSessions.Source = new AvailableSessionSource(availableData, .1 * (sessionView.Bounds.Height - 60),sessionView.Bounds.Width, frnJID, addList);
      availableSessions.ReloadData();   
      //////////////////////////////////////////////////////////////////////////////////////////

      addSession = new UIButton(new CGRect(.25 * sessionView.Bounds.Width, .88 * (sessionView.Bounds.Height - 60),.5 * sessionView.Bounds.Width, .05 * (sessionView.Bounds.Height - 60)));
      addSession.SetTitle("Add Selected", UIControlState.Normal);
      addSession.SetTitleColor(UIColor.Black, UIControlState.Normal);
      addSession.BackgroundColor = UIColor.FromRGB(255, 215, 101);
      addSession.Layer.BorderWidth = 1f;

      addSession.TouchDown += (sender, e) => {addSession.BackgroundColor = UIColor.Blue;};
      addSession.TouchUpOutside += (sender, e) => {addSession.BackgroundColor = UIColor.FromRGB(255, 215, 101);};
      addSession.TouchUpInside += (sender, e) => {
        addSession.BackgroundColor = UIColor.FromRGB(255, 215, 101);
        if(addList.Count > 0){
          NoneAttached.Hidden = true;
          attachedSessions.Hidden = false;   

          foreach(var id in addList){
            ion.database.Query<ION.Core.Database.SessionRow>("UPDATE SessionRow SET frn_JID = ? WHERE SID = ?",frnJID,id);
          }

          addList = new List<int>();
          availableData = new List<ION.IOS.ViewController.Logging.SessionData>();
          var redoAll = ion.database.Query<ION.Core.Database.SessionRow>("SELECT SID, sessionStart, sessionEnd, frn_JID FROM SessionRow WHERE frn_JID <> ? OR frn_JID IS NULL",frnJID);

          foreach (var session in redoAll) {
            var data = new ION.IOS.ViewController.Logging.SessionData(session.SID,session.sessionStart,session.sessionEnd,session.frn_JID);
            availableData.Add(data);
          }

          attachedData = new List<ION.IOS.ViewController.Logging.SessionData>();
          var redoAttached = ion.database.Query<ION.Core.Database.SessionRow>("SELECT SID, sessionStart, sessionEnd FROM SessionRow WHERE frn_JID = ?", frnJID);

          foreach (var session in redoAttached) {
            var data = new ION.IOS.ViewController.Logging.SessionData(session.SID,session.sessionStart,session.sessionEnd);
            attachedData.Add(data);
          }

          //availableSessions.Source = new AvailableSessionSource(availableData, .05 * sessionView.Bounds.Height,sessionView.Bounds.Width, frnJID, addList);
          availableSessions.Source = new AvailableSessionSource(availableData, .1 * (sessionView.Bounds.Height - 60),sessionView.Bounds.Width, frnJID, addList);
          availableSessions.ReloadData();

          //attachedSessions.Source = new AssociatedSessionSource(attachedData,.05 * sessionView.Bounds.Height,sessionView.Bounds.Width, frnJID,removeList);
          attachedSessions.Source = new AssociatedSessionSource(attachedData,.1 * (sessionView.Bounds.Height - 60),sessionView.Bounds.Width, frnJID,removeList);
          attachedSessions.ReloadData();
        }
      };

      sessionView.AddSubview(attachedHeader);
      sessionView.AddSubview(NoneAttached);
      sessionView.AddSubview(attachedSessions);
      sessionView.AddSubview(removeSession);

      sessionView.AddSubview(sessionDivider);

      sessionView.AddSubview(availableHeader);
      sessionView.AddSubview(availableWarning);
      sessionView.AddSubview(NoneAvailable);
      sessionView.AddSubview(availableSessions);
      sessionView.AddSubview(addSession);
    }
  }
}

