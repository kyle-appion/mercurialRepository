namespace ION.Droid {

  using System;

  using Android.Content;
  using Android.Graphics;
  using Android.Graphics.Drawables;
	using Android.Support.V4.Content;

  public static class GraphicsExtensions {

    /// <summary>
    /// Queries a color from resources.
    /// </summary>
    /// <returns>The color.</returns>
    /// <param name="colorResource">Color resource.</param>
    /// <param name="context">Context.</param>
    public static Color AsResourceColor(this int colorResource, Context context) {
      return new Color(ContextCompat.GetColor(context, colorResource));
    }

    public static Drawable AsResourceDrawable(this int drawableResource, Context context) {
      return ContextCompat.GetDrawable(context, drawableResource);
    }
  }
}
