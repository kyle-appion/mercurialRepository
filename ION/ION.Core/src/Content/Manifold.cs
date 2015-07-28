using System;

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
    public Sensor primarySensor { get; private set; }

    /// <summary>
    /// The secondary sensor for the manifold.
    /// </summary>
    public Sensor secondarySensor {
      get {
        return __secondarySensor;
      }

      set {
        if (__secondarySensor != null) {
          __secondarySensor.readingChanged -= OnManifoldSensorChanged;
        }

        __secondarySensor = value;

        if (__secondarySensor != null) {
          __secondarySensor.readingChanged += OnManifoldSensorChanged;
        }
        NotifyChanged();
      }
    } Sensor __secondarySensor;

    public Manifold() {
    }

    /// <summary>
    /// Called when a manifold's sensor's reading changes.
    /// </summary>
    /// <param name="sensor"></param>
    /// <param name="reading"></param>
    private void OnManifoldSensorChanged(Sensor sensor, Scalar reading) {

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

