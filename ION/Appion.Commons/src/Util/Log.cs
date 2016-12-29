namespace Appion.Commons.Util {

	using System;
	using System.Text;

  /// <summary>
  /// A logger class that will manager the application logging and persistence
  /// (if enabled).
  /// </summary>
  // TODO ahodder@appioninc.com: This can be cleaned up a lot using lambdas
  public class Log {
    /// <summary>
    /// The current log level of the log class.
    /// </summary>
    public static Level logLevel { get; set; }
    /// <summary>
    /// The printer that is delegated to print for the logger.
    /// </summary>
    /// <value>The printer.</value>
    public static IPrinter printer {
      get {
        return __printer;
      }
      set {
        if (value == null) {
          __printer = new DeadPrinter();
        } else {
          __printer = value;
        }
      }
    } static IPrinter __printer = new DeadPrinter();

    static Log() {
#if DEBUG
      logLevel = Level.Debug;
#else
      logLevel = Level.Error;
#endif // DEBUG
    }

    /// <summary>
    /// Logs a debug message.
    /// </summary>
    /// <param name="tag"></param>
    /// <param name="message"></param>
    /// <param name="e"></param>
    public static void D(object tag, string message, Exception e = null) {
      if (Level.Debug >= logLevel) {
        if (e == null) {
          printer.Print(Level.Debug, FormatTag(tag), message);
        } else {
          printer.Print(Level.Debug, FormatTag(tag), message, e.ToString());
        }
      }
      // TODO ahodder@appioninc.com: Write to log file.
    }

    /// <summary>
    /// Logs a verbose message.
    /// </summary>
    /// <param name="tag"></param>
    /// <param name="message"></param>
    /// <param name="e"></param>
    public static void V(object tag, string message, Exception e = null) {
      if (Level.Verbose >= logLevel) {
        if (e == null) {
          printer.Print(Level.Verbose, FormatTag(tag), message);
        } else {
          printer.Print(Level.Verbose, FormatTag(tag), message, e.ToString());
        }
      }
    }

    /// <summary>
    /// Logs an error message.
    /// </summary>
    /// <param name="tag"></param>
    /// <param name="message"></param>
    /// <param name="e"></param>
    public static void E(object tag, string message, Exception e = null) {
      if (Level.Error >= logLevel) {
        if (e == null) {
          printer.Print(Level.Error, FormatTag(tag), message);
        } else {
          printer.Print(Level.Error, FormatTag(tag), message, e.ToString());
        }
      }
    }

    public static void C(object tag, string message, Exception e = null) {
      if (Level.Critical >= logLevel) {
        if (e == null) {
          printer.Print(Level.Debug, FormatTag(tag), message);
        } else {
          printer.Print(Level.Debug, FormatTag(tag), message, e.ToString());
        }
      }
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
      Error,
      Critical,
    }

    private class DeadPrinter : IPrinter {
      // Overridden from IPrinter
      public void Print(Level level, string tag, string msg) {
        // Nope
      }

      // Overridden from IPrinter
      public void Print(Level level, string tag, string msg, string error) {
        // Nope
      }
    }
  }

  /// <summary>
  /// Console printer for the logger..
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
}
