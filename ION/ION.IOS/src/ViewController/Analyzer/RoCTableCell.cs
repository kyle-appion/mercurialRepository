using System;
using Foundation;
using UIKit;
using CoreGraphics;
using System.Threading.Tasks;
using ION.Core.Measure;
using ION.Core.Sensors;
using ION.Core.Sensors.Properties;
using ION.IOS.Util;

namespace ION.IOS.ViewController.Analyzer {
  
  public partial class RoCTableCell : UITableViewCell {
    
    //private UILabel cellHeader = new UILabel(new CGRect(0,0,149, 30));
    private UILabel cellHeader;
    //private UILabel cellReading = new UILabel(new CGRect(30,30,119,30));
    private UILabel cellReading;
    //private UIImageView cellImage = new UIImageView(new CGRect(0,30,30,30));
    private UIImageView cellImage;

    public RoCTableCell(IntPtr handle) {
     
    }

    public void makeEvents(lowHighSensor lhSensor, CGRect tableRect){
      cellHeader = new UILabel(new CGRect(0, 0, 1.006 * tableRect.Width, .5 * lhSensor.cellHeight));
      //cellReading = new UILabel(new CGRect(.2 * tableRect.Width, .5 * lhSensor.cellHeight, .8 * tableRect.Width, .5 * lhSensor.cellHeight));
      cellImage = new UIImageView(new CGRect(0, .5 * lhSensor.cellHeight, .2 * tableRect.Width, .5 * lhSensor.cellHeight));

      cellHeader.Text = "RoC";
      cellHeader.TextColor = UIColor.White;
      cellHeader.BackgroundColor = UIColor.Black;
      cellHeader.Font = UIFont.FromName("Helvetica-Bold", 21f);
      cellHeader.TextAlignment = UITextAlignment.Center;
      cellHeader.AdjustsFontSizeToFitWidth = true;

      cellReading = lhSensor.rocReading;
      cellReading.Text = Util.Strings.Analyzer.STABLE;
      cellReading.TextAlignment = UITextAlignment.Right;
      cellReading.Font = UIFont.FromName("Helvetica-Bold", 18f);
      cellReading.AdjustsFontSizeToFitWidth = true;
      cellReading.Layer.BorderColor = UIColor.Black.CGColor;
      cellReading.Layer.BorderWidth = 1f;

      cellImage = lhSensor.rocImage;
      cellImage.Layer.BorderColor = UIColor.Black.CGColor;
      cellImage.Layer.BorderWidth = 1f;    

      this.AddSubview(cellHeader);
      this.AddSubview(cellReading);
      this.AddSubview(cellImage);
    }

  }
}

