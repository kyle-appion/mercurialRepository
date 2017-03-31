namespace ION.IOS.ViewController.Workbench {

  using System;
  using System.Threading.Tasks;

  using Foundation;
  using UIKit;

  using ION.Core.Content;
  using ION.Core.Sensors;
  using ION.Core.Sensors.Properties;

  using ION.IOS.Util;

  public class RateOfChangeRecord : SensorPropertyRecord {
    public override WorkbenchTableSource.ViewType viewType {
      get {
        return WorkbenchTableSource.ViewType.RateOfChange;
      }
    }

    public RateOfChangeSensorProperty roc { get; set; }

    public RateOfChangeRecord(Manifold manifold, ISensorProperty sensorProperty) : base(manifold, sensorProperty) {
    }
  }

	public partial class RateOfChangeSensorPropertyCell : UITableViewCell {

    private RateOfChangeRecord record {
      get {
        return __record;
      }
      set {
        if (__record != null) {
          __record.sensorProperty.onSensorPropertyChanged -= OnSensorPropertyChanged;
        }

        __record = value;

        if (__record != null) {
          __record.sensorProperty.onSensorPropertyChanged += OnSensorPropertyChanged;
          OnSensorPropertyChanged(__record.sensorProperty);
        }
      }
    } RateOfChangeRecord __record;

    private bool isUpdating { get; set; }

		public RateOfChangeSensorPropertyCell (IntPtr handle) : base (handle) {
		}

    public void UpdateTo(RateOfChangeRecord record) {
    	this.Layer.BorderWidth = 1f;
      this.record = record;
      labelTitle.Text = Strings.Workbench.Viewer.ROC;
    }

    private void OnSensorPropertyChanged(ISensorProperty sensorProperty) {
      if (!isUpdating) {
        isUpdating = true;
        DoUpdateCell();
      }
    }

    private async void DoUpdateCell() {
      var sp = record.sensorProperty as RateOfChangeSensorProperty;
			var roc = sp.GetAverageRateOfChange(TimeSpan.FromSeconds(1), TimeSpan.FromMinutes(1));
			var abs = Math.Abs(roc.amount);
      var range = (sp.sensor.maxMeasurement - sp.sensor.minMeasurement) / 10;

			if (abs > range.magnitude) {
        labelMeasurement.Text = ">" + SensorUtils.ToFormattedString(sp.sensor.type, range) + Strings.Measure.PER_MINUTE;
      } else {
				labelMeasurement.Text = SensorUtils.ToFormattedString(sp.sensor.type, roc.unit.OfScalar(abs)) + Strings.Measure.PER_MINUTE;
      }

			if (roc.amount == 0) {
        buttonIcon.Hidden = true;
        labelMeasurement.Text = Strings.Workbench.Viewer.ROC_STABLE;
        isUpdating = false;
      } else {
        buttonIcon.Hidden = false;
        if (roc < 0) {
          buttonIcon.SetImage(UIImage.FromBundle("ic_arrow_trend_down"), UIControlState.Normal);
        } else {
          buttonIcon.SetImage(UIImage.FromBundle("ic_arrow_trend_up"), UIControlState.Normal);
        }

        await Task.Delay(100);
        DoUpdateCell();
      }
    }
	}
}
