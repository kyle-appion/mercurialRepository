namespace ION.IOS.ViewController.ScreenshotReport {

  using System;
  using System.Collections.Generic;

  using Foundation;
  using UIKit; 

  public class ScreenshotReportSource : UITableViewSource {

    private const string CELL_DISPLAY = "cellDisplay";
    private const string CELL_ENTRY = "cellEntry";
    private const string CELL_SELECTION = "cellSelection";
    private const string CELL_NOTES = "cellNotes";

    private List<IItem> items { get; set; }

    public ScreenshotReportSource(List<IItem> items) {
      this.items = items;
    }

    // Overridden from UITableViewSource
    public override nint NumberOfSections(UITableView tableView) {
      return 1;
    }

    // Overridden from UITableViewSource
    public override nint RowsInSection(UITableView tableview, nint section) {
      return items.Count;
    }

    // Overriden from UITableViewSource
    public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath) {
      var item = items[(int)indexPath.Row];
      if (item is NotesItem) {
        return (nfloat)100;
      } else {
        return (nfloat)44;
      }
    }

    // Overridden from UITableViewSource
    public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath) {
      var item = items[(int)indexPath.Row];

      if (item is DisplayItem) {
        var cell = tableView.DequeueReusableCell(CELL_DISPLAY) as DisplayCell;
        cell.UpdateFrom(item as DisplayItem);
        return cell;
      } else if (item is EntryItem) {
        var cell = tableView.DequeueReusableCell(CELL_ENTRY) as EntryCell;
        cell.UpdateFrom(item as EntryItem);
        return cell;
      } else if (item is SelectionCell) {
        var cell = tableView.DequeueReusableCell(CELL_SELECTION) as SelectionCell;
        cell.UpdateFrom(item as SelectionItem);
        return cell;
      } else if (item is NotesItem) {
        var cell = tableView.DequeueReusableCell(CELL_NOTES) as NotesCell;        
        cell.UpdateFrom(item as NotesItem);
        return cell;
      } else {
        throw new Exception("Cannot handle item " + item);
      }
    }
  }

  public interface IItem {
    string header { get; }
    string value { get; set; }
  }

  public abstract class AbstractItem : IItem {
    public string header { get; }
    public string value { get; set; }

    public AbstractItem(string header) {
      this.header = header;
      this.value = "";
    }

    public AbstractItem(string header, string value) {
      this.header = header;
      this.value = value;
    }
  }

  public class DisplayItem : AbstractItem {
    public DisplayItem(string header, string value) : base(header, value) {
    }
  }

  public class EntryItem : AbstractItem {
    public EntryItem(string header) : base(header) {
    }
    public EntryItem(string header, string value) : base(header, value) {
    }
  }

  public class SelectionItem : AbstractItem {
    public string optionsTitle { get; set; }
    public string[] options { get; set; }

    public SelectionItem(string header, string value, string title, string[] options) : base(header, value) {
      this.optionsTitle = title;
      this.options = options;
    }
  }

  public class NotesItem : AbstractItem {
    public NotesItem(string header) : base(header) {
    }
    public NotesItem(string header, string value) : base(header, value) {
    }
  }
}

