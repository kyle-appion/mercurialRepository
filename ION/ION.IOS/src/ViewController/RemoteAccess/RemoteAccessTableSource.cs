using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Foundation;
using CoreGraphics;
using UIKit;
using System.Threading.Tasks;
using ION.IOS.App;
using System.Net;
using System.Text;
using Newtonsoft.Json.Linq;

namespace ION.IOS.ViewController.RemoteAccess {

	public class RemoteAccessTableSource : UITableViewSource {
	  
		List<accessData> tableItems;
    nfloat cellHeight;
    ObservableCollection<int> selectedUser;
    public const string deleteUserUrl = "http://ec2-54-205-38-19.compute-1.amazonaws.com/App/deleteAccess.php";
    
		public RemoteAccessTableSource(List<accessData> accessItems, ObservableCollection<int> selected) {
			tableItems = accessItems;
			selectedUser = selected;
		}
		
		public override nint NumberOfSections(UITableView tableView) {
      return 1;
    }

    // Overridden from UITableViewSource
    public override nint RowsInSection(UITableView tableview, nint section) {
      return tableItems.Count;
    }

    // Overriden from UITableViewSource
    public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath) {
    	//Console.WriteLine("Cell height will be " + (tableItems[indexPath.Row].description.Count * 40) + " for update " + tableItems[indexPath.Row].title);
      //return tableItems[indexPath.Row].description.Count * 40;
      return 50;
    }
    // Overriden from UITableViewSource
    public override UIView GetViewForHeader(UITableView tableView, nint section) {
      return new UIView(new CGRect(0,0,0,0));
    }
    // Overriden from UITableViewSource
    public override UIView GetViewForFooter (UITableView tableView, nint section)
    {
      return new UIView(new CGRect(0,0,0,0));
    }

    public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath) {
      var cell = tableView.DequeueReusableCell("remoteAccessCell") as RemoteAccessTableCell;

      if (cell == null){
      	Console.WriteLine("Did not deque a cell");
        cell = new UITableViewCell(UITableViewCellStyle.Default, "remoteAccessCell") as RemoteAccessTableCell;
      }
    	cellHeight = 50;
    	
    	cell.Layer.BorderWidth = 1f;
      cell.makeCellData(cellHeight, tableItems[indexPath.Row]);
      cell.SelectionStyle = UITableViewCellSelectionStyle.None;
      
      if(selectedUser.Contains(tableItems[indexPath.Row].id)){
        cell.Accessory = UITableViewCellAccessory.Checkmark;
        //cell.BackgroundColor = UIColor.LightGray;
      }
      return cell;
    }

    public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath) {
      return true;
    }

    public async override void CommitEditingStyle (UITableView tableView, UITableViewCellEditingStyle editingStyle, Foundation.NSIndexPath indexPath)
    {
      switch (editingStyle) {
        case UITableViewCellEditingStyle.Delete:
					await DeleteUserAccess(tableView, indexPath);
          // remove the item from the underlying data source
          tableItems.RemoveAt(indexPath.Row);
          // delete the row from the table
          tableView.DeleteRows(new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Fade);
          break;
        case UITableViewCellEditingStyle.None:
          Console.WriteLine ("CommitEditingStyle:None called");
          break;
        case UITableViewCellEditingStyle.Insert:
          Console.WriteLine("wants to insert");
          break;
      } 
    }

    public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
    {
			Console.WriteLine("Chose id " + tableItems[indexPath.Row].id.ToString());
			if(selectedUser.Contains(tableItems[indexPath.Row].id)){
				selectedUser.Clear();
				NSUserDefaults.StandardUserDefaults.SetString("","viewedUser");
			} else {
				selectedUser.Clear();
				selectedUser.Add(tableItems[indexPath.Row].id);
				NSUserDefaults.StandardUserDefaults.SetString(tableItems[indexPath.Row].id.ToString(),"viewedUser");
			}
			
			tableView.ReloadData();
    }
    
		public async Task DeleteUserAccess(UITableView tableView, Foundation.NSIndexPath indexPath){
			await Task.Delay(TimeSpan.FromMilliseconds(1));
			WebClient wc = new WebClient();
			wc.Proxy = null;
			
			var userID = KeychainAccess.ValueForKey("userID");
			//Create the data package to send for the post request
			//Key value pair for post variable check
			var data = new System.Collections.Specialized.NameValueCollection();
			data.Add("deleteUserAccess","true");
			data.Add("userID", userID);
			data.Add("accessID", tableItems[indexPath.Row].id.ToString());

			//initiate the post request and get the request result in a byte array 
			byte[] result = wc.UploadValues(deleteUserUrl,data);
			
			//get the string conversion for the byte array
			var textResponse = Encoding.UTF8.GetString(result);
			Console.WriteLine(textResponse);
			//parse the text string into a json object to be deserialized
			JObject response = JObject.Parse(textResponse);
			var success = response.GetValue("success");
			
			var window = UIApplication.SharedApplication.KeyWindow;
  		var rootVC = window.RootViewController as IONPrimaryScreenController;
			var responseMessage = response.GetValue("message");
			
			if(success.ToString() == "true"){
				var alert = UIAlertController.Create ("Delete User", responseMessage.ToString(), UIAlertControllerStyle.Alert);
				alert.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Cancel, null));
				rootVC.PresentViewController (alert, animated: true, completionHandler: null);
			} else {					
				var alert = UIAlertController.Create ("Delete User", responseMessage.ToString(), UIAlertControllerStyle.Alert);
				alert.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Cancel, null));
				rootVC.PresentViewController (alert, animated: true, completionHandler: null);
				tableItems.Add(new accessData(){displayName = tableItems[indexPath.Row].displayName, id = tableItems[indexPath.Row].id});
				tableView.ReloadData();
			}
		} 
	}
}

