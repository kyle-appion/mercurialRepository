using System;
using Foundation;
using ION.Core.Content;
using ION.Core.Devices;
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
          __record.sensor.onSensorEvent -= OnSensorChanged;
				}

				__record = value;

				if (__record != null) {
					__record.sensorProperty.onSensorPropertyChanged += OnSensorPropertyChanged;
					__record.sensor.onSensorEvent += OnSensorChanged;
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

    private void OnSensorChanged(SensorEvent sensorEvent){
			HandleSuperheatSubcoolSensorPropertyChanged(record.sensorProperty as TargetSuperheatSubcoolProperty);
		}

		private void OnSensorPropertyChanged(ISensorProperty sensorProperty) {
			HandleSuperheatSubcoolSensorPropertyChanged(sensorProperty as TargetSuperheatSubcoolProperty);
		}

		private void HandleSuperheatSubcoolSensorPropertyChanged(TargetSuperheatSubcoolProperty sensorProperty) {
      labelTarget.Text = record.sensor.targetSHSC.ToString();

      switch (sensorProperty.sensor.fluidState) {
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

			////set subview for not existing sensor linkage
			if (sensorProperty.sensor.linkedSensor == null) {
				labelOffset.Text = Strings.Workbench.Viewer.SHSC_SETUP;
			} else {
				var meas = sensorProperty.temperatureDelta - record.sensor.targetSHSC;

        labelOffset.Text = SensorUtils.ToFormattedString(ESensorType.Temperature, meas, true);
			}

		}
  }
}
