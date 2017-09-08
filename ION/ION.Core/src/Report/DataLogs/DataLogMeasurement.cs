namespace ION.Core.Report.DataLogs {

  using System;
  using Appion.Commons.Measure;

	/// <summary>
	/// A simple structure that represents data log measurement entry.
	/// </summary>
  public struct DataLogMeasurement : IComparable<DataLogMeasurement> {
    /// <summary>
    /// The measurement from a SensorMeasurementRow.
    /// </summary>
    public Scalar measurement { get; private set; }
    /// <summary>
    /// The date that the date log measurement was recorded.
    /// </summary>
    /// <value>The recorded date.</value>
    public DateTime recordedDate { get; private set; }

    public DataLogMeasurement(Scalar measurement, DateTime recordedDate) {
      this.measurement = measurement;
      this.recordedDate = recordedDate;
    }

    // Implemented for IComparable<DataLogMeasurement.
    public int CompareTo(DataLogMeasurement dlm) {
      return recordedDate.CompareTo(dlm.recordedDate);
    }
	}
}
