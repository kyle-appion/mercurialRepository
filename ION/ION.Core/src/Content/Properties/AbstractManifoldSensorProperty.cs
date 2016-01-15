using System;

using ION.Core.Measure;
using ION.Core.Sensors;
using ION.Core.Sensors.Properties;

namespace ION.Core.Content.Properties {
  public class AbstractManifoldSensorProperty : ISensorProperty {

    // Overridden from ISensorProperty
    public event OnSensorPropertyChanged onSensorPropertyChanged;

    // Overridden from ISensorProperty
    public Sensor sensor {
      get {
        return manifold.primarySensor;
      }
    }

    // Overridden from ISensorProperty
    public virtual Scalar modifiedMeasurement {
      get {
        return sensor.measurement;
      }
      protected set {
        // Nope
      }
    }

    // Overridden from ISensorProperty
    public virtual bool supportedReset {
      get {
        return false;
      }
    }

    /// <summary>
    /// The manifold that this property applies to.
    /// </summary>
    /// <value>The manifold.</value>
    public Manifold manifold { get; private set; }
    
    public AbstractManifoldSensorProperty(Manifold manifold) {
      this.manifold = manifold;
      manifold.onManifoldChanged += OnManifoldChanged;
    }

    // Overridden from ISensorProperty
    public void Dispose() {
      manifold.onManifoldChanged -= OnManifoldChanged;
    }

    // Overridden from ISensorProperty
    public virtual void Reset() {
      modifiedMeasurement = sensor.measurement;
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

    /// <summary>
    /// The delegate that is called when the property's manifold changes.
    /// </summary>
    /// <param name="manifold">Manifold.</param>
    private void OnManifoldChanged(Manifold manifold) {
      NotifyChanged();
    }
  }
}

