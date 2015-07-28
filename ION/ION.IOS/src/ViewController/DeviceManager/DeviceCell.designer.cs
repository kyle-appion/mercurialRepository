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

namespace ION.IOS.ViewController.DeviceManager
{
	[Register ("DeviceCell")]
	partial class DeviceCell
	{
		[Outlet]
		public UIKit.UIActivityIndicatorView activityDeviceConnection { get; set; }

		[Outlet]
		public UIKit.UIButton buttonDeviceToggleConnect { get; set; }

		[Outlet]
		public UIKit.UIImageView iconDevice { get; set; }

		[Outlet]
		public UIKit.UIImageView iconDeviceExpandIndicator { get; set; }

		[Outlet]
		public UIKit.UILabel labelDeviceName { get; set; }

		[Outlet]
		public UIKit.UILabel labelDeviceSerialNumber { get; set; }

		[Outlet]
		public UIKit.UILabel labelDeviceType { get; set; }

		[Outlet]
		public UIKit.UIView viewContainerExpanded { get; set; }

		[Outlet]
		public UIKit.UIView viewDeviceContent { get; set; }

		[Outlet]
		public UIKit.UIView viewDeviceSensorContainer { get; set; }

		void ReleaseDesignerOutlets ()
		{
		}
	}
}
