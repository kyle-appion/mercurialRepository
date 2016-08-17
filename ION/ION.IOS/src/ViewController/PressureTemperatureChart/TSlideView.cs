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
  public class TSlideView : UIView{

    CGPath temperaturePath;
    PTChart ptChart;
    Unit lookup;
    Unit tempLookup;
    Sensor pressureSensor;
    public Scalar minTemperature;
    double sViewWidth;
    double sViewHeight;
    public double startGap;
    public double tempTicks;
    public double maxTemperature;
    public double measurementWidth;
    IION ion;

    public TSlideView(PTChart chart, UIScrollView scrollView, Unit pUnit, Unit tUnit, Sensor sensor) {
      ion = AppState.context;
      pressureSensor = sensor;
      var minPressure = setPressureStart(pUnit);
      minTemperature = new Scalar();
      minTemperature = chart.GetTemperature(new Scalar(pUnit,minPressure),pressureSensor.isRelative).ConvertTo(tUnit);
      ptChart = chart;
      sViewWidth = scrollView.Bounds.Width;
      sViewHeight = scrollView.Bounds.Height;
      lookup = pUnit;
      tempLookup = tUnit;
      this.Frame = new CGRect(0, 0, 2188.8, sViewHeight);
    }
    public TSlideView(IntPtr handle) : base(handle){}

    public void resetData(PTChart newChart, Unit pressureUnit, Unit temperatureUnit){
      Console.WriteLine("Resetting data from change. Pressure unit: " + pressureUnit + " temperature unit: " + temperatureUnit);
      tempLookup = temperatureUnit;

      lookup = pressureUnit;

      ptChart = newChart;

      this.SetNeedsDisplay();
    }

    public override void Draw (CGRect rect) {
      base.Draw (rect);

      var minPressure = setPressureStart(lookup);
      var maxPressure = ptChart.fluid.GetMaximumPressure(ptChart.state).ConvertTo(lookup);
      maxPressure = ION.Core.Math.Physics.ConvertAbsolutePressureToRelative(maxPressure,ion.locationManager.lastKnownLocation.altitude);

      minTemperature = ptChart.GetTemperature(new Scalar(lookup,minPressure),pressureSensor.isRelative).ConvertTo(tempLookup);
      maxTemperature = ptChart.fluid.GetMaximumTemperature().ConvertTo(tempLookup).amount;

      Console.WriteLine("temp min pressure: " + minPressure);
      Console.WriteLine("temp max pressure: " + maxPressure.amount);
      Console.WriteLine("temp min temperature: " + minTemperature.amount);
      Console.WriteLine("temp max temperature: " + maxTemperature);

      startGap = sViewWidth / 2.0;
      measurementWidth = this.Frame.Width - sViewWidth;

      CGContext gctx = UIGraphics.GetCurrentContext();

      //set up drawing attributes
      gctx.SetLineWidth (.0001f * rect.Width);
      gctx.SetLineCap (CGLineCap.Round);

      temperaturePath = new CGPath ();

      var tempCount = Math.Ceiling(minTemperature.amount);
      var tempRange = maxTemperature - minTemperature.amount;

      tempTicks = measurementWidth / tempRange;

      var tempIncrease = startGap + ((tempCount - minTemperature.amount)/ tempRange * measurementWidth);

      if (tempCount % 10 == 0) {
        temperaturePath.AddLines(new CGPoint[] {
          new CGPoint(tempIncrease, 0),

          new CGPoint(tempIncrease, .5 * sViewHeight)
        });
        DrawTemperatureNumber(tempCount, tempIncrease);
      } else {
        temperaturePath.AddLines(new CGPoint[] {
          new CGPoint(tempIncrease, 0),

          new CGPoint(tempIncrease, .26 * sViewHeight)
        });
      }
      var pressLower = ptChart.GetPressure(new Scalar(tempLookup,tempCount),pressureSensor.isRelative).ConvertTo(lookup).amount;
      tempIncrease += tempTicks;
      tempCount++;

      for (; tempCount <= maxTemperature; tempCount++) {        
        if (tempCount % 10 == 0) {
          temperaturePath.AddLines(new CGPoint[] {
            new CGPoint(tempIncrease, 0),

            new CGPoint(tempIncrease, .5 * sViewHeight)
          });
          DrawTemperatureNumber(tempCount, tempIncrease);
        } else if (tempCount % 5 == 0){
          temperaturePath.AddLines(new CGPoint[] {
            new CGPoint(tempIncrease, 0),

            new CGPoint(tempIncrease, .4 * sViewHeight)
          });
        } else {
          temperaturePath.AddLines(new CGPoint[] {
            new CGPoint(tempIncrease, 0),

            new CGPoint(tempIncrease, .26 * sViewHeight)
          });
        }
        tempIncrease += tempTicks;
      }

      UIColor.Red.SetStroke ();
      gctx.AddPath (temperaturePath);
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
        return -.5;
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

        CGRect erect = new CGRect(increase -15 , (.25 * this.Frame.Height) + 20, 30, 20);

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

