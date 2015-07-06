using System;
using System.Text;

namespace ION.Core.Util {
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
          Print("D", FormatTag(tag), message);
        } else {
          Print("D", FormatTag(tag), message, e.ToString());
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
          Print("V", FormatTag(tag), message);
        } else {
          Print("V", FormatTag(tag), message, e.ToString());
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
          Print("E", FormatTag(tag), message);
        } else {
          Print("E", FormatTag(tag), message, e.ToString());
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
    /// Prints the log to the platform console.
    /// </summary>
    /// <param name="tag"></param>
    /// <param name="msg"></param>
    private static void Print(string level, string tag, string msg) {
      Console.WriteLine("{0}   {1,15}: {2}", level, tag, msg);
    }

    /// <summary>
    /// Prints the log to the platform console.
    /// </summary>
    /// <param name="tag"></param>
    /// <param name="msg"></param>
    /// <param name="error"></param>
    private static void Print(string level, string tag, string msg, string error) {
      Console.WriteLine("{0}   {1,15}: {2}\n{3}", level, tag, msg, error);
    }

    /// <summary>
    /// Enumerates the possible log levels of the logger.
    /// </summary>
    public static enum Level {
      Debug,
      Verbose,
      Error,
    }
  }
}
