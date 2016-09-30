using System;
using Foundation;
using CoreGraphics;
using UIKit; 

namespace ION.IOS.Viewcontroller.RemoteAccess {
	public class RemoteControls {
		public UIView controlView;
		public UILabel remoteLabel;
		public UIButton editButton;
		public UIButton disconnectButton;
		
		public RemoteControls(nfloat offset,nfloat width,nfloat height) {
			controlView = new UIView(new CGRect(0,offset,width,height));
			
			remoteLabel = new UILabel(new CGRect(0,0,controlView.Bounds.Width,controlView.Bounds.Height));
			remoteLabel.Text = "Viewing Remotely";
			remoteLabel.TextAlignment = UITextAlignment.Left;
			remoteLabel.TextColor = UIColor.White;
			remoteLabel.BackgroundColor = UIColor.Red;
			remoteLabel.Alpha = .8f;
			
			editButton = new UIButton(new CGRect(.45 * controlView.Bounds.Width,0,.15 * controlView.Bounds.Width,controlView.Bounds.Height));
			editButton.SetTitle("Edit",UIControlState.Normal);
			
			disconnectButton = new UIButton(new CGRect(.7 * controlView.Bounds.Width, 0, .3 * controlView.Bounds.Width, controlView.Bounds.Height));
			disconnectButton.SetTitle("Disconnect",UIControlState.Normal);
			
			controlView.AddSubview(remoteLabel);
			controlView.AddSubview(editButton);
			controlView.AddSubview(disconnectButton);
		}		
		
	}
}
