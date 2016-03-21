namespace ION.Core.Report.DataLogs {

  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading.Tasks;

  using ION.Core.App;
  using ION.Core.Database;
  using ION.Core.Sensors;
  using ION.Core.Util;

  /// <summary>
  /// The manager that will allow easy interactions with how devices will log there data.
  /// </summary>
  public class DataLogManager : IIONManager {
    /// <summary>
    /// Gets a value indicating whether this <see cref="ION.Core.Report.DataLogs.DataLogManager"/> is recording.
    /// </summary>
    /// <value><c>true</c> if is recording; otherwise, <c>false</c>.</value>
    public bool isRecording { 
      get {
        return currentSession != null;
      }
    }

    /// <summary>
    /// The application context that this manager lives in.
    /// </summary>
    private IION ion;

    /// <summary>
    /// The current logging session. If null, then the application is not currently logging.
    /// </summary>
    private LoggingSession currentSession;

    public DataLogManager(IION ion) {
      this.ion = ion;
    }

    /// <summary>
    /// Initializes the IIONManager to the given IION.
    /// </summary>
    /// <returns>The async.</returns>
    public Task<InitializationResult> InitAsync() {
      return Task.FromResult(new InitializationResult() { success = true });
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
        if (currentSession != null) {
          currentSession.Dispose();
        }
      }
    }

    /// <summary>
    /// Informs the DataLogManager that is should begin a new recording session.
    /// </summary>
    public Task<bool> BeginRecording(TimeSpan interval, JobRow job = null) {
      return Task.Factory.StartNew(() => {
        if (currentSession != null) {
          return false;
        }

        var db = ion.database;

        var id = job != null ? job.id : 0;

        var session = new SessionRow() {
          jobId = id,
          sessionStart = DateTime.Now,
          sessionEnd = DateTime.Now,
        };

        if (!db.SaveAsync<SessionRow>(session).Result) {
          return false;
        }

        currentSession = new LoggingSession(ion, session, interval);

        return true;
      });
    }

    /// <summary>
    /// Stops the current recording session.
    /// </summary>
    /// <returns>True if a session was saved, false otherwise. Note: we will return false if the manager was not
    /// currently recording.</returns>
    public Task<bool> StopRecording() {
      return Task.Factory.StartNew(() => {
        Log.D(this, "Stopping Recording");
        if (currentSession == null) {
          return false;
        }

        Log.D(this, "Cancelling current logging session.");

        currentSession.Cancel();

        Log.D(this, "Saving session: " + currentSession.session);

        var ret = ion.database.SaveAsync(currentSession.session).Result;

        currentSession.Dispose();
        currentSession = null;

        return ret;
      });
    }

    /// <summary>
    /// Queries all of the session data for the given session.
    /// </summary>
    /// <returns>The session data.</returns>
    /// <param name="session">Session.</param>
    public Task<SessionResults> QuerySessionData(SessionRow session) {
      return Task.Factory.StartNew(() => {
        var db = ion.database;

        // TODO ahodder@appioninc.com: This could be optimized
        var res = db.Table<SensorMeasurementRow>()
          .Where(smr => smr.sessionId == session.id)
          .GroupBy(smr => smr.deviceId)
          .Select(g => g.Last());

        var dsl = new List<DeviceSensorLogs>();
        foreach (var row in res) {
          var query = db.Table<SensorMeasurementRow>()
            .Where(smr => smr.deviceId == row.deviceId)
            .Where(smr => smr.sessionId == session.id)
            .OrderBy(s => s.recordedDate)
            .AsEnumerable();

          var count = query.Count();
          var logs = new SensorLog[count];
          int i = 0;
          foreach (var smr in query) {
            logs[i++] = new SensorLog(smr.measurement, smr.recordedDate);
          }

          dsl.Add(new DeviceSensorLogs() {
            deviceId = row.deviceId,
            logs = logs,
            index = row.sensorIndex,
            unitCode = row.unitCode,
          });
        }

        return new SessionResults() {
          complete = true,
          deviceSensorLogs = dsl.ToArray(),
          startTime = session.sessionStart,
          endTime = session.sessionEnd,
        };
      });
    }
  }

  /// <summary>
  /// The class that holds the results of a session.
  /// </summary>
  public class SessionResults {
    /// <summary>
    /// Whether or not this object represents the complete results of the session.
    /// </summary>
    /// <value><c>true</c> if complete; otherwise, <c>false</c>.</value>
    public bool complete { get; internal set; }
    /// <summary>
    /// The array of device sensor logs that make up the session.
    /// </summary>
    /// <value>The start time.</value>
    public DeviceSensorLogs[] deviceSensorLogs { get; internal set; }
    /// <summary>
    /// The time that the session started.
    /// </summary>
    /// <value>The start time.</value>
    public DateTime startTime { get; internal set; }
    /// <summary>
    /// The time that the session ended.
    /// </summary>
    /// <value>The end time.</value>
    public DateTime endTime { get; internal set; }
  }

  /// <summary>
  /// The class that will hold all of the sensor logs for a particular device's sensor.
  /// </summary>
  public class DeviceSensorLogs {
    /// <summary>
    /// The id of the device who owns the device sensor.
    /// </summary>
    /// <value>The device identifier.</value>
    public int deviceId { get; internal set; }
    /// <summary>
    /// The index of the device sensor.
    /// </summary>
    /// <value>The index.</value>
    public int index { get; internal set; }
    /// <summary>
    /// The unit code that all of the senor log measurements will be in.
    /// </summary>
    /// <value>The unit code.</value>
    public int unitCode { get; internal set; }
    /// <summary>
    /// The array of the actual logs.
    /// </summary>
    /// <value>The logs.</value>
    public SensorLog[] logs { get; internal set; }
  }

  /// <summary>
  /// This struct represents a single logging event that was stored in the database. This struct is slightly different
  /// than its counter part, SensorMeasurementRow, as it is meant to be loaded into memory in masse. As such, it is meant
  /// to be as memory efficient as possible.
  /// </summary>
  public class SensorLog {
    public double measurement { get; internal set; }
    public DateTime recordedDate { get; internal set; }

    public SensorLog(double measurement, DateTime recordedDate) {
      this.measurement = measurement;
      this.recordedDate = recordedDate;
    }
  }
}

