namespace ION.Droid.Activity {

  using System;
  using System.IO;

  using Android.App;
  using Android.Content;
  using Android.Content.PM;
  using Android.Graphics;
  using Android.Graphics.Drawables;
  using Android.Preferences;
  using Android.OS;
  using Android.Views;
  using Android.Widget;

	using Appion.Commons.Util;

  using ION.Core.App;

  using ION.Droid.App;
  using ION.Droid.Fragments;
  using ION.Droid.Util;

  [Activity(Label = "@string/preferences", ScreenOrientation=ScreenOrientation.Portrait)]      
  public class IONPreferenceActivity : PreferenceActivity, ISharedPreferencesOnSharedPreferenceChangeListener {

    /// <summary>
    /// Queries the current running ion instance.
    /// </summary>
    /// <value>The ion.</value>
    public AndroidION ion {
      get {
        return AppState.context as AndroidION;
      }
    }
    /// <summary>
    /// The cache that will store bitmaps.
    /// </summary>
    /// <value>The cache.</value>
    public BitmapCache cache { get; set; }
    /// <summary>
    /// The flags that are used by the activity.
    /// </summary>
    /// <value>The flags.</value>
    protected EFlags flags { get; private set; }

    // Overridden form IONActivity
    protected override void OnCreate(Bundle bundle) {
      base.OnCreate(bundle);

      cache = new BitmapCache(Resources);

      ActionBar.SetDisplayHomeAsUpEnabled(true);
    }

    // Overridden from IONActivity
    public override bool OnMenuItemSelected(int featureId, IMenuItem item) {
      switch (item.ItemId) {
        case Android.Resource.Id.Home:
          Finish();
          return true;
        default:
          return base.OnMenuItemSelected(featureId, item);
      }
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

    /// <summary>
    /// Queries the color for the given resource.
    /// </summary>
    /// <returns>The color.</returns>
    /// <param name="colorRes">Color res.</param>
    public Color GetColor(int colorRes) {
      return new Color(Resources.GetColor(colorRes));
    }

    /// <summary>
    /// Sets the activity's flags.
    /// </summary>
    /// <param name="flags">Flags.</param>
    public void SetFlags(EFlags flags) {
      this.flags = flags;

      for (int i = 0; i < 32; i++) {
        switch ((EFlags)i) {
          case EFlags.AllowScreenshot:
            InvalidateOptionsMenu();
            break;
        }
      }
    }

    /// <summary>
    /// Queries whether or not the activity has the given flags.
    /// </summary>
    /// <returns><c>true</c> if this instance has flags the specified flags; otherwise, <c>false</c>.</returns>
    /// <param name="flags">Flags.</param>
    public bool HasFlags(EFlags flags) {
      return (this.flags & flags) == flags;
    }

    /// <summary>
    /// Adds the given flags to the activity's flags.
    /// </summary>
    /// <param name="flags">Flags.</param>
    public void AddFlags(EFlags flags) {
      SetFlags(flags | flags);
    }

    /// <summary>
    /// Removes the given flags to the activity's flags.
    /// </summary>
    /// <param name="flags">Flags.</param>
    public void RemoveFlags(EFlags flags) {
      SetFlags(flags ^ flags);
    }

    // Overridden from ISharedPreferencesOnSharedPreferenceChangeListener
    public virtual void OnSharedPreferenceChanged(ISharedPreferences prefs, string key) {
      if (GetString(Resource.String.preferences_display_wakelock).Equals(key)) {
        SetWakeLock(prefs.GetBoolean(key, false));   
      }
    }

    /// <summary>
    /// Toasts the given message.
    /// </summary>
    /// <param name="stringResource">String resource.</param>
    public void Alert(int stringResource) {
      Alert(GetString(stringResource));
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
    /// Toasts the given message.
    /// </summary>
    /// <param name="message">Message.</param>
    public void Alert(string message) {
      Toast.MakeText(this, message, ToastLength.Short).Show();
    }

    /// <summary>
    /// Displays and error toast to the user.
    /// </summary>
    /// <param name="message">Message.</param>
    public void Error(string message, Exception e = null) {
      Log.E(this, message, e);
      Toast.MakeText(this, message, ToastLength.Long).Show();
    }
  }
}

