namespace ION.Droid.Widgets.Adapters.Workbench {

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

  class MeasurementSubviewViewHolder : WorkbenchViewHolder<MeasurementRecord> {
    private MeasurementSubviewTemplate template;

    public MeasurementSubviewViewHolder(ViewGroup parent, int viewResource, BitmapCache cache) : base(parent, viewResource) {
      template = new MeasurementSubviewTemplate(view, cache);
    }

    public override void BindTo(MeasurementRecord t) {
      template.Bind(t.item);
			template.UpdateAssociation(t.manifold);
    }

    public override void Unbind() {
      template.Unbind();
    }
  }
}

