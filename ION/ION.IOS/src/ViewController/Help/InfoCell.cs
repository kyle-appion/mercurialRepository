namespace ION.IOS.ViewController.Help {

  using System;

  using Foundation;
  using UIKit;

  public partial class InfoCell : UITableViewCell {
    public InfoCell (IntPtr handle) : base (handle) {
		}

    public void UpdateFrom(InfoHelpItem item) {
      labelHeader.Text = item.title;
      labelDescription.Text = item.subtitle;
    }
	}
}
