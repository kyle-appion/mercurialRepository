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
	[Register ("FluidCell")]
	partial class FluidCell
	{
		[Outlet]
		public UIKit.UIImageView iconFavorite { get; set; }

		[Outlet]
    public UIKit.UIImageView iconSelected { get; set; }

		[Outlet]
    public UIKit.UILabel labelFluidName { get; set; }

		[Outlet]
    public UIKit.UIView viewFluidColor { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (viewFluidColor != null) {
				viewFluidColor.Dispose ();
				viewFluidColor = null;
			}

			if (labelFluidName != null) {
				labelFluidName.Dispose ();
				labelFluidName = null;
			}

			if (iconFavorite != null) {
				iconFavorite.Dispose ();
				iconFavorite = null;
			}

			if (iconSelected != null) {
				iconSelected.Dispose ();
				iconSelected = null;
			}
		}
	}
}
