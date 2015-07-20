// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace ION.IOS
{
	partial class DeviceManagerSensorView
	{
		[Outlet]
		UIKit.UIImageView iconAdd { get; set; }

		[Outlet]
		UIKit.UIImageView iconAddAnalyzer { get; set; }

		[Outlet]
		UIKit.UIImageView iconAddWorkbench { get; set; }

		[Outlet]
		UIKit.UIImageView iconSensor { get; set; }

		[Outlet]
		UIKit.UILabel labelSensorMeasurement { get; set; }

		[Outlet]
		UIKit.UILabel labelSensorType { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (iconAdd != null) {
				iconAdd.Dispose ();
				iconAdd = null;
			}

			if (iconAddAnalyzer != null) {
				iconAddAnalyzer.Dispose ();
				iconAddAnalyzer = null;
			}

			if (iconAddWorkbench != null) {
				iconAddWorkbench.Dispose ();
				iconAddWorkbench = null;
			}

			if (iconSensor != null) {
				iconSensor.Dispose ();
				iconSensor = null;
			}

			if (labelSensorMeasurement != null) {
				labelSensorMeasurement.Dispose ();
				labelSensorMeasurement = null;
			}

			if (labelSensorType != null) {
				labelSensorType.Dispose ();
				labelSensorType = null;
			}
		}
	}
}
