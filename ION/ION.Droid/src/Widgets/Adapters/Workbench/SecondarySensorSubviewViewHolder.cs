namespace ION.Droid.Widgets.Adapters.Workbench {

	using Android.Views;

	using ION.Core.Sensors.Properties;

	using ION.Droid.Widgets.RecyclerViews;
	using ION.Droid.Widgets.Templates;

	class SecondarySensorSubviewRecord : SensorPropertyRecord<SecondarySensorProperty> {
		public SecondarySensorSubviewRecord(SecondarySensorProperty sp) : base(EViewType.SecondarySensorSubview, sp) {
		}
	}

	class SecondarySensorSubviewViewHolder : SwipableViewHolder<SecondarySensorSubviewRecord> {
		private SecondarySensorSubviewTemplate template;

		public SecondarySensorSubviewViewHolder(ViewGroup parent, int viewResource) : base(parent, viewResource) {
			template = new SecondarySensorSubviewTemplate(view);
		}

		public override void OnBindTo() {
			template.Bind(t.item);
			template.UpdateAssociation(t.manifold);
		}

		public override void Unbind() {
			template.Unbind();
		}
	}
}

