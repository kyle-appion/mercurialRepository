namespace ION.IOS.UI {

	using System;

	using CoreGraphics;
	using UIKit;

	using Appion.Commons.Util;

  public class Toast : UIView {
    /// <summary>
    /// The height of the toast window.
    /// </summary>
    /// <value>The message.</value>
    private static readonly nfloat TOAST_HEIGHT = 21f;
    /// <summary>
    /// The distance of the toast from the bottom of the window. 
    /// </summary>
    private static readonly nfloat TOAST_PHONE_GAP = 10f;

    /// <summary>
    /// The message that will be displayed with the toast.
    /// </summary>
    /// <value>The message.</value>
    public UILabel label { get; set; }
    /// <summary>
    /// The duration of the toast in milliseconds.
    /// </summary>
    /// <value>The duration.</value>
    public long duration { get; set; }

    public Toast() {
      Initialize();
    }

    // Overridden from IOView
    public void Initialize() {
      label = new UILabel();
      label.BackgroundColor = UIColor.Clear;
      label.TextColor = UIColor.White;
      label.TextAlignment = UITextAlignment.Center;
      label.Lines = 2;
      label.Font = UIFont.SystemFontOfSize(17);
      label.LineBreakMode = UILineBreakMode.CharacterWrap;
      AddSubview(label);
    }

    private void SetFrame(CGRect frame) {
      Frame = frame;
      label.Frame = new CGRect(5.0f, 5.0, Frame.Size.Width - 5.0f, Frame.Size.Height - 5.0f);
    }

    /// <summary>
    /// Creates a small ui that will show a string to the user.
    /// </summary> 
    /// <param name="message">Message.</param>
    public static void New(UIView parent, string message, long duration=1500) {
      ShowToast(parent, message, duration);
    }

    private static void ShowToast(UIView parent, string message, long duration) {
      Log.D("Toast", "Toast {" + message + "}");
      int toastsInParent = 0;
      foreach (UIView baby in parent.Subviews) {
        if (baby is Toast) {
          toastsInParent++;
        }
      }

      CGRect parentFrame = parent.Frame;

      nfloat yorigin = parentFrame.Size.Height - (70f + TOAST_HEIGHT * toastsInParent + TOAST_PHONE_GAP * toastsInParent);

      Toast toast = new Toast();
      toast.SetFrame(new CGRect(parentFrame.X + 20f, yorigin, parentFrame.Size.Width - 40, TOAST_HEIGHT));
      toast.BackgroundColor = UIColor.DarkGray;
      toast.label.Text = message;
      toast.Alpha = 1.0f;
      toast.Layer.CornerRadius = 4f;

      parent.AddSubview(toast);

      UIView.Animate(
        duration: duration / 1000,
        animation: () => {
          toast.Alpha = 0.8f;
        },
        completion: () => {
          toast.RemoveFromSuperview();
        }
      );
    }
  }
}

