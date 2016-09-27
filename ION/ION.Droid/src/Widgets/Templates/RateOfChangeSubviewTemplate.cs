namespace ION.Droid.Widgets.Templates {

  using System;

  using Android.OS;
  using Android.Views;
  using Android.Widget;

  using ION.Core.Sensors;
  using ION.Core.Sensors.Properties;

  using ION.Droid.Sensors.Properties;
  using ION.Droid.Util;
  using ION.Droid.Views;

  /// <summary>
  /// A template for displaying a rate of change sensor property.
  /// </summary>
  /// <code>
  /// RateOfChangeSubviewTemplate requires the following view contract:
  ///   View            | id
  /// ------------------+-------------------------------
  ///   TextView        | Resource.Id.title
  ///   ImageView       | Resource.Id.icon
  ///   View            | Resource.Id.view
  ///   TextView        | Resource.Id.measurement
  /// </code>
	public class RateOfChangeSubviewTemplate : SubviewTemplate<RateOfChangeSensorProperty> {
    private const int MSG_INVALIDATE = 1;

    private BitmapCache cache;
    private TextView title;
    private ImageView icon;
    private View divider;
    private TextView measurement;

    private Handler handler;
    private bool wasStarted;

    public RateOfChangeSubviewTemplate(View view, BitmapCache cache) : base(view) {
      this.cache = cache;
      title = view.FindViewById<TextView>(Resource.Id.title);
      icon = view.FindViewById<ImageView>(Resource.Id.icon);
      divider = view.FindViewById(Resource.Id.view);
      measurement = view.FindViewById<TextView>(Resource.Id.measurement);

      handler = new Handler(HandleMessage);
    }

    /// <summary>
    /// Binds the view template to the given data.
    /// </summary>
    /// <param name="t">T.</param>
    protected override void OnBind(RateOfChangeSensorProperty t) {
      t.onSensorPropertyChanged += OnSensorPropertyChanged;
      handler.SendEmptyMessageDelayed(MSG_INVALIDATE, 333);
    }

    /// <summary>
    /// Invalidates the view within the template.
    /// </summary>
    public override void Invalidate() {
      var c = title.Context;

      title.Text = item.GetLocalizedStringAbreviation(parentView.Context);

      if (item.isStable) {
        measurement.Text = c.GetString(Resource.String.stable);
        icon.Visibility = ViewStates.Invisible;
      } else {
				var mod = item.modifiedMeasurement.amount;

				var dmax = item.sensor.maxMeasurement.amount / 10;

				if (System.Math.Abs(mod) >= dmax) {
          measurement.Text = ">" + (int)dmax;
        } else { 
          measurement.Text = "" + (int)mod;
        }

        icon.Visibility = ViewStates.Visible;
        if (mod < 0) {
          icon.SetImageBitmap(cache.GetBitmap(Resource.Drawable.ic_arrow_trenddown));
        } else {
          icon.SetImageBitmap(cache.GetBitmap(Resource.Drawable.ic_arrow_trendup));
        }
      }
    }

    /// <summary>
    /// Informs the view template that it should unbind itself from its data source.
    /// </summary>
    protected override void OnUnbind() {
      item.onSensorPropertyChanged -= OnSensorPropertyChanged;
      handler.RemoveMessages(MSG_INVALIDATE);
    }

    /// <summary>
    /// Handles the handler message that will update the template.
    /// </summary>
    /// <param name="message">Message.</param>
    private void HandleMessage(Message message) {
      if (item != null) {
        Invalidate();

        handler.SendEmptyMessageDelayed(MSG_INVALIDATE, 333);
      }
    }

    private void OnSensorPropertyChanged(ISensorProperty sensorProperty) {
      Invalidate();
    }
  }
}

