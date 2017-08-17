namespace ION.Core.Report.DataLogs {

  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading;
  using System.Threading.Tasks;

  using MoreLinq;

  using Appion.Commons.Util;

  using ION.Core.App;
  using ION.Core.Database;
  using ION.Core.Devices;

  public class DataLogManagerEvent {
    public EType type { get; private set; }

    public DataLogManagerEvent(EType type) {
      this.type = type;
    }

    public enum EType {
      RecordingStarted,
      RecordingEnded,
    }
  }

  /// <summary>
  /// The manager that will allow easy interactions with how devices will log there data.
  /// </summary>
  public class DataLogManager : IManager {
    /// <summary>
    /// The event handler that is called when the datalog manager throws a new event.
    /// </summary>
    public event Action<DataLogManager, DataLogManagerEvent> onDataLogManagerEvent;

    public bool isInitialized { get { return __isInitialized; } }
    bool __isInitialized;

    /// <summary>
    /// Gets a value indicating whether this <see cref="ION.Core.Report.DataLogs.DataLogManager"/> is recording.
    /// </summary>
    /// <value><c>true</c> if is recording; otherwise, <c>false</c>.</value>
    public bool isRecording { get { return recordingTask != null; } }
    /// <summary>
    /// The interval inbetween recording events.
    /// </summary>
    /// <value>The recording interval.</value>
    public TimeSpan recordingInterval { get; private set; }

    /// <summary>
    /// Queries the current recording session id or 0 if not recording.
    /// </summary>
    /// <value>The current session identifier.</value>
    public int currentSessionId { get { return currentSession._id; } }

    /// <summary>
    /// The application context that this manager lives in.
    /// </summary>
    private IION ion;
    /// <summary>
    /// The current session that we are saving to. Null if not recording.
    /// </summary>
    private SessionRow currentSession;
    /// <summary>
    /// The token that is used to cancel the recordingTask.
    /// </summary>
    private CancellationTokenSource cancelToken;
    /// <summary>
    /// The task that is launched when begin recording is called.
    /// </summary>
    private Task recordingTask;

    public DataLogManager(IION ion) {
      this.ion = ion;
    }

    /// <summary>
    /// Initializes the IIONManager to the given IION.
    /// </summary>
    /// <returns>The async.</returns>
    public Task<InitializationResult> InitAsync() {
      return Task.FromResult(new InitializationResult() { success = __isInitialized = true });
    }

    // Implemented for IManager
    public void PostInit() {
    }

    /// <summary>
    /// Releases all resources used by the <see cref="ION.Core.Logs.DataLogManager"/> object.
    /// </summary>
    /// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="ION.Core.Logs.DataLogManager"/>. The
    /// <see cref="Dispose"/> method leaves the <see cref="ION.Core.Logs.DataLogManager"/> in an unusable state. After
    /// calling <see cref="Dispose"/>, you must release all references to the <see cref="ION.Core.Logs.DataLogManager"/>
    /// so the garbage collector can reclaim the memory that the <see cref="ION.Core.Logs.DataLogManager"/> was occupying.</remarks>
    public void Dispose() {
      lock (this) {
        if (isRecording) {
          cancelToken.Cancel();
          recordingTask = null;
          cancelToken = null;
        }
      }
    }

    /// <summary>
    /// Informs the DataLogManager that is should begin a new recording session.
    /// </summary>
    public async Task<bool> BeginRecording(TimeSpan interval, JobRow job = null) {
      if (isRecording) {
        return false;
      }

      var db = ion.database;

      var id = job != null ? job.JID : 0;

      currentSession = new SessionRow() {
        frn_JID = id,
        sessionStart = DateTime.Now,
        sessionEnd = DateTime.Now,
      };

      if (!await db.SaveAsync<SessionRow>(currentSession)) {
        return false;
      }

      var devices = new List<GaugeDevice>();
      foreach (var device in ion.deviceManager.knownDevices) {
        var gd = device as GaugeDevice;
        if (gd != null && gd.isConnected) {
          devices.Add(gd);
        }
      }

      cancelToken = new CancellationTokenSource();
      recordingTask = RecordingTaskAsync(currentSession._id, devices, cancelToken);

      recordingInterval = interval;
      NotifyEvent(DataLogManagerEvent.EType.RecordingStarted);

      return true;
    }

    /// <summary>
    /// Stops the current recording session.
    /// </summary>
    /// <returns>True if a session was saved, false otherwise. Note: we will return false if the manager was not
    /// currently recording.</returns>
    public Task<bool> StopRecording() {
      if (!isRecording) {
        return Task.FromResult(false);
      }

      cancelToken.Cancel();
      recordingTask = null;
      cancelToken = null;
      currentSession = null;

      NotifyEvent(DataLogManagerEvent.EType.RecordingEnded);

      return Task.FromResult(true);
    }

    /// <summary>
    /// Queries all of the session data for the given session.
    /// </summary>
    /// <returns>The session data.</returns>
    /// <param name="session">Session.</param>
    public Task<SessionResults> QuerySessionDataAsync(int sessionId) {
      return Task.Factory.StartNew(() => {
        var db = ion.database;


        var identifiers = db.Table<SensorMeasurementRow>()
          .Where(smr => smr.frn_SID == sessionId)
          .DistinctBy(smr => new { smr.serialNumber, smr.sensorIndex });

        var dsl = new List<DeviceSensorLogs>();
        foreach (var ident in identifiers) {
          var query = db.Table<SensorMeasurementRow>()
            .Where(smr => smr.serialNumber == ident.serialNumber)
            .Where(smr => smr.sensorIndex == ident.sensorIndex)
            .Where(smr => smr.frn_SID == sessionId)
            .OrderBy(s => s.recordedDate)
            .AsEnumerable();

          var count = query.Count();

          var logs = new SensorLog[count];
          int i = 0;
          foreach (var smr in query) {
            logs[i++] = new SensorLog(smr.measurement, smr.recordedDate);
          }

          dsl.Add(new DeviceSensorLogs(ident.serialNumber, ident.sensorIndex, logs));
        }

        var tmp = dsl;

        var start = DateTime.UtcNow;
        var end = start;

        foreach (var d in tmp) {
          foreach (var log in d.logs) {
            if (log.recordedDate < start) {
              start = log.recordedDate;
            } else if (log.recordedDate > end) {
              end = log.recordedDate;
            }
          }
        }

        return new SessionResults() {
          complete = true,
          deviceSensorLogs = tmp,
          startTime = start,
          endTime = end,
        };
      });
    }

    /// <summary>
    /// Queries a set of SensorDataLogResults using the given collection of sessions ids.
    /// Note: the sensor is expected to be a GaugeDeviceSensor.
    /// </summary>
    /// <returns>The device sensor session results.</returns>
    public Task<HashSet<SensorDataLogResults>> QuerySensorResultsAsync(List<int> sessionIds) {
      return Task.Factory.StartNew(() => {
        // Build the list of serial numbers that are present in all the session Ids.
        var smrQuery = ion.database.Table<SensorMeasurementRow>()
           .Where(x => sessionIds.Contains(x.frn_SID))
           .Select(x => new { x.serialNumber, x.sensorIndex })
           .Distinct();

        var ret = new HashSet<SensorDataLogResults>();
        // For every item that we found in our query, inflate the gauge device sensor, and load all of the sessions that
        // the sensor is found in clamped by the given collection of session ids.
        foreach (var item in smrQuery) {
          var device = ion.deviceManager[item.serialNumber.ParseSerialNumber()] as GaugeDevice;
          var sensor = device[item.sensorIndex];
          var baseUnit = sensor.unit.standardUnit;

          var results = new Dictionary<int, List<DataLogMeasurement>>();

          foreach (var sid in sessionIds) {
            var query = ion.database.Table<SensorMeasurementRow>()
                           .Where(x => x.frn_SID == sid && x.serialNumber.Equals(item.serialNumber))
                           .OrderBy(x => x.recordedDate);

            var measurements = new List<DataLogMeasurement>();
            foreach (var smr in query) {
              measurements.Add(new DataLogMeasurement(baseUnit.OfScalar(smr.measurement), smr.recordedDate));
            }

            if (measurements.Count > 0) {
              results[sid] = measurements;
            }
          }

          ret.Add(new SensorDataLogResults(sensor, results));
        }

        return ret;
      });
    }

    /// <summary>
    /// Performs
    /// </summary>
    /// <returns>The task async.</returns>
    /// <param name="devices">Devices.</param>
    /// <param name="cancelToken">Cancel token.</param>
    private Task RecordingTaskAsync(int sessionId, List<GaugeDevice> devices, CancellationTokenSource cancelToken) {
      return Task.Factory.StartNew(async () => {
        while (!cancelToken.Token.IsCancellationRequested) {
          await Task.Delay(recordingInterval, cancelToken.Token);
          if (cancelToken.IsCancellationRequested) {
            break;
          }

          // It's time to make a recording. Go through all of the device's and record their measurements.
          var date = DateTime.UtcNow;
          var newRows = new List<SensorMeasurementRow>();

          foreach (var device in devices) {
            for (int i = 0; i < device.sensorCount; i++) {
              var su = device[i].unit.standardUnit;
              newRows.Add(new SensorMeasurementRow() {
                serialNumber = device.serialNumber.ToString(),
                sensorIndex = i,
                recordedDate = date,
                measurement = device[i].measurement.ConvertTo(su).amount,
                frn_SID = sessionId,
              });
            }
          }

          currentSession.sessionEnd = date;
          if ((ion.database.Update(currentSession, typeof(SessionRow)) == 1).Assert("Failed to update SessionRow end date")) {
            var inserted = ion.database.InsertAll(newRows, typeof(SensorMeasurementRow), true);

            if ((inserted != newRows.Count).Assert("Failed to insert all of the measurements")) {
              // todo ahodder@appioninc.com: revert last changes.
            }
          }
        }
      }, cancelToken.Token);
  	}

    private void NotifyEvent(DataLogManagerEvent.EType type) {
      if (onDataLogManagerEvent != null) {
        onDataLogManagerEvent(this, new DataLogManagerEvent(type));
      }
    }
  }
}
