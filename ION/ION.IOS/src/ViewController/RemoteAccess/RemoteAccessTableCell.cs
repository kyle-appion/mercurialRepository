
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
		public UILabel deviceInfo;
		public UILabel status;
		private IosION ion;
		
		public RemoteAccessTableCell(IntPtr handle) {
		} 
		
		public RemoteAccessTableCell() {
		
		} 		
		public void makeCellData(double cellWidth,double cellHeight, accessData user){
			ion = AppState.context as IosION;
			
			var currentlyViewing = NSUserDefaults.StandardUserDefaults.StringForKey("viewedUser");
	
			header = new UILabel(new CGRect(0,0, .5 * cellWidth, .5 * cellHeight));
			header.TextAlignment = UITextAlignment.Left;
			header.AdjustsFontSizeToFitWidth = true;

			deviceInfo = new UILabel(new CGRect(0,.5 * cellHeight, .5 * cellWidth, .5 * cellHeight));
			deviceInfo.Font = UIFont.ItalicSystemFontOfSize(18f);
			deviceInfo.Text = user.deviceName;
			deviceInfo.AdjustsFontSizeToFitWidth = true;
			
			status = new UILabel(new CGRect(.5 * cellWidth,0, .3 * cellWidth, cellHeight));
			status.TextAlignment = UITextAlignment.Left;
			status.TextColor = UIColor.FromRGB(49, 111, 18);
			status.AdjustsFontSizeToFitWidth = true;
			
			header.Text = " " + user.displayName;
			
			if(Convert.ToInt32(KeychainAccess.ValueForKey("userID")) == user.id){
				header.Text = " Your Account";
				if(ion.webServices.uploading && UIDevice.CurrentDevice.IdentifierForVendor.ToString() == user.deviceID){   
					status.TextColor = UIColor.Red;
					status.Text = "Uploading";
				} else {
					status.Text = "Viewable";
				}
			} else if (ion.webServices.remoteViewing && !String.IsNullOrEmpty(currentlyViewing) && currentlyViewing == user.id.ToString()){
				status.Text += "Viewing";
				status.TextColor = UIColor.Blue;
			} else {
				if(user.online == 1){
					status.Text += "Viewable";
				}
			}
	
			this.AddSubview(header);
			this.AddSubview(deviceInfo);
			this.AddSubview(status);
		}
		
	}
}
