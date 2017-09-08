// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace ION.IOS.ViewController.FluidManager
{
	[Register ("FluidManagerViewController")]
	partial class FluidManagerViewController
	{
		[Outlet]
		UIKit.UILabel fluidNameLabel { get; set; }

		[Outlet]
		UIKit.UILabel fluidSafetyLabel { get; set; }

		[Outlet]
		UIKit.UILabel labelFluidName { get; set; }

		[Outlet]
		UIKit.UIButton safetyHelpButton { get; set; }

		[Outlet]
		UIKit.UISegmentedControl switchFluidSource { get; set; }

		[Outlet]
		UIKit.UITableView tableContent { get; set; }

		[Outlet]
		UIKit.UIView viewFluidColor { get; set; }

		[Outlet]
		UIKit.UIView viewSelectedFluidContainer { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (fluidNameLabel != null) {
				fluidNameLabel.Dispose ();
				fluidNameLabel = null;
			}

			if (fluidSafetyLabel != null) {
				fluidSafetyLabel.Dispose ();
				fluidSafetyLabel = null;
			}

			if (labelFluidName != null) {
				labelFluidName.Dispose ();
				labelFluidName = null;
			}

			if (switchFluidSource != null) {
				switchFluidSource.Dispose ();
				switchFluidSource = null;
			}

			if (tableContent != null) {
				tableContent.Dispose ();
				tableContent = null;
			}

			if (viewFluidColor != null) {
				viewFluidColor.Dispose ();
				viewFluidColor = null;
			}

			if (viewSelectedFluidContainer != null) {
				viewSelectedFluidContainer.Dispose ();
				viewSelectedFluidContainer = null;
			}

			if (safetyHelpButton != null) {
				safetyHelpButton.Dispose ();
				safetyHelpButton = null;
			}
		}
	}
}
