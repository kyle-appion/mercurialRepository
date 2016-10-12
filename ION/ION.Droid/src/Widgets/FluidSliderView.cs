namespace ION.Droid.Widgets {

	using System;

	using Android.Content;
	using Android.Graphics;
	using Android.Views;

	using ION.Core.App;
	using ION.Core.Fluids;
	using ION.Core.Math;
	using ION.Core.Measure;
	using ION.Core.Util;
	
	public class FluidSliderView : View {
		/// <summary>
		/// The delegate that is called when the fluid slider is scrolled.
		/// </summary>
		public delegate void OnScroll(FluidSliderView slider, bool touching, Scalar pressure, Scalar temperature);

		/// <summary>
		/// The text size for the paints.
		/// </summary>
		private const float TEXT_SIZE = 0.1f;

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
		/// Whether or not the fluid slider is touch locked.
		/// </summary>
		/// <value><c>true</c> if is touch locked; otherwise, <c>false</c>.</value>
		public bool isTouchLocked { get; set; }

		/// <summary>
		/// The minimum relative pressure for the slider.
		/// </summary>
		private float minPressure;
		/// <summary>
		/// The maximum relative pressure for the slider.
		/// </summary>
		private float maxPressure;
		private float minTemperature;
		private float maxTemperature;

		/// <summary>
		/// The window that the content of the view will be drawn to.
		/// </summary>
		private RectF window;
		/// <summary>
		/// The paint that the window rect will be drawn as.
		/// </summary>
		private Paint windowPaint;
		/// <summary>
		/// The paint that is used to paint the pressure ticks and numbers.
		/// </summary>
		private Paint pressurePaint;
		/// <summary>
		/// The paint that is used to paint the temperature ticks and numbers.
		/// </summary>
		private Paint temperaturePaint;
		/// <summary>
		/// The last drag x that was recorded for movement.
		/// </summary>
		private float lastDragX;
		/// <summary>
		/// The current scroll offset. This is relative from the lowest pressure value that acts as the bounds of slider.
		/// (ie, the slider will not scroll its center value passed the lowest pressure value).
		/// </summary>
		private float offset;
		/// <summary>
		/// The distance between a single temperature values.
		/// </summary>
		private float step;
		/// <summary>
		/// The full height of a tick.
		/// </summary>
		private float fullTickHeight;

		public FluidSliderView(Context context) : this(context, null) {
		}

		public FluidSliderView(Context context, Android.Util.IAttributeSet attrs) : base(context, attrs) {
			InitPaints();

			__pressureUnit = AppState.context.defaultUnits.pressure;
			__temperatureUnit = AppState.context.defaultUnits.temperature;
			ptChart = PTChart.New(AppState.context, Fluid.EState.Bubble);
			isTouchLocked = false;
		}

		protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec) {
			var w = MeasureSpec.GetSize(widthMeasureSpec);
			var h = MeasureSpec.GetSize(heightMeasureSpec);

			SetMeasuredDimension(MeasureSpec.MakeMeasureSpec(w, MeasureSpecMode.Exactly), MeasureSpec.MakeMeasureSpec(h, MeasureSpecMode.Exactly));

			var padding = h * 0;

			window = new RectF(padding, padding, w - padding, h - padding);

			step = window.Width() / 50;
			fullTickHeight = window.Height() * 0.25f;
			offset = 0;

			InitPaints();
		}

		public override bool OnTouchEvent(MotionEvent e) {
			if (isTouchLocked) {
				return false;
			}

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

					NotifyOfScroll(true);
					return true;
				case MotionEventActions.Up:
					return true;
				default:
					return false;
			}
		}

		public void ScrollToPressure(Scalar pressure, bool animate) {
			ScrollToTemperature(ptChart.GetTemperature(pressure), animate);
		}

		public void ScrollToTemperature(Scalar temperature, bool animate) {
			if (animate) {
				var temp = (float)temperature.ConvertTo(temperatureUnit).amount;
				var newOffset = this.CalculateOffsetFromTemperature(temp);
				ScrollToOffset(newOffset);
			} else {
				var temp = (float)temperature.ConvertTo(temperatureUnit).amount;
				offset = CalculateOffsetFromTemperature(temp);
				PostInvalidate();
				NotifyOfScroll(false);
			}
		}

		/// <summary>
		/// Scrolls the to the given x coordinate as generated by the calculate x coord for temperature.
		/// </summary>
		private void ScrollToOffset(float newOffset) {
			offset = newOffset;
			Invalidate();
			NotifyOfScroll(false);
		}

		protected override void OnDraw(Canvas canvas) {
			var cx = window.CenterX();

			DrawPressureTicks(canvas);
			DrawTemperatureTicks(canvas);

			canvas.ClipRect(window);

			// Draw the window
			canvas.DrawRoundRect(window, 7, 7, windowPaint);
			// Draw the selection line
			canvas.DrawLine(cx, 0, cx, MeasuredHeight, windowPaint);
		}

		/// <summary>
		/// Draws the pressure tick scale to the canvas.
		/// </summary>
		/// <returns>The pressure ticks.</returns>
		/// <param name="canvas">Canvas.</param>
		private void DrawPressureTicks(Canvas canvas) {
			var minPress = (int)Math.Round(minPressure);
			var span = CalculatePressureTickSpan(minPress);
			minPress = minPress - Math.Sign(minPress) * (minPress % span);
			var y = window.Height() * 0.5f;

			while (minPress < maxPressure) {
				// Draw the big tick
				var x = CalculateRelativeXCoordForTemperature((float)ptChart.GetTemperature(pressureUnit.OfScalar(minPress)).ConvertTo(temperatureUnit).amount);
				canvas.DrawLine(x, y, x, y - fullTickHeight, pressurePaint);
				canvas.DrawText(minPress + "", x, y - fullTickHeight * 1.1f, pressurePaint);

				// Calculate location of subticks
				var subticks = (span <= 2 ? 2 : 5);
				var st = span / subticks;

				// Draw sub ticks
				for (int i = 1; i < subticks; i++) {
					var fx = CalculateRelativeXCoordForTemperature((float)ptChart.GetTemperature(pressureUnit.OfScalar(minPress + st * i)).ConvertTo(temperatureUnit).amount);
					canvas.DrawLine(fx, y, fx, y - fullTickHeight * 0.5f, pressurePaint);
				}

				minPress += span;
				span = CalculatePressureTickSpan(minPress);
			}
		}

		private int CalculatePressureTickSpan(double minPress) {
			if (pressureUnit == Units.Pressure.CM_HG) {
				if (minPress < 50) {
					return 10;
				} else if (minPress < 100) {
					return 50;
				} else if (minPress < 250) {
					return 100;
				} else {
					return 250;
				}
			} else {
				if (minPress < 10) {
					return 2;
				} else if (minPress < 40) {
					return 5;
				} else if (minPress < 100) {
					return 10;
				} else if (minPress < 200) {
					return 50;
				} else {
					return 100;
				}
			}
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

			// Draw the meat of the temperatures
			for (int i = 0; i <= range; i++) {
				var temp = min + i;
				var x = CalculateRelativeXCoordForTemperature(temp);
				var y = window.Height() * 0.5f;
				var tsd = MeasuredHeight * TEXT_SIZE;

				// Check if we need to draw a large temperature tick
				if (temp % 10 == 0) {
					// Draw the temperature line
					canvas.DrawLine(x, y, x, y + fullTickHeight, temperaturePaint);
					canvas.DrawText(temp + "", x, y + fullTickHeight * 1.1f + tsd, temperaturePaint);
				} else if (temp % (10 / 2) == 0) {
					canvas.DrawLine(x, y, x, y + fullTickHeight / 2, temperaturePaint);
				} else {
					canvas.DrawLine(x, y, x, y + fullTickHeight / 5, temperaturePaint);
				}
			}
		}

		/// <summary>
		/// Initialize the paints that are used by the view.
		/// </summary>
		private void InitPaints() {
			windowPaint = new Paint();
			windowPaint.Color = Android.Graphics.Color.Black;
			windowPaint.SetStyle(Paint.Style.Stroke);

			pressurePaint = new Paint();
			pressurePaint.Color = Android.Graphics.Color.Blue;
			pressurePaint.SetStyle(Paint.Style.FillAndStroke);
			pressurePaint.AntiAlias = true;
			pressurePaint.StrokeWidth = 1.0f;
			pressurePaint.TextSize = MeasuredHeight * TEXT_SIZE;
			pressurePaint.TextAlign = Paint.Align.Center;

			temperaturePaint = new Paint();
			temperaturePaint.Color = Android.Graphics.Color.Red;
			temperaturePaint.SetStyle(Paint.Style.FillAndStroke);
			temperaturePaint.AntiAlias = true;
			temperaturePaint.StrokeWidth = 1.0f;
			temperaturePaint.TextSize = MeasuredHeight * TEXT_SIZE;
			temperaturePaint.TextAlign = Paint.Align.Center;
		}

		private void InvalidateExtrema() {
			var f = ptChart.fluid;
			var state = ptChart.state;
			var smp = Units.Pressure.PSIG.OfScalar(-8);

			minPressure = (float)smp.ConvertTo(pressureUnit).amount;
			maxPressure = (float)f.GetMaximumPressure(state).ConvertTo(pressureUnit).amount;

			minTemperature = (float)f.GetTemperatureFromAbsolutePressure(state, Physics.ConvertRelativePressureToAbsolute(smp, ptChart.elevation)).ConvertTo(temperatureUnit).amount;
			maxTemperature = (float)f.GetMaximumTemperature().ConvertTo(temperatureUnit).amount;

			Invalidate();
		}

		private float CalculateContentWidth() {
			var min = minTemperature;
			var max = maxTemperature;
			var range = max - min;
			return step * range;
		}

		private float CalculateAbsoluteXCoordForTemperature(float temperature) {
			if (window == null) {
				return -1;
			}
			var fullRange = maxTemperature - minTemperature;
			var width = CalculateContentWidth();

			var ret = window.Left + (((temperature - minTemperature) / fullRange) * width);
			return ret;
		}

		private float CalculateRelativeXCoordForTemperature(float temperature) {
			return CalculateAbsoluteXCoordForTemperature(temperature) + offset;
		}

		private float CalculateOffsetFromTemperature(float temperature) {
			var fullRange = maxTemperature - minTemperature;
			var width = CalculateContentWidth();
			if (window == null || width == 0) {
				return 0;
			}
			return -((temperature - minTemperature) / fullRange * width) + (window.Right - window.Left) / 2;
		}

		/// <summary>
		/// Calculates the temperature using the given x. Note: the x should be in relation to the width of the content,
		/// not necessarily the actual pixel x. 
		/// </summary>
		/// <returns>The temperature from x.</returns>
		/// <param name="x">The x coordinate.</param>
		private Scalar CalculateTemperatureFromX(float x) {
			var fullRange = maxTemperature - minTemperature;
			var width = CalculateContentWidth();

			if (width == 0) {
				return temperatureUnit.OfScalar(double.NaN);
			}

			return temperatureUnit.OfScalar((x - window.Left - offset) / width * fullRange + minTemperature);
		}

		private void NotifyOfScroll(bool touching) {
			if (onScroll != null && window != null) {
				var x = window.Width() / 2;
				var temp = CalculateTemperatureFromX(x);
				onScroll(this, touching, ptChart.GetPressure(temp), temp);
     	}
    }
	}
}

