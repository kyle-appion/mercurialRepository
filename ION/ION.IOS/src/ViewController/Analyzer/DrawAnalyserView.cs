using System;
using UIKit;
using System.Drawing;
using CoreGraphics;
using Foundation;

namespace ION.IOS.ViewController.Analyzer
{

	[Register("DrawAnalyserView")]
	public class DrawAnalyserView : UIView
	{
		CGPath leftPath;
		CGPath rightPath;

		public DrawAnalyserView (){}
		public DrawAnalyserView(IntPtr handle) : base(handle) {}
			
		public override void Draw (CGRect rect)
		{
			base.Draw (rect);

			//get graphics context
			CGContext gctx = UIGraphics.GetCurrentContext();

			//set up drawing attributes
      //gctx.SetLineWidth (10);
      gctx.SetLineWidth (.031f * rect.Width);
			UIColor.Blue.SetStroke ();
			gctx.SetLineCap (CGLineCap.Round);

			//create geometry
      //based on 320 x 568 iphone
			leftPath = new CGPath ();
			rightPath = new CGPath ();
			///MIDDLE -> LEFT
			leftPath.AddLines (new CGPoint[] {

        //new CGPoint (.5 * rect.Width, .184 * rect.Height),
        new CGPoint (.5 * rect.Width, .115 * rect.Height),

        //new CGPoint (.062 * rect.Width, .184 * rect.Height)
        new CGPoint (.062 * rect.Width, .115 * rect.Height)
			});
			///LEFT -> DOWN
			leftPath.AddLines (new CGPoint[] {

        //new CGPoint (.062 * rect.Width, .190 * rect.Height),
        new CGPoint (.062 * rect.Width, .121 * rect.Height),

        //new CGPoint (.062 * rect.Width, .285 * rect.Height)
        new CGPoint (.062 * rect.Width, .216 * rect.Height)
			});
			///LEFT -> RIGHT
			leftPath.AddLines (new CGPoint [] {

        //new CGPoint (.062 * rect.Width, .285 * rect.Height),
        new CGPoint (.062 * rect.Width, .216 * rect.Height),

        //new CGPoint (.281 * rect.Width, .285 * rect.Height)
        new CGPoint (.281 * rect.Width, .216 * rect.Height)
			});
			///RIGHT -> DOWN
			leftPath.AddLines (new CGPoint[] {

        //new CGPoint (.281 * rect.Width, .285 * rect.Height),
        new CGPoint (.281 * rect.Width, .216 * rect.Height),

        //new CGPoint (.281 * rect.Width, .332 * rect.Height)
        new CGPoint (.281 * rect.Width, .263 * rect.Height)
			});
			///RIGHT -> LEFT
			leftPath.AddLines (new CGPoint[] {

        new CGPoint (.281 * rect.Width, .263 * rect.Height),

        new CGPoint (.062 * rect.Width, .263 * rect.Height)
			});
			///LEFT -> DOWN
			leftPath.AddLines (new CGPoint [] {

        //new CGPoint (.062 * rect.Width, .332 * rect.Height),
        new CGPoint (.062 * rect.Width, .263 * rect.Height),

        //new CGPoint (.062 * rect.Width, .375 * rect.Height)
        new CGPoint (.062 * rect.Width, .306 * rect.Height)
			});
			///LEFT -> RIGHT
			leftPath.AddLines (new CGPoint [] {

        //new CGPoint (.062 * rect.Width, .375 * rect.Height),
        new CGPoint (.062 * rect.Width, .306 * rect.Height),

        //new CGPoint (.281 * rect.Width, .375 * rect.Height)
        new CGPoint (.281 * rect.Width, .306 * rect.Height)
			});
			///RIGHT -> DOWN
			leftPath.AddLines (new CGPoint [] {

        //new CGPoint (.281 * rect.Width, .375 * rect.Height),
        new CGPoint (.281 * rect.Width, .306 * rect.Height),

        //new CGPoint (.281 * rect.Width, .422 * rect.Height)
        new CGPoint (.281 * rect.Width, .353 * rect.Height)
			});
			///RIGHT -> LEFT
			leftPath.AddLines (new CGPoint [] {

        //new CGPoint (.281 * rect.Width, .422 * rect.Height),
        new CGPoint (.281 * rect.Width, .353 * rect.Height),

        //new CGPoint (.062 * rect.Width, .422 * rect.Height)
        new CGPoint (.062 * rect.Width, .353 * rect.Height)
			});
			///LEFT -> DOWN
			leftPath.AddLines (new CGPoint [] {

        //new CGPoint (.062 * rect.Width, .422 * rect.Height),
        new CGPoint (.062 * rect.Width, .353 * rect.Height),

        //new CGPoint (.062 * rect.Width, .524 * rect.Height)
        new CGPoint (.062 * rect.Width, .458 * rect.Height)
			});
			///LEFT -> MIDDLE
			leftPath.AddLines (new CGPoint [] {

        //new CGPoint (.062 * rect.Width, .524 * rect.Height),
        new CGPoint (.062 * rect.Width, .458 * rect.Height),

        //new CGPoint (.5 * rect.Width, .524 * rect.Height)
        new CGPoint (.5 * rect.Width, .458 * rect.Height)
			});
			leftPath.CloseSubpath ();

			//add geometry to graphics context and draw it
			gctx.AddPath (leftPath);     
			gctx.DrawPath (CGPathDrawingMode.FillStroke);

			UIColor.Red.SetStroke ();
			///RIGHT HALF OF ANALYSER DIAGRAM
			rightPath.AddLines (new CGPoint [] {

        //new CGPoint (.5 * rect.Width, .184 * rect.Height),
        new CGPoint (.5 * rect.Width, .115 * rect.Height),
  
        //new CGPoint (.937 * rect.Width, .184 * rect.Height)
        new CGPoint (.937 * rect.Width, .115 * rect.Height)
			});
			rightPath.AddLines (new CGPoint [] {

        //new CGPoint (.937 * rect.Width, .190 * rect.Height),
        new CGPoint (.937 * rect.Width, .121 * rect.Height),

       //new CGPoint (.937 * rect.Width, .285 * rect.Height)
        new CGPoint (.937 * rect.Width, .216 * rect.Height)
			});
			rightPath.AddLines (new CGPoint [] {

        //new CGPoint (.937 * rect.Width, .285 * rect.Height),
        new CGPoint (.937 * rect.Width, .216 * rect.Height),

        //new CGPoint (.718 * rect.Width, .285 * rect.Height)
        new CGPoint (.718 * rect.Width, .216 * rect.Height)
			});
			rightPath.AddLines (new CGPoint [] {

        //new CGPoint (.718 * rect.Width, .285 * rect.Height),
        new CGPoint (.718 * rect.Width, .216 * rect.Height),

        //new CGPoint (.718 * rect.Width, .332 * rect.Height)
        new CGPoint (.718 * rect.Width, .263 * rect.Height)
			});
			rightPath.AddLines (new CGPoint[] {

        //new CGPoint (.718 * rect.Width, .332 * rect.Height),
        new CGPoint (.718 * rect.Width, .263 * rect.Height),

        //new CGPoint (.937 * rect.Width, .332 * rect.Height)
        new CGPoint (.937 * rect.Width, .263 * rect.Height)
			});
			rightPath.AddLines (new CGPoint [] {

        //new CGPoint (.937 * rect.Width, .332 * rect.Height),
        new CGPoint (.937 * rect.Width, .263 * rect.Height),

        //new CGPoint (.937 * rect.Width, .375 * rect.Height)
        new CGPoint (.937 * rect.Width, .306 * rect.Height)
			});
			rightPath.AddLines (new CGPoint [] {

        //new CGPoint (.937 * rect.Width, .375 * rect.Height),
        new CGPoint (.937 * rect.Width, .306 * rect.Height),

        //new CGPoint (.718 * rect.Width, .375 * rect.Height)
        new CGPoint (.718 * rect.Width, .306 * rect.Height)
			});
			rightPath.AddLines (new CGPoint [] {
  
        //new CGPoint (.718 * rect.Width, .375 * rect.Height),
        new CGPoint (.718 * rect.Width, .306 * rect.Height),
     
        //new CGPoint (.718 * rect.Width, .422 * rect.Height)
        new CGPoint (.718 * rect.Width, .353 * rect.Height)
			});
			rightPath.AddLines (new CGPoint [] {
       
        //new CGPoint (.718 * rect.Width, .422 * rect.Height),
        new CGPoint (.718 * rect.Width, .353 * rect.Height),
       
        //new CGPoint (.937 * rect.Width, .422 * rect.Height)
        new CGPoint (.937 * rect.Width, .353 * rect.Height)
			});
			rightPath.AddLines (new CGPoint [] {
       
        //new CGPoint (.937 * rect.Width, .422 * rect.Height),
        new CGPoint (.937 * rect.Width, .353 * rect.Height),
       
        //new CGPoint (.937 * rect.Width, .524 * rect.Height)
        new CGPoint (.937 * rect.Width, .458 * rect.Height)
			});
			rightPath.AddLines (new CGPoint [] {
        
        //new CGPoint (.937 * rect.Width, .524 * rect.Height),
        new CGPoint (.937 * rect.Width, .458 * rect.Height),
       
        //new CGPoint (.5 * rect.Width, .524 * rect.Height)
        new CGPoint (.5 * rect.Width, .458 * rect.Height)
			});
			gctx.AddPath (rightPath);     
			gctx.DrawPath (CGPathDrawingMode.FillStroke);


		}
	}
}

