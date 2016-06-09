using System;

namespace ION.Core.Sensors.Properties {
  /// <summary>
  /// The timer sensor property is a sensor property who will maintain an aggragated time.
  /// The timer is wholly used by calling three methods: Start, Stop and Reset.
  /// 
  /// Start will set the timers temporal start point allowing for ellasped time to be
  /// updated on a per call basis.
  /// 
  /// Stop will clear the temporal start point and update the ellapsed time. Note: the
  /// ellapsed time is not cleared or reset. It is still available for use. This allows
  /// the timer to be stopped and started multiple time consecutively with each start /
  /// stop delta being added to the ellapsed time. This creates a kind of stop watch.
  /// 
  /// Reset is how ellapsed time is forcibly cleared. It is recommended that you call
  /// reset before each usage of reset to ensure that the timer does not have any
  /// artifacts.
  /// </summary>
  public class TimerSensorProperty : AbstractSensorProperty {

    // Overridden from AbstractSensorProperty
    public override ION.Core.Measure.Scalar modifiedMeasurement {
      get {
        return sensor.measurement;
      }
      protected set {
        // Nope
      }
    }

    public bool isStarted { get; private set; }

    public TimeSpan ellapsedTime {
      get {
        if (isStarted) {
          return DateTime.Now - startTime + __ellapsedTime;
        } else {
          return __ellapsedTime;
        }
      }
      private set {
        __ellapsedTime = value;
      }
    } TimeSpan __ellapsedTime;

    private DateTime startTime { get; set; }

    public TimerSensorProperty(Sensor sensor) : base(sensor) {
    }

    // Overridden from AbstractSensorProperty
    public override void Reset() {
      startTime = DateTime.Now;
      ellapsedTime = TimeSpan.FromMilliseconds(0);
      NotifyChanged();
    }

    /// <summary>
    /// Toggles whether or not the timer is running. Returns true if the toggles started the timer.
    /// </summary>
    public bool Toggle() {
      if (isStarted) {
        Stop();
      } else {
        Start();
      }

      return isStarted;
    }

    public void Start() {
      startTime = DateTime.Now;
      isStarted = true;
    }

    public void Stop() {
      ellapsedTime = DateTime.Now - startTime + __ellapsedTime;
      isStarted = false;
    }
  }
}

