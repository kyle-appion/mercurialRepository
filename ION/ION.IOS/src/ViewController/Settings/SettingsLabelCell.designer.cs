// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace ION.IOS.ViewController.Settings
{
	[Register ("SettingsLabelCell")]
	partial class SettingsLabelCell
	{
		[Outlet]
		UIKit.UILabel label { get; set; }

		[Outlet]
		UIKit.UILabel labelDescription { get; set; }

		[Outlet]
		UIKit.UILabel labelTitle { get; set; }

		[Outlet]
		UIKit.UIView viewPreferenceArea { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (labelTitle != null) {
				labelTitle.Dispose ();
				labelTitle = null;
			}

			if (labelDescription != null) {
				labelDescription.Dispose ();
				labelDescription = null;
			}

			if (viewPreferenceArea != null) {
				viewPreferenceArea.Dispose ();
				viewPreferenceArea = null;
			}

			if (label != null) {
				label.Dispose ();
				label = null;
			}
		}
	}
}
