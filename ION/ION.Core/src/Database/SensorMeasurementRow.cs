namespace ION.Core {

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
    public int id { get; set; }
    /// <summary>
    /// The id of the session that the measurement was recorded under.
    /// </summary>
    /// <value>The session identifier.</value>
    [Indexed]
    public int sessionId { get; set; }
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
    /// The magnitude of the measurement. The unit for the measurement is stored in unit.
    /// </summary>
    /// <value>The measurement.</value>
    public double measurement { get; set; }
    /// <summary>
    /// The unit code for the sensor measurement. The measurement's unit can be retreived from
    /// ION.Core.Sensors.UnitLookup.GetUnit(int)
    /// </summary>
    /// <value>The unit code.</value>
    public int unitCode { get; set; }
  }
}

