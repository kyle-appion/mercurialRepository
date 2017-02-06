
using System;
using CoreGraphics;
using UIKit;
using Foundation;
using ION.IOS.App;

namespace ION.IOS.ViewController.RemoteAccess {

	public class RemoteAccessTableCell : UITableViewCell{
	
		public UILabel header;
		
		public RemoteAccessTableCell(IntPtr handle) {
		} 
		
		public RemoteAccessTableCell() {
		
		} 		
		public void makeCellData(double cellWidth,double cellHeight, accessData user){
			header = new UILabel(new CGRect(0,0,cellWidth, cellHeight));
			header.TextAlignment = UITextAlignment.Center;
			
			if(string.IsNullOrEmpty(user.displayName)){
				header.Text = user.email;
			} else {
				header.Text = user.displayName;
			}
			
			if(Convert.ToInt32(KeychainAccess.ValueForKey("userID")) == user.id){
				header.Text = "View Your Layout";
			} else {
				if(user.online == 1){
					header.Text += "(Online)";
				} else {
					header.Text += "(Offline)";
				}
			}
	
			this.AddSubview(header);
		}
		
	}
}
