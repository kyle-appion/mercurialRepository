using System;
using UIKit;
using CoreGraphics;
using Foundation;
using CoreText;

using ION.Core.Fluids;
using ION.Core.Measure;
using ION.Core.App;
using ION.Core.Sensors;

namespace ION.IOS.ViewController.PressureTemperatureChart {
  public class PTSlideView : UIView{

		CGPath pressurePath;
    CGPath temperaturePath;
    PTChart ptChart;
    public Unit lookup;
    public Unit tempLookup;
    Sensor pressureSensor;
		public Scalar maxPressure;
		public Scalar minTemperature;
    public double maxTemperature;
    double sViewWidth;
    double sViewHeight;
    double firstMod;
    double secondMod;
    double thirdMod;
    double fourthMod;
    public double minPressure;
    public double firstMeasurements;
    public int secondMeasurements;
    public int middleMeasurements;
    public double startGap;
		public double pressTicks;
		public double tempTicks;
    public double measurementWidth;
    IION ion;

    public PTSlideView(PTChart chart, UIScrollView scrollView, Unit pUnit, Unit tUnit, Sensor sensor) {
      ion = AppState.context;
      pressureSensor = sensor;
		  lookup = pUnit;
		  tempLookup = tUnit;
		  ptChart = chart;

		  minPressure = setPressureStart(lookup);
		  minTemperature = new Scalar();
		  if(pUnit == null){Console.WriteLine("punit is null");}
		  if(pressureSensor == null){Console.WriteLine("Pressure sensor null");}
		  minTemperature = chart.GetTemperature(new Scalar(pUnit,minPressure),pressureSensor.isRelative).ConvertTo(tUnit);

      sViewWidth = scrollView.Bounds.Width;
      sViewHeight = scrollView.Bounds.Height;

      this.Frame = new CGRect(0, 0, 2188.8, sViewHeight);
    }
    public PTSlideView(IntPtr handle) : base(handle){}

    public void resetData(PTChart newChart, Unit pressureUnit, Unit temperatureUnit){
      //Console.WriteLine("Resetting data from change. Pressure unit: " + pressureUnit + " temperature unit: " + temperatureUnit);
      tempLookup = temperatureUnit;

      lookup = pressureUnit;

      ptChart = newChart;

      this.SetNeedsDisplay();
    }

