using System;
using Foundation;
using CoreGraphics;
using UIKit;
using ION.IOS.App;
using ION.Core.App;

namespace ION.IOS.Viewcontroller.RemoteAccess {
	public class RemoteControls {
		public UIView controlView;
		//public UIButton editButton;
		//public UIButton remoteButton;
		public UIButton remoteLoggingButton;
		public UIButton disconnectButton;
		public IosION ion;
		
		public RemoteControls(nfloat offset, UIView parentView) {
			ion = AppState.context as IosION;
			////CHECK IF USER IS VIEWING A LAYOUT BEING UPLOADED UNDER THEIR OWN ACCOUNT
			////USER IS GRANTED REMOTE OPERATION CONTROL FOR SOME THINGS IF THEY OWN THE ACCOUNT
			if(NSUserDefaults.StandardUserDefaults.StringForKey("viewedUser") == KeychainAccess.ValueForKey("userID")){
				controlView = new UIView(new CGRect(.6 * parentView.Bounds.Width, offset, .4 * parentView.Bounds.Width, .16 * parentView.Bounds.Height));
				remoteLoggingButton = new UIButton(new CGRect(0,0,controlView.Bounds.Width,.5 * controlView.Bounds.Height));
				disconnectButton = new UIButton(new CGRect(0, .5 * controlView.Bounds.Height,controlView.Bounds.Width, .5 * controlView.Bounds.Height));
			
				if(ion.remoteDevice.loggingStatus == 1){
					NSUserDefaults.StandardUserDefaults.SetString("1","remoteLogging");
					remoteLoggingButton.SetTitle("Stop Logging", UIControlState.Normal);
				} else {
					NSUserDefaults.StandardUserDefaults.SetString("","remoteLogging");					
					remoteLoggingButton.SetTitle("Start Logging", UIControlState.Normal);
				}
				remoteLoggingButton.SetTitleColor(UIColor.Black, UIControlState.Normal);
				remoteLoggingButton.ClipsToBounds = true;
				
				remoteLoggingButton.TouchUpInside += changeLogStatus;
				
				controlView.AddSubview(remoteLoggingButton);
				controlView.AddSubview(disconnectButton);
			} else {
				controlView = new UIView(new CGRect(.6 * parentView.Bounds.Width, offset, .4 * parentView.Bounds.Width, .08 * parentView.Bounds.Height));
				disconnectButton = new UIButton(new CGRect(0, 0,controlView.Bounds.Width, controlView.Bounds.Height));
				
				controlView.AddSubview(disconnectButton);
			}
			controlView.BackgroundColor = UIColor.White;
			controlView.Layer.CornerRadius = 5f;
			controlView.Layer.BorderWidth = 1f;
			controlView.ClipsToBounds = true;
			controlView.Hidden = true;
			
			//editButton = new UIButton(new CGRect(0,0,controlView.Bounds.Width,.5 * controlView.Bounds.Height));
			//editButton.SetTitle("Edit",UIControlState.Normal);
			//editButton.SetTitleColor(UIColor.Black, UIControlState.Normal);
			//editButton.ClipsToBounds = true;
			//editButton.TouchUpInside += (sender, e) => {
			//	editButton.Hidden = true;
			//	remoteButton.Hidden = false;
			//	controlView.Hidden = true;
			//};
			//editButton.Layer.BorderWidth = 1f;

			//remoteButton = new UIButton(new CGRect(0,0,controlView.Bounds.Width,.5 * controlView.Bounds.Height));
			//remoteButton.SetTitle("Remote",UIControlState.Normal);
			//remoteButton.SetTitleColor(UIColor.Black, UIControlState.Normal);
			//remoteButton.ClipsToBounds = true;
			//remoteButton.Hidden = true;
			//remoteButton.TouchUpInside += (sender, e) => {
			//	remoteButton.Hidden = true;
			//	editButton.Hidden = false;
			//	controlView.Hidden = true;
			//};
			//remoteButton.Layer.BorderWidth = 1f;

			
			disconnectButton.SetTitle("Disconnect",UIControlState.Normal);
			disconnectButton.SetTitleColor(UIColor.Black, UIControlState.Normal);
			disconnectButton.ClipsToBounds = true;

			//controlView.AddSubview(editButton);
			//controlView.AddSubview(remoteButton);
		}
		
		public async void changeLogStatus(object sender, EventArgs e){
			if(String.IsNullOrEmpty(NSUserDefaults.StandardUserDefaults.StringForKey("remoteLogging"))){
				remoteLoggingButton.SetTitle("Stop Logging", UIControlState.Normal);
				NSUserDefaults.StandardUserDefaults.SetString("1","remoteLogging");
				var feedback = await ion.webServices.SetRemoteDataLog(NSUserDefaults.StandardUserDefaults.StringForKey("viewedUser"), NSUserDefaults.StandardUserDefaults.StringForKey("viewedLayout"),"1");
				var textResponse = feedback.Content.ReadAsStringAsync();
				Console.WriteLine(textResponse);				
			} else {
				remoteLoggingButton.SetTitle("Start Logging", UIControlState.Normal);
				NSUserDefaults.StandardUserDefaults.SetString("","remoteLogging");
				var feedback = await ion.webServices.SetRemoteDataLog(NSUserDefaults.StandardUserDefaults.StringForKey("viewedUser"), NSUserDefaults.StandardUserDefaults.StringForKey("viewedLayout"),"0");
				var textResponse = feedback.Content.ReadAsStringAsync();
				Console.WriteLine(textResponse);
			}
		}		
	}
}
