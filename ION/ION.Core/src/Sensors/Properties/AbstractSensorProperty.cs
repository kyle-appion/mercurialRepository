using System;
using System.Runtime.Serialization;

using ION.Core.IO;
using ION.Core.Measure;
using ION.Core.Sensors;

namespace ION.Core.Sensors.Properties {
  /// <summary>
  /// A common implementation of a sensor property that will allow for quick
  /// implementation for a sensor property and provides common utility methods.
  /// </summary>
  /// <remarks>
  /// As part of the implementation of this class, the modifiedMeasurement will
  /// automatically be "set" when the sensors measurement is changed. Thus, if
  /// you wish to react to changes within the property sensor, simply resolve
  /// them in modifiedMeasurement.set.
  /// </remarks>
  [DataContract(Name="AbstractSensorProperty")]
  public abstract class AbstractSensorProperty : ISensorProperty {
    // Overridden from ISensorProperty
    public event OnSensorPropertyChanged onSensorPropertyChanged;

    // Overridden from ISensorProperty
    public Sensor sensor { get; private set; }

    // Overridden from ISensorProperty
    public virtual Scalar modifiedMeasurement {
      get {
        return sensor.measurement;
      }
      protected set {
        // Nope
      }
    }

    /// <summary>
    /// Used by the serialization system.
    /// </summary>
    public AbstractSensorProperty() {
      // Nope
    }

    public AbstractSensorProperty(Sensor sensor) {
      this.sensor = sensor;
      Reset();
      this.sensor.onSensorStateChangedEvent += OnSensorChangedDelegate;
    }

    // Overridden from ISensorProperty
    public void Dispose() {
      sensor.onSensorStateChangedEvent -= OnSensorChangedDelegate;
    }

    // Overridden from ISensorProperty
    public virtual void Reset() {
      modifiedMeasurement = sensor.measurement;
    }

    /// <summary>
    /// The callback that will set the sensor's modified measurement to the
    /// sensor's new reading.
    /// </summary>
    /// <param name="sensor">Sensor.</param>
    private void OnSensorChangedDelegate(Sensor sensor) {
      modifiedMeasurement = sensor.measurement;
      NotifyChanged();
    }

    /// <summary>
    /// Notifies the properties onSensorPropertyChanged event that the property
    /// has changed.
    /// </summary>
    protected void NotifyChanged() {
      if (onSensorPropertyChanged != null) {
        onSensorPropertyChanged(this);
      }
    }
  }
}

