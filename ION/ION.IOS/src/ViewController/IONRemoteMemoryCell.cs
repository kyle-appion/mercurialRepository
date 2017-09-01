using System;

using Foundation;
using UIKit;
using ObjCRuntime;

using Appion.Commons.Util;

using ION.IOS.App;
using ION.Core.App;

namespace ION.IOS {
  public partial class IONRemoteMemoryCell : UITableViewCell {
    public static readonly NSString Key = new NSString("IONRemoteMemoryCell");
    public static readonly UINib Nib;
    
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
      var ion = AppState.context as RemoteIosION;
      if ((ion != null).Assert(GetType().Name + " was told to update with with a local ion instance")) {
        return;
      }
      
      // todo ahodder@appioninc.com: this may cause a memory leak as the view is still referenced by the ion instance
      ion.onRemoteUpdateEvent += updateMemory;
            
      this.BackgroundColor = UIColor.Clear;
      this.Layer.BorderWidth = 2f;
      this.Layer.BorderColor = UIColor.FromRGB(255,30,30).CGColor;
      
      updateMemory();
      
      if(textColor != null){
        labelTitle.TextColor = textColor;
      }
    }

    public void updateMemory() {
      var ion = AppState.context as RemoteIosION;
      if ((ion != null).Assert(GetType().Name + " was told to update with with a local ion instance")) {
        return;
      }
    
      if(ion.remotePlatformInfo != null){
        labelTitle.Text = (((ion.remotePlatformInfo.freeMemory / 1024) / 1024) / 1000) + " GB ";
      } else {
        labelTitle.Text = "N/A";
      }
    }
  }
}
