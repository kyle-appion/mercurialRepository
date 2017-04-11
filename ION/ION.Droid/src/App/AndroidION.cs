namespace ION.Droid.App {

  using System;
  using System.IO;

	using Java.IO;

	using Appion.Commons.Util;

  using ION.Core.Alarms;
  using ION.Core.Alarms.Alerts;
  using ION.Core.Database;
  using ION.Core.Devices;
  using ION.Core.Fluids;
  using ION.Core.IO;
  using ION.Core.Report.DataLogs;

  using ION.CoreExtensions.IO;

  using ION.Droid.Alarms.Alerts;
  using ION.Droid.Connections;
  using ION.Droid.Location;

  public class AndroidION : BaseAndroidION {
		public AndroidION(AppService context) : base(context) {
		}

		/// <summary>
		/// Initializes the ion instance.
		/// </summary>
		/// <returns>The async.</returns>
    protected override bool OnPreInit() {
			if (!base.OnPreInit()) {
				return false;
			}

			fileManager = new AndroidFileManager(context);

//			var bluetoothService = new IONBluetoothService(this);

      var path = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "ION.database");
      database = new IONDatabase(new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid(), path, this);
      deviceManager = new BaseDeviceManager(this, new AndroidConnectionFactory(context), new AndroidConnectionHelper(this));

      locationManager = new AndroidLocationManager(this);
      alarmManager = new BaseAlarmManager(this);
      dataLogManager = new DataLogManager(this);
      alarmManager.alertFactory = (IAlarmManager am, IAlarm alarm) => {
        return new CompoundAlarmAlert(alarm, 
          new PopupActivityAlert(alarm, context),
          new ToneAlarmAlert(alarm, this),
          new VibrateAlarmAlert(alarm, this)
        );
      };

      fluidManager = new BaseFluidManager(this);

			return true;
/*
// TODO ahodder@appioninc.com: Remove
			var ar = new GoogleMapsAltitudeRetriever();
			if (ar.IsNetworkAvailable(this)) {
				var result = ar.FetchAltitudeFromLatitudeLongitude(39.74, -104.98);
			} else {
				Log.E(this, "Cannot fetch elevation: network is not available");
			}
*/
    }

		protected override bool OnPostInit() {
			context.UpdateNotification();
      isDisposed = false;
			return base.OnPostInit();
		}

		// Implemented from IION
    private bool isDisposed = false;
    public override void Dispose() {
			base.Dispose();

#if DEBUG
      if (!isDisposed) {
  			ExportDatabaseToSdCard();
        ExportLogsToSdCard();
        isDisposed = true;
      }
#endif

    }

#if DEBUG
		private void ExportDatabaseToSdCard() {
			Log.D(this, "Exporting database");
			try {
				var backup = new Java.IO.File(context.GetExternalFilesDir("").AbsolutePath, "ION_External.database");
				var original = new Java.IO.File(database.path);

				Log.D(this, "Backup: " + backup.AbsolutePath);

				if (original.Exists()) {
					var fis = new FileInputStream(original);
					var fos = new FileOutputStream(backup);

					Log.D(this, "Transfered: " + fos.Channel.TransferFrom(fis.Channel, 0, fis.Channel.Size()) + " bytes");
					fis.Close();
					fos.Flush();
					fos.Close();

					Log.D(this, "Successfully exported the database to the sd card.");
				}
			} catch (Exception e) {
				Log.E(this, "Failed to export database to SD card", e);
			}
		}


    private void ExportLogsToSdCard() {
      Log.D(this, "Exporting logs");
      try {
        var newLocation = new Java.IO.File(context.GetExternalFilesDir(""), "logs");
        var logs = context.GetFileStreamPath("logs");
        var dir = new IONFolder(new DirectoryInfo(logs.AbsolutePath));
        dir.CopyToOrThrow(newLocation.AbsolutePath, true);

        Log.D(this, "Successfully exported logs to the sd card.");
      } catch (Exception e) {
        Log.E(this, "Failed to export logs to SD card", e);
      }
    }
#endif
  }
}

