
using System;
using CoreGraphics;
using Foundation;
using UIKit;

namespace ION.IOS.ViewController.AccessRequest {
	public partial class AccessRequestTableCell : UITableViewCell {
		public UILabel header;

		protected AccessRequestTableCell(IntPtr handle)  {
			
		}
		
		public void makeCellData(requestData user, nfloat cellHeight){
			header = new UILabel(new CGRect(0,0,this.Bounds.Width, cellHeight));
			header.Text ="("+user.accessCode+") " + user.displayName;
			header.AdjustsFontSizeToFitWidth = true;
			header.TextAlignment = UITextAlignment.Center;
			
			this.AddSubview(header);			
		}
	}
}
