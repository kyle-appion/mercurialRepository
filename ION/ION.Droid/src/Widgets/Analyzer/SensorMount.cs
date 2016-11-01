namespace ION.Droid {

  using System;

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
      var gray = context.Resources.GetColor(Resource.Color.gray);

      root = new LinearLayout(context);

      add = new ImageButton(context);
      add.SetBackgroundResource(Resource.Drawable.np_rounded_rect_gold);
      add.SetImageResource(Resource.Drawable.ic_devices_add);
      add.Background.Alpha = (int)(0.55f * 255);
      add.Clickable = false;

      content = new LinearLayout(context);
      content.Orientation = Orientation.Vertical;
      content.Clickable = false;

      title = new TextView(context);
      title.Id = Resource.Id.title;
      title.Gravity = GravityFlags.CenterHorizontal;
      title.SetBackgroundResource(Resource.Drawable.np_half_rounded_square_upper_white);
      title.SetPadding(10, 10, 10, 0);
      title.SetTextColor(gray);
        title.SetSingleLine(true);
      title.Ellipsize = TextUtils.TruncateAt.End;
      title.SetIncludeFontPadding(false);
      title.Text = "       ";
//      title.SetTypeface(regular);

      measurement = new TextView(context);
      measurement.SetBackgroundResource(Resource.Drawable.np_rounded_square_middle);
      measurement.SetPadding(10, 0, 10, 0);
      measurement.Id = Resource.Id.measurement;
      measurement.SetTextColor(gray);
      measurement.Gravity = GravityFlags.Right;
//			measurement.SetTextSize(Android.Util.ComplexUnitType.Dip, context.Resources.GetDimension(Resource.Dimension.text_size_large));
      measurement.SetSingleLine(true);
      measurement.Ellipsize = TextUtils.TruncateAt.End;
      measurement.SetIncludeFontPadding(false);
      measurement.Text = "000,000";
//      measurement.SetTypeface(regular);

      unit = new TextView(context);
      unit.SetBackgroundResource(Resource.Drawable.np_half_rounded_square_lower_white);
      unit.SetPadding(10, 0, 10, 10);
      unit.Id = Resource.Id.unit;
      unit.SetTextColor(gray);
      unit.Gravity = GravityFlags.Right;
      unit.SetSingleLine(true);
      unit.Ellipsize = TextUtils.TruncateAt.End;
      unit.SetIncludeFontPadding(false);
      unit.Text = "       ";
//      unit.SetTypeface(regular);

      var s1 = new View(context);
      s1.SetBackgroundResource(Resource.Drawable.np_rounded_square_middle);
      var s1lp = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.WrapContent, LinearLayout.LayoutParams.WrapContent);
      s1lp.Weight = 1;

      var s2 = new View(context);
      s2.SetBackgroundResource(Resource.Drawable.np_rounded_square_middle);
      var s2lp = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.WrapContent, LinearLayout.LayoutParams.WrapContent);
      s2lp.Weight = 1;

      content.AddView(title, new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.WrapContent));
      content.AddView(s1, s1lp);
      content.AddView(measurement, new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.WrapContent));
      content.AddView(s2, s2lp);
      content.AddView(unit, new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.WrapContent));
//      content.SetBackgroundResource(Resource.Drawable.np_rounded_rect_white);

      root.AddView(add, new LinearLayout.LayoutParams(FrameLayout.LayoutParams.MatchParent, FrameLayout.LayoutParams.MatchParent));
      root.AddView(content, new LinearLayout.LayoutParams(FrameLayout.LayoutParams.MatchParent, FrameLayout.LayoutParams.MatchParent));

      sensor = null;
    }

    /// <summary>
    /// Called when the sensor's state changes.
    /// </summary>
    /// <param name="sensor">Sensor.</param>
    private void OnSensorStateChangedEvent(Sensor sensor) {
      title.Text = sensor.name;
      measurement.Text = sensor.ToFormattedString(false);
      unit.Text = sensor.unit.ToString();

      Analyzer.ESide side;
      analyzer.GetSideOfSensor(sensor, out side);

      if (analyzer.IsSensorAttachedToManifold(sensor) && !isSet) {
        UpdateTitleBackground(side);
        isSet = true;
      }
    }

    /// <summary>
    /// Updates the title bar's background to match the sensor's representation within the analyzer.
    /// </summary>
    /// <param name="side">Side.</param>
    private void UpdateTitleBackground(Analyzer.ESide side) {
      switch (side) {
        case Analyzer.ESide.Low:
          title.SetBackgroundResource(Resource.Drawable.np_half_rounded_square_upper_blue);
          title.SetTextColor(context.Resources.GetColor(Resource.Color.white));
          break;
        case Analyzer.ESide.High:
          title.SetBackgroundResource(Resource.Drawable.np_half_rounded_square_upper_red);
          title.SetTextColor(context.Resources.GetColor(Resource.Color.white));
          break;
        default:
          title.SetBackgroundResource(Resource.Drawable.np_half_rounded_square_upper_white);
          title.SetTextColor(context.Resources.GetColor(Resource.Color.black));
          break;
      }
    }
  }
}

