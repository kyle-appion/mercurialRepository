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
  /// A template for displaying a timer sensor property.
  /// </summary>
  /// <code>
  /// MeasurementSubviewTemplate requires the following contract:
  ///   View            | id
  /// ------------------+-----------------------------
  ///   TextView        | Resource.Id.title
  ///   ImageView       | Resource.Id.icon
  ///   ImageView       | Resource.Id.play
  ///   View            | Resource.Id.view
  ///   TextView        | Resource.Id.measurement
  /// </code>
  public class TimerSubviewTemplate : ViewTemplate<TimerSensorProperty> {

    private const int MSG_INVALIDATE = 1;

    private BitmapCache cache { get; set; }
    private TextView title { get; set; }
    private ImageView icon { get; set; }
    private ImageView play { get; set; }
    private View divider { get; set; }
    private TextView measurement { get; set; }

    private Handler handler;
    private bool wasStarted;

    public TimerSubviewTemplate(View view, BitmapCache cache) : base(view) {
      this.cache = cache;
      title = view.FindViewById<TextView>(Resource.Id.title);
      icon = view.FindViewById<ImageView>(Resource.Id.icon);
      play = view.FindViewById<ImageView>(Resource.Id.play);
      divider = view.FindViewById(Resource.Id.view);
      measurement = view.FindViewById<TextView>(Resource.Id.measurement);

      handler = new Handler(HandleMessage);

      icon.SetOnClickListener(new ViewClickAction((v) => {
        if (item != null && item.supportedReset) {
          item.Reset();
        }
      }));

      play.SetOnClickListener(new ViewClickAction((v) => {
        if (item != null) {
          item.Toggle();
        }
      }));

      play.SetImageBitmap(cache.GetBitmap(Resource.Drawable.ic_action_play));
      wasStarted = false;
    }

    /// <summary>
    /// Binds the view template to the given data.
    /// </summary>
    /// <param name="t">T.</param>
    protected override void OnBind(TimerSensorProperty t) {
      t.onSensorPropertyChanged += OnSensorPropertyChanged;
      handler.SendEmptyMessageDelayed(MSG_INVALIDATE, 333);
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
        icon.Visibility = ViewStates.Gone;
        divider.Visibility = ViewStates.Gone;
      }

      if (item.isStarted) {
        play.SetImageBitmap(cache.GetBitmap(Resource.Drawable.ic_action_pause));
      } else {
        play.SetImageBitmap(cache.GetBitmap(Resource.Drawable.ic_action_play));
      }

      wasStarted = item.isStarted;

      if (item.ellapsedTime.Hours > 0) {
        measurement.Text = item.ellapsedTime.ToString("h'h 'm'm 's's'");
      } else {
        measurement.Text = item.ellapsedTime.ToString("m'm 's's'");
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

