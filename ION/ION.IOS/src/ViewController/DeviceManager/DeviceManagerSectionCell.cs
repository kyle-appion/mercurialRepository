using System;

using CoreGraphics;
using Foundation;
using ObjCRuntime;
using UIKit;

namespace ION.IOS.ViewController.DeviceManager {
  [Register("DeviceManagerSectionCell")]
  public partial class DeviceManagerSectionCell : UIView {

    /// <summary>
    /// The delegate contract that is used to listen for menu button clicks.
    /// </summary>
    public delegate void OnMenuButtonClicked();

    /// <summary>
    /// The delegate that will be notified when the cell's menu button is clicked.
    /// </summary>
    /// <value>The on menu button clicked delegate.</value>
    public OnMenuButtonClicked onMenuButtonClickedDelegate { private get; set; }

    /// <summary>
    /// Creates a new DeviceManagerSectionCell.
    /// </summary>
    public static DeviceManagerSectionCell Create(UITableView tableView) {
      var arr = NSBundle.MainBundle.LoadNib("DeviceManagerSectionCell", tableView, null);
      var v = Runtime.GetNSObject(arr.ValueAt(0)) as DeviceManagerSectionCell;
      return v;
    }

    public DeviceManagerSectionCell(IntPtr handle) : base(handle) {
      buttonMenu.TouchUpInside += ((object obj, EventArgs args) => {
        if (onMenuButtonClickedDelegate != null) {
          onMenuButtonClickedDelegate();
        }
      });
    }

    /// <summary>
    /// Applies an update to the cell. Changes take effect immediately.
    /// </summary>
    /// <param name="count">Count.</param>
    /// <param name="title">Title.</param>
    public void Apply(int count, string title, OnMenuButtonClicked onMenuButtonClicked) {
      labelCounter.Text = "" + count;
      labelTitle.Text = title;
      onMenuButtonClickedDelegate = onMenuButtonClicked;
    }
  }
}

