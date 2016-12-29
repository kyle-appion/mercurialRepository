
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CoreGraphics;
using ION.Core.App;
using ION.Core.Net;
using ION.IOS.ViewController.Logging;
using UIKit;

namespace ION.IOS.ViewController.AccessRequest {

	public class AccessSettings {
		public UIView settingsView;
		public UIButton onlineButton;
		public UIButton sessionButton;
		public UITableView sessionTable;
    public UIRefreshControl refreshSessions;
    public List<SessionData> allSessions;
    public ObservableCollection<int> selectedSessions;
		public IION ion;
		public WebPayload webServices;
		
		public nfloat cellHeight;
		
		public AccessSettings(UIView parentView, WebPayload webServices) {
			ion = AppState.context;
			this.webServices = webServices;
			
			settingsView = new UIView(new CGRect(0,0,parentView.Bounds.Width, parentView.Bounds.Height - 50));
			settingsView.BackgroundColor = UIColor.White;
			settingsView.Hidden = true;
			selectedSessions = new ObservableCollection<int>();
			
      selectedSessions.CollectionChanged += checkForSelected;
			cellHeight = .07f * settingsView.Bounds.Height;

			onlineButton = new UIButton(new CGRect(.15 * settingsView.Bounds.Width, .05 * settingsView.Bounds.Height, .7 * settingsView.Bounds.Width, .1 * settingsView.Bounds.Height));
			onlineButton.Layer.CornerRadius = 5f;
			onlineButton.Layer.BorderWidth = 1f;
			onlineButton.BackgroundColor = UIColor.FromRGB(255, 215, 101);
			onlineButton.SetTitle("Allow Remote", UIControlState.Normal);
			onlineButton.SetTitleColor(UIColor.Black, UIControlState.Normal);
			onlineButton.TouchDown += (sender, e) => {onlineButton.BackgroundColor = UIColor.Blue;};
			onlineButton.TouchUpOutside += (sender, e) => {onlineButton.BackgroundColor = UIColor.FromRGB(255, 215, 101);};
			
			sessionTable = new UITableView(new CGRect(.05 * settingsView.Bounds.Width,.18 * settingsView.Bounds.Height,.9 * settingsView.Bounds.Width, .65 * settingsView.Bounds.Height));
      sessionTable.RegisterClassForCellReuse(typeof(SessionCell),"sessionCell");
      sessionTable.BackgroundColor = UIColor.Clear;
      sessionTable.SeparatorStyle = UITableViewCellSeparatorStyle.None;
      sessionTable.EstimatedRowHeight = 0;
      sessionTable.Layer.BorderWidth = 1f;

      refreshSessions = new UIRefreshControl();
      refreshSessions.ValueChanged += (sender, e) => {
        if(!ion.dataLogManager.isRecording){
          ReloadAllSessions();
        }
      };

      sessionTable.InsertSubview(refreshSessions,0);
      sessionTable.SendSubviewToBack(refreshSessions);      
      
			sessionButton = new UIButton(new CGRect(.25 * settingsView.Bounds.Width, .85 * settingsView.Bounds.Height, .5 * settingsView.Bounds.Width, .1 * settingsView.Bounds.Height));
			sessionButton.SetTitle("Upload Session(s)", UIControlState.Normal);
			sessionButton.SetTitleColor(UIColor.Black, UIControlState.Normal);
			sessionButton.Layer.BorderWidth = 1f;
			sessionButton.Layer.CornerRadius = 5f;
			sessionButton.BackgroundColor = UIColor.FromRGB(255, 215, 101);
			sessionButton.Enabled = false;
			sessionButton.Alpha = .6f;
			sessionButton.TouchDown += (sender, e) => {sessionButton.BackgroundColor = UIColor.Blue;};
			sessionButton.TouchUpOutside += (sender, e) => {sessionButton.BackgroundColor = UIColor.FromRGB(255, 215, 101);};
			sessionButton.TouchUpInside += startUpload;
			
			settingsView.AddSubview(sessionTable);
			settingsView.AddSubview(sessionButton);
			settingsView.AddSubview(onlineButton);
			refreshSessions.SendActionForControlEvents(UIControlEvent.ValueChanged);
		}
		
    public void ReloadAllSessions(){
      var result = ion.database.Query<ION.Core.Database.SessionRow>("SELECT SID, sessionStart, sessionEnd FROM SessionRow ORDER BY SID DESC");
      List<SessionData> queriedSessions = new List<SessionData>();

      for (int i = 0; i < result.Count; i++) {
        queriedSessions.Add(new SessionData(result[i].SID, result[i].sessionStart, result[i].sessionEnd));

        var measurements = ion.database.Query<ION.Core.Database.SensorMeasurementRow>("SELECT * FROM SensorMeasurementRow WHERE frn_SID = ? ORDER BY MID ASC", result[i].SID);

        foreach (var meas in measurements) {        
          queriedSessions[i].sessionMeasurements.Add(new MeasurementData(meas.MID, meas.frn_SID, meas.serialNumber, meas.measurement.ToString()));
        }
      }
      allSessions = queriedSessions;

      sessionTable.Source = new LoggingSessionSource(allSessions,cellHeight, selectedSessions);
      sessionTable.ReloadData();
      sessionTable.LayoutIfNeeded();

      refreshSessions.EndRefreshing();
    }
 
    public void checkForSelected(object sender, EventArgs e){
      if(selectedSessions.Count > 0){
        sessionButton.Enabled = true;
        sessionButton.Alpha = 1f;
      } else {
        sessionButton.Enabled = false;
        sessionButton.Alpha = .6f;
      }
    }
    
    public void startUpload(object sender, EventArgs e){
			sessionButton.BackgroundColor = UIColor.FromRGB(255, 215, 101);
			webServices.getSession(selectedSessions);
		}
	}
}
