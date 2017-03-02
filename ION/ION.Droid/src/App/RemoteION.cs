namespace ION.Droid.App {

	using System;
	using System.Threading.Tasks;

	using Android.Content;
	using Android.OS;

	using Appion.Commons.Util;

	using ION.Core.Alarms;
	using ION.Core.Alarms.Alerts;
	using ION.Core.App;
	using ION.Core.Content;
	using ION.Core.Content.Parsers;
	using ION.Core.Database;
	using ION.Core.Devices;
	using ION.Core.Fluids;
	using ION.Core.IO;
	using ION.Core.Location;
	using ION.Core.Report.DataLogs;

	using ION.CoreExtensions.Net.Portal;

	using ION.Droid.Alarms.Alerts;
	using ION.Droid.Activity;
	using ION.Droid.Connections;
	using ION.Droid.Location;
	using ION.Droid.Preferences;

	/// <summary>
	/// The base class for an ION instance on the Android platform.
	/// </summary>
	public class RemoteION : BaseAndroidION {

		private const long REQUEST_LAYOUT_INTERVAL = 1000;

		/// <summary>
		/// The id of the user that we are downloading from.
		/// </summary>
		/// <value>The user identifier.</value>
		public string userId { get; set; }
		/// <summary>
		/// Whether or not the app is currently uploading its state to the server.
		/// </summary>
		/// <value><c>true</c> if is uploading state; otherwise, <c>false</c>.</value>
		public bool isUploadingState { get; /*private*/ set; }

		private HandlerThread handlerThread;
		private Handler handler;

		public RemoteION(AppService context, string userId) : base(context) {
			this.userId = userId;
		}

		// Overridden from BaseAndroidION
		protected override bool OnPreInit() {
			if (!base.OnPreInit()) {
				return false;
			}

			fileManager = new AndroidFileManager(context);

			var file = fileManager.CreateTemporaryFile("TEMPORARY_DATABASE.db", EFileAccessResponse.CreateIfMissing);
			database = new IONDatabase(new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid(), file.fullPath, this);

			deviceManager = new RemoteDeviceManager(this);
			locationManager = new AndroidLocationManager(this);
			alarmManager = new BaseAlarmManager(this);
			dataLogManager = new DataLogManager(this);
			alarmManager.alertFactory = (IAlarmManager am, IAlarm alarm) => {
				return new CompoundAlarmAlert(alarm, new PopupActivityAlert(alarm, context), new ToneAlarmAlert(alarm, this), new VibrateAlarmAlert(alarm, this));
			};
			fluidManager = new BaseFluidManager(this);

			return true;
		}

		// Overridden from BaseAndroidION
		protected override bool OnPostInit() {
			if (!base.OnPostInit()) {
				return false;
			}

			handlerThread = new HandlerThread("RemoteIonThread");
			handlerThread.Start();
			handler = new Handler(handlerThread.Looper);

			handler.PostDelayed(async () => await CloneRemote(), REQUEST_LAYOUT_INTERVAL);

			return true;
		}

		// Overridden from BaseAndroidION
		public override void Dispose() {
			base.Dispose();
			handler.RemoveCallbacksAndMessages(null);
			handlerThread.QuitSafely();

			handlerThread = null;
			handler = null;
		}

		private async Task CloneRemote() {
			await portal.CloneFromRemote(this, userId);
			// TODO ahodder@appioninc.com: Sometimes the user would dispose the remote instance in the middle of a remote download
			if (handler != null) {
				handler.PostDelayed(async () => {
					await CloneRemote();
				}, REQUEST_LAYOUT_INTERVAL);
			}
		}
	}
}
