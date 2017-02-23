namespace ION.Core.Sensors.Properties {

	using System;

	using Appion.Commons.Measure;
	using Appion.Commons.Util;

  public class RateOfChangeSensorProperty : AbstractSensorProperty {

		private const long MAX_PLOT_LIFE = 60 * 1000;
		private const long STABILITY_TIME = 5 * 1000;

		private static DateTime DAWN_OF_TIME = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

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
				return tracker.rateOfChange.amount.DEquals(0);
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
}
