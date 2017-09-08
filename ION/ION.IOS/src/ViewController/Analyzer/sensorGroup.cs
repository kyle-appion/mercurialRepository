using System;
using System.Collections.Generic;
using UIKit;
using CoreGraphics;
using ION.Core.App;
using ION.Core.Sensors;

namespace ION.IOS.ViewController.Analyzer
{
	public class sensorGroup
	{

		public sensor snapArea1;
		public sensor snapArea2;
		public sensor snapArea3;
		public sensor snapArea4;
		public sensor snapArea5;
		public sensor snapArea6;
		public sensor snapArea7;
		public sensor snapArea8;
		public CGRect snapRect1;
		public CGRect snapRect2;
		public CGRect snapRect3;
		public CGRect snapRect4;
		public CGRect snapRect5;
		public CGRect snapRect6;
		public CGRect snapRect7;
		public CGRect snapRect8;
		public CGPoint snapPoint1;
		public CGPoint snapPoint2;
		public CGPoint snapPoint3;
		public CGPoint snapPoint4;
		public CGPoint snapPoint5;
		public CGPoint snapPoint6;
		public CGPoint snapPoint7;
		public CGPoint snapPoint8;
		public UISnapBehavior snap;
		public UIDynamicAnimator animator;
    public LowHighArea lowHighSensors;
		public List<CGPoint> locationList = new List<CGPoint>(8);
		public List<sensor> viewList = new List<sensor>();

    IION ion;

    public sensorGroup (UIView mainView, AnalyzerViewController ViewController)
		{
      ion = AppState.context;

			////CREATE STATIC SENSOR LOCATIONS 
			/// LEFT SIDE 
			/// 4  3
			/// 2  1
			snapRect1 = new CGRect(.25 * mainView.Bounds.Width, .304 * mainView.Bounds.Height, .203 * mainView.Bounds.Width, .114 * mainView.Bounds.Height);
			snapRect2 = new CGRect(.024 * mainView.Bounds.Width, .304 * mainView.Bounds.Height, .203 * mainView.Bounds.Width, .114 * mainView.Bounds.Height);
			snapRect3 = new CGRect(.25 * mainView.Bounds.Width, .012 * mainView.Bounds.Height, .203 * mainView.Bounds.Width, .114 * mainView.Bounds.Height);
			snapRect4 = new CGRect(.024 * mainView.Bounds.Width, .012 * mainView.Bounds.Height, .203 * mainView.Bounds.Width, .114 * mainView.Bounds.Height);
			/// RIGHT SIDE
			/// 7  8
			/// 5  6
			snapRect5 = new CGRect(.546 * mainView.Bounds.Width, .304 * mainView.Bounds.Height, .203 * mainView.Bounds.Width, .114 * mainView.Bounds.Height);
			snapRect6 = new CGRect(.769 * mainView.Bounds.Width, .304 * mainView.Bounds.Height, .203 * mainView.Bounds.Width, .114 * mainView.Bounds.Height);
			snapRect7 = new CGRect(.546 * mainView.Bounds.Width, .012 * mainView.Bounds.Height, .203 * mainView.Bounds.Width, .114 * mainView.Bounds.Height);
			snapRect8 = new CGRect(.769 * mainView.Bounds.Width, .012 * mainView.Bounds.Height, .203 * mainView.Bounds.Width, .114 * mainView.Bounds.Height);

			////CREATE CONSTANT POINT OF ORIGIN FOR EACH AREA
			/// LEFT SIDE
			snapPoint1 = new CGPoint(snapRect1.GetMidX(), snapRect1.GetMidY());
			snapPoint2 = new CGPoint(snapRect2.GetMidX(), snapRect2.GetMidY());
			snapPoint3 = new CGPoint(snapRect3.GetMidX(), snapRect3.GetMidY());
			snapPoint4 = new CGPoint(snapRect4.GetMidX(), snapRect4.GetMidY());
			///RIGHT SIDE
			snapPoint5 = new CGPoint(snapRect5.GetMidX(), snapRect5.GetMidY());
			snapPoint6 = new CGPoint(snapRect6.GetMidX(), snapRect6.GetMidY());
			snapPoint7 = new CGPoint(snapRect7.GetMidX(), snapRect7.GetMidY());
			snapPoint8 = new CGPoint(snapRect8.GetMidX(), snapRect8.GetMidY());

			snapArea1 = new sensor(snapRect1, "1", mainView);
			viewList.Add(snapArea1);
      AddSensorGestures(snapArea1,mainView);
			snapArea2 = new sensor(snapRect2, "2", mainView);
			viewList.Add(snapArea2);
			AddSensorGestures(snapArea2, mainView);
			snapArea3 = new sensor(snapRect3, "3", mainView);
			viewList.Add(snapArea3);
			AddSensorGestures(snapArea3, mainView);
			snapArea4 = new sensor(snapRect4, "4", mainView);
			viewList.Add(snapArea4);
			AddSensorGestures(snapArea4, mainView);
			snapArea5 = new sensor(snapRect5, "5", mainView);
			viewList.Add(snapArea5);
			AddSensorGestures(snapArea5, mainView);
			snapArea6 = new sensor(snapRect6, "6", mainView);
			viewList.Add(snapArea6);
			AddSensorGestures(snapArea6, mainView);
			snapArea7 = new sensor(snapRect7, "7", mainView);
			viewList.Add(snapArea7);
			AddSensorGestures(snapArea7, mainView);
			snapArea8 = new sensor(snapRect8, "8", mainView);
			viewList.Add(snapArea8);
			AddSensorGestures(snapArea8, mainView);

			///STORE POINT LOCATIONS AND INITIALIZE SENSOR ORDER
			locationList.Add(snapPoint1);
			locationList.Add(snapPoint2);
			locationList.Add(snapPoint3);
			locationList.Add(snapPoint4);
			locationList.Add(snapPoint5);
			locationList.Add(snapPoint6);
			locationList.Add(snapPoint7);
			locationList.Add(snapPoint8);
		}

