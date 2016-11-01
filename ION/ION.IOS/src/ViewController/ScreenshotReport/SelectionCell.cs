namespace ION.IOS.ViewController.ScreenshotReport {
  
  using System;

  using Foundation;
  using UIKit;

  using ION.IOS.UI;

	public partial class SelectionCell : UITableViewCell {

    private SelectionItem item { get; set; }

		public SelectionCell (IntPtr handle) : base (handle) {
		}

    public override void AwakeFromNib() {
      base.AwakeFromNib();

      button.TouchUpInside += (object sender, EventArgs e) => {
        OnBeginSelection();
      };
    }

    public void UpdateFrom(SelectionItem item) {
      this.item = item;
      labelHeader.Text = item.header;
      button.SetTitle(item.value, UIControlState.Normal);
    }

    private void OnBeginSelection() {
      var dialog = UIAlertController.Create(item.optionsTitle, "", UIAlertControllerStyle.ActionSheet);

      foreach (var option in item.options) {
        dialog.AddAction(UIAlertAction.Create(option, UIAlertActionStyle.Default, (action) => {
          if (item != null) {
            item.value = option;
            button.SetTitle(option, UIControlState.Normal);
          }
        }));
      }
    }
	}
}
