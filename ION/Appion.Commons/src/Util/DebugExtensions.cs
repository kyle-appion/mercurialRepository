namespace Appion.Commons.Util {

  using System;
  using System.Runtime.CompilerServices;
  using System.Collections.Generic;
  using System.Linq;

  /// <summary>
  /// An extensions file that provide useful debug methods.
  /// </summary>
  public static class DebugExtensions {
    /// <summary>
    /// Performs an assertion on the given expression. If the expression is true, nothing will happen, otherwise, if we
    /// are in debug mode, we will throw an exception, otherwise, we will preferm an error log of the event.
    /// </summary>
    /// <remarks>
    /// This method is a debug method used to catch critical things that may go wrong in code and alert the developer of
    /// the error. More correctly, we want to catch potential rare events in development rather than just let them slide
    /// by, so it is recommended that you assert that important actions succeeded such that if in the rare instance they
    /// don't, we will catch them sooner.
    /// </remarks>
    /// <returns>The assert.</returns>
    /// <param name="expression">If set to <c>true</c> expression.</param>
    /// <param name="errorMessage">Error message.</param>
    public static bool Assert(this bool expression, string errorMessage, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string called = null) {
      if (!expression) {
#if DEBUG
        throw new Exception(errorMessage);
#else
        Log.E(caller, string.Format("Assertion failed in production code line {0}:\n\t{1}", lineNumber, errorMessage);
#endif
      }

      return expression;
    }

    /// <summary>
    /// A simple wrapper that allows for quick printing of an object wrather than directly referencing Log.D.
    /// Note: print will only work in debug build.
    /// </summary>
    /// <returns>The print.</returns>
    /// <param name="">.</param>
    public static void Print(this object obj, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null) {
#if DEBUG
      Log.D(caller, obj.ToString());
#endif
    }

    public static void PrintAll<T>(this IEnumerable<T> items, [CallerMemberName] string caller = null) {
#if DEBUG
      Log.D(caller, string.Join(",", items));
#endif
    }
  }
}
