namespace ION.Core.Sensors.Properties {

	using System;

	using Appion.Commons.Collections;
	using Appion.Commons.Measure;

	using ION.Core.Content;

	public class RateOfChangeSensorProperty : AbstractSensorProperty {

		private static TimeSpan GRAPH_WINDOW = TimeSpan.FromSeconds(30);
		private static TimeSpan GRAPH_INTERVAL = TimeSpan.FromMilliseconds(100);

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
		public PlotPoint[] primarySensorPoints {
			get {
				RegisterPoint();
				return primarySensorBuffer.ToArray();
			}
		}
		/// <summary>
		/// Returns the secondary sensor graph points that have been saved in the proprty.
		/// </summary>
		/// <value>The points.</value>
		public PlotPoint[] secondarySensorPoints {
			get {
				RegisterPoint();
				return secondarySensorBuffer.ToArray();
			}
		}

		/// <summary>
		/// Returns the tertiary sensor graph points that have been generated.
		/// </summary>
		/// <value>The tertiary points.</value>
		public PlotPoint[] tertiaryPoints {
			get {
				RegisterPoint();
				return tertiaryBuffer.ToArray();
			}
		}

		/// <summary>
		/// Returns whether or not the rate of change has a third change plot.
		/// </summary>
		/// <value><c>true</c> if has tertiary points; otherwise, <c>false</c>.</value>
		public bool hasTertiaryPoints {
			get {
				return manifold.HasSensorPropertyOfType(typeof(SuperheatSubcoolSensorProperty)) &&
					     manifold.secondarySensor != null;
			}
		}

		/// <summary>
		/// The buffer that will hold the primary sensor's history.
		/// </summary>
		private RingBuffer<PlotPoint> primarySensorBuffer;
		/// <summary>
		/// The buffer that will hold the primary sensor's history.
		/// </summary>
		private RingBuffer<PlotPoint> secondarySensorBuffer;
		/// <summary>
		/// The buffer that will hold the third plot history.
		/// </summary>
		private RingBuffer<PlotPoint> tertiaryBuffer;
		/// <summary>
		/// The last time that a record was added to the buffer.
		/// </summary>
		private DateTime lastRecord;

		private bool isRegisteredToSecondary;

		[Obsolete("Don't call this constructor. It is only used for the analyzer (and remote) in iOS and needs to be removed")]
		public RateOfChangeSensorProperty(Sensor sensor) : this(new Manifold(sensor)) {

		}

		public RateOfChangeSensorProperty(Manifold manifold) : this(manifold, GRAPH_WINDOW, GRAPH_INTERVAL) {
		}

		private RateOfChangeSensorProperty(Manifold manifold, TimeSpan window, TimeSpan interval) : base(manifold) {
			this.window = window;
			this.interval = interval;
			flags = EFlags.ShowAll;
			primarySensorBuffer = new RingBuffer<PlotPoint>((int)(window.TotalMilliseconds / interval.TotalMilliseconds));
			secondarySensorBuffer = new RingBuffer<PlotPoint>((int)(window.TotalMilliseconds / interval.TotalMilliseconds));
			tertiaryBuffer = new RingBuffer<PlotPoint>((int)(window.TotalMilliseconds / interval.TotalMilliseconds));
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

			if (manifold.secondarySensor != null && !isRegisteredToSecondary) {
				manifold.secondarySensor.onSensorStateChangedEvent += SensorChangeEvent;
			}
		}

		// Overridden from AbstractSensorProperty
		public override void Dispose() {
			base.Dispose();
			if (isRegisteredToSecondary) {
				if (manifold.secondarySensor != null) {
					manifold.secondarySensor.onSensorStateChangedEvent -= SensorChangeEvent;
				}
			}
		}

		// Overridden from AbstractSensorProperty
		protected override void OnManifoldEvent(ManifoldEvent e) {
			base.OnManifoldEvent(e);

			switch (e.type) {
				case ManifoldEvent.EType.SecondarySensorAdded: {
					if (manifold.secondarySensor != null && !isRegisteredToSecondary) {
						manifold.secondarySensor.onSensorStateChangedEvent += SensorChangeEvent;
					}
					break;
				} // ManifoldEvent.EType.SecondarySensorAdd
				case ManifoldEvent.EType.SecondarySensorRemoved: {
					if (manifold.secondarySensor != null && isRegisteredToSecondary) {
						manifold.secondarySensor.onSensorStateChangedEvent -= SensorChangeEvent;
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
		public Scalar GetPrimaryAverageRateOfChange(TimeSpan window, TimeSpan timeUnit) {
			var p = primarySensorPoints;
			var i = 0;
			// Find the oldest windowed item.
			while (i < p.Length - 1 && DateTime.Now - p[i].date < window) i++;
			var pu = sensor.unit.standardUnit;
			var pmag = (p[0].measurement - p[i].measurement) / (window.TotalMilliseconds / timeUnit.TotalMilliseconds);
			var ret = pu.OfScalar(pmag).ConvertTo(manifold.primarySensor.unit);
			return ret;
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
				
      var u = manifold.primarySensor.unit.standardUnit;
			return new MinMax(u.OfScalar(min), u.OfScalar(max));
		}

		public MinMax GetSecondaryMinMax() {
			if (manifold.secondarySensor == null) {
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

        var u = manifold.secondarySensor.unit.standardUnit;
				return new MinMax(u.OfScalar(min), u.OfScalar(max));
			}
		}

		public MinMax GetTertiaryMinMax() {
			if (!hasTertiaryPoints) {
				return null;
			} else {
				double min = double.MaxValue, max = double.MinValue;
				var points = tertiaryPoints;
				foreach (var dp in points) {
					if (dp.measurement < min) {
						min = dp.measurement;
					}
					if (dp.measurement > max) {
						max = dp.measurement;
					}
				}

				var shsc = manifold.GetSensorPropertyOfType(typeof(SuperheatSubcoolSensorProperty));
				var u = shsc.modifiedMeasurement.unit.standardUnit;
				return new MinMax(u.OfScalar(min), u.OfScalar(max));
			}
		}

		/// <summary>
		/// Sets the new window size for the graph sensor property.
		/// </summary>
		/// <param name="window">Window.</param>
		public void Resize(TimeSpan window, TimeSpan interval) {
			this.window = window;
			var size = (int)(window.TotalMilliseconds / interval.TotalMilliseconds);
			primarySensorBuffer.Resize(size);
			secondarySensorBuffer.Resize(size);
		}

		private void RegisterPoint() {
			Trim();
			if (DateTime.Now - lastRecord >= interval) {
				primarySensorBuffer.Add(new PlotPoint(sensor.measurement.ConvertTo(sensor.unit.standardUnit).amount));
				if (manifold.secondarySensor != null) {
					var ss = manifold.secondarySensor;
					secondarySensorBuffer.Add(new PlotPoint(ss.measurement.ConvertTo(ss.unit.standardUnit).amount));

					if (hasTertiaryPoints) {
						var shsc = manifold.GetSensorPropertyOfType(typeof(SuperheatSubcoolSensorProperty));
						var meas = shsc.modifiedMeasurement;
						tertiaryBuffer.Add(new PlotPoint(meas.ConvertTo(meas.unit.standardUnit).amount));

					}
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
