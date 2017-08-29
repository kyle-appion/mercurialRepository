using System;
using Foundation;
using ION.Core.Content;
using ION.Core.Fluids;
using ION.Core.Sensors;
using ION.Core.Sensors.Properties;
using ION.IOS.Util;
using UIKit;

namespace ION.IOS.ViewController.Workbench {

  public partial class TargetSHSCCell : UITableViewCell {
		private FluidRecord record {
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
		}
		FluidRecord __record;

    public TargetSHSCCell(IntPtr handle) : base(handle) {

		}

    public void UpdateTo(FluidRecord record){
      this.record = record;
    }

		private void OnSensorPropertyChanged(ISensorProperty sensorProperty) {
			HandleSuperheatSubcoolSensorPropertyChanged(sensorProperty as TargetSuperheatSubcoolProperty);
		}

		private void HandleSuperheatSubcoolSensorPropertyChanged(TargetSuperheatSubcoolProperty sensorProperty) {
			var ptchart = sensorProperty.manifold.ptChart;
      labelTarget.Text = record.manifold.targetSHSC.ToString();

			if (ptchart.fluid.mixture) {
				switch (sensorProperty.manifold.ptChart.state) {
					case Fluid.EState.Bubble:
					//labelTitle.Text = Strings.Fluid.SUBCOOL_ABRV;
					labelTitle.Text = "T-SC";
					break;
					case Fluid.EState.Dew:
					//labelTitle.Text = Strings.Fluid.SUPERHEAT_ABRV;
					labelTitle.Text = "T-SH";
					break;
					default:
					labelTitle.Text = Strings.UNKNOWN;
					break;
				}
			} else {
				if (sensorProperty.pressureSensor == null || sensorProperty.temperatureSensor == null) {
					labelTitle.Text = Strings.Fluid.SHSC;
				} else {
					var meas = sensorProperty.modifiedMeasurement;

					if (meas > 0) {
						//labelTitle.Text = Strings.Fluid.SH;
						labelTitle.Text = "T-SH";
					} else if (meas < 0) {
						//labelTitle.Text = Strings.Fluid.SC;
						labelTitle.Text = "T-SC";
					} 
				}
			}

      ////set subview for not existing sensor linkage
			if (sensorProperty.pressureSensor == null || sensorProperty.temperatureSensor == null) {
				labelOffset.Text = Strings.Workbench.Viewer.SHSC_SETUP;
			} else {
				var meas = sensorProperty.temperatureDelta - record.manifold.targetSHSC;

        labelOffset.Text = SensorUtils.ToFormattedString(ESensorType.Temperature, meas, true);
				//if (ptchart.fluid.mixture) {
				//	labelOffset.Text = SensorUtils.ToFormattedString(ESensorType.Temperature, meas, true);
				//} else {
				//	if (meas < 0) {
				//		meas = meas * -1;
				//	}
				//	labelOffset.Text = SensorUtils.ToFormattedString(ESensorType.Temperature, meas, true);
				//}
			}

		}
  }
}
