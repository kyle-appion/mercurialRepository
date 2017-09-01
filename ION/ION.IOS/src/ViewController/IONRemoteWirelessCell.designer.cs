// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace ION.IOS
{
	[Register ("IONRemoteWirelessCell")]
	partial class IONRemoteWirelessCell
	{
		[Outlet]
		UIKit.UIImageView cellImage { get; set; }

		[Outlet]
		UIKit.UILabel labelTitle { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (labelTitle != null) {
				labelTitle.Dispose ();
				labelTitle = null;
			}

			if (cellImage != null) {
				cellImage.Dispose ();
				cellImage = null;
			}
		}
	}
}
