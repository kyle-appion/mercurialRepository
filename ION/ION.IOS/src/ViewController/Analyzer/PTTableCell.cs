using System;
using Foundation;
using UIKit;
using CoreGraphics;

namespace ION.IOS {
  public partial class PTTableCell : UITableViewCell {
    private UILabel cellHeader = new UILabel(new CGRect(0,0,149, 30));
    private UILabel fluidType = new UILabel(new CGRect(0,30,100,30));
    private UILabel tempReading = new UILabel(new CGRect(100,30,49,30));


    public PTTableCell(IntPtr handle) {
    }

    public void makeEvents(){
      cellHeader.Text = "PTdew";
      cellHeader.TextColor = UIColor.White;
      cellHeader.BackgroundColor = UIColor.Black;
      cellHeader.Font = UIFont.FromName("Helvetica-Bold", 21f);
      cellHeader.TextAlignment = UITextAlignment.Center;
      cellHeader.AdjustsFontSizeToFitWidth = true;

      fluidType.Text = "R22";
      fluidType.TextAlignment = UITextAlignment.Center;
      fluidType.BackgroundColor = UIColor.Green;
      fluidType.Font = UIFont.FromName("Helvetica", 18f);
      fluidType.AdjustsFontSizeToFitWidth = true;
      fluidType.Layer.BorderColor = UIColor.Black.CGColor;
      fluidType.Layer.BorderWidth = 1f;

      tempReading.Text = "-41.46" + "º";
      tempReading.AdjustsFontSizeToFitWidth = true;
      tempReading.TextAlignment = UITextAlignment.Center;

      this.AddSubview(cellHeader);
      this.AddSubview(fluidType);
      this.AddSubview(tempReading);

    }
  }
}

