namespace ION.Core.Report.DataLogs {

  using System;

  /// <summary>
  /// This struct represents a single logging event that was stored in the database. This struct is slightly different
  /// than its counter part, SensorMeasurementRow, as it is meant to be loaded into memory in masse. As such, it is meant
  /// to be as memory efficient as possible.
  /// </summary>
  public class SensorLog {
    public double measurement { get; internal set; }
    public DateTime recordedDate { get; internal set; }

    public SensorLog(double measurement, DateTime recordedDate) {
      this.measurement = measurement;
			this.recordedDate = recordedDate.ToLocalTime();
    }
  }
}

