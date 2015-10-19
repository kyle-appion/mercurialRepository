using System;
using System.Collections.Generic;

namespace ION.Core.Util {
  public static class Arrays {
    /// <summary>
    /// Concatenates the other arrays to this array.
    /// </summary>
    /// <param name="source">Source.</param>
    /// <param name="others">Others.</param>
    /// <typeparam name="T">The 1st type parameter.</typeparam>
    public static T[] Concat<T>(this T[] source, params T[][] others) {
      var list = new List<T>();
      list.AddRange(source);

      foreach (T[] t in others) {
        if (t != null) {
          list.AddRange(t);
        }
      }

      return list.ToArray();
    }
  }
}

