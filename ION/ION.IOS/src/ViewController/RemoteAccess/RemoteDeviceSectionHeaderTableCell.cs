namespace ION.IOS.ViewController.RemoteDeviceManager {

  using System;
	using CoreGraphics;
  using Foundation;
  using UIKit;

  /// <summary>
  /// The TableCell that displays a device group within a device manager.
  /// </summary>
  public partial class RemoteDeviceSectionHeaderTableCell : UITableViewCell {
		//public UIButton buttonOptions;
		//public UIView viewBackground;
		//public UILabel labelCounter;
		//public UILabel labelTitle;
		public int cellHeight = 48;
    /// <summary>
    /// The action that is called when the cell options button is clicked.
    /// </summary>
    /// <value>The options clicked.</value>
    private Action optionsClicked { get; set; }

		public RemoteDeviceSectionHeaderTableCell (IntPtr handle) : base (handle) {
		}

    // Overridden from UITableViewCell
    public override void AwakeFromNib() {
      base.AwakeFromNib();
      buttonOptions = new UIButton(new CGRect(.8 * this.Bounds.Width, 0, cellHeight, cellHeight));
      
      viewBackground = new UIView(new CGRect(0,0,this.Bounds.Width, cellHeight));
      labelCounter = new UILabel(new CGRect(0,0,.2 * this.Bounds.Width, cellHeight));
			labelTitle = new UILabel(new CGRect(.2 * this.Bounds.Width,0,.6 * this.Bounds.Width,cellHeight));
			      

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
