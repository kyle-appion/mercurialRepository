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
  }
}

