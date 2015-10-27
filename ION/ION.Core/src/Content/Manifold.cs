namespace ION.Core.Content {

  using System;
  using System.Collections.Generic;
  using System.Runtime.Serialization;

  using ION.Core.App;
  using ION.Core.Devices;
  using ION.Core.Fluids;
  using ION.Core.IO;
  using ION.Core.Measure;
  using ION.Core.Sensors;
  using ION.Core.Sensors.Properties;

  /// <summary>
  /// A Manifold is an abstract representation of a collection of sensors that
  /// come together to create and "explain" a certain context. For example: a
  /// manifold consiting of a pressure and a temperature sensor create a context
  /// that allows for a Pressure/Temperature lookup chart and a Superheat/subcool
  /// reference measurement.
  /// </summary>
  public class Manifold : IDisposable {
    private const int VERSION = 1;

    /// <summary>
    /// The delegate that is call when a manifold'a context is changed. This
    /// can be as simple as a reading update or an entirely new context has
    /// been added to the manifold.
    /// </summary>
    /// <param name="manifold"></param>
    /// <param name="context"></param>
    public delegate void OnManifoldChanged(Manifold manifold);
    /// <summary>
    /// The event handler that is notified when the manifold changes.
    /// </summary>
    public event OnManifoldChanged onManifoldChanged;

    /// <summary>
    /// The primary sensor for the manifold.
    /// </summary>
    public Sensor primarySensor {
      get {
        return __primarySensor;
      }
      private set {
        if (__primarySensor != null) {
          __primarySensor.onSensorStateChangedEvent -= OnManifoldSensorChanged;
        }

        __primarySensor = value;

        if (__primarySensor != null) {
          __primarySensor.onSensorStateChangedEvent += OnManifoldSensorChanged;
          OnManifoldSensorChanged(__primarySensor);
        }
      }
    } Sensor __primarySensor;

    /// <summary>
    /// The secondary sensor for the manifold.
    /// </summary>
    public Sensor secondarySensor {
      get {
        return __secondarySensor;
      }

      set {
        if (__secondarySensor != null) {
          __secondarySensor.onSensorStateChangedEvent -= OnManifoldSensorChanged;
        }

        __secondarySensor = value;

        if (__secondarySensor != null) {
          __secondarySensor.onSensorStateChangedEvent += OnManifoldSensorChanged;
          OnManifoldSensorChanged(__secondarySensor);
        }
        NotifyChanged();
      }
    } Sensor __secondarySensor;

    /// <summary>
    /// The sensor properties that are within the manifold.
    /// </summary>
    /// <value>The sensor properties.</value>
    public List<ISensorProperty> manifoldProperties { 
      get {
        return new List<ISensorProperty>(__sensorProperties);
      }
    } List<ISensorProperty> __sensorProperties = new List<ISensorProperty>();

    /// <summary>
    /// An indexer that will retrieve the sensor properties from the manifold.
    /// </summary>
    /// <param name="index">Index.</param>
    public ISensorProperty this[int index] {
      get {
        return __sensorProperties[index];
      }
    }

    /// <summary>
    /// The pt chart that the manifold is expected to work with.
    /// </summary>
    public PTChart ptChart {
      get {
        return __ptChart;
      }
      set {
        __ptChart = value;
        ION.Core.Util.Log.D(this, "pt chart state is " + __ptChart.state);
        NotifyChanged();
      }
    } PTChart __ptChart;


    public Manifold(Sensor primarySensor) {
      this.primarySensor = primarySensor;
    }

    // Overridden from IDispose
    public void Dispose() {
      primarySensor.onSensorStateChangedEvent -= OnManifoldSensorChanged;
      if (__secondarySensor != null) {
        __secondarySensor.onSensorStateChangedEvent -= OnManifoldSensorChanged;
      }
    }

    /// <summary>
    /// Adds the sensor property to the manifold if the manifold does not already have a
    /// sensor property of the given type.
    /// </summary>
    /// <param name="sensorProperty">Sensor property.</param>
    /// <returns>True if the property was added, false if the manifold already has the property.</returns>
    public bool AddSensorProperty(ISensorProperty sensorProperty) {
      if (HasSensorPropertyOfType(sensorProperty.GetType())) {
        return false;
      } else {
        __sensorProperties.Add(sensorProperty);
        return true;
      }
    }

    /// <summary>
    /// Queries the index of the given sensor property.
    /// </summary>
    /// <returns>The of sensor property.</returns>
    /// <param name="sensorProperty">Sensor property.</param>
    public int IndexOfSensorProperty(ISensorProperty sensorProperty) {
      return __sensorProperties.IndexOf(sensorProperty);
    }

    /// <summary>
    /// Removes the given sensor property from the manifold.
    /// </summary>
    /// <param name="sensorProperty">Sensor property.</param>
    public void RemoveSensorProperty(ISensorProperty sensorProperty) {
      __sensorProperties.Remove(sensorProperty);
      NotifyChanged();
    }

    /// <summary>
    /// Removes the sensor property at the given index.
    /// </summary>
    /// <param name="index">Index.</param>
    public void RemoveSensorPropertyAt(int index) {
      __sensorProperties.RemoveAt(index);
      NotifyChanged();
    }

    /// <summary>
    /// Queries whether or not the manifold has a sensor property of the given type.
    /// </summary>
    /// <returns><c>true</c> if this instance has sensor property of type; otherwise, <c>false</c>.</returns>
    /// <typeparam name="T">The 1st type parameter.</typeparam>
    public bool HasSensorPropertyOfType(Type type) {
      foreach (var prop in manifoldProperties) {
        if (prop.GetType().Equals(type)) {
          return true;
        }
      }

      return false;
    }

    /// <summary>
    /// Called when a manifold's sensor's reading changes.
    /// </summary>
    /// <param name="sensor"></param>
    /// <param name="reading"></param>
    private void OnManifoldSensorChanged(Sensor sensor) {
      NotifyChanged();
    }

    /// <summary>
    /// Call to notify all known OnManifoldChanged delegates of a change to the
    /// manifold.
    /// </summary>
    private void NotifyChanged() {
      if (onManifoldChanged != null) {
        onManifoldChanged(this);
      }
    }
  }
}

