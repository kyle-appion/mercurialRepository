using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CoreGraphics;
using UIKit;
using Newtonsoft.Json;

using ION.Core.App;
using ION.IOS.ViewController.WebServices;

namespace ION.IOS.ViewController.RemoteAccess {
	public class remoteSelectionView {
		public UIView selectionView;
		public UIButton remoteMenuButton;
		public UIButton fullMenuButton;
		public UILabel onlineLabel;
		public UILabel offlineLabel;
		public UITableView onlineTable;
		public UITableView offlineTable;
		public List<accessData> onlineUsers;
		public List<accessData> offlineUsers;
		public ObservableCollection<int> selectedUser;
		public UIActivityIndicatorView loadingUsers;
		public IION ion;
		public SessionPayload webActivities;
		public bool isViewing = false;
		
		public remoteSelectionView(UIView parentView, IION ion, SessionPayload webServices) {
			this.ion = ion;
			webActivities = webServices;
			// Perform any additional setup after loading the view, typically from a nib.
			selectedUser = new ObservableCollection<int>();
			selectedUser.CollectionChanged += checkForSelected;
			selectionView = new UIView(new CGRect(0,40,parentView.Bounds.Width, parentView.Bounds.Height - 40));
			selectionView.BackgroundColor = UIColor.White;
			
			var window = UIApplication.SharedApplication.KeyWindow;
      var rootVC = window.RootViewController as IONPrimaryScreenController;
      
      onlineLabel = new UILabel(new CGRect(.05 * parentView.Bounds.Width, 0, .9 * parentView.Bounds.Width, .08 * selectionView.Bounds.Height));
      onlineLabel.Text = "Online Users";
      onlineLabel.TextAlignment = UITextAlignment.Center;
      onlineLabel.AdjustsFontSizeToFitWidth = true;
      
      onlineTable = new UITableView(new CGRect(.05 * parentView.Bounds.Width, .08 * selectionView.Bounds.Height, .9 * parentView.Bounds.Width, .35 * selectionView.Bounds.Height));
      onlineTable.SeparatorStyle = UITableViewCellSeparatorStyle.None;
      onlineTable.Layer.CornerRadius = 5f;
      onlineTable.Layer.BorderWidth = 1f;
      onlineTable.RegisterClassForCellReuse(typeof(RemoteAccessTableCell),"remoteAccessCell");

      offlineLabel = new UILabel(new CGRect(.05 * parentView.Bounds.Width, .43 * selectionView.Bounds.Height, .9 * parentView.Bounds.Width, .08 * selectionView.Bounds.Height));
      offlineLabel.Text = "Offline Users";
      offlineLabel.TextAlignment = UITextAlignment.Center;
      offlineLabel.AdjustsFontSizeToFitWidth = true;
      
      offlineTable = new UITableView(new CGRect(.05 * parentView.Bounds.Width, .51 * selectionView.Bounds.Height, .9 * parentView.Bounds.Width, .35 * selectionView.Bounds.Height));
      offlineTable.SeparatorStyle = UITableViewCellSeparatorStyle.None;
      offlineTable.Layer.CornerRadius = 5f;
      offlineTable.Layer.BorderWidth = 1f;
      offlineTable.RegisterClassForCellReuse(typeof(RemoteAccessTableCell),"remoteAccessCell");
  
			remoteMenuButton = new UIButton(new CGRect(.3 * selectionView.Bounds.Width, .86 * selectionView.Bounds.Height, .4 * selectionView.Bounds.Width, .1 * selectionView.Bounds.Height));
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
				remoteMenuButton.Hidden = true;
				fullMenuButton.Hidden = false;

				await Task.Delay(TimeSpan.FromMilliseconds(1));
				//var viewing = NSUserDefaults.StandardUserDefaults.StringForKey("viewedUser");

				//if(!string.IsNullOrEmpty(viewing)){				
				//	await ion.setRemoteDeviceManager();
    //    	rootVC.setRemoteMenu();
    //    } else {        	
				//	var alert = UIAlertController.Create ("Unable to View", "User is not available. Please try again.", UIAlertControllerStyle.Alert);
				//	alert.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Cancel, null));
				//	rootVC.PresentViewController (alert, animated: true, completionHandler: null);
				//}
			};
			
			fullMenuButton = new UIButton(new CGRect(.3 * selectionView.Bounds.Width, .86 * selectionView.Bounds.Height, .4 * selectionView.Bounds.Width, .1 * selectionView.Bounds.Height));
			fullMenuButton.Layer.CornerRadius = 5f;
			fullMenuButton.Layer.BorderWidth = 1f;
			fullMenuButton.BackgroundColor = UIColor.FromRGB(255, 215, 101);
			fullMenuButton.SetTitle("Normal Mode",UIControlState.Normal);
			fullMenuButton.SetTitleColor(UIColor.Black, UIControlState.Normal);
			fullMenuButton.Hidden = true;
			fullMenuButton.TouchDown += (sender, e) => {fullMenuButton.BackgroundColor = UIColor.Blue;};
			fullMenuButton.TouchUpOutside += (sender, e) => {fullMenuButton.BackgroundColor = UIColor.FromRGB(255, 215, 101);};			
			fullMenuButton.TouchUpInside += async (sender, e) => {			
				fullMenuButton.BackgroundColor = UIColor.FromRGB(255, 215, 101);
				fullMenuButton.Hidden = true;
				remoteMenuButton.Hidden = false;

				await Task.Delay(TimeSpan.FromMilliseconds(1));
				//NSUserDefaults.StandardUserDefaults.SetString("","viewedUser");
				//await ion.setOriginalDeviceManager();
				//rootVC.setMainMenu();
			};
			
			loadingUsers = new UIActivityIndicatorView(new CGRect(0, 0, parentView.Bounds.Width, parentView.Bounds.Height));
			loadingUsers.Alpha = .8f;
			loadingUsers.BackgroundColor = UIColor.Gray;
			loadingUsers.HidesWhenStopped = true;			
			
			selectionView.AddSubview(onlineLabel);
			selectionView.AddSubview(offlineLabel);
			selectionView.AddSubview(remoteMenuButton);
			selectionView.AddSubview(fullMenuButton);
			selectionView.AddSubview(onlineTable);
			selectionView.AddSubview(offlineTable);
			selectionView.AddSubview(loadingUsers);
			selectionView.BringSubviewToFront(loadingUsers);
			GetAccessList();
		}

		public async void GetAccessList(){
			loadingUsers.StartAnimating();
			await Task.Delay(TimeSpan.FromMilliseconds(1));
			onlineUsers = new List<accessData>();
			offlineUsers = new List<accessData>();

			await webActivities.GetAccessList(onlineUsers, offlineUsers);
			
			onlineTable.Source = new RemoteAccessTableSource(onlineUsers, selectedUser, webActivities.webClient);
			onlineTable.ReloadData();

			offlineTable.Source = new RemoteAccessTableSource(offlineUsers, null, webActivities.webClient);
			offlineTable.ReloadData();
			
			loadingUsers.StopAnimating();
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
	}
	[WebServices.Preserve(AllMembers = true)]
	public class accessData{
		public accessData(){}
		[JsonProperty("display")]
		public string displayName { get; set; }
		[JsonProperty("id")]
		public int id { get; set; }
		[JsonProperty("online")]
		public int online { get; set; }
	}
}