    public override void Draw (CGRect rect) {
      base.Draw (rect);
		
      minPressure = setPressureStart(lookup);
	  //minPressure = ptChart.fluid.GetMinimumPressure(ptChart.state).ConvertTo(lookup).amount;
	  	maxPressure = ptChart.fluid.GetMaximumPressure(ptChart.state).ConvertTo(lookup);
      maxPressure = ION.Core.Math.Physics.ConvertAbsolutePressureToRelative(maxPressure,ion.locationManager.lastKnownLocation.altitude).ConvertTo(lookup);

      minTemperature = ptChart.GetTemperature(new Scalar(lookup,minPressure), pressureSensor.isRelative).ConvertTo(tempLookup);
	  	maxTemperature = ptChart.fluid.GetMaximumTemperature().ConvertTo(tempLookup).amount;

		  //Console.WriteLine ("pressure sensor relativeness: " + pressureSensor.isRelative);
	   // Console.WriteLine ("min pressure: " + minPressure);
	   // Console.WriteLine ("max pressure: " + maxPressure.amount);
		  //Console.WriteLine ("min temperature: " + minTemperature);
		  //Console.WriteLine ("fluid min temperature: " + ptChart.fluid.GetMinimumTemperature ().ConvertTo(tempLookup));
		  //Console.WriteLine ("min pressure for min temperature" + ptChart.GetPressure(new Scalar(minTemperature.unit,minTemperature.amount),pressureSensor.isRelative).ConvertTo(lookup));
		  //Console.WriteLine ("min pressure for fluid min temperature: " + ptChart.GetPressure(new Scalar(tempLookup,ptChart.fluid.GetMinimumTemperature ().ConvertTo(tempLookup).amount),pressureSensor.isRelative).ConvertTo(lookup));
	   // Console.WriteLine ("max temperature: " + maxTemperature);

      startGap = sViewWidth / 2.0;
      measurementWidth = this.Frame.Width - sViewWidth;

      firstMeasurements = setPressureRange(lookup,1);

      secondMeasurements = setPressureRange(lookup, 2);

      middleMeasurements = setPressureRange(lookup, 3);

      //var lastMeasurements = maxPressure.amount - middleMeasurements;     

      firstMod = setPressureMod(lookup, 1);
      secondMod = setPressureMod(lookup, 2);
      thirdMod = setPressureMod(lookup, 3);
      fourthMod = setPressureMod(lookup, 4);

      CGContext gctx = UIGraphics.GetCurrentContext();

      //set up drawing attributes
      gctx.SetLineWidth (.0001f * rect.Width);
      UIColor.Blue.SetStroke ();
      gctx.SetLineCap (CGLineCap.Round);

	  pressurePath = new CGPath ();
      temperaturePath = new CGPath ();
	  var tempCount = (int)Math.Ceiling(minTemperature.amount);
	  var tempRange = maxTemperature - minTemperature.amount;

	  var pressCount = (int)Math.Ceiling (minPressure);

	  tempTicks = measurementWidth / tempRange;

	  var tempIncrease = startGap + ((tempCount - minTemperature.amount)/ tempRange * measurementWidth);


	  if (tempCount % 10 == 0) {
		temperaturePath.AddLines(new CGPoint[] {
			new CGPoint(tempIncrease, .5 * sViewHeight),

			new CGPoint(tempIncrease, .75 * sViewHeight)
		});
		DrawTemperatureNumber(tempCount, tempIncrease);
	  } else {
		  temperaturePath.AddLines(new CGPoint[] {
			new CGPoint(tempIncrease, .5 * sViewHeight),

			new CGPoint(tempIncrease, .6 * sViewHeight)
		  });
	  }
	  var pressLower = ptChart.GetPressure(new Scalar(tempLookup,tempCount),pressureSensor.isRelative).ConvertTo(lookup).amount;
	  tempIncrease += tempTicks;
	  tempCount++;

	  for (; tempCount <= maxTemperature; tempCount++) {        
		if (tempCount % 10 == 0) {
			temperaturePath.AddLines(new CGPoint[] {
				new CGPoint(tempIncrease, .5 * sViewHeight),

				new CGPoint(tempIncrease, .85 * sViewHeight)
			});
			DrawTemperatureNumber(tempCount, tempIncrease);
		} else if (tempCount % 5 == 0){
			temperaturePath.AddLines(new CGPoint[] {
				new CGPoint(tempIncrease, .5 * sViewHeight),

				new CGPoint(tempIncrease, .75 * sViewHeight)
			});
		} else {
			temperaturePath.AddLines(new CGPoint[] {
				new CGPoint(tempIncrease, .5 * sViewHeight),

				new CGPoint(tempIncrease, .6 * sViewHeight)
			});
		}
		tempIncrease += tempTicks;
	  }
	  
	  for (; pressCount < maxPressure.amount; pressCount++) {
		var pressTemperature = ptChart.GetTemperature (new Scalar (lookup,pressCount), pressureSensor.isRelative).ConvertTo(tempLookup).amount;
		var tempDifference = pressTemperature - minTemperature.amount;
		var tempOffset = tempDifference * tempTicks + startGap;
		
		if (pressCount < firstMeasurements) {
			if (pressCount % firstMod == 0) {
				pressurePath.AddLines (new CGPoint[] { 
					new CGPoint (tempOffset, .5 * sViewHeight),

					new CGPoint (tempOffset, .25 * sViewHeight)
				});
				DrawPressureNumber (pressCount, tempOffset);
			} else {
				pressurePath.AddLines (new CGPoint[] { 
					new CGPoint (tempOffset, .5 * sViewHeight),

					new CGPoint (tempOffset, .4 * sViewHeight)
				});
			}
		} else if (pressCount < secondMeasurements) {
			if (pressCount % secondMod == 0) {
				pressurePath.AddLines (new CGPoint[] { 
					new CGPoint (tempOffset, .5 * sViewHeight),

					new CGPoint (tempOffset, .25 * sViewHeight)
				});
				DrawPressureNumber (pressCount, tempOffset);
			} else if (pressCount % secondMod == 0) {
				pressurePath.AddLines (new CGPoint[] { 
					new CGPoint (tempOffset, .5 * sViewHeight),

					new CGPoint (tempOffset, .4 * sViewHeight)
				});
			}
		} else if (pressCount < middleMeasurements) {
			if (pressCount % thirdMod == 0) {
				pressurePath.AddLines (new CGPoint[] { 
					new CGPoint (tempOffset, .5 * sViewHeight),

					new CGPoint (tempOffset, .25 * sViewHeight)
				});
				DrawPressureNumber (pressCount, tempOffset);
			} else if (pressCount % secondMod == 0) {
				pressurePath.AddLines (new CGPoint[] { 
					new CGPoint (tempOffset, .5 * sViewHeight),

					new CGPoint (tempOffset, .4 * sViewHeight)
				});
			}
		} else {
			if (pressCount % fourthMod == 0) {
				pressurePath.AddLines (new CGPoint[] { 
					new CGPoint (tempOffset, .5 * sViewHeight),

					new CGPoint (tempOffset, .25 * sViewHeight)
				});
				DrawPressureNumber (pressCount, tempOffset);
			} else if (pressCount % thirdMod == 0) {
				pressurePath.AddLines (new CGPoint[] { 
					new CGPoint (tempOffset, .5 * sViewHeight),

					new CGPoint (tempOffset, .4 * sViewHeight)
				});
			}
		}		
	  }

	  UIColor.Red.SetStroke ();
	  gctx.AddPath (temperaturePath);
	  gctx.DrawPath (CGPathDrawingMode.FillStroke);

	  UIColor.Blue.SetStroke ();
      gctx.AddPath (pressurePath);
      gctx.DrawPath (CGPathDrawingMode.FillStroke);
    }

