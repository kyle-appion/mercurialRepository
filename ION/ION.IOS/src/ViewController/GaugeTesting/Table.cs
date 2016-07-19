namespace ION.IOS.ViewController.GaugeTesting {

  using System;

  /// <summary>
  /// A simple mutable implementation of an ITable.
  /// </summary>
  public class Table : ITable {
    /// <summary>
    /// The column headers of the table. May be null or exactly equal to rowCount.
    /// </summary>
    /// <value>The headers.</value>
    public string[] columnHeaders { get; internal set; }
    /// <summary>
    /// Queries whether or not the table has column headers.
    /// </summary>
    /// <value>true</value>
    /// <c>false</c>
    public bool hasColumnHeaders { 
      get {
        return columnHeaders != null;
      }
    }
    /// <summary>
    /// The row headers of the table. May be null or exactly equal to rowCount.
    /// </summary>
    /// <value>The row headers.</value>
    public string[] rowHeaders { get; internal set; }
    /// <summary>
    /// Queries whether or not the table has column headers.
    /// </summary>
    /// <value>true</value>
    /// <c>false</c>
    public bool hasRowHeaders { 
      get {
        return rowHeaders != null;
      }
    }
    /// <summary>
    /// The number of columns that are in the table.
    /// </summary>
    /// <value>The column count.</value>
    public int columnCount { 
      get {
        return table.GetLength(0);
      }
    }
    /// <summary>
    /// The number of rows that are present in the table.
    /// </summary>
    /// <value>The row count.</value>
    public int rowCount {
      get {
        return table.GetLength(1);
      }
    }
    /// <summary>
    /// The two dimensional array of strings that make up the table content, less the headers.
    /// </summary>
    /// <value>The table.</value>
    public string[,] table { get; internal set; }
    /// <summary>
    /// Indexes a single cell from the table.
    /// </summary>
    /// <param name="column">Column.</param>
    /// <param name="row">Row.</param>
    public string this[int column, int row] {
      get {
        return table[column, row];
      }
      set {
        table[column, row] = value;
      }
    }

    /// <summary>
    /// Creates a new table wrapping the given table content.
    /// </summary>
    /// <param name="tableContent">Table content.</param>
    public Table(string[,] tableContent) : this(null, null, tableContent) {
    }

    /// <summary>
    /// Creates a new table wrapping the given table content. If column headers is not null, then its length must match
    /// the width of the table content. If row headers is not null, then its length must match the rank of the table
    /// content. If either of those conditions are not met, an exception will be thrown.
    /// </summary>
    /// <param name="columnHeaders">Column headers.</param>
    /// <param name="rowHeaders">Row headers.</param>
    /// <param name="tableContent">Table content.</param>
    public Table(string[] columnHeaders, string[] rowHeaders, string[,] tableContent) {
      if (tableContent == null) {
        throw new Exception("Cannot create a table with no content");
      }
      if (columnHeaders != null && columnHeaders.Length != tableContent.GetLength(0)) {
        throw new Exception("Cannot create table: column headers must either be null or its length must match the " +
        "width of the table content.");
      }
      if (rowHeaders != null && rowHeaders.Length != tableContent.Length / tableContent.GetLength(0)) {
        throw new Exception("Cannot create table: row headers must either be null or its length must match the rank " +
          "of the table content");
      }

      this.columnHeaders = columnHeaders;
      this.rowHeaders = rowHeaders; 
      this.table = tableContent;
    }
  }
}

