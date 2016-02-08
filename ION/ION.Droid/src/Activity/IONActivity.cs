﻿namespace ION.Droid.Activity {

  using System;
  using System.IO;

  using Android.App;
  using Android.Content;
  using Android.Graphics;
  using Android.Graphics.Drawables;
  using Android.OS;
  using Android.Views;
  using Android.Widget;

  using ION.Core.App;
  using ION.Core.Util;

  using ION.Droid.App;
  using ION.Droid.Fragments;
  using ION.Droid.Util;

  public class IONActivity : Activity, ISharedPreferencesOnSharedPreferenceChangeListener {

    /// <summary>
    /// Queries the current running ion instance.
    /// </summary>
    /// <value>The ion.</value>
    public IION ion {
      get {
        return AppState.context;
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

    /// <summary>
    /// Raises the create event.
    /// </summary>
    /// <param name="state">State.</param>
    protected override void OnCreate(Bundle state) {
      base.OnCreate(state);

      cache = new BitmapCache(Resources);
      GetSharedPreferences(AndroidION.PREFERENCES_GENERAL, FileCreationMode.Append)
        .RegisterOnSharedPreferenceChangeListener(this);
    }

/*

    /// <Docs>The options menu in which you place your items.</Docs>
    /// <returns>To be added.</returns>
    /// <summary>
    /// Raises the create options menu event.
    /// </summary>
    /// <param name="menu">Menu.</param>
    public override bool OnCreateOptionsMenu(IMenu menu) {
      var ret = base.OnCreateOptionsMenu(menu);

      MenuInflater.Inflate(Resource.Menu.screenshot, menu);
      var ss = menu.FindItem(Resource.Id.screenshot);
      var icon = ss.Icon;
      icon.Mutate().SetColorFilter(GetColor(Resource.Color.gray), PorterDuff.Mode.SrcIn);
      ss.SetIcon(icon);

      return true;
    }

    /// <Docs>The options menu as last shown or first initialized by
    ///  onCreateOptionsMenu().</Docs>
    /// <summary>
    /// Raises the prepare options menu event.
    /// </summary>
    /// <param name="menu">Menu.</param>
    public override bool OnPrepareOptionsMenu(IMenu menu) {
      base.OnPrepareOptionsMenu(menu);

      menu.FindItem(Resource.Id.screenshot).SetVisible(HasFlags(EFlags.AllowScreenshot));


      return true;
    }

    /// <Docs>The panel that the menu is in.</Docs>
    /// <summary>
    /// Raises the menu item selected event.
    /// </summary>
    /// <param name="featureId">Feature identifier.</param>
    /// <param name="item">Item.</param>
    public override bool OnMenuItemSelected(int featureId, IMenuItem item) {
      switch (item.ItemId) {
        case Resource.Id.screenshot:
          CaptureScreenToBitmap();
          return true;
        default:
          return base.OnMenuItemSelected(featureId, item);
      }
    }

*/


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
    public void OnSharedPreferenceChanged(ISharedPreferences prefs, string key) {
      if (GetString(Resource.String.preferences_display_wakelock).Equals(key)) {
        SetWakeLock(prefs.GetBoolean(key, false));   
      }
    }

    /// <summary>
    /// Captures the current activity's view to a bitmap. This will start an activity to resolve the screenshot.
    /// </summary>
    public void CaptureScreenToBitmap() {
      try {
        // TODO ahodder@appioninc.com: Turn this into an asynchronous task.
        var v = Window.DecorView.RootView;
        var drawingCache = v.DrawingCacheEnabled;
        v.DrawingCacheEnabled = true;
        var b = Bitmap.CreateBitmap(v.GetDrawingCache(true));
        v.DrawingCacheEnabled = drawingCache;

        byte[] bytes = null;
        using (var stream = new MemoryStream()) {
          b.Compress(Bitmap.CompressFormat.Png, 100, stream);
          bytes = stream.ToArray();
        }

        b.Recycle();

        var i = new Intent(this, typeof(ScreenshotActivity));
        i.PutExtra(ScreenshotActivity.EXTRA_PNG_BYTES, bytes);
        StartActivity(i);
      } catch (Exception e) {
        Error("Failed to take screenshot", e);
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

  /// <summary>
  /// The flags that the ion activity supports.
  /// </summary>
  [Flags]
  public enum EFlags {
    AllowScreenshot = 1 << 0,
  }
}

