namespace ION.Droid.Widgets.Analyzer {
  
  using System;

  using Android.Views;

  using ION.Core.Sensors.Properties;

  using ION.Droid.Widgets.Templates;

  class PTChartSubviewRecord : SensorPropertyRecord<PTChartSensorProperty> {
    public PTChartSubviewRecord(PTChartSensorProperty sp) : base(EViewType.PTChartSubview, sp) {
    }
  }

  class PTChartSubviewViewHolder : SubviewAdapterViewHolder<PTChartSubviewRecord> {
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