    public double setPressureStart(Unit pressureUnit){
      if(pressureUnit == Units.Pressure.PSIG){
        return -8;
      } else if (pressureUnit == Units.Pressure.BAR){
        return -.5;
      } else if (pressureUnit == Units.Pressure.IN_HG){
        return -12;
      } else if (pressureUnit == Units.Pressure.CM_HG){
        return -20;
      } else if (pressureUnit == Units.Pressure.KG_CM){
        return -.5;
      } else if (pressureUnit == Units.Pressure.KILOPASCAL){
        return -50;
      } else if (pressureUnit == Units.Pressure.MEGAPASCAL){
		return ptChart.fluid.GetMinimumPressure(ptChart.state).ConvertTo(lookup).amount;
      } else {
        return 0;
      }
    }

    private int setPressureRange(Unit pressureUnit, int section){
      if(pressureUnit == Units.Pressure.PSIG){
        switch (section) {
          case 1:
            return 10;

          case 2:
            return 40;

          default:
            return 100;
        }
      } else if (pressureUnit == Units.Pressure.BAR){
        switch (section) {
          case 1:
            return 1;

          case 2:
            return 3;

          default:
            return 10;
        }
      } else if (pressureUnit == Units.Pressure.IN_HG){
        switch (section) {
          case 1:
            return 60;

          case 2:
            return 100;

          default:
            return 600;
        }
      } else if (pressureUnit == Units.Pressure.CM_HG){
        switch (section) {
          case 1:
            return 120;

          case 2:
            return 200;

          default:
            return 1200;
        }
      } else if (pressureUnit == Units.Pressure.KG_CM){
        switch (section) {
          case 1:
            return 2;

          case 2:
            return 4;

          default:
            return 20;
        }
      } else if (pressureUnit == Units.Pressure.KILOPASCAL){
        switch (section) {
          case 1:
            return 100;

          case 2:
            return 400;

          default:
            return 1200;
        }
      } else if (pressureUnit == Units.Pressure.MEGAPASCAL){
        switch (section) {
          case 1:
            return 1;

          case 2:
            return 2;

          default:
            return 5;
        }
      } else {
        switch (section) {
          case 1:
            return 1;

          case 2:
            return 2;

          default:
            return 5;
        }
      }
    }

    private double setPressureMod(Unit pressureUnit, int section){
      if(pressureUnit == Units.Pressure.PSIG){
        switch (section) {
          case 1:
            return 2;

          case 2:
            return 5;

          case 3:
            return 10;

          default:
            return 50;
        }
      } else if (pressureUnit == Units.Pressure.BAR){
        switch (section) {
          case 1:
            return .2;

          case 2:
            return .5;

          case 3:
            return 1;

          default:
            return 5;
        }
      } else if (pressureUnit == Units.Pressure.IN_HG){
        switch (section) {
          case 1:
            return 5;

          case 2:
            return 25;

          case 3:
            return 50;

          default:
            return 100; 
        }
      } else if (pressureUnit == Units.Pressure.CM_HG){
        switch (section) {
          case 1:
            return 10;

          case 2:
            return 50;

          case 3:
            return 100;

          default:
            return 200;  
        }
      } else if (pressureUnit == Units.Pressure.KG_CM){
        switch (section) {
          case 1:
            return .2;

          case 2:
            return .5;

          case 3:
            return 1;

          default:
            return 5;
        }
      } else if (pressureUnit == Units.Pressure.KILOPASCAL){
        switch (section) {
          case 1:
            return 10;

          case 2:
            return 50;

          case 3:
            return 100;

          default:
            return 500;  
        }
      } else if (pressureUnit == Units.Pressure.MEGAPASCAL){
        switch (section) {
          case 1:
            return .2;

          case 2:
            return 1;

          case 3:
            return 1;

          default:
            return 10;    
        }
      } else {
        switch (section) {
          case 1:
            return .2;

          case 2:
            return 1;

          case 3:
            return 5;

          default:
            return 10;    
        }
      }
    }

