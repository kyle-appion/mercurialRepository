namespace ION.Core.Database {

  using SQLite.Net.Attributes;

  /// <summary>
  /// A job is an encapsulation of work performed for a po.
  /// </summary>
  public class Job : ITableRow {
    /// <summary>
    /// Queries the primary id of the table item.
    /// </summary>
    /// <value>The identifier.</value>
    [PrimaryKey, AutoIncrement]
    public int id { get; set;}
    /// <summary>
    /// The user defined name of the job.
    /// </summary>
    /// <value>The name of the job.</value>
    public string jobName { get; set; }
  }
}

