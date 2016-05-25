using System;
using System.Collections.Generic;
using CoreGraphics;
using Foundation;
using UIKit;

namespace ION.IOS.ViewController.Walkthrough {
  public class WalkthroughMenuView {
    public UIView MenuView;
    public UITableView menuTable;

    public WalkthroughMenuView(UIViewController parentVC,List<string> sections) {
      MenuView = new UIView(new CGRect(0,0,parentVC.View.Bounds.Width, parentVC.View.Bounds.Height));

      menuTable = new UITableView(new CGRect(0,0,MenuView.Bounds.Width,MenuView.Bounds.Height));
      menuTable.SeparatorStyle = UITableViewCellSeparatorStyle.None;
      menuTable.Bounces = false;
      menuTable.Source = new WalkthroughTableSource(sections,parentVC);

      MenuView.AddSubview(menuTable);
    }
  }
}

