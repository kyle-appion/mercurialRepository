// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace ION.IOS.ViewController.Workbench
{
	[Register ("RateofChangeSettingsViewController")]
	partial class RateofChangeSettingsViewController
	{
		[Outlet]
		UIKit.UILabel BLMeasurement { get; set; }

		[Outlet]
		UIKit.UILabel BRMeasurement { get; set; }

		[Outlet]
		UIKit.UIView graphView { get; set; }

		[Outlet]
		UIKit.UIView legendView { get; set; }

		[Outlet]
		UIKit.UIImageView linkedLegendImage { get; set; }

		[Outlet]
		UIKit.UILabel linkedLegendLabel { get; set; }

		[Outlet]
		UIKit.UIImageView primaryLegendImage { get; set; }

		[Outlet]
		UIKit.UILabel primaryLegendLabel { get; set; }

		[Outlet]
		UIKit.UILabel TLMeasurement { get; set; }

		[Outlet]
		UIKit.UILabel trendInfoHeader { get; set; }

		[Outlet]
		UIKit.UILabel trendInfoInterval { get; set; }

		[Outlet]
		UIKit.UILabel trendIntervalSettings { get; set; }

		[Outlet]
		UIKit.UILabel TRMeasurement { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (graphView != null) {
				graphView.Dispose ();
				graphView = null;
			}

			if (legendView != null) {
				legendView.Dispose ();
				legendView = null;
			}

			if (TLMeasurement != null) {
				TLMeasurement.Dispose ();
				TLMeasurement = null;
			}

			if (TRMeasurement != null) {
				TRMeasurement.Dispose ();
				TRMeasurement = null;
			}

			if (BRMeasurement != null) {
				BRMeasurement.Dispose ();
				BRMeasurement = null;
			}

			if (BLMeasurement != null) {
				BLMeasurement.Dispose ();
				BLMeasurement = null;
			}

			if (primaryLegendLabel != null) {
				primaryLegendLabel.Dispose ();
				primaryLegendLabel = null;
			}

			if (linkedLegendLabel != null) {
				linkedLegendLabel.Dispose ();
				linkedLegendLabel = null;
			}

			if (primaryLegendImage != null) {
				primaryLegendImage.Dispose ();
				primaryLegendImage = null;
			}

			if (linkedLegendImage != null) {
				linkedLegendImage.Dispose ();
				linkedLegendImage = null;
			}

			if (trendInfoHeader != null) {
				trendInfoHeader.Dispose ();
				trendInfoHeader = null;
			}

			if (trendInfoInterval != null) {
				trendInfoInterval.Dispose ();
				trendInfoInterval = null;
			}

			if (trendIntervalSettings != null) {
				trendIntervalSettings.Dispose ();
				trendIntervalSettings = null;
			}
		}
	}
}
