namespace ION.IOS.Util {

	using System;

	using Appion.Commons.Util;

  /// <summary>
  /// The implementation for an iOS printer.
  /// </summary>
  public class LogPrinter : IPrinter {
    public LogPrinter() {
    }
    public void Print(Log.Level level, string tag, string msg) {
      Console.WriteLine("{0,25} {1,5} {2}", tag, level, msg);
    }

    public void Print(Log.Level level, string tag, string msg, string error) {
      Console.WriteLine("{0,25} {1,5} {2}\n{3}", tag, level, msg, error);
    }
  }
}

