namespace ION.Core.Util {

  using System;
  using System.Collections.Generic;
  using System.Text;

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

		/// <summary>
		/// Creates a subset of T starting at start and ending at the end of the array.
		/// </summary>
		/// <param name="t">T.</param>
		/// <param name="start">Start.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static T[] Subset<T>(this T[] t, int start) {
			return Subset(t, start, t.Length - 1);
		}

		/// <summary>
		/// Creates a subset of T start at start and ending at the end point.
		/// </summary>
		/// <param name="t">T.</param>
		/// <param name="start">The inclusive start index within t.</param>
		/// <param name="end">The exclusive end index within t.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static T[] Subset<T>(this T[] t, int start, int end) {
			var ret = new T[end - start];

			Array.Copy(t, start, ret, 0, ret.Length);

			return ret;
		}

    /// <summary>
    /// Converts the array to a comma separated string.
    /// </summary>
    /// <returns>The string.</returns>
    public static string AsString<T>(this T[] source) {
      if (source == null) {
        return "NULL";
      } else if (source.Length < 0) {
        return "";
      } else if (source.Length == 1) { 
        return source[0].ToString();
      } else {
        var sb = new StringBuilder();
        for (int i = 0; i < source.Length - 1; i++) {
          sb.Append(source[i].ToString());
          sb.Append(",");
        }

        sb.Append(source[source.Length - 1]);

        return sb.ToString();
      }
    }
  }
}

