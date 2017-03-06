namespace ION.Core.Sensors.Properties {

	using System;
	using System.Collections.Generic;

	using OxyPlot;
	using OxyPlot.Series;

	using Appion.Commons.Measure;

	using ION.Core.Content;

	public class GraphSensorProperty : AbstractSensorProperty {

		// Overridden from AbstractSensorProperty
		public override Scalar modifiedMeasurement { get { return base.modifiedMeasurement; } }

		public Manifold manifold { get; private set; }

		public GraphSensorProperty(Manifold manifold) {
			this.manifold = manifold;
		}

		public override void Reset() {
			NotifyChanged();
		}

		/// <summary>
		/// The object that will encapsulate a medium of graph data for the sensor property.
		/// </summary>
		private class GraphSeries {
			
		}
	}
}
