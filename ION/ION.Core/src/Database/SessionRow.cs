namespace ION.Core.Database {

  using System;

  using SQLite.Net.Attributes;

  /// <summary>
  /// An aggragation of sensor measurement contained into a logical component.
  /// </summary>
	[Preserve (AllMembers = true)]
  public class SessionRow : ITableRow {
    /// <summary>
    /// Queries the primary id of the table item.
    /// </summary>
    /// <value>The identifier.</value>
    [PrimaryKey, AutoIncrement]
    public int SID { get; set;}
    [Ignore]
    public int _id {get { return SID;} set { SID = value;}}

    /// <summary>
    /// The of the job that the session belongs to.
    /// </summary>
    /// <value>The job identifier.</value>
    [Indexed]
    public int frn_JID { get; set; }

    /// <summary>
    /// The start date of the session.
    /// </summary>
    /// <value>The session start.</value>
    public DateTime sessionStart { get; set; }
    /// <summary>
    /// The end date of the session.
    /// </summary>
    /// <value>The session end.</value>
    public DateTime sessionEnd { get; set; }

    /// <summary>
    /// Serves as a hash function for a <see cref="ION.Core.Database.SessionRow"/> object.
    /// </summary>
    /// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a hash table.</returns>
    public override int GetHashCode() {
      return SID;
    }

    /// <summary>
    /// Determines whether the specified <see cref="System.Object"/> is equal to the current <see cref="ION.Core.Database.SessionRow"/>.
    /// </summary>
    /// <param name="obj">The <see cref="System.Object"/> to compare with the current <see cref="ION.Core.Database.SessionRow"/>.</param>
    /// <returns><c>true</c> if the specified <see cref="System.Object"/> is equal to the current
    /// <see cref="ION.Core.Database.SessionRow"/>; otherwise, <c>false</c>.</returns>
    public override bool Equals(object obj) {
      var sr = obj as SessionRow;
      return sr != null && SID == sr.SID;
    }

    /// <summary>
    /// Returns a <see cref="System.String"/> that represents the current <see cref="ION.Core.Database.SessionRow"/>.
    /// </summary>
    /// <returns>A <see cref="System.String"/> that represents the current <see cref="ION.Core.Database.SessionRow"/>.</returns>
    public override string ToString() {
      return string.Format("[SessionRow: id={0}, jobId={1}, sessionStart={2}, sessionEnd={3}]", SID, frn_JID, sessionStart, sessionEnd);
    }
  }
}

