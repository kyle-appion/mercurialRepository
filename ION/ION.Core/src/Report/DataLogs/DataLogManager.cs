﻿namespace ION.Core.Report.DataLogs {

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

        var id = job != null ? job.JID : 0;

        var session = new SessionRow() {
          frn_JID = id,
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

        currentSession.session.sessionEnd = DateTime.Now;

        if (!ion.database.SaveAsync<SessionRow>(currentSession.session).Result) {
          Log.E(this, "Failed to update session end time.");
        }

        currentSession.Cancel();

        Log.D(this, "Saving session: " + currentSession.session);

        var ret = ion.database.SaveAsync(currentSession.session).Result;

//        ion.database.Update(ret);
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
    public Task<SessionResults> QuerySessionData(int sessionId) {
      return Task.Factory.StartNew(() => {
        var db = ion.database;

        // TODO ahodder@appioninc.com: This could be optimized
        var res = db.Table<SensorMeasurementRow>()
          .Where(smr => smr.frn_SID == sessionId)
          .GroupBy(smr => smr.serialNumber)
          .Select(g => g.Last());

        var dsl = new List<DeviceSensorLogs>();
        foreach (var row in res) {
          var query = db.Table<SensorMeasurementRow>()
            .Where(smr => smr.serialNumber == row.serialNumber)
            .Where(smr => smr.frn_SID == sessionId)
            .OrderBy(s => s.recordedDate)
            .AsEnumerable();

          var count = query.Count();
          var logs = new SensorLog[count];
          int i = 0;
          foreach (var smr in query) {
            logs[i++] = new SensorLog(smr.measurement, smr.recordedDate);
          }

          dsl.Add(new DeviceSensorLogs() {
            deviceId = row.serialNumber,
            logs = logs,
            index = row.sensorIndex,
//            unitCode = row.unitCode,
          });
        }
        var tmp = dsl.ToArray();

        var start = DateTime.Now;
        var end = DateTime.FromFileTime(0);

        foreach (var d in tmp) {
          foreach (var log in d.logs) {
            if (log.recordedDate < start) { 
              start = log.recordedDate;
            } else if (log.recordedDate > end) {
              end = log.recordedDate;
            }
          }
        }

        // TODO ahodder@appioninc.com: Finish start/end time
        return new SessionResults() {
          complete = true,
          deviceSensorLogs = tmp,
          startTime = start,
          endTime = end,
        };
      });
    }
  }
}
