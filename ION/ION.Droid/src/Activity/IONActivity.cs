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

  using ION.CoreExtensions.Net.Portal;

  using ION.Droid.App;
  using ION.Droid.Dialog;
  using ION.Droid.Fragments;
	using ION.Droid.Preferences;
  using ION.Droid.Util;

	public class IONActivity : Activity, IServiceConnection, ISharedPreferencesOnSharedPreferenceChangeListener, ActivityCompat.IOnRequestPermissionsResultCallback {

		/// <summary>
		/// The request code that is used to request the location permissions from the user.
		/// </summary>
		public const int REQUEST_LOCATION_PERMISSIONS = 1;

		/// <summary>
		/// The request code that is used when the app requests to enable bluetooth.
		/// </summary>
		public const int REQUEST_BLUETOOTH_ENABLE = -1;

		/// <summary>
		/// The primary application service.
		/// </summary>
		/// <value>The service.</value>
		public AppService service { get; private set; }

    /// <summary>
    /// Queries the current running ion instance.
    /// </summary>
    /// <value>The ion.</value>
		public BaseAndroidION ion { get { return AppState.context as BaseAndroidION; } }
		/// <summary>
		/// Queries whether or not the bluetooth adapter is enabled.
		/// </summary>
		/// <value><c>true</c> if is bluetooth on; otherwise, <c>false</c>.</value>
		public bool isBluetoothOn { get { return Android.Bluetooth.BluetoothAdapter.DefaultAdapter.IsEnabled; } }
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


    protected override void OnCreate(Bundle savedInstanceState) {
      base.OnCreate(savedInstanceState);

      cache = new BitmapCache(Resources);
			prefs = AppPrefs.Get(this);
			prefs.prefs.RegisterOnSharedPreferenceChangeListener(this);

			BindService(new Intent(this, typeof(AppService)), this, 0);
    }

		protected override void OnResume() {
			base.OnResume();
			SetWakeLock(prefs.isWakeLocked);
			if (ion is RemoteION) {
        ActionBar.SetBackgroundDrawable(new ColorDrawable(Resource.Color.red.AsResourceColor(this)));
			} else {
        ActionBar.SetBackgroundDrawable(new ColorDrawable(Android.Resource.Color.BackgroundDark.AsResourceColor(this)));
			}
		}

    protected override void OnStop() {
      base.OnStop();
      cache.Clear();
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

		public void StartDialogFragment(Android.App.DialogFragment fragment, string tag) {
			var ft = FragmentManager.BeginTransaction();
			var prev = FragmentManager.FindFragmentByTag(tag);
			if (prev != null) {
				ft.Remove(prev);
			}
			ft.AddToBackStack(null);
			fragment.Show(ft, tag);
		}


		// Implemented from IServiceConnection
		public void OnServiceConnected(ComponentName name, IBinder service) {
			var binder = service as AppService.AppBinder;
			this.service = binder.service;
		}

		// Implemented from IServiceConnection
		public void OnServiceDisconnected(ComponentName name) {
			this.service = null;
		}

    /// <summary>
    /// Builds and returned a gray colored drawable.
    /// </summary>
    /// <returns>The gray drawable.</returns>
    /// <param name="drawableRes">Drawable res.</param>
    public Drawable GetColoredDrawable(int drawableRes, int colorRes) {
      var ret = new BitmapDrawable(cache.GetBitmap(drawableRes));
      ret.SetColorFilter(colorRes.AsResourceColor(this), PorterDuff.Mode.SrcAtop);
      return ret;
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

		/// <summary>
		/// The test function that is used to create a remote ion for testing and development puposes.
		/// </summary>
		/// <returns>The remote.</returns>
		public async Task<bool> StartRemoteIONAsync(ConnectionData connectionData) {
			bool result = false;

			try {
				result = await service.InitRemoteION(connectionData);
			} catch (Exception e) {
				Log.E(this, "Failed to start RemoteION", e);
			}

			if (!result) {
				var adb = new IONAlertDialog(this);
				// TODO-LOCALIZE ahodder@appioninc.com:
				adb.SetTitle("US Error");
				adb.SetMessage("US Failed to start remote ION. Please check your internet connection. If the issue persists, please contact Appion at " + AppionConstants.EMAIL_SUPPORT);
				adb.SetNegativeButton(Resource.String.close, (sender, args) => {}); 
				adb.Show();
				return false;
			}

			return true;
		}

		public async Task<bool> StartLocalIONAsync() {
			bool result = false;

			try {
				result = await service.InitLocalION();
			} catch (Exception e) {
				Log.E(this, "Failed to start LocalION", e);
			}

			if (!result) {
				var adb = new IONAlertDialog(this);
				// TODO-LOCALIZE ahodder@appioninc.com:
				adb.SetTitle("US Error");
				adb.SetMessage("US Failed to start remote ION. Please check your internet connection. If the issue persists, please contact Appion at " + AppionConstants.EMAIL_SUPPORT);
				adb.SetNegativeButton(Resource.String.close, (sender, args) => {}); 
				adb.Show();
				return false;
			}

			return true;
		}
  }
}

