// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace ION.IOS.ViewController.Workbench
{
	[Register ("TargetSHSCCell")]
	partial class TargetSHSCCell
	{
		[Outlet]
		UIKit.UILabel labelOffset { get; set; }

		[Outlet]
		UIKit.UILabel labelTarget { get; set; }

		[Outlet]
		UIKit.UILabel labelTitle { get; set; }

		[Outlet]
		UIKit.UIView viewBackground { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (viewBackground != null) {
				viewBackground.Dispose ();
				viewBackground = null;
			}

			if (labelTitle != null) {
				labelTitle.Dispose ();
				labelTitle = null;
			}

			if (labelTarget != null) {
				labelTarget.Dispose ();
				labelTarget = null;
			}

			if (labelOffset != null) {
				labelOffset.Dispose ();
				labelOffset = null;
			}
		}
	}
}
