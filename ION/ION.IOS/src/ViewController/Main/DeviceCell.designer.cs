// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace ION.IOS.ViewController.Main
{
	[Register ("DeviceCell")]
	partial class DeviceCell
	{
		[Outlet]
		public UIKit.UIActivityIndicatorView activityDeviceConnecting { get; private set; }

		[Outlet]
		public UIKit.UIButton buttonDeviceConnect { get; private set; }

		[Outlet]
		public UIKit.UIImageView imageDeviceIcon { get; private set; }

		[Outlet]
		public UIKit.UILabel labelDeviceName { get; private set; }

		[Outlet]
		public UIKit.UILabel labelDeviceType { get; private set; }

		[Outlet]
		public UIKit.UIView viewBackground { get; private set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (activityDeviceConnecting != null) {
				activityDeviceConnecting.Dispose ();
				activityDeviceConnecting = null;
			}

			if (buttonDeviceConnect != null) {
				buttonDeviceConnect.Dispose ();
				buttonDeviceConnect = null;
			}

			if (imageDeviceIcon != null) {
				imageDeviceIcon.Dispose ();
				imageDeviceIcon = null;
			}

			if (labelDeviceName != null) {
				labelDeviceName.Dispose ();
				labelDeviceName = null;
			}

			if (labelDeviceType != null) {
				labelDeviceType.Dispose ();
				labelDeviceType = null;
			}

			if (viewBackground != null) {
				viewBackground.Dispose ();
				viewBackground = null;
			}
		}
	}
}
