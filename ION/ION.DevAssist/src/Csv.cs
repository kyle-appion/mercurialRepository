namespace ION.DevAssist {
	
	using System;
	using System.Collections.Generic;
	using System.IO;

	/// <summary>
	/// This simple class allows for quick formatted CSV's to be created.
	/// </summary>
	public class Csv {
		/// <summary>
		/// The rows that are present in the exporter.
		/// </summary>
		private List<Row> rows = new List<Row>();
    private int width { 
      get {
        if (changed) {
          __width = 0;
          foreach (var row in rows) {
            __width = Max(__width, row.width);
          }
          changed = false;
        }

        return __width;
      }
    } int __width;
    /// <summary>
    /// Whether or not the csv has changed.
    /// </summary>
    private bool changed;

		public Csv() {
		}

		/// <summary>
		/// Adds the row to the CsvExporter.
		/// </summary>
		/// <returns>The row.</returns>
		/// <param name="row">Row.</param>
		public void AddRow(Row row) {
      changed = true;
			rows.Add(row);
		}

		/// <summary>
		/// Exports the Csv data to the stream.
		/// </summary>
		/// <param name="stream">Stream.</param>
		public bool Export(Stream stream) {
			using (var s = new StreamWriter(stream)) {
				foreach (var row in rows) {
					var i = 0;
					var lim = Min(width, row.width);

					for (; i < lim; i++) {
						s.Write(row[i]);
            s.Write(",");
					}

          for (; i < Max(width, row.width); i++) {
						s.Write(",");
					}

          s.WriteLine();
				}
			}

			return true;
		}

		/// <summary>
		/// The minumum value of v1 and v2;
		/// </summary>
		/// <param name="v1">V1.</param>
		/// <param name="v2">V2.</param>
		private int Min(int v1, int v2) {
			return v1 < v2 ? v1 : v2;
		}

		private int Max(int v1, int v2) {
			return v1 > v2 ? v1 : v2;
		}

		/// <summary>
		/// A row within the csv.
		/// </summary>
		public class Row {
			/// <summary>
			/// The width of the row.
			/// </summary>
			/// <value>The width.</value>
			public int width { 
				get {
					return cols.Count;
				}
			}

			/// <summary>
			/// Returns the column at the given index.
			/// </summary>
			/// <param name="index">Index.</param>
			public string this[int index] {
				get { 
					return cols[index];
				}
			}

			/// <summary>
			/// The columns that are present in the row.
			/// </summary>
			private List<string> cols = new List<string>();

			/// <summary>
			/// Adds an empty value to the row.
			/// </summary>
			/// <returns>The col.</returns>
			public Row AddEmptyCol() {
				cols.Add("");
				return this;
			}

			/// <summary>
			/// Adds count empty columns to the row.
			/// </summary>
			/// <returns>The empty cols.</returns>
			/// <param name="count">Count.</param>
			public Row AddEmptyCols(int count) {
				while (count-- > 0) {
					AddEmptyCol();
				}
				return this;
			}

			/// <summary>
			/// Adds the value to the row.
			/// </summary>
			/// <returns>The col.</returns>
			/// <param name="value">Value.</param>
			public Row AddCol(string value) {
				if (value == null) {
					value = "";
				}
				cols.Add(value);
				return this;
			}

			/// <summary>
			/// Adds a formated date to the CSV.
			/// </summary>
			/// <returns>The col.</returns>
			/// <param name="date">Date.</param>
			public Row AddCol(DateTime date) {
				AddCol(date.Year + "-" + date.Month + "-" + date.Day);
				return this;
			}
		}
	}
}

