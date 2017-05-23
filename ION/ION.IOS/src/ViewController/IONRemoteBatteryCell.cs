using System;

using CoreGraphics;
using Foundation;
using UIKit;
using ObjCRuntime;

using ION.IOS.Util;
using ION.Core.App;
using ION.IOS.App;

namespace ION.IOS {
  public partial class IONRemoteBatteryCell : UITableViewCell {
    public static readonly NSString Key = new NSString("IONRemoteBatteryCell");
    public static readonly UINib Nib;
    public IosION ion;
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
      ion = AppState.context as IosION;
      ion.remotePlatformChanged += updateBattery;
    
      this.BackgroundColor = UIColor.Clear;
      this.Layer.BorderWidth = 2f;
      this.Layer.BorderColor = UIColor.FromRGB(255,30,30).CGColor;
      
      if(ion.remoteDevice != null){
        lastBattery = ion.remoteDevice.batteryPercentage;
        cellImage.Hidden = false;
        
        if (ion.remoteDevice.batteryPercentage >= 100) {
          cellImage.Image = UIImage.FromBundle("img_battery_100");
        } else if (ion.remoteDevice.batteryPercentage >= 75) {
          cellImage.Image = UIImage.FromBundle("img_battery_75");
        } else if (ion.remoteDevice.batteryPercentage >= 50) {
          cellImage.Image = UIImage.FromBundle("img_battery_50");
        } else if (ion.remoteDevice.batteryPercentage >= 25) {
          cellImage.Image = UIImage.FromBundle("img_battery_25");
        } else if (ion.remoteDevice.batteryPercentage >= 0) {
          cellImage.Image = UIImage.FromBundle("img_battery_0");
        } else {
          cellImage.Hidden = true;
        }
        labelTitle.Text = ion.remoteDevice.batteryPercentage + "% remaining";
      } else {
        cellImage.Hidden = true;
        labelTitle.Text = "N/A";
      }
      if(textColor != null){
        labelTitle.TextColor = textColor;
      }
    }

    public void updateBattery(IPlatformInfo remoteInfo){

      if(ion.remoteDevice != null){
        if(ion.remoteDevice.batteryPercentage != lastBattery){
          lastBattery = ion.remoteDevice.batteryPercentage;
        
          if (ion.remoteDevice.batteryPercentage >= 100) {
            cellImage.Image = UIImage.FromBundle("img_battery_100");
          } else if (ion.remoteDevice.batteryPercentage >= 75) {
            cellImage.Image = UIImage.FromBundle("img_battery_75");
          } else if (ion.remoteDevice.batteryPercentage >= 50) {
            cellImage.Image = UIImage.FromBundle("img_battery_50");
          } else if (ion.remoteDevice.batteryPercentage >= 25) {
            cellImage.Image = UIImage.FromBundle("img_battery_25");
          } else if (ion.remoteDevice.batteryPercentage >= 0) {
            cellImage.Image = UIImage.FromBundle("img_battery_0");
          } else {
            cellImage.Hidden = true;
          }
          labelTitle.Text = ion.remoteDevice.batteryPercentage + "%";
        }
      } else {
        cellImage.Hidden = true;
        labelTitle.Text = "N/A";
      }
     }    
  }
}
