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

      if (appPrefs.portal.rememberMe) {
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


		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		/// This is used to test fluid create lookups for the ION PT Gauge Fluid Protocol
		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/*
		private async Task GeneratePTFluidLookupTables() {
		  var fluid = await fluidManager.GetFluidAsync("R410A");
      var file = CreateFluidFileFromFluid(fluid);

      try {
        ExportBinaryTable(file);
        ExportCsvTable(file, Units.Pressure.KILOPASCAL, Units.Temperature.KELVIN);
        ExportCsvTable(file, Units.Pressure.PSIG, Units.Temperature.FAHRENHEIT);
        Log.D(this, "Exported " + fluid.name + " successfully");
      } catch (Exception e) {
        Log.E(this, "Failed to export " + fluid.name, e);
      }

      try {
        var fluidCount = 0;
        var fluidName = "";
        var size = 0;
        foreach (var name in fluidManager.GetAvailableFluidNames()) {
          try {
            fluid = await fluidManager.GetFluidAsync(name);
            file = CreateFluidFileFromFluid(fluid);
            if (file.size > size) {
              size = file.size;
              fluidName = name;
            }
            fluidCount++;
          } catch (Exception ee) {
            Log.E(this, "Failed to create file from fluid: " + name, ee);
          }
        }

        Log.D(this, "||||||||||||| Out of " + fluidCount + " fluid, the max size is " + size + " bytes and is from fluid: " + fluidName);

        fluid = await fluidManager.GetFluidAsync(fluidName);
				file = CreateFluidFileFromFluid(fluid);
				ExportBinaryTable(file);
        ExportCsvTable(file, Units.Pressure.KILOPASCAL, Units.Temperature.KELVIN);
				ExportCsvTable(file, Units.Pressure.PSIG, Units.Temperature.FAHRENHEIT);
				Log.D(this, "Exported " + fluid.name + " successfully");
      } catch (Exception e) {
        Log.E(this, "Failed to get the max byte size.", e);
      }
    }

    private void ExportBinaryTable(FluidFile file) {
      var sdcard = fileManager.GetApplicationExternalDirectory();
      var testing = sdcard.GetFolder("Testing", EFileAccessResponse.CreateIfMissing);
      var lookup = testing.GetFile(file.fluid.name + ".lookup", EFileAccessResponse.CreateIfMissing);

      using (var w = new BinaryWriter(lookup.OpenForWriting())) {
        w.Write(file.version);
        w.Write(file.pmin);
        w.Write(file.pmax);
        for (int i = 0; i < file.tbub.Length; i++) {
          w.Write(file.tbub[i]);
        }

				for (int i = 0; i < file.tbub.Length; i++) {
					w.Write(file.tdew[i]);
				}
      }
    }

    private void ExportCsvTable(FluidFile file, Unit pressureUnit, Unit temperatureUnit) {
      var sdcard = fileManager.GetApplicationExternalDirectory();
      var testing = sdcard.GetFolder("Testing", EFileAccessResponse.CreateIfMissing);
      var lookup = testing.GetFile(file.fluid.name + "[" + pressureUnit + "_" + temperatureUnit + "]" + ".csv", EFileAccessResponse.CreateIfMissing);

			var psi = Units.Pressure.PSIG;
			var kpa = Units.Pressure.KILOPASCAL;
      var kel = Units.Temperature.KELVIN;

      var csv = new Csv();

      csv.AddRow(new Csv.Row().AddCol(file.version + ""));
      csv.AddRow(new Csv.Row().AddCol(kpa.OfScalar(file.pmin / 10.0).ConvertTo(pressureUnit).ToString()));
      csv.AddRow(new Csv.Row().AddCol(kpa.OfScalar(file.pmax / 10.0).ConvertTo(pressureUnit).ToString()));

			var step = psi.OfScalar(0.5).ConvertTo(kpa).amount;
			for (int i = 0; i < file.tbub.Length; i++) {
        csv.AddRow(new Csv.Row().AddCol(kpa.OfScalar(file.pmin / 10.0 + step * i).ConvertTo(pressureUnit).ToString())
                   .AddCol(kel.OfScalar(file.tbub[i] / 10.0).ConvertTo(temperatureUnit).ToString())
                   .AddCol(kel.OfScalar(file.tdew[i] / 10.0).ConvertTo(temperatureUnit).ToString()));
      }

      using (var stream = lookup.OpenForWriting()) {
        csv.Export(stream);
      }
		}

    private FluidFile CreateFluidFileFromFluid(Fluid fluid) {
			var psi = Units.Pressure.PSIG;
			var kpa = Units.Pressure.KILOPASCAL;
      var psia_0 = ION.Core.Math.Physics.ConvertRelativePressureToAbsolute(Units.Pressure.PSIG.OfScalar(0), Units.Length.METER.OfScalar(0));
			var psia_800 = ION.Core.Math.Physics.ConvertRelativePressureToAbsolute(Units.Pressure.PSIG.OfScalar(800), Units.Length.METER.OfScalar(0));

			var min = (int)Math.Max(fluid.GetMinimumPressure(Fluid.EState.Dew).ConvertTo(kpa).amount,
							   fluid.GetMinimumPressure(Fluid.EState.Bubble).ConvertTo(kpa).amount);
      min = Math.Max(min, (int)psia_0.ConvertTo(kpa).amount);
			var max = (int)Math.Min(fluid.GetMaximumPressure(Fluid.EState.Dew).ConvertTo(kpa).amount,
					   fluid.GetMaximumPressure(Fluid.EState.Bubble).ConvertTo(kpa).amount);
      max = Math.Min(max, (int)psia_800.ConvertTo(kpa).amount);

      // Adjust to relative at sea level
      min = (int)(min - psia_0.ConvertTo(kpa).amount);
      max = (int)(max - psia_0.ConvertTo(kpa).amount);

      // Set min and max pressure to an interval of 0.5 psi


			var step = psi.OfScalar(0.5).ConvertTo(kpa).amount;
			var rows = (int)((max - min) / psi.OfScalar(0.5).ConvertTo(kpa).amount);
			var bub = new int[rows];
			var dew = new int[rows];
			// Write the bubble temperatures
			for (int i = 0; i < rows; i++) {
				var pres = min + step * i;
				var row = (int)(fluid.GetTemperatureFromAbsolutePressure(Fluid.EState.Bubble, kpa.OfScalar(pres)).amount * 10);
				bub[i] = row;
			}

			// Write the dew temperatures
			for (int i = 0; i < rows; i++) {
				var pres = min + step * i;
				var row = (int)(fluid.GetTemperatureFromAbsolutePressure(Fluid.EState.Dew, kpa.OfScalar(pres)).amount * 10);
				dew[i] = row;
			}

      return new FluidFile(fluid, 1, min * 10, max * 10, bub, dew);
    }

    private class FluidFile {
      public Fluid fluid { get; private set; }
      public byte version { get; private set; }
      public int pmin { get; private set; }
      public int pmax { get; private set; }
      public int[] tbub { get; private set; }
      public int[] tdew { get; private set; }

      public int size { get { return 9 + tbub.Length * 8; } }

      public FluidFile(Fluid fluid, byte version, int pmin, int pmax, int[] tbub, int[] tdew) {
        this.fluid = fluid;
        this.version = version;
        this.pmin = pmin;
        this.pmax = pmax;
        this.tbub = tbub;
        this.tdew = tdew;
      }
    }
*/
  }
}

