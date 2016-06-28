namespace ION.IOS.ViewController.GaugeTesting {

  /// <summary>
  /// An immutable data structure that represents a set of tabular data.
  /// </summary>
  public interface ITable {
    /// <summary>
    /// The column headers of the table. May be null or exactly equal to rowCount.
    /// </summary>
    /// <value>The headers.</value>
    string[] columnHeaders { get; }

    /// <summary>
    /// Queries whether or not the table has column headers.
    /// </summary>
    /// <value><c>true</c> if has column headers; otherwise, <c>false</c>.</value>
    bool hasColumnHeaders { get; }

    /// <summary>
    /// The row headers of the table. May be null or exactly equal to rowCount.
    /// </summary>
    /// <value>The row headers.</value>
    string[] rowHeaders { get; }

    /// <summary>
    /// Queries whether or not the table has row headers.
    /// </summary>
    /// <value><c>true</c> if has row headers; otherwise, <c>false</c>.</value>
    bool hasRowHeaders { get; }

    /// <summary>
    /// The number of columns that are in the table.
    /// </summary>
    /// <value>The column count.</value>
    int columnCount { get; }

    /// <summary>
    /// The number of rows that are present in the table.
    /// </summary>
    /// <value>The row count.</value>
    int rowCount { get; }

    /// <summary>
    /// The two dimensional array of strings that make up the table content, less the headers.
    /// </summary>
    /// <value>The table.</value>
    string[,] table { get; }

    /// <summary>
    /// Indexes a single cell from the table.
    /// </summary>
    /// <param name="column">Column.</param>
    /// <param name="row">Row.</param>
    string this[int column, int row] { get; }
  }
}

