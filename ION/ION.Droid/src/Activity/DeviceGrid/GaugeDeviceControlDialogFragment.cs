namespace ION.Droid.Activity.Grid {

  using System;

  using Android.App;
  using Android.Content;
  using Android.Graphics;
  using Android.Graphics.Drawables;
  using Android.OS;
  using Android.Views;
  using Android.Widget;

  using ION.Core.App;
  using ION.Core.Connections;
  using ION.Core.Content;
  using ION.Core.Devices;
  using ION.Core.Sensors.Properties;

  using ION.Droid.Devices;
  using ION.Droid.Dialog;
  using ION.Droid.Views;

  public class GaugeDeviceControlDialogFragment : DialogFragment {

    private IION ion;

    private GaugeDevice device;

    private View contentView;

    private ImageView settingsView;
    private ImageView connectionStateView;
    private ImageView iconView;
    private ImageView batteryView;

    private ProgressBar progressView;

    private TextView nameView;

    private View sensor1ContainerView;
    private View sensor1LocationView;

    private View divider;

    private View sensor2ContainerView;
    private View sensor2LocationView;

    private int lastBatteryLevel = -1;
    private EConnectionState lastConnectionState;

    private readonly Handler handler = new Handler();

    public override void OnCreate(Bundle savedInstanceState) {
      base.OnCreate(savedInstanceState);
      SetStyle(DialogFragmentStyle.NoFrame, Resource.Style.ComplicatedDialogTheme);

      ion = AppState.context;
    }

    public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
      contentView = inflater.Inflate(Resource.Layout.fragment_gauge_dialog_device_control, container, false);

      settingsView = contentView.FindViewById<ImageView>(Resource.Id.settings);
      connectionStateView = contentView.FindViewById<ImageView>(Resource.Id.connection);
      iconView = contentView.FindViewById<ImageView>(Resource.Id.icon);
      batteryView = contentView.FindViewById<ImageView>(Resource.Id.battery);

      progressView = contentView.FindViewById<ProgressBar>(Resource.Id.progress);

      nameView = contentView.FindViewById<TextView>(Resource.Id.name);

      sensor1ContainerView = contentView.FindViewById(Resource.Id.content);
      sensor1LocationView = contentView.FindViewById(Resource.Id.control);

      divider = contentView.FindViewById(Resource.Id.divider);

      sensor2ContainerView = contentView.FindViewById(Resource.Id.content2);
      sensor2LocationView = contentView.FindViewById(Resource.Id.control2);

      ((ViewGroup)connectionStateView.Parent).Click += (sender, args) => {
        if (device.connection.connectionState == EConnectionState.Disconnected) {
          device.connection.Connect();
        } else {
          device.connection.Disconnect();
        }
      };

      return contentView;
    }

    public override void OnActivityCreated(Bundle savedInstanceState) {
      base.OnActivityCreated(savedInstanceState);
    }

    public override void OnResume() {
      base.OnResume();

      Invalidate();
      InvalidateLocation();
      device.onDeviceEvent += OnDeviceEvent;
      ion.currentWorkbench.onWorkbenchEvent += OnWorkbenchEvent;
      ion.currentAnalyzer.onAnalyzerEvent += OnAnalyzerEvent;
    }

    public override void OnPause() {
      base.OnPause();
      device.onDeviceEvent -= OnDeviceEvent;
      ion.currentWorkbench.onWorkbenchEvent -= OnWorkbenchEvent;
      ion.currentAnalyzer.onAnalyzerEvent -= OnAnalyzerEvent;
    }

    public GaugeDeviceControlDialogFragment(GaugeDevice device) {
      this.device = device;
    }

    private void Invalidate() {
      nameView.Text = device.serialNumber.deviceModel.GetTypeString() + ": " + device.serialNumber;
      iconView.SetImageResource(device.GetDeviceIcon());

      if (device.connection.connectionState == EConnectionState.Resolving || device.connection.connectionState == EConnectionState.Connecting) {
        progressView.Visibility = ViewStates.Visible;
        connectionStateView.Visibility = ViewStates.Gone;
      } else {
        progressView.Visibility = ViewStates.Gone;
        connectionStateView.Visibility = ViewStates.Visible;

        if (lastConnectionState != device.connection.connectionState) {
          switch (device.connection.connectionState) {
            case EConnectionState.Connected:
              connectionStateView.SetImageResource(Resource.Drawable.ic_bluetooth_connected);
              connectionStateView.SetBackgroundResource(Resource.Drawable.xml_rect_green_black_bordered_round);
              break;

            case EConnectionState.Broadcasting:
              connectionStateView.SetImageResource(Resource.Drawable.ic_bluetooth_broadcast);
              connectionStateView.SetBackgroundResource(Resource.Drawable.xml_rect_light_blue_black_bordered_round);
              break;

            default:
              if (device.isNearby) {
                connectionStateView.SetImageResource(Resource.Drawable.ic_bluetooth_disconnected);
                connectionStateView.SetBackgroundResource(Resource.Drawable.xml_rect_yellow_black_bordered_round);
              } else {
                connectionStateView.SetImageResource(Resource.Drawable.ic_bluetooth_disconnected);
                connectionStateView.SetBackgroundResource(Resource.Drawable.xml_rect_red_black_bordered_round);
              }
              break;
          }

          lastConnectionState = device.connection.connectionState;
        }
      }

      if (device.isConnected || device.isNearby) {
        batteryView.Visibility = ViewStates.Visible;
        if (lastBatteryLevel != device.battery) {
          batteryView.SetImageResource(device.GetBatteryIconVert());
          lastBatteryLevel = device.battery;
        }
      } else {
        batteryView.Visibility = ViewStates.Invisible;
      }

      InvalidateSensor(device.sensors[0], sensor1ContainerView);
      if (device.sensorCount >= 2) {
        InvalidateSensor(device.sensors[1], sensor2ContainerView);
      } else {
        divider.Visibility = ViewStates.Invisible;
        sensor2ContainerView.Visibility = ViewStates.Gone;
      }
    }

    private void InvalidateLocation() {
      InvalidateLocationForSensor(device.sensors[0], sensor1LocationView);
      if (device.sensorCount >= 2) {
        sensor2LocationView.Visibility = ViewStates.Visible;
        InvalidateLocationForSensor(device.sensors[1], sensor2LocationView);
      } else {
        sensor2LocationView.Visibility = ViewStates.Gone;
      }
    }

    private void InvalidateLocationForSensor(GaugeDeviceSensor sensor, View container) {
      var wb = container.FindViewById<ImageView>(Resource.Id.workbench);
      var anal = container.FindViewById<ImageView>(Resource.Id.analyzer);

      if (ion.currentWorkbench.ContainsSensor(sensor)) {
        wb.SetBackgroundResource(0);
        wb.SetOnClickListener(new ViewClickAction((v) => {
          var adb = new IONAlertDialog(Activity);
          adb.SetTitle(Resource.String.workbench_remove);
          adb.SetMessage(Resource.String.workbench_remove_sensor);
          adb.SetNegativeButton(Resource.String.cancel, (sender, args) => { });
          adb.SetPositiveButton(Resource.String.remove, (sender, e) => {
            ion.currentWorkbench.Remove(sensor);
          });
          adb.Show();
        }));
      } else {
        wb.SetBackgroundResource(Resource.Drawable.xml_rect_gold_black_bordered);
        wb.SetOnClickListener(new ViewClickAction((v) => {
          AddSensorToWorkbench(sensor);
        }));
      }

      if (ion.currentAnalyzer.HasSensor(sensor)) {
        anal.SetBackgroundResource(0);
        anal.SetOnClickListener(new ViewClickAction((c) => {
          var adb = new IONAlertDialog(Activity);
          adb.SetTitle(Resource.String.analyzer_remove_sensor);
          adb.SetMessage(Resource.String.analyzer_remove_sensor_remote);
					adb.SetNegativeButton(Resource.String.cancel, (sender, args) => { });
					adb.SetPositiveButton(Resource.String.remove, (sender, e) => {
            ion.currentAnalyzer.RemoveSensor(sensor);
					});
          adb.Show();
        }));
      } else {
        anal.SetBackgroundResource(Resource.Drawable.xml_rect_gold_black_bordered);
        anal.SetOnClickListener(new ViewClickAction((c) => {
          AddSensorToAnalyzer(sensor);
        }));
      }
    }

    private void InvalidateSensor(GaugeDeviceSensor sensor, View container) {
      var title = container.FindViewById<TextView>(Resource.Id.title);
      var measurement = container.FindViewById<TextView>(Resource.Id.measurement);
      var state = container.FindViewById<TextView>(Resource.Id.state);
      var unit = container.FindViewById<TextView>(Resource.Id.unit);

      title.Text = sensor.type.GetSensorTypeName();
      if (sensor.removed) {
        measurement.Text = "- - - -";
      } else {
        measurement.Text = sensor.ToFormattedString(false);
      }
      state.Visibility = ViewStates.Gone;
      unit.Text = sensor.measurement.unit.ToString();
    }

    private void OnDeviceEvent(DeviceEvent e) {
      handler.Post(Invalidate);
    }

    private void OnWorkbenchEvent(WorkbenchEvent e) {
      switch (e.type) {
        case WorkbenchEvent.EType.Added:
        case WorkbenchEvent.EType.Removed:
          handler.Post(InvalidateLocation);
          break;
      }
    }

    private void OnAnalyzerEvent(AnalyzerEvent e) {
      switch (e.type) {
        case AnalyzerEvent.EType.Added:
        case AnalyzerEvent.EType.Removed:
          handler.Post(InvalidateLocation);
          break;
      }
    }

    private void AddSensorToWorkbench(GaugeDeviceSensor sensor) {
      if (ion.currentWorkbench.ContainsDevice(this.device)) {
        Toast(GetString(Resource.String.workbench_error_already_contains_sensor));
      } else {
        var device = sensor.device;
        var deviceType = device.serialNumber.deviceModel;

        if (deviceType == EDeviceModel.PT500 || deviceType == EDeviceModel.PT800) {
          var manifold = new Manifold(device.sensors[0], device.sensors[1]);
          manifold.AddSensorProperty(new SecondarySensorProperty(manifold));
          manifold.AddSensorProperty(new SuperheatSubcoolSensorProperty(manifold));
          ion.currentWorkbench.Add(manifold);
          Toast(GetString(Resource.String.workbench_added_sensor));
        } else {
          var manifold = new Manifold(sensor);
          if (deviceType == EDeviceModel.AV760) {
            manifold.AddSensorProperty(new RateOfChangeSensorProperty(manifold, ion.preferences.device.trendInterval));
          }
          ion.currentWorkbench.AddSensor(sensor);
          Toast(GetString(Resource.String.workbench_added_sensor));
        }
      }
    }

    private void AddSensorToAnalyzer(GaugeDeviceSensor sensor) {
      var deviceType = sensor.device.serialNumber.deviceModel;
      if (deviceType == EDeviceModel.P500 || deviceType == EDeviceModel.PT500) {
        if (ion.currentAnalyzer.IsSideFull(Analyzer.ESide.Low)) {
          Toast(string.Format(GetString(Resource.String.analyzer_side_full_1sarg), GetString(Resource.String.analyzer_side_low)));
        } else if (ion.currentAnalyzer.HasSensor(sensor)) {
          Toast(GetString(Resource.String.analyzer_sensor_already_in_viewer));
        } else {
          ion.currentAnalyzer.AddSensorToSide(Analyzer.ESide.Low, sensor);
          Toast(GetString(Resource.String.analyzer_added_to_low));
        }
      } else if (deviceType == EDeviceModel.P800 || deviceType == EDeviceModel.PT800) {
        if (ion.currentAnalyzer.IsSideFull(Analyzer.ESide.High)) {
          Toast(string.Format(GetString(Resource.String.analyzer_side_full_1sarg), GetString(Resource.String.analyzer_side_high)));
        } else if (ion.currentAnalyzer.HasSensor(sensor)) {
          Toast(GetString(Resource.String.analyzer_sensor_already_in_viewer));
        } else {
          ion.currentAnalyzer.AddSensorToSide(Analyzer.ESide.High, sensor);
					Toast(GetString(Resource.String.analyzer_added_to_high));
				}
      } else {
        if (!ion.currentAnalyzer.IsSideFull(Analyzer.ESide.Low)) {
          ion.currentAnalyzer.AddSensorToSide(Analyzer.ESide.Low, sensor);
					Toast(GetString(Resource.String.analyzer_added_to_low));
				} else if (!ion.currentAnalyzer.IsSideFull(Analyzer.ESide.High)) {
          ion.currentAnalyzer.AddSensorToSide(Analyzer.ESide.High, sensor);
					Toast(GetString(Resource.String.analyzer_added_to_high));
				} else {
          Toast(GetString(Resource.String.analyzer_full));
        }
      }
    }

    private void Toast(string msg) {
      Android.Widget.Toast.MakeText(Activity, msg, ToastLength.Long).Show();
    }
  }
}
