using System;
using System.Collections.Generic;
using CoreGraphics;
using Foundation;
using UIKit; 

namespace ION.IOS {
  public class LoggingTableSource : UITableViewSource {

    public LoggingTableSource() {
      
    }

    // Overridden from UITableViewSource
    public override nint NumberOfSections(UITableView tableView) {
      return 1;
    }

    // Overridden from UITableViewSource
    public override nint RowsInSection(UITableView tableview, nint section) {
      return 1;
    }

    // Overriden from UITableViewSource
    public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath) {
        return (nfloat)44;
    }
    // Overriden from UITableViewSource
    public override UIView GetViewForHeader(UITableView tableView, nint section) {
      return new UIView(new CGRect(0,0,0,0));
    }
    // Overriden from UITableViewSource
    public override UIView GetViewForFooter (UITableView tableView, nint section)
    {
      return new UIView(new CGRect(0,0,0,0));
    }

    public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath) {
      return null;
    }


  }
}

