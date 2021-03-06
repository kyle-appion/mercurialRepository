﻿﻿namespace ION.Droid.Widgets {

	using System;

	using Android.Content;
	using Android.Graphics;
	using Android.Views;

	using Appion.Commons.Measure;

	using ION.Core.App;
	using ION.Core.Fluids;
	using ION.Core.Math;
	
	public class FluidSliderView : View {

    private const long SCROLL_DURATION = 500;

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
		private int minPressure;
		/// <summary>
		/// The maximum relative pressure for the slider.
		/// </summary>
		private int maxPressure;
		private int minTemperature;
		private int maxTemperature;

		/// <summary>
		/// The window that the content of the view will be drawn to.
		/// </summary>
		private Window window;
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

      var ion = AppState.context;

      __pressureUnit = ion.preferences.units.pressure;
      __temperatureUnit = ion.preferences.units.temperature;
      ptChart = ion.fluidManager.lastUsedFluid.GetPtChart(Fluid.EState.Bubble);
			isTouchLocked = false;
		}

		protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec) {
			var w = MeasureSpec.GetSize(widthMeasureSpec);
			var h = MeasureSpec.GetSize(heightMeasureSpec);

			SetMeasuredDimension(MeasureSpec.MakeMeasureSpec(w, MeasureSpecMode.Exactly), MeasureSpec.MakeMeasureSpec(h, MeasureSpecMode.Exactly));

			Init();
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
					var hw = window.width / 2;
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
			ScrollToTemperature(ptChart.GetTemperature(pressure, true), animate);
		}

		public void ScrollToTemperature(Scalar temperature, bool animate) {
			var t = (float)temperature.ConvertTo(temperatureUnit).amount;
/*
			if (t < minTemperature) {
				t = minTemperature;
			} else if (t > maxTemperature) {
				t = maxTemperature;
			}
*/

			if (animate) {
				var newOffset = this.CalculateOffsetFromTemperature(t);
				ScrollToOffset(newOffset);
			} else {
				offset = CalculateOffsetFromTemperature(t);
				Invalidate();
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

		private readonly RectF rect = new RectF();
		protected override void OnDraw(Canvas canvas) {
			var cx = window.centerX;

			DrawPressureTicks(canvas);
			DrawTemperatureTicks(canvas);

			window.FillRect(rect);

			canvas.ClipRect(rect);

      // Draw the window

      rect.Left += 1;
      rect.Top += 1;
      rect.Right -= 1;
      rect.Bottom -= 1;
			canvas.DrawRoundRect(rect, 7, 7, windowPaint);
			// Draw the selection line
			canvas.DrawLine(cx, 0, cx, MeasuredHeight, windowPaint);
		}

		/// <summary>
		/// Draws the pressure tick scale to the canvas.
		/// </summary>
		/// <returns>The pressure ticks.</returns>
		/// <param name="canvas">Canvas.</param>
		private void DrawPressureTicks(Canvas canvas) {
			var minPress = minPressure;
			var span = CalculatePressureTickSpan(minPress);
			minPress = minPress - Math.Sign(minPress) * (minPress % span);
			var y = window.height * 0.5f;

			while (minPress <= maxPressure) {
				// Draw the big tick
				var x = CalculateRelativeXCoordForTemperature((float)ptChart.GetTemperature(pressureUnit.OfScalar(minPress), true).ConvertTo(temperatureUnit).amount);
				canvas.DrawLine(x, y, x, y - fullTickHeight, pressurePaint);
				canvas.DrawText(minPress + "", x, y - fullTickHeight * 1.1f, pressurePaint);

				// Calculate location of subticks
				var subticks = (span <= 2 ? 2 : 5);
				var st = span / subticks;

				// Draw sub ticks
				for (int i = 1; i < subticks; i++) {
					var fx = CalculateRelativeXCoordForTemperature((float)ptChart.GetTemperature(pressureUnit.OfScalar(minPress + st * i), true).ConvertTo(temperatureUnit).amount);
					canvas.DrawLine(fx, y, fx, y - fullTickHeight * 0.5f, pressurePaint);
				}

				minPress += span;
				span = CalculatePressureTickSpan(minPress);
			}
		}

		private int CalculatePressureTickSpan(double minPress) {
      // TODO ahodder@appioninc.com: This needs to be more algorithmic.
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
      } else if (pressureUnit == Units.Pressure.KILOPASCAL) {
        if (minPress < 40) {
          return 5;
        } else if (minPress < 100) {
          return 10;
        } else if (minPress < 200) {
          return 50;
        } else if (minPress < 500) {
          return 100; 
        } else if (minPress < 1000) {
          return 200;
        } else {
          return 500;
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
        } else if (minPress < 500) {
					return 100; 
        } else if (minPress < 1000) {
          return 200;
        } else {
          return 500;
        }
			}
		}

		/// <summary>
		/// Draws the tempreature tick scale to the canvas.
		/// </summary>
		/// <returns>The temperature ticks.</returns>
		/// <param name="canvas">Canvas.</param>
		private void DrawTemperatureTicks(Canvas canvas) {
			var min = minTemperature;
			var max = maxTemperature;
			var range = max - min;

			// Draw the meat of the temperatures
			for (int i = 0; i <= range; i++) {
				var temp = min + i;
				var x = CalculateRelativeXCoordForTemperature(temp);
				var y = window.height * 0.5f;
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

		private void Init() {
			var w = MeasuredWidth;
			var h = MeasuredHeight;
			var padding = h * 0;

			window.Set(new RectF(padding, padding, w - padding, h - padding));

      if (this.pressureUnit.Equals(Units.Pressure.KILOPASCAL)) {
  			step = window.width / 50;
      } else {
        step = pressurePaint.MeasureText("99");
      }


			fullTickHeight = window.height * 0.25f;
			offset = 0;

			InitPaints();
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
			var smp = Units.Pressure.PSIG.OfScalar(-8);
      var amin = ptChart.minRelativePressure;
      var amax = ptChart.maxRelativePressure;

      amin = amin.ConvertTo(pressureUnit);
      amax = amax.ConvertTo(pressureUnit);

			if (smp.amount > amin.amount) {
				amin = smp;
			}

			minPressure = (int)Math.Ceiling(amin.amount);
      maxPressure = (int)Math.Floor(amax.amount);

			amin = pressureUnit.OfScalar(minPressure);
			amax = pressureUnit.OfScalar(maxPressure);

      minTemperature = (int)Math.Ceiling(ptChart.GetTemperatureFromRelativePressure(amin).ConvertTo(temperatureUnit).amount);
      maxTemperature = (int)Math.Floor(ptChart.GetTemperatureFromRelativePressure(amax).ConvertTo(temperatureUnit).amount);

			Invalidate();
		}

		private float CalculateContentWidth() {
			var min = minTemperature;
			var max = maxTemperature;
			var range = max - min;
			return step * range;
		}

		private float CalculateAbsoluteXCoordForTemperature(float temperature) {
			var fullRange = maxTemperature - minTemperature;
			var width = CalculateContentWidth();

			var ret = window.left + (((temperature - minTemperature) / fullRange) * width);
			return ret;
		}

		private float CalculateRelativeXCoordForTemperature(float temperature) {
			return CalculateAbsoluteXCoordForTemperature(temperature) + offset;
		}

		private float CalculateOffsetFromTemperature(float temperature) {
			var fullRange = maxTemperature - minTemperature;
			var width = CalculateContentWidth();
			if (width == 0) {
				return 0;
			}
			return -((temperature - minTemperature) / fullRange * width) + (window.right - window.left) / 2;
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

			return temperatureUnit.OfScalar((x - window.left - offset) / width * fullRange + minTemperature);
		}

		private void NotifyOfScroll(bool touching) {
			if (onScroll != null) {
				var x = window.width / 2;
				var temp = CalculateTemperatureFromX(x);
				onScroll(this, touching, ptChart.GetRelativePressure(temp), temp);
     	}
    }

		/// <summary>
		/// This struct was created to replace RectF because it kept getting disposed by the fucking xamarin framework.
		/// </summary>
		private struct Window {
			public float left, right, top, bottom;

			public float width { get { return right - left; } }
			public float height { get { return bottom - top; } }

			public float centerX { get { return (left + right) / 2; } }
			public float centerY { get { return (top + bottom) / 2; } }

			public void Set(RectF rect) {
				left = rect.Left;
				right = rect.Right;
				top = rect.Top;
				bottom = rect.Bottom;
			}

			public void FillRect(RectF rect) {
				rect.Left = left;
				rect.Right = right;
				rect.Top = top;
				rect.Bottom = bottom;
			}
		}
	}
}

