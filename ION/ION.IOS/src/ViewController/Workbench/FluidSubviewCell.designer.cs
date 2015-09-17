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
	[Register ("FluidSubviewCell")]
	partial class FluidSubviewCell
	{
		[Outlet]
		UIKit.UIButton buttonFluid { get; set; }

		[Outlet]
		UIKit.UILabel labelMeasurement { get; set; }

		[Outlet]
		UIKit.UIView viewBackground { get; set; }

		[Outlet]
		UIKit.UIView viewDivider { get; set; }

		[Outlet]
		UIKit.UILabel viewTitle { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (labelMeasurement != null) {
				labelMeasurement.Dispose ();
				labelMeasurement = null;
			}

			if (viewBackground != null) {
				viewBackground.Dispose ();
				viewBackground = null;
			}

			if (viewDivider != null) {
				viewDivider.Dispose ();
				viewDivider = null;
			}

			if (buttonFluid != null) {
				buttonFluid.Dispose ();
				buttonFluid = null;
			}

			if (viewTitle != null) {
				viewTitle.Dispose ();
				viewTitle = null;
			}
		}
	}
}
