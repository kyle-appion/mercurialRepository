using System;
using Foundation;
using CoreGraphics;
using UIKit;

using ION.Core.App;
using ION.IOS.App;

namespace ION.IOS.ViewController.RemoteAccess {
	public class remoteSelectionView {
		public UIView selectionView;
		public UIButton remoteMenu;
		public UIButton fullMenu;

		public UITableView accessTable;

		
		public remoteSelectionView(UIView parentView, IION ion) {
			// Perform any additional setup after loading the view, typically from a nib.
			selectionView = new UIView(new CGRect(0,40,parentView.Bounds.Width, parentView.Bounds.Height));
			selectionView.BackgroundColor = UIColor.White;
			
			var window = UIApplication.SharedApplication.KeyWindow;
      var rootVC = window.RootViewController as IONPrimaryScreenController;
   
			remoteMenu = new UIButton(new CGRect(.05 * selectionView.Bounds.Width, .8 * selectionView.Bounds.Height, .4 * selectionView.Bounds.Width, .1 * selectionView.Bounds.Height));
			remoteMenu.BackgroundColor = UIColor.FromRGB(255, 215, 101);
			remoteMenu.SetTitle("Remote Mode", UIControlState.Normal);
			remoteMenu.SetTitleColor(UIColor.Black, UIControlState.Normal);
			remoteMenu.Layer.CornerRadius = 5f;
			remoteMenu.Layer.BorderWidth = 1f;
			remoteMenu.TouchDown += (sender, e) => {remoteMenu.BackgroundColor = UIColor.Blue;};
			remoteMenu.TouchUpOutside += (sender, e) => {remoteMenu.BackgroundColor = UIColor.FromRGB(255, 215, 101);};
			remoteMenu.TouchUpInside += async (sender, e) => {
				remoteMenu.BackgroundColor = UIColor.FromRGB(255, 215, 101);
				Console.WriteLine("pressed remote menu");
				AppState.context.Dispose();
				var remoteIon = new RemoteIosION();
				await remoteIon.Init();
				AppState.context = ion;
        rootVC.setRemoteMenu();
			};
			
			fullMenu = new UIButton(new CGRect(.55 * selectionView.Bounds.Width, .8 * selectionView.Bounds.Height, .4 * selectionView.Bounds.Width, .1 * selectionView.Bounds.Height));
			fullMenu.Layer.CornerRadius = 5f;
			fullMenu.Layer.BorderWidth = 1f;
			fullMenu.BackgroundColor = UIColor.FromRGB(255, 215, 101);
			fullMenu.SetTitle("Normal Mode",UIControlState.Normal);
			fullMenu.SetTitleColor(UIColor.Black, UIControlState.Normal);
			
			fullMenu.TouchDown += (sender, e) => {fullMenu.BackgroundColor = UIColor.Blue;};
			fullMenu.TouchUpOutside += (sender, e) => {fullMenu.BackgroundColor = UIColor.FromRGB(255, 215, 101);};			
			fullMenu.TouchUpInside += async (sender, e) => {
				fullMenu.BackgroundColor = UIColor.FromRGB(255, 215, 101);
				Console.WriteLine("original menu pressed");
				AppState.context.Dispose();
				var newIon = new IosION();
				await newIon.Init();
				AppState.context = newIon;
				rootVC.setMainMenu();
			};
			selectionView.AddSubview(remoteMenu);
			selectionView.AddSubview(fullMenu);	
		}		
	}
}

