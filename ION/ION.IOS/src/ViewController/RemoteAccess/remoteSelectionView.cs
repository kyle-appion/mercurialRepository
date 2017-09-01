using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CoreGraphics;
using UIKit;
using Newtonsoft.Json;

using ION.Core.App;
using ION.Core.Net;
using Foundation;
using ION.IOS.App;
using AudioToolbox;
using Newtonsoft.Json.Linq;

namespace ION.IOS.ViewController.RemoteAccess {
	public class remoteSelectionView {
		void HandleAction() {

		}

		public UIView selectionView;
		public UIButton remoteMenuButton;
		public UIButton fullMenuButton;
		public UILabel onlineLabel;
		public UITableView onlineTable;
		public UIActivityIndicatorView loadingUsers;
		public UIRefreshControl reloadOnline;
		public List<accessData> onlineUsers;
		public ObservableCollection<accessData> selectedUser;
		public IosION ion;
		
		public remoteSelectionView(UIView parentView) {
			ion = AppState.context as IosION; 

      ion.webServices.timedOut += timeOutAlert;
			// Perform any additional setup after loading the view, typically from a nib.
			selectedUser = new ObservableCollection<accessData>();
			selectedUser.CollectionChanged += checkForSelected;
			selectionView = new UIView(new CGRect(0,0,parentView.Bounds.Width, parentView.Bounds.Height));
			selectionView.BackgroundColor = UIColor.White;
			
			var window = UIApplication.SharedApplication.KeyWindow;
      var rootVC = window.RootViewController as IONPrimaryScreenController;
      
      onlineLabel = new UILabel(new CGRect(0, 0, parentView.Bounds.Width, .08 * selectionView.Bounds.Height));
      onlineLabel.BackgroundColor = UIColor.Black;
      onlineLabel.TextColor = UIColor.FromRGB(255, 215, 101);
      onlineLabel.Text = "Remote Viewing Access";
      onlineLabel.TextAlignment = UITextAlignment.Center;
      onlineLabel.AdjustsFontSizeToFitWidth = true;
      
      onlineTable = new UITableView(new CGRect(.05 * parentView.Bounds.Width, .08 * selectionView.Bounds.Height, .9 * parentView.Bounds.Width, .78 * selectionView.Bounds.Height));
      onlineTable.SeparatorStyle = UITableViewCellSeparatorStyle.None;
      onlineTable.Layer.CornerRadius = 5f;
      onlineTable.Layer.BorderWidth = 1f;
      onlineTable.RegisterClassForCellReuse(typeof(RemoteAccessTableCell),"remoteAccessCell");
 
      reloadOnline = new UIRefreshControl();
      reloadOnline.ValueChanged += (sender, e) => {
        GetAccessList();
      };
      onlineTable.InsertSubview(reloadOnline,0);  
      onlineTable.SendSubviewToBack(reloadOnline);
       
			remoteMenuButton = new UIButton(new CGRect(.3 * selectionView.Bounds.Width, .87 * selectionView.Bounds.Height, .4 * selectionView.Bounds.Width, .1 * selectionView.Bounds.Height));
			remoteMenuButton.BackgroundColor = UIColor.FromRGB(255, 215, 101);
			remoteMenuButton.SetTitle("Remote Mode", UIControlState.Normal);
			remoteMenuButton.SetTitleColor(UIColor.Black, UIControlState.Normal);
			remoteMenuButton.Enabled = false;
			remoteMenuButton.Alpha = .6f;
			remoteMenuButton.Layer.CornerRadius = 5f; 
			remoteMenuButton.Layer.BorderWidth = 1f;
			remoteMenuButton.TouchDown += (sender, e) => {remoteMenuButton.BackgroundColor = UIColor.Blue;};
			remoteMenuButton.TouchUpOutside += (sender, e) => {remoteMenuButton.BackgroundColor = UIColor.FromRGB(255, 215, 101);};
			remoteMenuButton.TouchUpInside += async (sender, e) => {
				remoteMenuButton.BackgroundColor = UIColor.FromRGB(255, 215, 101);
				var checkSource = onlineTable.Source as RemoteAccessTableSource;
				if(ion.webServices.uploading && checkSource.selectedUser[0].deviceID == UIDevice.CurrentDevice.IdentifierForVendor.ToString()){
					checkSource.selectedUser.Clear();
					var alert = UIAlertController.Create ("Unable to View", "You cannot view your current layout while uploading from the same device", UIAlertControllerStyle.Alert);
					alert.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Cancel, null));
					rootVC.PresentViewController (alert, animated: true, completionHandler: null);
				} else {				
					await Task.Delay(TimeSpan.FromMilliseconds(1));
					var viewing = NSUserDefaults.StandardUserDefaults.StringForKey("viewedUser");
					//var viewingLayout = NSUserDefaults.StandardUserDefaults.StringForKey("viewedLayout");
					
					if(!string.IsNullOrEmpty(viewing)){
					  ///TURN OFF ANY LOCATION POLLING TO ALLOW FOR REMOTE LOCATION BEING USED
            if (ion.settings._location.allowsGps) {			      	
			        ion.locationManager.StopAutomaticLocationPolling();
			      }
					
						remoteMenuButton.Hidden = true;
						fullMenuButton.Hidden = false;
						onlineTable.UserInteractionEnabled = false;
						ion.webServices.remoteViewing = true;
						
						///CHANGE THE APP MENU AND DEVICE MANAGER TO REFLECT REMOTE VIEWING OPTIONS
            var rion = new RemoteIosION(ion.webServices);
            if (!await rion.InitAsync()) {
              // todo ahoder@appioninc.com: do an actual error dialog here
              throw new Exception("Failed to initialize remote ion");
            }
						AppState.context = rion;
	        	rootVC.setRemoteMenu();
						ion.webServices.downloading = true;
						///START THE LAYOUT DOWNLOADING PROCESS
						startDownloading();
						onlineTable.ReloadData();
						ion.webServices.timedOut += timeOutAlert;					
	        } else {
						var alert = UIAlertController.Create ("Unable to View", "User is not available. Please try again.", UIAlertControllerStyle.Alert);
						alert.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Cancel, null));
						rootVC.PresentViewController (alert, animated: true, completionHandler: null);
					}
				}
			};
			
			fullMenuButton = new UIButton(new CGRect(.3 * selectionView.Bounds.Width, .87 * selectionView.Bounds.Height, .4 * selectionView.Bounds.Width, .1 * selectionView.Bounds.Height));
			fullMenuButton.Layer.CornerRadius = 5f;
			fullMenuButton.Layer.BorderWidth = 1f;
			fullMenuButton.BackgroundColor = UIColor.FromRGB(255, 215, 101);
			fullMenuButton.SetTitle("Local Mode",UIControlState.Normal);
			fullMenuButton.SetTitleColor(UIColor.Black, UIControlState.Normal);
			fullMenuButton.Hidden = true;
			fullMenuButton.TouchDown += (sender, e) => {fullMenuButton.BackgroundColor = UIColor.Blue;};
			fullMenuButton.TouchUpOutside += (sender, e) => {fullMenuButton.BackgroundColor = UIColor.FromRGB(255, 215, 101);};			
			fullMenuButton.TouchUpInside += async (sender, e) => {			
				fullMenuButton.BackgroundColor = UIColor.FromRGB(255, 215, 101);
	
			  ///TURN BACK ON ANY LOCATION POLLING THE LOCAL DEVICE WAS USING
        if (ion.settings._location.allowsGps) {			      	
	        ion.locationManager.StartAutomaticLocationPolling();
	      }
			
				fullMenuButton.Hidden = true;
				remoteMenuButton.Hidden = false;
				onlineTable.UserInteractionEnabled = true;
				ion.webServices.remoteViewing = false;
				ion.webServices.downloading = false;
				ion.webServices.paused = null;
				await Task.Delay(TimeSpan.FromMilliseconds(1));
				
				NSUserDefaults.StandardUserDefaults.SetString("","viewedUser");
				NSUserDefaults.StandardUserDefaults.SetString("","viewedLayout");
				selectedUser.Clear();
				onlineTable.ReloadData();						
				
				///SET THE APP MENU AND DEVICE MANAGER BACK TO THE LOCAL DEVICE'S SETTINGS
        var lion = new LocalIosION(ion.webServices);
        if (!await lion.InitAsync()) {
          // todo ahoder@appioninc.com: do an actual error dialog here
          throw new Exception("Failed to initialize local ion");
        }
        AppState.context = lion;
				rootVC.setMainMenu();
				ion.webServices.timedOut -= timeOutAlert;
			};
			
			loadingUsers = new UIActivityIndicatorView(new CGRect(0, 0, parentView.Bounds.Width, parentView.Bounds.Height));
			loadingUsers.Alpha = .8f;
			loadingUsers.BackgroundColor = UIColor.Gray;
			loadingUsers.HidesWhenStopped = true;			
			
			selectionView.AddSubview(onlineLabel);
			selectionView.AddSubview(remoteMenuButton);
			selectionView.AddSubview(fullMenuButton);
			selectionView.AddSubview(onlineTable);
			selectionView.AddSubview(loadingUsers);
			selectionView.BringSubviewToFront(loadingUsers);
			GetAccessList();
		}

		public async void GetAccessList(){
			loadingUsers.StartAnimating();
			await Task.Delay(TimeSpan.FromMilliseconds(1));
			onlineUsers = new List<accessData>();
			var ID = KeychainAccess.ValueForKey("userID");

			var feedback = await ion.webServices.GetAccessList(ID);
			
			var window = UIApplication.SharedApplication.KeyWindow;
  		var rootVC = window.RootViewController as IONPrimaryScreenController;
  		
			if(feedback != null){
			
				var textResponse = await feedback.Content.ReadAsStringAsync();
				////parse the text string into a json object to be deserialized
				JObject response = JObject.Parse(textResponse);
				var success = response.GetValue("success");
	
				if(success.ToString() == "true"){
					var viewing = response.GetValue("online");

					foreach(var user in viewing){
						var deserializedToken = JsonConvert.DeserializeObject<accessData>(user.ToString());	
              if(deserializedToken.online	== 1){			
							  onlineUsers.Add(deserializedToken);
              }
					}
				}  else {
					var errorMessage = response.GetValue("message").ToString();
					
					var alert = UIAlertController.Create ("Remote Viewing", errorMessage, UIAlertControllerStyle.Alert);
					alert.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Cancel, null));
					rootVC.PresentViewController (alert, animated: true, completionHandler: null);
				}
			} else {
				var alert = UIAlertController.Create ("Remote Viewing", "There was no response. Please try again.", UIAlertControllerStyle.Alert);
				alert.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Cancel, null));
				rootVC.PresentViewController (alert, animated: true, completionHandler: null);
			}
			
			onlineTable.Source = new RemoteAccessTableSource(onlineUsers, selectedUser, ion.webServices.webClient);
			onlineTable.ReloadData();
			
			loadingUsers.StopAnimating();
      reloadOnline.EndRefreshing();
		}
		
    public void checkForSelected(object sender, EventArgs e){
      if(selectedUser.Count > 0){
        remoteMenuButton.Enabled = true;
        remoteMenuButton.Alpha = 1f;
      } else {
        remoteMenuButton.Enabled = false;
        remoteMenuButton.Alpha = .6f;
      }
    }
    
		public async void timeOutAlert(string offlineMessage){
			if(ion.webServices.downloading == false){
				return;
			}		
			ion.webServices.timedOut -= timeOutAlert;

      var window = UIApplication.SharedApplication.KeyWindow;
      var rootVC = window.RootViewController as IONPrimaryScreenController;
      
		 	ion.webServices.downloading = false;
		 	ion.webServices.remoteViewing = false;
		 	ion.webServices.paused = null;
		 	
			NSUserDefaults.StandardUserDefaults.SetString("","viewedUser");
			NSUserDefaults.StandardUserDefaults.SetString("","viewedLayout");

			var lion = new LocalIosION(ion.webServices);
        if (!await lion.InitAsync()) {
          // todo ahoder@appioninc.com: do an actual error dialog here
          throw new Exception("Failed to initialize local ion");
        }
        AppState.context = lion;
			rootVC.setMainMenu();   
			
			var alert = UIAlertController.Create ("Viewing User", offlineMessage, UIAlertControllerStyle.Alert);

			alert.AddAction (UIAlertAction.Create ("OK", UIAlertActionStyle.Cancel, (action) => {}));
			rootVC.PresentViewController (alert, animated: true, completionHandler: null);			
		}
		
		public async void startDownloading(){
			var viewingID = NSUserDefaults.StandardUserDefaults.StringForKey("viewedUser");
			var viewingLayout =	NSUserDefaults.StandardUserDefaults.StringForKey("viewedLayout");
				
			var loggedUser = KeychainAccess.ValueForKey("userID");
			var loggingInterval = Convert.ToInt32(NSUserDefaults.StandardUserDefaults.IntForKey("settings_default_logging_interval"));
			ion.webServices.paused += pauseRemote;
			while(ion.webServices.downloading){
				await ion.webServices.DownloadLayouts(ion, viewingID, loggingInterval, viewingLayout);
			}
			//webServices.StartLayoutDownload(viewingID,loggedUser,loggingInterval,viewingLayout);
		}
		
		public void pauseRemote(bool paused){
			if(paused == false){
				Console.WriteLine("Should be starting the download now");
				var viewingID = NSUserDefaults.StandardUserDefaults.StringForKey("viewedUser");
				var viewingLayout =	NSUserDefaults.StandardUserDefaults.StringForKey("viewedLayout");
				
				var loggedUser = KeychainAccess.ValueForKey("userID");
				var loggingInterval = Convert.ToInt32(NSUserDefaults.StandardUserDefaults.IntForKey("settings_default_logging_interval"));
				
				ion.webServices.StartLayoutDownload(ion, viewingID,loggedUser,loggingInterval,viewingLayout);
			}
		}
	}
	
	[Foundation.Preserve(AllMembers = true)]
	public class accessData{
		public accessData(){}
		[JsonProperty("display")]
		public string displayName { get; set; }
		[JsonProperty("email")]
		public string email { get; set; }
		[JsonProperty("ID")]
		public int id { get; set; }
		[JsonProperty("online")]
		public int online { get; set; }
		[JsonProperty("devicename")]
		public string deviceName { get; set; }
		[JsonProperty("deviceid")]
		public string deviceID { get; set; }		
		[JsonProperty("layoutid")]
		public int layoutid { get; set; }
	}
}
