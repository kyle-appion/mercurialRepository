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

    public FluidRecord(Manifold manifold, ISensorProperty sensorProperty) : base(manifold, sensorProperty) {
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

      UpdateToFluid(sensorProperty.manifold.ptChart.fluid);

      switch (sensorProperty.manifold.ptChart.state) {
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

      var meas = sensorProperty.sensor.measurement;
      var chart = sensorProperty.manifold.ptChart;
      switch (sensorProperty.sensor.type) {
        case ESensorType.Pressure:
          var temp = chart.GetTemperature(meas);
          temp = temp.ConvertTo(ion.defaultUnits.temperature);
          labelMeasurement.Text = SensorUtils.ToFormattedString(ESensorType.Temperature, temp, true);
          break;
        case ESensorType.Temperature:
          var press = chart.GetPressure(meas);
          press = press.ConvertTo(ion.defaultUnits.pressure); 
          labelMeasurement.Text = SensorUtils.ToFormattedString(ESensorType.Pressure, press, true);
          break;
      }
    }

    private void HandleSuperheatSubcoolSensorPropertyChanged(SuperheatSubcoolSensorProperty sensorProperty) {
      UpdateToFluid(sensorProperty.manifold.ptChart.fluid);

      switch (sensorProperty.manifold.ptChart.state) {
        case Fluid.EState.Bubble:
          labelTitle.Text = Strings.Fluid.SUPERHEAT_ABRV;
          break;
        case Fluid.EState.Dew:
          labelTitle.Text = Strings.Fluid.SUBCOOL_ABRV;
          break;
        default:
          labelTitle.Text = Strings.UNKNOWN;
          break;
      }

      if (sensorProperty.pressureSensor == null || sensorProperty.temperatureSensor == null) {
        labelMeasurement.Text = Strings.Workbench.Viewer.SHSC_SETUP;        
      } else {
        var meas = sensorProperty.modifiedMeasurement;
        labelMeasurement.Text = SensorUtils.ToFormattedString(ESensorType.Temperature, meas, true);
      }
    }

    private void UpdateToFluid(Fluid fluid) {
      labelFluid.Text = fluid.name;
      labelFluid.BackgroundColor = new UIColor(Colors.FromInt((uint)fluid.color));
    }
	}
}
