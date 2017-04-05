namespace Appion.Commons.Math {

	using System.Text;

	/*
	var m = new Appion.Commons.Math.Matrix(new double[,] {
				{ 8, 26, 170, 1232, 9686, 24.5 },
				{ 26, 170, 1232, 9686, 79256, 106.5 },
				{ 170, 1232, 9686, 79256, 665510, 676.5 },
				{ 1232, 9686, 79256, 665510, 5.68695e6, 5032.5 },
				{ 9686, 79256, 665510, 5.68695e6, 4.9209e7, 39904.5 },
			});
			m.Echelonize();
	 */
	public class Matrix {

		public int width { get; private set; }
		public int height { get; private set; }

		/// <summary>
		/// Indexes a particular cell within the matrix.
		/// </summary>
		/// <param name="row">Row.</param>
		/// <param name="column">Column.</param>
		public double this[int row, int column] {
			get {
				return internalMatrix[row * width + column];
			}
			set {
				internalMatrix[row * width + column] = value;
			}
		}

		/// <summary>
		/// The internal matrix represented as a linear set of data.
		/// </summary>
		private double[] internalMatrix;

		/// <summary>
		/// Creates a new matrix with the given width and height.
		/// </summary>
		/// <param name="width">Width.</param>
		/// <param name="height">Height.</param>
		public Matrix(int width, int height) {
			this.width = width;
			this.height = height;
			internalMatrix = new double[width * height];
		}

		/// <summary>
		/// Wraps the given matrix.
		/// The first collection of data is the width and the second is the height.
		/// </summary>
		/// <param name="matrix">Matrix.</param>
		public Matrix(double[,] matrix) : this(matrix.GetLength(1), matrix.GetLength(0)) {
			for (int r = 0; r < height; r++) {
				for (int c = 0; c < width; c++) {
					this[r, c] = matrix[r, c];
				}
			}
		}

		/// <summary>
		/// Performs a Row reduction to echeolon form upon the matrix.
		/// </summary>
		/// <code>
		/// var m = new Matrix(new double[3, 4]{
		///		{  1, 2, -1,  -4 },
		///		{  2, 3, -1, -11 },
		///		{ -2, 0, -3,  22 }
		/// };
		/// m.Echelonize();
		/// Console.Print(m.ToString()
		/// OUTPUT:
		/// 1   0   0   -8
		///	0   1   0   1
		///	0   0   1   -2
		/// </code>
		public void Echelonize() {
			int lead = 0;
			for (int r = 0; r < height; r++) {
				if (lead >= width) {
					break;
				}

				int i = r;
				while (this[i, lead] == 0) {
					i++;
					if (i == height) {
						i = r;
						lead++;
						if (lead == width) {
							lead--;
							break;
						}
					}
				}

				for (int j = 0; j < width; j++) {
					double tmp = this[r, j];
					this[r, j] = this[i, j];
					this[i, j] = tmp;
				}

				double div = this[r, lead];
				if (div != 0) {
					for (int j = 0; j < width; j++) {
						this[r, j] /= div;
					}
				}

				for (int j = 0; j < height; j++) {
					if (j != r) {
						double sub = this[j, lead];
						for (int k = 0; k < width; k++) {
							this[j, k] -= (sub * this[r, k]);
						}
					}
				}

				lead++;
			}
		}


		/// <summary>
		/// Returns a pretty string of the contents of this matrix.
		/// </summary>
		/// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:Appion.Commons.Matrix"/>.</returns>
		public override string ToString() {
			var sb = new StringBuilder();

			for (int r = 0; r < height; r++) {
				sb.Append("[");
				for (int c = 0; c < width; c++) {
					sb.Append(this[r, c].ToString("e5")).Append(", ");
				}
				sb.Append("]\n");
			}

			return sb.ToString();
		}
	}
}
