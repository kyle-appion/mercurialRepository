// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace ION.IOS.ViewController.RemoteDeviceManager
{
	[Register ("RemoteDeviceManagerViewController")]
	partial class RemoteDeviceManagerViewController
	{
		[Outlet]
		public UIKit.UILabel labelEmpty { get; private set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (labelEmpty != null) {
				labelEmpty.Dispose ();
				labelEmpty = null;
			}
		}
	}
}
