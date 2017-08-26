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
		public UITableView availableSessions;
		public UILabel NoneAvailable;
    public UILabel availableHeader;
    public UILabel availableWarning;
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

      sessionView = new UIView(new CGRect(0,0,parentView.Bounds.Width, .95 * parentView.Bounds.Height));
      sessionView.BackgroundColor = UIColor.White;
      sessionView.Hidden = true;
      sessionView.Layer.BorderWidth = 2f;

      var availableData = new List<ION.IOS.ViewController.Logging.SessionData>();

      foreach (var session in queryAll) {
        var data = new ION.IOS.ViewController.Logging.SessionData(session.SID,session.sessionStart,session.sessionEnd,session.frn_JID);
        availableData.Add(data);
      }
      
      //////////////////////////////////////////////////////////////////////////////////////////
      attachedHeader = new UILabel(new CGRect(0, 0, sessionView.Bounds.Width, .05 * (sessionView.Bounds.Height - 60) ));
      attachedHeader.BackgroundColor = UIColor.FromRGB(0, 166, 81);
      attachedHeader.Font = UIFont.BoldSystemFontOfSize(20);
      attachedHeader.TextAlignment = UITextAlignment.Center;
      attachedHeader.AdjustsFontSizeToFitWidth = true;     
      attachedHeader.Text = Util.Strings.Job.CURRENTSESSIONS;

      NoneAttached = new UILabel(new CGRect(.1 * sessionView.Bounds.Width,.07 * (sessionView.Bounds.Height - 60),.8 * sessionView.Bounds.Width,.08 * (sessionView.Bounds.Height - 60)));
      NoneAttached.BackgroundColor = UIColor.White;
      NoneAttached.Text = Util.Strings.Job.NOSESSIONS;
      NoneAttached.AdjustsFontSizeToFitWidth = true;
      NoneAttached.TextAlignment = UITextAlignment.Center;

      attachedSessions = new UITableView(new CGRect(0, .05 * (sessionView.Bounds.Height - 60), sessionView.Bounds.Width, .3 * (sessionView.Bounds.Height - 60)));
      attachedSessions.Bounces = false;
      attachedSessions.Layer.BorderWidth = 1f;
      attachedSessions.RegisterClassForCellReuse(typeof(AssociatedSessionCell), "associatedCell");
      attachedSessions.SeparatorStyle = UITableViewCellSeparatorStyle.None;
      if (queryAttached.Count.Equals(0)) {
				attachedSessions.BackgroundColor = UIColor.Clear;
        NoneAttached.Hidden = false;
      } else {
				attachedSessions.BackgroundColor = UIColor.White;
        NoneAttached.Hidden = true;
      }
      var attachedData = new List<ION.IOS.ViewController.Logging.SessionData>();
      foreach (var session in queryAttached) {
        var data = new ION.IOS.ViewController.Logging.SessionData(session.SID,session.sessionStart,session.sessionEnd);
        attachedData.Add(data);
      }
      attachedSessions.Source = new AssociatedSessionSource(attachedData,.12 * (sessionView.Bounds.Height - 60),sessionView.Bounds.Width, frnJID,removeList);
      attachedSessions.ReloadData();
      //////////////////////////////////////////////////////////////////////////////////////////

      removeSession = new UIButton(new CGRect(.25 * sessionView.Bounds.Width,.36 * (sessionView.Bounds.Height - 60),.5 * sessionView.Bounds.Width,.07 * (sessionView.Bounds.Height - 60)));
      removeSession.SetTitle(Util.Strings.Job.REMOVESELECTED, UIControlState.Normal);
      removeSession.SetTitleColor(UIColor.FromRGB(85, 85, 85), UIControlState.Normal);
      removeSession.BackgroundColor = UIColor.FromRGB(194, 194, 194);
      removeSession.Layer.BorderWidth = 1f;

      removeSession.TouchDown += (sender, e) => {removeSession.BackgroundColor = UIColor.Gray;};
      removeSession.TouchUpOutside += (sender, e) => {removeSession.BackgroundColor = UIColor.FromRGB(194, 194, 194);};
      removeSession.TouchUpInside += (sender, e) => {
        removeSession.BackgroundColor = UIColor.FromRGB(194, 194, 194);
        if(removeList.Count > 0){
          var window = UIApplication.SharedApplication.KeyWindow;
          var vc = window.RootViewController;
          while (vc.PresentedViewController != null) {
            vc = vc.PresentedViewController;
          }
          UIAlertController moreInfoSheet;

          moreInfoSheet = UIAlertController.Create (Util.Strings.Job.REMOVINGSESSIONS, Util.Strings.Job.REMOVEDIALOGUE, UIAlertControllerStyle.Alert);

          moreInfoSheet.AddAction (UIAlertAction.Create (Util.Strings.Job.CONFIRMREMOVAL, UIAlertActionStyle.Default, (action) => {

            foreach(var id in removeList){
              ion.database.Query<ION.Core.Database.SessionRow>("UPDATE SessionRow SET frn_JID = 0 WHERE SID = ?",id);
            } 

            removeList.Clear();
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

            attachedSessions.Source = new AssociatedSessionSource(attachedData,.12 * (sessionView.Bounds.Height - 60),sessionView.Bounds.Width, frnJID,removeList);
            attachedSessions.ReloadData();   

            availableSessions.Source = new AvailableSessionSource(availableData, .12 * (sessionView.Bounds.Height - 60),sessionView.Bounds.Width, frnJID, addList);
            availableSessions.ReloadData();

            if(removeList.Count == 0){
              NoneAttached.Hidden = false;
              attachedSessions.BackgroundColor = UIColor.Clear;
            } else {
				      NoneAttached.Hidden = true;
				      attachedSessions.BackgroundColor = UIColor.White;
            }

            if(addList.Count > 0){
              NoneAvailable.Hidden = true;
              availableSessions.BackgroundColor = UIColor.White;
            }
          }));

          moreInfoSheet.AddAction (UIAlertAction.Create (Util.Strings.CANCEL, UIAlertActionStyle.Cancel, (action) => Console.WriteLine ("Cancel Action")));
          vc.PresentViewController (moreInfoSheet, true, null);
        } 
      };
      ////////////////////////////////////////////////////////////////////////////////////////// 
      
      sessionDivider = new UILabel(new CGRect(0,.445 * (sessionView.Bounds.Height - 60),sessionView.Bounds.Width,5));
      sessionDivider.BackgroundColor = UIColor.Black;
      
      //////////////////////////////////////////////////////////////////////////////////////////
      availableHeader = new UILabel(new CGRect(0,.45 * (sessionView.Bounds.Height - 60),sessionView.Bounds.Width, .05 * (sessionView.Bounds.Height - 60)));
      availableHeader.BackgroundColor = UIColor.FromRGB(230, 103, 39);
      availableHeader.Font = UIFont.BoldSystemFontOfSize(20);
      availableHeader.TextAlignment = UITextAlignment.Center;
      availableHeader.AdjustsFontSizeToFitWidth = true;
      availableHeader.Text = Util.Strings.Job.AVAILABLESESSIONS;

      availableWarning = new UILabel(new CGRect(0, .5 * (sessionView.Bounds.Height - 60), sessionView.Bounds.Width,.07 * (sessionView.Bounds.Height - 60)));
      availableWarning.Font = UIFont.ItalicSystemFontOfSize(14);
      availableWarning.AdjustsFontSizeToFitWidth = true;
      availableWarning.TextAlignment = UITextAlignment.Center;
      availableWarning.Text = Util.Strings.Job.REDSESSIONS;
      availableWarning.Lines = 0;
      availableWarning.LineBreakMode = UILineBreakMode.WordWrap;

      NoneAvailable = new UILabel(new CGRect(.1 * sessionView.Bounds.Width,.57 * (sessionView.Bounds.Height - 60),.8 * sessionView.Bounds.Width,.08 * (sessionView.Bounds.Height - 60)));
      NoneAvailable.BackgroundColor = UIColor.White;
      NoneAvailable.Text = Util.Strings.Job.NONEAVAILABLE;
      NoneAvailable.AdjustsFontSizeToFitWidth = true;
      NoneAvailable.TextAlignment = UITextAlignment.Center;

      availableSessions = new UITableView(new CGRect(0,.57 * (sessionView.Bounds.Height - 60), sessionView.Bounds.Width,.32 * (sessionView.Bounds.Height - 60)));
      availableSessions.Bounces = false;
      availableSessions.RegisterClassForCellReuse(typeof(AvailableSessionCell), "availableCell");
      availableSessions.Layer.BorderWidth = 1f;
      availableSessions.SeparatorStyle = UITableViewCellSeparatorStyle.None;
      
      availableSessions.Source = new AvailableSessionSource(availableData, .12 * (sessionView.Bounds.Height - 60),sessionView.Bounds.Width, frnJID, addList);
      availableSessions.ReloadData(); 
      if (queryAll.Count.Equals(0)) {
				availableSessions.BackgroundColor = UIColor.Clear;
        NoneAvailable.Hidden = false;
      } else {
				availableSessions.BackgroundColor = UIColor.White;
				NoneAvailable.Hidden = true;
      }
      //////////////////////////////////////////////////////////////////////////////////////////

      addSession = new UIButton(new CGRect(.25 * sessionView.Bounds.Width, .92 * (sessionView.Bounds.Height - 60),.5 * sessionView.Bounds.Width, .07 * (sessionView.Bounds.Height - 60)));
      addSession.SetTitle(Util.Strings.Job.ADDSELECTED, UIControlState.Normal);
      addSession.SetTitleColor(UIColor.FromRGB(85,85,85), UIControlState.Normal);
      addSession.BackgroundColor = UIColor.FromRGB(194, 194, 194);
      addSession.Layer.BorderWidth = 1f;

      addSession.TouchDown += (sender, e) => {addSession.BackgroundColor = UIColor.Gray;};
      addSession.TouchUpOutside += (sender, e) => {addSession.BackgroundColor = UIColor.FromRGB(194, 194, 194);};
      addSession.TouchUpInside += (sender, e) => {
        addSession.BackgroundColor = UIColor.FromRGB(194, 194, 194);
        if(addList.Count > 0){

          foreach(var id in addList){
            ion.database.Query<ION.Core.Database.SessionRow>("UPDATE SessionRow SET frn_JID = ? WHERE SID = ?",frnJID,id);
			    }

          addList.Clear();
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

          availableSessions.Source = new AvailableSessionSource(availableData, .12 * (sessionView.Bounds.Height - 60),sessionView.Bounds.Width, frnJID, addList);
          availableSessions.ReloadData();

          attachedSessions.Source = new AssociatedSessionSource(attachedData,.12 * (sessionView.Bounds.Height - 60),sessionView.Bounds.Width, frnJID,removeList);
          attachedSessions.ReloadData();

  			  if (removeList.Count == 0) {
  				  NoneAttached.Hidden = false;
  				  attachedSessions.BackgroundColor = UIColor.Clear;
          }

  			  if (addList.Count > 0) {
  				  NoneAvailable.Hidden = true;
  				  availableSessions.BackgroundColor = UIColor.White;
  			  } else {
            NoneAvailable.Hidden = false;
  				  availableSessions.BackgroundColor = UIColor.Clear;
  			  }
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

