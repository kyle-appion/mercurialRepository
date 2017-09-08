namespace ION.IOS.ViewController.Help {
  using Foundation;
  using System.CodeDom.Compiler;

	[Register ("HelpViewController")]
	partial class HelpViewController
	{
		[Outlet]
		UIKit.UITableView table { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (table != null) {
				table.Dispose ();
				table = null;
			}
		}
	}
}
