﻿﻿namespace ION.Core.Sensors.Properties {

	using System;

	using Appion.Commons.Collections;
	using Appion.Commons.Measure;
	using Appion.Commons.Util;

  using ION.Core.App;
	using ION.Core.Content;
  using ION.Core.Devices;

	public class RateOfChangeSensorProperty : AbstractSensorProperty {

    private static int POINT_LIMIT = 300;
		private static TimeSpan GRAPH_INTERVAL = TimeSpan.FromMilliseconds(100);
    private static TimeSpan ROC_WINDOW = TimeSpan.FromSeconds(30);
    private static TimeSpan ROC_INTERVAL = TimeSpan.FromMilliseconds(100);

		// Overridden from AbstractSensorProperty
		public override Scalar modifiedMeasurement { get { return base.modifiedMeasurement; } }

		/// <summary>
		/// The window of time that the graph should show.
		/// </summary>
		public TimeSpan window { get; private set; }
		/// <summary>
		/// The minimum interval of time that a sensor should wait before registering a new plot point.
		/// </summary>
		public TimeSpan interval { get; private set; }
		/// <summary>
		/// The flags for the sensor property.
		/// </summary>
		/// <value>The flags.</value>
		public EFlags flags { get; set; }
		/// <summary>
		/// Returns the primary sensor graph points that have been saved in the proprty.
		/// </summary>
		/// <value>The points.</value>
		public Slice<PlotPoint> primarySensorPoints {
			get {
				RegisterPoint();
				var len = primarySensorBuffer.ToArray(buffer);
        var ret = new Slice<PlotPoint>(buffer, len - 1);
        return ret;
			}
		}
		/// <summary>
		/// Returns the secondary sensor graph points that have been saved in the proprty.
		/// </summary>
		/// <value>The points.</value>
		public Slice<PlotPoint> secondarySensorPoints {
			get {
				RegisterPoint();
				var len = secondarySensorBuffer.ToArray(buffer);
        var ret = new Slice<PlotPoint>(buffer, len - 1);
        return ret;
			}
		}

		/// <summary>
		/// Whether or not the roc of change property is considered to be stable.
		/// </summary>
		/// <value><c>true</c> if is stable; otherwise, <c>false</c>.</value>
		public bool isStable {
			get {
        // todo ahodder@appioninc.com: crashes if manual sensor (max measurement is null)
				var scale = sensor.maxMeasurement.ConvertTo(sensor.unit).amount;
        var rocMag = primaryRateOfChange.ConvertTo(sensor.unit).magnitude;
				var now = DateTime.Now;

        var percent = Math.Abs(rocMag / scale);

				if (percent > 0.1) {
					percent = 0.1;
				} else if (percent < 0.02) {
					percent = 0.02;
				}

				var mag = percent * 100;

        var dt = now - lastRocChange;
        var ret = dt >= TimeSpan.FromSeconds(mag);
				return ret;
			}
		}

    /// <summary>
    /// Gets the primary rate of change.
    /// </summary>
    /// <value>The primary rate of change.</value>
    public ScalarSpan primaryRateOfChange { get; private set; }

    /// <summary>
    /// The buffer that will hold the rate of change.
    /// </summary>
    private RingBuffer<PlotPoint> rocBuffer;
		/// <summary>
		/// The buffer that will hold the primary sensor's history.
		/// </summary>
		private RingBuffer<PlotPoint> primarySensorBuffer;
		/// <summary>
		/// The buffer that will hold the primary sensor's history.
		/// </summary>
		private RingBuffer<PlotPoint> secondarySensorBuffer;
    /// <summary>
    /// The buffer that will act as the buffer for calulcations or returning.
    /// </summary>
    private PlotPoint[] buffer;
		/// <summary>
		/// The last time that a record was added to the buffer.
		/// </summary>
		private DateTime lastRecord;
    /// <summary>
    /// The last time that the roc buffer was updated.
    /// </summary>
    private DateTime lastRocRecord;
    /// <summary>
    /// The last time that the roc changed.
    /// </summary>
    private DateTime lastRocChange;

		private bool isRegisteredToSecondary;

		//		[Obsolete("Don't call this constructor. It is only used for the analyzer (and remote) in iOS and needs to be removed")]
		//		public RateOfChangeSensorProperty(Sensor sensor) : this(new Manifold(sensor)) {
		//		}

		//		public RateOfChangeSensorProperty(Manifold manifold) : this(manifold, GRAPH_INTERVAL) {
		//		}

		//public RateOfChangeSensorProperty(Manifold manifold, TimeSpan interval) : base(manifold){
		//public RateOfChangeSensorProperty(Sensor sensor, TimeSpan interval) : base(manifold){
		public RateOfChangeSensorProperty(Sensor sensor, TimeSpan interval) : base(sensor) {
      this.interval = interval;
      window = TimeSpan.FromMilliseconds(interval.TotalMilliseconds * POINT_LIMIT);
			flags = EFlags.ShowAll;
      var size = POINT_LIMIT;
      rocBuffer = new RingBuffer<PlotPoint>((int)(ROC_WINDOW.TotalMilliseconds / ROC_INTERVAL.TotalMilliseconds));
			primarySensorBuffer = new RingBuffer<PlotPoint>(size);
      secondarySensorBuffer = new RingBuffer<PlotPoint>(size);
      buffer = new PlotPoint[size];
      AppState.context.preferences.onPreferencesChanged += OnPreferencesChanged;
      primaryRateOfChange = sensor.unit.OfSpan(0);
		}

		// Overridden from AbstractSensorProperty
		public override void Reset() {
			if (primarySensorBuffer != null) {
				NotifyChanged();
				primarySensorBuffer.Clear();
			}

			if (secondarySensorBuffer != null) {
				NotifyChanged();
				secondarySensorBuffer.Clear();
			}

      // Clear the rate of change buffer
      if (rocBuffer != null) {
        rocBuffer.Clear();
      }

			if (sensor.linkedSensor != null && !isRegisteredToSecondary) {
				//sensor.linkedSensor.onSensorStateChangedEvent += SensorChangeEvent;
				sensor.linkedSensor.onSensorEvent += SensorChangeEvent;
			}
		}

		// Overridden from AbstractSensorProperty
		public override void Dispose() {
			base.Dispose();
			if (isRegisteredToSecondary) {
				if (sensor.linkedSensor != null) {
					//sensor.linkedSensor.onSensorStateChangedEvent -= SensorChangeEvent;
					sensor.linkedSensor.onSensorEvent -= SensorChangeEvent;
				}
			}
			AppState.context.preferences.onPreferencesChanged -= OnPreferencesChanged;
		}

		// Overridden from AbstractSensorProperty
		protected override void OnManifoldEvent(ManifoldEvent e) {
			base.OnManifoldEvent(e);

			switch (e.type) {
				case ManifoldEvent.EType.SecondarySensorAdded: {
					if (sensor.linkedSensor != null && !isRegisteredToSecondary) {
							//sensor.linkedSensor.onSensorStateChangedEvent += SensorChangeEvent;
							sensor.linkedSensor.onSensorEvent += SensorChangeEvent;
					}
					break;
				} 
				case ManifoldEvent.EType.SecondarySensorRemoved: {
					if (sensor.linkedSensor != null && isRegisteredToSecondary) {
							//sensor.linkedSensor.onSensorStateChangedEvent -= SensorChangeEvent;
							sensor.linkedSensor.onSensorEvent -= SensorChangeEvent;
					}
					break;
				} // Manifold.EType.SecondarySensorRemove
			}
		}

		// Overridden from AbstractSensorProperty
		protected override void OnSensorChanged() {
			base.OnSensorChanged();
			RegisterPoint();
		}

		/// <summary>
		/// Removes the given flags from the sensor property.
		/// </summary>
		/// <param name="flags">Flags.</param>
		public void RemoveFlags(EFlags flags) {
			this.flags &= ~flags;
		}

		/// <summary>
		/// Adds the given flags from the sensor property.
		/// </summary>
		/// <param name="flags">Flags.</param>
		public void AddFlags(EFlags flags) {
			this.flags |= flags;
		}

		/// <summary>
		/// Returns true if the sensor property's flags are active.
		/// </summary>
		/// <returns><c>true</c>, if flag was hased, <c>false</c> otherwise.</returns>
		/// <param name="flags">Flags.</param>
		public bool HasFlag(EFlags flags) {
			return (this.flags & flags) == flags;
		}

		/// <summary>
		/// Toggles the given flags in the sensor property.
		/// </summary>
		/// <returns>True if all of the given flags are active for the sensor property.</returns>
		/// <param name="flags">Flags.</param>
		public bool ToggleFlags(EFlags flags) {
			this.flags ^= flags;
			return (this.flags & flags) == flags; 
		}

		/// <summary>
		/// Queries the average rate of change of the graph.
		/// </summary>
		/// <returns>The average rate of change.</returns>
		/// <param name="window">Window.</param>
		/// <param name="timeUnit">The unit of time for this rate of change.</param>
		public ScalarSpan GetPrimaryAverageRateOfChange() {
      TrimRoc();
      var p = new PlotPoint[rocBuffer.count];
      var su = sensor.unit.standardUnit;
      var cnt = rocBuffer.ToArray(p);
			if (cnt <= 2) {
        return su.OfSpan(0);
      }

      return CalculateExponetialWeightedMovingAverage(p);
    }

		public MinMax GetPrimaryMinMax() {
			double min = double.MaxValue, max = double.MinValue;
			var points = primarySensorPoints;
			foreach (var dp in points) {
				if (dp.measurement < min) {
					min = dp.measurement;
				}
				if (dp.measurement > max) {
					max = dp.measurement;
				}
			}

			//var u = manifold.primarySensor.unit.standardUnit;
			var u = sensor.unit.standardUnit;
			return new MinMax(u.OfScalar(min), u.OfScalar(max));
		}

		public MinMax GetSecondaryMinMax() {
			//if (manifold.secondarySensor == null)	{
			if (sensor.linkedSensor == null) {
				return null;
			} else {
				double min = double.MaxValue, max = double.MinValue;
				var points = secondarySensorPoints;
				foreach (var dp in points) {
					if (dp.measurement < min) {
						min = dp.measurement;
					}
					if (dp.measurement > max) {
						max = dp.measurement;
					}
				}

				//var u = manifold.secondarySensor.unit.standardUnit;
				var u = sensor.linkedSensor.unit.standardUnit;
				return new MinMax(u.OfScalar(min), u.OfScalar(max));
			}
		}

    /// <summary>
		/// Sets the new window size for the graph sensor property.
		/// </summary>
		/// <param name="window">Window.</param>
		public void Resize(TimeSpan interval) {
      this.interval = interval;
			window = TimeSpan.FromMilliseconds(interval.TotalMilliseconds * POINT_LIMIT);
			var size = (int)(window.TotalMilliseconds / interval.TotalMilliseconds);
			primarySensorBuffer.Resize(size);
			secondarySensorBuffer.Resize(size);
		}

		private void RegisterPoint() {
			Trim();
      var gds = sensor as GaugeDeviceSensor;
      var su = gds.unit.standardUnit;
      if (gds != null && gds.device.isConnected && (DateTime.Now - lastRocRecord) >= TimeSpan.FromMilliseconds(100)) {
        var meas = gds.measurement;

        if (!rocBuffer.isEmpty && !rocBuffer.first.measurement.DEquals(meas.ConvertTo(su).amount)) {
          lastRocChange = DateTime.Now;
        }

        rocBuffer.Add(new PlotPoint(meas.ConvertTo(sensor.unit.standardUnit).amount));

        lastRocRecord = DateTime.Now;

        this.primaryRateOfChange = GetPrimaryAverageRateOfChange();
      }
			if (DateTime.Now - lastRecord >= interval) {
				primarySensorBuffer.Add(new PlotPoint(sensor.measurement.ConvertTo(sensor.unit.standardUnit).amount));
				//if (manifold.secondarySensor != null)	{
				if (sensor.linkedSensor != null) {
					//var ss = manifold.secondarySensor;
					var ss = sensor.linkedSensor;
					secondarySensorBuffer.Add(new PlotPoint(ss.measurement.ConvertTo(ss.unit.standardUnit).amount));
				}
				lastRecord = DateTime.Now;
			}
		}

		/// <summary>
		/// Ensure that the content of the buffer is up to date.
		/// </summary>
		private void Trim() {
			var now = DateTime.Now;
			while (now - primarySensorBuffer.last.date > window && primarySensorBuffer.RemoveLast());
			while (now - secondarySensorBuffer.last.date > window && secondarySensorBuffer.RemoveLast());
		}

    private void TrimRoc() {
      var now = DateTime.Now;
      while (now - rocBuffer.last.date > ROC_WINDOW && rocBuffer.RemoveLast());
    }

		/// <summary>
		/// Performs an exponetial weighted moving average calculation on the primary sensor's rate of change buffer.
		/// This algorithm is described here: http://www.statsref.com/HTML/index.html?moving_averages.html.
		/// </summary>
		/// <returns>The exponetial weighted moving average.</returns>
    private ScalarSpan CalculateExponetialWeightedMovingAverage(PlotPoint[] buffer) {

      var sum = 0.0;
      var initial = buffer[0];
      for (int i = 1; i < buffer.Length; i++) {
        var dt = (initial.date - buffer[i].date).TotalMilliseconds / ROC_WINDOW.TotalMilliseconds;

        var wi = 1 / dt;
        var xi = initial.measurement - buffer[i].measurement;

        sum += wi * xi;
      }
      sum /= buffer.Length;

      sum /= (initial.date - buffer[buffer.Length - 1].date).TotalMilliseconds / TimeSpan.FromMinutes(1).TotalMilliseconds;

      return sensor.unit.standardUnit.OfSpan(sum).ConvertTo(sensor.unit);
    }

    private void OnPreferencesChanged() {
      var ion = AppState.context;
      this.Resize(ion.preferences.device.trendInterval);
    }

		/// <summary>
		/// The flags that maintain the RoC's binary states.
		/// </summary>
		[Flags]
		public enum EFlags {
			ShowAll = ShowPrimary | ShowSecondary | ShowTertiary,
			ShowPrimary = 1 << 0,
			ShowSecondary = 1 << 1,
			ShowTertiary = 1 << 2,
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

		public struct RoC {
			public Scalar primaryRoc { get; private set; }
			public Scalar secondaryRoc { get; private set; }

			public RoC(Scalar primaryRoc, Scalar secondaryRoc) {
				this.primaryRoc = primaryRoc;
				this.secondaryRoc = secondaryRoc;
			}
		}

		public class MinMax {
			public Scalar min { get; private set; }
			public Scalar max { get; private set; }

			// TODO ahodder@appioninc.com: I don't think this needs to be conversion checked as the standard unit is used for all points
			public double diff { get { return max.amount - min.amount; } }

			public MinMax(Scalar min, Scalar max) {
				this.min = min;
				this.max = max;
			}
		}
	}
}
