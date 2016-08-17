namespace ION.Droid.Widgets.Analyzer {

  using System;

  using Android.Views;

  using ION.Core.Sensors;
  using ION.Core.Sensors.Properties;

  using ION.Droid.Util;
  using ION.Droid.Widgets.Templates;

  class MeasurementRecord : SensorPropertyRecord<ISensorProperty> {
    public MeasurementRecord(ISensorProperty sp) : base(EViewType.MeasurementSubview, sp) {
    }
  }

  class MeasurementSubviewViewHolder : SubviewAdapterViewHolder<MeasurementRecord> {
    private MeasurementSubviewTemplate template;

    public MeasurementSubviewViewHolder(View view, BitmapCache cache) : base(view) {
      template = new MeasurementSubviewTemplate(view, cache);
    }

    public override void BindTo(MeasurementRecord t) {
      template.Bind(t.item);
    }

    public override void Unbind() {
      template.Unbind();
    }
  }
}

