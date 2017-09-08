// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace ION.IOS.ViewController.FileManager
{
	[Register ("FileCell")]
	partial class FileCell
	{
		[Outlet]
		UIKit.UIImageView imageIcon { get; set; }

		[Outlet]
		UIKit.UILabel labelFilePath { get; set; }

		[Outlet]
		UIKit.UIView viewBackground { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (viewBackground != null) {
				viewBackground.Dispose ();
				viewBackground = null;
			}

			if (imageIcon != null) {
				imageIcon.Dispose ();
				imageIcon = null;
			}

			if (labelFilePath != null) {
				labelFilePath.Dispose ();
				labelFilePath = null;
			}
		}
	}
}
