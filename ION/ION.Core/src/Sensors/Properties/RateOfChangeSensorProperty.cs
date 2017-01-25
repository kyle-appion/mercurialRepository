namespace ION.Core.Sensors.Properties {

	using System;

	using Appion.Commons.Measure;
	using Appion.Commons.Util;

  public class RateOfChangeSensorProperty : AbstractSensorProperty {

    private const long MAX_PLOT_LIFE = 60 * 1000;
		private const long STABILITY_TIME = 10 * 1000;

    /// <summary>
    /// The measurement of the rate of change property. Getting this property will
    /// return the current rate of change. Setting this property will add the value
    /// into the rate of change tracker. Set is expected to be called when the
    /// sensor's measurement changes.
    /// </summary>
    /// <value>The modified measurement.</value>
    // Overridden from AbstractSensorProperty
    public override Scalar modifiedMeasurement {
      get {
				tracker.AddPoint(sensor.measurement);
        var ret = tracker.rateOfChange.ConvertTo(sensor.unit);
				return ret;
      }
    }

		/// <summary>
		/// The tracker
		/// </summary>
		/// <value>The tracker.</value>
		public RateOfChange tracker { get; private set; }

    /// <summary>
    /// Whether or not the rate of change property is currently stable (not
    /// substantially fluctuating).
    /// </summary>
    /// <value><c>true</c> if is stable; otherwise, <c>false</c>.</value>
    public bool isStable {
      get {
				return tracker.rateOfChange.amount.DEquals(0);//!tracker.HasChangedIn(TimeSpan.FromMilliseconds(STABILITY_TIME));
      }
    }

    public RateOfChangeSensorProperty(Sensor sensor) : base(sensor) {
			tracker = new RateOfChange(sensor.unit.standardUnit, TimeSpan.FromMilliseconds(STABILITY_TIME), TimeSpan.FromMilliseconds(MAX_PLOT_LIFE));
    }

    // Overridden from AbstractSensorProperty
    public override void Reset() {
      if (tracker != null) {
        tracker.Clear();
      }
      NotifyChanged();
    }

    /// <summary>
    /// Called when the sensor property changes.
    /// </summary>
    protected override void OnSensorChanged() {
      tracker.AddPoint(sensor.measurement);
    }
  }


/*
  public class RateOfChange {
    private static TimeSpan DELAY = TimeSpan.FromMilliseconds(500);

    public Delta rateOfChange {
      get {
        lock (this) {
          Prune();

          Delta ret = new Delta(TimeSpan.FromMilliseconds(0), baseUnit.OfScalar(0)); 

          if (history.Count >= 2) {
            var tc = 0.0;

            foreach (var p in history) {
              tc += p.rise;
            }

            ret.change = baseUnit.OfScalar(tc / history.Count);
            ret.rate = TimeSpan.FromMilliseconds((history.Last.Value.time - history.First.Value.time) / history.Count);

            return ret;
          } else {
            return new Delta(TimeSpan.FromMilliseconds(0), baseUnit.OfScalar(0));
          }
        }
      }
    }

    public TimeSpan window {
      get {
        return __window;
      }
      set {
        __window = value;
        Prune();
      }
    } TimeSpan __window;

    private LinkedList<Point> history { get; set; }
    public Unit baseUnit { get; private set; }
    private DateTime lastAdd { get; set; }

    private double lastAmount;

    public RateOfChange(Unit baseUnit) {
      history = new LinkedList<Point>();
      this.baseUnit = baseUnit.standardUnit;
      window = TimeSpan.FromMilliseconds(1000);
      lastAmount = 0;
    }

    /// <summary>
    /// Adds a new plot point to the rate of change tracker.
    /// </summary>
    /// <param name="scalar">Scalar.</param>
    public void Add(Scalar scalar) {
      if (scalar.CompatibleWith(baseUnit.quantity)) {
        lock (this) {

          if (scalar.amount == 0 && lastAmount == 0) {
            var b = scalar.ConvertTo(baseUnit).amount; 
            history.AddLast(new Point(Now(), 0));
            lastAmount = b;
          } else {
            var b = scalar.ConvertTo(baseUnit).amount; 
            var rise = b - lastAmount;
            history.AddLast(new Point(Now(), rise));
            lastAmount = b;
          }
        }
      }
    }

    /// <summary>
    /// Clears the rate of change's history.
    /// </summary>
    public void Clear() {
      history.Clear();
      lastAdd = DateTime.Now;
    }

    /// <summary>
    /// Prunes the contents of the rate of change graph down to the window.
    /// </summary>
    private void Prune() {
      lock (this) {
        while (history.Count > 0 && Now() - history.First.Value.time > window.TotalMilliseconds) {
          history.RemoveFirst();
        }
      }
    }

    private long Now() {
      return DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
    }
  }

  public struct Delta {
    public TimeSpan rate;
    public Scalar change;

    public Delta(TimeSpan rate, Scalar change) : this() {
      this.rate = rate;
      this.change = change;
    }
  }

  internal class Point {
    /// <summary>
    /// The time in milliseconds
    /// </summary>
    public long time;
    /// <summary>
    /// The rise in increments of baseUnit.
    /// </summary>
    public double rise;

    public Point(long time, double rise) {
      Set(time, rise);
    }

    public Point Set(long time, double rise) {
      this.time = time;
      this.rise = rise;
      return this;
    }
  }

*/
}
