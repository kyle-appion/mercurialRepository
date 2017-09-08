namespace Appion.Commons.Math {

  using System;

  public static class MathLib {

    /// <summary>
    /// Rounds the given double to the given number of significant digits.
    /// </summary>
    /// <returns>The to significant digits.</returns>
    /// <param name="d">D.</param>
    /// <param name="digitis">Digitis.</param>
    public static double RoundToSignificantDigits(this double d, int digits) {
      if (d == 0) {
        return 0;
      }

      var scale = Math.Pow(10, Math.Floor(Math.Log10(Math.Abs(d))) + 1);
      return scale * Math.Round(d / scale, digits);
    }

    /// <summary>
    /// Truncates the double to the given digits place.
    /// </summary>
    /// <returns>The to signifiant digits.</returns>
    /// <param name="d">D.</param>
    /// <param name="digits">Digits.</param>
    public static double TruncateToSignifiantDigits(this double d, int digits) {
      if (d == 0) {
        return 0;
      }

      var scale = Math.Pow(10, Math.Floor(Math.Log10(Math.Abs(d))) + 1 - digits);
      return scale * Math.Truncate(d / scale);
    }

    // <summary>
    /// Finds the greatest common denominator of m and n.
    /// <code>
    /// long m = 12;
    /// long n = 3;
    /// Console.WriteLine("Gcd(" + m + ", " + n + ") => " + gcd(m, n));
    /// >>> OUT
    ///   gcd(12, 3) -> 3
    /// </code>
    /// </summary>
    /// 
    /// <param name="m">The first parameter used to calculate the gcd.</param>
    /// <param name="n">The second paramter used to calculate the gcd.</param>
    /// 
    /// <returns>The greatest common denominator of m and n.</returns>
    public static long Gcd(this long m, long n) {
      if (n == 0) {
        return m;
      } else {
        return Gcd(n, m % n);
      }
    }
  }
}
