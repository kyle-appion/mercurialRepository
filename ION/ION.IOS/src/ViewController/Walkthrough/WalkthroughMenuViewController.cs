using System;
using System.Collections.Generic;
using CoreGraphics;
using Foundation;
using UIKit; 

namespace ION.IOS.ViewController.Walkthrough {
  public partial class WalkthroughMenuViewController : BaseIONViewController {
    public WalkthroughMenuViewController(IntPtr handle) : base (handle)   {
    }

    public WalkthroughMenuView menuView;

    public override void ViewDidLoad() {
      base.ViewDidLoad();
      // Perform any additional setup after loading the view, typically from a nib.
      var sectionList = new List<string>();
      sectionList.Add("WorkBench");
      sectionList.Add("Analyzer");
      sectionList.Add("PT Chart");
      sectionList.Add("Superheat/Subcool");
      sectionList.Add("Calibration Certificates");
      sectionList.Add("Screenshot Archives");
      //sectionList.Add("Settings");

      menuView = new WalkthroughMenuView(this,sectionList);

      View.AddSubview(menuView.MenuView);
    }

    public override void DidReceiveMemoryWarning() {
      base.DidReceiveMemoryWarning();
      // Release any cached data, images, etc that aren't in use.
    }
  }
}


