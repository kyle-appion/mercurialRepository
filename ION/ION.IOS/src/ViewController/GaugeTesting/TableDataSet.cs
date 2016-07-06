namespace ION.IOS.ViewController.GaugeTesting {

  using System;

  using DSoft.Datatypes.Grid.Data;

  public class TableDataSet : DSDataTable, ITable {
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
        /*
        var dr = GetRow(row);
        dr[columnHeaders[column]] = value;
*/
        table[column, row] = value;
      }
    }

    public TableDataSet(string name, string[] columnHeaders, string[] rowHeaders, string[,]tableContent) : base(name) {
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

      foreach (var column in columnHeaders) {
        Columns.Add(new DSDataColumn(column) {
          Caption = column,
          DataType = typeof(string),
          AllowSort = false,
          ReadOnly = true,
          Width = 110,
        });
      }

      for (int i = 0; i < rowHeaders.Length; i++) {
        var dr = new DSDataRow();
        foreach (var column in columnHeaders) {
          dr[column] = new DSDataValue() {
            Value = this[GetColumnIndex(column), i],
          };
        }
        Rows.Add(dr);
      }
    }

    /// <summary>
    /// Gets the row count.
    /// </summary>
    /// <returns>The row count.</returns>
    public override int GetRowCount() {
      return this.rowCount;
    }

    /// <summary>
    /// Gets the row at the specified indexs
    /// </summary>
    /// <returns>The row.</returns>
    /// <param name="Index">Index.</param>
    /// <param name="index">Index.</param>
    public override DSDataRow GetRow(int index) {
      var ret = new DSDataRow();
      foreach (var column in columnHeaders) {
        var ci = GetColumnIndex(column);
        ret[column] = this[ci, index];
      }
      return ret;
    }

    /// <summary>
    /// Gets the row at the specified indexs
    /// </summary>
    /// <returns>The row.</returns>
    /// <param name="Index">Index.</param>
    /// <param name="RowId">Row identifier.</param>
    public override DSDataRow GetRow(string RowId) {
      throw new Exception("Not supported");
    }

    /// <summary>
    /// Gets the value.
    /// </summary>
    /// <returns>The value.</returns>
    /// <param name="RowIndex">Row index.</param>
    /// <param name="ColumnName">Column name.</param>
    public override DSDataValue GetValue(int rowIndex, string columnName) {
      return new DSDataValue() {
        Value = this[GetColumnIndex(columnName), rowIndex],
      };
    }

    /// <summary>
    /// Sets the value.
    /// </summary>
    /// <param name="RowIndex">Row index.</param>
    /// <param name="ColumnName">Column name.</param>
    /// <param name="Value">Value.</param>
    /// <param name="rowIndex">Row index.</param>
    /// <param name="columnName">Column name.</param>
    /// <param name="value">Value.</param>
    public override void SetValue(int rowIndex, string columnName, object value) {
      this[GetColumnIndex(columnName), rowIndex] = value as string;
    }

    /// <summary>
    /// Queries the index of the given column.
    /// </summary>
    /// <returns>The column index.</returns>
    /// <param name="columnName">Column name.</param>
    public int GetColumnIndex(string columnName) {
      var ci = -1;
      for (int i = 0; i < columnHeaders.Length; i++) {
        if (columnHeaders[i].Equals(columnName)) {
          ci = i;
          break;
        }
      }

      if (ci == -1) {
        throw new Exception("Cannot index of the column {" + columnName + "}: column does not exist in the table");
      }

      return ci;
    }
  }
}

