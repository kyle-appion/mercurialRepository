namespace ION.IOS.ViewController.RemoteDeviceManager {

  using System;
	using CoreGraphics;
  using Foundation;
  using UIKit;

  /// <summary>
  /// The TableCell that displays a device group within a device manager.
  /// </summary>
  public partial class RemoteDeviceSectionHeaderTableCell : UITableViewCell {
		public UIButton buttonOptions;
		public UIView viewBackground;
		public UILabel labelCounter;
		public UILabel labelTitle;
		public int cellHeight = 48;
    /// <summary>
    /// The action that is called when the cell options button is clicked.
    /// </summary>
    /// <value>The options clicked.</value>
    private Action optionsClicked { get; set; }

		public RemoteDeviceSectionHeaderTableCell (IntPtr handle) {
		
		}

    /// <summary>
    /// Updates the table cell to the given device group.
    /// </summary>
    /// <param name="deviceGroup">Device group.</param>
    public void UpdateTo(Section section, Action optionsClicked = null) {   	
     	buttonOptions = new UIButton(new CGRect(this.Bounds.Width - cellHeight, 0, cellHeight, cellHeight));
     	buttonOptions.BackgroundColor = UIColor.Black;
      
      viewBackground = new UIView(new CGRect(0,0,this.Bounds.Width, cellHeight));
      
      labelCounter = new UILabel(new CGRect(0,0,cellHeight, cellHeight));
      labelCounter.Layer.BorderWidth = 1f;
      labelCounter.Layer.BorderColor = UIColor.Black.CGColor;
      labelCounter.BackgroundColor = UIColor.Clear;
      labelCounter.TextAlignment = UITextAlignment.Center;
      labelCounter.Font = UIFont.BoldSystemFontOfSize(24f);
      
			labelTitle = new UILabel(new CGRect(cellHeight,0,this.Bounds.Width - (2 * cellHeight),cellHeight));
			labelTitle.BackgroundColor = UIColor.Clear;
			labelTitle.Font = UIFont.SystemFontOfSize(24f);
			labelTitle.Layer.BorderWidth = 1f;			      

      buttonOptions.TouchUpInside += (object sender, EventArgs e) => {
        if (optionsClicked != null) {
          optionsClicked();
        }
      };    
    
      viewBackground.BackgroundColor = UIColor.FromRGB(255,255,0);

      labelCounter.Text = "" + section.devices.Count;
      labelTitle.Text = " " + section.name;
      var image = UIImage.FromBundle("ic_more_info");
      image = image.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
      this.buttonOptions.SetImage(image, UIControlState.Normal);
      buttonOptions.TintColor = UIColor.White;
      this.optionsClicked = optionsClicked;
      
      viewBackground.AddSubview(labelCounter);
      viewBackground.AddSubview(labelTitle);
      viewBackground.AddSubview(buttonOptions);      
      this.AddSubview(viewBackground);
    }
	}
}
