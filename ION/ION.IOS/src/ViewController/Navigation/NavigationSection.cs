
using System;

using Foundation;
using UIKit;

namespace ION.IOS.ViewController.Navigation {
  public partial class NavigationSection : UITableViewCell {
    public static readonly NSString Key = new NSString("NavigationSection");
    public static readonly UINib Nib;

    static NavigationSection() {
      Nib = UINib.FromName("NavigationSection", NSBundle.MainBundle);
    }

    public NavigationSection(IntPtr handle) : base(handle) {
    }

    public static NavigationSection Create() {
      return (NavigationSection)Nib.Instantiate(null, null)[0];
    }
  }
}

