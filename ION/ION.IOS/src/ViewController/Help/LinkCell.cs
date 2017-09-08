namespace ION.IOS.ViewController.Help {

  using System;

  using Foundation;
  using UIKit;

	public partial class LinkCell : UITableViewCell {
		public LinkCell (IntPtr handle) : base (handle) {
		}

    public void UpdateFrom(LinkHelpItem item) {
      labelHeader.Text = item.title;
    }
	}
}
