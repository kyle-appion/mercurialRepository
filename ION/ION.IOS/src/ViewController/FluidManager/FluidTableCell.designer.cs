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
	[Register ("FluidTableCell")]
	partial class FluidTableCell
	{
		[Outlet]
		UIKit.UIImageView imageFavorite { get; set; }

		[Outlet]
		UIKit.UIImageView imageSelected { get; set; }

		[Outlet]
		UIKit.UILabel labelFluidName { get; set; }

		[Outlet]
		UIKit.UIView viewFluidColor { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (imageFavorite != null) {
				imageFavorite.Dispose ();
				imageFavorite = null;
			}

			if (imageSelected != null) {
				imageSelected.Dispose ();
				imageSelected = null;
			}

			if (labelFluidName != null) {
				labelFluidName.Dispose ();
				labelFluidName = null;
			}

			if (viewFluidColor != null) {
				viewFluidColor.Dispose ();
				viewFluidColor = null;
			}
		}
	}
}
