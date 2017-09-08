// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace ION.IOS.ViewController.Alarms
{
	[Register ("SensorAlarmViewController")]
	partial class SensorAlarmViewController
	{
		[Outlet]
		UIKit.UIButton buttonHighAlarmUnit { get; set; }

		[Outlet]
		UIKit.UIButton buttonLowAlarmUnit { get; set; }

		[Outlet]
		UIKit.UITextField editHighAlarmMeasurement { get; set; }

		[Outlet]
		UIKit.UITextField editLowAlarmMeasurement { get; set; }

		[Outlet]
		UIKit.UILabel labelHighAlarmDescription { get; set; }

		[Outlet]
		UIKit.UILabel labelHighAlarmHeader { get; set; }

		[Outlet]
		UIKit.UILabel labelLowAlarmDescription { get; set; }

		[Outlet]
		UIKit.UILabel labelLowAlarmHeader { get; set; }

		[Outlet]
		UIKit.UISwitch switchHighAlarmEnabler { get; set; }

		[Outlet]
		UIKit.UISwitch switchLowAlarmEnabler { get; set; }

		[Outlet]
		UIKit.UIView viewHighAlarmContent { get; set; }

		[Outlet]
		UIKit.UIView viewLowAlarmSection { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (buttonHighAlarmUnit != null) {
				buttonHighAlarmUnit.Dispose ();
				buttonHighAlarmUnit = null;
			}

			if (buttonLowAlarmUnit != null) {
				buttonLowAlarmUnit.Dispose ();
				buttonLowAlarmUnit = null;
			}

			if (editHighAlarmMeasurement != null) {
				editHighAlarmMeasurement.Dispose ();
				editHighAlarmMeasurement = null;
			}

			if (editLowAlarmMeasurement != null) {
				editLowAlarmMeasurement.Dispose ();
				editLowAlarmMeasurement = null;
			}

			if (labelHighAlarmDescription != null) {
				labelHighAlarmDescription.Dispose ();
				labelHighAlarmDescription = null;
			}

			if (labelHighAlarmHeader != null) {
				labelHighAlarmHeader.Dispose ();
				labelHighAlarmHeader = null;
			}

			if (labelLowAlarmDescription != null) {
				labelLowAlarmDescription.Dispose ();
				labelLowAlarmDescription = null;
			}

			if (labelLowAlarmHeader != null) {
				labelLowAlarmHeader.Dispose ();
				labelLowAlarmHeader = null;
			}

			if (switchLowAlarmEnabler != null) {
				switchLowAlarmEnabler.Dispose ();
				switchLowAlarmEnabler = null;
			}

			if (switchHighAlarmEnabler != null) {
				switchHighAlarmEnabler.Dispose ();
				switchHighAlarmEnabler = null;
			}

			if (viewHighAlarmContent != null) {
				viewHighAlarmContent.Dispose ();
				viewHighAlarmContent = null;
			}

			if (viewLowAlarmSection != null) {
				viewLowAlarmSection.Dispose ();
				viewLowAlarmSection = null;
			}
		}
	}
}
