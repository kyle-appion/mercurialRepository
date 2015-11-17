namespace ION.Droid.Activity {

  using System;

  using Android.App;
  using Android.Content;
  using Android.Graphics;
  using Android.Graphics.Drawables;
  using Android.OS;
  using Android.Views;
  using Android.Widget;

  using ION.Core.Util;

  using ION.Droid.App;
  using ION.Droid.Fragment;
  using ION.Droid.Util;

  public class IONActivity : Activity, ISharedPreferencesOnSharedPreferenceChangeListener {
    /// <summary>
    /// The cache that will store bitmaps.
    /// </summary>
    /// <value>The cache.</value>
    protected BitmapCache cache { get; set; }

    // Overridden from Activity
    protected override void OnCreate(Bundle state) {
      base.OnCreate(state);

      cache = new BitmapCache(Resources);
      GetSharedPreferences(AndroidION.PREFERENCES_GENERAL, FileCreationMode.Append)
        .RegisterOnSharedPreferenceChangeListener(this);
    }

    /// <summary>
    /// Builds and returned a gray colored drawable.
    /// </summary>
    /// <returns>The gray drawable.</returns>
    /// <param name="drawableRes">Drawable res.</param>
    public Drawable GetColoredDrawable(int drawableRes, int colorRes) {
      var ret = new BitmapDrawable(cache.GetBitmap(drawableRes));
      ret.SetColorFilter(new Color(Resources.GetColor(colorRes)), PorterDuff.Mode.SrcAtop);
      return ret;
    }

    // Overridden from ISharedPreferencesOnSharedPreferenceChangeListener
    public void OnSharedPreferenceChanged(ISharedPreferences prefs, string key) {
      if (GetString(Resource.String.preferences_display_wakelock).Equals(key)) {
        SetWakeLock(prefs.GetBoolean(key, false));   
      }
    }

    /// <summary>
    /// Sets whether or not the activity will take control of the device's wake lock
    /// and prevent the screen from turning off.
    /// </summary>
    /// <param name="locked">If set to <c>true</c> locked.</param>
    protected void SetWakeLock(bool locked) {
      if (locked) {
        Window.AddFlags(WindowManagerFlags.KeepScreenOn);
      } else {
        Window.ClearFlags(WindowManagerFlags.KeepScreenOn);
      }
    }

    /// <summary>
    /// Displays and error toast to the user.
    /// </summary>
    /// <param name="message">Message.</param>
    public void Error(string message) {
      Log.E(this, message);
      Toast.MakeText(this, message, ToastLength.Long).Show();
    }
  }
}

