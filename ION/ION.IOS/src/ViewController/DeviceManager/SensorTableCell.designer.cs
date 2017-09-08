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
	[Register ("SensorTableCell")]
	partial class SensorTableCell
	{
		[Outlet]
		UIKit.UIButton buttonAdd { get; set; }

		[Outlet]
		UIKit.UILabel labelMeasurement { get; set; }

		[Outlet]
		UIKit.UILabel labelType { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (labelType != null) {
				labelType.Dispose ();
				labelType = null;
			}

			if (buttonAdd != null) {
				buttonAdd.Dispose ();
				buttonAdd = null;
			}

			if (labelMeasurement != null) {
				labelMeasurement.Dispose ();
				labelMeasurement = null;
			}
		}
	}
}
