namespace TestBench.Droid {

	using System;

	using Appion.Commons.Util;

  public class LogPrinter : IPrinter {
    // Overridden from IPrinter
    public void Print(Log.Level level, string tag, string msg) {
      switch (level) {
      case Log.Level.Debug: {
          Android.Util.Log.Debug(tag, msg);
          break;
        }
      case Log.Level.Verbose: {
          Android.Util.Log.Verbose(tag, msg);
          break;
        }
      case Log.Level.Error: {
          Android.Util.Log.Error(tag, msg);
          break;
        }
      }
    }

    // Overridden from IPrinter
    public void Print(Log.Level level, string tag, string msg, string error) {
      switch (level) {
        case Log.Level.Debug: {
          Android.Util.Log.Debug(tag, String.Format("{0}\n{1}", msg, error));
          break;
        }
        case Log.Level.Verbose: {
          Android.Util.Log.Verbose(tag, String.Format("{0}\n{1}", msg, error));
          break;
        }
        case Log.Level.Error: {
          Android.Util.Log.Error(tag, String.Format("{0}\n{1}", msg, error));
          break;
        }
      }
    }
  }
}

