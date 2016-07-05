// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace ION.IOS.ViewController.GaugeTesting
{
	[Register ("SaveInternalTestViewController")]
	partial class SaveInternalTestViewController
	{
		[Outlet]
		UIKit.UIButton editInstrumentDetails { get; set; }

		[Outlet]
		UIKit.UIDatePicker enterCalibrationDate { get; set; }

		[Outlet]
		UIKit.UITextField enterInstrumentAccuracy { get; set; }

		[Outlet]
		UIKit.UITextField enterInstrumentModel { get; set; }

		[Outlet]
		UIKit.UITextField enterInstrumentSerial { get; set; }

		[Outlet]
		UIKit.UITextField enterTester { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (enterInstrumentModel != null) {
				enterInstrumentModel.Dispose ();
				enterInstrumentModel = null;
			}

			if (editInstrumentDetails != null) {
				editInstrumentDetails.Dispose ();
				editInstrumentDetails = null;
			}

			if (enterInstrumentSerial != null) {
				enterInstrumentSerial.Dispose ();
				enterInstrumentSerial = null;
			}

			if (enterInstrumentAccuracy != null) {
				enterInstrumentAccuracy.Dispose ();
				enterInstrumentAccuracy = null;
			}

			if (enterCalibrationDate != null) {
				enterCalibrationDate.Dispose ();
				enterCalibrationDate = null;
			}

			if (enterTester != null) {
				enterTester.Dispose ();
				enterTester = null;
			}
		}
	}
}
