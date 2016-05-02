﻿using System;
using System.Collections.Generic;
using CoreGraphics;
using Foundation;
using UIKit;

using ION.Core.IO;

using ION.IOS.Util;
using ION.IOS.ViewController.FileManager;

using QuickLook;

namespace ION.IOS.ViewController.Logging {
  public class SpreadsheetTableSource : UITableViewSource  {
    List<string> fileList;
    public SpreadsheetTableSource(List<string> fileNames) {
      fileList = fileNames;
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
    // Override from UITableViewSource
    public override void CommitEditingStyle(UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath) {
      switch (editingStyle) {
        case UITableViewCellEditingStyle.Delete:
          var index = (int)indexPath.Row;
          Console.WriteLine("Deleting from index: " + index);
//          files[index].Delete();
//          files.RemoveAt(index);

          //tableView.DeleteRows(new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Left);
          System.IO.File.Delete(fileList[index]);
          fileList.RemoveAt(indexPath.Row);
          tableView.ReloadData();
          break;
      }
    }

    public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath) {
      return .1f * tableView.Bounds.Width;
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
      return fileList.Count;
    }

    // Overridden from UITableViewSource
    public override void RowSelected(UITableView tableView, NSIndexPath indexPath) {      
      var window = UIApplication.SharedApplication.Windows[0].RootViewController;
      var vc = window;
      var splits = fileList[indexPath.Row].Split('/');
      var name = splits[splits.Length - 1];

      QLPreviewItemBundle prevItem = new QLPreviewItemBundle (name, fileList[indexPath.Row]);
      QLPreviewController previewController = new QLPreviewController ();
      previewController.DataSource = new PreviewControllerDS (prevItem);
      vc.PresentViewController (previewController, true, null);
    }

    // Overridden from UITableViewSource
    public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath) {
      var cell = tableView.DequeueReusableCell("spreadsheetCell") as SpreadsheetCell;
      if (cell == null) {
        cell = new UITableViewCell(UITableViewCellStyle.Default, "spreadsheetCell") as SpreadsheetCell;
      }

        var file = fileList[indexPath.Row];
         
        cell.setupTable("ic_excel", tableView.Bounds.Width,file);
      cell.Layer.BorderWidth = 1f;
      return cell;
    }
  }
}

