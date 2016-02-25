namespace ION.Droid.Fragments {

  using System;

  using Android.App;
  using Android.Content;
  using Android.OS;
  using Android.Views;
  using Android.Widget;

  using ION.Core.Content;
  using ION.Core.Devices;
  using ION.Core.Sensors;
  using ION.Core.Sensors.Properties;
  using ION.Core.Util;

  using ION.Droid.Activity;
  using ION.Droid.Dialog;
  using ION.Droid.Sensors;
  using ION.Droid.Widgets;
  using ION.Droid.Widgets.Analyzer;

  public class AnalyzerFragment : IONFragment {

    /// <summary>
    /// The mask that is used to check request values.
    /// </summary>
    private const int MASK_REQUEST = unchecked((int)0xff000000);
    /// <summary>
    /// The mask that is used to chack payload values.
    /// </summary>
    private const int MASK_REQUEST_PAYLOAD = unchecked((int)0x000000ff);
    /// <summary>
    /// The constant value indicating that the fragment requested a sensor for a sensor mount. The first byte of this
    /// request will be the index of the sensor.
    /// </summary>
    private const int REQUEST_SENSOR_MOUNT_SENSOR = unchecked((int)0x01000000);
    /// <summary>
    /// The constant that indicates a low side manifold.
    /// </summary>
    private const int LOW_SIDE_MANIFOLD = -1;
    /// <summary>
    /// The constant that indicates a high side manifold.
    /// </summary>
    private const int HIGH_SIDE_MANIFOLD = -2;

    /// <summary>
    /// The view that will display the application's analyzer.
    /// </summary>
    private AnalyzerView analyzerView;
    /// <summary>
    /// The analyzer that is being displayed.
    /// </summary>
    private Analyzer analyzer;

    /// <Docs>The LayoutInflater object that can be used to inflate
    ///  any views in the fragment,</Docs>
    /// <param name="savedInstanceState">If non-null, this fragment is being re-constructed
    ///  from a previous saved state as given here.</param>
    /// <returns>To be added.</returns>
    /// <summary>
    /// Raises the create view event.
    /// </summary>
    /// <param name="inflater">Inflater.</param>
    /// <param name="container">Container.</param>
    public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
      analyzerView = new AnalyzerView(inflater.Context);

      analyzerView.onSensorMountClicked += OnSensorMountClicked;
      analyzerView.onSensorMountLongClicked += OnSensorMountLongClicked;
      analyzerView.onManifoldClicked += OnManifoldClicked;

      return analyzerView;
    }

    /// <Docs>If the fragment is being re-created from
    ///  a previous saved state, this is the state.</Docs>
    /// <summary>
    /// Raises the activity created event.
    /// </summary>
    /// <param name="savedInstanceState">Saved instance state.</param>
    public override void OnActivityCreated(Bundle savedInstanceState) {
      base.OnActivityCreated(savedInstanceState);

      analyzer = new Analyzer();
      analyzerView.analyzer = analyzer;
    }

    /// <Docs>The integer request code originally supplied to
    ///  startActivityForResult(), allowing you to identify who this
    ///  result came from.</Docs>
    /// <param name="data">An Intent, which can return result data to the caller
    ///  (various data can be attached to Intent "extras").</param>
    /// <summary>
    /// Raises the activity result event.
    /// </summary>
    /// <param name="requestCode">Request code.</param>
    /// <param name="resultCode">Result code.</param>
    public override void OnActivityResult(int requestCode, Result resultCode, Intent data) {
      if (Result.Ok != resultCode) {
        return;
      }

      var request = requestCode & MASK_REQUEST;

      switch (request) {
        case REQUEST_SENSOR_MOUNT_SENSOR:
          var index = requestCode & MASK_REQUEST_PAYLOAD;
          var sp = (SensorParcelable)data.GetParcelableExtra(DeviceManagerActivity.EXTRA_SENSOR);
          analyzer.PutSensor(index, sp.Get(ion));
          Log.D(this, "Received a sensor");
          break;
        default:
          Log.D(this, "Unknown request: " + request);
          break;
      }
    }

    /// <summary>
    /// Creates an encoded sensor mount request value.
    /// </summary>
    /// <returns>The sensor mount request.</returns>
    /// <param name="sensorMountIndex">Sensor mount index.</param>
    private int EncodeSensorMountRequest(int sensorMountIndex) {
      return REQUEST_SENSOR_MOUNT_SENSOR | (MASK_REQUEST_PAYLOAD & sensorMountIndex);
    }

    /// <summary>
    /// Shows a context dialog for a manifold. This will present all the options that are available for
    /// the manifold.
    /// </summary>
    private void ShowManifoldContextDialog(Manifold manifold) {
      var ldb = new ListDialogBuilder(Activity);
      ldb.SetTitle(Resource.String.manifold_select_action);

      var dgs = manifold.primarySensor as GaugeDeviceSensor;

      if (dgs != null && !dgs.device.isConnected) {
        ldb.AddItem(Resource.String.connect, () => {
          dgs.device.connection.Connect();
        });
      }

      ldb.AddItem(Resource.String.rename, () => {
        Toast.MakeText(Activity, "Rename up in this bitch", ToastLength.Short).Show();
      });


      ldb.AddItem(Resource.String.workbench_add_viewer_sub, () => {
        ShowAddSubviewDialog(manifold);
      });

      ldb.AddItem(Resource.String.alarm, () => {
        Toast.MakeText(Activity, "Show some alarms up in dis bitch", ToastLength.Short).Show();
      });

      if (dgs != null && dgs.device.isConnected) {
        ldb.AddItem(Resource.String.disconnect, () => {
          dgs.device.connection.Disconnect();
        });
      }

      ldb.AddItem(Resource.String.remove, () => {
        analyzer.RemoveManifold(manifold);
      });

      ldb.Show();
    }

    /// <summary>
    /// Shows a dialog that will allow the user to add subviews to the given manifold.
    /// </summary>
    /// <param name="manifold">Manifold.</param>
    private void ShowAddSubviewDialog(Manifold manifold) {
      Func<int, int, string> format = delegate (int full, int abrv) {
        return GetString(full) + " (" + GetString(abrv) + ")";
      };

      var ldb = new ListDialogBuilder(Activity);
      ldb.SetTitle(Resource.String.manifold_select_subview);

      if (!manifold.HasSensorPropertyOfType(typeof(AlternateUnitSensorProperty))) {
        ldb.AddItem(format(Resource.String.workbench_alt, Resource.String.workbench_alt_abrv), () => {
          manifold.AddSensorProperty(new AlternateUnitSensorProperty(manifold.primarySensor));
        });
      }

      if (!manifold.HasSensorPropertyOfType(typeof(RateOfChangeSensorProperty))) {
        ldb.AddItem(format(Resource.String.workbench_roc, Resource.String.workbench_roc_abrv), () => {
          manifold.AddSensorProperty(new RateOfChangeSensorProperty(manifold.primarySensor));
        });
      }

      if (!manifold.HasSensorPropertyOfType(typeof(MinSensorProperty))) {
        ldb.AddItem(format(Resource.String.workbench_min, Resource.String.workbench_min_abrv), () => {
          manifold.AddSensorProperty(new MinSensorProperty(manifold.primarySensor));
        });
      }

      if (!manifold.HasSensorPropertyOfType(typeof(MaxSensorProperty))) {
        ldb.AddItem(format(Resource.String.workbench_max, Resource.String.workbench_max_abrv), () => {
          manifold.AddSensorProperty(new MaxSensorProperty(manifold.primarySensor));
        });
      }

      if (!manifold.HasSensorPropertyOfType(typeof(HoldSensorProperty))) {
        ldb.AddItem(format(Resource.String.workbench_hold, Resource.String.workbench_hold_abrv), () => {
          manifold.AddSensorProperty(new MinSensorProperty(manifold.primarySensor));
        });
      }

      if (!manifold.HasSensorPropertyOfType(typeof(TimerSensorProperty))) {
        ldb.AddItem(format(Resource.String.workbench_timer, Resource.String.workbench_timer_abrv), () => {
          manifold.AddSensorProperty(new MinSensorProperty(manifold.primarySensor));
        });
      }

      if (ESensorType.Pressure == manifold.primarySensor.type || ESensorType.Temperature == manifold.primarySensor.type) {
        if (!manifold.HasSensorPropertyOfType(typeof(PTChartSensorProperty))) {
          ldb.AddItem(format(Resource.String.workbench_ptchart, Resource.String.fluid_pt_abrv), () => {
            manifold.AddSensorProperty(new MinSensorProperty(manifold.primarySensor));
          });
        }

        if (!manifold.HasSensorPropertyOfType(typeof(SuperheatSubcoolSensorProperty))) {
          ldb.AddItem(format(Resource.String.workbench_shsc, Resource.String.workbench_shsc_abrv), () => {
            manifold.AddSensorProperty(new MinSensorProperty(manifold.primarySensor));
          });
        }
      }

      ldb.Show();
    }

    /// <summary>
    /// Called when a sensor mount is clicked in the analyzer.
    /// </summary>
    /// <param name="view">View.</param>
    /// <param name="analyzer">Analyzer.</param>
    /// <param name="index">Index.</param>
    private void OnSensorMountClicked(AnalyzerView view, Analyzer analyzer, int index) {
      if (analyzer.HasSensorAt(index)) {
        Log.D(this, "Analyzer view callback sensor mount clicked at index: " + index);
      } else {
        var i = new Intent(Activity, typeof(DeviceManagerActivity));
        i.SetAction(Intent.ActionPick);
        StartActivityForResult(i, EncodeSensorMountRequest(index));
      }
    }

    /// <summary>
    /// Called when a sensor mount is clicked in the analyzer.
    /// </summary>
    /// <param name="view">View.</param>
    /// <param name="analyzer">Analyzer.</param>
    /// <param name="index">Index.</param>
    private void OnSensorMountLongClicked(AnalyzerView view, Analyzer analyzer, int index) {
      if (analyzer.HasSensorAt(index)) {
        analyzerView.StartDraggingSensorMount(index);
      } else {
      }
      Log.D(this, "Analyzer view callback sensor mount long clicked at index: " + index);
    }

    /// <summary>
    /// Called when a manifold is clicked in the analyzer view.
    /// </summary>
    /// <param name="view">View.</param>
    /// <param name="analyzer">Analyzer.</param>
    /// <param name="side">Side.</param>
    private void OnManifoldClicked(AnalyzerView view, Analyzer analyzer, Analyzer.ESide side) {
      var manifold = analyzer.GetManifoldFromSide(side);

      if (manifold != null) {
        ShowManifoldContextDialog(manifold);
      }
    }
  }
}

