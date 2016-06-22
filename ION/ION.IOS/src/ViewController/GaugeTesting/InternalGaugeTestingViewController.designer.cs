// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace ION.IOS.ViewController.GaugeTesting
{
	[Register ("InternalGaugeTestingViewController")]
	partial class InternalGaugeTestingViewController
	{
		[Outlet]
		DSoft.UI.Grid.DSGridView gridView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (gridView != null) {
				gridView.Dispose ();
				gridView = null;
			}
		}
	}
}
