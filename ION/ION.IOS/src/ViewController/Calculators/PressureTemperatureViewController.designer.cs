// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace ION.IOS.ViewController.Calculators
{
	[Register ("PressureTemperatureViewController")]
	partial class PressureTemperatureViewController
	{
		[Outlet]
		public UIKit.UIButton buttonHelp { get; set; }

		[Outlet]
    public UIKit.UIButton buttonPressureUnit { get; set; }

		[Outlet]
    public UIKit.UIButton buttonTemperatureUnit { get; set; }

		[Outlet]
    public UIKit.UITextView editPressure { get; set; }

		[Outlet]
    public UIKit.UITextView editTemperature { get; set; }

		[Outlet]
    public UIKit.UIImageView iconPressure { get; set; }

		[Outlet]
    public UIKit.UIImageView iconPressureLock { get; set; }

		[Outlet]
    public UIKit.UIImageView iconTemperature { get; set; }

		[Outlet]
    public UIKit.UIImageView iconTemperatureLock { get; set; }

		[Outlet]
    public UIKit.UILabel labelFluidName { get; set; }

		[Outlet]
    public UIKit.UISegmentedControl switchDewBubble { get; set; }

		[Outlet]
    public UIKit.UIView viewAssignPressureSensor { get; set; }

		[Outlet]
    public UIKit.UIView viewAssignTemperatureSensor { get; set; }

		[Outlet]
    public UIKit.UIView viewFluidColor { get; set; }

		[Outlet]
    public UIKit.UIView viewFluidTouchArea { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (viewFluidTouchArea != null) {
				viewFluidTouchArea.Dispose ();
				viewFluidTouchArea = null;
			}

			if (viewFluidColor != null) {
				viewFluidColor.Dispose ();
				viewFluidColor = null;
			}

			if (labelFluidName != null) {
				labelFluidName.Dispose ();
				labelFluidName = null;
			}

			if (buttonHelp != null) {
				buttonHelp.Dispose ();
				buttonHelp = null;
			}

			if (switchDewBubble != null) {
				switchDewBubble.Dispose ();
				switchDewBubble = null;
			}

			if (viewAssignPressureSensor != null) {
				viewAssignPressureSensor.Dispose ();
				viewAssignPressureSensor = null;
			}

			if (iconPressureLock != null) {
				iconPressureLock.Dispose ();
				iconPressureLock = null;
			}

			if (iconPressure != null) {
				iconPressure.Dispose ();
				iconPressure = null;
			}

			if (buttonPressureUnit != null) {
				buttonPressureUnit.Dispose ();
				buttonPressureUnit = null;
			}

			if (editPressure != null) {
				editPressure.Dispose ();
				editPressure = null;
			}

			if (viewAssignTemperatureSensor != null) {
				viewAssignTemperatureSensor.Dispose ();
				viewAssignTemperatureSensor = null;
			}

			if (iconTemperatureLock != null) {
				iconTemperatureLock.Dispose ();
				iconTemperatureLock = null;
			}

			if (iconTemperature != null) {
				iconTemperature.Dispose ();
				iconTemperature = null;
			}

			if (buttonTemperatureUnit != null) {
				buttonTemperatureUnit.Dispose ();
				buttonTemperatureUnit = null;
			}

			if (editTemperature != null) {
				editTemperature.Dispose ();
				editTemperature = null;
			}
		}
	}
}
