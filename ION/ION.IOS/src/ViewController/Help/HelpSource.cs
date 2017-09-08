namespace ION.IOS.ViewController.Help {

  using System;

  using Foundation;
  using UIKit;

  public class HelpSource : UITableViewSource {

    private const string CELL_LINK = "cellLink";
    private const string CELL_INFO = "cellInfo";

    private HelpViewController viewController { get; set; }

    private HelpPage page { get; set; }

    public HelpSource(HelpViewController viewController, HelpPage page) {
      this.viewController = viewController;
      this.page = page;
    }

    // Overridden from UITableViewSource
    public override nint NumberOfSections(UITableView table) {
      return 1;
    }

    // Overridden from UITableViewSource
    public override nint RowsInSection(UITableView tableview, nint section) {
      return page.count;
    }

    // Overriddenf from UITableViewSource
    public override void RowSelected(UITableView tableView, NSIndexPath indexPath) {
      page[(int)indexPath.Row].PerformClick(viewController);
    }

    public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath) {
      var item = page[(int)indexPath.Row];

      if (item is LinkHelpItem) {
        var link = item as LinkHelpItem;

        var cell = tableView.DequeueReusableCell(CELL_LINK) as LinkCell;
				cell.SelectionStyle = UITableViewCellSelectionStyle.None;
        cell.UpdateFrom(link);

        return cell;
      } else if (item is InfoHelpItem) {
        var info = item as InfoHelpItem;

        var cell = tableView.DequeueReusableCell(CELL_INFO) as InfoCell;
				cell.SelectionStyle = UITableViewCellSelectionStyle.None;
        cell.UpdateFrom(info);

        return cell;
      } else {
        throw new Exception("Unsupported IHelpItem " + item);
      }
    }
  }
}

