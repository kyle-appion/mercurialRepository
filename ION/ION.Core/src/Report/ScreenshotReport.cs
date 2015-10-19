namespace ION.Core.Report {
  
  using System;


  /// <summary>
  /// A Screenshot report is the simplest report that ION offers. Simply, it is
  /// a PNG with meta data that the user my provide to describe what the PNG is
  /// descibing.
  /// </summary>
  public class ScreenshotReport {

    /// <summary>
    /// The date that the report was created.
    /// </summary>
    /// <value>The created.</value>
    public DateTime created { get; set; }
    /// <summary>
    /// The title of the report.
    /// </summary>
    /// <value>The name.</value>
    public string title { get; set; }
    /// <summary>
    /// The subtitle of the report.
    /// </summary>
    /// <value>The subtitle.</value>
    public string subtitle { get; set; }
    /// <summary>
    /// Gets or sets the table user defined string data that will be dsplayed in
    /// the report.
    /// </summary>
    /// <value>The table data.</value>
    public string[,] tableData { get; set; }
    /// <summary>
    /// The user notes for the screen shot.
    /// </summary>
    /// <value>The noted.</value>
    public string notes { get; set; }
    /// <summary>
    /// The bytes for the screenshot as a png.
    /// </summary>
    /// <value>The screenshot.</value>
    public byte[] screenshot { get; set; }

    public ScreenshotReport() {
      created = DateTime.Now;
      tableData = new string[0, 0];
      notes = "";
      screenshot = new byte[0];
    }
  }
}

