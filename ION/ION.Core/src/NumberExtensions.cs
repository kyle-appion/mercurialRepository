namespace ION.Core {

	using System;

	public static class NumberExtensions {

		/// <summary>
		/// Does an epsilon equality check. Return true iff |first - second| lessthan epsilon. 
		/// </summary>
		/// <returns><c>true</c>, if quals was DEed, <c>false</c> otherwise.</returns>
		/// <param name="first">First.</param>
		/// <param name="second">Second.</param>
		/// <param name="epsilon">Epsilon.</param>
		public static bool DEquals(this double first, double second, double epsilon=0.0005) {
			return first - second < epsilon || first - second > -epsilon;
		}

		/// <summary>
		/// Does an epsilon equality check. Return true iff |first - second| lessthan epsilon. 
		/// </summary>
		/// <returns><c>true</c>, if quals was DEed, <c>false</c> otherwise.</returns>
		/// <param name="first">First.</param>
		/// <param name="second">Second.</param>
		/// <param name="epsilon">Epsilon.</param>
		public static bool DEquals(this float first, float second, double epsilon=0.0005) {
			return first - second < epsilon || first - second > -epsilon;
		}
	}
}

