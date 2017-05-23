using System.Text;
namespace Appion.Commons.Util {

	using System;
	using System.IO;
	using System.Threading.Tasks;

  /// <summary>
  /// A logger class that will manager the application logging and persistence (if enabled).
  /// </summary>
  public class Log {
    /// <summary>
    /// The current log level of the log class.
    /// </summary>
    public static Level logLevel { get; set; }

		/// <summary>
		/// The logger that will abstract away the platform specific details of logging.
		/// </summary>
		/// <value>The logger.</value>
		public static ILogger logger { get; set; }

		/// <summary>
		/// The object that is used to lock concurrent log to.
		/// </summary>
		private static object locker = new object();
    /// <summary>
    /// The pending task queue for the logger.
    /// </summary>
    private static Task pendingTasks;


    static Log() {
#if DEBUG
      logLevel = Level.Debug;
#else
      logLevel = Level.Error;
#endif // DEBUG
			logger = new DeadLogger();
    }

    /// <summary>
    /// Logs a debug message.
    /// </summary>
    /// <param name="tag"></param>
    /// <param name="message"></param>
    /// <param name="e"></param>
    public static void D(object tag, string message, Exception e = null) {
      if (Level.Debug >= logLevel) {
        var data = logger.NewLogData(Level.Debug, FormatTag(tag), message, e);
        logger.Print(data);
//        SaveLogData(data);
      }
    }

    /// <summary>
    /// Logs a verbose message.
    /// </summary>
    /// <param name="tag"></param>
    /// <param name="message"></param>
    /// <param name="e"></param>
    public static void V(object tag, string message, Exception e = null) {
			if (Level.Verbose >= logLevel) {
        var data = logger.NewLogData(Level.Verbose, FormatTag(tag), message, e);
        logger.Print(data);
//        SaveLogData(data);
			}
    }

    public static void M(object tag, string message, Exception e = null) {
      var data = logger.NewLogData(Level.Metric, FormatTag(tag), message, e);
      logger.Print(data);
      SaveLogData(data);
		}

    /// <summary>
    /// Logs an error message.
    /// </summary>
    /// <param name="tag"></param>
    /// <param name="message"></param>
    /// <param name="e"></param>
    public static void E(object tag, string message, Exception e = null) {
      var data = logger.NewLogData(Level.Error, FormatTag(tag), message, e);
      logger.Print(data);
      SaveLogData(data);
    }

    public static void C(object tag, string message, Exception e = null) {
      var data = logger.NewLogData(Level.Critical, FormatTag(tag), message, e);
			logger.Print(data);
      SaveLogData(data);
    }

    /// <summary>
    /// Uploads the logs to the appion server.
    /// </summary>
    /// <returns>The logs async.</returns>
    public static void UploadLogs() {
      if (pendingTasks == null || pendingTasks.IsCompleted) {
        pendingTasks = Task.Factory.StartNew(() => {
          lock (locker) {
            logger.UploadLogs();
          }
        });
      } else {
        pendingTasks.ContinueWith((arg) => {
          lock (locker) {
            logger.UploadLogs();
          }
        });
      }
    }

		/// <summary>
		/// Saves the log data to disk.
		/// </summary>
		/// <param name="data">Data.</param>
    private static void SaveLogData(LogData data) {
      try {
        if (pendingTasks == null || pendingTasks.IsCompleted) {
          pendingTasks = Task.Factory.StartNew(() => {
            DoSaveLogData(data);
          });
        } else {
          pendingTasks.ContinueWith((task) => {
            DoSaveLogData(data);
          });
        }
      } catch (Exception e) {
        logger.Print(new LogData(Level.Error, "Log.cs", "Failed to save log data", e));
      }
		}

