using System;

using ION.Core.Alarms;
using ION.Core.Sensors;

namespace ION.Core.Sensors.Alarms {
  public abstract class SensorAlarm : AbstractAlarm {

    /// <summary>
    /// The sensor who this alarm will wrap.
    /// </summary>
    /// <value>The sensor.</value>
    public Sensor sensor { 
      get {
        return __sensor;
      }
      set {
        if (__sensor != null) {
					//__sensor.onSensorStateChangedEvent -= OnSensorChanged;
					__sensor.onSensorEvent -= OnSensorChanged;
        }

        __sensor = value;

        if (__sensor != null) {
					//__sensor.onSensorStateChangedEvent += OnSensorChanged;
					__sensor.onSensorEvent += OnSensorChanged;
        }
      }
    } Sensor __sensor;

    public SensorAlarm(string name, string description, Sensor sensor) : base(name, description) {
      this.sensor = sensor;
    }

    // Overridden from AbstractAlarm
    public override void Dispose() {
      sensor = null;
    }

    /// <summary>
    /// Called when the alarm's sensor's measurement changes.
    /// </summary>
    /// <param name="sensor">Sensor.</param>
    //private void OnSensorChanged(Sensor sensor)	{
    private void OnSensorChanged(SensorEvent sensorEvent) {
      Fire();
    }
  }
}

