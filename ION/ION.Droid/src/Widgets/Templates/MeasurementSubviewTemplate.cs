namespace ION.Droid.Widgets.Templates {

  using System;

  using Android.Views;
  using Android.Widget;

  using ION.Core.Sensors;
  using ION.Core.Sensors.Properties;

  using ION.Droid.Sensors.Properties;
  using ION.Droid.Util;
  using ION.Droid.Views;

  /// <summary>
  /// A template for a generic sensor property subview.
  /// </summary>
  /// <code>
  /// MeasurementSubviewTemplate requires the following contract:
  ///   View            | id
  /// ------------------+-----------------------------
  ///   TextView        | Resource.Id.title
  ///   ImageView       | Resource.Id.icon
  ///   View            | Resource.Id.view
  ///   TextView        | Resource.Id.measurement
  /// </code>
  public class MeasurementSubviewTemplate : ViewTemplate<ISensorProperty> {
    private BitmapCache cache { get; set; }
    private TextView title { get; set; }
    private ImageView icon { get; set; }
    private View divider { get; set; }
    private TextView measurement { get; set; }

    public MeasurementSubviewTemplate(View view, BitmapCache cache) : base(view) {
      this.cache = cache;
      title = view.FindViewById<TextView>(Resource.Id.title);
      icon = view.FindViewById<ImageView>(Resource.Id.icon);
      divider = view.FindViewById(Resource.Id.view);
      measurement = view.FindViewById<TextView>(Resource.Id.measurement);

      icon.SetOnClickListener(new ViewClickAction((v) => {
        if (item != null && item.supportedReset) {
          item.Reset();
        }
      }));
    }

    /// <summary>
    /// Binds the view template to the given data.
    /// </summary>
    /// <param name="t">T.</param>
    protected override void OnBind(ISensorProperty t) {
      t.onSensorPropertyChanged += OnSensorPropertyChanged;
    }

    /// <summary>
    /// Invalidates the view within the template.
    /// </summary>
    public override void Invalidate() {
      title.Text = item.GetLocalizedStringAbreviation(parentView.Context);

      if (item.supportedReset) {
        icon.SetImageBitmap(cache.GetBitmap(Resource.Drawable.ic_refresh));
        divider.Visibility = ViewStates.Visible;
      } else {
        icon.Visibility = ViewStates.Invisible;
        divider.Visibility = ViewStates.Invisible;
      }

      measurement.Text = SensorUtils.ToFormattedString(item.sensor.type, item.modifiedMeasurement, true);
    }

    /// <summary>
    /// Informs the view template that it should unbind itself from its data source.
    /// </summary>
    protected override void OnUnbind() {
      item.onSensorPropertyChanged -= OnSensorPropertyChanged;
    }

    private void OnSensorPropertyChanged(ISensorProperty sensorProperty) {
      Invalidate();
    }
  }
}

