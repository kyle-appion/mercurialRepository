namespace ION.Droid.Widgets.Templates {
  
  using System;

  using Android.Graphics;
  using Android.Views;
  using Android.Widget;

  using ION.Core.Fluids;
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
	public class SuperheatSubcoolSubviewTemplate : SubviewTemplate<SuperheatSubcoolSensorProperty> {
    private TextView title;
    private TextView fluid;
    private View divider;
    private TextView measurement;
    private TextView unit;

    public SuperheatSubcoolSubviewTemplate(View view) : base(view) {
      title = view.FindViewById<TextView>(Resource.Id.title);
      fluid = view.FindViewById<TextView>(Resource.Id.fluid);
      divider = view.FindViewById(Resource.Id.view);
      measurement = view.FindViewById<TextView>(Resource.Id.measurement);
      unit = view.FindViewById<TextView>(Resource.Id.unit);
    }

    /// <summary>
    /// Binds the view template to the given data.
    /// </summary>
    /// <param name="t">T.</param>
    protected override void OnBind(SuperheatSubcoolSensorProperty t) {
      t.onSensorPropertyChanged += OnSensorPropertyChanged;
    }

    /// <summary>
    /// Invalidates the view within the template.
    /// </summary>
    public override void Invalidate() {
      Func<int, string> GetString = parentView.Context.GetString;
      var pt = item.manifold.ptChart;

      if (pt.fluid.mixture) {
        switch (pt.state) {
          case Fluid.EState.Bubble:
            title.Text = GetString(Resource.String.fluid_sc_abrv);
            break;
          case Fluid.EState.Dew:
            title.Text = GetString(Resource.String.fluid_sh_abrv);
            break;
        }
      } else {
        if (item.manifold.secondarySensor != null) {
          if (item.modifiedMeasurement <= 0) {
            title.Text = GetString(Resource.String.fluid_sh_abrv);
          } else {
            title.Text = GetString(Resource.String.fluid_sc_abrv);
          }
        } else {
          title.Text = GetString(Resource.String.fluid_sh_sc);
        }
      }

      fluid.Text = pt.fluid.name;
      fluid.SetBackgroundColor(new Color(pt.fluid.color));

      if (item.manifold.secondarySensor != null) {
        switch (item.sensor.type) {
          case ESensorType.Pressure:
            measurement.Text = SensorUtils.ToFormattedString(ESensorType.Temperature, item.modifiedMeasurement);
            break;
          case ESensorType.Temperature:
            measurement.Text = SensorUtils.ToFormattedString(ESensorType.Pressure, item.modifiedMeasurement);
            break;
        }

				unit.Text = item.temperatureSensor.unit.ToString();
      } else {
        measurement.Text = parentView.Context.GetString(Resource.String.fluid_setup);
				unit.Text = "";
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

