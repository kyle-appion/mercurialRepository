
using System;
using CoreGraphics;
using UIKit;
using Foundation;
using ION.IOS.App;
using ION.Core.Net;
using ION.Core.App;

namespace ION.IOS.ViewController.RemoteAccess {

	public class RemoteAccessTableCell : UITableViewCell{
	
		public UILabel header;
		public UILabel status;
		IosION ion;
		WebPayload webServices;		
		
		public RemoteAccessTableCell(IntPtr handle) {
		} 
		
		public RemoteAccessTableCell() {
		
		} 		
		public void makeCellData(double cellWidth,double cellHeight, accessData user){
			ion = AppState.context as IosION;
			webServices = ion.webServices;
	
			header = new UILabel(new CGRect(0,0, .6 * cellWidth, cellHeight));
			header.TextAlignment = UITextAlignment.Left;
			header.AdjustsFontSizeToFitWidth = true;
			
			status = new UILabel(new CGRect(.6 * cellWidth,0, .3 * cellWidth, cellHeight));
			status.TextAlignment = UITextAlignment.Left;
			status.TextColor = UIColor.FromRGB(49, 111, 18);
			status.AdjustsFontSizeToFitWidth = true;
			
			if(string.IsNullOrEmpty(user.displayName)){
				header.Text = " " + user.email;
			} else {
				header.Text = " " + user.displayName;
			}
			
			if(Convert.ToInt32(KeychainAccess.ValueForKey("userID")) == user.id){
				header.Text = " Your Feed";
				if(webServices.uploading){
					status.TextColor = UIColor.Red;
					status.Text = "Uploading";
				} else {
					status.Text = "Viewable";
				}
			} else {
				if(user.online == 1){
					status.Text += "Viewable";
				} else {
					status.Text += "Offline";
					status.TextColor = UIColor.Red;
				}
			}
	
			this.AddSubview(header);
			this.AddSubview(status);
		}
		
	}
}
