namespace ION.Droid.App {

  using System;
  using System.IO;
  using System.Threading.Tasks;

	using Java.IO;

  using Appion.Commons.Measure;
	using Appion.Commons.Util;

  using ION.Core.Alarms;
  using ION.Core.Alarms.Alerts;
  using ION.Core.Content;
  using ION.Core.Database;
  using ION.Core.Devices;
  using ION.Core.Fluids;
  using ION.Core.IO;
  using ION.Core.IO.Exporter;
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
    protected async override Task<bool> OnPreInitAsync() {
			if (!await base.OnPreInitAsync()) {
				return false;
			}

			fileManager = new AndroidFileManager(context);

      var path = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "ION.database");
      database = new IONDatabase(new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid(), path, this);
      deviceManager = new BaseDeviceManager(this, new AndroidConnectionManager(this));
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
    }

		protected async override Task<bool> OnPostInitAsync() {
			context.UpdateNotification();
      isDisposed = false;

      currentWorkbench.onWorkbenchEvent += OnWorkbenchEvent;
      currentAnalyzer.onAnalyzerEvent += OnAnalyzerEvent;


      if (hasNetworkConnection && appPrefs.portal.rememberMe) {
        var result = await portal.RequestLoginAsync(appPrefs.portal.username, appPrefs.portal.password);
        if (!result.success) {
          Log.E(this, "Failed to automatically log into to ION Portal: {" + result.message + "}");
        }
      }

      var _ = preferences.lastKnownAppVersion;

			return await base.OnPostInitAsync();
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

    private void OnWorkbenchEvent(WorkbenchEvent e) {
      switch (e.type) {
        case WorkbenchEvent.EType.Added:
        case WorkbenchEvent.EType.Removed:
        case WorkbenchEvent.EType.Swapped:
          SaveWorkbenchAsync();
          break;

        case WorkbenchEvent.EType.ManifoldEvent: {
            switch (e.manifoldEvent.type) {
              case ManifoldEvent.EType.SecondarySensorAdded:
              case ManifoldEvent.EType.SecondarySensorRemoved:
              case ManifoldEvent.EType.SensorPropertySwapped:
              case ManifoldEvent.EType.SensorPropertyAdded:
              case ManifoldEvent.EType.SensorPropertyRemoved:
                SaveWorkbenchAsync();
                break;
            }
          break;
        }// WorkbenchEvent.EType.ManifoldEvent
      }
    }

    private void OnAnalyzerEvent(AnalyzerEvent e) {
      switch (e.type) {
        case AnalyzerEvent.EType.Added:
        case AnalyzerEvent.EType.Removed:
        case AnalyzerEvent.EType.Swapped:
        case AnalyzerEvent.EType.ManifoldAdded:
        case AnalyzerEvent.EType.ManifoldRemoved:
          SaveAnalyzerAsync();
          break;

        case AnalyzerEvent.EType.ManifoldEvent: {
          switch (e.manifoldEvent.type) {
            case ManifoldEvent.EType.SecondarySensorAdded:
            case ManifoldEvent.EType.SecondarySensorRemoved:
            case ManifoldEvent.EType.SensorPropertySwapped:
            case ManifoldEvent.EType.SensorPropertyAdded:
            case ManifoldEvent.EType.SensorPropertyRemoved:
              SaveAnalyzerAsync();
              break;
          }
          break;
        } // AnalyzerEvent.EType.ManifoldEvent
      }
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

