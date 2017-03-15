namespace ION.Core.Sensors.Properties {

	using System;
	using System.Collections.Generic;

	using OxyPlot;
	using OxyPlot.Series;

	using Appion.Commons.Collections;
	using Appion.Commons.Measure;
	using Appion.Commons.Util;

	using ION.Core.Content;

	public class GraphSensorProperty : AbstractSensorProperty {

		// Overridden from AbstractSensorProperty
		public override Scalar modifiedMeasurement { get { return base.modifiedMeasurement; } }

		public Manifold manifold { get; private set; }

		/// <summary>
		/// The window of time that the graph should show.
		/// </summary>
		public TimeSpan window { get; private set; }
		/// <summary>
		/// The minimum interval of time that a sensor should wait before registering a new plot point.
		/// </summary>
		public TimeSpan interval { get; private set; }
		/// <summary>
		/// Returns the graph points that have been saved in the proprty.
		/// </summary>
		/// <value>The points.</value>
		public PlotPoint[] points {
			get {
				RegisterPoint();
				return buffer.ToArray();
			}
		}

		/// <summary>
		/// The buffer that will hold the sensor's history.
		/// </summary>
		private RingBuffer<PlotPoint> buffer;
		/// <summary>
		/// The last time that a record was added to the buffer.
		/// </summary>
		private DateTime lastRecord;

		public GraphSensorProperty(Manifold manifold, TimeSpan window, TimeSpan interval) : base(manifold.primarySensor) {
			this.manifold = manifold;
			this.window = window;
			this.interval = interval;
			buffer = new RingBuffer<PlotPoint>((int)(window.TotalMilliseconds / interval.TotalMilliseconds));
		}

		// Overridden from AbstractSensorProperty
		public override void Reset() {
			if (buffer != null) {
				NotifyChanged();
				buffer.Clear();
			}
		}

		protected override void OnSensorChanged() {
			base.OnSensorChanged();
			RegisterPoint();
//			NotifyChanged();
		}

		/// <summary>
		/// Sets the new window size for the graph sensor property.
		/// </summary>
		/// <param name="window">Window.</param>
		public void SetWindow(TimeSpan window) {
			this.window = window;
			buffer.Resize((int)(window.TotalMilliseconds / interval.TotalMilliseconds));
		}

		/// <summary>
		/// Sets the new interval for the graph sensor property.
		/// </summary>
		/// <param name="interval">Interval.</param>
		public void SetInterval(TimeSpan interval) {
			this.interval = interval;
			buffer.Resize((int)(window.TotalMilliseconds / interval.TotalMilliseconds));
		}

		private void RegisterPoint() {
			if (DateTime.Now - lastRecord >= interval) {
				buffer.Add(new PlotPoint(sensor.measurement.ConvertTo(sensor.unit.standardUnit).amount));
				lastRecord = DateTime.Now;
			}
		}

		/// <summary>
		/// A structure representing the x,y points on the graph.
		/// </summary>
		public struct PlotPoint {
			/// <summary>
			/// The time that the measurement was made.
			/// </summary>
			public DateTime date { get; private set; }
			/// <summary>
			/// The measurement of a point on the graph. The measurement's unit is the base unit for the sensor.
			/// </summary>
			public double measurement { get; private set; }

			public PlotPoint(double measurement) {
				date = DateTime.Now;
				this.measurement = measurement;
			}

			// Overridden from object
			public override string ToString() {
				return string.Format("[PlotPoint: date={0}, measurement={1}]", date, measurement);
			}
		}
	}
}
