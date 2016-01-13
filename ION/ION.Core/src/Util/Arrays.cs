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

    /// <summary>
    /// Creates a new array ranging from start to end inclusively.
    /// </summary>
    /// <param name="start">Start.</param>
    /// <param name="end">End.</param>
    public static int[] Range(int start, int end) {
      var ret = new int[end - start + 1];

      for (int i = 0; i < ret.Length; i++) {
        ret[i] = start + i;
      }

      return ret;
    }
  }
}

