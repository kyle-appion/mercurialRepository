using System;

using CoreGraphics;
using Foundation;
using UIKit;
using ObjCRuntime;

using Appion.Commons.Util;

using ION.IOS.Util;
using ION.Core.App;
using ION.IOS.App;

namespace ION.IOS {
  public partial class IONRemoteBatteryCell : UITableViewCell {
    public static readonly NSString Key = new NSString("IONRemoteBatteryCell");
    public static readonly UINib Nib;
    public int lastBattery = 0;
    
    static IONRemoteBatteryCell() {
      Nib = UINib.FromName("IONRemoteBatteryCell", NSBundle.MainBundle);
    }
    
    /// <summary>
    /// Creates a new cell.
    /// </summary>
    public static IONRemoteBatteryCell Create() {
      var arr = NSBundle.MainBundle.LoadNib("IONRemoteBatteryCell", null, null);
      return Runtime.GetNSObject<IONRemoteBatteryCell>(arr.ValueAt(0));
    }

    protected IONRemoteBatteryCell(IntPtr handle) : base(handle) {
    
    }
    
    public void UpdateTo(string title, UIImage icon, UIColor textColor) {
      var ion = AppState.context as RemoteIosION;
      if ((ion != null).Assert(GetType().Name + " was told to update with with a local ion instance")) {
        return;
      }
      
      var pi = ion.remotePlatformInfo;
      
      ion.onRemoteUpdateEvent += updateBattery;
    
      this.BackgroundColor = UIColor.Clear;
      this.Layer.BorderWidth = 2f;
      this.Layer.BorderColor = UIColor.FromRGB(255,30,30).CGColor;
      
      updateBattery();
      
      if(textColor != null) {
        labelTitle.TextColor = textColor;
      }
    }

    public void updateBattery() {
      var ion = AppState.context as RemoteIosION;
      if ((ion != null).Assert(GetType().Name + " was told to update with with a local ion instance")) {
        return;
      }
      
      var pi = ion.remotePlatformInfo;
      
      if(pi != null){
        if(pi.batteryPercentage != lastBattery){
          lastBattery = pi.batteryPercentage;
        
          if (pi.batteryPercentage >= 100) {
            cellImage.Image = UIImage.FromBundle("img_battery_100");
          } else if (pi.batteryPercentage >= 75) {
            cellImage.Image = UIImage.FromBundle("img_battery_75");
          } else if (pi.batteryPercentage >= 50) {
            cellImage.Image = UIImage.FromBundle("img_battery_50");
          } else if (pi.batteryPercentage >= 25) {
            cellImage.Image = UIImage.FromBundle("img_battery_25");
          } else if (pi.batteryPercentage >= 0) {
            cellImage.Image = UIImage.FromBundle("img_battery_0");
          } else {
            cellImage.Hidden = true;
          }
          labelTitle.Text = pi.batteryPercentage + "%";
        }
      } else {
        cellImage.Hidden = true;
        labelTitle.Text = "N/A";
      }
     }    
  }
}
