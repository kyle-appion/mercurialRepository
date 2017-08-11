// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace ION.IOS.ViewController.DeviceGrid
{
	[Register ("DeviceGridViewController")]
	partial class DeviceGridViewController
	{
		[Outlet]
		UIKit.UICollectionView gridView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (gridView != null) {
				gridView.Dispose ();
				gridView = null;
			}
		}
	}
}
