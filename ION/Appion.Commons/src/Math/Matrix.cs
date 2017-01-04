namespace Appion.Commons.Math {

	using System.Text;

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
		/// </summary>
		/// <param name="matrix">Matrix.</param>
		public Matrix(double[,] matrix) : this(matrix.GetLength(0), matrix.GetLength(1)) {
			for (int c = 0; c < width; c++) {
				for (int r = 0; r < height; r++) {
					this[r, c] = matrix[r, c];
				}
			}
		}

		/// <summary>
		/// Performs a Row reduction to echeolon form upon the matrix.
		/// </summary>
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
		public string ToString() {
			var sb = new StringBuilder();

			for (int c = 0; c < width; c++) {
				sb.Append("[");
				for (int r = 0; r < height; r++) {
					sb.Append(this[r, c].ToString("e5")).Append(", ");
				}
				sb.Append("]\n");
			}

			return sb.ToString();
		}
	}
}
