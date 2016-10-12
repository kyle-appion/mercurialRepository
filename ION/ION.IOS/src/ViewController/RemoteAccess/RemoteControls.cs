using System;
using Foundation;
using CoreGraphics;
using UIKit; 

namespace ION.IOS.Viewcontroller.RemoteAccess {
	public class RemoteControls {
		public UIView controlView;
		public UIButton editButton;
		public UIButton remoteButton;
		public UIButton disconnectButton;
		
		public RemoteControls(nfloat offset, UIView parentView) {
			controlView = new UIView(new CGRect(.7 * parentView.Bounds.Width, offset, .3 * parentView.Bounds.Width, .2 * parentView.Bounds.Height));
			controlView.BackgroundColor = UIColor.White;
			controlView.Layer.CornerRadius = 5f;
			controlView.Layer.BorderWidth = 1f;
			controlView.ClipsToBounds = true;
			controlView.Hidden = true;
			
			editButton = new UIButton(new CGRect(0,0,controlView.Bounds.Width,.5 * controlView.Bounds.Height));
			editButton.SetTitle("Edit",UIControlState.Normal);
			editButton.SetTitleColor(UIColor.Black, UIControlState.Normal);
			editButton.ClipsToBounds = true;
			editButton.TouchUpInside += (sender, e) => {
				editButton.Hidden = true;
				remoteButton.Hidden = false;
				controlView.Hidden = true;
			};
			editButton.Layer.BorderWidth = 1f;

			remoteButton = new UIButton(new CGRect(0,0,controlView.Bounds.Width,.5 * controlView.Bounds.Height));
			remoteButton.SetTitle("Remote",UIControlState.Normal);
			remoteButton.SetTitleColor(UIColor.Black, UIControlState.Normal);
			remoteButton.ClipsToBounds = true;
			remoteButton.Hidden = true;
			remoteButton.TouchUpInside += (sender, e) => {
				remoteButton.Hidden = true;
				editButton.Hidden = false;
				controlView.Hidden = true;
			};
			remoteButton.Layer.BorderWidth = 1f;
			
			disconnectButton = new UIButton(new CGRect(0, .5 * controlView.Bounds.Height,controlView.Bounds.Width, .5 * controlView.Bounds.Height));
			disconnectButton.SetTitle("Disconnect",UIControlState.Normal);
			disconnectButton.SetTitleColor(UIColor.Black, UIControlState.Normal);
			disconnectButton.ClipsToBounds = true;

			controlView.AddSubview(editButton);
			controlView.AddSubview(remoteButton);
			controlView.AddSubview(disconnectButton);
		}		
	}
}
