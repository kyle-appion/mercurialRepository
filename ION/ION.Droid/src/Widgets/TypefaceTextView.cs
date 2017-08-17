﻿namespace ION.Droid.Widgets {

  using System;
  using System.Collections.Generic;

  using Android.App;
  using Android.Content;
  using Android.Graphics;
  using Android.Runtime;
  using Android.Support.V4.Widget;
  using Android.Text;
	using Android.Util;
  using Android.Widget;

	/// <summary>
	/// A text view that will allow for fonts to be set via xml. Also allows for automatic text resizing. The text resizing
	/// was stolen from: https://stackoverflow.com/a/5535672/480691
  ///
  /// To enable auto resizing, the autoresize field must be set to true, and the text view must be single line.
	/// </summary>
	[Register("ION.Droid.Widgets.TypeTextView")]
  public class TypefaceTextView : TextView {
    /// <summary>
    /// The cache of typefaces.
    /// </summary>
    private static Dictionary<string, Typeface> TYPEFACES = new Dictionary<string, Typeface>();
    /// <summary>
    /// The path to the default typeface for the text view.
    /// </summary>
    private const string DEFAULT_TYPEFACE = "fonts/DroidSans.ttf";

    public TypefaceTextView(IntPtr handle, JniHandleOwnership transfer) : base(handle, transfer) {
    }

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
        var typeface = (ETypeface)ta.GetInt(Resource.Styleable.TypefaceTextView_typeface, (int)ETypeface.Unknown);
        string path = null;
        switch (typeface) {
          case ETypeface.DroidSans: path = "fonts/DroidSans.ttf"; break;
          case ETypeface.DroidSansBold: path = "fonts/DroidSans_Bold.ttf"; break;
        }

        if (path == null) {
          path = DEFAULT_TYPEFACE;
        }

        SetFontFromAssets(path);

        ta.Recycle();
      }
    }

    /// <summary>
    /// Sets the typeface for the view based on the given asset path.
    /// </summary>
    /// <param name="assetPath">Asset path.</param>
    private void SetFontFromAssets(string assetPath) {
      try {
        if (assetPath != null) {
          Typeface t = null;

          if (TYPEFACES.ContainsKey(assetPath)) {
            t = TYPEFACES[assetPath];
          } else {
            try {
              t = Typeface.CreateFromAsset(Application.Context.Assets, assetPath);
              TYPEFACES[assetPath] = t;
            } catch (Exception e) {
              Appion.Commons.Util.Log.E(this, "Failed to load typeface: " + assetPath, e);
              t = Typeface.Default;
            }
          }

          Typeface = t;
        }
      } catch (Exception e) {
        Appion.Commons.Util.Log.E(this, "Failed to set typeface", e);
      }
    }

      [Flags]
    public enum ETypeface {
      Unknown = -1,
      DroidSans = 1,
      DroidSansBold = 2,
    }
  }
}
