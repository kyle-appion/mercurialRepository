// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace ION.IOS.ViewController.Ancillary
{
	[Register ("FluidManagerViewController")]
	partial class FluidManagerViewController
	{
		[Outlet]
		public UIKit.UILabel labelFluidName { get; set; }

		[Outlet]
		public UIKit.UISegmentedControl switchContent { get; set; }

		[Outlet]
		public UIKit.UITableView table { get; set; }

		[Outlet]
		public UIKit.UIView viewFluidColor { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (switchContent != null) {
				switchContent.Dispose ();
				switchContent = null;
			}

			if (viewFluidColor != null) {
				viewFluidColor.Dispose ();
				viewFluidColor = null;
			}

			if (labelFluidName != null) {
				labelFluidName.Dispose ();
				labelFluidName = null;
			}

			if (table != null) {
				table.Dispose ();
				table = null;
			}
		}
	}
}
