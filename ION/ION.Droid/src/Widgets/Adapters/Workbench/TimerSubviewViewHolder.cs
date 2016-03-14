namespace ION.Droid.Widgets.Adapters.Workbench {
  
  using System;

  using Android.Views;

  using ION.Core.Sensors;
  using ION.Core.Sensors.Properties;

  using ION.Droid.Util;
  using ION.Droid.Widgets.Templates;

  class TimerSubviewRecord : SensorPropertyRecord<TimerSensorProperty> {
    public TimerSubviewRecord(TimerSensorProperty sp) : base(EViewType.TimerSubview, sp) {
    }
  }

  class TimerSubviewViewHolder : WorkbenchViewHolder<TimerSubviewRecord> {
    private TimerSubviewTemplate template;

    public TimerSubviewViewHolder(View view, BitmapCache cache) : base(view) {
      template = new TimerSubviewTemplate(view, cache);
    }

    public override void BindTo(TimerSubviewRecord t) {
      template.Bind(t.item);
    }

    public override void Unbind() {
      template.Unbind();
    }
  }
}

