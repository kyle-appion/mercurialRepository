using System;
using System.Drawing;

using Foundation;
using UIKit;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoreGraphics;
using Xfinium.Pdf.Rendering;
using Xfinium.Pdf;

namespace PDFToImage
{
	public partial class PDFToImageViewController : UIViewController
	{
		public PDFToImageViewController (IntPtr handle) : base (handle)
		{
		}

		private PdfFixedDocument document;
		int currentPageNumber = 0;

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		#region View lifecycle

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			// Perform any additional setup after loading the view, typically from a nib.
			document = new PdfFixedDocument (NSBundle.MainBundle.BundlePath + "/xfinium.pdf");
			PageNumberSelector.MaximumValue = document.Pages.Count - 1;
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);
		}

		public override void ViewDidDisappear (bool animated)
		{
			base.ViewDidDisappear (animated);
		}

		partial void PageNumberChanged (UIStepper sender)
		{
			this.pageNumberLabel.Text = "Page number: " + sender.Value.ToString();
			this.currentPageNumber = (int)sender.Value;
		}

		partial void ConvertPageButtonTouchUp (UIButton sender)
		{
			PageRenderingIndicator.StartAnimating();

			var t = Task<CGImage>.Factory.StartNew(() =>
				{
					CGImage pageImage = null;
					PdfPageRenderer renderer = new PdfPageRenderer(document.Pages[currentPageNumber]);
					pageImage = renderer.ConvertPageToImage(96);
					return pageImage;
				})
				.ContinueWith(value =>
					{
						PageImageView.Image = new UIImage(value.Result);
						PageRenderingIndicator.StopAnimating();
					}, TaskScheduler.FromCurrentSynchronizationContext());

		}

		#endregion
	}
}

