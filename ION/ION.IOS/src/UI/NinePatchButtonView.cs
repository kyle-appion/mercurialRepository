namespace ION.IOS.UI {

	using System;

	using UIKit;

	using Appion.Commons.Util;

  /// <summary>
  /// A button who can only display ninepatches.
  /// </summary>
	public partial class NinePatchButtonView : UIButton {
		public NinePatchButtonView (IntPtr handle) : base (handle) {
		}

    // Overridden from UIButton
    public override void SetBackgroundImage(UIImage image, UIControlState state) {
      base.SetBackgroundImage(NinePatchImageFactory.CreateResizableNinePatchImage(image), state);
    }

    // Overridden from UIButton
    public override void SetImage(UIImage image, UIControlState state) {
      base.SetImage(NinePatchImageFactory.CreateResizableNinePatchImage(image), state);
    }
	}
}
