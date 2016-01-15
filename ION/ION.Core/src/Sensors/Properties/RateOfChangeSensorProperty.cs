using System;
using System.Collections.Generic;

using ION.Core.Measure;

namespace ION.Core.Sensors.Properties {
  public class RateOfChangeSensorProperty : AbstractSensorProperty {

    private const long MAX_PLOT_LIFE = 5 * 1000;

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
        tracker.Add(sensor.measurement);
        var roc = tracker.rateOfChange;

        ION.Core.Util.Log.D(this, "(" + roc.change + " / " + roc.rate.TotalMilliseconds + ") * " + (TimeSpan.FromMinutes(1) - roc.rate).Milliseconds);
//        var ret = (roc.change / roc.rate.Milliseconds) * (TimeSpan.FromMinutes(1) - roc.rate).Milliseconds;
        var ret = (roc.change / roc.rate.TotalMilliseconds) * (TimeSpan.FromMinutes(1) - roc.rate).TotalMilliseconds;

        if (ret.Abs().amount <= 0.001) {
          ret = tracker.baseUnit.OfScalar(0);
        }

        return ret.ConvertTo(sensor.unit);
      }
      protected set {
        tracker.Add(value);
        NotifyChanged();
      }
    }

    public RateOfChange tracker { get; private set; }

    public Scalar stabilityScale { get; set; }
    /// <summary>
    /// Whether or not the rate of change property is currently stable (not
    /// substantially fluctuating).
    /// </summary>
    /// <value><c>true</c> if is stable; otherwise, <c>false</c>.</value>
    public bool isStable {
      get {
        return tracker.rateOfChange.change.Abs() <= stabilityScale.ConvertTo(tracker.baseUnit).amount;
      }
    }

    public RateOfChangeSensorProperty(Sensor sensor) : base(sensor) {
      tracker = new RateOfChange(sensor.unit.standardUnit);
      tracker.window = TimeSpan.FromMilliseconds(MAX_PLOT_LIFE);
      stabilityScale = sensor.unit.standardUnit.OfScalar(0.001);
    }

    // Overridden from AbstractSensorProperty
    public override void Reset() {
      if (tracker != null) {
        tracker.Clear();
      }
      NotifyChanged();
    }
  }

  public class RateOfChange {
    private static TimeSpan DELAY = TimeSpan.FromMilliseconds(500);

    public Delta rateOfChange {
      get {
        lock (this) {
          Prune();

          Delta ret = new Delta(TimeSpan.FromMilliseconds(0), baseUnit.OfScalar(0)); 

          var newest = history.Last;
          var oldest = history.First;

          if (newest != null && oldest != null && newest.Value != null && oldest.Value != null) { 
            ION.Core.Util.Log.D(this, "Time: " + (newest.Value.time - oldest.Value.time).TotalMilliseconds);
            ret = new Delta(newest.Value.time - oldest.Value.time, baseUnit.OfScalar(newest.Value.rise - oldest.Value.rise));
          } else {
            ret = new Delta(TimeSpan.FromMilliseconds(1), baseUnit.OfScalar(0));
          }

          if (ret.rate.Milliseconds == 0) {
            ret.rate = TimeSpan.FromMilliseconds(1);
          }

          return ret;
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

    public RateOfChange(Unit baseUnit) {
      history = new LinkedList<Point>();
      this.baseUnit = baseUnit.standardUnit;
      window = TimeSpan.FromMilliseconds(1000);
    }

    /// <summary>
    /// Adds a new plot point to the rate of change tracker.
    /// </summary>
    /// <param name="scalar">Scalar.</param>
    public void Add(Scalar scalar) {
      if (scalar.CompatibleWith(baseUnit.quantity)) {
        lock (this) {
//          if (DateTime.Now - lastAdd > DELAY) {
            var b = scalar.ConvertTo(baseUnit); 
            history.AddLast(new Point(DateTime.Now, b.amount));
//          }
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
        while (history.Count > 1 && history.Last.Value.time - history.First.Value.time > window) {
          history.RemoveFirst();
        }
      }
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
    public DateTime time;
    public double rise;

    public Point(DateTime time, double rise) {
      Set(time, rise);
    }

    public Point Set(DateTime time, double rise) {
      this.time = time;
      this.rise = rise;
      return this;
    }
  }
}
