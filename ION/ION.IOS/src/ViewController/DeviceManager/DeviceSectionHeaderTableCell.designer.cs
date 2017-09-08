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
	[Register ("DeviceSectionHeaderTableCell")]
	partial class DeviceSectionHeaderTableCell
	{
		[Outlet]
		UIKit.UIButton buttonOptions { get; set; }

		[Outlet]
		UIKit.UILabel labelCounter { get; set; }

		[Outlet]
		UIKit.UILabel labelTitle { get; set; }

		[Outlet]
		UIKit.UIView viewBackground { get; set; }

		[Outlet]
		UIKit.UIView viewDivider { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (buttonOptions != null) {
				buttonOptions.Dispose ();
				buttonOptions = null;
			}

			if (labelCounter != null) {
				labelCounter.Dispose ();
				labelCounter = null;
			}

			if (labelTitle != null) {
				labelTitle.Dispose ();
				labelTitle = null;
			}

			if (viewBackground != null) {
				viewBackground.Dispose ();
				viewBackground = null;
			}

			if (viewDivider != null) {
				viewDivider.Dispose ();
				viewDivider = null;
			}
		}
	}
}
