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
	[Register ("Viewer")]
	partial class Viewer
	{
		[Outlet]
		public UIKit.UIImageView imageAlarmIcon { get; set; }

		[Outlet]
    public UIKit.UIImageView imageBattery { get; set; }

		[Outlet]
    public UIKit.UIButton buttonConnection { get; set; }

		[Outlet]
    public UIKit.UIImageView imageSensor { get; set; }

		[Outlet]
    public UIKit.UILabel labelAlarm { get; set; }

		[Outlet]
    public UIKit.UILabel labelConnectionStatus { get; set; }

		[Outlet]
    public UIKit.UILabel labelHeader { get; set; }

		[Outlet]
    public UIKit.UILabel labelMeasurement { get; set; }

		[Outlet]
    public UIKit.UILabel labelSerialNumber { get; set; }

		[Outlet]
    public UIKit.UILabel labelUnit { get; set; }

		[Outlet]
    public UIKit.UIView viewBackground { get; set; }

		[Outlet]
    public UIKit.UIView viewContent { get; set; }

		[Outlet]
    public UIKit.UIView viewDivider { get; set; }

		[Outlet]
    public UIKit.UIView viewFooter { get; set; }

		[Outlet]
    public UIKit.UIView viewHeader { get; set; }

		[Outlet]
    public UIKit.UIView viewSubviewHeader { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (viewBackground != null) {
				viewBackground.Dispose ();
				viewBackground = null;
			}

			if (viewHeader != null) {
				viewHeader.Dispose ();
				viewHeader = null;
			}

			if (labelHeader != null) {
				labelHeader.Dispose ();
				labelHeader = null;
			}

      if (buttonConnection != null) {
        buttonConnection.Dispose ();
        buttonConnection = null;
			}

			if (imageBattery != null) {
				imageBattery.Dispose ();
				imageBattery = null;
			}

			if (viewDivider != null) {
				viewDivider.Dispose ();
				viewDivider = null;
			}

			if (viewContent != null) {
				viewContent.Dispose ();
				viewContent = null;
			}

			if (imageSensor != null) {
				imageSensor.Dispose ();
				imageSensor = null;
			}

			if (labelMeasurement != null) {
				labelMeasurement.Dispose ();
				labelMeasurement = null;
			}

			if (viewFooter != null) {
				viewFooter.Dispose ();
				viewFooter = null;
			}

			if (labelAlarm != null) {
				labelAlarm.Dispose ();
				labelAlarm = null;
			}

			if (imageAlarmIcon != null) {
				imageAlarmIcon.Dispose ();
				imageAlarmIcon = null;
			}

			if (labelConnectionStatus != null) {
				labelConnectionStatus.Dispose ();
				labelConnectionStatus = null;
			}

			if (labelUnit != null) {
				labelUnit.Dispose ();
				labelUnit = null;
			}

			if (viewSubviewHeader != null) {
				viewSubviewHeader.Dispose ();
				viewSubviewHeader = null;
			}

			if (labelSerialNumber != null) {
				labelSerialNumber.Dispose ();
				labelSerialNumber = null;
			}
		}
	}
}
