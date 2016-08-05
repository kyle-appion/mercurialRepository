using System;
using Foundation;
using CoreGraphics;
using UIKit;

using ION.Core.App;
using ION.IOS.Util;

using ION.IOS.ViewController.Analyzer;
using ION.IOS.ViewController.PressureTemperatureChart;
using ION.IOS.ViewController.Settings;
using ION.IOS.ViewController.SuperheatSubcool;
using ION.IOS.ViewController.Workbench;
using ION.IOS.ViewController.Logging;
using ION.IOS.ViewController.JobManager;
using MonoTouch.Dialog;
using ION.IOS.ViewController.Help;
using ION.IOS.ViewController.Walkthrough;
using ION.IOS.ViewController.RssFeed;

namespace ION.IOS.ViewController.RemoteAccess {
	public partial class RemoteSystemViewController : BaseIONViewController {
		public UIView remoteView;
		public UIButton remoteMenu;
		public UIButton originalMenu;
		public IION ion;
		
		public RemoteSystemViewController(IntPtr handle) : base(handle) {
		}

		public override void ViewDidLoad() {
			base.ViewDidLoad();

      InitNavigationBar("ic_nav_workbench", false);
      backAction = () => {
        root.navigation.ToggleMenu();
      };
      ion = AppState.context;
			// Perform any additional setup after loading the view, typically from a nib.
			remoteView = new UIView(new CGRect(0,40,View.Bounds.Width, View.Bounds.Height));
			remoteView.BackgroundColor = UIColor.White;
			
			var window = UIApplication.SharedApplication.KeyWindow;
      var rootVC = window.RootViewController as IONPrimaryScreenController;
			
			remoteMenu = new UIButton(new CGRect(.05 * remoteView.Bounds.Width, .8 * remoteView.Bounds.Height, .4 * remoteView.Bounds.Width, .1 * remoteView.Bounds.Height));
			remoteMenu.TouchDown += (sender, e) => {remoteMenu.BackgroundColor = UIColor.Blue;};
			remoteMenu.TouchUpOutside += (sender, e) => {remoteMenu.BackgroundColor = UIColor.FromRGB(255, 215, 101);};
			remoteMenu.TouchUpInside += (sender, e) => {
				remoteMenu.BackgroundColor = UIColor.FromRGB(255, 215, 101);
				remoteView.BringSubviewToFront(originalMenu);
				remoteMenu.Enabled = false;
				originalMenu.Enabled = true;
        if(ion.dataLogManager.isRecording){
          var done = ion.dataLogManager.StopRecording().Result;
        }
        rootVC.setRemoteMenu();       
			};
			remoteMenu.Layer.CornerRadius = 5f;
			remoteMenu.BackgroundColor = UIColor.FromRGB(255, 215, 101);
			remoteMenu.SetTitle("Begin Remote",UIControlState.Normal);
			
			
			originalMenu = new UIButton(new CGRect(.55 * remoteView.Bounds.Width, .8 * remoteView.Bounds.Height, .4 * remoteView.Bounds.Width, .1 * remoteView.Bounds.Height));
			originalMenu.TouchDown += (sender, e) => {originalMenu.BackgroundColor = UIColor.Blue;};
			originalMenu.TouchUpOutside += (sender, e) => {originalMenu.BackgroundColor = UIColor.FromRGB(255, 215, 101);};			
			originalMenu.TouchUpInside += (sender, e) => {
				originalMenu.BackgroundColor = UIColor.FromRGB(255, 215, 101);
				originalMenu.Enabled = false;
				remoteMenu.Enabled = true;
        rootVC.setMainMenu();
			};
			originalMenu.Layer.CornerRadius = 5f;
			originalMenu.BackgroundColor = UIColor.FromRGB(255, 215, 101);
			originalMenu.SetTitle("End Remote",UIControlState.Normal);
			
			remoteView.AddSubview(originalMenu);
			remoteView.BringSubviewToFront(originalMenu);
			remoteView.AddSubview(remoteMenu);
			remoteView.BringSubviewToFront(remoteMenu);
			
			View.AddSubview(remoteView);
		}
    
    
    
    
    
    
    
    
    
    
    
		public override void DidReceiveMemoryWarning() {
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}


