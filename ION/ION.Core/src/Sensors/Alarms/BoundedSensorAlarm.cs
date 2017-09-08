namespace ION.Core.Sensors.Alarms {

	using Appion.Commons.Measure;

	using ION.Core.Sensors;

  public abstract class BoundedSensorAlarm : SensorAlarm {

    /// <summary>
    /// The bounds (limit) of the alarm. If the alarm's independant measurement exceeds
    /// this boundary in an implementation specific way, the alarm will trigger.
    /// </summary>
    /// <value>The bounds.</value>
    public Scalar bounds {
      get {
        return __bounds;
      }
      set {
        __bounds = value;
        // Perform alarm check.
        Fire();
      }
    } Scalar __bounds;

    public BoundedSensorAlarm(string name, string description, Sensor sensor) : base(name, description, sensor) {
      bounds = sensor.measurement;
    }
  }
}

