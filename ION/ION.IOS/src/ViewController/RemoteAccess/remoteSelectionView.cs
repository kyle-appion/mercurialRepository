using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Foundation;
using CoreGraphics;
using UIKit;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

using ION.Core.App;
using ION.IOS.App;

namespace ION.IOS.ViewController.RemoteAccess {
	public class remoteSelectionView {
		public UIView selectionView;
		public UIButton remoteMenu;
		public UIButton fullMenu;
		public UITableView accessTable;
		public List<accessData> retrievedUsers;
		public ObservableCollection<int> selectedUser;
		public UIActivityIndicatorView loadingUsers;
		public const string accessUrl = "http://ec2-54-205-38-19.compute-1.amazonaws.com/App/retrieveAccess.php";
		
		public remoteSelectionView(UIView parentView, IION ion) {
			// Perform any additional setup after loading the view, typically from a nib.
			selectedUser = new ObservableCollection<int>();
			selectedUser.CollectionChanged += checkForSelected;
			selectionView = new UIView(new CGRect(0,40,parentView.Bounds.Width, parentView.Bounds.Height - 40));
			selectionView.BackgroundColor = UIColor.White;
			
			var window = UIApplication.SharedApplication.KeyWindow;
      var rootVC = window.RootViewController as IONPrimaryScreenController;
      
      accessTable = new UITableView(new CGRect(.05 * parentView.Bounds.Width, 50, .9 * parentView.Bounds.Width, .7 * selectionView.Bounds.Height));
      accessTable.SeparatorStyle = UITableViewCellSeparatorStyle.None;
      accessTable.Layer.CornerRadius = 5f;
      accessTable.Layer.BorderWidth = 1f;
      accessTable.RegisterClassForCellReuse(typeof(RemoteAccessTableCell),"remoteAccessCell");
  
			remoteMenu = new UIButton(new CGRect(.05 * selectionView.Bounds.Width, .8 * selectionView.Bounds.Height, .4 * selectionView.Bounds.Width, .1 * selectionView.Bounds.Height));
			remoteMenu.BackgroundColor = UIColor.FromRGB(255, 215, 101);
			remoteMenu.SetTitle("Remote Mode", UIControlState.Normal);
			remoteMenu.SetTitleColor(UIColor.Black, UIControlState.Normal);
			remoteMenu.Enabled = false;
			remoteMenu.Alpha = .6f;
			remoteMenu.Layer.CornerRadius = 5f;
			remoteMenu.Layer.BorderWidth = 1f;
			remoteMenu.TouchDown += (sender, e) => {remoteMenu.BackgroundColor = UIColor.Blue;};
			remoteMenu.TouchUpOutside += (sender, e) => {remoteMenu.BackgroundColor = UIColor.FromRGB(255, 215, 101);};
			remoteMenu.TouchUpInside += async (sender, e) => {
				remoteMenu.BackgroundColor = UIColor.FromRGB(255, 215, 101);
				remoteMenu.Hidden = true;
				fullMenu.Hidden = false;
				var viewing = NSUserDefaults.StandardUserDefaults.StringForKey("viewedUser");

				if(!string.IsNullOrEmpty(viewing)){
					Console.WriteLine("pressed remote menu to start viewing user with id " + viewing);				
					await ion.setRemoteDeviceManager();
        	rootVC.setRemoteMenu();
        } else {        	
					var alert = UIAlertController.Create ("Unable to View", "User is not available. Please try again.", UIAlertControllerStyle.Alert);
					alert.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Cancel, null));
					rootVC.PresentViewController (alert, animated: true, completionHandler: null);
				}
			};
			
