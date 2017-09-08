namespace ION.Core.Database {

  using System;

  using SQLite.Net.Attributes;

  /// <summary>
  /// This class acts as a table definition for sensors that are linked at the time of data logging. An instance of this
  /// class represents a single row of that table.
  /// </summary>
  [Preserve(AllMembers=true)]
  public class SessionLinkedSensors : ITableRow {
    public int _id { get { return 0; } set { } }
    /// <summary>
    /// The id of the session that the linked sensors belongs to.
    /// </summary>
    /// <value>The session identifier.</value>
    [Indexed]
    public int sessionId { get; set; }
    /// <summary>
    /// The serial number of the device.
    /// </summary>
    /// <value>The serial number.</value>
    public string serialNumber { get; set; }
    /// <summary>
    /// The index where the sensor lives in the device.
    /// </summary>
    /// <value>The index of the sensor.</value>
    public int sensorIndex { get; set; }
    /// <summary>
    /// The order in which the linked sensor appear in its batch.
    /// </summary>
    /// <value>The index of the linked.</value>
    public int linkedIndex { get; set; }

    /// <summary>
    /// Serves as a hash function for a <see cref="T:ION.Core.Database.SessionLinkedSensors"/> object.
    /// </summary>
    /// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a hash table.</returns>
    public override int GetHashCode() {
      return sessionId.GetHashCode() ^ serialNumber.GetHashCode() * sensorIndex.GetHashCode();
    }

    /// <summary>
    /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="T:ION.Core.Database.SessionLinkedSensors"/>.
    /// </summary>
    /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="T:ION.Core.Database.SessionLinkedSensors"/>.</param>
    /// <returns><c>true</c> if the specified <see cref="object"/> is equal to the current
    /// <see cref="T:ION.Core.Database.SessionLinkedSensors"/>; otherwise, <c>false</c>.</returns>
    public override bool Equals(object obj) {
      var sls = obj as SessionLinkedSensors;
      if (sls != null) {
        return sessionId == sls.sessionId && 
               serialNumber.Equals(sls.serialNumber) && 
               sensorIndex == sls.sensorIndex && 
               linkedIndex == sls.linkedIndex;
      } else {
        return false;
      }
    }

    /// <summary>
    /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:ION.Core.Database.SessionLinkedSensors"/>.
    /// </summary>
    /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:ION.Core.Database.SessionLinkedSensors"/>.</returns>
    public override string ToString() {
      return string.Format("[SessionLinkedSensors: _id={0}, sessionId={1}, serialNumber={2}, sensorIndex={3}, linkedIndex={4}]", _id, sessionId, serialNumber, sensorIndex, linkedIndex);
    }
  }
}
