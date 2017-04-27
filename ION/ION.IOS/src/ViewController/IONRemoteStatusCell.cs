using System;

using CoreGraphics;
using Foundation;
using UIKit;
using ObjCRuntime;

using ION.IOS.Util;
using ION.IOS.App;
using ION.Core.App;

namespace ION.IOS {
	public partial class IONRemoteStatusCell : UITableViewCell {
		public static readonly NSString Key = new NSString("IONRemoteStatusCell");
		public static readonly UINib Nib;
    public IosION ion;
    
		static IONRemoteStatusCell() {
			Nib = UINib.FromName("IONRemoteStatusCell", NSBundle.MainBundle);
		}
		
		/// <summary>
    /// Creates a new cell.
    /// </summary>
		public static IONRemoteStatusCell Create() {
      var arr = NSBundle.MainBundle.LoadNib("IONRemoteStatusCell", null, null);
      return Runtime.GetNSObject<IONRemoteStatusCell>(arr.ValueAt(0));
    }

		protected IONRemoteStatusCell(IntPtr handle) : base(handle) {
		
		}
		
		public void UpdateTo(string title, UIImage icon, UIColor textColor) {
      ion = AppState.context as IosION;
      ion.remotePlatformChanged += updateLogging;
      
      this.BackgroundColor = UIColor.Clear;
      this.Layer.BorderWidth = 2f;
			this.Layer.BorderColor = UIColor.FromRGB(255,30,30).CGColor;

      if(ion.remoteDevice != null && ion.remoteDevice.loggingStatus == 1){
        labelTitle.Text = "Data Logging: Active";
      } else {
        labelTitle.Text = "Data Logging: Inactive";
      }      

      if(textColor != null){
      	labelTitle.TextColor = textColor;
      }   
    }  

    public void updateLogging(IPlatformInfo remoteInfo){
      if(ion.remoteDevice != null && ion.remoteDevice.loggingStatus == 1){
        labelTitle.Text = "Data Logging: Active";
      } else {
        labelTitle.Text = "Data Logging: Inactive";
      }
    }
    
	}
}
