namespace ION.Droid.Widgets.Adapters.Workbench {

  using System;

  using Android.Views;

  using ION.Core.Sensors;
  using ION.Core.Sensors.Properties;

  using ION.Droid.Util;
  using ION.Droid.Widgets.Templates;

  class RateOfChangeSubviewRecord : SensorPropertyRecord<RateOfChangeSensorProperty> {
    public RateOfChangeSubviewRecord(RateOfChangeSensorProperty sp) : base(EViewType.RateOfChangeSubview, sp) {
    }
  }

  class RateOfChangeSubviewViewHolder : WorkbenchViewHolder<RateOfChangeSubviewRecord> {
    private RateOfChangeSubviewTemplate template;

    public RateOfChangeSubviewViewHolder(View view, BitmapCache cache) : base(view) {
      template = new RateOfChangeSubviewTemplate(view, cache);
    }

    public override void BindTo(RateOfChangeSubviewRecord t) {
      template.Bind(t.item);
    }

    public override void Unbind() {
      template.Unbind();
    }
  }
}

