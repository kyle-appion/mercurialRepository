using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Animation;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace ION.Droid.Animation {
  /// <summary>
  /// This class is a utility class that will provide common animations in a
  /// quick factory fashion.
  /// </summary>
  public class AnimationHelper {

    /// <summary>
    /// Creates a simple animator that will animate the hieght of the given view.
    /// </summary>
    /// <param name="view"></param>
    /// <param name="startHeight"></param>
    /// <param name="endHeight"></param>
    /// <returns></returns>
    public static ValueAnimator CreateHeightAnimator(View view, int startHeight, int endHeight) {
      ValueAnimator ret = ValueAnimator.OfInt(startHeight, endHeight);

      ret.AddUpdateListener(new HeightAnimatorListener(view));

      return ret;
    }

    /// <summary>
    /// A simple callback implementation that will update the view's height when
    /// the animator updates.
    /// </summary>
    private class HeightAnimatorListener : Java.Lang.Object, ValueAnimator.IAnimatorUpdateListener {
      private View view { get; set; }

      public HeightAnimatorListener(View view) {
        this.view = view;
      }

      // Overridden from IAnimatorUpdateListener
      public void OnAnimationUpdate(ValueAnimator va) {
        int value = (int)va.AnimatedValue;

        ViewGroup.LayoutParams lp = view.LayoutParameters;
        lp.Height = value;
        view.LayoutParameters = lp; // Trigger a set update
      }
    }
  }
}