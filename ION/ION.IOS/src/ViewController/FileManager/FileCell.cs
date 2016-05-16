namespace ION.IOS.ViewController.FileManager {
  
  using System;
  using CoreGraphics;
  using Foundation;
  using UIKit;


	public partial class FileCell : UITableViewCell {
		public FileCell (IntPtr handle) : base (handle) {
		}

    /// <summary>
    /// Updates the cell from the given strings.
    /// </summary>
    /// <param name="iconString">Icon string.</param>
    /// <param name="filePath">File path.</param>
    public void UpdateFrom(string iconString, string filePath) {
      imageIcon.Image = UIImage.FromBundle(iconString);
      labelFilePath.Text = filePath;
    }
	}
}
