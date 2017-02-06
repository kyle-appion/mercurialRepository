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
		public UIView selectionView;
		public UIButton remoteMenuButton;
		public UIButton fullMenuButton;
		public UILabel onlineLabel;
		//public UILabel offlineLabel;
		public UITableView onlineTable;
		//public UITableView offlineTable;
		public UIActivityIndicatorView loadingUsers;
		public UIRefreshControl reloadOnline;
		//public UIRefreshControl reloadOffline;
		public List<accessData> onlineUsers;
		public List<accessData> offlineUsers;
		public ObservableCollection<int> selectedUser;
		public IosION ion;
		public WebPayload webServices;

		
		public remoteSelectionView(UIView parentView) {
			ion = AppState.context as IosION; 

      this.webServices = ion.webServices;
      webServices.timedOut += timeOutAlert;
			// Perform any additional setup after loading the view, typically from a nib.
			selectedUser = new ObservableCollection<int>();
			selectedUser.CollectionChanged += checkForSelected;
			selectionView = new UIView(new CGRect(0,0,parentView.Bounds.Width, parentView.Bounds.Height));
			selectionView.BackgroundColor = UIColor.White;
			
			var window = UIApplication.SharedApplication.KeyWindow;
      var rootVC = window.RootViewController as IONPrimaryScreenController;
      
      //onlineLabel = new UILabel(new CGRect(.05 * parentView.Bounds.Width, 0, .9 * parentView.Bounds.Width, .08 * selectionView.Bounds.Height));
      onlineLabel = new UILabel(new CGRect(0, 0, parentView.Bounds.Width, .08 * selectionView.Bounds.Height));
      onlineLabel.BackgroundColor = UIColor.Black;
      onlineLabel.TextColor = UIColor.FromRGB(255, 215, 101);
      //onlineLabel.Text = "Online Users";
      onlineLabel.Text = "Remote Viewing Access";
      onlineLabel.TextAlignment = UITextAlignment.Center;
      onlineLabel.AdjustsFontSizeToFitWidth = true;
      
      //onlineTable = new UITableView(new CGRect(.05 * parentView.Bounds.Width, .08 * selectionView.Bounds.Height, .9 * parentView.Bounds.Width, .35 * selectionView.Bounds.Height));
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
     
      //offlineLabel = new UILabel(new CGRect(.05 * parentView.Bounds.Width, .43 * selectionView.Bounds.Height, .9 * parentView.Bounds.Width, .08 * selectionView.Bounds.Height));
      //offlineLabel.Text = "Offline Users";
      //offlineLabel.TextAlignment = UITextAlignment.Center;
      //offlineLabel.AdjustsFontSizeToFitWidth = true;
      
      //offlineTable = new UITableView(new CGRect(.05 * parentView.Bounds.Width, .51 * selectionView.Bounds.Height, .9 * parentView.Bounds.Width, .35 * selectionView.Bounds.Height));
      //offlineTable.SeparatorStyle = UITableViewCellSeparatorStyle.None;
      //offlineTable.Layer.CornerRadius = 5f;
      //offlineTable.Layer.BorderWidth = 1f;
      //offlineTable.RegisterClassForCellReuse(typeof(RemoteAccessTableCell),"remoteAccessCell");

      //reloadOffline = new UIRefreshControl();
      //reloadOffline.ValueChanged += (sender, e) => {
      //  GetAccessList();
      //};

      //offlineTable.InsertSubview(reloadOffline,0);  
      //offlineTable.SendSubviewToBack(reloadOffline);
  
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
				Console.WriteLine("clicked to start remote");   
				await Task.Delay(TimeSpan.FromMilliseconds(1));
				var viewing = NSUserDefaults.StandardUserDefaults.StringForKey("viewedUser");

				if(!string.IsNullOrEmpty(viewing)){
					remoteMenuButton.Hidden = true;
					fullMenuButton.Hidden = false;
					onlineTable.UserInteractionEnabled = false;
					webServices.remoteViewing = true;
					
					await ion.setRemoteDeviceManager(); 
        	rootVC.setRemoteMenu();
					webServices.downloading = true;
					startDownloading();
        } else {
					var alert = UIAlertController.Create ("Unable to View", "User is not available. Please try again.", UIAlertControllerStyle.Alert);
					alert.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Cancel, null));
					rootVC.PresentViewController (alert, animated: true, completionHandler: null);
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
				
				fullMenuButton.Hidden = true;
				remoteMenuButton.Hidden = false;
				onlineTable.UserInteractionEnabled = true;
				webServices.remoteViewing = false;
				webServices.downloading = false;
				webServices.paused = null;
				await Task.Delay(TimeSpan.FromMilliseconds(1));
				
				NSUserDefaults.StandardUserDefaults.SetString("","viewedUser");
				selectedUser.Clear();
				onlineTable.ReloadData();
				
				await ion.setOriginalDeviceManager();
				rootVC.setMainMenu();
			};
			
			loadingUsers = new UIActivityIndicatorView(new CGRect(0, 0, parentView.Bounds.Width, parentView.Bounds.Height));
			loadingUsers.Alpha = .8f;
			loadingUsers.BackgroundColor = UIColor.Gray;
			loadingUsers.HidesWhenStopped = true;			
			
			selectionView.AddSubview(onlineLabel);
			//selectionView.AddSubview(offlineLabel);
			selectionView.AddSubview(remoteMenuButton);
			selectionView.AddSubview(fullMenuButton);
			selectionView.AddSubview(onlineTable);
			//selectionView.AddSubview(offlineTable);
			selectionView.AddSubview(loadingUsers);
			selectionView.BringSubviewToFront(loadingUsers);
			GetAccessList();
		}

		public async void GetAccessList(){
			loadingUsers.StartAnimating();
			await Task.Delay(TimeSpan.FromMilliseconds(1));
			onlineUsers = new List<accessData>();
			offlineUsers = new List<accessData>();
			var ID = KeychainAccess.ValueForKey("userID");
			var userName = KeychainAccess.ValueForKey("userDisplay");
			var userEmail = KeychainAccess.ValueForKey("userEmail");
	
			var feedback = await webServices.GetAccessList(ID);
			
			var window = UIApplication.SharedApplication.KeyWindow;
  		var rootVC = window.RootViewController as IONPrimaryScreenController;
  		
			if(feedback != null){
			
				var textResponse = await feedback.Content.ReadAsStringAsync();
				Console.WriteLine(textResponse);
				//parse the text string into a json object to be deserialized
				JObject response = JObject.Parse(textResponse);
				var success = response.GetValue("success");
	
				if(success.ToString() == "true"){
					var viewing = response.GetValue("viewing");
					onlineUsers.Add(new accessData(){displayName = userName, email = userEmail, id = Convert.ToInt32(ID), online = 1});
					foreach(var user in viewing){
						var deserializedToken = JsonConvert.DeserializeObject<accessData>(user.ToString());					
							onlineUsers.Add(deserializedToken);
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
			
			onlineTable.Source = new RemoteAccessTableSource(onlineUsers, selectedUser, webServices.webClient);
			onlineTable.ReloadData();

			//offlineTable.Source = new RemoteAccessTableSource(offlineUsers, null, webServices.webClient);
			//offlineTable.ReloadData();
			
			loadingUsers.StopAnimating();
      reloadOnline.EndRefreshing();
      //reloadOffline.EndRefreshing();
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
    
		public async void timeOutAlert(bool timeout){
				var window = UIApplication.SharedApplication.KeyWindow;
	  		var rootVC = window.RootViewController as IONPrimaryScreenController;
	  		
				SystemSound sound = new SystemSound(1005);
				var alert = UIAlertController.Create ("Viewing User", "Are you still viewing the selected user?", UIAlertControllerStyle.Alert);
				alert.AddAction (UIAlertAction.Create ("Yes", UIAlertActionStyle.Default, (action) => {
					webServices.downloading = true;
					sound.Close();
					webServices.StartLayoutDownload();
				}));
				alert.AddAction (UIAlertAction.Create ("No", UIAlertActionStyle.Cancel, (action) => {sound.Close();}));
				rootVC.PresentViewController (alert, animated: true, completionHandler: null);
				AbsentRemoteTurnoff(alert);
				
				sound.PlaySystemSound();
				await Task.Delay(TimeSpan.FromSeconds(2));
				sound.Close();
		}
		public void startDownloading(){
			webServices.StartLayoutDownload();
			webServices.paused += pauseRemote;
		}
		public void pauseRemote(bool paused){
			if(paused == false){
				Console.WriteLine("Should be starting the download now");
				webServices.StartLayoutDownload();
			}
		}
	
		public async void AbsentRemoteTurnoff(UIAlertController alert){
			await Task.Delay(TimeSpan.FromSeconds(15));
			
			alert.DismissViewController(false,null);
			if(!webServices.remoteViewing){
				Console.WriteLine("dismissed alert and user didn't choose to continue");
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
	}
}
