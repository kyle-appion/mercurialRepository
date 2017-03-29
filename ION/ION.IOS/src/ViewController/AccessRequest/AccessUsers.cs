using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CoreGraphics;
using ION.Core.App;
using ION.Core.Net;
using ION.IOS.App;
using ION.IOS.ViewController.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UIKit;

namespace ION.IOS.ViewController.AccessRequest {

	public class AccessUsers {
		public UIView accessView;
		public UILabel allowedHeader;
		public UILabel grantedHeader;
		public UITableView viewingTable;
		public UITableView allowingTable;
    public UIRefreshControl followingRefresh;
    public UIRefreshControl followerRefresh;
		public WebPayload webServices;
		public IosION ion;
		public UIActivityIndicatorView loadingRequests;
		public List<accessUserData> followingUsers;
		public List<accessUserData> followerUsers;
	
		public AccessUsers(UIView parentView) {
			ion = AppState.context as IosION;
			webServices = ion.webServices;
		
			accessView = new UIView(new CGRect(0,0,parentView.Bounds.Width, parentView.Bounds.Height));
			accessView.BackgroundColor = UIColor.White;

			allowedHeader = new UILabel(new CGRect(.05 * parentView.Bounds.Width,0,.9 * parentView.Bounds.Width, .05 * parentView.Bounds.Height));
			allowedHeader.AdjustsFontSizeToFitWidth = true;
			allowedHeader.Font = UIFont.BoldSystemFontOfSize(20);
			allowedHeader.TextAlignment = UITextAlignment.Center;
			allowedHeader.BackgroundColor = UIColor.Black;
			allowedHeader.TextColor = UIColor.FromRGB(255, 215, 101);
			allowedHeader.Text = "Following";
			
			viewingTable = new UITableView(new CGRect(.05 * parentView.Bounds.Width, .05 * parentView.Bounds.Height, .9 * parentView.Bounds.Width, .4 * parentView.Bounds.Height));
			viewingTable.Layer.BorderWidth = 1f;
			viewingTable.SeparatorStyle = UITableViewCellSeparatorStyle.None;
      viewingTable.RegisterClassForCellReuse(typeof(ViewingUserCell), "viewingCell");
			
			grantedHeader = new UILabel(new CGRect(.05 * parentView.Bounds.Width,.5 * parentView.Bounds.Height,.9 * parentView.Bounds.Width, .05 * parentView.Bounds.Height));
			grantedHeader.Font = UIFont.BoldSystemFontOfSize(20);
			grantedHeader.AdjustsFontSizeToFitWidth = true;
			grantedHeader.TextAlignment = UITextAlignment.Center;
			grantedHeader.BackgroundColor = UIColor.Black;
			grantedHeader.TextColor = UIColor.FromRGB(255, 215, 101);
			grantedHeader.Text = "Followers";
			
			allowingTable = new UITableView(new CGRect(.05 * parentView.Bounds.Width, .55 * parentView.Bounds.Height, .9 * parentView.Bounds.Width, .4 * parentView.Bounds.Height));
			allowingTable.Layer.BorderWidth = 1f;
			allowingTable.SeparatorStyle = UITableViewCellSeparatorStyle.None;
      allowingTable.RegisterClassForCellReuse(typeof(AllowingUserCell), "allowingCell");

      followingRefresh = new UIRefreshControl();
      followingRefresh.ValueChanged += (sender, e) => {
        getAllUserAccess(parentView);
      };
      
      followerRefresh = new UIRefreshControl();
      followerRefresh.ValueChanged += (sender, e) => {
        getAllUserAccess(parentView);
      };

      viewingTable.InsertSubview(followingRefresh,0);
      viewingTable.SendSubviewToBack(followingRefresh);

      allowingTable.InsertSubview(followerRefresh,0);
      allowingTable.SendSubviewToBack(followerRefresh);
      
			loadingRequests = new UIActivityIndicatorView(new CGRect(.05 * parentView.Bounds.Width, 0, .9 * parentView.Bounds.Width, .95 * parentView.Bounds.Height));
			loadingRequests.Alpha = .8f;
			loadingRequests.BackgroundColor = UIColor.Gray;
			loadingRequests.HidesWhenStopped = true; 
     
			accessView.AddSubview(loadingRequests);   			
			accessView.AddSubview(allowedHeader);
			accessView.AddSubview(viewingTable);
			accessView.AddSubview(grantedHeader);
			accessView.AddSubview(allowingTable);
			accessView.BringSubviewToFront(loadingRequests);
			getAllUserAccess(parentView);
		}
		
			
	    /// <summary>
    /// Post to the server and get the list of open requests for the user
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="e">E.</param>
		public async void getAllUserAccess(UIView parentView){
			
			loadingRequests.StartAnimating();
			
			await Task.Delay(TimeSpan.FromMilliseconds(1));

			followingUsers = new List<accessUserData>();

			followerUsers = new List<accessUserData>();
			
			var window = UIApplication.SharedApplication.KeyWindow;
  		var rootVC = window.RootViewController as IONPrimaryScreenController;
			var ID = KeychainAccess.ValueForKey("userID");
			
			var feedback = await webServices.GetAccessList(ID);
			
			if(feedback != null){
				var textResponse = await feedback.Content.ReadAsStringAsync();
				Console.WriteLine(textResponse);
				//parse the text string into a json object to be deserialized
				JObject response = JObject.Parse(textResponse);
				var success = response.GetValue("success");
	
				if(success.ToString() == "true"){
					var viewing = response.GetValue("viewing");
					foreach(var user in viewing){
						var deserializedToken = JsonConvert.DeserializeObject<accessUserData>(user.ToString());
						followingUsers.Add(deserializedToken);
					}
					
					var granting = response.GetValue("allowing");
					
					foreach(var user in granting){
						var deserializedToken = JsonConvert.DeserializeObject<accessUserData>(user.ToString());
						followerUsers.Add(deserializedToken);
					}
					viewingTable.Source = new AccessViewingTableSource(followingUsers, .1 * accessView.Bounds.Height);
					viewingTable.ReloadData();
					allowingTable.Source = new AccessAllowingTableSource(followerUsers, .1 * accessView.Bounds.Height);
					allowingTable.ReloadData();
					
				}  else {
					var errorMessage = response.GetValue("message").ToString();
					
					var alert = UIAlertController.Create ("Viewer Access", errorMessage, UIAlertControllerStyle.Alert);
					alert.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Cancel, null));
					rootVC.PresentViewController (alert, animated: true, completionHandler: null);
				}
			} else {
				var alert = UIAlertController.Create ("Viewer Access", "There was no response. Please try again.", UIAlertControllerStyle.Alert);
				alert.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Cancel, null));
				rootVC.PresentViewController (alert, animated: true, completionHandler: null);
			}
			followingRefresh.EndRefreshing();
			followerRefresh.EndRefreshing();
			loadingRequests.StopAnimating();
		}
	}

	
	[Foundation.Preserve(AllMembers = true)]
	public class accessUserData{
		public accessUserData(){}
		[JsonProperty("display")]
		public string userEmail { get; set; }
		[JsonProperty("email")]
		public string displayName { get; set; }		
		[JsonProperty("ID")]
		public int id { get; set; }
	}	
}
