/*

namespace ION.IOS.ViewController.GaugeTesting {

  using System;
  using System.Collections.Generic;
  using System.Threading.Tasks;

  using CoreGraphics;
  using Foundation;
  using UIKit;

  using DSoft.Themes.Grid;
  using DSoft.Datatypes.Enums;
  using DSoft.Datatypes.Grid.Data;
  using DSoft.Datatypes.Types;
  using DSoft.Datatypes.Formatters;
  using DSoft.UI.Grid;

  using ION.Core.App;
  using ION.Core.Sensors;
  using ION.Core.Util;

  public class TestBenchViewController : UIViewController {


    /// <summary>
    /// The table of data that we are displaying.
    /// </summary>
    private TableDataSet table;
    /// <summary>
    /// The grid view.
    /// </summary>
    private DSGridView view;

    public TestBenchViewController() : base() {
      var scanButton = new UIButton(new CGRect(0, 0, 45, 30));
      scanButton.SetTitle("Scan", UIControlState.Normal);
      scanButton.TouchUpInside += (object sender, EventArgs e) => {
        ScanForDevices(ESensorType.Vacuum);
      };
      Title = "Gauge Test Bench";
      var b = new UIBarButtonItem(scanButton);
//      NavigationItem.RightBarButtonItem = b;

      var ch = new string[] { "Serial", "500,000", "150,000", "50,000", "25,000", "1,000", "500", "200"};

      var dm = AppState.context.deviceManager;
      var gds = dm.GetAllGaugeDeviceSensorsOfType(ESensorType.Vacuum);
      var serials = new string[gds.Count];

      for (int i = 0; i < gds.Count; i++) {
        serials[i] = gds[i].device.serialNumber.ToString();
      }

      var content = new string[ch.Length, serials.Length];

      for (int r = 0; r < serials.Length; r++) {
        content[0, r] = serials[r];
      }

      for (int c = 1; c < ch.Length; c++) {
        for (int r = 0; r < serials.Length; r++) {
          content[c,r] = "[" + c + "," + r + "]";
        }
      }

      table = new TableDataSet("Test Table", ch, serials, content);
    }

    /// <summary>
    /// Scans for the devices of the given sensor type.
    /// </summary>
    /// <param name="sensorType">Sensor type.</param>
    public async void ScanForDevices(ESensorType sensorType) {
      var loadingView = new LoadingOverlay(UIScreen.MainScreen.Bounds, "Scanning and connecting to all " + sensorType + " sensors.");
      View.Add(loadingView);
      var dm = AppState.context.deviceManager;

      await Task.Delay(5000);

      loadingView.Hide();
    }

    /// <summary>
    /// Views the did load.
    /// </summary>
    public override void ViewDidLoad() {
      base.ViewDidLoad();
      view = new DSGridView(new CGRect(0, 40, View.Bounds.Width, View.Bounds.Height - 40));
      view.DataSource = table;
      View.AddSubview(view);
    }
  }

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

  public class LoadingOverlay : UIView {
    private UIActivityIndicatorView activitySpinner;
    private UILabel loadingLabel;

    public LoadingOverlay(CGRect frame, string message) : base(frame) {
      // configurable bits
      BackgroundColor = UIColor.Black;
      Alpha = 0.75f;
      AutoresizingMask = UIViewAutoresizing.All;

      nfloat labelHeight = 22;
      nfloat labelWidth = Frame.Width - 20;

      // derive the center x and y
      nfloat centerX = Frame.Width / 2;
      nfloat centerY = Frame.Height / 2;

      // create the activity spinner, center it horizontall and put it 5 points above center x
      activitySpinner = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.WhiteLarge);
      activitySpinner.Frame = new CGRect (
        centerX - (activitySpinner.Frame.Width / 2) ,
        centerY - activitySpinner.Frame.Height - 20 ,
        activitySpinner.Frame.Width,
        activitySpinner.Frame.Height);
      activitySpinner.AutoresizingMask = UIViewAutoresizing.All;
      AddSubview (activitySpinner);
      activitySpinner.StartAnimating ();

      // create and configure the "Loading Data" label
      loadingLabel = new UILabel(new CGRect (
        centerX - (labelWidth / 2),
        centerY + 20 ,
        labelWidth ,
        labelHeight
      ));
      loadingLabel.BackgroundColor = UIColor.Clear;
      loadingLabel.TextColor = UIColor.White;
      loadingLabel.Text = message;
      loadingLabel.TextAlignment = UITextAlignment.Center;
      loadingLabel.AutoresizingMask = UIViewAutoresizing.All;
      AddSubview (loadingLabel);
    }

    /// <summary>
    /// Hide this instance.
    /// </summary>
    public void Hide() {
      UIView.Animate (
        0.5, // duration
        () => { Alpha = 0; },
        () => { RemoveFromSuperview(); }
      );
    }
  }
}

*/
