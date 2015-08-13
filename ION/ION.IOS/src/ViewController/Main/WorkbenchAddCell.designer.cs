// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace ION.IOS.ViewController.Main
{
	[Register ("WorkbenchAddCell")]
	partial class WorkbenchAddCell
	{
		[Outlet]
		public ION.IOS.UI.NinePatchButtonView buttonAdd { get; private set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (buttonAdd != null) {
				buttonAdd.Dispose ();
				buttonAdd = null;
			}
		}
	}
}
