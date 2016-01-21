namespace ION.IOS {

  using System;

  using CoreGraphics;
  using Foundation;
  using UIKit;
  using ObjCRuntime;

  using ION.IOS.Util;

  public partial class IONNavigationCell : UITableViewCell {
    public static readonly NSString Key = new NSString("IONNavigationCell");
    public static readonly UINib Nib;

    static IONNavigationCell() {
      Nib = UINib.FromName("IONNavigationCell", NSBundle.MainBundle);
    }

    /// <summary>
    /// Creates a new cell.
    /// </summary>
    public static IONNavigationCell Create() {
      var arr = NSBundle.MainBundle.LoadNib("IONNavigationCell", null, null);
      return Runtime.GetNSObject<IONNavigationCell>(arr.ValueAt(0));
    }

    public IONNavigationCell(IntPtr handle) : base(handle) {
    }

    public void UpdateTo(string title, UIImage icon) {
      labelTitle.Text = title;
      imageIcon.Image = icon;
      imageIcon.TintColor = new UIColor(Colors.LIGHT_GRAY);
    }
  }
}
