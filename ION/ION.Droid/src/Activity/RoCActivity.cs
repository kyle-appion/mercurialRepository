namespace ION.Droid.Activity {

  using System;
  using System.Collections.Generic;

  using Android.App;
  using Android.Content;
  using Android.Content.PM;
  using Android.OS;
  using Android.Views;
  using Android.Widget;

  using OxyPlot;
  using OxyPlot.Axes;
  using OxyPlot.Series;
  using OxyPlot.Xamarin.Android;

  using Appion.Commons.Math;
  using Appion.Commons.Measure;
  using Appion.Commons.Util;

  using ION.Core.Content;
  using ION.Core.Devices;
  using ION.Core.Sensors;
  using ION.Core.Sensors.Properties;
  using ROCFlags = ION.Core.Sensors.Properties.RateOfChangeSensorProperty.EFlags;

  using ION.Droid.Content;
  using ION.Droid.Devices;
  using ION.Droid.Util;
  using ION.Droid.Sensors.Properties;
	using ION.Droid.Widgets;
	using ION.Droid.Widgets.RecyclerViews;

  [Activity(Label = "@string/live_trending", ScreenOrientation = ScreenOrientation.Portrait)]
  public class RoCActivity : IONActivity {
    /// <summary>
    /// The extra that pulls a ManifoldParcelable from the launching intent.
    /// </summary>
    public const string EXTRA_MANIFOLD = "ION.Droid.Activity.Extra.MANIFOLD";

    private PlotView plot;
    private Manifold manifold;
    private RateOfChangeSensorProperty roc;

    private TextView title;

    private View content1;
    private View content2;

    private TextView text1;
    private TextView text2;

    private ImageView icon1;
    private ImageView icon2;

		private RocWidgetManager rocManager;

		private Handler handler;

    // Overridden from IONActivity
    protected override void OnCreate(Bundle state) {
      base.OnCreate(state);

      ActionBar.SetDisplayHomeAsUpEnabled(true);

      SetContentView(Resource.Layout.activity_roc);

      plot = FindViewById<PlotView>(Resource.Id.graph);

      title = FindViewById<TextView>(Resource.Id.title);

      content1 = FindViewById(Resource.Id._1);
      content2 = FindViewById(Resource.Id._2);

			text1 = content1.FindViewById<TextView>(Resource.Id.text);
			text2 = content2.FindViewById<TextView>(Resource.Id.text);

			icon1 = content1.FindViewById<ImageView>(Resource.Id.icon);
      icon2 = content2.FindViewById<ImageView>(Resource.Id.icon);

      LoadManifold();

      handler = new Handler(OnHandleMessage);
    }

    // Overridden from Activity
    protected override void OnResume() {
      base.OnResume();
      handler.SendEmptyMessageDelayed(0, 100);
    }

    // Overridden from Activity
    protected override void OnPause() {
      base.OnPause();
      handler.RemoveCallbacksAndMessages(null);
    }

    // Overridden from Activity
    public override bool OnMenuItemSelected(int featureId, IMenuItem item) {
      switch (item.ItemId) {
        case Android.Resource.Id.Home:
          SetResult(Result.Canceled);
          Finish();
          return true;
        default:
          return base.OnMenuItemSelected(featureId, item);
      }
    }

    private void Invalidate() {
			rocManager.Invalidate();
    }

    private void OnHandleMessage(Message message) {
      Invalidate();
      handler.SendEmptyMessageDelayed(0, 100);
    }

    private void LoadManifold() {
      var mp = Intent.GetParcelableExtra(EXTRA_MANIFOLD) as ManifoldParcelable;
      if (mp == null) {
        // TODO ahodder@appioninc.com: Localize
        Error("US Manifold was not passed to activity");
        Finish();
        return;
      }

      manifold = mp.Get(ion);
      if (manifold == null) {
        // TODO ahodder@appioninc.com: Localize
        Error("US Manifold did not load from parcelable");
        Finish();
        return;
      }

      // Primary sensor
      var ps = manifold.primarySensor;
      text1.Text = ps.type.GetSensorTypeName() + " " + ps.name;

      // Secondary sensor
      if (manifold.secondarySensor != null) {
        var ss = manifold.secondarySensor;
        text2.Text = ss.type.GetSensorTypeName() + " " + ss.name;
      } else {
        content2.Visibility = ViewStates.Gone;
      }

      // Title
      var values = Resources.GetStringArray(Resource.Array.preferences_device_trend_interval_values);
      var entries = Resources.GetStringArray(Resource.Array.preferences_device_trend_interval_entries);
      var interval = ion.preferences.device.trendInterval;

      var index = -1;
      for (int i = 0; i < values.Length; i++) {
        if (values[i].Equals((int)interval.TotalMilliseconds + "")) {
          index = i;
        }        
      }

      if (index == -1) {
        Log.E(this, "Failed to find text for interval: " + interval);
      } else {
        title.Text = string.Format(GetString(Resource.String.trend_update_1arg), entries[index]);
      }

      roc = manifold.GetSensorPropertyOfType<RateOfChangeSensorProperty>();
      if (roc == null) {
        Error("How did you get here if the manifold doesn't have a RateOfChangeSensorProperty");
        Finish();
        return;
      }

			rocManager = new RocWidgetManager(roc.manifold, plot, true);
			rocManager.Initialize();
    }
  }

  internal class MinMaxLineSeries : LinearAxis {
    private double min;
    private double max;

    public void SetMinMax(double min, double max) {
      this.min = min;
      this.max = max;
    }

    protected override IList<double> CreateTickValues(double from, double to, double step, int maxTicks) {
      var list = new List<double>();
      list.Add(min);
      list.Add(max);
      return list;
    }
  }
}
