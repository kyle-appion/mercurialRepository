namespace ION.Core.Database {

  using System;

  using SQLite.Net.Attributes;

  /// <summary>
  /// An aggragation of sensor measurement contained into a logical component.
  /// </summary>
  public class Session : ITableRow {
    /// <summary>
    /// Queries the primary id of the table item.
    /// </summary>
    /// <value>The identifier.</value>
    [PrimaryKey, AutoIncrement]
    public int id { get; set;}
    /// <summary>
    /// The of the job that the session belongs to.
    /// </summary>
    /// <value>The job identifier.</value>
    public int jobId { get; set; }
    /// <summary>
    /// The start date of the session.
    /// </summary>
    /// <value>The session start.</value>
    public DateTime sessionStart { get; set;}
    /// <summary>
    /// The end date of the session.
    /// </summary>
    /// <value>The session end.</value>
    public DateTime sessionEnd { get; set;}
  }
}

