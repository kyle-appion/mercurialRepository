using System;

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
  }
}

