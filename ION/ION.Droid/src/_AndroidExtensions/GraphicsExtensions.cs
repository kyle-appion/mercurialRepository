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

    /// <summary>
    /// Queries the given drawable from resources. If colorResource is not 0, then we will apply the color to the
    /// drawable.
    /// </summary>
    /// <param name="drawableResource"></param>
    /// <param name="context"></param>
    /// <param name="colorResource"></param>
    /// <returns></returns>
    public static Drawable AsResourceDrawable(this int drawableResource, Context context, int colorResource = 0) {
      var ret = ContextCompat.GetDrawable(context, drawableResource);
      
      if (colorResource != 0) {
        ret.SetTint(colorResource.AsResourceColor(context).ToArgb());
      }
      
      return ret;
    }
  }
}
