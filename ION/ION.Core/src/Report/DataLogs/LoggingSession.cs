namespace ION.Core.Report.DataLogs {

  using System;
  using System.Collections.Generic;
  using System.Threading.Tasks;

	using Appion.Commons.Util;

  using ION.Core.App;
  using ION.Core.Database;
  using ION.Core.Devices;

  /// <summary>
  /// A construct that will maintain the session of device sensor logs.
  /// </summary>
  internal class LoggingSession : IDisposable {

    /// <summary>
    /// The database representation of this session.
    /// </summary>
    public SessionRow session { get; private set; }
    /// <summary>
    /// The ION application instance.
    /// </summary>
    private IION ion;
    /// <summary>
    /// Whether or not the logging session is active.
    /// </summary>
    private bool isActive;

    public LoggingSession(IION ion, SessionRow session, TimeSpan interval) {
      this.ion = ion;
      this.session = session;
      //timer = new Timer(OnTimerCallback, interval);
      isActive = true;
      recordInterval(interval);
    }
    
    /// <summary>
    /// checking if timer error when saving
    /// </summary>
    public async void recordInterval(TimeSpan interval){
    	await Task.Delay(TimeSpan.FromMilliseconds(1));
    	while(isActive){
    		Log.D(this, "Logging devices");
	      try {

	        var sensors = new HashSet<GaugeDeviceSensor>();
	
	
					////// pull ios sensorlist items
	        if(ion.currentAnalyzer != null && ion.currentAnalyzer.sensorList != null){
	          foreach (var s in ion.currentAnalyzer.sensorList) {
	            var gds = s as GaugeDeviceSensor;
	            if (gds != null && gds.device.isConnected) {
	              sensors.Add(gds);
	            }
	          }
	        }
	
	        foreach (var m in ion.currentWorkbench.manifolds) {
	          var gds = m.primarySensor as GaugeDeviceSensor;
	          if (gds != null && gds.device.isConnected) {
	            sensors.Add(gds);
	          }
	        }
	
	        var rows = new List<SensorMeasurementRow>();
	        var db = ion.database;
	        var now = DateTime.Now;
	        foreach(var gds in sensors){
	          var existing = db.Query<LoggingDeviceRow>("SELECT * FROM LoggingDeviceRow WHERE serialNumber = ?",gds.device.serialNumber.ToString());
	
						if(existing.Count == 0){
	            var newDevice = new LoggingDeviceRow{serialNumber = gds.device.serialNumber.ToString()};
	            db.Insert(newDevice);
	          }
	        }
	
	        foreach (var gds in sensors) {
	          // TODO ahodder@appioninc.com: Normalize the device unit or the resulting data backend will be funky.
	          rows.Add(await CreateSensorMeasurement(db, gds, now));
	        }
	
	        var inserted = db.InsertAll(rows, true);
					Log.D(this, "Inserting: " + inserted + " rows");
	      } catch (Exception e) {
	        Log.E(this, "Failed to resolve timer callback", e);
	      }    	
    		await Task.Delay(interval);
			}
		}

    /// <summary>
    /// Stops the logging session.
    /// </summary>
    /// <returns><c>true</c> if this instance cancel ; otherwise, <c>false</c>.</returns>
    public void Cancel() {
      isActive = false;
    }

    /// <summary>
    /// Releases all resource used by the <see cref="ION.Core.Report.DataLogs.LoggingSession"/> object.
    /// </summary>
    /// <remarks>Call <see cref="Dispose"/> when you are finished using the
    /// <see cref="ION.Core.Report.DataLogs.LoggingSession"/>. The <see cref="Dispose"/> method leaves the
    /// <see cref="ION.Core.Report.DataLogs.LoggingSession"/> in an unusable state. After calling <see cref="Dispose"/>,
    /// you must release all references to the <see cref="ION.Core.Report.DataLogs.LoggingSession"/> so the garbage
    /// collector can reclaim the memory that the <see cref="ION.Core.Report.DataLogs.LoggingSession"/> was occupying.</remarks>
    public void Dispose() {
      if (isActive) {
        //timer.Cancel();
      }
      //timer.Dispose();
      isActive = false;
    }

    /// <summary>
    /// Creates a new sensor measurement row for the database.
    /// </summary>
    /// <param name="db">Db.</param>
    /// <param name="sensor">Sensor.</param>
    private Task<SensorMeasurementRow> CreateSensorMeasurement(IONDatabase db, GaugeDeviceSensor sensor, DateTime date) {
      var ret = new SensorMeasurementRow();

			// constant checking caused issue for timer when ending a logging session plus it was not used for anything....
      //var d = await db.QueryForUsingSerialNumberAsync(sensor.device.serialNumber);
      var meas = sensor.measurement.ConvertTo(sensor.unit.standardUnit);

      ret.serialNumber = sensor.device.serialNumber.ToString();

      ret.frn_SID = session.SID;
      ret.sensorIndex = sensor.index;
      ret.recordedDate = date;
      ret.measurement = meas.amount;

      return Task.FromResult(ret);
    }
  }
}

