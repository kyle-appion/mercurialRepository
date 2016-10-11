namespace ION.Core.Measure {

	using System;
	using System.Collections.Generic;

	using ION.Core.Util;


	public class RateOfChange {

		/// <summary>
		/// Queries the current rate of change from the algorithm
		/// </summary>
		/// <value>The rate of change.</value>
		public Scalar rateOfChange {
			get {
				if (!dirty) {
					return __roc;
				}

				Prune();

				if (plot.Count <= 1) {
					return baseUnit.OfScalar(0);
				}


				var first = plot.First.Value;
				var last = plot.Last.Value;

				var dm = last.measurement - first.measurement;
				var dt = (last.time - first.time).TotalMinutes;

				return baseUnit.OfScalar(dm / dt);
			}
		} Scalar __roc; // Roc cache

		/// <summary>
		/// The time the rate of change is calculated against.
		/// </summary>
		public TimeSpan desiredTimeScale;
		/// <summary>
		/// The length of time that the rate of change will track measurements.
		/// </summary>
		public TimeSpan history;
		/// <summary>
		/// The plot.
		/// </summary>
		private LinkedList<PlotPoint> plot;
		/// <summary>
		/// The base unit used for the measurements.
		/// </summary>
		private Unit baseUnit;
		/// <summary>
		/// Whether or not a PlotPoint has been added since the rate of change has been recalculated.
		/// </summary>
		private bool dirty;

		public RateOfChange(Unit baseUnit, TimeSpan history, TimeSpan desiredTimeScale) {
			this.baseUnit = baseUnit;
			this.history = history;
			this.desiredTimeScale = desiredTimeScale;
			plot = new LinkedList<PlotPoint>();
			dirty = true;
		}

		/// <summary>
		/// Adds a new measurement to the RateOfChange using now as the current time.
		/// </summary>
		/// <param name="measurement">Measurement.</param>
		public bool AddPoint(Scalar measurement) {
			if (!measurement.unit.IsCompatible(baseUnit)) {
				return false;
			}

			Prune();

			plot.AddLast(new PlotPoint() {
				measurement = measurement.ConvertTo(baseUnit).amount,
				time = DateTime.Now,
			});

			dirty = true;

			return true;
		}

		/// <summary>
		/// Clears the rate of change such that it will start tracking a new window.
		/// </summary>
		public void Clear() {
			plot.Clear();
		}

		/// <summary>
		/// Queries whether or not the rate of change has change in the given time span.
		/// </summary>
		/// <returns><c>true</c>, if changed in was hased, <c>false</c> otherwise.</returns>
		/// <param name="timeSpan">Time span.</param>
		public bool HasChanged() {
			var now = DateTime.Now;

			if (plot.Count == 0) {
				return false;
			} else if (now - plot.First.Value.time < history) {
				return true;
			}

			var first = plot.First.Value;

			for (var node = plot.First; node != null; node = node.Next) {
				if (node.Value.time - now > history) {
					return false;
				} else {
					if (!first.measurement.DEquals(node.Value.measurement)) {
						return true;
					}
				}
			}

			return false;
		}

		/// <summary>
		/// Removes all of the plot points that fall outside of the history span.
		/// </summary>
		private void Prune() {
			lock (this) {
				while (plot.Count > 0 && DateTime.Now - plot.First.Value.time > history) {
					plot.RemoveFirst();
				}
			}
		}

		/// <summary>
		/// The structure representing a point on the rate of change graph.
		/// </summary>
		private struct PlotPoint {
			public double measurement;
			public DateTime time;
		}
	}


/*

	public class RateOfChange {

		/// <summary>
		/// Queries the current rate of change from the algorithm
		/// </summary>
		/// <value>The rate of change.</value>
		public Scalar rateOfChange {
			get {
				if (!dirty) {
					return __roc;
				}

				Prune();

				if (plot.Count <= 1) {
					return baseUnit.OfScalar(0);
				}

				LinkedListNode<PlotPoint> node = plot.First;

				double rise = 0;
				double run = (plot.Last.Value.time - node.Value.time).TotalMilliseconds;

				var lastMeas = node.Value.measurement;
				var startDate = node.Value.time;

				Log.D(this, "Starting averaging");
				while ((node = node.Next) != null) {
					var meas = node.Value.measurement;
					var dp = meas - lastMeas;
					var dt = (node.Value.time - startDate).TotalMilliseconds;
					rise += dp * dt / run;//(dp / dt);
					Log.D(this, "dp: " + dp + " dt: " + dt);
					lastMeas = meas;
				}
				Log.D(this, "Total rise: " + rise);

				dirty = false;

				__roc = baseUnit.OfScalar(rise / run * desiredTimeScale.TotalMilliseconds);
				return __roc;
			}
		} Scalar __roc; // Roc cache

		/// <summary>
		/// The time the rate of change is calculated against.
		/// </summary>
		public TimeSpan desiredTimeScale;
		/// <summary>
		/// The length of time that the rate of change will track measurements.
		/// </summary>
		public TimeSpan history;
		/// <summary>
		/// The plot.
		/// </summary>
		private LinkedList<PlotPoint> plot;
		/// <summary>
		/// The base unit used for the measurements.
		/// </summary>
		private Unit baseUnit;
		/// <summary>
		/// Whether or not a PlotPoint has been added since the rate of change has been recalculated.
		/// </summary>
		private bool dirty;

		public RateOfChange(Unit baseUnit, TimeSpan history, TimeSpan desiredTimeScale) {
			this.baseUnit = baseUnit;
			this.history = history;
			this.desiredTimeScale = desiredTimeScale;
			plot = new LinkedList<PlotPoint>();
			dirty = true;
		}

		/// <summary>
		/// Adds a new measurement to the RateOfChange using now as the current time.
		/// </summary>
		/// <param name="measurement">Measurement.</param>
		public bool AddPoint(Scalar measurement) {
			if (!measurement.unit.IsCompatible(baseUnit)) {
				return false;
			}

			Prune();

			plot.AddLast(new PlotPoint() {
				measurement = measurement.ConvertTo(baseUnit).amount,
				time = DateTime.Now,
			});

			dirty = true;

			return true;
		}

		/// <summary>
		/// Clears the rate of change such that it will start tracking a new window.
		/// </summary>
		public void Clear() {
			plot.Clear();
		}

		/// <summary>
		/// Queries whether or not the rate of change has change in the given time span.
		/// </summary>
		/// <returns><c>true</c>, if changed in was hased, <c>false</c> otherwise.</returns>
		/// <param name="timeSpan">Time span.</param>
		public bool HasChanged() {
			var now = DateTime.Now;

			if (plot.Count == 0) {
				return false;
			} else if (now - plot.First.Value.time < history) {
				return true;
			}

			var first = plot.First.Value;

			for (var node = plot.First; node != null; node = node.Next) {
				if (node.Value.time - now > history) {
					return false;
				} else {
					if (!first.measurement.DEquals(node.Value.measurement)) {
						return true;
					}
				}
			}

	    return false;
		}

		/// <summary>
		/// Removes all of the plot points that fall outside of the history span.
		/// </summary>
		private void Prune() {
			lock (this) {
				while (plot.Count > 0 && DateTime.Now - plot.First.Value.time > history) {
					plot.RemoveFirst();
				}
			}
		}

		/// <summary>
		/// The structure representing a point on the rate of change graph.
		/// </summary>
		private struct PlotPoint {
			public double measurement;
			public DateTime time;
		}
	}

*/
}
