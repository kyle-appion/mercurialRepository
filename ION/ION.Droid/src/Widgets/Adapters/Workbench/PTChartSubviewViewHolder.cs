﻿namespace ION.Droid.Widgets.Adapters.Workbench {
  
  using System;

  using Android.Views;

  using ION.Core.Sensors.Properties;

  using ION.Droid.Widgets.Templates;

  class PTChartSubviewRecord : SensorPropertyRecord<PTChartSensorProperty> {
    public PTChartSubviewRecord(PTChartSensorProperty sp) : base(EViewType.PTChartSubview, sp) {
    }
  }

  class PTChartSubviewViewHolder : WorkbenchViewHolder<PTChartSubviewRecord> {
    private PTChartSubviewTemplate template;

    public PTChartSubviewViewHolder(ViewGroup parent, int viewResource) : base(parent, viewResource) {
      template = new PTChartSubviewTemplate(view);
    }

    public override void BindTo(PTChartSubviewRecord t) {
      template.Bind(t.item);
			template.UpdateAssociation(t.manifold);
    }

    public override void Unbind() {
      template.Unbind();
    }
  }
}

