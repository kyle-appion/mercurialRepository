using System;
using UIKit;
using CoreGraphics;
using Foundation;
using CoreText;

using ION.Core.Fluids;
using ION.Core.Measure;
using ION.Core.App;

namespace ION.IOS.ViewController.PressureTemperatureChart {
  public class PTSlideView : UIView{

    CGPath pressurePath;
    CGPath temperaturePath;
    PTChart ptChart;
    UILabel temperatureLabels;
    Unit lookup;
    Unit tempLookup;
    double sViewWidth;
    double sViewHeight;
    double firstMod;
    double secondMod;
    double thirdMod;
    double fourthMod;
    public double firstMeasurements;
    public int secondMeasurements;
    public int middleMeasurements;
    public double startGap;
    public double firstTicks;
    public double secondTicks;
    public double thirdTicks;
    public double lastTicks;
    public double first15;
    public double firstEnd;
    public double second15;
    public double secondEnd;
    public double middle25;
    public double middleEnd;
    public double last45;
    public double lastEnd;
    public double minTemperature;
    public double maxTemperature;
    IION ion;
    public PTSlideView(PTChart chart, UIScrollView scrollView, Unit pUnit, Unit tUnit) {
      ion = AppState.context;
      ptChart = chart;
      sViewWidth = scrollView.Bounds.Width;
      sViewHeight = scrollView.Bounds.Height;
      lookup = pUnit;
      tempLookup = tUnit;
      this.Frame = new CGRect(0, 0, 2188.8, sViewHeight);
      Console.WriteLine("slider content size: " + this.Frame.Width);
    }
    public PTSlideView(IntPtr handle) : base(handle){}

    public void resetData(PTChart newChart, Unit pressureUnit, Unit temperatureUnit){
      Console.WriteLine("Resetting data from change. Pressure unit: " + pressureUnit + " temperature unit: " + temperatureUnit);
      tempLookup = temperatureUnit;

      lookup = pressureUnit;

      ptChart = newChart;

      this.SetNeedsDisplay();
    }

