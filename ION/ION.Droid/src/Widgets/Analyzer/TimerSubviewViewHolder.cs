namespace ION.Droid.Widgets.Analyzer {
  
  using System;

  using Android.Views;

  using ION.Core.Sensors;
  using ION.Core.Sensors.Properties;

  using ION.Droid.Util;
	using ION.Droid.Widgets.RecyclerViews;
  using ION.Droid.Widgets.Templates;

  class TimerSubviewRecord : SensorPropertyRecord<TimerSensorProperty> {
    public TimerSubviewRecord(TimerSensorProperty sp) : base(EViewType.TimerSubview, sp) {
    }
  }

  class TimerSubviewViewHolder : SwipableViewHolder<TimerSubviewRecord> {
    private TimerSubviewTemplate template;

		public TimerSubviewViewHolder(ViewGroup parent, BitmapCache cache) : base(parent, Resource.Layout.subview_timer_small) {
      template = new TimerSubviewTemplate(view, cache);
    }

		public override void OnBindTo() {
      template.Bind(t.item);
    }

    public override void Unbind() {
      template.Unbind();
    }
  }
}

