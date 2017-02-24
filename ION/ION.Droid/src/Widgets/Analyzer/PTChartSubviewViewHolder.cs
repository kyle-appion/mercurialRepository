namespace ION.Droid.Widgets.Analyzer {
  
  using System;

  using Android.Views;

  using ION.Core.Sensors.Properties;

	using ION.Droid.Widgets.RecyclerViews;
  using ION.Droid.Widgets.Templates;

	class PTChartSubviewRecord : SensorPropertyRecord<PTChartSensorProperty> {
    public PTChartSubviewRecord(PTChartSensorProperty sp) : base(EViewType.PTChartSubview, sp) {
    }
  }

  class PTChartSubviewViewHolder : SwipableViewHolder<PTChartSubviewRecord> {
    private PTChartSubviewTemplate template;

		public PTChartSubviewViewHolder(ViewGroup parent) : base(parent, Resource.Layout.subview_fluid_small) {
      template = new PTChartSubviewTemplate(view);
    }

		public override void OnBindTo() {
      template.Bind(t.item);
    }

    public override void Unbind() {
      template.Unbind();
    }
  }
}

