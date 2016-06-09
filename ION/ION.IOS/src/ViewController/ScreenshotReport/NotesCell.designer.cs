// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace ION.IOS.ViewController.ScreenshotReport
{
	[Register ("NotesCell")]
	partial class NotesCell
	{
		[Outlet]
		UIKit.UILabel labelHeader { get; set; }

		[Outlet]
		UIKit.UITextField text { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (labelHeader != null) {
				labelHeader.Dispose ();
				labelHeader = null;
			}

			if (text != null) {
				text.Dispose ();
				text = null;
			}
		}
	}
}
