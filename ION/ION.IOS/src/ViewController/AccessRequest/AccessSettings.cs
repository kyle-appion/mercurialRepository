
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CoreGraphics;
using ION.Core.App;
using ION.Core.Net;
using ION.IOS.App;
using ION.IOS.ViewController.Logging;
using Newtonsoft.Json.Linq;
using UIKit;

namespace ION.IOS.ViewController.AccessRequest {

	public class AccessSettings {
		public UIView settingsView;
		public UILabel uploadHeader;
		public UIButton sessionButton;
		public UITableView sessionTable;
    public UIRefreshControl refreshSessions;
    public List<SessionData> allSessions;
    public ObservableCollection<int> selectedSessions;
		public IosION ion;
		UIActivityIndicatorView uploadActivity;
		public nfloat cellHeight;
		
		public AccessSettings(UIView parentView) {
		
			ion = AppState.context as IosION;  
			
			settingsView = new UIView(new CGRect(0,0,parentView.Bounds.Width, parentView.Bounds.Height));
			settingsView.BackgroundColor = UIColor.White;
			//settingsView.Hidden = true;
			selectedSessions = new ObservableCollection<int>();
			
      selectedSessions.CollectionChanged += checkForSelected;
			cellHeight = .07f * settingsView.Bounds.Height;
			
			uploadHeader = new UILabel(new CGRect(0,0,settingsView.Bounds.Width, .1 * settingsView.Bounds.Height));
			uploadHeader.BackgroundColor = UIColor.Black;
			uploadHeader.TextAlignment = UITextAlignment.Center;
			uploadHeader.Text = "Session List";
			uploadHeader.TextColor = UIColor.FromRGB(255, 215, 101);
			
			sessionTable = new UITableView(new CGRect(.05 * settingsView.Bounds.Width,.12 * settingsView.Bounds.Height,.9 * settingsView.Bounds.Width, .65 * settingsView.Bounds.Height));
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
			sessionButton.SetTitle("Upload Session", UIControlState.Normal);
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
			settingsView.AddSubview(uploadHeader);
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
    
    public async void startUpload(object sender, EventArgs e){
			uploadActivity = new UIActivityIndicatorView(new CGRect(0,0, settingsView.Bounds.Width, settingsView.Bounds.Height));
			uploadActivity.BackgroundColor = UIColor.Black;
			uploadActivity.Alpha = .8f;
			
			settingsView.AddSubview(uploadActivity);
			settingsView.BringSubviewToFront(uploadActivity);
			uploadActivity.StartAnimating();    
    
			sessionButton.BackgroundColor = UIColor.FromRGB(255, 215, 101);
			var window = UIApplication.SharedApplication.KeyWindow;
    	var rootVC = window.RootViewController as IONPrimaryScreenController;
    	
			var ID = KeychainAccess.ValueForKey("userID");
    	
			var uploadResponse = await ion.webServices.getSession(ion, selectedSessions,ID);
    	uploadActivity.StopAnimating();
	
			if(uploadResponse != null){
				var textResponse = await uploadResponse.Content.ReadAsStringAsync();
				Console.WriteLine(textResponse);
				//parse the text string into a json object to be deserialized
				JObject response = JObject.Parse(textResponse);
				//var isregistered = response.GetValue("success").ToString();
				var message = response.GetValue("message").ToString();
			
				var alert = UIAlertController.Create ("Session Upload", message, UIAlertControllerStyle.Alert);
				alert.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Cancel, null));
				rootVC.PresentViewController (alert, animated: true, completionHandler: null);

			} else {				
				var alert = UIAlertController.Create ("Session Upload", "Upload connection was lost. Please try again.", UIAlertControllerStyle.Alert);
				alert.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Cancel, null));
				rootVC.PresentViewController (alert, animated: true, completionHandler: null);
			}
		}
	}
}