		/// <summary>
		/// CREATE GESTURE RECOGNIZERS FOR DRAG/DROP AND LONG PRESS
		/// </summary>
		void AddSensorGestures(sensor Sensor, UIView mainView) {
			////CREATE OFFSETS TO FOLLOW EACH SUBVIEW AS IT MOVES

			nfloat dx = 0;
			nfloat dy = 0;  

			Sensor.panGesture = new UIPanGestureRecognizer(() => {
				if (Sensor.panGesture.State == UIGestureRecognizerState.Began) {
					//View.BringSubviewToFront(Sensor.snapArea);
					mainView.BringSubviewToFront(Sensor.snapArea);
				}
				if ((Sensor.panGesture.State == UIGestureRecognizerState.Began || Sensor.panGesture.State == UIGestureRecognizerState.Changed) && (Sensor.panGesture.NumberOfTouches == 1)) {

					// remove any previosuly applied snap behavior to avoid a flicker that will occur if both the gesture and physics are operating on the view simultaneously
					if (snap != null)
						animator.RemoveBehavior(snap);

					//var p0 = Sensor.panGesture.LocationInView (View);
					var p0 = Sensor.panGesture.LocationInView(mainView);

					if (dx == 0) {
						dx = p0.X - Sensor.snapArea.Center.X;
					}

					if (dy == 0) {
						dy = p0.Y - Sensor.snapArea.Center.Y;
					}

					// this is where the offsets are applied so that the location of the image follows the point where the image is touched as it is dragged,
					// otherwise the center of the image would snap to the touch point at the start of the pan gesture

					var p1 = new CGPoint(p0.X - dx, p0.Y - dy);

					Sensor.snapArea.Center = p1;


				} else if (Sensor.panGesture.State == UIGestureRecognizerState.Ended) {
					//View.SendSubviewToBack(Sensor.snapArea);
					mainView.SendSubviewToBack(Sensor.snapArea);
					// reset offsets when dragging ends so that they will be recalculated for next touch and drag that occurs
					dx = 0;
					dy = 0;

					/// CHECK IF SENSOR WAS DROPPED IN LOW OR HIGH SECTION

					AnalyserUtilities.updateLowHighArea(Sensor.panGesture.LocationInView(mainView), Sensor, lowHighSensors, this, mainView);

					////FIGURE OUT WHERE TO SNAP THE SUBVIEW BASED ON IT'S LOCATION AND IDENTIFIER
					AnalyserUtilities.LHSwapCheck(this, lowHighSensors, Convert.ToInt32(Sensor.snapArea.AccessibilityIdentifier), Sensor.panGesture.LocationInView(mainView), mainView);

				} else if (Sensor.panGesture.State == UIGestureRecognizerState.Failed) {
					Console.WriteLine("Touch has failed to be recognized for " + Sensor.snapArea.AccessibilityIdentifier + " area");
				}
			});

			Sensor.holdGesture = new UILongPressGestureRecognizer(() => {
				if (Sensor.holdGesture.State == UIGestureRecognizerState.Began) {

				}
			});

		}

    public sensor GetSlotFromSensor(Sensor sensor){
      for (int i = 0; i < viewList.Count; i++){
        if(viewList[i].currentSensor != null && viewList[i].currentSensor == sensor){
          return viewList[i];
        }
      }

      return null;
    }
  }
}

