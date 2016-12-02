
using System;
using System.IO;
using System.Drawing;

using Foundation;
using UIKit;

namespace Xfinium.Pdf.SamplesExplorer.Xamarin.iOS
{
	public partial class SourceCodeViewController : UIViewController
	{
		public SourceCodeViewController () : base ("SourceCodeViewController", null)
		{
		}

		private SampleInfo sample;
		private UIWebView webView;

		public void SetSample (SampleInfo sample)
		{
			if (sample != null) {
				this.sample = sample;

				string localHtmlUrl = Path.Combine (NSBundle.MainBundle.BundlePath, "Support/" + sample.CSharpSourceCodeFile);
				if (webView != null) {
					webView.LoadRequest (new NSUrlRequest (new NSUrl (localHtmlUrl, false)));
					webView.ScalesPageToFit = false;
				}
			}
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			Title = "Source code";
			View.BackgroundColor = UIColor.White;
			
			webView = new UIWebView(View.Bounds);			
			View.AddSubview(webView);
			SetSample (sample);
		}
		
		public override void ViewDidUnload ()
		{
			base.ViewDidUnload ();
			
			// Clear any references to subviews of the main view in order to
			// allow the Garbage Collector to collect them sooner.
			//
			// e.g. myOutlet.Dispose (); myOutlet = null;
			
			ReleaseDesignerOutlets ();
		}
		
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			// Return true for supported orientations
			return (toInterfaceOrientation != UIInterfaceOrientation.PortraitUpsideDown);
		}
	}
}

