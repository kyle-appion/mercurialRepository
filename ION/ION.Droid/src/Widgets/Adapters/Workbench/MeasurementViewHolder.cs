namespace ION.Droid.Widgets.Adapters.Workbench {

  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;

  using Android.App;
  using Android.Content;
  using Android.OS;
  using Android.Runtime;
  using Android.Views;
  using Android.Widget;

  using ION.Core.Sensors;
  using ION.Core.Sensors.Properties;

  using ION.Droid.Sensors.Properties;
  using ION.Droid.Util;
  using ION.Droid.Widgets.Templates;

  class MeasurementRecord : IRecord {
    public EViewType viewType {
      get {
        return EViewType.MeasurementSubview;
      }
    }

    public ISensorProperty sensorProperty { get; set; }

    public MeasurementRecord(ISensorProperty sensorProperty) {
      this.sensorProperty = sensorProperty;
    }
  }

  class MeasurementViewHolder : WorkbenchViewHolder<MeasurementRecord> {
    private MeasurementRecord record;
    private MeasurementSubviewTemplate template;

    public MeasurementViewHolder(View view, BitmapCache cache) : base(view) {
      template = new MeasurementSubviewTemplate(view, cache);
    }

    /// <summary>
    /// Binds to.
    /// </summary>
    /// <param name="t">T.</param>
    public override void BindTo(MeasurementRecord t) {
      Unbind();

      template.Bind(t.sensorProperty);
    }

    public override void Unbind() {
      template.Unbind();
    }
  }
}

