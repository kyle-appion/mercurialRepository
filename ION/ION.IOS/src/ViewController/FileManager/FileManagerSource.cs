namespace ION.IOS.ViewController.FileManager {

  using System;
  using System.Collections.Generic;

  using Foundation;
  using UIKit;

  using ION.Core.IO;

  using ION.IOS.Util;

  public class FileManagerSource : UITableViewSource {
    public delegate void OnFileRowClicked(IFile file, NSIndexPath indexPath);

    private const string CELL_FILE = "cellFile";

    public OnFileRowClicked onFileRowClicked { get; set; }
    /// <summary>
    /// The files that the source is presenting.
    /// </summary>
    /// <value>The files.</value>
    public List<IFile> files { get; private set; }

    public FileManagerSource(List<IFile> files) {
      this.files = files;
    }

    // Override from UITableViewSource
    public override void CommitEditingStyle(UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath) {
      switch (editingStyle) {
        case UITableViewCellEditingStyle.Delete:
          var index = (int)indexPath.Row;
          files[index].Delete();
          files.RemoveAt(index);
          tableView.DeleteRows(new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Left);
          tableView.ReloadData();
          break;
      }
    }

    // Overridden from UITableViewSource
    public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath) {
      return true;
    }

    // Overridden from UITableViewSource
    public override string TitleForDeleteConfirmation(UITableView tableView, NSIndexPath indexPath) {
      return Strings.DELETE_QUESTION;
    }

    // Overridden from UITableViewSource
    public override nint NumberOfSections(UITableView tableView) {
      return 1;
    }

    // Overridden from UITableViewSource
    public override nint RowsInSection(UITableView tableView, nint section) {
      return files.Count;
    }

    // Overridden from UITableViewSource
    public override void RowSelected(UITableView tableView, NSIndexPath indexPath) {
      if (onFileRowClicked != null) {
        onFileRowClicked(files[indexPath.Row], indexPath);
      }
    }

    // Overridden from UITableViewSource
    public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath) {
      var cell = tableView.DequeueReusableCell(CELL_FILE) as FileCell;

      if (cell != null) {
        var file = files[indexPath.Row];

        cell.UpdateFrom("ic_pdf", file.name);
      }

      return cell;
    }
  }
}

