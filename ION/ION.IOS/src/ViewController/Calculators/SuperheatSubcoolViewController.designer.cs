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
	[Register ("SuperheatSubcoolViewController")]
	partial class SuperheatSubcoolViewController
	{
		[Outlet]
		public UIKit.UIButton buttonMeasTempUnit { get; private set; }

		[Outlet]
		public UIKit.UIButton buttonPressureUnit { get; private set; }

		[Outlet]
		public UIKit.UITextField editMeasTemp { get; private set; }

		[Outlet]
		public UIKit.UITextField editPressure { get; private set; }

		[Outlet]
		public UIKit.UIImageView imageMeasTempIcon { get; private set; }

		[Outlet]
		public UIKit.UIImageView imageMeasTempLock { get; private set; }

		[Outlet]
		public UIKit.UIImageView imagePressureIcon { get; private set; }

		[Outlet]
		public UIKit.UIImageView imagePressureLock { get; private set; }

		[Outlet]
		public UIKit.UILabel labelFluidName { get; private set; }

		[Outlet]
		public UIKit.UILabel labelFluidState { get; private set; }

		[Outlet]
		public UIKit.UILabel labelFluidStateDelta { get; private set; }

		[Outlet]
		public UIKit.UILabel labelMeasTemp { get; private set; }

		[Outlet]
		public UIKit.UILabel labelPressure { get; private set; }

		[Outlet]
		public UIKit.UILabel labelSatTemp { get; private set; }

		[Outlet]
		public UIKit.UILabel labelSatTempDisplay { get; private set; }

		[Outlet]
		public UIKit.UILabel labelSatTempUnit { get; private set; }

		[Outlet]
		public UIKit.UISegmentedControl switchDewBubble { get; private set; }

		[Outlet]
		public UIKit.UIView viewFluidColor { get; private set; }

		[Outlet]
		public UIKit.UIView viewFluidPicker { get; private set; }

		[Outlet]
		public UIKit.UIView viewMeasTempSensorPicker { get; set; }

		[Outlet]
		public UIKit.UIView viewPressureSensorPicker { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (viewPressureSensorPicker != null) {
				viewPressureSensorPicker.Dispose ();
				viewPressureSensorPicker = null;
			}

			if (viewMeasTempSensorPicker != null) {
				viewMeasTempSensorPicker.Dispose ();
				viewMeasTempSensorPicker = null;
			}

			if (buttonMeasTempUnit != null) {
				buttonMeasTempUnit.Dispose ();
				buttonMeasTempUnit = null;
			}

			if (buttonPressureUnit != null) {
				buttonPressureUnit.Dispose ();
				buttonPressureUnit = null;
			}

			if (editMeasTemp != null) {
				editMeasTemp.Dispose ();
				editMeasTemp = null;
			}

			if (editPressure != null) {
				editPressure.Dispose ();
				editPressure = null;
			}

			if (imageMeasTempIcon != null) {
				imageMeasTempIcon.Dispose ();
				imageMeasTempIcon = null;
			}

			if (imageMeasTempLock != null) {
				imageMeasTempLock.Dispose ();
				imageMeasTempLock = null;
			}

			if (imagePressureIcon != null) {
				imagePressureIcon.Dispose ();
				imagePressureIcon = null;
			}

			if (imagePressureLock != null) {
				imagePressureLock.Dispose ();
				imagePressureLock = null;
			}

			if (labelFluidName != null) {
				labelFluidName.Dispose ();
				labelFluidName = null;
			}

			if (labelFluidState != null) {
				labelFluidState.Dispose ();
				labelFluidState = null;
			}

			if (labelFluidStateDelta != null) {
				labelFluidStateDelta.Dispose ();
				labelFluidStateDelta = null;
			}

			if (labelMeasTemp != null) {
				labelMeasTemp.Dispose ();
				labelMeasTemp = null;
			}

			if (labelPressure != null) {
				labelPressure.Dispose ();
				labelPressure = null;
			}

			if (labelSatTemp != null) {
				labelSatTemp.Dispose ();
				labelSatTemp = null;
			}

			if (labelSatTempDisplay != null) {
				labelSatTempDisplay.Dispose ();
				labelSatTempDisplay = null;
			}

			if (labelSatTempUnit != null) {
				labelSatTempUnit.Dispose ();
				labelSatTempUnit = null;
			}

			if (switchDewBubble != null) {
				switchDewBubble.Dispose ();
				switchDewBubble = null;
			}

			if (viewFluidColor != null) {
				viewFluidColor.Dispose ();
				viewFluidColor = null;
			}

			if (viewFluidPicker != null) {
				viewFluidPicker.Dispose ();
				viewFluidPicker = null;
			}
		}
	}
}
