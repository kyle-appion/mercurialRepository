// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace ION.IOS.ViewController.Workbench
{
	[Register ("RateofChangeSettingsViewController")]
	partial class RateofChangeSettingsViewController
	{
		[Outlet]
		UIKit.UILabel actualLabel { get; set; }

		[Outlet]
		UIKit.UIView graphView { get; set; }

		[Outlet]
		UIKit.UISwitch secondaryToggle { get; set; }

		[Outlet]
		UIKit.UILabel shscLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (graphView != null) {
				graphView.Dispose ();
				graphView = null;
			}

			if (actualLabel != null) {
				actualLabel.Dispose ();
				actualLabel = null;
			}

			if (shscLabel != null) {
				shscLabel.Dispose ();
				shscLabel = null;
			}

			if (secondaryToggle != null) {
				secondaryToggle.Dispose ();
				secondaryToggle = null;
			}
		}
	}
}
