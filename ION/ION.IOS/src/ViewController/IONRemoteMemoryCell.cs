using System;

using CoreGraphics;
using Foundation;
using UIKit;
using ObjCRuntime;

using ION.IOS.Util;
using ION.IOS.App;
using ION.Core.App;

namespace ION.IOS {
  public partial class IONRemoteMemoryCell : UITableViewCell {
    public static readonly NSString Key = new NSString("IONRemoteMemoryCell");
    public static readonly UINib Nib;
    public IosION ion;
    
    static IONRemoteMemoryCell() {
      Nib = UINib.FromName("IONRemoteMemoryCell", NSBundle.MainBundle);
    }
    
    /// <summary>
    /// Creates a new cell.
    /// </summary>
    public static IONRemoteMemoryCell Create() {
      var arr = NSBundle.MainBundle.LoadNib("IONRemoteMemoryCell", null, null);
      return Runtime.GetNSObject<IONRemoteMemoryCell>(arr.ValueAt(0));
    }

    protected IONRemoteMemoryCell(IntPtr handle) : base(handle) {
    
    }
    
    public void UpdateTo(string title, UIImage icon, UIColor textColor) {
      ion = AppState.context as IosION;
      ion.remotePlatformChanged += updateMemory;
            
      this.BackgroundColor = UIColor.Clear;
      this.Layer.BorderWidth = 2f;
      this.Layer.BorderColor = UIColor.FromRGB(255,30,30).CGColor;
      
      if(ion.remoteDevice != null){
        labelTitle.Text = Math.Round((((ion.remoteDevice.freeMemory / 1024) / 1024) / 1000.0),3) + " GB ";
      } else {
        labelTitle.Text = "N/A";
      }
      
      if(textColor != null){
        labelTitle.TextColor = textColor;
      }
    }

    public void updateMemory(IPlatformInfo remoteInfo){
      if(ion.remoteDevice != null){
        labelTitle.Text = Math.Round((((ion.remoteDevice.freeMemory / 1024) / 1024) / 1000.0),3) + " GB ";
      } else {
        labelTitle.Text = "N/A";
      }
    }
  }
}
