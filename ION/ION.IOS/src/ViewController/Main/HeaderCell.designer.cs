// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace ION.IOS.ViewController.Main {
	[Register ("HeaderCell")]
	partial class HeaderCell {
		[Outlet]
		public UIKit.UIButton buttonOptions { get; set; }

		[Outlet]
    public UIKit.UILabel labelCounter { get; set; }

		[Outlet]
    public UIKit.UILabel labelHeader { get; set; }

		[Outlet]
    public UIKit.UIView viewBackground { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (viewBackground != null) {
				viewBackground.Dispose ();
				viewBackground = null;
			}

			if (labelCounter != null) {
				labelCounter.Dispose ();
				labelCounter = null;
			}

			if (buttonOptions != null) {
				buttonOptions.Dispose ();
				buttonOptions = null;
			}

			if (labelHeader != null) {
				labelHeader.Dispose ();
				labelHeader = null;
			}
		}
	}
}
