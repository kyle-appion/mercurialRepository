// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace ION.IOS.ViewController.PressureTemperatureChart
{
	[Register ("PTChartViewController")]
	partial class PTChartViewController
	{
		[Outlet]
		UIKit.UIButton buttonPressureUnit { get; set; }

		[Outlet]
		UIKit.UIButton buttonTemperatureUnit { get; set; }

		[Outlet]
		UIKit.UITextField editPressure { get; set; }

		[Outlet]
		UIKit.UITextField editTemperature { get; set; }

		[Outlet]
		UIKit.UIImageView imagePressureIcon { get; set; }

		[Outlet]
		UIKit.UIImageView imagePressureLock { get; set; }

		[Outlet]
		UIKit.UIImageView imageTemperatureIcon { get; set; }

		[Outlet]
		UIKit.UIImageView imageTemperatureLock { get; set; }

		[Outlet]
    UIKit.UILabel labelFluidName { get; set ; }

		[Outlet]
		UIKit.UILabel labelPressure { get; set; }

		[Outlet]
		UIKit.UILabel labelTemperature { get; set; }

		[Outlet]
		UIKit.UISegmentedControl switchFluidState { get; set; }

		[Outlet]
    UIKit.UIView viewFluidColor {
      get {
        return p;
      }
      set {
        ION.Core.Util.Log.D(this, "setting label to " + p?.Description);
        p = value;
      }
    } UIKit.UIView p;

		[Outlet]
		UIKit.UIView viewFluidHeader { get; set; }

		[Outlet]
		UIKit.UIView viewPressureSection { get; set; }

		[Outlet]
		UIKit.UIView viewPressureTouchArea { get; set; }

		[Outlet]
		UIKit.UIView viewTemperatureSection { get; set; }

		[Outlet]
		UIKit.UIView viewTemperatureTouchArea { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (viewFluidHeader != null) {
				viewFluidHeader.Dispose ();
				viewFluidHeader = null;
			}

			if (viewFluidColor != null) {
				viewFluidColor.Dispose ();
				viewFluidColor = null;
			}

			if (labelFluidName != null) {
				labelFluidName.Dispose ();
				labelFluidName = null;
			}

			if (switchFluidState != null) {
				switchFluidState.Dispose ();
				switchFluidState = null;
			}

			if (viewPressureSection != null) {
				viewPressureSection.Dispose ();
				viewPressureSection = null;
			}

			if (imagePressureLock != null) {
				imagePressureLock.Dispose ();
				imagePressureLock = null;
			}

			if (imagePressureIcon != null) {
				imagePressureIcon.Dispose ();
				imagePressureIcon = null;
			}

			if (buttonPressureUnit != null) {
				buttonPressureUnit.Dispose ();
				buttonPressureUnit = null;
			}

			if (editPressure != null) {
				editPressure.Dispose ();
				editPressure = null;
			}

			if (labelPressure != null) {
				labelPressure.Dispose ();
				labelPressure = null;
			}

			if (viewPressureTouchArea != null) {
				viewPressureTouchArea.Dispose ();
				viewPressureTouchArea = null;
			}

			if (viewTemperatureSection != null) {
				viewTemperatureSection.Dispose ();
				viewTemperatureSection = null;
			}

			if (viewTemperatureTouchArea != null) {
				viewTemperatureTouchArea.Dispose ();
				viewTemperatureTouchArea = null;
			}

			if (imageTemperatureLock != null) {
				imageTemperatureLock.Dispose ();
				imageTemperatureLock = null;
			}

			if (imageTemperatureIcon != null) {
				imageTemperatureIcon.Dispose ();
				imageTemperatureIcon = null;
			}

			if (buttonTemperatureUnit != null) {
				buttonTemperatureUnit.Dispose ();
				buttonTemperatureUnit = null;
			}

			if (editTemperature != null) {
				editTemperature.Dispose ();
				editTemperature = null;
			}

			if (labelTemperature != null) {
				labelTemperature.Dispose ();
				labelTemperature = null;
			}
		}
	}
}
