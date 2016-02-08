namespace ION.Droid {

  using System;

  using Android.Content;
  using Android.Text;
  using Android.Views;
  using Android.Widget;

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
    public View root;

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
      }
    } Sensor __sensor;


    /// <summary>
    /// Creates a new sensor mount.
    /// </summary>
    /// <param name="context">Context.</param>
    public SensorMount(Context context) {
      var gray = context.Resources.GetColor(Resource.Color.gray);

      var ll = new LinearLayout(context);

      add = new ImageButton(context);
      add.SetBackgroundResource(Resource.Drawable.np_rounded_rect_gold);
      add.SetImageResource(Resource.Drawable.ic_devices_add);
      add.Background.Alpha = (int)(0.55f * 255);

      content = new LinearLayout(context);
      content.Orientation = Orientation.Vertical;

      title = new TextView(context);
      title.Id = Resource.Id.title;
      title.SetTextColor(gray);
      title.Gravity = GravityFlags.Left;
      title.SetSingleLine(true);
      title.Ellipsize = TextUtils.TruncateAt.End;
      title.SetIncludeFontPadding(false);
      title.Text = "Boogers";
//      title.SetTypeface(regular);

      measurement = new TextView(context);
      measurement.Id = Resource.Id.measurement;
      measurement.SetTextColor(gray);
      measurement.Gravity = GravityFlags.Left;
      measurement.SetSingleLine(true);
      measurement.Ellipsize = TextUtils.TruncateAt.End;
      measurement.SetIncludeFontPadding(false);
      measurement.Text = "000,000";
//      measurement.SetTypeface(regular);

      unit = new TextView(context);
      unit.Id = Resource.Id.unit;
      unit.SetTextColor(gray);
      unit.Gravity = GravityFlags.Left;
      unit.SetSingleLine(true);
      unit.Ellipsize = TextUtils.TruncateAt.End;
      unit.SetIncludeFontPadding(false);
      unit.Text = "Million";
//      unit.SetTypeface(regular);

      content.AddView(title);
      content.AddView(measurement);
      content.AddView(unit);
      content.SetBackgroundResource(Resource.Drawable.np_rounded_rect_white);


     ll.AddView(add, new LinearLayout.LayoutParams(FrameLayout.LayoutParams.MatchParent, FrameLayout.LayoutParams.MatchParent));
     ll.AddView(content, new LinearLayout.LayoutParams(FrameLayout.LayoutParams.MatchParent, FrameLayout.LayoutParams.MatchParent));

      root = ll;

      sensor = null;
    }

    /// <summary>
    /// Called when the sensor's state changes.
    /// </summary>
    /// <param name="sensor">Sensor.</param>
    private void OnSensorStateChangedEvent(Sensor sensor) {
      var s = sensor as GaugeDeviceSensor;

      title.Text = sensor.name;
      measurement.Text = sensor.ToFormattedString(false);
      unit.Text = sensor.unit.ToString();
    }
  }
}

