// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace ION.IOS.ViewController.Settings
{
	[Register ("SettingsNavigationItemCell")]
	partial class SettingsNavigationItemCell
	{
		[Outlet]
		UIKit.UIView labelTitle { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (labelTitle != null) {
				labelTitle.Dispose ();
				labelTitle = null;
			}
		}
	}
}