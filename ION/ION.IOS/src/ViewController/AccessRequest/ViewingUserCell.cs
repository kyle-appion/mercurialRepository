
using System;
using CoreGraphics;
using Foundation;
using UIKit;

namespace ION.IOS.ViewController.AccessRequest {

	public class ViewingUserCell : UITableViewCell  {
		public UILabel header;
	
		protected ViewingUserCell(IntPtr handle)  {
			
		}
		public void makeCellData(accessUserData user, nfloat cellHeight, bool small = false){
			header = new UILabel(new CGRect(0,0,this.Bounds.Width, cellHeight));
			if(small == false){
				header.Text ="("+user.displayName+") " + user.userEmail;
			} else {
				header.Text = user.displayName;
			}
			
			header.AdjustsFontSizeToFitWidth = true;
			header.TextAlignment = UITextAlignment.Center;
			
			this.AddSubview(header);
		}		
	}
}
