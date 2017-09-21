namespace ION.Core.Sensors.Properties {

	using System;

	using Appion.Commons.Measure;

	using ION.Core.Content;
	            
  /// <summary>
  /// A property that will retain the minimum measurement that a sensor makes.
  /// </summary>
  public class MinSensorProperty : AbstractSensorProperty {
    // Overridden from AbstractSensorProperty
    public override Scalar modifiedMeasurement {
      get {
        return __modifiedMeasurement;
      }
      protected set {
        if (value == null) {
        } else {
          if (__modifiedMeasurement == null || value < __modifiedMeasurement) {
            __modifiedMeasurement = value;
          }
        }
        NotifyChanged();
      }
    } Scalar __modifiedMeasurement;


		//[Obsolete("Don't call this constructor. It is only used for the analyzer (and remote) in iOS and needs to be removed")]
		//public MinSensorProperty(Sensor sensor) : base(new Manifold(sensor)){
		public MinSensorProperty(Sensor sensor) : base(sensor) {
		}

		//public MinSensorProperty(Manifold manifold) : base(manifold){
    //  // Nope
    //}

    // Overridden from AbstractSensorProperty
    public override void Reset() {
      __modifiedMeasurement = sensor.measurement;
      NotifyChanged();
    }
  }
}

