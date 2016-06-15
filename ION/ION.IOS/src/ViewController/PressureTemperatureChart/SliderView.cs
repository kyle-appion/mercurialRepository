using System;
using System.Threading.Tasks;
using System.Drawing;
using CoreGraphics;
using UIKit;
using Foundation; 

using ION.Core.Fluids;
using ION.Core.Measure;
using ION.Core.Sensors;
using ION.Core.Devices;

namespace ION.IOS.ViewController.PressureTemperatureChart {
  public class SliderView {

	public PTSlideView ptView;
	public UIScrollView ptScroller;
	public UILabel sliderLabel;
	
	internal PTChartViewController.SensorEntryMode entryMode{
		get{return __entryMode;}
		set{
			if(__entryMode != null){
				__entryMode.sensor.onSensorStateChangedEvent -= setOffsetFromSensorMeasurement;
			}
			__entryMode = value;
			
			if(__entryMode != null){
				__entryMode.sensor.onSensorStateChangedEvent += setOffsetFromSensorMeasurement;
			}
		}
	} PTChartViewController.SensorEntryMode __entryMode;
	
	public PTChart Chart;
	public Unit pUnit;
	public Unit tUnit;

    internal SliderView(UIView View,PTChart ptChart, Unit pressureUnit, Unit temperatureUnit, Sensor temperatureSensor, PTChartViewController.SensorEntryMode sensorEntry) {
    	Chart = ptChart;
    	pUnit = pressureUnit;
    	tUnit = temperatureUnit;
    	
       	sliderLabel = new UILabel(new CGRect(.025 * View.Bounds.Width,290,.95 * View.Bounds.Width,20));
       	sliderLabel.AdjustsFontSizeToFitWidth = true;
       	sliderLabel.TextAlignment = UITextAlignment.Center;
       	sliderLabel.Text = "Saturated Pressure and Temperature";
		
		ptScroller = new UIScrollView(new CGRect(.025 * View.Bounds.Width,320, .95 * View.Bounds.Width, 128));
		ptScroller.ShowsHorizontalScrollIndicator = false;
		ptScroller.ContentSize = new CGSize(2188.8,ptScroller.Bounds.Height);
		ptScroller.AutoresizingMask = UIViewAutoresizing.FlexibleWidth;
		ptScroller.Layer.CornerRadius = 5;
		ptScroller.Layer.BorderWidth = 1f;
		ptScroller.BackgroundColor = UIColor.White;

	    ptView = new PTSlideView (ptChart, ptScroller, pressureUnit, temperatureUnit, temperatureSensor);
		ptView.BackgroundColor = UIColor.White;
		
		entryMode = sensorEntry;
		
	  ptScroller.AddSubview (ptView);
      RedrawMeasurements(ptChart);
    }
    
    public async void setOffsetFromSensorMeasurement(Sensor sensor){
    	await Task.Delay(TimeSpan.FromMilliseconds(5)); 
		if(sensor is GaugeDeviceSensor){
			if(sensor.type == ESensorType.Pressure){
				if(sensor.measurement.amount >= ptView.minPressure && sensor.measurement.amount <= ptView.maxPressure.amount){
					if(ptView.lookup != sensor.unit){
						ptView.resetData(Chart,sensor.unit,ptView.tempLookup);
						await Task.Delay(TimeSpan.FromMilliseconds(2));
					}
					var convertedTemperature = Chart.GetTemperature(new Scalar(sensor.unit,sensor.measurement.amount),sensor.isRelative).ConvertTo(ptView.tempLookup);
					var tempOffset = (convertedTemperature.amount - ptView.minTemperature.amount) * ptView.tempTicks;
					ptScroller.SetContentOffset(new CGPoint(tempOffset,0),true);
				}
			} else if (sensor.type == ESensorType.Temperature){
				if(sensor.measurement.amount >= ptView.minTemperature.amount && sensor.measurement.amount <= ptView.maxTemperature){
					if(ptView.tempLookup != sensor.unit){
						ptView.resetData(Chart,ptView.lookup,sensor.unit);
						await Task.Delay(TimeSpan.FromMilliseconds(2));
					}
					var tempOffset = (sensor.measurement.amount - ptView.minTemperature.amount) * ptView.tempTicks + ptView.startGap;
					ptScroller.SetContentOffset(new CGPoint(tempOffset,0),true);
				}
			}
		}
	}

    public void RedrawMeasurements(PTChart ptChart){
      ptView.SetNeedsDisplay();
    }
  }
}

