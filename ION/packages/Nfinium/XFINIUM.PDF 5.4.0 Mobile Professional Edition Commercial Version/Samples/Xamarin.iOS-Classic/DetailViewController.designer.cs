// WARNING
//
// This file has been generated automatically by MonoDevelop to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;

namespace Xfinium.Pdf.SamplesExplorer.Xamarin.iOS
{
	[Register ("DetailViewController")]
	partial class DetailViewController
	{
		[Action ("viewSourceCodeClick:")]
		partial void viewSourceCodeClick (global::MonoTouch.Foundation.NSObject sender);

		[Action ("runSampleClick:")]
		partial void runSampleClick (global::MonoTouch.Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (sampleDescriptionLabel != null) {
				sampleDescriptionLabel.Dispose ();
				sampleDescriptionLabel = null;
			}
		}
	}
}
