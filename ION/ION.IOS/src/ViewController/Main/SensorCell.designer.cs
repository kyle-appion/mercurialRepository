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
		public ION.IOS.UI.NinePatchButtonView buttonAdd { get; set; }

		[Outlet]
    public ION.IOS.UI.NinePatchButtonView buttonAnalyzer { get; set; }

		[Outlet]
    public ION.IOS.UI.NinePatchButtonView buttonWorkbench { get; set; }

		[Outlet]
    public UIKit.UILabel labelSensorMeasurement { get; set; }

		[Outlet]
    public UIKit.UILabel labelSensorType { get; set; }

		[Outlet]
    public UIKit.UIView viewBackground { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (viewBackground != null) {
				viewBackground.Dispose ();
				viewBackground = null;
			}

			if (labelSensorType != null) {
				labelSensorType.Dispose ();
				labelSensorType = null;
			}

			if (labelSensorMeasurement != null) {
				labelSensorMeasurement.Dispose ();
				labelSensorMeasurement = null;
			}

			if (buttonAdd != null) {
				buttonAdd.Dispose ();
				buttonAdd = null;
			}

			if (buttonWorkbench != null) {
				buttonWorkbench.Dispose ();
				buttonWorkbench = null;
			}

			if (buttonAnalyzer != null) {
				buttonAnalyzer.Dispose ();
				buttonAnalyzer = null;
			}
		}
	}
}
