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
		public CGRect deviceRect;

		public DrawAnalyserView (){}
		public DrawAnalyserView(IntPtr handle) : base(handle) {}
			
		public override void Draw (CGRect rect)
		{			
			if(deviceRect.Width == 0){
				deviceRect = rect;
			}
			rect = deviceRect;
		
			base.Draw (rect);
			
			if(rect.Height < 550){
				smallDraw(rect);
			} else if (rect.Height < 900){
				mediumDraw(rect);
			} else {
				largeDraw(rect);
			}
		}
		
		public void smallDraw(CGRect rect){
			//get graphics context
			CGContext gctx = UIGraphics.GetCurrentContext();

			//set up drawing attributes
      gctx.SetLineWidth (.031f * rect.Width);
			UIColor.Blue.SetStroke ();
			gctx.SetLineCap (CGLineCap.Round);

			//create geometry
      //based on 320 x 568 iphone
			leftPath = new CGPath ();
			rightPath = new CGPath ();
			///MIDDLE -> LEFT
			leftPath.AddLines (new CGPoint[] {

        new CGPoint (.5 * rect.Width, .05 * rect.Height),
        new CGPoint (.062 * rect.Width, .05 * rect.Height)
			});
			///LEFT -> DOWN
			leftPath.AddLines (new CGPoint[] {

        new CGPoint (.062 * rect.Width, .05 * rect.Height),
        new CGPoint (.062 * rect.Width, .18 * rect.Height)
			});
			///LEFT -> RIGHT
			leftPath.AddLines (new CGPoint [] {

        new CGPoint (.062 * rect.Width, .18 * rect.Height),
        new CGPoint (.281 * rect.Width, .18 * rect.Height)
			});
			///RIGHT -> DOWN
			leftPath.AddLines (new CGPoint[] {

        new CGPoint (.281 * rect.Width, .18 * rect.Height),
        new CGPoint (.281 * rect.Width, .227 * rect.Height)
			});
			///RIGHT -> LEFT
			leftPath.AddLines (new CGPoint[] {

        new CGPoint (.281 * rect.Width, .227 * rect.Height),
        new CGPoint (.062 * rect.Width, .227 * rect.Height)
			});
			///LEFT -> DOWN
			leftPath.AddLines (new CGPoint [] {

        new CGPoint (.062 * rect.Width, .227 * rect.Height),
        new CGPoint (.062 * rect.Width, .27 * rect.Height)
			});
			///LEFT -> RIGHT
			leftPath.AddLines (new CGPoint [] {

        new CGPoint (.062 * rect.Width, .27 * rect.Height),
        new CGPoint (.281 * rect.Width, .27 * rect.Height)
			});
			///RIGHT -> DOWN
			leftPath.AddLines (new CGPoint [] {

        new CGPoint (.281 * rect.Width, .27 * rect.Height),
        new CGPoint (.281 * rect.Width, .317 * rect.Height)
			});
			///RIGHT -> LEFT
			leftPath.AddLines (new CGPoint [] {

        new CGPoint (.281 * rect.Width, .317 * rect.Height),
        new CGPoint (.062 * rect.Width, .317 * rect.Height)
			});
			///LEFT -> DOWN
			leftPath.AddLines (new CGPoint [] {

        new CGPoint (.062 * rect.Width, .317 * rect.Height),
        new CGPoint (.062 * rect.Width, .44 * rect.Height)
			});
			///LEFT -> MIDDLE
			leftPath.AddLines (new CGPoint [] {

        new CGPoint (.062 * rect.Width, .44 * rect.Height),
        new CGPoint (.5 * rect.Width, .44 * rect.Height)
			});
			leftPath.CloseSubpath ();

			//add geometry to graphics context and draw it
			gctx.AddPath (leftPath);     
			gctx.DrawPath (CGPathDrawingMode.FillStroke);

			UIColor.Red.SetStroke ();
			///RIGHT HALF OF ANALYSER DIAGRAM
			rightPath.AddLines (new CGPoint [] {

        new CGPoint (.5 * rect.Width, .05 * rect.Height),  
        new CGPoint (.937 * rect.Width, .05 * rect.Height)
			});
			rightPath.AddLines (new CGPoint [] {

        new CGPoint (.937 * rect.Width, .05 * rect.Height),
        new CGPoint (.937 * rect.Width, .18 * rect.Height)
			});
			rightPath.AddLines (new CGPoint [] {

        new CGPoint (.937 * rect.Width, .18 * rect.Height),
        new CGPoint (.718 * rect.Width, .18 * rect.Height)
			});
			rightPath.AddLines (new CGPoint [] {

        new CGPoint (.718 * rect.Width, .18 * rect.Height),
        new CGPoint (.718 * rect.Width, .227 * rect.Height)
			});
			rightPath.AddLines (new CGPoint[] {

        new CGPoint (.718 * rect.Width, .227 * rect.Height),
        new CGPoint (.937 * rect.Width, .227 * rect.Height)
			});
			rightPath.AddLines (new CGPoint [] {

        new CGPoint (.937 * rect.Width, .227 * rect.Height),
        new CGPoint (.937 * rect.Width, .27 * rect.Height)
			});
			rightPath.AddLines (new CGPoint [] {

        new CGPoint (.937 * rect.Width, .27 * rect.Height),
        new CGPoint (.718 * rect.Width, .27 * rect.Height)
			});
			rightPath.AddLines (new CGPoint [] {
  
        new CGPoint (.718 * rect.Width, .27 * rect.Height),     
        new CGPoint (.718 * rect.Width, .317 * rect.Height)
			});
			rightPath.AddLines (new CGPoint [] {
       
        new CGPoint (.718 * rect.Width, .317 * rect.Height),       
        new CGPoint (.937 * rect.Width, .317 * rect.Height)
			});
			rightPath.AddLines (new CGPoint [] {
       
        new CGPoint (.937 * rect.Width, .317 * rect.Height),       
        new CGPoint (.937 * rect.Width, .44 * rect.Height)
			});
			rightPath.AddLines (new CGPoint [] {
        
        new CGPoint (.937 * rect.Width, .44 * rect.Height),       
        new CGPoint (.5 * rect.Width, .44 * rect.Height)
			});
			gctx.AddPath (rightPath);     
			gctx.DrawPath (CGPathDrawingMode.FillStroke);
		}
		
		/// <summary>
		/// Analyzer background for medium iphones
		/// </summary>
		/// <param name="rect">Rect.</param>
		public void mediumDraw(CGRect rect){
			//get graphics context
			CGContext gctx = UIGraphics.GetCurrentContext();

			//set up drawing attributes
      gctx.SetLineWidth (.031f * rect.Width);
			UIColor.Blue.SetStroke ();
			gctx.SetLineCap (CGLineCap.Round);

			//create geometry
      //based on 320 x 568 iphone
			leftPath = new CGPath ();
			rightPath = new CGPath ();
			///MIDDLE -> LEFT
			leftPath.AddLines (new CGPoint[] {

        new CGPoint (.5 * rect.Width, .05 * rect.Height),
        new CGPoint (.062 * rect.Width, .05 * rect.Height)
			});
			///LEFT -> DOWN
			leftPath.AddLines (new CGPoint[] {

        new CGPoint (.062 * rect.Width, .05 * rect.Height),
        new CGPoint (.062 * rect.Width, .17 * rect.Height)
			});
			///LEFT -> RIGHT
			leftPath.AddLines (new CGPoint [] {

        new CGPoint (.062 * rect.Width, .17 * rect.Height),
        new CGPoint (.281 * rect.Width, .17 * rect.Height)
			});
			///RIGHT -> DOWN
			leftPath.AddLines (new CGPoint[] {

        new CGPoint (.281 * rect.Width, .17 * rect.Height),
        new CGPoint (.281 * rect.Width, .217 * rect.Height)
			});
			///RIGHT -> LEFT
			leftPath.AddLines (new CGPoint[] {

        new CGPoint (.281 * rect.Width, .217 * rect.Height),
        new CGPoint (.062 * rect.Width, .217 * rect.Height)
			});
			///LEFT -> DOWN
			leftPath.AddLines (new CGPoint [] {

        new CGPoint (.062 * rect.Width, .217 * rect.Height),
        new CGPoint (.062 * rect.Width, .26 * rect.Height)
			});
			///LEFT -> RIGHT
			leftPath.AddLines (new CGPoint [] {

        new CGPoint (.062 * rect.Width, .26 * rect.Height),
        new CGPoint (.281 * rect.Width, .26 * rect.Height)
			});
			///RIGHT -> DOWN
			leftPath.AddLines (new CGPoint [] {

        new CGPoint (.281 * rect.Width, .26 * rect.Height),
        new CGPoint (.281 * rect.Width, .307 * rect.Height)
			});
			///RIGHT -> LEFT
			leftPath.AddLines (new CGPoint [] {

        new CGPoint (.281 * rect.Width, .307 * rect.Height),
        new CGPoint (.062 * rect.Width, .307 * rect.Height)
			});
			///LEFT -> DOWN
			leftPath.AddLines (new CGPoint [] {

        new CGPoint (.062 * rect.Width, .307 * rect.Height),
        new CGPoint (.062 * rect.Width, .43 * rect.Height)
			});
			///LEFT -> MIDDLE
			leftPath.AddLines (new CGPoint [] {

        new CGPoint (.062 * rect.Width, .43 * rect.Height),
        new CGPoint (.5 * rect.Width, .43 * rect.Height)
			});
			leftPath.CloseSubpath ();

			//add geometry to graphics context and draw it
			gctx.AddPath (leftPath);     
			gctx.DrawPath (CGPathDrawingMode.FillStroke);

			UIColor.Red.SetStroke ();
			///RIGHT HALF OF ANALYSER DIAGRAM
			rightPath.AddLines (new CGPoint [] {

        new CGPoint (.5 * rect.Width, .05 * rect.Height),  
        new CGPoint (.937 * rect.Width, .05 * rect.Height)
			});
			rightPath.AddLines (new CGPoint [] {

        new CGPoint (.937 * rect.Width, .05 * rect.Height),
        new CGPoint (.937 * rect.Width, .17 * rect.Height)
			});
			rightPath.AddLines (new CGPoint [] {

        new CGPoint (.937 * rect.Width, .17 * rect.Height),
        new CGPoint (.718 * rect.Width, .17 * rect.Height)
			});
			rightPath.AddLines (new CGPoint [] {

        new CGPoint (.718 * rect.Width, .17 * rect.Height),
        new CGPoint (.718 * rect.Width, .217 * rect.Height)
			});
			rightPath.AddLines (new CGPoint[] {

        new CGPoint (.718 * rect.Width, .217 * rect.Height),
        new CGPoint (.937 * rect.Width, .217 * rect.Height)
			});
			rightPath.AddLines (new CGPoint [] {

        new CGPoint (.937 * rect.Width, .217 * rect.Height),
        new CGPoint (.937 * rect.Width, .26 * rect.Height)
			});
			rightPath.AddLines (new CGPoint [] {

        new CGPoint (.937 * rect.Width, .26 * rect.Height),
        new CGPoint (.718 * rect.Width, .26 * rect.Height)
			});
			rightPath.AddLines (new CGPoint [] {
  
        new CGPoint (.718 * rect.Width, .26 * rect.Height),     
        new CGPoint (.718 * rect.Width, .307 * rect.Height)
			});
			rightPath.AddLines (new CGPoint [] {
       
        new CGPoint (.718 * rect.Width, .307 * rect.Height),       
        new CGPoint (.937 * rect.Width, .307 * rect.Height)
			});
			rightPath.AddLines (new CGPoint [] {
       
        new CGPoint (.937 * rect.Width, .307 * rect.Height),       
        new CGPoint (.937 * rect.Width, .43 * rect.Height)
			});
			rightPath.AddLines (new CGPoint [] {
        
        new CGPoint (.937 * rect.Width, .43 * rect.Height),       
        new CGPoint (.5 * rect.Width, .43 * rect.Height)
			});
			gctx.AddPath (rightPath);     
			gctx.DrawPath (CGPathDrawingMode.FillStroke);
		}
		
		/// <summary>
		/// Analyzer background for ipad sizes
		/// </summary>
		/// <param name="rect">Rect.</param>
		public void largeDraw(CGRect rect){
			//get graphics context
			CGContext gctx = UIGraphics.GetCurrentContext();

			//set up drawing attributes
      gctx.SetLineWidth (.028f * rect.Width);
			UIColor.Blue.SetStroke ();
			gctx.SetLineCap (CGLineCap.Round);

			//create geometry
			leftPath = new CGPath ();
			rightPath = new CGPath ();
			///MIDDLE -> LEFT
			leftPath.AddLines (new CGPoint[] {
        new CGPoint (.5 * rect.Width, .05 * rect.Height),
        new CGPoint (.062 * rect.Width, .05 * rect.Height)
			});
			///LEFT -> DOWN
			leftPath.AddLines (new CGPoint[] {
        new CGPoint (.062 * rect.Width, .05 * rect.Height),
        new CGPoint (.062 * rect.Width, .165 * rect.Height)
			});
			///LEFT -> RIGHT
			leftPath.AddLines (new CGPoint [] {
        new CGPoint (.062 * rect.Width, .165 * rect.Height),
        new CGPoint (.281 * rect.Width, .165 * rect.Height)
			});
			///RIGHT -> DOWN
			leftPath.AddLines (new CGPoint[] {
        new CGPoint (.281 * rect.Width, .165 * rect.Height),
        new CGPoint (.281 * rect.Width, .206 * rect.Height)
			});
			///RIGHT -> LEFT
			leftPath.AddLines (new CGPoint[] {
        new CGPoint (.281 * rect.Width, .206 * rect.Height),
        new CGPoint (.062 * rect.Width, .206 * rect.Height)
			});
			///LEFT -> DOWN
			leftPath.AddLines (new CGPoint [] {
        new CGPoint (.062 * rect.Width, .206 * rect.Height),
        new CGPoint (.062 * rect.Width, .249 * rect.Height)
			});
			///LEFT -> RIGHT
			leftPath.AddLines (new CGPoint [] {
        new CGPoint (.062 * rect.Width, .249 * rect.Height),
        new CGPoint (.281 * rect.Width, .249 * rect.Height)
			});
			///RIGHT -> DOWN
			leftPath.AddLines (new CGPoint [] {
        new CGPoint (.281 * rect.Width, .249 * rect.Height),
        new CGPoint (.281 * rect.Width, .296 * rect.Height)
			});
			///RIGHT -> LEFT
			leftPath.AddLines (new CGPoint [] {
        new CGPoint (.281 * rect.Width, .296 * rect.Height),
        new CGPoint (.062 * rect.Width, .296 * rect.Height)
			});
			///LEFT -> DOWN
			leftPath.AddLines (new CGPoint [] {
        new CGPoint (.062 * rect.Width, .296 * rect.Height),
        new CGPoint (.062 * rect.Width, .41 * rect.Height)
			});
			///LEFT -> MIDDLE
			leftPath.AddLines (new CGPoint [] {
        new CGPoint (.062 * rect.Width, .41 * rect.Height),
        new CGPoint (.5 * rect.Width, .41 * rect.Height)
			});
			leftPath.CloseSubpath ();

			//add geometry to graphics context and draw it
			gctx.AddPath (leftPath);     
			gctx.DrawPath (CGPathDrawingMode.FillStroke);

			UIColor.Red.SetStroke ();
			///RIGHT HALF OF ANALYSER DIAGRAM
			rightPath.AddLines (new CGPoint [] {
        new CGPoint (.5 * rect.Width, .05 * rect.Height),  
        new CGPoint (.937 * rect.Width, .05 * rect.Height)
			});
			rightPath.AddLines (new CGPoint [] {
        new CGPoint (.937 * rect.Width, .05 * rect.Height),
        new CGPoint (.937 * rect.Width, .165 * rect.Height)
			});
			rightPath.AddLines (new CGPoint [] {
        new CGPoint (.937 * rect.Width, .165 * rect.Height),
        new CGPoint (.718 * rect.Width, .165 * rect.Height)
			});
			rightPath.AddLines (new CGPoint [] {
        new CGPoint (.718 * rect.Width, .165 * rect.Height),
        new CGPoint (.718 * rect.Width, .206 * rect.Height)
			});
			rightPath.AddLines (new CGPoint[] {
        new CGPoint (.718 * rect.Width, .206 * rect.Height),
        new CGPoint (.937 * rect.Width, .206 * rect.Height)
			});
			rightPath.AddLines (new CGPoint [] {
        new CGPoint (.937 * rect.Width, .206 * rect.Height),
        new CGPoint (.937 * rect.Width, .249 * rect.Height)
			});
			rightPath.AddLines (new CGPoint [] {
        new CGPoint (.937 * rect.Width, .249 * rect.Height),
        new CGPoint (.718 * rect.Width, .249 * rect.Height)
			});
			rightPath.AddLines (new CGPoint [] {  
        new CGPoint (.718 * rect.Width, .249 * rect.Height),     
        new CGPoint (.718 * rect.Width, .296 * rect.Height)
			});
			rightPath.AddLines (new CGPoint [] {       
        new CGPoint (.718 * rect.Width, .296 * rect.Height),       
        new CGPoint (.937 * rect.Width, .296 * rect.Height)
			});
			rightPath.AddLines (new CGPoint [] {       
        new CGPoint (.937 * rect.Width, .296 * rect.Height),       
        new CGPoint (.937 * rect.Width, .41 * rect.Height)
			});
			rightPath.AddLines (new CGPoint [] {        
        new CGPoint (.937 * rect.Width, .41 * rect.Height),       
        new CGPoint (.5 * rect.Width, .41 * rect.Height)
			});
			gctx.AddPath (rightPath);     
			gctx.DrawPath (CGPathDrawingMode.FillStroke);
		}
	}
}

