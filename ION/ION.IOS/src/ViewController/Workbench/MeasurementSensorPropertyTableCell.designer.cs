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
	[Register ("MeasurementSensorPropertyTableCell")]
	partial class MeasurementSensorPropertyTableCell
	{
		[Outlet]
		UIKit.UIButton button { get; set; }

		[Outlet]
		UIKit.UIButton buttonIcon { get; set; }

		[Outlet]
		UIKit.UILabel labelMeasurement { get; set; }

		[Outlet]
		UIKit.UILabel labelTitle { get; set; }

		[Outlet]
		UIKit.UIView viewBackground { get; set; }

		[Outlet]
		UIKit.UIView viewDivider { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (buttonIcon != null) {
				buttonIcon.Dispose ();
				buttonIcon = null;
			}

			if (labelMeasurement != null) {
				labelMeasurement.Dispose ();
				labelMeasurement = null;
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

			if (button != null) {
				button.Dispose ();
				button = null;
			}
		}
	}
}
