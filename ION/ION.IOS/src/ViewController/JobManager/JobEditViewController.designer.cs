// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace ION.IOS.ViewController.JobManager
{
	[Register ("JobEditViewController")]
	partial class JobEditViewController
	{
		[Outlet]
		UIKit.UIButton dataLogginButton { get; set; }

		[Outlet]
		UIKit.UILabel dataLoggingHighlight { get; set; }

		[Outlet]
		UIKit.UIView holderView { get; set; }

		[Outlet]
		UIKit.UIScrollView infoScroller { get; set; }

		[Outlet]
		UIKit.UIButton jobInfoButton { get; set; }

		[Outlet]
		UIKit.UILabel jobInfoHighlight { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (holderView != null) {
				holderView.Dispose ();
				holderView = null;
			}

			if (jobInfoButton != null) {
				jobInfoButton.Dispose ();
				jobInfoButton = null;
			}

			if (dataLogginButton != null) {
				dataLogginButton.Dispose ();
				dataLogginButton = null;
			}

			if (jobInfoHighlight != null) {
				jobInfoHighlight.Dispose ();
				jobInfoHighlight = null;
			}

			if (dataLoggingHighlight != null) {
				dataLoggingHighlight.Dispose ();
				dataLoggingHighlight = null;
			}

			if (infoScroller != null) {
				infoScroller.Dispose ();
				infoScroller = null;
			}
		}
	}
}
