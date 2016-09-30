namespace ION.Droid.Fragments {

  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Linq;
  using System.Text;

  using Android.App;
  using Android.Content;
  using Android.Graphics;
  using Android.Graphics.Drawables;
  using Android.OS;
  using Android.Runtime;
  using Android.Views;
	using Android.Views.InputMethods;
  using Android.Widget;

  using ION.Core.App;
  using ION.Core.Util;

  using ION.Droid.Activity;
	using ION.Droid.App;
  using ION.Droid.Util;

  public class IONFragment : Fragment {

    /// <summary>
    /// The ion context for the fragment.
    /// </summary>
    /// <value>The ion.</value>
		public AndroidION ion { get { return (AndroidION)AppState.context; } }

    /// <summary>
    /// The bitmap cache that will store common bitmaps.
    /// </summary>
    /// <value>The cache.</value>
    public BitmapCache cache { get; private set; }

    /// <summary>
    /// The flags that are used by the activity.
    /// </summary>
    /// <value>The flags.</value>
    protected EFlags flags { get; private set; }

    // Overridden from Fragment
    public override void OnCreate(Bundle savedInstanceState) {
      base.OnCreate(savedInstanceState);

      // Create your fragment here
    }

    /// <Docs>If the fragment is being re-created from
    ///  a previous saved state, this is the state.</Docs>
    /// <summary>
    /// Raises the activity created event.
    /// </summary>
    /// <param name="savedInstanceState">Saved instance state.</param>
    public override void OnActivityCreated(Bundle savedInstanceState) {
      base.OnActivityCreated(savedInstanceState);

      var a = Activity as IONActivity;
      if (a != null) {
        cache = a.cache;
      } else {
        cache = new BitmapCache(Resources);
      }
    }

		public override void OnResume() {
			base.OnResume();
		}

    /// <Docs>The options menu in which you place your items.</Docs>
    /// <returns>To be added.</returns>
    /// <summary>
    /// Raises the create options menu event.
    /// </summary>
    /// <param name="menu">Menu.</param>
    public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater) {
      inflater.Inflate(Resource.Menu.screenshot, menu);
      inflater.Inflate(Resource.Menu.record, menu);

			var r = menu.FindItem(Resource.Id.record);
			if (ion.dataLogManager.isRecording) {
				r.SetIcon(GetColoredDrawable(Resource.Drawable.ic_action_pause, Resource.Color.light_gray));
			} else {
				r.SetIcon(GetColoredDrawable(Resource.Drawable.ic_action_play, Resource.Color.light_gray));
			}


      var ss = menu.FindItem(Resource.Id.screenshot);
      var icon = ss.Icon;
      icon.Mutate().SetColorFilter(GetColor(Resource.Color.gray), PorterDuff.Mode.SrcIn);
      ss.SetIcon(icon);
    }

    /// <Docs>The options menu as last shown or first initialized by
    ///  onCreateOptionsMenu().</Docs>
    /// <summary>
    /// Raises the prepare options menu event.
    /// </summary>
    /// <param name="menu">Menu.</param>
    public override void OnPrepareOptionsMenu(IMenu menu) {
      base.OnPrepareOptionsMenu(menu);

      menu.FindItem(Resource.Id.screenshot).SetVisible(HasFlags(EFlags.AllowScreenshot));
			menu.FindItem(Resource.Id.record).SetVisible(HasFlags(EFlags.StartRecording));
    }

    /// <Docs>The menu item that was selected.</Docs>
    /// <returns>To be added.</returns>
    /// <para tool="javadoc-to-mdoc">This hook is called whenever an item in your options menu is selected.
    ///  The default implementation simply returns false to have the normal
    ///  processing happen (calling the item's Runnable or sending a message to
    ///  its Handler as appropriate). You can use this method for any items
    ///  for which you would like to do processing without those other
    ///  facilities.</para>
    /// <summary>
    /// Raises the options item selected event.
    /// </summary>
    /// <param name="item">Item.</param>
    public override bool OnOptionsItemSelected(IMenuItem item) {
      switch (item.ItemId) {
        case Resource.Id.screenshot:
          CaptureScreenToBitmap();
          return true;
        case Resource.Id.record:
          ToggleRecordingSession(item);
          return true;
        default:
          return base.OnOptionsItemSelected(item);
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
		/// A utility method that will forcefully close the keyboard.
		/// </summary>
		public void HideKeyboard() {
			var imm = Activity.GetSystemService(Activity.InputMethodService) as InputMethodManager;
			var view = Activity.CurrentFocus;
			if (Activity.CurrentFocus == null) {
				view = new View(Activity);
			}
			imm.HideSoftInputFromWindow(view.WindowToken, 0);
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
            Activity.InvalidateOptionsMenu();
            break;
          case EFlags.StartRecording:
            Activity.InvalidateOptionsMenu();
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

    /// <summary>
    /// Captures the current activity's view to a bitmap. This will start an activity to resolve the screenshot.
    /// </summary>
    public void CaptureScreenToBitmap() {
      try {
        // TODO ahodder@appioninc.com: Turn this into an asynchronous task.
        // TODO ahodder@appioninc.com: Figure out how to properly compress the screenshot such that we aren't scaling.
        var v = Activity.Window.DecorView.RootView;
        var drawingCache = v.DrawingCacheEnabled;
        v.DrawingCacheEnabled = true;
        var b = Bitmap.CreateBitmap(v.GetDrawingCache(true));
        v.DrawingCacheEnabled = drawingCache;

        var aspectRatio = b.Width / (float)b.Height;

        var scaled = Bitmap.CreateScaledBitmap(b, (int)(800 * aspectRatio), 800, true);
        b.Recycle();

        byte[] bytes = null;
        var stream = new MemoryStream();
        scaled.Compress(Bitmap.CompressFormat.Png, 100, stream);
        bytes = stream.ToArray();
        stream.Dispose();
        scaled.Recycle();

        var i = new Intent(Activity, typeof(ScreenshotActivity));
        i.PutExtra(ScreenshotActivity.EXTRA_PNG_BYTES, bytes);
        StartActivity(i);
      } catch (Exception e) {
        Error("Failed to take screenshot", e);
      }
    }

    /// <summary>
    /// Toggles the recording session.
    /// </summary>
    public async void ToggleRecordingSession(IMenuItem item) {
      if (ion.dataLogManager.isRecording) {
        if (!await ion.dataLogManager.StopRecording()) {
          Log.D(this, "Failed to stop recording");
        }
        item.SetIcon(GetColoredDrawable(Resource.Drawable.ic_action_play, Resource.Color.light_gray));
      } else {
				var interval = ion.preferences.reports.DataLoggingInterval;
				Log.D(this, "Starting record with an interval of: " + interval.ToString());

				if (!await ion.dataLogManager.BeginRecording(interval)) {
          Log.D(this, "Failed to begin recording");
        }
        item.SetIcon(GetColoredDrawable(Resource.Drawable.ic_action_pause, Resource.Color.light_gray));
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
    /// Toasts the given message.
    /// </summary>
    /// <param name="message">Message.</param>
    public void Alert(string message) {
      Toast.MakeText(Activity, message, ToastLength.Short).Show();
    }

    /// <summary>
    /// Displays and error toast to the user.
    /// </summary>
    /// <param name="message">Message.</param>
    public void Error(string message, Exception e = null) {
      Log.E(this, message, e);
      Toast.MakeText(Activity, message, ToastLength.Long).Show();
    }
  }

  /// <summary>
  /// The flags that the ion activity supports.
  /// </summary>
  [Flags]
  public enum EFlags {
    AllowScreenshot = 1 << 0,
    StartRecording = 1 << 1,
  }
}

