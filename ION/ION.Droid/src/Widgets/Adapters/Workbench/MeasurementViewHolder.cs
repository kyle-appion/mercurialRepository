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
    private MeasurementRecord record { get; set; }

    private BitmapCache cache { get; set; }
    private TextView title { get; set; }
    private ImageView icon { get; set; }
    private View divider { get; set; }
    private TextView measurement { get; set; }

    public MeasurementViewHolder(BitmapCache cache, View view) : base(view) {
      this.cache = cache;
      title = view.FindViewById<TextView>(Resource.Id.title);
      icon = view.FindViewById<ImageView>(Resource.Id.icon);
      divider = view.FindViewById(Resource.Id.view);
      measurement = view.FindViewById<TextView>(Resource.Id.measurement);
    }

    /// <summary>
    /// Binds to.
    /// </summary>
    /// <param name="t">T.</param>
    public override void BindTo(MeasurementRecord t) {
      Unbind();

      record = t;
      record.sensorProperty.onSensorPropertyChanged += OnSensorPropertyChanged;

      Invalidate();
    }

    public override void Unbind() {
      if (record != null) {
        record.sensorProperty.onSensorPropertyChanged -= OnSensorPropertyChanged;
      }
    }

    private void Invalidate() {
      var sp = record.sensorProperty;
      title.Text = sp.GetLocalizedStringAbreviation(ItemView.Context);

      if (sp.supportedReset) {
        icon.SetImageBitmap(cache.GetBitmap(Resource.Drawable.ic_refresh));
        divider.Visibility = ViewStates.Visible;
      } else {
        icon.Visibility = ViewStates.Gone;
        divider.Visibility = ViewStates.Gone;
      }

      measurement.Text = SensorUtils.ToFormattedString(sp.sensor.type, sp.modifiedMeasurement, true);
    }

    private void OnSensorPropertyChanged(ISensorProperty sensorProperty) {
      Invalidate();
    }
  }
}

