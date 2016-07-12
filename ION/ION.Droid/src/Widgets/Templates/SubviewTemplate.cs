namespace ION.Droid.Widgets.Templates {

	using System;

	using Android.Views;
	using Android.Widget;

	using ION.Core.Content;
	using ION.Core.Sensors.Properties;

	public abstract class SubviewTemplate<T> : ViewTemplate<T> where T : ISensorProperty {
		private View association;

		public SubviewTemplate(View view) : base(view) {
			association = view.FindViewById(Resource.Id.association);
		}

		/// <summary>
		/// Updates the association image for the subview template.
		/// </summary>
		/// <returns>The association.</returns>
		/// <param name="manifold">Manifold.</param>
		public void UpdateAssociation(Manifold manifold) {
			if (association != null) {
				if (manifold.IndexOfSensorProperty(item) >= manifold.sensorPropertyCount - 1) {
					association.SetBackgroundResource(Resource.Drawable.ic_association);
				} else {
					association.SetBackgroundResource(Resource.Drawable.ic_association_linked);
				}
			}
		}
	}
}

