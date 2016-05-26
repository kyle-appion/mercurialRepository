namespace ION.Droid.Widgets {

  using System;
  using System.Collections.Generic;

  using Android.App;
  using Android.Content;
  using Android.Graphics;
  using Android.Runtime;
  using Android.Util;
  using Android.Views;
  using Android.Widget;

  /// <summary>
  /// A text view that will allow for fonts to be set via xml.
  /// </summary>
  [Register("ION.Droid.Widgets.TypeTextView")]
  public class TypefaceTextView : TextView {
    /// <summary>
    /// The cache of typefaces.
    /// </summary>
    private static Dictionary<string, Typeface> TYPEFACES = new Dictionary<string, Typeface>();
    
    public TypefaceTextView(Context context) : this(context, null, 0) {
    }

    public TypefaceTextView(Context context, IAttributeSet attrs) : this(context, attrs, 0) {
    }

    public TypefaceTextView(Context context, IAttributeSet attrs, int defStyle) : base(context, attrs, defStyle) {
      // Prevent view preview from throwing a shit fit.
      if (IsInEditMode) {
        return;
      }

      var ta = context.ObtainStyledAttributes(attrs, Resource.Styleable.TypefaceTextView);
      if (ta != null) {
        var path = ta.GetString(Resource.Styleable.TypefaceTextView_typeface);

        if (path != null) {
          Typeface t = null;

          if (TYPEFACES.ContainsKey(path)) {
            t = TYPEFACES[path];
          } else {
            try {
              t = Typeface.CreateFromAsset(Application.Context.Assets, path);
              TYPEFACES[path] = t;
            } catch (Exception e) {
              ION.Core.Util.Log.E(this, "Failed to load typeface: " + path, e);
              t = Typeface.Default;
            }
          }

          Typeface = t;
        }

        ta.Recycle();
      }
    }
  }
}

