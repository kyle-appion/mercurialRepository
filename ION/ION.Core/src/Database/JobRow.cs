namespace ION.Core.Database {

  using SQLite.Net.Attributes;

  /// <summary>
  /// A job is an encapsulation of work performed for a po.
  /// </summary>
	[Preserve (AllMembers = true)]
  public class JobRow : ITableRow {
    [Ignore]
    public int _id {get { return JID;} set { JID = value;}}
    /// <summary>
    /// Queries the primary id of the table item.
    /// </summary>
    /// <value>The identifier.</value>
    [PrimaryKey, AutoIncrement]
    public int JID { get; set;}


    /// <summary>
    /// The user defined name of the job.
    /// </summary>
    /// <value>The name of the job.</value>
    public string jobName { get; set; }
    /// <summary>
    /// Gets or sets the po number.
    /// </summary>
    /// <value>The po number.</value>
    public string poNumber { get; set; }
    /// <summary>
    /// Gets or sets the customer number.
    /// </summary>
    /// <value>The customer number.</value>
    public string customerNumber { get; set; }
    /// <summary>
    /// Gets or sets the dispatch number.
    /// </summary>
    /// <value>The dispatch number.</value>
    public string dispatchNumber { get; set; }
    /// <summary>
    /// Gets or sets the notes.
    /// </summary>
    /// <value>The notes.</value>
    public string notes { get; set; }
    /// <summary>
    /// Gets or sets the name of the tech.
    /// </summary>
    /// <value>The name of the tech.</value>
    public string techName {get; set;}
    /// <summary>
    /// Gets or sets the type of the system.
    /// </summary>
    /// <value>The type of the system.</value>
    public string systemType { get; set;}
    /// <summary>
    /// Gets or sets the job location.
    /// </summary>
    /// <value>The job location.</value>
    public string jobLocation {get; set;}
    /// <summary>
    /// Gets or sets the job address.
    /// </summary>
    /// <value>The job address.</value>
    public string jobAddress {get; set;}
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

