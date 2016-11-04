namespace ION.Core.Sensors.Properties {

	using ION.Core.Content;
	using ION.Core.Measure;

  public class SecondarySensorProperty : AbstractSensorProperty  {
    // Overridden from AbstractSensorProperty
    public override Scalar modifiedMeasurement {
      get {
				if (manifold.secondarySensor == null) {
					return Units.Dimensionless.NONE.OfScalar(0);
				} else {
					return manifold.secondarySensor.measurement;
				}
      }
    }

		public override bool supportedReset {
			get {
				return false;
			}
		}

		/// <summary>
		/// The manifold whose sensor we are showing.
		/// </summary>
		/// <value>The manifold.</value>
		public Manifold manifold { get; private set; }
		/// <summary>
		/// Whether or not the manifold has a secondary sensor.
		/// </summary>
		/// <value><c>true</c> if has secondary sensor; otherwise, <c>false</c>.</value>
		public bool hasSecondarySensor { get { return manifold.secondarySensor != null; } }

    public SecondarySensorProperty(Manifold manifold): base(manifold.primarySensor) {
			this.manifold = manifold;
			manifold.onManifoldEvent += ManifoldEventListener;
			ION.Core.Util.Log.D(this, "We are initially registered to manifold: " + manifold.GetHashCode());
    }

		protected override void OnSensorChanged() {
			NotifyChanged();
		}

		public override void Dispose() {
			base.Dispose();
			manifold.onManifoldEvent -= ManifoldEventListener;
		}

		public void ManifoldEventListener(ManifoldEvent e) {
			ION.Core.Util.Log.D(this, "Received manifold event from: " + e.manifold.GetHashCode() + " we are listening to: " + manifold.GetHashCode());
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

