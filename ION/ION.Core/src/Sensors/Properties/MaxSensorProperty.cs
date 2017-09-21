namespace ION.Core.Sensors.Properties {

	using System;

	using Appion.Commons.Measure;

	using ION.Core.Content;

  /// <summary>
  /// A property that will retain the maximum measurement that a sensor makes.
  /// </summary>
  public class MaxSensorProperty : AbstractSensorProperty {
    // Overridden from AbstractSensorProperty
    public override Scalar modifiedMeasurement {
      get {
        return __modifiedMeasurement;
      }
      protected set {
        if (value == null) {
        } else {
          if (__modifiedMeasurement == null || value > __modifiedMeasurement) {
            __modifiedMeasurement = value;
          }
        }
        NotifyChanged();
      }
    } Scalar __modifiedMeasurement;

		//[Obsolete("Don't call this constructor. It is only used for the analyzer (and remote) in iOS and needs to be removed")]
		//public MaxSensorProperty(Sensor sensor) : base(new Manifold(sensor)) {
		//}

		//public MaxSensorProperty(Manifold manifold) : base(manifold){
		public MaxSensorProperty(Sensor sensor) : base(sensor) {
      // Nope
    }

    // Overridden from AbstractSensorProperty
    public override void Reset() {
      __modifiedMeasurement = sensor.measurement;
      NotifyChanged();
    }
  }
}

