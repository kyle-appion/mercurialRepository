using Org.Json;
namespace ION.Droid {

	using System;
	using System.Diagnostics;
  using System.IO;
  using System.Net;
  using System.Text;
	using System.Threading;

	using Android.Content;

  using Java.Util.Zip;

	using Appion.Commons.Util;

	using ION.Droid.Preferences;

  public class AndroidLogger : ILogger {
		private static string TAG = typeof(AndroidLogger).Name;

		// Implemented for ILogger
		public bool isDiskSaveEnabled {
			get {
				var prefs = AppPrefs.Get(context);
				return prefs.allowAppAnalytics;
			}
		}

		/// <summary>
		/// The context that is necessary for platform specific logging.
		/// </summary>
		private Context context;

		public AndroidLogger(Context context) {
			this.context = context;
		}

    // Implemented for ILogger
		public void Print(LogData logData) {
			if (logData.exception != null) {
				switch (logData.logLevel) {
		      case Log.Level.Debug: {
						Android.Util.Log.Debug(logData.tag, string.Format("{0}\n{1}", logData.message, logData.exception));
		        break;
		      } // Log.Level.Debug
		      case Log.Level.Verbose: {
						Android.Util.Log.Verbose(logData.tag, string.Format("{0}\n{1}", logData.message, logData.exception));
		        break;
		      } // Log.Level.Verbose
		      case Log.Level.Error: {
						Android.Util.Log.Error(logData.tag, string.Format("{0}\n{1}", logData.message, logData.exception));
		        break;
		      } // Log.Level.Error
	      }
			} else {
				switch (logData.logLevel) {
					case Log.Level.Debug: {
						Android.Util.Log.Debug(logData.tag, logData.message);
						break;
					} // Log.Level.Debug
					case Log.Level.Verbose: {
						Android.Util.Log.Verbose(logData.tag, logData.message);
						break;
					} // Log.Level.Verbose
					case Log.Level.Error: {
						Android.Util.Log.Error(logData.tag, logData.message);
						break;
					} // Log.Level.Error
				}
			}
    }

		// Implemented for ILogger
		public LogData NewLogData(Log.Level level, string tag, string msg, Exception error = null) {
			return new LogData(level, tag, msg, error) {
				threadId = Thread.CurrentThread.ManagedThreadId,
				processId = Process.GetCurrentProcess().Id,
			};
		}

		// Implemented for ILogger
		public Stream CreateLogDataStream(LogData data) {
			try {
				var date = data.logtime;
        var filename = "AppionLogFile_" + date.Year + "_" + date.Month + "_" + date.Day;

				// Create directory
				var logs = context.GetFileStreamPath("logs");
        if (!logs.Exists()) {
          if (!logs.Mkdir()) {
            Android.Util.Log.Error(TAG, "Failed to make logs dir");
          }
        }
        // Create log file
        var file = new Java.IO.File(logs, filename);
				if (!file.Exists()) {
          if (!file.CreateNewFile()) {
            Android.Util.Log.Error(TAG, "Failed to create log file");
            return null;
          }
				}

				// Save the log data to the file
        return new FileStream(file.AbsolutePath, FileMode.Append);
			} catch (Exception e) {
				Android.Util.Log.Error(TAG, "Failed to save log data...", e);
				return null;
			}
		}

    // Implemented from ILogger
    public void UploadLogs() {
      try {
        var logs = context.GetFileStreamPath("logs");
        var fn = "AnalyticsUpload_" + DateTime.Now.ToUTCMilliseconds();
        var zip = Java.IO.File.CreateTempFile(fn, ".zip");

        using (var s = new ZipOutputStream(File.Create(zip.AbsolutePath))) {
          s.SetLevel(9);
          var buffer = new byte[4096];

          foreach (var file in logs.ListFiles()) {
            var entry = new ZipEntry(file.Name);
            entry.Time = DateTime.Now.ToUTCMilliseconds();
            s.PutNextEntry(entry);
            using (var fs = File.OpenRead(file.AbsolutePath)) {
              int sourceBytes;
              do {
                sourceBytes = fs.Read(buffer, 0, buffer.Length);
                s.Write(buffer, 0, sourceBytes);
              } while (sourceBytes > 0);
            }
          }

          s.Finish();
          s.Close();
        }

        var client = new WebClient();
        var response = client.UploadFile("http://portal.appioninc.com/App/uploadErrorLogs.php", zip.AbsolutePath);
        client.Dispose();

#if DEBUG
        var sr = Encoding.UTF8.GetString(response);
        var json = new JSONObject(sr);
        Android.Util.Log.Debug(TAG, json.GetString("link"));
#endif

        if (!logs.Delete()) {
          Android.Util.Log.Error(TAG, "Failed to delete old user logs");
        }
      } catch (Exception e) {
        Log.E(this, "Failed to upload analytics.", e);
      }
    }

    public void TestLoggingSystem() {
      Log.C(this, "here is a critical log... Yay!");
      Log.D(this, "Here is a debig log");

      try {
        throw new Exception("TestException");
      } catch (Exception e) {
        Log.E(this, "An expected exception occurred", e);
      }

      Log.M(this, "here is a metric");
      Log.V(this, "Verboseness");

      Log.UploadLogs();
    }
  }
}

