namespace ION.IOS.ViewController.ScreenshotReport{

  using System;

  using Foundation;
  using UIKit;

  public partial class DisplayCell : UITableViewCell {
    
    public DisplayCell(IntPtr handle) : base(handle) {
    }

    public void UpdateFrom(IItem item ) {
      labelHeader.Text = item.header;
      labelDisplay.Text = item.value;
    }
  }
}

