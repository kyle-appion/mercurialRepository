namespace ION.Droid.Widgets.Adapters.Workbench {
  
  using System;

  using Android.Views;

  using ION.Core.Sensors.Properties;

  using ION.Droid.Widgets.Templates;

  class PTChartSubviewRecord : WorkbenchRecord<PTChartSensorProperty> {
    public PTChartSubviewRecord(PTChartSensorProperty sp) : base(EViewType.PTChartSubview, sp) {
    }
  }

  class PTChartSubviewViewHolder : WorkbenchViewHolder<PTChartSubviewRecord> {
    private PTChartSubviewTemplate template;

    public PTChartSubviewViewHolder(View view) : base(view) {
      template = new PTChartSubviewTemplate(view);
    }

    public override void BindTo(PTChartSubviewRecord t) {
      template.Bind(t.item);
    }

    public override void Unbind() {
      template.Unbind();
    }
  }
}

