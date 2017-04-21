namespace Appion.Commons.Math {

	using System;

	/// <summary>
	/// 
	/// 
	/// </summary>
	public class Regression {
		public Regression() {
		}

		/// <summary>
		/// Builds the matrix that will be used in the regression.
		/// </summary>
		/// <returns>The matrix.</returns>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		/// <param name="degree">The degree of the polynomial.</param>
		/// <exception cref="ArgumentException">If the length of x does not exactly match the length of y.</exception>
		public static Matrix BuildMatrix(double[] x, double[] y, int degree) {
			// The logic for this regression algorithm is explained here.
			// http://arachnoid.com/sage/polynomial.html
			if (x.Length != y.Length) {
				throw new ArgumentException("Cannot build regression matrix: data length are not exactly equal");
			}

			int len = x.Length;
			int mag = degree + 1;
			Matrix matrix = new Matrix(mag + 1, mag);
			// Build the matrix
			for (int c = 0; c < mag; c++) {
				for (int r = 0; r < mag; r++) {
					double xi = 0;
					for (int i = 0; i < len - 1; i++) {
						xi += Math.Pow(x[i], r + c);
					}
					matrix[r, c] = xi;
				}
			}

			// Update the y column for the matrix
			for (int m = 0; m < mag; m++) {
				double yi = 0;
				for (int i = 0; i < len - 1; i++) {
					yi = Math.Pow(x[i], i) * y[i];
				}
				matrix[mag, m] = yi;
			}

			return matrix;
		}
	}
}
