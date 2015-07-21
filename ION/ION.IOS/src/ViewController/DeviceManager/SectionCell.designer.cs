// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace ION.IOS.ViewController.DeviceManager
{
	[Register ("SectionCell")]
	partial class SectionCell
	{
		[Outlet]
		UIKit.UIButton buttonMenu { get; set; }

		[Outlet]
		UIKit.UILabel labelCounter { get; set; }

		[Outlet]
		UIKit.UILabel labelTitle { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (labelCounter != null) {
				labelCounter.Dispose ();
				labelCounter = null;
			}

			if (labelTitle != null) {
				labelTitle.Dispose ();
				labelTitle = null;
			}

			if (buttonMenu != null) {
				buttonMenu.Dispose ();
				buttonMenu = null;
			}
		}
	}
}
