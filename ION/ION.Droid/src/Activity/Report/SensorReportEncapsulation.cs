namespace ION.Droid.Activity.Report {

  using System;
  using System.Collections.Generic;

	using OxyPlot.Series;

  using ION.Core.Devices;

  /// <summary>
  /// This encapsulation unit represents all of the data necessary to use flexcel to graph a collection of sensors
  /// recorded data of an arbitrary number of sessions.
  /// </summary>
	public class SensorReportEncapsulation {
    /// <summary>
    /// The sensor that this report encapsulates.
    /// </summary>
    public GaugeDeviceSensor sensor;
    /// <summary>
    /// The entity that will match dates to indices.
    /// </summary>
    public DateIndexLookup dil { get; private set; }
    /// <summary>
    /// The collection of series that make up the sensor's graphical representation.
    /// </summary>
    public PointSeries[] pointSeries { get; private set; }

    /// <summary>
    /// The minimum value of all of the point series.
    /// </summary>
    /// <value>The minimum.</value>
		public double min { get; private set; }
    /// <summary>
    /// The maximum value of all of the point series.
    /// </summary>
    /// <value>The max.</value>
		public double max { get; private set; }

    public SensorReportEncapsulation(GaugeDeviceSensor sensor, DateIndexLookup dil, PointSeries[] pointSeries) {
      this.sensor = sensor;
      this.dil = dil;
      this.pointSeries = pointSeries;

      min = double.MaxValue;
      max = double.MinValue;
			foreach (var s in pointSeries) {
				if (s.min < min) {
					min = s.min;
				}
        if (s.max > max) {
					max = s.max;
				}
			}

			var span = sensor.maxMeasurement.ConvertTo(sensor.unit.standardUnit).amount * 0.05;
			min -= span;
			max += span;
    }

    /// <summary>
    /// Builds a collection of lineseries that will make up the full session graph of this point series data.
    /// </summary>
    /// <returns>The series.</returns>
    public LineSeries[] CreateSeries() {
      var ret = new List<LineSeries>();

      foreach (var ps in pointSeries) {
        ret.Add(ps.BuildLineSeries(dil));
      }

      return ret.ToArray();
    }
  }
}
