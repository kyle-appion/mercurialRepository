namespace ION.Droid.Widgets.Templates {
  
  using System;

  using Android.Graphics;
  using Android.Views;
  using Android.Widget;

  using ION.Core.Fluids;
  using ION.Core.Sensors;
  using ION.Core.Sensors.Properties;

  /// <summary>
  /// A template for a ptchart subview.
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
  public class PTChartSubviewTemplate : SubviewTemplate<PTChartSensorProperty> {
    private TextView title;
    private TextView fluid;
    private TextView measurement;
    private TextView unit;

    public PTChartSubviewTemplate(View view) : base(view) {
      title = view.FindViewById<TextView>(Resource.Id.title);
      fluid = view.FindViewById<TextView>(Resource.Id.fluid);
      measurement = view.FindViewById<TextView>(Resource.Id.measurement);
      unit = view.FindViewById<TextView>(Resource.Id.unit);
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
      var pt = item.manifold.ptChart;

      if (pt.fluid.mixture) {
        switch (pt.state) {
          case Fluid.EState.Bubble:
            title.Text = parentView.Context.GetString(Resource.String.fluid_bubble_abrv);
            break;
          case Fluid.EState.Dew:
            title.Text = parentView.Context.GetString(Resource.String.fluid_dew_abrv);
            break;
          default:
            break;
        }
      } else {
        title.Text = parentView.Context.GetString(Resource.String.fluid_pt_abrv);
      }
      fluid.Text = item.manifold.ptChart.fluid.name;
      fluid.SetBackgroundColor(new Color(item.manifold.ptChart.fluid.color));

      switch (item.sensor.type) {
        case ESensorType.Pressure:
          measurement.Text = SensorUtils.ToFormattedString(ESensorType.Pressure, item.modifiedMeasurement);
          break;
        case ESensorType.Temperature:
          measurement.Text = SensorUtils.ToFormattedString(ESensorType.Temperature, item.modifiedMeasurement);
          break;
      }

      unit.Text = item.unit.ToString();
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

