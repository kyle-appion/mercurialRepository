
using System;

using Foundation;
using UIKit;

namespace ION.IOS.ViewController.Navigation {
  public partial class NavigationItem : UITableViewCell {
    public static readonly NSString Key = new NSString("NavigationItem");
    public static readonly UINib Nib;

    static NavigationItem() {
      Nib = UINib.FromName("NavigationItem", NSBundle.MainBundle);
    }

    public NavigationItem(IntPtr handle) : base(handle) {
    }

    public static NavigationItem Create() {
      return (NavigationItem)Nib.Instantiate(null, null)[0];
    }
  }
}

