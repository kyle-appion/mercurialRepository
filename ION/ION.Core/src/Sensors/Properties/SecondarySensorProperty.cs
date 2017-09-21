namespace ION.Core.Sensors.Properties {

	using Appion.Commons.Measure;
	using Appion.Commons.Util;

	using ION.Core.Content;

  public class SecondarySensorProperty : AbstractSensorProperty  {
    // Overridden from AbstractSensorProperty
    public override Scalar modifiedMeasurement {
      get {
				//if (manifold.secondarySensor == null)	{
				if (sensor.linkedSensor == null) {
					return Units.Dimensionless.NONE.OfScalar(0);
				} else {
					//return manifold.secondarySensor.measurement;
					return sensor.linkedSensor.measurement;
				}
      }
    }

		public override bool supportedReset {
			get {
				return false;
			}
		}

		/// <summary>
		/// Whether or not the manifold has a secondary sensor.
		/// </summary>
		/// <value><c>true</c> if has secondary sensor; otherwise, <c>false</c>.</value>
		//public bool hasSecondarySensor { get { return manifold.secondarySensor != null; } }
		public bool hasSecondarySensor { get { return sensor.linkedSensor != null; } }

		//public SecondarySensorProperty(Manifold manifold) : base(manifold){
		public SecondarySensorProperty(Sensor sensor): base(sensor) {
			//manifold.onManifoldEvent += ManifoldEventListener;
    }

		protected override void OnSensorChanged() {
			NotifyChanged();
		}

		public override void Dispose() {
			base.Dispose();
			//manifold.onManifoldEvent -= ManifoldEventListener;
		}

		public void ManifoldEventListener(ManifoldEvent e) {
			switch (e.type) {
				case ManifoldEvent.EType.Invalidated:
				case ManifoldEvent.EType.SecondarySensorAdded:
				case ManifoldEvent.EType.SecondarySensorRemoved:
					OnSensorChanged();		
					break;
			}
		}
  }
}