			fullMenu = new UIButton(new CGRect(.55 * selectionView.Bounds.Width, .8 * selectionView.Bounds.Height, .4 * selectionView.Bounds.Width, .1 * selectionView.Bounds.Height));
			fullMenu.Layer.CornerRadius = 5f;
			fullMenu.Layer.BorderWidth = 1f;
			fullMenu.BackgroundColor = UIColor.FromRGB(255, 215, 101);
			fullMenu.SetTitle("Normal Mode",UIControlState.Normal);
			fullMenu.SetTitleColor(UIColor.Black, UIControlState.Normal);
			fullMenu.Hidden = true;
			fullMenu.TouchDown += (sender, e) => {fullMenu.BackgroundColor = UIColor.Blue;};
			fullMenu.TouchUpOutside += (sender, e) => {fullMenu.BackgroundColor = UIColor.FromRGB(255, 215, 101);};			
			fullMenu.TouchUpInside += async (sender, e) => {
				fullMenu.BackgroundColor = UIColor.FromRGB(255, 215, 101);
				fullMenu.Hidden = true;
				remoteMenu.Hidden = false;
				Console.WriteLine("original menu pressed");
				await ion.setOriginalDeviceManager();
				rootVC.setMainMenu();
			};
			
			loadingUsers = new UIActivityIndicatorView(new CGRect(.05 * parentView.Bounds.Width, 50, .9 * parentView.Bounds.Width, .7 * selectionView.Bounds.Height));
			loadingUsers.Alpha = .8f;
			loadingUsers.BackgroundColor = UIColor.Gray;
			loadingUsers.HidesWhenStopped = true;
			
			selectionView.AddSubview(remoteMenu);
			selectionView.AddSubview(fullMenu);
			selectionView.AddSubview(accessTable);
			selectionView.AddSubview(loadingUsers);
			selectionView.BringSubviewToFront(loadingUsers);
		}

		public async void GetAccessList(){
			loadingUsers.StartAnimating();
			await Task.Delay(TimeSpan.FromMilliseconds(1));
      WebClient wc = new WebClient();
      wc.Proxy = null;
			retrievedUsers = new List<accessData>();
       
			//Create the data package to send for the post request
			//Key value pair for post variable check
			var data = new System.Collections.Specialized.NameValueCollection();
			data.Add("retrieveAccess","users");
			data.Add("userID","1");
 
			//initiate the post request and get the request result in a byte array 
			byte[] result = wc.UploadValues(accessUrl,data);
			
			//get the string conversion for the byte array
			var textResponse = Encoding.UTF8.GetString(result);
			Console.WriteLine(textResponse);
			//parse the text string into a json object to be deserialized
			JObject response = JObject.Parse(textResponse);
			var retrieved = response.GetValue("success");
			if(retrieved.ToString() == "true"){
				var users = response.GetValue("users");			
				foreach(var user in users){
					var deserializedToken = JsonConvert.DeserializeObject<accessData>(user.ToString());
					retrievedUsers.Add(deserializedToken);
				}
				accessTable.Source = new RemoteAccessTableSource(retrievedUsers, selectedUser);
				accessTable.ReloadData();
			} else {
					var window = UIApplication.SharedApplication.KeyWindow;
      		var rootVC = window.RootViewController as IONPrimaryScreenController;
					var errorMessage = response.GetValue("message");
					
					var alert = UIAlertController.Create ("User List", errorMessage.ToString(), UIAlertControllerStyle.Alert);
					alert.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Cancel, null));
					rootVC.PresentViewController (alert, animated: true, completionHandler: null);
			}
			loadingUsers.StopAnimating();
		}
		
    public void checkForSelected(object sender, EventArgs e){
        if(selectedUser.Count > 0){
          remoteMenu.Enabled = true;
          remoteMenu.Alpha = 1f;
        } else {
          remoteMenu.Enabled = false;
          remoteMenu.Alpha = .6f;
        }
    }
		
	}
		[Preserve(AllMembers = true)]
		public class accessData{
			public accessData(){}
			[JsonProperty("display")]
			public string displayName { get; set; }
			[JsonProperty("id")]
			public int id { get; set; }
		}
}

