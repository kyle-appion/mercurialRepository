namespace ION.Droid.Fragments._Analyzer {

  using Android.Content;
  using Android.Text;
  using Android.Views;
  using Android.Widget;

  using ION.Core.Content;
	using ION.Core.Devices;
  using ION.Core.Sensors;

  /// <summary>
  /// A simple wrapper around the 
  /// </summary>
  public class SensorMount {
    /// <summary>
    /// The root view for the sensor mount. Will display the add ImageView when inactive and the sensor linear layout
    /// when active.
    /// </summary>
    public LinearLayout root;

    /// <summary>
    /// The view that is displayed when the sensor mount is inactive.
    /// </summary>
    public ImageView add;

    /// <summary>
    /// The view that will hold the sensor text views. This view is display when the sensor mount is active.
    /// </summary>
    public LinearLayout content;
    /// <summary>
    /// The text view showing the name of the sensor.
    /// </summary>
    public TextView title;
    /// <summary>
    /// The text view showing the measurement of the sensor.
    /// </summary>
    public TextView measurement;
    /// <summary>
    /// The text view showing the unit of the sensor.
    /// </summary>
    public TextView unit;

    /// <summary>
    /// The sensor that is providing the sensor mount's content data.
    /// </summary>
    /// <value>The sensor.</value>
    public Sensor sensor { 
      get {
        return __sensor;
      }

      set {
        if (__sensor != null) {
          __sensor.onSensorStateChangedEvent -= OnSensorStateChangedEvent;
        }

        __sensor = value;

        if (__sensor != null) {
          __sensor.onSensorStateChangedEvent += OnSensorStateChangedEvent;
          OnSensorStateChangedEvent(__sensor);

          add.Visibility = ViewStates.Gone;
          content.Visibility = ViewStates.Visible;
        } else {
          add.Visibility = ViewStates.Visible;
          content.Visibility = ViewStates.Gone;
        }

        isSet = false;
      }
    } Sensor __sensor;

    private Context context;
    /// <summary>
    /// The analyzer.
    /// </summary>
    private Analyzer analyzer;
    /// <summary>
    /// Is the side set;
    /// </summary>
    private bool isSet;

    /// <summary>
    /// Creates a new sensor mount.
    /// </summary>
    /// <param name="context">Context.</param>
    public SensorMount(Context context, Analyzer analyzer) {
      this.context = context;
      this.analyzer = analyzer;
      var white = Resource.Color.white.AsResourceColor(context);
			var black = Resource.Color.black.AsResourceColor(context);
      var gray = Resource.Color.gray.AsResourceColor(context);

      root = new LinearLayout(context);

      add = new ImageButton(context);
      add.SetBackgroundResource(Resource.Drawable.xml_rect_gold_black_bordered);
      add.SetImageResource(Resource.Drawable.ic_devices_add);
      add.Background.Alpha = (int)(0.55f * 255);
      add.Clickable = false;

      content = new LinearLayout(context);
      content.Orientation = Orientation.Vertical;
      content.Clickable = false;

      title = new TextView(context);
      title.Id = Resource.Id.title;
      title.Gravity = GravityFlags.CenterHorizontal;
			title.SetBackgroundResource(Resource.Drawable.shape_round_top_black);
      title.SetTextColor(white);
			title.SetTextSize(Android.Util.ComplexUnitType.Dip, context.Resources.GetDimension(Resource.Dimension.analyzer_sensor_mount_header));
      title.SetSingleLine(true);
      title.Ellipsize = TextUtils.TruncateAt.End;
      title.SetIncludeFontPadding(false);
      title.Text = "       ";

      measurement = new TextView(context);
			measurement.SetBackgroundResource(Resource.Drawable.shape_white_center);
      measurement.Id = Resource.Id.measurement;
      measurement.SetTextColor(gray);
			measurement.Gravity = GravityFlags.Right | GravityFlags.CenterVertical;
			measurement.SetTextSize(Android.Util.ComplexUnitType.Sp, context.Resources.GetDimension(Resource.Dimension.analyzer_sensor_mount_measurement));
      measurement.SetSingleLine(true);
      measurement.Ellipsize = TextUtils.TruncateAt.End;
      measurement.SetIncludeFontPadding(false);
      measurement.Text = "000,000";

      unit = new TextView(context);
			unit.SetBackgroundResource(Resource.Drawable.shape_round_bottom_white);
      unit.Id = Resource.Id.unit;
      unit.SetTextColor(gray);
			unit.Gravity = GravityFlags.Center;
      unit.SetSingleLine(true);
      unit.Ellipsize = TextUtils.TruncateAt.End;
      unit.SetIncludeFontPadding(false);
      unit.Text = "       ";

      var s1 = new View(context);
			s1.SetBackgroundResource(Resource.Drawable.shape_white_center);
      var s1lp = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.WrapContent, LinearLayout.LayoutParams.WrapContent);
      s1lp.Weight = 1;

      var s2 = new View(context);
			s2.SetBackgroundResource(Resource.Drawable.shape_white_center);
      var s2lp = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.WrapContent, LinearLayout.LayoutParams.WrapContent);
      s2lp.Weight = 1;

			var mlp = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, 0);
			mlp.Weight = 1;

      content.AddView(title, new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.WrapContent));
      content.AddView(s1, s1lp);
			content.AddView(measurement, new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.WrapContent));
      content.AddView(s2, s2lp);
      content.AddView(unit, new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.WrapContent));

			root.AddView(add, new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.MatchParent));
			root.AddView(content, new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.MatchParent));

      sensor = null;
    }

    /// <summary>
    /// Called when the sensor's state changes.
    /// </summary>
    /// <param name="sensor">Sensor.</param>
    private void OnSensorStateChangedEvent(Sensor sensor) {
      var gray = Resource.Color.gray.AsResourceColor(context);
      var black = Resource.Color.black.AsResourceColor(context);

      title.Text = sensor.name;
      measurement.Text = sensor.ToFormattedString(false);
      unit.Text = sensor.unit.ToString();

      Analyzer.ESide side;
      analyzer.GetSideOfSensor(sensor, out side);

      if (analyzer.IsSensorAttachedToManifold(sensor) && !isSet) {
        UpdateTitleBackground(side);
        isSet = true;
      }

			var gds = sensor as GaugeDeviceSensor;
			if (gds == null || gds.device.isConnected) {
				measurement.SetTextColor(black);
			} else {
				measurement.SetTextColor(gray);
			}
    }

    /// <summary>
    /// Updates the title bar's background to match the sensor's representation within the analyzer.
    /// </summary>
    /// <param name="side">Side.</param>
    private void UpdateTitleBackground(Analyzer.ESide side) {
      switch (side) {
        case Analyzer.ESide.Low:
					title.SetBackgroundResource(Resource.Drawable.shape_round_top_blue);
          title.SetTextColor(Resource.Color.white.AsResourceColor(context));
          break;
        case Analyzer.ESide.High:
					title.SetBackgroundResource(Resource.Drawable.shape_round_top_red);
          title.SetTextColor(Resource.Color.white.AsResourceColor(context));
          break;
        default:
					title.SetBackgroundResource(Resource.Drawable.shape_round_top_black);
          title.SetTextColor(Resource.Color.white.AsResourceColor(context));
          break;
      }
    }
  }
}

