using System;

using CoreGraphics;
using Foundation;
using UIKit;

using ION.Core.Util;

namespace ION.IOS.UI {
  public static class UIViewExtensions {
    /// <summary>
    /// Animates the resizing of the given view.
    /// </summary>
    /// <param name="view">View.</param>
    /// <param name="toHeight">To height.</param>
    /// <param name="duration">Duration.</param>
    public static void AnimageResize(this UIView view, float toHeight, float duration=0.5f) {
      UIView.Animate(duration, () => {
        var rect = view.Frame;
        view.Frame = new CGRect(rect.Left, rect.Top, rect.Width, (nfloat)toHeight);
        Log.D("Animate", "height = " + view.Frame.Height);
      });
    }

    /// <summary>
    /// Captures this view into an ancillary image. This is primarily used for screen shots.
    /// </summary>
    public static UIImage Capture(this UIView view) {
      var rect = view.Bounds.Size;

      UIGraphics.BeginImageContext(rect); 

      view.Layer.RenderInContext(UIGraphics.GetCurrentContext());

      var image = UIGraphics.GetImageFromCurrentImageContext();
      UIGraphics.EndImageContext();

      return image;
    }
  }
}