    public override void Draw (CGRect rect) {
      base.Draw (rect);

      var minTemperature = ptChart.fluid.GetMinimumTemperature().ConvertTo(tempLookup);
      maxTemperature = ptChart.fluid.GetMaximumTemperature().ConvertTo(tempLookup).amount;
      var minPressure = ptChart.fluid.GetMinimumPressure(ptChart.state).ConvertTo(lookup);
      minPressure = ION.Core.Math.Physics.ConvertAbsolutePressureToRelative(minPressure,ion.locationManager.lastKnownLocation.altitude);
      var maxPressure = ptChart.fluid.GetMaximumPressure(ptChart.state).ConvertTo(lookup);
      maxPressure = ION.Core.Math.Physics.ConvertAbsolutePressureToRelative(maxPressure,ion.locationManager.lastKnownLocation.altitude);
        
      Console.WriteLine("Pulled min temperature: " + ptChart.fluid.GetMinimumTemperature() + " conveted to unit using: " + minTemperature);
      Console.WriteLine("Pulled max temperature: " + ptChart.fluid.GetMaximumTemperature()+ " conveted to unit using: " + maxTemperature);
      Console.WriteLine("Pulled min pressure: " + ptChart.fluid.GetMinimumPressure(ptChart.state) + " converted to : " + minPressure);
      Console.WriteLine("Pulled max pressure: " + ptChart.fluid.GetMaximumPressure(ptChart.state) + " converted to unit using: " + maxPressure);
      Console.WriteLine("Min starting value: " + Math.Ceiling(minPressure.amount));

      startGap = sViewWidth / 2.0;
      var measurementWidth = this.Frame.Width - sViewWidth;

      first15 = (measurementWidth * .15);
      second15 = (measurementWidth * .15);
      middle25 = (measurementWidth * .25);
      last45 = (measurementWidth * .45);

      firstMeasurements = setPressureRange(lookup,1);
      firstTicks = first15/(firstMeasurements - minPressure.amount);

      secondMeasurements = setPressureRange(lookup, 2);
      secondTicks = second15 / (secondMeasurements - setPressureRange(lookup,1));

      middleMeasurements = setPressureRange(lookup, 3);
      thirdTicks = middle25 / (middleMeasurements - setPressureRange(lookup,2));

      var lastMeasurements = maxPressure.amount - middleMeasurements;
      lastTicks = last45 / lastMeasurements;      

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
      var count = (int)Math.Ceiling(minPressure.amount);
      Console.WriteLine("Count starts at: " + count);
      var increase = startGap + ((count - minPressure.amount)/ first15 * measurementWidth);

      if(count % firstMod == 0){          
        pressurePath.AddLines(new CGPoint[] {
          new CGPoint(increase, .5 * sViewHeight), 

          new CGPoint(increase, .25 * this.Frame.Height)
        });
        DrawPressureNumber(count, increase);
      } else {
        pressurePath.AddLines(new CGPoint[] {
          new CGPoint(increase, .5 * sViewHeight),

          new CGPoint(increase, .4 * this.Frame.Height)         
        });
      }
      increase += firstTicks;
      count++;

      for(; count < firstMeasurements; count++){        
        if(count % firstMod == 0){          
          pressurePath.AddLines(new CGPoint[] {
            new CGPoint(increase, .5 * sViewHeight), 

            new CGPoint(increase, .25 * this.Frame.Height)
          });
          DrawPressureNumber(count, increase);
        } else {
          pressurePath.AddLines(new CGPoint[] {
            new CGPoint(increase, .5 * sViewHeight),

            new CGPoint(increase, .4 * this.Frame.Height)         
          });
        }       
        increase += firstTicks;
      }

      for (; count < secondMeasurements;count++) {        
        if (count % secondMod == 0) {
          pressurePath.AddLines(new CGPoint[] {
            new CGPoint(increase, .5 * sViewHeight),

            new CGPoint(increase, .25 * sViewHeight)         
          });
          DrawPressureNumber(count, increase);
        } else {
          pressurePath.AddLines(new CGPoint[] {
            new CGPoint(increase, .5 * sViewHeight), 

            new CGPoint(increase, .4 * sViewHeight)         
          });
        }
        increase += secondTicks;
      }

      for (; count < middleMeasurements;count++) {       
        if (count % thirdMod == 0) {
          pressurePath.AddLines(new CGPoint[] {
            new CGPoint(increase, .5 * sViewHeight),

            new CGPoint(increase, .25 * sViewHeight)
          });
          DrawPressureNumber(count, increase);
        } else if (count % secondMod == 0){
          pressurePath.AddLines(new CGPoint[] {
            new CGPoint(increase, .5 * sViewHeight),

            new CGPoint(increase, .35 * sViewHeight)
          });
        }
        increase += thirdTicks;
      }

      for (; count < maxPressure.amount;count++) {        
        if (count % fourthMod == 0) {
          pressurePath.AddLines(new CGPoint[] {
            new CGPoint(increase, .5 * sViewHeight),

            new CGPoint(increase, .25 * sViewHeight)
          });
          DrawPressureNumber(count, increase);
        } 
        else if(count % thirdMod == 0){
          pressurePath.AddLines(new CGPoint[] {
            new CGPoint(increase, .5 * sViewHeight),

            new CGPoint(increase, .4 * sViewHeight)
          });
        }
        increase += lastTicks;
      }

      var below0 = minTemperature.amount;
      var above0 = maxTemperature;
      var tempRange = above0 - below0;
      var tempTicks = measurementWidth / tempRange;
      count = (int)Math.Ceiling(below0);
      Console.WriteLine("Temp count starts at: " + count);
      increase = startGap + ((count - minTemperature.amount)/ tempRange * measurementWidth);

      if (count % 10 == 0) {
        temperaturePath.AddLines(new CGPoint[] {
          new CGPoint(increase, .5 * sViewHeight),

          new CGPoint(increase, .75 * sViewHeight)
        });
        DrawTemperatureNumber(count, increase);
      } else {
        temperaturePath.AddLines(new CGPoint[] {
          new CGPoint(increase, .5 * sViewHeight),

          new CGPoint(increase, .63 * sViewHeight)
        });
      }
      increase += tempTicks;
      count++;

      for (; count <= above0; count++) {        
        if (count % 10 == 0) {
          temperaturePath.AddLines(new CGPoint[] {
            new CGPoint(increase, .5 * sViewHeight),

            new CGPoint(increase, .75 * sViewHeight)
          });
          DrawTemperatureNumber(count, increase);
        } else {
          temperaturePath.AddLines(new CGPoint[] {
            new CGPoint(increase, .5 * sViewHeight),

            new CGPoint(increase, .63 * sViewHeight)
          });
        }
        increase += tempTicks;
      }

      gctx.AddPath (pressurePath);
      gctx.DrawPath (CGPathDrawingMode.FillStroke);
      UIColor.Red.SetStroke ();
      gctx.AddPath (temperaturePath);
      gctx.DrawPath (CGPathDrawingMode.FillStroke);
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

        CGRect erect = new CGRect(increase -15 , (.63 * sViewHeight) + 20, 30, 20);

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

