namespace ION.IOS.ViewController.Workbench {

  using System;

  using Foundation;
  using UIKit;

  using ION.Core.App;
  using ION.Core.Content;
  using ION.Core.Content.Properties;
  using ION.Core.Fluids;
  using ION.Core.Sensors;
  using ION.Core.Sensors.Properties;

  using ION.IOS.Util;

  public class FluidRecord : SensorPropertyRecord {
    public override WorkbenchTableSource.ViewType viewType { 
      get { 
        return WorkbenchTableSource.ViewType.Fluid;
      }
    }

    public PTChartSensorProperty pt { get; set; }
    public SuperheatSubcoolSensorProperty shsc { get; set; }

		//public FluidRecord(Manifold manifold, ISensorProperty sensorProperty) : base(manifold, sensorProperty) {
		public FluidRecord(Sensor sensor, ISensorProperty sensorProperty) : base(sensor, sensorProperty) {
      pt = sensorProperty as PTChartSensorProperty;
      shsc = sensorProperty as SuperheatSubcoolSensorProperty;
    }
  }

	public partial class FluidSubviewCell : UITableViewCell {

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
    } FluidRecord __record;

		public FluidSubviewCell (IntPtr handle) : base (handle) {
		}

    public void UpdateTo(FluidRecord record) {
      this.record = record;
    }

    private void OnSensorPropertyChanged(ISensorProperty sensorProperty) {
      if (sensorProperty is SuperheatSubcoolSensorProperty) {
        HandleSuperheatSubcoolSensorPropertyChanged(sensorProperty as SuperheatSubcoolSensorProperty);
      } else {
        HandlePTChartSensorPropertyChanged(sensorProperty as PTChartSensorProperty);
      }
    }

    private void HandlePTChartSensorPropertyChanged(PTChartSensorProperty sensorProperty) {
    	
      var ion = AppState.context;

			//UpdateToFluid(sensorProperty.manifold.ptChart.fluid);
			UpdateToFluid(ion.fluidManager.lastUsedFluid);

      //var fluid = sensorProperty.manifold.ptChart.fluid;
      var fluid = ion.fluidManager.lastUsedFluid;

      if (fluid.mixture) {
				//switch (sensorProperty.manifold.ptChart.state)	{

		  switch (sensorProperty.sensor.fluidState) {
          case Fluid.EState.Bubble:
            labelTitle.Text = Strings.Fluid.PT_CHART_BUB;
            break;
          case Fluid.EState.Dew:
            labelTitle.Text = Strings.Fluid.PT_CHART_DEW;
            break;
          default:
            labelTitle.Text = Strings.UNKNOWN;
            break;
        }
      } else {
        labelTitle.Text = Strings.Fluid.PT;
      }


      switch (sensorProperty.sensor.type) {
        case ESensorType.Pressure:
					labelMeasurement.Text = SensorUtils.ToFormattedString(sensorProperty.modifiedMeasurement);
          break;
        case ESensorType.Temperature:
					labelMeasurement.Text = SensorUtils.ToFormattedString(sensorProperty.modifiedMeasurement);
          break;
      }
    }

    private void HandleSuperheatSubcoolSensorPropertyChanged(SuperheatSubcoolSensorProperty sensorProperty) {
			UpdateToFluid(AppState.context.fluidManager.lastUsedFluid);

      if (AppState.context.fluidManager.lastUsedFluid.mixture) {
				//switch (sensorProperty.manifold.ptChart.state)	{
		    switch (sensorProperty.sensor.fluidState) {
          case Fluid.EState.Bubble:
            labelTitle.Text = Strings.Fluid.SUBCOOL_ABRV;
            break;
          case Fluid.EState.Dew:
            labelTitle.Text = Strings.Fluid.SUPERHEAT_ABRV;
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
            labelTitle.Text = Strings.Fluid.SH;
          } else if (meas < 0) {
            labelTitle.Text = Strings.Fluid.SC;
          } else {
            labelTitle.Text = Strings.Fluid.SATURATED;
          }
        }
      }

      if (sensorProperty.pressureSensor == null || sensorProperty.temperatureSensor == null) {
        labelMeasurement.Text = Strings.Workbench.Viewer.SHSC_SETUP;        
      } else {
				var meas = sensorProperty.temperatureDelta;
        if (AppState.context.fluidManager.lastUsedFluid.mixture) {
          labelMeasurement.Text = SensorUtils.ToFormattedString(ESensorType.Temperature, meas, true);
        } else {
          if (meas < 0) {
						meas = meas * -1;
          }
          labelMeasurement.Text = SensorUtils.ToFormattedString(ESensorType.Temperature, meas, true);
        }
      }
    }

    private void UpdateToFluid(Fluid fluid) {
    	labelFluid.Layer.BorderWidth = 1f;
    	this.Layer.BorderWidth = 1f;
      labelFluid.Text = fluid.name;
      labelFluid.BackgroundColor = new UIColor(Colors.FromInt((uint)fluid.color));
    }
	}
}
