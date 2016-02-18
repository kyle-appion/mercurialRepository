namespace ION.Droid.Widgets.Adapters.Workbench {

  using System;

  using Android.Views;

  using ION.Core.Sensors;
  using ION.Core.Sensors.Properties;

  using ION.Droid.Util;
  using ION.Droid.Widgets.Templates;

  class MeasurementRecord : WorkbenchRecord<ISensorProperty> {
    public MeasurementRecord(ISensorProperty sp) : base(EViewType.MeasurementSubview, sp) {
    }
  }

  class MeasurementViewHolder : WorkbenchViewHolder<MeasurementRecord> {
    private MeasurementSubviewTemplate template;

    public MeasurementViewHolder(View view, BitmapCache cache) : base(view) {
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

