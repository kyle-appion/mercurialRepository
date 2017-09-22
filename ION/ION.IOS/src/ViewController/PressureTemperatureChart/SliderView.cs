using System;
using System.Threading.Tasks;
using System.Drawing;
using CoreGraphics;
using UIKit;
using Foundation; 

using Appion.Commons.Measure;

using ION.Core.Fluids;
using ION.Core.Sensors;
using ION.Core.Devices;
using ION.Core.App;

namespace ION.IOS.ViewController.PressureTemperatureChart {
  public class SliderView {

		public PTSlideView ptView;
		public UIScrollView ptScroller;
		public UILabel sliderLabel;
		
		internal PTChartViewController.SensorEntryMode entryMode{
			get{return __entryMode;}
			set{
				if(__entryMode != null){
					__entryMode.sensor.onSensorEvent -= setOffsetFromSensorMeasurement;
				}
				__entryMode = value;
				
				if(__entryMode != null){
					__entryMode.sensor.onSensorEvent += setOffsetFromSensorMeasurement;
				}
			}
		} PTChartViewController.SensorEntryMode __entryMode;
		

		public Unit pUnit;
		public Unit tUnit;

	  internal SliderView(UIView View, Unit pressureUnit, Unit temperatureUnit, Sensor temperatureSensor, PTChartViewController.SensorEntryMode sensorEntry) {

	  	pUnit = pressureUnit;
	  	tUnit = temperatureUnit;
	  	
	   	sliderLabel = new UILabel(new CGRect(.025 * View.Bounds.Width,290,.95 * View.Bounds.Width,20));
	   	sliderLabel.AdjustsFontSizeToFitWidth = true;
	   	sliderLabel.TextAlignment = UITextAlignment.Center;
	   	sliderLabel.Text = Util.Strings.Fluid.SLIDERHEADING;
		
			ptScroller = new UIScrollView(new CGRect(.025 * View.Bounds.Width,320, .95 * View.Bounds.Width, 128));
			ptScroller.ShowsHorizontalScrollIndicator = false;
			ptScroller.ContentSize = new CGSize(2188.8,ptScroller.Bounds.Height);
			ptScroller.AutoresizingMask = UIViewAutoresizing.FlexibleWidth;
			ptScroller.Layer.CornerRadius = 5;
			ptScroller.Layer.BorderWidth = 1f;
			ptScroller.BackgroundColor = UIColor.White;
	
		  ptView = new PTSlideView (ptScroller, pressureUnit, temperatureUnit, temperatureSensor);
			ptView.BackgroundColor = UIColor.White;
			
			entryMode = sensorEntry;
			
		  ptScroller.AddSubview (ptView);
	    RedrawMeasurements();
	  }
    
    public async void setOffsetFromSensorMeasurement(SensorEvent sensorEvent){
    	await Task.Delay(TimeSpan.FromMilliseconds(5)); 
			if(sensorEvent.sensor is GaugeDeviceSensor){
				if(sensorEvent.sensor.type == ESensorType.Pressure){
					if(sensorEvent.sensor.measurement.amount >= ptView.minPressure && sensorEvent.sensor.measurement.amount <= ptView.maxPressure.amount){
						if(ptView.lookup != sensorEvent.sensor.unit){
							ptView.resetData(sensorEvent.sensor.unit,ptView.tempLookup);
							await Task.Delay(TimeSpan.FromMilliseconds(2));
						}					
						var convertedTemperature = AppState.context.fluidManager.lastUsedFluid.GetSaturatedTemperature(sensorEvent.sensor.fluidState,new Scalar(sensorEvent.sensor.unit,sensorEvent.sensor.measurement.amount), AppState.context.locationManager.lastKnownLocation.altitude).ConvertTo(ptView.tempLookup);
						var tempOffset = (convertedTemperature.amount - ptView.minTemperature.amount) * ptView.tempTicks;
						ptScroller.SetContentOffset(new CGPoint(tempOffset,0),true);
					}
				 } else if (sensorEvent.sensor.type == ESensorType.Temperature){
					if(sensorEvent.sensor.measurement.amount >= ptView.minTemperature.amount && sensorEvent.sensor.measurement.amount <= ptView.maxTemperature){
						if(ptView.tempLookup != sensorEvent.sensor.unit){
							ptView.resetData(ptView.lookup,sensorEvent.sensor.unit);
							await Task.Delay(TimeSpan.FromMilliseconds(2));
						}
						var tempOffset = (sensorEvent.sensor.measurement.amount - ptView.minTemperature.amount) * ptView.tempTicks + ptView.startGap;
						ptScroller.SetContentOffset(new CGPoint(tempOffset,0),true);
					}
				 }
			 }
	 	}

    public void RedrawMeasurements(){
      ptView.SetNeedsDisplay();
    }
  }
}

