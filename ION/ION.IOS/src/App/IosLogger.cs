namespace ION.IOS {

  using System;
  using System.Diagnostics;
  using System.IO;
  using System.IO.Compression;
  using System.Net;
  using System.Text;
  using System.Threading;

  using Appion.Commons.Util;

  using ION.Core.App;

  using ION.IOS.App;

  public class IosLogger : ILogger {
    private static string TAG = typeof(IosLogger).Name;

    // Implemented for ILogger
    public bool isDiskSaveEnabled {
      get {
        return AppPrefs.Get().allowAppAnalytics;
      }
    }

    public IosLogger() {
    }

    // Implemented for ILogger
    public void Print(LogData logData) {
      Print(logData.logLevel, logData.tag, logData.message);
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
        var filename = "AppionLogFile_" + date.Year + "_" + date.Month + "_" + date.Day + ".txt";

        // Get logs directory directory
        var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        var library = Path.Combine(documents, "..", "Library");
        var cache = Path.Combine(library, "Caches");
        var logs = Path.Combine(cache, "logs");

        DirectoryInfo ldi = null;
        if (!Directory.Exists(logs)) {
          ldi = Directory.CreateDirectory(logs);
          if (ldi == null || !ldi.Exists) {
            Print(TAG, "Failed to create logs directory");
            return null;
          }
        }

        // Create log file
        var file = Path.Combine(logs, filename);
        if (!File.Exists(file)) {
          return File.Create(file);
        } else {
          return File.AppendText(file).BaseStream;
        }
      } catch (Exception e) {
        Print(TAG, "Failed to save log data...", e);
        return null;
      }
    }

    // Implemented from ILogger
    public void UploadLogs() {
      try {
        // Get logs directory directory
        var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        var library = Path.Combine(documents, "..", "Library");
        var cache = Path.Combine(library, "Caches");
        var logs = Path.Combine(cache, "logs");

        var fn = Path.Combine(cache, "AnalyticsUpload_" + DateTime.Now.ToUTCMilliseconds() + ".zip");

        ZipFile.CreateFromDirectory(logs, fn);

        var client = new WebClient();
        var response = client.UploadFile("http://portal.appioninc.com/App/uploadErrorLogs.php", fn);
        client.Dispose();

#if DEBUG
        var sr = Encoding.UTF8.GetString(response);
        Print(TAG, sr);
#endif

        Directory.Delete(logs, true);
        File.Delete(fn);
      } catch (Exception e) {
        Log.E(this, "Failed to upload analytics.", e);
      }
    }
/*
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
*/

    private void SaveSystemInfo(string dest) {
      if (AppState.context == null) {
        return;
      }

      try {
        var fn = "SystemInfo.txt";
        var file = Path.Combine(dest, fn);

        var dump = new BaseAppDump(AppState.context, new IOSPlatformInfo());

        using (var w = new StreamWriter(new FileStream(file, FileMode.OpenOrCreate))) {
          w.Write(dump.ToString());
        }
      } catch (Exception e) {
        Print(TAG, "Failed to save system info", e);
      }
    }

    private void Print(string tag, string msg, Exception e = null) {
      var estr = e == null ? "" : e.ToString();
      Console.WriteLine("{0,25} {1,5} {2}\n{3}", tag, Log.Level.Debug, msg, estr);
    }

    private void Print(Log.Level level, string tag, string msg) {
      Console.WriteLine("{0,25} {1,5} {2}", tag, level, msg);
    }
  }
}

