namespace ION.Droid.App {

	using System;
	using System.Threading.Tasks;

	using Android.App;
	using Android.Content;
	using Android.OS;
	using Android.Support.V4.App;

	using Appion.Commons.Util;

	using ION.Core.App;

	using ION.CoreExtensions.Net.Portal;

	using ION.Droid.Activity;

	/// <summary>
	/// AppService is the service that will maintain the ion instance in the background of the android device. Is the app
	/// is killed and was not disconnected, then the service is responsible for recreating the application.
	/// </summary>
	[Service]
  public class AppService : Service, Java.Lang.Thread.IUncaughtExceptionHandler {
		/// <summary>
		/// The id of that application's notification.
		/// </summary>
		private const int NOTIFICATION_APP_ID = 1;

		/// <summary>
		/// The ION instance that the service is maintaining.
		/// </summary>
		/// <value>The ion.</value>
		public IION ion { get; private set; }
    /// <summary>
    /// The service that provides access to the ion cloud service.
    /// </summary>
    /// <value>The portal.</value>
    public IONPortalService portal { get; private set; }

		// Overridden from Service
		public override void OnCreate() {
			base.OnCreate();
      portal = new IONPortalService();
      Java.Lang.Thread.DefaultUncaughtExceptionHandler = this;
      AppDomain.CurrentDomain.UnhandledException += (sender, e) => {
        Log.E(this, "Uncaught Exception", e.ExceptionObject as Exception);
      };
		}

    public void UncaughtException(Java.Lang.Thread thread, Java.Lang.Throwable e) {
      Log.E(this, "Uncaught Exception: for thread: " + thread.Id + "\n" + e.Message + "\n" + e.StackTrace);
      Java.Lang.JavaSystem.Exit(1);
    }

		/// <summary>
		/// Initializes the service to the given ion.
		/// Note: this method will call NOT IION#InitAsync.
		/// </summary>
		/// <returns>The ion.</returns>
		/// <param name="ion">Ion.</param>
		public async Task<bool> InitLocalION() {
			Log.D(this, "Creating the AndroidION");
			return await SetION(new AndroidION(this));
		}

		public async Task<bool> InitRemoteION(ConnectionData connectionData) {
			Log.D(this, "Creating the RemoteION");
			return await SetION(new RemoteION(this, connectionData));
		}

		// Overridden from Service
		public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId) {
			base.OnStartCommand(intent, flags, startId);

      return StartCommandResult.NotSticky;
		}

		// Overridden from Service
		public override void OnDestroy() {
			base.OnDestroy();

			var nm = GetSystemService(NotificationService) as NotificationManager;
			nm.Cancel(NOTIFICATION_APP_ID);

			if (AppState.context != null) {
				try {
					AppState.context.Dispose();
					AppState.context = null;
				} catch (Exception e) {
					Log.E(this, "Failed to dispose of app context", e);
				}
			}
		}

		private async Task<bool> SetION(BaseAndroidION newIon) {
			try {
        var old = AppState.context;
        ion = AppState.context = newIon;

        var ret = await newIon.InitAsync();
        if (ret) {
          if (old != null) {
            old.Dispose();
          }
          ion = AppState.context = newIon;
          return true;
        } else {
          // Revert to previous ion.
          Log.E(this, "Ion failed to init async...");
          ion = AppState.context = null;
          return false;
        }
			} catch (Exception e) {
				Log.E(this, "Failed to create remote ion", e);
				return false;
			}
		}


		/// <summary>
		/// Raises the bind event.
		/// </summary>
		/// <param name="intent">Intent.</param>
		public override IBinder OnBind(Intent intent) {
			return new AppBinder(this);
		}

		/// <summary>
		/// Updates (creating if necessary the application's notification.
		/// </summary>
		public void UpdateNotification() {
			var i = new Intent(this, typeof(HomeActivity));
			var pi = PendingIntent.GetActivity(this, 0, i, PendingIntentFlags.UpdateCurrent);


			var connected = 0;
			foreach (var d in ion.deviceManager.devices) {
				if (d.isConnected) {
					connected++;
				}
			}
			var total = ion.deviceManager.devices.Count;

			var ic = Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.Lollipop ? Resource.Drawable.ic_nav_analyzer : Resource.Drawable.ic_nav_analyzer_white;
			var bitmap = Android.Graphics.BitmapFactory.DecodeResource(Resources, ic);

			var note = new NotificationCompat.Builder(this)
			                                 .SetColor(Resource.Color.gold)
			                                 .SetLargeIcon(bitmap)
			                                 .SetSmallIcon(ic)
			                                 .SetContentTitle(GetString(Resource.String.app_name))
			                                 .SetContentText(string.Format(GetString(Resource.String.devices_connected_2arg), connected, total))
			                                 .SetContentIntent(pi)
			                                 .SetOngoing(true);

			var nm = GetSystemService(NotificationService) as NotificationManager;
			nm.Notify(NOTIFICATION_APP_ID, note.Build());
		}

		/// <summary>
		/// The binder that will retrieve the ion from bind calls.
		/// </summary>
		public class AppBinder : Binder {
			public AppService service { get; private set; }

			public AppBinder(AppService service) {
				this.service = service;
			}
		}
	}
}
