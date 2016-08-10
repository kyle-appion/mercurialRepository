// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace ION.IOS.ViewController.RemoteDeviceManager {
	[Register("RemoteDeviceManagerViewController")]
	partial class RemoteDeviceManagerViewController {
	
		[Outlet]
		public UIKit.UILabel labelEmpty { get; set; }

		[Outlet]
		UIKit.UITableView tableContent { get; set; }
		
		void ReleaseDesignerOutlets() {
			if (labelEmpty != null) {
				labelEmpty.Dispose ();
				labelEmpty = null;
			}

			if (tableContent != null) {
				tableContent.Dispose ();
				tableContent = null;
			}
		}
	}
}

