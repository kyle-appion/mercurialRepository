using System;

using ION.Core.Fluids;
using ION.Core.Measure;
using ION.Core.Sensors;

namespace ION.Core.Content {
  /// <summary>
  /// A Manifold is an abstract representation of a collection of sensors that
  /// come together to create and "explain" a certain context. For example: a
  /// manifold consiting of a pressure and a temperature sensor create a context
  /// that allows for a Pressure/Temperature lookup chart and a Superheat/subcool
  /// reference measurement.
  /// </summary>
  public class Manifold : IDisposable {
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
    /// The fluid that the manifold is expected to work with.
    /// </summary>
    public Fluid fluid {
      get {
        return __fluid;
      }
      set {
        __fluid = fluid;
        NotifyChanged();
      }
    } Fluid __fluid;

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

