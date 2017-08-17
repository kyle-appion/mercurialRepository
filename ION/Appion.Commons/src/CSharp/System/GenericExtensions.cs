namespace System {

  using System.Collections.Generic;

  public static class GenericExtensions {

		/// <summary>
		/// Clamps the value between min and max such that <code>min &lt;= val &lt;= max</code>.
		/// </summary>
		/// <returns>Returns the clamped value.</returns>
		public static T Clamp<T>(this T val, T min, T max) where T : IComparable<T> {
      if (val.CompareTo(min) < 0) {
        return min;
      } else if (val.CompareTo(max) > 0) {
        return max;
      } else {
        return val;
      }
    }

    /// <summary>
    /// Returns whether or not the value is between min and max such that <code>min &lt;= val &lt;= max</code>.
    /// </summary>
    /// <returns><c>true</c>, if by was boundeded, <c>false</c> otherwise.</returns>
    /// <param name="val">Value.</param>
    /// <param name="min">Minimum.</param>
    /// <param name="max">Max.</param>
    /// <typeparam name="T">The 1st type parameter.</typeparam>
    public static bool BoundedBy<T>(this T val, T min, T max) where T : IComparable<T> {
      return val.CompareTo(min) >= 0 && val.CompareTo(max) <= 0;
    }
  }
}
