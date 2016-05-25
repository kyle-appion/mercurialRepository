using System;
using System.Collections.Generic;
using CoreGraphics;
using UIKit;
using Foundation;

using ION.Core.App;
using ION.Core.Content;
using ION.Core.Devices;
using ION.Core.Util;
using ION.Core.Sensors;
using ION.Core.Sensors.Properties;
using ION.Core.Fluids;
using ION.Core.Measure;
using ION.Core.App;

namespace ION.IOS.ViewController.Walkthrough {
  public class WalkthroughTableSource : UITableViewSource {

    public List<string> walkthroughs;
    public UIViewController pushVC;

    public WalkthroughTableSource(List<string> sections, UIViewController parentVC) {
      walkthroughs = sections;
      pushVC = parentVC;
    }

    // Overriden from UITableViewSource
    public override UIView GetViewForHeader(UITableView tableView, nint section) {
      //return new UIView(new CGRect(0,0,0,0)); 
      return null;
    }
    // Overriden from UITableViewSource
    public override UIView GetViewForFooter (UITableView tableView, nint section)
    {
      return new UIView (new CGRect(0,0,0,0)); 
    }

    public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath) {
      return .1f * tableView.Bounds.Width;
    }

    // Overridden from UITableViewSource
    public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath) {
      return false;
    }
 

    // Overridden from UITableViewSource
    public override nint NumberOfSections(UITableView tableView) {
      return 1;
    }

    // Overridden from UITableViewSource
    public override nint RowsInSection(UITableView tableView, nint section) {
      return walkthroughs.Count;
    }

    // Overridden from UITableViewSource
    public override void RowSelected(UITableView tableView, NSIndexPath indexPath) {      
      Console.WriteLine("Selected row: " + walkthroughs[indexPath.Row]);
      var svc = (WalkthroughScreenshotViewController)pushVC.Storyboard.InstantiateViewController("viewControllerWalkthroughScreenshot");
      svc.sectionName = walkthroughs[indexPath.Row];
      pushVC.NavigationController.PushViewController(svc,true);
    }

    // Overridden from UITableViewSource
    public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath) {
      var cell = tableView.DequeueReusableCell("walkthroughCell");
      if (cell == null) {
        cell = new UITableViewCell(UITableViewCellStyle.Default, "walkthroughCell");
      }     
        
      cell.TextLabel.Text = walkthroughs[indexPath.Row];
      cell.TextLabel.Font = UIFont.BoldSystemFontOfSize(20);
      cell.Layer.BorderWidth = 1f;
      return cell;
    }
  }
}

