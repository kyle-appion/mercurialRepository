namespace ION.IOS.ViewController.Alarms {

  using System;

  using Foundation;
  using UIKit;

  using ION.Core.App;
  using ION.Core.Devices;
  using ION.Core.Sensors;
  using ION.Core.Sensors.Alarms;
  using ION.Core.Util;

  using ION.IOS.UI;
  using ION.IOS.Util;
  using ION.IOS.ViewController.Dialog;

	public partial class SensorAlarmViewController : BaseIONViewController {

    /// <summary>
    /// Gets or sets the sensor.
    /// </summary>
    /// <value>The sensor.</value>
    public Sensor sensor { get; set; }

    private IION ion { get; set; }

    private LowSensorAlarm lowAlarm {
      get {
        return __lowAlarm;
      }
      set {
        __lowAlarm = value;

        if (__lowAlarm != null) {
          editLowAlarmMeasurement.Text = __lowAlarm.bounds.amount + "";
          buttonLowAlarmUnit.SetTitle(__lowAlarm.bounds.unit.ToString(), UIControlState.Normal);
        } else {
          editLowAlarmMeasurement.Text = "";
          buttonLowAlarmUnit.SetTitle("", UIControlState.Normal);
        }
      }
    } LowSensorAlarm __lowAlarm;

    private HighSensorAlarm highAlarm {
      get {
        return __highAlarm;
      }
      set {
        __highAlarm = value;

        if (__highAlarm != null) {
          editHighAlarmMeasurement.Text = __highAlarm.bounds.amount + "";
          buttonHighAlarmUnit.SetTitle(__highAlarm.bounds.unit.ToString(), UIControlState.Normal);
        } else {
          editHighAlarmMeasurement.Text = "";
          buttonHighAlarmUnit.SetTitle("", UIControlState.Normal);
        }
      }
    } HighSensorAlarm __highAlarm;

		public SensorAlarmViewController (IntPtr handle) : base (handle) {
		}

    // Overridden from BaseIONViewController
    public override void ViewDidLoad() {
      base.ViewDidLoad();

      if (sensor == null) {
        throw new Exception("Cannot start " + typeof(SensorAlarmViewController).Name + ": sensor cannot be null.");
      }

      View.AddGestureRecognizer(new UITapGestureRecognizer(() => {
        editLowAlarmMeasurement.ResignFirstResponder();
        editHighAlarmMeasurement.ResignFirstResponder();
      }));

      editLowAlarmMeasurement.ShouldReturn += (textField) => {
        textField.ResignFirstResponder();
        return true;
      };

      editHighAlarmMeasurement.ShouldReturn += (textField) => {
        textField.ResignFirstResponder();
        return true;
      };

      ion = AppState.context;

      lowAlarm = ion.alarmManager.GetAlarmOfTypeFromHost<LowSensorAlarm>(sensor);
      highAlarm = ion.alarmManager.GetAlarmOfTypeFromHost<HighSensorAlarm>(sensor);

      if (lowAlarm == null) {
        lowAlarm = new LowSensorAlarm("", "", sensor);
        ion.alarmManager.RegisterAlarmToHost(sensor, lowAlarm);
      }

      if (highAlarm == null) {
        highAlarm = new HighSensorAlarm("", "", sensor);
        ion.alarmManager.RegisterAlarmToHost(sensor, highAlarm);
      }

      lowAlarm.name = Strings.Alarms.LOW_ALARM;
      highAlarm.name = Strings.Alarms.HIGH_ALARM;
      UpdateAlarmDescriptions();

      switchLowAlarmEnabler.On = lowAlarm.enabled;
      switchHighAlarmEnabler.On = highAlarm.enabled;

      /*
      buttonLowAlarmUnit.TouchUpInside += (source, args) => {
        var dialog = CommonDialogs.CreateUnitPicker(Strings.Measure.PICK_UNIT, lowAlarm.sensor.supportedUnits, (obj, unit) => {
          lowAlarm.sensor.unit = unit;
          buttonLowAlarmUnit.SetTitle(unit.ToString(), UIControlState.Normal);
        });
        PresentViewController(dialog, true, null);
      };
      */
    }

    // Overridden from BaseIONViewController
    public override void ViewWillDisappear(bool animated) {
      base.ViewWillDisappear(animated);
      Log.D(this, "Saving alarms");

      // Save Low Alarm
      UpdateLowAlarmFromInput();
      lowAlarm.enabled = switchLowAlarmEnabler.On;

      // Save High Alarm
      UpdateHighAlarmFromInput();
      highAlarm.enabled = switchHighAlarmEnabler.On;
    }

    private void UpdateLowAlarmFromInput() {
      var input = editLowAlarmMeasurement.Text;
      var unit = lowAlarm.bounds.unit;
      try {
        lowAlarm.bounds = unit.OfScalar(double.Parse(input));
        UpdateAlarmDescriptions();
      } catch (Exception e) {
        Log.E(this, "Failed to update low alarm: invalid double", e);
      }
    }

    private void UpdateHighAlarmFromInput() {
      var input = editHighAlarmMeasurement.Text;
      var unit = highAlarm.bounds.unit;
      try {
        highAlarm.bounds = unit.OfScalar(double.Parse(input));
        UpdateAlarmDescriptions();
      } catch (Exception e) {
        Log.E(this, "Failed to update high alarm: invalid double", e);
      }
    }

    private void UpdateAlarmDescriptions() {
      if (sensor is GaugeDeviceSensor) {
        var gsd = sensor as GaugeDeviceSensor;
        lowAlarm.description = String.Format(Strings.Alarms.LOW_ALARM_FIRED, gsd.device.name, lowAlarm.bounds.ToString());
        highAlarm.description = String.Format(Strings.Alarms.HIGH_ALARM_FIRED, gsd.device.name, highAlarm.bounds.ToString());
      } else {
        // TODO ahodder@appioninc.com: This needs to be thought through
      }
    }
	}
}
