// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace ION.IOS.ViewController.Main
{
	[Register ("DefaultSubviewCell")]
	partial class DefaultSubviewCell
	{
		[Outlet]
		public UIKit.UIButton buttonIcon { get; set; }

		[Outlet]
		public UIKit.UILabel labelMeasurement { get; private set; }

		[Outlet]
		public UIKit.UILabel labelTitle { get; private set; }

		[Outlet]
		public UIKit.UIView viewDivider { get; private set; }
		
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

			if (viewDivider != null) {
				viewDivider.Dispose ();
				viewDivider = null;
			}
		}
	}
}
