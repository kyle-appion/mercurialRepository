// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using System;
using Foundation;
using UIKit;
using System.CodeDom.Compiler;

namespace PDFToImage
{
	[Register ("PDFToImageViewController")]
	partial class PDFToImageViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton ConvertPageButton { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIImageView PageImageView { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel pageNumberLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIStepper PageNumberSelector { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIActivityIndicatorView PageRenderingIndicator { get; set; }

		[Action ("ConvertPageButtonTouchUp:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void ConvertPageButtonTouchUp (UIButton sender);

		[Action ("PageNumberChanged:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void PageNumberChanged (UIStepper sender);

		void ReleaseDesignerOutlets ()
		{
			if (ConvertPageButton != null) {
				ConvertPageButton.Dispose ();
				ConvertPageButton = null;
			}
			if (PageImageView != null) {
				PageImageView.Dispose ();
				PageImageView = null;
			}
			if (pageNumberLabel != null) {
				pageNumberLabel.Dispose ();
				pageNumberLabel = null;
			}
			if (PageNumberSelector != null) {
				PageNumberSelector.Dispose ();
				PageNumberSelector = null;
			}
			if (PageRenderingIndicator != null) {
				PageRenderingIndicator.Dispose ();
				PageRenderingIndicator = null;
			}
		}
	}
}
