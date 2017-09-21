namespace ION.Core.Sensors.Properties {

	using System;

	using Appion.Commons.Measure;

	using ION.Core.Content;

  public class HoldSensorProperty : AbstractSensorProperty {

    public override Scalar modifiedMeasurement {
      get {
        return __modifiedMeasurement;
      }
      protected set {
        // Nope
      }
    } Scalar __modifiedMeasurement;

		//[Obsolete("Don't call this constructor. It is only used for the analyzer (and remote) in iOS and needs to be removed")]
		//public HoldSensorProperty(Sensor sensor) : base(new Manifold(sensor)) {
		//}

		//public HoldSensorProperty(Manifold manifold) : base(manifold){
		public HoldSensorProperty(Sensor sensor) : base(sensor) {
      // Nope
    }

    // Overridden from AbstractSensorProperty
    public override void Reset() {
      __modifiedMeasurement = sensor.measurement;
      NotifyChanged();
    }
  }
}

