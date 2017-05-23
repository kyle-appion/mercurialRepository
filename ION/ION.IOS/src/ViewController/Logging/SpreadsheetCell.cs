using System;
using System.Collections.Generic;
using UIKit;
using Foundation;
using CoreGraphics;

namespace ION.IOS.ViewController.Logging {
  public class SpreadsheetCell : UITableViewCell {
    public SpreadsheetCell(IntPtr handle) {
    }
    UIImageView documentImage;
    UILabel documentLabel;

    public void setupTable(string imageName, double cellWidth, string fileName){
      documentImage = new UIImageView(new CGRect(0,.005 * cellWidth,.09 * cellWidth,.09 * cellWidth));
      documentImage.Layer.CornerRadius = 8f;
      documentLabel = new UILabel(new CGRect(.1 * cellWidth,0,.9 * cellWidth, .1 * cellWidth));

      var splits = fileName.Split('/');
      var name = splits[splits.Length - 1];

      documentImage.Image = UIImage.FromBundle(imageName);
      documentLabel.Text = name;
      this.AddSubview(documentImage);
      this.AddSubview(documentLabel);
    }
  }
}

