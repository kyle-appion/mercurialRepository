namespace ION.Core.Database {

  using System;

  using SQLite.Net.Attributes;

  /// <summary>
  /// A simple data object that represents a sensor's measurement that is stored in the database.
  /// </summary>
  /// <remarks>
  /// This entity is a struct due to the nature of its inteded use. Many thousands of these are expected to be loaded
  /// into an array, an we will need the most efficient storage medium possible to ensure the lowest system load.
  /// </remarks>
  public class SensorMeasurementRow : ITableRow {
    /// <summary>
    /// Queries the primary id of the table item.
    /// </summary>
    /// <value>The identifier.</value>
    [PrimaryKey, AutoIncrement]
    public int MID { get; set; }
    [Ignore]
    public int _id {get { return MID;} set { MID = value;}}
    /// <summary>
    /// The id of the session that the measurement was recorded under.
    /// </summary>
    /// <value>The session identifier.</value>
    [Indexed]
    public int frn_SID { get; set; }
    /// <summary>
    /// The id of the device who owns the sensor that stored the measurement.
    /// </summary>
    /// <value>The device identifier.</value>
    [Indexed]
    public int deviceId { get; set; }
    /// <summary>
    /// The sensors index within the device.
    /// </summary>
    /// <value>The index of the sensor.</value>
    public int sensorIndex { get; set; }
    /// <summary>
    /// The date that the measurement was recorded.
    /// </summary>
    /// <value>The recorded date.</value>
    public DateTime recordedDate { get; set; }
    /// <summary>
    /// The magnitude of the measurement. The unit for this measurement is the base unit for the device.
    /// </summary>
    /// <value>The measurement.</value>
    public double measurement { get; set; }
    /// <summary>
    /// The unit code for the sensor measurement. The measurement's unit can be retreived from
    /// ION.Core.Sensors.UnitLookup.GetUnit(int)
    /// </summary>
    /// <value>The unit code.</value>
//    public int unitCode { get; set; }

    /// <summary>
    /// Serves as a hash function for a <see cref="ION.Core.Database.SensorMeasurementRow"/> object.
    /// </summary>
    /// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a hash table.</returns>
    public override int GetHashCode() {
      return MID;
    }

    /// <summary>
    /// Determines whether the specified <see cref="System.Object"/> is equal to the current <see cref="ION.Core.Database.SensorMeasurementRow"/>.
    /// </summary>
    /// <param name="obj">The <see cref="System.Object"/> to compare with the current <see cref="ION.Core.Database.SensorMeasurementRow"/>.</param>
    /// <returns><c>true</c> if the specified <see cref="System.Object"/> is equal to the current
    /// <see cref="ION.Core.Database.SensorMeasurementRow"/>; otherwise, <c>false</c>.</returns>
    public override bool Equals(object obj) {
      var smr = obj as SensorMeasurementRow;
      return smr != null && MID == smr.MID;
    }

/*
    /// <summary>
    /// Returns a <see cref="System.String"/> that represents the current <see cref="ION.Core.Database.SensorMeasurementRow"/>.
    /// </summary>
    /// <returns>A <see cref="System.String"/> that represents the current <see cref="ION.Core.Database.SensorMeasurementRow"/>.</returns>
    public override string ToString() {
      return string.Format("[SensorMeasurementRow: id={0}, sessionId={1}, deviceId={2}, sensorIndex={3}, recordedDate={4}, measurement={5}, unitCode={6}]", id, sessionId, deviceId, sensorIndex, recordedDate, measurement, unitCode);
    }
*/

    public override string ToString() {
      return string.Format("[SensorMeasurementRow: id={0}, sessionId={1}, deviceId={2}, sensorIndex={3}, recordedDate={4}, measurement={5}]", MID, frn_SID, deviceId, sensorIndex, recordedDate, measurement);
    }
  }
}

