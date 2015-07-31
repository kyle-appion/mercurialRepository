// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace ION.IOS.ViewController.Navigation {
	[Register ("NavigationItem")]
	partial class NavigationItem {
		[Outlet]
		public UIKit.UIImageView iconNav { get; set; }

		[Outlet]
		public UIKit.UILabel labelTitle { get; set; }

		[Outlet]
		public UIKit.UIView viewDivider { get; set; }
		
    void ReleaseDesignerOutlets() {
			if (iconNav != null) {
				iconNav.Dispose ();
				iconNav = null;
			}

			if (viewDivider != null) {
				viewDivider.Dispose ();
				viewDivider = null;
			}

			if (labelTitle != null) {
				labelTitle.Dispose ();
				labelTitle = null;
			}
		}
	}
}
