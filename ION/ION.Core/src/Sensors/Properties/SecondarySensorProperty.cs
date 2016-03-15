using System;
using ION.Core.Content;
using ION.Core.Measure;

namespace ION.Core.Sensors.Properties {

  public class SecondarySensorProperty : AbstractSensorProperty  {
    // Overridden from AbstractSensorProperty
    public override Scalar modifiedMeasurement {
      get {
        return __modifiedMeasurement;
      }
      protected set {
        
      }
    } Scalar __modifiedMeasurement;

    public SecondarySensorProperty(Manifold manifold): base(manifold.primarySensor) {
      
    }

  }
}

