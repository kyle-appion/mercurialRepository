namespace ION.IOS.ViewController.Workbench {

  using System;

  using Foundation;
  using UIKit;

  using ION.IOS.UI;
  using ION.IOS.Util;

	public partial class AddViewerTableCell : UITableViewCell {

    /// <summary>
    /// The action that is call when the add button is clicked.
    /// </summary>
    /// <value>The on add clicked.</value>
    private Action onAddClicked { get; set; }

		public AddViewerTableCell (IntPtr handle) : base (handle) {
		}

    public override void AwakeFromNib() {
      base.AwakeFromNib();

      buttonAdd.SetTitle(Strings.Workbench.ADD, UIControlState.Normal);
      buttonAdd.SetBackgroundImage(UIImage.FromBundle("ButtonGold").AsNinePatch(), UIControlState.Normal);
      buttonAdd.SetBackgroundImage(UIImage.FromBundle("ButtonBlack").AsNinePatch(), UIControlState.Selected);
      buttonAdd.TouchUpInside += (object sender, EventArgs e) => {
        if (onAddClicked != null) {
          onAddClicked();
        }
      };
    }

    /// <summary>
    /// Updates the table cell with the given action.
    /// </summary>
    /// <param name="addClicked">Add clicked.</param>
    public void UpdateTo(Action addClicked = null) { 
      onAddClicked = addClicked;
    }
	}
}
