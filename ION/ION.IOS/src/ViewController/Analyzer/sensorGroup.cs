using System;
using System.Collections.Generic;
using UIKit;
using CoreGraphics;
namespace ION.IOS.ViewController.Analyzer
{
	public class sensorGroup
	{
		public sensorGroup ()
		{
		}

		public sensor snapArea1 = new sensor();
		public sensor snapArea2 = new sensor();
		public sensor snapArea3 = new sensor();
		public sensor snapArea4 = new sensor();
		public sensor snapArea5 = new sensor();
		public sensor snapArea6 = new sensor();
		public sensor snapArea7 = new sensor();
		public sensor snapArea8 = new sensor();
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
		public List<CGPoint> locationList = new List<CGPoint>(8);
		public List<int> areaList = new List<int> (){1,2,3,4,5,6,7,8};
		public List<sensor> viewList = new List<sensor>();
	}


}

