namespace ION.IOS.ViewController.Workbench {

	using System;
	using System.Threading.Tasks;

	using Foundation;
	using UIKit;

	using ION.Core.Content;
	using ION.Core.Sensors;
	using ION.Core.Sensors.Properties;

	using ION.IOS.Util;
	using OxyPlot.Xamarin.iOS;
	using CoreGraphics;
	using OxyPlot;
	using OxyPlot.Axes;
	using OxyPlot.Series;

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
		public PlotView plotView;
		public CategoryAxis BAX;
		public LinearAxis LAX;
		
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
      buttonIcon.Layer.BorderWidth = 1f;
      
      ///SETUP THE TRENDING GRAPH
			plotView = new PlotView(new CGRect(0,35, this.Bounds.Width, 55)){
				Model = CreatePlotModel(),
        BackgroundColor = UIColor.Clear,
			};      
      plotView.Layer.BorderWidth = 1f;
      viewBackground.AddSubview(plotView);
    }

    private void OnSensorPropertyChanged(ISensorProperty sensorProperty) {
      if (!isUpdating) {
        isUpdating = true;
        DoUpdateCell();
      }
    }

    private async void DoUpdateCell() {
      var sp = record.sensorProperty as RateOfChangeSensorProperty;
			var roc = sp.GetPrimaryAverageRateOfChange(TimeSpan.FromSeconds(1), TimeSpan.FromMinutes(1));
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
    
    public PlotModel CreatePlotModel(){
    	Console.WriteLine("Creating roc plotmodel");
				var plotModel = new PlotModel();

			plotModel.Background = OxyColors.Transparent;
			plotModel.PlotAreaBackground = OxyColors.White;
			plotModel.DefaultFontSize = 0;
			plotModel.PlotAreaBorderThickness = new OxyThickness(0,0,0,0);
			plotModel.PlotMargins = new OxyThickness(-5,-5,-5,-5);

			/// The bottom axis of the graph will be index based. Each measurement is one "tick"
      /// Corresponsding date indexes will provide the plot points

      BAX = new CategoryAxis {
        Position = AxisPosition.Bottom,
        Maximum = 50,
        AbsoluteMaximum = 50,
        Minimum = 0,
        AbsoluteMinimum = -1,
        IntervalLength = 1,
        IsAxisVisible = false,
        IsZoomEnabled = false,
        IsPanEnabled = false,
        MinimumPadding = 0,
        MaximumPadding = 0,
        AxislineThickness = 0,
        Angle = 90,
        Title = "Time",
      };

			for(int i = 1; i < 51; i++){
				BAX.ActualLabels.Add(i.ToString());				
			}
			
			///YOU WOULD OBVIOUSLY WANT TICK MARKS TO BE CENTERED AS DEFAULT, SO WHY IS THE DEFAULT FALSE OXYPLOT!?!?!
			BAX.IsTickCentered = true;
			
			LAX = new LinearAxis {
				Position = AxisPosition.Left,
        Maximum = 110,
				AbsoluteMaximum = 110,
        Minimum = -5,
        AbsoluteMinimum = -5,
				Key = "left",
				IsAxisVisible = false,
				IsZoomEnabled = false,
				IsPanEnabled = false,
				MinimumPadding = 0,
				MaximumPadding = 0,
				AxislineThickness = 0,
				Title = "Value",
				MajorStep = 1,
				
			};			

    	var markSize = 0.0;
      var series = new LineSeries {
        MarkerType = MarkerType.Circle,
        MarkerSize = markSize,
        MarkerStroke = OxyColors.Blue,
        MarkerFill = OxyColors.Blue,
        LineStyle = LineStyle.Solid,
        Color = OxyColors.Blue,
      };

     for(int v = 0, index = 1; v < 51; v+=2, index++){

        series.Points.Add(new DataPoint(index,v));
      }
      
      plotModel.Series.Add (series);
      
			plotModel.Axes.Add(BAX);      
			plotModel.Axes.Add(LAX);			
			
			return plotModel;
		}
	}
}
