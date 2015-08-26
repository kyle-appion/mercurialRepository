using System;
using System.Runtime.Serialization;

using ION.Core.Content;
using ION.Core.Measure;
using ION.Core.Sensors;

namespace ION.Core.Sensors.Properties {

  /// <summary>
  /// The delegate that is called when the SensorProperty state or content changes.
  /// </summary>
  public delegate void OnSensorPropertyChanged(ISensorProperty property);

  /// <summary>
  /// A SensorProperty is an entity that procures the result of a modification 
  /// to a sensor base reading. For example, a SensorMaxProperty would retain
  /// the maximum measurement that the sensor received.
  /// </summary>
  public interface ISensorProperty : IDisposable {
    /// <summary>
    /// The event that will be notified when the sensor property changes.
    /// </summary>
    event OnSensorPropertyChanged onSensorPropertyChanged;

    /// <summary>
    /// The sensor that the property applies to.
    /// </summary>
    /// <value>The sensor.</value>
    Sensor sensor { get; }

    /// <summary>
    /// Queries the modified measurement of the property.
    /// </summary>
    Scalar modifiedMeasurement { get; }

    /// <summary>
    /// Resets the sensor property. Any retained state will be discarded and
    /// zeroed until the sensor updates.
    /// </summary>
    void Reset();
  }
}

