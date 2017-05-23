// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace ION.IOS.ViewController.Analyzer
{
	[Register ("AnalyzerViewController")]
	partial class AnalyzerViewController
	{
		[Outlet]
		UIKit.UIView viewAnalyzerContainer { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (viewAnalyzerContainer != null) {
				viewAnalyzerContainer.Dispose ();
				viewAnalyzerContainer = null;
			}
		}
	}
}
