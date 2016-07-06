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

    public RateOfChangeSubviewViewHolder(ViewGroup parent, int viewResource, BitmapCache cache) : base(parent, viewResource) {
      template = new RateOfChangeSubviewTemplate(view, cache);
    }

    public override void BindTo(RateOfChangeSubviewRecord t) {
      template.Bind(t.item);
			template.UpdateAssociation(t.manifold);
    }

    public override void Unbind() {
      template.Unbind();
    }
  }
}

