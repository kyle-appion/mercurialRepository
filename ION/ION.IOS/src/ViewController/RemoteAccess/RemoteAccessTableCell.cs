using System;
using CoreGraphics;
using UIKit;
using Foundation;


namespace ION.IOS.ViewController.RemoteAccess {

	public class RemoteAccessTableCell : UITableViewCell{
	
		public UILabel header;
		
		public RemoteAccessTableCell(IntPtr handle) {
		} 
		
		public RemoteAccessTableCell() {
		
		} 		
		public void makeCellData(double cellHeight, accessData user){
			header = new UILabel(new CGRect(0,0,this.Bounds.Width, cellHeight));
			header.TextAlignment = UITextAlignment.Center;
			header.Text = user.displayName;
			this.AddSubview(header);
		}
		
	}
}

