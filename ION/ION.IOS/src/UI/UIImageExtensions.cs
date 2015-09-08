using System;

using CoreGraphics;
using Foundation;
using UIKit;

namespace ION.IOS.UI {
  public static class UIImageExtensions {
    /// <summary>
    /// Converts the given image into a ninepatch image.
    /// </summary>
    /// <returns>The nine patch.</returns>
    /// <param name="image">Image.</param>
    public static UIImage AsNinePatch(this UIImage image) {
      return NinePatchImageFactory.CreateResizableNinePatchImage(image);
    }

    /// <summary>
    /// Crops the UI image.
    /// </summary>
    /// <param name="image">Image.</param>
    public static UIImage Crop(this UIImage image, CGRect rect) {
      var scale = image.CurrentScale;
      rect = new CGRect(rect.X * scale, rect.Y * scale, rect.Right * scale, rect.Bottom * scale);

      var reference = image.CGImage.WithImageInRect(rect);
      UIImage ret = new UIImage(reference, scale, image.Orientation);
      reference.Dispose();
      return ret;
    }
  }
}

