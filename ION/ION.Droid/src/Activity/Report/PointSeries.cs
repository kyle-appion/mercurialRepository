namespace ION.Droid.Activity.Report {

  using System;

	using OxyPlot;
	using OxyPlot.Axes;
	using OxyPlot.Series;
	using OxyPlot.Xamarin.Android;

  using ION.Core.Report.DataLogs;
  
	public class PointSeries {
		public DeviceSensorLogs logs;
		/// <summary>
		/// The times that the measurements are mapped to.
		/// </summary>
		public DateTime[] times;
		public double[] measurements;

    public double min { get; private set; }
    public double max { get; private set; }

		public int length {
			get {
				return Math.Min(times.Length, measurements.Length);
			}
		}

		public PointSeries(DeviceSensorLogs logs) {
			this.logs = logs;

			var len = logs.logs.Count;
			times = new DateTime[len];
			measurements = new double[len];

			min = double.MaxValue;
			max = double.MinValue;

			for (int i = 0; i < len; i++) {
				var sl = logs.logs[i];
				times[i] = sl.recordedDate;
				measurements[i] = sl.measurement;
				if (measurements[i] < min) {
					min = measurements[i];
				}
				if (measurements[i] > max) {
					max = measurements[i];
				}
			}
		}

    public LineSeries BuildLineSeries(DateIndexLookup dil) {
      var ret = new LineSeries {
			  StrokeThickness = 1,
			  MarkerType = MarkerType.Circle,
			  MarkerSize = 0,
			  MarkerStroke = OxyColors.Transparent,
			  MarkerStrokeThickness = 0,
      };

      for (int i = 0; i < measurements.Length; i++) {
        ret.Points.Add(new DataPoint(dil.IndexOfDate(times[i]), measurements[i]));
      }

      return ret;
    }
  }
}
