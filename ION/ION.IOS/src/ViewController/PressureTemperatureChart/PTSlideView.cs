using System;
using UIKit;
using CoreGraphics;
using Foundation;
using CoreText;

using ION.Core.Fluids;
using ION.Core.Measure;

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
    public int firstMeasurements;
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
    public double maxPressure;
    public double minTemperature;
    public double maxTemperature;

    public PTSlideView(PTChart chart, UIScrollView scrollView, Unit pUnit, Unit tUnit) {
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

      //var minPressure = Math.Floor(ptChart.fluid.GetMinimumPressure(ptChart.state).ConvertTo(lookup).amount);
      var minTemperature = ptChart.fluid.GetMinimumTemperature();
      Console.WriteLine("Pulled min temperature: " + minTemperature);
      maxPressure = ptChart.fluid.GetMaximumPressure(ptChart.state).ConvertTo(lookup).amount;
      maxPressure = maxPressure - (maxPressure % 5);
      var maxTemperature = ptChart.fluid.GetMaximumTemperature();
      Console.WriteLine("Pulled max temperature: " + maxTemperature);
      startGap = sViewWidth / 2.0;
      var measurementWidth = this.Frame.Width - sViewWidth;
      var minPressure = ptChart.fluid.GetMinimumPressure(ptChart.state);
      Console.WriteLine("Min Pressure: " + minPressure.ConvertTo(lookup).amount + lookup + " Max Pressure: " + maxPressure);

      first15 = (measurementWidth * .15);
      Console.WriteLine("First area is " + first15 + " pixels wide");
      second15 = (measurementWidth * .15);
      Console.WriteLine("Second area is " + second15 + " pixels wide");
      middle25 = (measurementWidth * .25);
      Console.WriteLine("Third area is " + middle25 + " pixels wide");
      last45 = (measurementWidth * .45);
      Console.WriteLine("Last area is " + last45 + " pixels wide");
      firstMeasurements = setPressureRange(lookup,1);
      firstTicks = first15 / firstMeasurements;
      Console.WriteLine("First tick width: " + firstTicks);
      firstEnd = first15 + startGap;

      secondMeasurements = setPressureRange(lookup,2);
      secondTicks = second15 / (secondMeasurements - firstMeasurements);
      secondEnd = second15 + firstEnd;
      Console.WriteLine("Second tick width: " + secondTicks);
      middleMeasurements = setPressureRange(lookup,3);
      thirdTicks = middle25 / (middleMeasurements - secondMeasurements);
      middleEnd = middle25 + secondEnd;
      Console.WriteLine("Third tick width: " + thirdTicks);
      lastTicks = last45 / ((maxPressure - middleMeasurements));
      lastEnd = last45 + middleEnd;
      Console.WriteLine("Fourth tick width: " + lastTicks);
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

      var increase = startGap;

      var count = 0;

      for(; count < firstMeasurements; count++){
        
        if(count % firstMod == 0){          
          pressurePath.AddLines(new CGPoint[] {
            new CGPoint(increase, .5 * sViewHeight), 

            new CGPoint(increase, .25 * this.Frame.Height)
          });
          DrawPressureNumber(count, increase);
          Console.WriteLine("first section "+ count + lookup + "="+ptChart.GetTemperature(new Scalar(lookup,count),true).ConvertTo(tempLookup) + tempLookup);
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
          Console.WriteLine("secnd section "+ count + lookup + "="+ptChart.GetTemperature(new Scalar(lookup,count),true).ConvertTo(tempLookup) + tempLookup);
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
          Console.WriteLine("Third section "+ count + lookup + "="+ptChart.GetTemperature(new Scalar(lookup,count),true).ConvertTo(tempLookup) + tempLookup);
        } else if (count % secondMod == 0){
          pressurePath.AddLines(new CGPoint[] {
            new CGPoint(increase, .5 * sViewHeight),

            new CGPoint(increase, .35 * sViewHeight)
          });
        }
        increase += thirdTicks;
      }

      for (; count < maxPressure;count++) {
        
        if (count % fourthMod == 0) {
          pressurePath.AddLines(new CGPoint[] {
            new CGPoint(increase, .5 * sViewHeight),

            new CGPoint(increase, .25 * sViewHeight)
          });
          DrawPressureNumber(count, increase);
          Console.WriteLine("fourth section "+ count + lookup + "="+ptChart.GetTemperature(new Scalar(lookup,count),true).ConvertTo(tempLookup) + tempLookup);
        } 
        else if(count % thirdMod == 0){
          pressurePath.AddLines(new CGPoint[] {
            new CGPoint(increase, .5 * sViewHeight),

            new CGPoint(increase, .4 * sViewHeight)
          });
        }
        increase += lastTicks;
      }

      //var below0 = Math.Floor(0 - minTemperature.ConvertTo(tempLookup).amount);
      var below0 = ptChart.GetTemperature(new Scalar(lookup,0), true).ConvertTo(tempLookup).amount;
      Console.WriteLine("Lowest temp: " + below0);
      var maxTempPressure = maxPressure;
      var above0 = ptChart.GetTemperature(new Scalar(lookup,maxTempPressure), true).ConvertTo(tempLookup).amount;
      Console.WriteLine("Highest temp: " + above0);

      var endBuffer = 0.0;
      while (System.Double.IsNaN(above0)) {
        maxTempPressure -= 1;
        endBuffer += .5;
        above0 = ptChart.GetTemperature(new Scalar(lookup,maxTempPressure), true).ConvertTo(tempLookup).amount;
        Console.WriteLine("Highest temp: " + above0);
      }
      above0 += endBuffer;

      var tempRange = 0.0;

      if (below0 < 0) {
        Console.WriteLine("total temp range: " + (above0 +(-1 * below0)));
        tempRange = above0 + (-1 * below0);
      } else {
        Console.WriteLine("total temp range: " + (above0 + below0));
        tempRange = above0 + below0;
      }

      var tempTicks = (2 * sViewWidth) / tempRange;
      Console.WriteLine("Each degree is " + tempTicks + " pixels wide");

      count = (int)Math.Floor(below0);
      increase = startGap;

      for (; count < 0; count++) {        
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

      for (int i = 0; i <= above0; i++) {        
        if (i % 10 == 0) {
          temperaturePath.AddLines(new CGPoint[] {
            new CGPoint(increase, .5 * sViewHeight),

            new CGPoint(increase, .75 * sViewHeight)
          });
          DrawTemperatureNumber(i, increase);
        } else if(i % 1 == 0){
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

