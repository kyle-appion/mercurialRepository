using System;

using CoreGraphics;
using Foundation;
using UIKit;
using ObjCRuntime;

using ION.IOS.Util;
using ION.IOS.App;
using ION.Core.App;

namespace ION.IOS {
  public partial class IONRemoteWirelessCell : UITableViewCell {
    public static readonly NSString Key = new NSString("IONRemoteWirelessCell");
    public static readonly UINib Nib;
    public IosION ion;
    
    static IONRemoteWirelessCell() {
      Nib = UINib.FromName("IONRemoteWirelessCell", NSBundle.MainBundle);
    }
    
    /// <summary>
    /// Creates a new cell.
    /// </summary>
    public static IONRemoteWirelessCell Create() {
      var arr = NSBundle.MainBundle.LoadNib("IONRemoteWirelessCell", null, null);
      return Runtime.GetNSObject<IONRemoteWirelessCell>(arr.ValueAt(0));
    }

    protected IONRemoteWirelessCell(IntPtr handle) : base(handle) {
    
    }
    
    public void UpdateTo(string title, UIImage icon, UIColor textColor) {
      ion = AppState.context as IosION;
      ion.remotePlatformChanged += updateWireless;
    
      this.BackgroundColor = UIColor.Clear;
      this.Layer.BorderWidth = 2f;
      this.Layer.BorderColor = UIColor.FromRGB(255,30,30).CGColor;
      cellImage.BackgroundColor = UIColor.Clear;
      
      if(ion.remoteDevice != null && ion.remoteDevice.wifiConnected){
        cellImage.Image = UIImage.FromBundle("wireless_connected");
        labelTitle.Text = "Connected";
      } else {
        cellImage.Image = UIImage.FromBundle("wireless_disconnected");
        labelTitle.Text = "Disconnected";
      }      
      
      if(textColor != null){
        labelTitle.TextColor = textColor;
      }
    }

    public void updateWireless(IPlatformInfo remoteInfo){
        if(ion.remoteDevice != null && ion.remoteDevice.wifiConnected){
        cellImage.Image = UIImage.FromBundle("wireless_connected");
        labelTitle.Text = "Connected";
      } else {
        cellImage.Image = UIImage.FromBundle("wireless_disconnected");
        labelTitle.Text = "Disconnected";
      }      
    }   
  }
}
