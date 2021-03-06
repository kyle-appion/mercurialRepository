namespace ION.IOS.ViewController.DeviceManager {

  using System;

  using Foundation;
  using UIKit;

  /// <summary>
  /// The TableCell that displays a device group within a device manager.
  /// </summary>
  public partial class DeviceSectionHeaderTableCell : UITableViewCell {

    /// <summary>
    /// The action that is called when the cell options button is clicked.
    /// </summary>
    /// <value>The options clicked.</value>
    private Action optionsClicked { get; set; }

		public DeviceSectionHeaderTableCell (IntPtr handle) : base (handle) {
		}

    // Overridden from UITableViewCell
    public override void AwakeFromNib() {
      base.AwakeFromNib();

      buttonOptions.TouchUpInside += (object sender, EventArgs e) => {
        if (optionsClicked != null) {
          optionsClicked();
        }
      };
    }

    /// <summary>
    /// Updates the table cell to the given device group.
    /// </summary>
    /// <param name="deviceGroup">Device group.</param>
    public void UpdateTo(Section section, Action optionsClicked = null) {
      viewBackground.BackgroundColor = new UIColor(section.color);
      labelCounter.Text = "" + section.devices.Count;
      labelTitle.Text = section.name;
      var image = UIImage.FromBundle("ic_more_info");
      image = image.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
      this.buttonOptions.SetImage(image, UIControlState.Normal);
      buttonOptions.TintColor = UIColor.White;
      this.optionsClicked = optionsClicked;
    }
	}
}
