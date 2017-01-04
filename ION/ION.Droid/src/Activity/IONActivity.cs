namespace ION.Droid.Activity {

  using System;
  using System.IO;
  using System.Threading.Tasks;

  using Android.App;
	using Android.Bluetooth;
  using Android.Content;
	using Android.Content.PM;
  using Android.Graphics;
  using Android.Graphics.Drawables;
	using Android.Locations;
  using Android.OS;
	using Android.Support.V4.App;
	using Android.Support.V4.Content;
  using Android.Views;
  using Android.Views.InputMethods;
  using Android.Widget;

	using Appion.Commons.Util;

  using ION.Core.App;

  using ION.Droid.App;
  using ION.Droid.Dialog;
  using ION.Droid.Fragments;
	using ION.Droid.Preferences;
  using ION.Droid.Util;

	public class IONActivity : Activity, ISharedPreferencesOnSharedPreferenceChangeListener, ActivityCompat.IOnRequestPermissionsResultCallback {

		/// <summary>
		/// The request code that is used to request the location permissions from the user.
		/// </summary>
		public const int REQUEST_LOCATION_PERMISSIONS = 1;

		/// <summary>
		/// The request code that is used when the app requests to enable bluetooth.
		/// </summary>
		public const int REQUEST_BLUETOOTH_ENABLE = -1;

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
		/// Queries whether or not the bluetooth adapter is enabled.
		/// </summary>
		/// <value><c>true</c> if is bluetooth on; otherwise, <c>false</c>.</value>
		public bool isBluetoothOn { get { return Android.Bluetooth.BluetoothAdapter.DefaultAdapter.IsEnabled; } }
		// TODO ahodder@appioninc.com: This property is required by android 6 for some reason.
		/// <summary>
		/// Queries whether or not the gps is enabled.
		/// </summary>
		/// <value><c>true</c> if is location on; otherwise, <c>false</c>.</value>
		public bool isLocationOn {
			get {
				var manager = GetSystemService(Context.LocationService) as LocationManager;
				return manager.IsProviderEnabled(LocationManager.GpsProvider);
			}
		}
		/// <summary>
		/// Queries whether or not the application has fine location permissions.
		/// </summary>
		/// <value><c>true</c> if has fine location perms; otherwise, <c>false</c>.</value>
		public bool hasFineLocationPerms { get { return ContextCompat.CheckSelfPermission(this, Android.Manifest.Permission.AccessFineLocation) == Permission.Granted; } } 

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
		/// The application's preferences.
		/// </summary>
		/// <value>The prefs.</value>
		protected AppPrefs prefs { get; private set; }


    /// <summary>
    /// Raises the create event.
    /// </summary>
    /// <param name="state">State.</param>
    protected override void OnCreate(Bundle state) {
      base.OnCreate(state);

      cache = new BitmapCache(Resources);
			prefs = AppPrefs.Get(this);
			prefs.prefs.RegisterOnSharedPreferenceChangeListener(this);
    }

		protected override void OnResume() {
			base.OnResume();
			SetWakeLock(prefs.isWakeLocked);
		}

		protected override void OnActivityResult(int requestCode, Result resultCode, Intent data) {
			switch (requestCode) {
				case REQUEST_BLUETOOTH_ENABLE:
					if (resultCode == Result.Ok) {
						Alert(GetString(Resource.String.bluetooth_enabled));
					} else {
						Alert(GetString(Resource.String.bluetooth_enable_failed));
					}
					break;
				default:
					base.OnActivityResult(requestCode, resultCode, data);
					break;
			}
		}

		/// <summary>
		/// Called by the system when we get a permission result.
		/// </summary>
		/// <param name="requestCode">Request code.</param>
		/// <param name="permissions">Permissions.</param>
		/// <param name="grantResults">Grant results.</param>
		public virtual void OnRequestPermissionsResult(int requestCode, String[] permissions, Permission[] grantResults) {
/*
			switch (requestCode) {
				case REQUEST_LOCATION_PERMISSIONS: {
						if (grantResults[0] == Permission.Granted) {
							EnsureBluetoothPermissions();
						} else {
							ShowMissingPermissionsDialog(GetString(Resource.String.location));
						}
				} break;
			}
*/
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
				Error(GetString(Resource.String.app_error_failed_to_take_screenshot), e);
      }
    }

    /// <summary>
    /// Shows a dialog that will turn explaining to the user that bluetooth is off.
    /// </summary>
    public void RequestBluetoothAdapterOn(Action onDeny = null) {
      var adb = new IONAlertDialog(this);
      adb.SetTitle(Resource.String.bluetooth_disabled);
      adb.SetMessage(Resource.String.error_bluetooth_scan_module_off);
			adb.SetCancelable(false);
      adb.SetNegativeButton(Resource.String.cancel, (obj, e) => {
        var dialog = obj as Dialog;
        dialog.Dismiss();
				if (onDeny != null) {
					onDeny();
				}
      });
        adb.SetPositiveButton(Resource.String.enable_bluetooth, (obj, e) => {
        var dialog = obj as Dialog;
        dialog.Dismiss();
        ShowEnableBluetoothDialog();
      });
      adb.Show();
    }

		/// <summary>
		/// Shows a dialog that will prompt a user to renable their gps.
		/// </summary>
		/// <param name="onDeny">On deny.</param>
		public void RequestLocationServicesEnabled(Action onDeny = null) {
			var adb = new IONAlertDialog(this);
			adb.SetTitle(Resource.String.bluetooth_location_off);
			adb.SetMessage(Resource.String.bluetooth_requires_location);
			adb.SetCancelable(true);
			adb.SetNegativeButton(Resource.String.cancel, (obj, e) => {
				var dialog = obj as Dialog;
				dialog.Dismiss();
				if (onDeny != null) {
					onDeny();
				}
			});
			adb.SetPositiveButton(Resource.String.enable, (obj, e) => {
				var dialog = obj as Dialog;
				dialog.Dismiss();
				var intent = new Intent(Android.Provider.Settings.ActionLocationSourceSettings);
				StartActivity(intent);
			});
			adb.Show();
		}

		/// <summary>
		/// Shows a dialog that will request that the user enable location so that we may scan.
		/// </summary>
		public void RequestFineLocationPermission(Action onDeny = null) {
			var adb = new IONAlertDialog(this);
			adb.SetTitle(Resource.String.error_location_disabled);
			adb.SetMessage(Resource.String.error_start_up_request_location_for_bluetooth);
			adb.SetCancelable(false);
			adb.SetNegativeButton(Resource.String.close, (sender, e) => {
				var d = sender as Dialog;
				d.Dismiss();
				if (onDeny != null) {
					onDeny();
				}
			});
			adb.SetPositiveButton(Resource.String.allow, (sender, e) => {
				var d = sender as Dialog;
				d.Dismiss();
				ActivityCompat.RequestPermissions(this, new string[] { Android.Manifest.Permission.AccessFineLocation }, REQUEST_LOCATION_PERMISSIONS);
			});
			adb.Show();
		}

    /// <summary>
    /// A utility method that will forcefully close the keyboard.
    /// </summary>
    protected void HideKeyboard() {
      var imm = GetSystemService(InputMethodService) as InputMethodManager;
      var view = CurrentFocus;
      if (CurrentFocus == null) {
        view = new View(this);
      }
      imm.HideSoftInputFromWindow(view.WindowToken, 0);
    }

    /// <summary>
    /// Shows an async progress dialog that will show until the bluetooth adapter is enabled or the operation times out.
    /// </summary>
    private void ShowEnableBluetoothDialog() {
			var intent = new Intent(BluetoothAdapter.ActionRequestEnable);
			StartActivityForResult(intent, REQUEST_BLUETOOTH_ENABLE);
    }

    /// <summary>
    /// Sets whether or not the activity will take control of the device's wake lock
    /// and prevent the screen from turning off.
    /// </summary>
    /// <param name="locked">If set to <c>true</c> locked.</param>
    protected void SetWakeLock(bool locked) {
			Log.D(this, "Setting wake lock: " + locked);
      if (locked) {
        Window.AddFlags(WindowManagerFlags.KeepScreenOn);
      } else {
        Window.ClearFlags(WindowManagerFlags.KeepScreenOn);
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
      Toast.MakeText(this, message, ToastLength.Long).Show();
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

