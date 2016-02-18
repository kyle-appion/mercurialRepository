namespace ION.Droid.Widgets.Templates {
  
  using System;

  using Android.Views;
  using Android.Widget;

  using ION.Core.Sensors;
  using ION.Core.Sensors.Properties;

  /// <summary>
  /// A template for a superheat subcool subview.
  /// </summary>
  /// <code>
  /// FluidSubviewTemplate requires the following contract:
  ///   View          | id
  /// ----------------+----------------------------
  ///   TextView      | Resource.Id.title
  ///   TextView      | Resource.Id.fluid
  ///   View          | Resource.Id.view
  ///   TextView      | Resource.Id.measurement
  /// </code>
  public class SuperheatSubcoolSubviewTemplate : ViewTemplate<PTChartSensorProperty> {
    private TextView title;
    private TextView fluid;
    private View divider;
    private TextView measurement;

    public SuperheatSubcoolSubviewTemplate(View view) : base(view) {
      title = view.FindViewById<TextView>(Resource.Id.title);
      fluid = view.FindViewById<TextView>(Resource.Id.fluid);
      divider = view.FindViewById(Resource.Id.view);
      measurement = view.FindViewById<TextView>(Resource.Id.measurement);
    }

    /// <summary>
    /// Binds the view template to the given data.
    /// </summary>
    /// <param name="t">T.</param>
    protected override void OnBind(PTChartSensorProperty t) {
      t.onSensorPropertyChanged += OnSensorPropertyChanged;
    }

    /// <summary>
    /// Invalidates the view within the template.
    /// </summary>
    public override void Invalidate() {
      title.Text = parentView.Context.GetString(Resource.String.fluid_pt_abrv);
      fluid.Text = item.manifold.ptChart.fluid.name;

      switch (item.sensor.type) {
        case ESensorType.Pressure:
          measurement.Text = SensorUtils.ToFormattedString(ESensorType.Pressure, item.modifiedMeasurement);
          break;
        case ESensorType.Temperature:
          measurement.Text = SensorUtils.ToFormattedString(ESensorType.Temperature, item.modifiedMeasurement);
          break;
      }
    }

    /// <summary>
    /// Informs the view template that it should unbind itself from its data source.
    /// </summary>
    protected override void OnUnbind() {
      item.onSensorPropertyChanged -= OnSensorPropertyChanged;
    }

    /// <summary>
    /// Called when the sensor property's content is changed.
    /// </summary>
    /// <param name="property">Property.</param>
    private void OnSensorPropertyChanged (ISensorProperty property) {
      Invalidate();
    }
  }
}

