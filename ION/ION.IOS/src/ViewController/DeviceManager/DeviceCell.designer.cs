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
	[Register ("DeviceCell")]
	partial class DeviceCell
	{
		[Outlet]
		UIKit.UIActivityIndicatorView activityDeviceConnection { get; set; }

		[Outlet]
		UIKit.UIButton buttonDeviceToggleConnect { get; set; }

		[Outlet]
		UIKit.UIImageView iconDevice { get; set; }

		[Outlet]
		UIKit.UIImageView iconDeviceExpandIndicator { get; set; }

		[Outlet]
		UIKit.UILabel labelDeviceName { get; set; }

		[Outlet]
		UIKit.UILabel labelDeviceSerialNumber { get; set; }

		[Outlet]
		UIKit.UILabel labelDeviceType { get; set; }

		[Outlet]
		UIKit.UIView viewContainerExpanded { get; set; }

		[Outlet]
		UIKit.UIView viewDeviceContent { get; set; }

		[Outlet]
		UIKit.UIView viewDeviceSensorContainer { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (iconDevice != null) {
				iconDevice.Dispose ();
				iconDevice = null;
			}

			if (buttonDeviceToggleConnect != null) {
				buttonDeviceToggleConnect.Dispose ();
				buttonDeviceToggleConnect = null;
			}

			if (activityDeviceConnection != null) {
				activityDeviceConnection.Dispose ();
				activityDeviceConnection = null;
			}

			if (viewDeviceContent != null) {
				viewDeviceContent.Dispose ();
				viewDeviceContent = null;
			}

			if (labelDeviceName != null) {
				labelDeviceName.Dispose ();
				labelDeviceName = null;
			}

			if (labelDeviceType != null) {
				labelDeviceType.Dispose ();
				labelDeviceType = null;
			}

			if (iconDeviceExpandIndicator != null) {
				iconDeviceExpandIndicator.Dispose ();
				iconDeviceExpandIndicator = null;
			}

			if (viewContainerExpanded != null) {
				viewContainerExpanded.Dispose ();
				viewContainerExpanded = null;
			}

			if (labelDeviceSerialNumber != null) {
				labelDeviceSerialNumber.Dispose ();
				labelDeviceSerialNumber = null;
			}

			if (viewDeviceSensorContainer != null) {
				viewDeviceSensorContainer.Dispose ();
				viewDeviceSensorContainer = null;
			}
		}
	}
}
