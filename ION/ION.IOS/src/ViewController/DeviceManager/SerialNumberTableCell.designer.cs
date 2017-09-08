// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace ION.IOS.ViewController.DeviceManager
{
	[Register ("SerialNumberTableCell")]
	partial class SerialNumberTableCell
	{
		[Outlet]
		UIKit.UILabel labelSerialNumber { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (labelSerialNumber != null) {
				labelSerialNumber.Dispose ();
				labelSerialNumber = null;
			}
		}
	}
}