    private static void DoSaveLogData(LogData data) {
      lock (locker) {
        try {
          var sb = new StringBuilder();
          sb.Append(data.logLevel).Append(": ").Append(FormatDateTime(data.logtime)).Append(" ")
              .Append("TID{").Append(data.threadId).Append("} ")
              .Append("PID{").Append(data.processId).Append("}\n")
              .Append("\t").Append(data.tag).Append(": ").Append(data.message).Append("\n");

          if (data.exception != null) {
            sb.Append(data.exception.ToString());
          }

          var stream = logger.CreateLogDataStream(data);
          if (stream == null) {
            logger.Print(new LogData(Level.Error, "Log.cs", "Failed to create log data stream."));
            return;
          }
          using (var w = new StreamWriter(stream)) {
            w.Write(sb.ToString());
          }
        } catch (Exception e) {
          logger.Print(new LogData(Level.Error, "Log.cs", "Failed to save log data", e));
        }
      }
    }

    private static string FormatDateTime(DateTime date) {
      return date.Year + "/" + date.Month + "/" + date.Day + " " +
                 date.Hour + ":" + date.Minute + ":" + date.Second + "-" + date.Millisecond;
    }

    /// <summary>
    /// Unpacks the tag into a useful string.
    /// </summary>
    /// <param name="tag"></param>
    /// <returns></returns>
    private static string FormatTag(object tag) {
      if (tag is string) {
        return (string)tag;
      } else {
        return tag.GetType().Name;
      }
    }


    /// <summary>
    /// Enumerates the possible log levels of the logger.
    /// </summary>
    public enum Level {
      Debug,
      Verbose,
			Metric,
      Error,
      Critical,
    }
  }

	/// <summary>
	/// The data structure that is used to store log data.
	/// </summary>
	public class LogData {
		public DateTime logtime;

		public Log.Level logLevel;

		public string tag;
		public string message;
		public Exception exception;

		public int threadId;
		public int processId;

		public LogData(Log.Level level, string tag, string message, Exception e = null) {
			logtime = DateTime.Now;
			logLevel = level;
			this.tag = tag;
			this.message = message;
			this.exception = e;
		}
	}

  /// <summary>
  /// Console printer for the logger.
  /// </summary>
  public interface IPrinter {
    /// <summary>
    /// Prints the log to the platform console.
    /// </summary>
    /// <param name="tag"></param>
    /// <param name="msg"></param>
    void Print(Log.Level level, string tag, string msg);
    
    /// <summary>
    /// Prints the log to the platform console.
    /// </summary>
    /// <param name="tag"></param>
    /// <param name="msg"></param>
    /// <param name="error"></param>
    void Print(Log.Level level, string tag, string msg, string error);
  }

	/// <summary>
	/// The contract for services that will commit logs to disk.
	/// </summary>
	public interface ILogger {
		/// <summary>
		/// Whether or not the logger allows saving to disk.
		/// </summary>
		/// <value><c>true</c> if is disk save enabled; otherwise, <c>false</c>.</value>
		bool isDiskSaveEnabled { get; }

		/// <summary>
		/// Prints the log to the platform console.
		/// </summary>
		void Print(LogData data);

		/// <summary>
		/// Creates a new LogData with platform specific data.
		/// </summary>
		/// <returns>The log data.</returns>
		/// <param name="level">Level.</param>
		/// <param name="tag">Tag.</param>
		/// <param name="msg">Message.</param>
		/// <param name="error">Error.</param>
		LogData NewLogData(Log.Level level, string tag, string msg, Exception error = null);

		/// <summary>
		/// Creates the stream that the log data will be stored to.
		/// </summary>
		/// <returns>The async.</returns>
		/// <param name="data">Data.</param>
		Stream CreateLogDataStream(LogData data);

    /// <summary>
    /// Uploads the zipped log data to the appion servers.
    /// </summary>
    void UploadLogs();
	}

	/// <summary>
	/// An implementation of ILogger that doesn't do anything.
	/// </summary>
	internal class DeadLogger : ILogger {
		// Implemented for ILogger
		public bool isDiskSaveEnabled { get { return false; } }

		// Implemented for ILogger
		public void Print(LogData data) {
		}

		// Implemented for ILogger
		public LogData NewLogData(Log.Level level, string tag, string msg, Exception error = null) {
			return new LogData(level, tag, msg, error);
		}

		// Implemented for ILogger
		public Stream CreateLogDataStream(LogData data) {
			return null;
		}

    public void UploadLogs() {
    }
	}
}