    private void DrawPressureNumber(int count, double increase){
      using (var font = new CTFont("Verdana", 10))
      using (var context = UIGraphics.GetCurrentContext ()) {
        UIColor.Clear.SetFill ();

        CGRect erect = new CGRect(increase - 15 , 10, 30, 20);

        // Fill and stroke the the circle first so the
        // number will be drawn on top of the circle
        //          using (var path = new CGPath()) { 
        //            path.AddEllipseInRect (erect);
        //            context.AddPath (path);
        //            context.DrawPath (CGPathDrawingMode.FillStroke);
        //          }


        // Then draw the number 'on top of' the circle
        using (var path = new CGPath())
        using (var attrString = new NSMutableAttributedString(count.ToString ())) {

          // We'll Set the justification to center so it'll
          // print the number in the center of the erect
          CTParagraphStyle alignStyle = new CTParagraphStyle(new CTParagraphStyleSettings {
            Alignment = CTTextAlignment.Center
          });

          // Calculate the range of the attributed string
          NSRange stringRange = new NSRange(0, attrString.Length);

          // Add style attributes to the attributed string
          attrString.AddAttributes (new CTStringAttributes {
            Font = font,
            ParagraphStyle = alignStyle,
            ForegroundColor = UIColor.Blue.CGColor
          }, stringRange);

          // Create a container for the attributed string 
          using (var framesetter = new CTFramesetter(attrString)) {

            erect.Y = -erect.Y;

            path.AddRect (erect);

            using (var frame = framesetter.GetFrame (stringRange, path, null)) {
              context.SaveState ();
              context.TranslateCTM ((nfloat)0, erect.Height);
              context.ScaleCTM (1, -1);
              frame.Draw (context);
              context.RestoreState ();
            }
          }
        }
      }
    }

    private void DrawTemperatureNumber(double degree, double increase){
      using (var font = new CTFont("Verdana", 10))
      using (var context = UIGraphics.GetCurrentContext ()) {
        UIColor.Clear.SetFill ();

        CGRect erect = new CGRect(increase -15 , (.7 * sViewHeight) + 20, 30, 20);

        // Fill and stroke the the circle first so the
        // number will be drawn on top of the circle
        //          using (var path = new CGPath()) { 
        //            path.AddEllipseInRect (erect);
        //            context.AddPath (path);
        //            context.DrawPath (CGPathDrawingMode.FillStroke);
        //          }


        // Then draw the number 'on top of' the circle
        using (var path = new CGPath())
        using (var attrString = new NSMutableAttributedString(degree.ToString ())) {

          // We'll Set the justification to center so it'll
          // print the number in the center of the erect
          CTParagraphStyle alignStyle = new CTParagraphStyle(new CTParagraphStyleSettings {
            Alignment = CTTextAlignment.Center
          });

          // Calculate the range of the attributed string
          NSRange stringRange = new NSRange(0, attrString.Length);

          // Add style attributes to the attributed string
          attrString.AddAttributes (new CTStringAttributes {
            Font = font,
            ParagraphStyle = alignStyle,
            ForegroundColor = UIColor.Red.CGColor
          }, stringRange);

          // Create a container for the attributed string 
          using (var framesetter = new CTFramesetter(attrString)) {

            erect.Y = -erect.Y;

            path.AddRect (erect);

            using (var frame = framesetter.GetFrame (stringRange, path, null)) {
              context.SaveState ();
              context.TranslateCTM ((nfloat)0, erect.Height);
              context.ScaleCTM (1, -1);
              frame.Draw (context);
              context.RestoreState ();
            }
          }
        }
      }
    }
  }
}

