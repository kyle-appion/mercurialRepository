namespace ION.Droid.Widgets.RecyclerViews {

  using System;

  using Android.Graphics;
  using Android.Graphics.Drawables;
  using Android.Support.V7.Widget;
  using Android.Support.V7.Widget.Helper;
  using Android.Views;

  /// <summary>
  /// An item decorator that will translate a view when swiped.
  /// </summary>
  public class SwipeAnimationDecorator : RecyclerView.ItemDecoration {
    private Drawable color;


    public SwipeAnimationDecorator(Color color) {
      this.color = new ColorDrawable(color);
    }

    public override void OnDraw(Canvas canvas, RecyclerView parent, RecyclerView.State state) {
      if (parent.GetItemAnimator().IsRunning) {
        /* Because this decorator will fire when an item is swiped for removal, meaning the view is removed and the 
         * recycler view is animating the closure of void, we will need to account for the views being animated. Because
         * the IONRecyclerViewAdapter is meant as a linear list of records, we can assert that the views being animated
         * we be the view above and below the removed view.
         */

        View aboveView = null;
        View belowView = null;

        int left = 0, right = parent.Width, top = 0, bottom = 0;

        int childCount = parent.GetLayoutManager().ChildCount;
        for (int i = 0; i < childCount; i++) {
          var child = parent.GetLayoutManager().GetChildAt(i);

          if (child.TranslationY < 0) {
            aboveView = child;
          } else if (child.TranslationY > 0) {
            if (belowView == null) {
              belowView = child;
            }
          }
        }

        if (aboveView != null && belowView != null) {
          top = aboveView.Bottom + (int)aboveView.TranslationY;
          bottom = belowView.Top + (int)belowView.TranslationY;
        } else if (aboveView != null) {
          top = aboveView.Bottom + (int)aboveView.TranslationY;
          bottom = aboveView.Bottom;
        } else if (belowView != null) {
          top = belowView.Top;
          bottom = belowView.Top + (int)belowView.TranslationY;
        }

        color.SetBounds(left, top, right, bottom);
        color.Draw(canvas);
      }

      base.OnDraw(canvas, parent, state);
    }
  }
}

