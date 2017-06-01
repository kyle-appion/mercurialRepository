namespace ION.Droid {

  using System;

  using Android.Content;
  using Android.Graphics;
  using Android.Graphics.Drawables;
  using Android.Views;

	using OxyPlot;
	using OxyPlot.Axes;
	using OxyPlot.Series;
	using OxyPlot.Xamarin.Android;

  using ION.Core.App;

  public class DetailedGraphGenerator {

    private Context context;
    private IION ion;

    private PlotView plot;
    private PlotModel model;
    private LinearAxis xAxis;
    private LinearAxis yAxis;

    public DetailedGraphGenerator(Context context, IION ion) {
      plot = new PlotView(context);
      model = new PlotModel();

			xAxis = new LinearAxis() {
				Position = AxisPosition.Bottom,
				IsAxisVisible = false,
				IsZoomEnabled = false,
				IsPanEnabled = false,
				MinimumPadding = 3,
				MaximumPadding = 3,
			};

			yAxis = new LinearAxis() {
				Position = AxisPosition.Left,
				IsAxisVisible = false,
				IsZoomEnabled = false,
				IsPanEnabled = false,
				MinimumPadding = 3,
				MaximumPadding = 3,
			};

			model.Axes.Add(xAxis);
			model.Axes.Add(yAxis);

			model.Background = OxyColors.Transparent;
			model.DefaultFontSize = 0;

			plot.Model = model;
    }

    public void Render(Canvas canvas) {
      
    }
  }
}
