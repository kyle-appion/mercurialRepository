namespace ION.IOS {

  using System;
  using System.Collections.Generic;

  using CoreGraphics;
  using Foundation;
  using UIKit;

  using ION.Core.Util;

  /// <summary>
  /// A helper that will scroll a ViewController when the text view is selected
  /// </summary>
  public class KeyboardScrollHelper : IDisposable {

    /// <summary>
    /// The viewcontroller that the helper is working with.
    /// </summary>
    /// <value>The vc.</value>
    private UIViewController vc { get; set; }
    /// <summary>
    /// The list of observers for the helper.
    /// </summary>
    /// <value>The observers.</value>
    private List<NSObject> observers { get; set; }
    /// <summary>
    /// The view that we are currently focused on.
    /// </summary>
    /// <value>The active view.</value>
    private UIView activeView { get; set; }
    /// <summary>
    /// The total scrolled amount.
    /// </summary>
    /// <value>The total scrolled.</value>
    private nfloat totalScrolled { get; set; }
    /// <summary>
    /// Whether or not we have scrolled the view controller.
    /// </summary>
    /// <value><c>true</c> if scrolled; otherwise, <c>false</c>.</value>
    private bool scrolled { get; set; }

    public KeyboardScrollHelper(UIViewController vc) {
      this.vc = vc;
      observers = new List<NSObject>();
      observers.Add(NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillShowNotification, OnKeyboardUp));
      observers.Add(NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillHideNotification, OnKeyboardDown));
    }

    /// <summary>
    /// Releases all resource used by the <see cref="ION.IOS.KeyboardScrollHelper"/> object.
    /// </summary>
    /// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="ION.IOS.KeyboardScrollHelper"/>. The
    /// <see cref="Dispose"/> method leaves the <see cref="ION.IOS.KeyboardScrollHelper"/> in an unusable state. After
    /// calling <see cref="Dispose"/>, you must release all references to the <see cref="ION.IOS.KeyboardScrollHelper"/>
    /// so the garbage collector can reclaim the memory that the <see cref="ION.IOS.KeyboardScrollHelper"/> was occupying.</remarks>
    public void Dispose() {
      NSNotificationCenter.DefaultCenter.RemoveObservers(observers);
    }

    /// <summary>
    /// Called when the keyboard is raised.
    /// </summary>
    /// <param name="notification">Notification.</param>
    private void OnKeyboardUp(NSNotification notification) {
      var keyboardRect = UIKeyboard.FrameBeginFromNotification(notification);

      if (!FindActiveView(vc.View)) {
        Log.E(this, "Failed to find active view. Ignoring keyboard event");
        return;
      } 

      //var viewRect = FindBoundsInViewController(activeView, vc);

      //if((keyboardRect.Y - keyboardRect.Height) < (vc.View.Bounds.Bottom - keyboardRect.Height) ){
      //  return;
      //}

      //if (vc.View.Bounds.Bottom.Equals(keyboardRect.Y)) {
      //  if (viewRect.Bottom < (keyboardRect.Y - keyboardRect.Height)) {
      //    return;
      //  }
      //}
      //else if (viewRect.Bottom < keyboardRect.Y) {
      //  PerformScroll(-totalScrolled);
      //  return;
      //}

      //var scrollAmount = viewRect.Bottom - keyboardRect.Height;

      //if((totalScrolled + scrollAmount) > keyboardRect.Height){
      //  scrollAmount = keyboardRect.Height - totalScrolled;
      //}

      //if (scrollAmount > 0) {
      //  PerformScroll(scrollAmount);
      //  scrolled = true;
      //}
    }

    /// <summary>
    /// Called when the keyboard is dropped.
    /// </summary>
    /// <param name="notification">Notification.</param>
    private void OnKeyboardDown(NSNotification notification) {
      if (scrolled) {
        PerformScroll(-totalScrolled);
      }

      scrolled = false;
    }

    /// <summary>
    /// Attempts to find the active view using the given view as a parent.
    /// </summary>
    /// <param name="view">View.</param>
    private bool FindActiveView(UIView view) {
      if (view.IsFirstResponder) {
        activeView = view;
        return true;
      }

      foreach (var v in view.Subviews) {
        if (FindActiveView(v)) {
          return true;
        }
      }

      return false;
    }

    /// <summary>
    /// Attempts to find the bounds of the given view in the given view controller.
    /// </summary>
    /// <returns>The bounds in view controller.</returns>
    /// <param name="view">View.</param>
    /// <param name="vc">Vc.</param>
    private CGRect FindBoundsInViewController(UIView view, UIViewController vc) {
      var ret = new CGRect();


      ret.X = view.Frame.X;
      ret.Y = view.Frame.Y;
      ret.Width = view.Frame.Width;
      ret.Height = view.Frame.Height;

      var parent = view.Superview;
      while (parent != vc.View) {
        ret.X += parent.Frame.X;
        ret.Y += parent.Frame.Y;

        parent = parent.Superview;
      }

      return ret;
    }

    /// <summary>
    /// Performs a scroll on the helper's view controller.
    /// </summary>
    /// <param name="scrollAmount">Scroll amount.</param>
    private void PerformScroll(nfloat scrollAmount) {
      UIView.BeginAnimations("");
      UIView.SetAnimationDuration(0.25);

      var f = vc.View.Frame;

      f.Y -= scrollAmount;
      totalScrolled += scrollAmount;

      vc.View.Frame = f;

      UIView.CommitAnimations();
    }
  }
}

