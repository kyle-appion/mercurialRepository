﻿namespace ION.Droid.Widgets {

	using System;

	using Android.Content;
	using Android.Graphics;
	using Android.Views;
	using Android.Widget;

	using ION.Core.App;
	using ION.Core.Fluids;
	using ION.Core.Math;
	using ION.Core.Measure;
	using ION.Core.Util;
	
	public class FluidSliderView : View {
		/// <summary>
		/// The delegate that is called when the fluid slider is scrolled.
		/// </summary>
		public delegate void OnScroll(FluidSliderView slider, Scalar pressure, Scalar temperature);

		/// <summary>
		/// The text size for the paints.
		/// </summary>
		private const float TEXT_SIZE = 0.15f;

		/// <summary>
		/// The event that is called when the slider is scrolled.
		/// </summary>
		/// <value>The fluid.</value>
		public event OnScroll onScroll;

		/// <summary>
		/// The pt chart that is driving this view.
		/// </summary>
		/// <value>The point chart.</value>
		public PTChart ptChart {
			get {
				return __ptchart;
			}
			set {
				__ptchart = value;

				InvalidateExtrema();
			}
		} PTChart __ptchart;
		/// <summary>
		/// The unit that the pressure values will be converted to.
		/// </summary>
		/// <value>The pressure unit.</value>
		public Unit pressureUnit {
			get {
				return __pressureUnit;
			}
			set {
				__pressureUnit = value;

				InvalidateExtrema();
			}
		} Unit __pressureUnit;
		/// <summary>
		/// The unit that the temperature values will be converted to.
		/// </summary>
		/// <value>The pressure from position.</value>
		public Unit temperatureUnit {
			get {
				return __temperatureUnit;
			}
			set {
				__temperatureUnit = value;

				InvalidateExtrema();
			}
		} Unit __temperatureUnit;

		/// <summary>
		/// Queries the pressure of the view's fluid at its current slide position.
		/// </summary>
		/// <value>The pressure from position.</value>
		public Scalar pressureFromPosition {
			get {
				return Units.Pressure.PSIA.OfScalar(0);
			}
		}

		/// <summary>
		/// Queries the temperature of the view's fluid at its current slide position.
		/// </summary>
		/// <value>The temperature from position.</value>
		public Scalar temperatureFromPosition {
			get {
				return Units.Temperature.FAHRENHEIT.OfScalar(0);
			}
		}

		private float minPressure;
		private float maxPressure;
		private float minTemperature;
		private float maxTemperature;

		/// <summary>
		/// The window that the content of the view will be drawn to.
		/// </summary>
		private RectF window;
		/// <summary>
		/// The width of the slider in pixels, were it able to occupy an ideal space.
		/// </summary>
		private float sliderWidth;
		/// <summary>
		/// The number of pixels that the view has been slid. 
		/// </summary>
		private float slideOffset;
		/// <summary>
		/// The paint that the window rect will be drawn as.
		/// </summary>
		private Paint windowPaint;
		/// <summary>
		/// The paint that is used to paint the pressure ticks and numbers.
		/// </summary>
		/// <param name="context">Context.</param>
		private Paint pressurePaint;
		/// <summary>
		/// The paint that is used to paint the temperature ticks and numbers.
		/// </summary>
		/// <param name="context">Context.</param>
		private Paint temperaturePaint;
		/// <summary>
		/// The last drag x that was recorded for movement.
		/// </summary>
		private float lastDragX;
		/// <summary>
		/// The current scroll offset.
		/// </summary>
		private float offset;
		/// <summary>
		/// The distance between a single temperature values.
		/// </summary>
		private float step;
		/// <summary>
		/// The full height of a tick.
		/// </summary>
		/// <param name="context">Context.</param>
		private float fullTickHeight;

		public FluidSliderView(Context context) : this(context, null) {
		}

		public FluidSliderView(Context context, Android.Util.IAttributeSet attrs) : base(context, attrs) {
			InitPaints();

			__pressureUnit = AppState.context.defaultUnits.pressure;
			__temperatureUnit = AppState.context.defaultUnits.temperature;
			ptChart = PTChart.New(AppState.context, Fluid.EState.Bubble);
		}

		protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec) {
			var w = MeasureSpec.GetSize(widthMeasureSpec);
			var h = MeasureSpec.GetSize(heightMeasureSpec);

			SetMeasuredDimension(MeasureSpec.MakeMeasureSpec(w, MeasureSpecMode.Exactly), MeasureSpec.MakeMeasureSpec(h, MeasureSpecMode.Exactly));

			var padding = h * 0.01f;

			window = new RectF(padding, padding, w - padding, h - padding);

			step = window.Width() / 100;
			fullTickHeight = window.Height() * 0.35f;

			var width = CalculateContentWidth();
//			offset = window.Left + -(width / 2);
			offset = 0;
		}

		public override bool OnTouchEvent(MotionEvent e) {
			var x = e.GetX();

			switch (e.Action) {
				case MotionEventActions.Down:
					lastDragX = x;
					return true;
				case MotionEventActions.Move:
					offset += (x - lastDragX);

					var width = CalculateContentWidth();
					var hw = window.Width() / 2;
					if (offset > hw) {
						offset = hw;
					} else if (offset < -width + hw) {
						offset = -width + hw;
          }

					lastDragX = x;
					Invalidate();

					NotifyOfScroll();
					Log.D(this, "offset: " + offset);
					return true;
				case MotionEventActions.Up:
					return true;
				default:
					Log.D(this, "Defaulting...");
					return false;
			}
		}

		protected override void OnDraw(Canvas canvas) {
			var cx = window.CenterX();

			DrawPressureTicks(canvas);
			DrawTemperatureTicks(canvas);

			canvas.ClipRect(window);

			// Draw the window
			canvas.DrawRoundRect(window, 5, 5, windowPaint);
			// Draw the selection line
			canvas.DrawLine(cx, 0, cx, MeasuredHeight, windowPaint);
		}

		/// <summary>
		/// Draws the pressure tick scale to the canvas.
		/// </summary>
		/// <returns>The pressure ticks.</returns>
		/// <param name="canvas">Canvas.</param>
		private void DrawPressureTicks(Canvas canvas) {
		}

		/// <summary>
		/// Draws the tempreature tick scale to the canvas.
		/// </summary>
		/// <returns>The temperature ticks.</returns>
		/// <param name="canvas">Canvas.</param>
		private void DrawTemperatureTicks(Canvas canvas) {
			var min = (int)minTemperature;
			var max = (int)maxTemperature;
			var range = max - min;
			var fullRange = maxTemperature - minTemperature;
			var width = CalculateContentWidth();


			// Draw the meat of the temperatures
			for (int i = 0; i <= range; i++) {
				var temp = min + i;
				var x = CalculateXCoordForTemperature(temp);
				var y = window.Height() * 0.5f;

				if (temp % 10 == 0) {
					canvas.DrawLine(x, y, x, y + fullTickHeight, temperaturePaint);
					canvas.DrawText(temp + "", x, y + fullTickHeight * 1.1f, temperaturePaint);
				} else if (temp % (10 / 2) == 0) {
					canvas.DrawLine(x, y, x, y + fullTickHeight / 2, temperaturePaint);
				} else {
					canvas.DrawLine(x, y, x, y + fullTickHeight / 10, temperaturePaint);
				}
			}
		}

		/// <summary>
		/// Initialize the paints that are used by the view.
		/// </summary>
		/// <returns>The paints.</returns>
		private void InitPaints() {
			windowPaint = new Paint();
			windowPaint.Color = Android.Graphics.Color.Black;
			windowPaint.SetStyle(Paint.Style.Stroke);

			pressurePaint = new Paint();
			pressurePaint.Color = Android.Graphics.Color.Blue;
			pressurePaint.SetStyle(Paint.Style.Stroke);
			pressurePaint.TextSize = MeasuredHeight * TEXT_SIZE;
			pressurePaint.TextAlign = Paint.Align.Center;

			temperaturePaint = new Paint();
			temperaturePaint.Color = Android.Graphics.Color.Red;
			temperaturePaint.SetStyle(Paint.Style.Stroke);
			temperaturePaint.StrokeWidth = 1.0f;
//			temperaturePaint.TextSize = MeasuredHeight * TEXT_SIZE;
			temperaturePaint.TextAlign = Paint.Align.Center;
		}

		private void InvalidateExtrema() {
			var f = ptChart.fluid;
			var state = ptChart.state;

			var absoluteMinPressure = Physics.ConvertRelativePressureToAbsolute(Units.Pressure.PSIG.OfScalar(-8),
			                                                                    AppState.context.locationManager.lastKnownLocation.altitude);

			minPressure = (float)absoluteMinPressure.ConvertTo(pressureUnit).amount;
			maxPressure = (float)f.GetMaximumPressure(state).ConvertTo(pressureUnit).amount;

			minTemperature = (float)f.GetTemperatureFromPressure(state, absoluteMinPressure).ConvertTo(temperatureUnit).amount;
			maxTemperature = (float)f.GetMaximumTemperature().ConvertTo(temperatureUnit).amount;

			Invalidate();
		}

    private int CalculateStartDivisibilityPoint() {
			return (int)minTemperature + Math.Abs((int)minTemperature % 10);
		}

		private int CalculateEndDivisibilityPoint() {
			return (int)maxTemperature - ((int)maxTemperature % 10);
		}

		private float CalculateContentWidth() {
			var min = minTemperature;
			var max = maxTemperature;
			var range = max - min;
			return step * range;
		}

		private float CalculateXCoordForTemperature(float temperature) {
			var fullRange = maxTemperature - minTemperature;
			var width = CalculateContentWidth();

			return window.Left + offset + (((temperature - minTemperature) / fullRange) * width);
		}

		/// <summary>
		/// Calculates the temperature using the given x. Note: the x should be in relation to the width of the content,
		/// not necessarily the actual pixel x. 
		/// </summary>
		/// <returns>The temperature from x.</returns>
		/// <param name="x">The x coordinate.</param>
		/// <param name="divisibility">Divisibility.</param>
		private Scalar CalculateTemperatureFromX(float x) {
			var fullRange = maxTemperature - minTemperature;
			var width = CalculateContentWidth();

			if (width == 0) {
				return temperatureUnit.OfScalar(double.NaN);
			}

			return temperatureUnit.OfScalar((x - window.Left - offset) / width * fullRange + minTemperature);
		}

		private void NotifyOfScroll() {
			if (onScroll != null) {
				var x = window.Width() / 2;
				var temp = CalculateTemperatureFromX(x);
				onScroll(this, ptChart.GetPressure(temp), temp);
     	}
    }
	}
}
