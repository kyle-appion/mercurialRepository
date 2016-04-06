namespace ION.Core.Database {

  using SQLite.Net.Attributes;

  /// <summary>
  /// A job is an encapsulation of work performed for a po.
  /// </summary>
  public class JobRow : ITableRow {
    [Ignore]
    public int _id { get { return id; } set { id = value; } } 
    /// <summary>
    /// Queries the primary id of the table item.
    /// </summary>
    /// <value>The identifier.</value>
    [PrimaryKey, AutoIncrement]
    public int JID { get; set;}

    [Ignore]
    public int _id {get { return JID;} set { JID = value;}}
    /// <summary>
    /// The user defined name of the job.
    /// </summary>
    /// <value>The name of the job.</value>
    public string jobName { get; set; }

    /// <summary>
    /// Serves as a hash function for a <see cref="ION.Core.Database.JobRow"/> object.
    /// </summary>
    /// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a hash table.</returns>
    public override int GetHashCode() {
      return JID;
    }
    /// <summary>
    /// Determines whether the specified <see cref="System.Object"/> is equal to the current <see cref="ION.Core.Database.JobRow"/>.
    /// </summary>
    /// <param name="obj">The <see cref="System.Object"/> to compare with the current <see cref="ION.Core.Database.JobRow"/>.</param>
    /// <returns><c>true</c> if the specified <see cref="System.Object"/> is equal to the current
    /// <see cref="ION.Core.Database.JobRow"/>; otherwise, <c>false</c>.</returns>
    public override bool Equals(object obj) {
      var jr = obj as JobRow;
      return jr != null && JID == jr.JID;
    }
    /// <summary>
    /// Returns a <see cref="System.String"/> that represents the current <see cref="ION.Core.Database.JobRow"/>.
    /// </summary>
    /// <returns>A <see cref="System.String"/> that represents the current <see cref="ION.Core.Database.JobRow"/>.</returns>
    public override string ToString() {
      return string.Format("[JobRow: id={0}, jobName={1}]", JID, jobName);
    }
  }
}

