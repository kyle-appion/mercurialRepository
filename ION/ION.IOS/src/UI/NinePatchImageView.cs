using System;

using Foundation;
using UIKit;

namespace ION.IOS.UI {
  /// <summary>
  /// A UIImageView who will only display ninepatch images.
  /// </summary>
  [Register("NinePatchImageView")]
  public class NinePatchImageView : UIImageView {

    // Overridden from UIImageView
    public override UIImage Image {
      get {
        return base.Image;
      }
      set {
        base.Image = NinePatchImageFactory.CreateResizableNinePatchImage(value);
      }
    }

    public NinePatchImageView(IntPtr handle) : base(handle) {
      // Nope
    }
  }
}