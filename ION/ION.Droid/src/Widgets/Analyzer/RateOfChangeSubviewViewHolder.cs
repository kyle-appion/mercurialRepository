namespace ION.Droid.Widgets.Analyzer {

  using System;

  using Android.Views;

  using ION.Core.Sensors;
  using ION.Core.Sensors.Properties;

  using ION.Droid.Util;
	using ION.Droid.Widgets.RecyclerViews;
  using ION.Droid.Widgets.Templates;

  class RateOfChangeSubviewRecord : SensorPropertyRecord<RateOfChangeSensorProperty> {
    public RateOfChangeSubviewRecord(RateOfChangeSensorProperty sp) : base(EViewType.RateOfChangeSubview, sp) {
    }
  }

  class RateOfChangeSubviewViewHolder : SwipableViewHolder<RateOfChangeSubviewRecord> {
    private RateOfChangeSubviewTemplate template;

		public RateOfChangeSubviewViewHolder(ViewGroup parent, BitmapCache cache) : base(parent, Resource.Layout.subview_measurement_small) {
      template = new RateOfChangeSubviewTemplate(view, cache);
    }

    public override void OnBindTo() {
      template.Bind(t.item);
    }

    public override void Unbind() {
      template.Unbind();
    }
  }
}

