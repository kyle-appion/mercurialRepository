using System;
using System.Collections.Generic;
using UIKit;
using Foundation;
using CoreGraphics;
using System.Net;
using System.Threading.Tasks;
using ION.IOS.App;
using System.Text;
using Newtonsoft.Json.Linq;

namespace ION.IOS.ViewController.AccessRequest
{
	public class AccessViewingTableSource : UITableViewSource
	{


		public AccessViewingTableSource ()
		{
		}

		//string cellIdentifier;
		List<accessUserData> tableItems;
		public nfloat cellHeight;
		public const string deleteRequestUrl = "http://portal.appioninc.com/App/deleteAccess.php";

		public AccessViewingTableSource (List<accessUserData> items,double cHeight){
			tableItems = items;
			cellHeight = (nfloat)cHeight;
		}

		public override nint RowsInSection (UITableView tableview, nint section)
		{
      return tableItems.Count;
		}

		public override nint NumberOfSections (UITableView tableView)
		{
			return 1;
		} 

		public override UIView GetViewForFooter (UITableView tableView, nint section)
		{
			return new UIView(new CGRect(0,0,0,0));
		}

		public override UIView GetViewForHeader(UITableView tableView, nint section) {
			return new UIView(new CGRect(0,0,0,0));
		}

		public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath) {
			return cellHeight;
		}

		public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath) {
			return true;
		}

		public async override void CommitEditingStyle (UITableView tableView, UITableViewCellEditingStyle editingStyle, Foundation.NSIndexPath indexPath)
		{
			switch (editingStyle) {
			case UITableViewCellEditingStyle.Delete:
				// delete the request from the database
				await DeleteUserRequests(tableView, indexPath);   
				// remove the item from the underlying data source
				tableItems.RemoveAt(indexPath.Row);

				// delete the row from the table
				tableView.DeleteRows(new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Fade);				
				
				break;
			case UITableViewCellEditingStyle.None:
				Console.WriteLine ("CommitEditingStyle:None called");
				break;
			}
		}    

		public override UITableViewCell GetCell (UITableView tableView, Foundation.NSIndexPath indexPath)
		{

			var cell = tableView.DequeueReusableCell("viewingCell") as ViewingUserCell;
			if (cell == null) {
				cell = new UITableViewCell(UITableViewCellStyle.Default, "viewingCell") as ViewingUserCell;
			}
			cell.Layer.BorderWidth = 1f;
			
      cell.makeCellData (tableItems [indexPath.Row], cellHeight);

			return cell;
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
				Console.WriteLine("request id: " + tableItems[indexPath.Row] + " access code: " + tableItems[indexPath.Row].id);

		}
		
		public async Task DeleteUserRequests(UITableView tableView, Foundation.NSIndexPath indexPath){
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
			try{
				//initiate the post request and get the request result in a byte array 
				byte[] result = wc.UploadValues(deleteRequestUrl,data);   
				
				//get the string conversion for the byte array
				var textResponse = Encoding.UTF8.GetString(result);
				Console.WriteLine(textResponse);
				//parse the text string into a json object to be deserialized
				JObject response = JObject.Parse(textResponse);
				var success = response.GetValue("success");
				
				var window = UIApplication.SharedApplication.KeyWindow;
	  		var rootVC = window.RootViewController as IONPrimaryScreenController;
				var responseMessage = response.GetValue("message");
				
					var alert = UIAlertController.Create ("Delete Request", responseMessage.ToString(), UIAlertControllerStyle.Alert);
					alert.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Cancel, null));
					rootVC.PresentViewController (alert, animated: true, completionHandler: null);
				if(success.ToString() == "false"){
					tableItems.Add(new accessUserData(){displayName = tableItems[indexPath.Row].displayName, id = tableItems[indexPath.Row].id, userEmail = tableItems[indexPath.Row].userEmail});
					tableView.ReloadData();
				}
			} catch (Exception exception){
				Console.WriteLine("Exception: " + exception);
				var window = UIApplication.SharedApplication.KeyWindow;
    		var rootVC = window.RootViewController as IONPrimaryScreenController;
				
				var alert = UIAlertController.Create ("Delete Request", "There was no response. Please try again.", UIAlertControllerStyle.Alert);
				alert.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Cancel, null));
				rootVC.PresentViewController (alert, animated: true, completionHandler: null);
			}
		}
	}
}
