// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace ION.IOS.ViewController.ScreenshotReport
{
	[Register ("SelectionCell")]
	partial class SelectionCell
	{
		[Outlet]
		UIKit.UIButton button { get; set; }

		[Outlet]
    UIKit.UILabel labelHeader { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (labelHeader != null) {
				labelHeader.Dispose ();
				labelHeader = null;
			}

			if (button != null) {
				button.Dispose ();
				button = null;
			}
		}
	}
}
