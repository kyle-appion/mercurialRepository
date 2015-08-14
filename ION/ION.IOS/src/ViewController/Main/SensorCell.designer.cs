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
	[Register ("SensorCell")]
	partial class SensorCell
	{
		[Outlet]
		public UIKit.UIButton buttonAdd { get; private set; }

		[Outlet]
		public UIKit.UIButton buttonAnalyzer { get; private set; }

		[Outlet]
		public UIKit.UIButton buttonWorkbench { get; private set; }

		[Outlet]
		public UIKit.UILabel labelSensorMeasurement { get; private set; }

		[Outlet]
		public UIKit.UILabel labelSensorType { get; private set; }

		[Outlet]
		public UIKit.UIView viewBackground { get; private set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (buttonAdd != null) {
				buttonAdd.Dispose ();
				buttonAdd = null;
			}

			if (buttonAnalyzer != null) {
				buttonAnalyzer.Dispose ();
				buttonAnalyzer = null;
			}

			if (buttonWorkbench != null) {
				buttonWorkbench.Dispose ();
				buttonWorkbench = null;
			}

			if (labelSensorMeasurement != null) {
				labelSensorMeasurement.Dispose ();
				labelSensorMeasurement = null;
			}

			if (labelSensorType != null) {
				labelSensorType.Dispose ();
				labelSensorType = null;
			}

			if (viewBackground != null) {
				viewBackground.Dispose ();
				viewBackground = null;
			}
		}
	}
}
