/*

namespace ION.Droid {
  using System;

  using Android.App;
  using Android.Content;
	using Android.Graphics;
  using Android.Graphics.Drawables;
	using Android.Views;
  using Android.OS;

	public class GridGaugeDeviceSensorView : View {
    /// <summary>
    /// The typeface for the view.
    /// </summary>
    private const string TYPEFACE = "fonts/DroidSans.ttf";

    /// <summary>
    /// The paint that is applied to the header text of the view.
    /// </summary>
    private Paint typePaint;
    /// <summary>
    /// The paint that is applied to the view's measurement. 
    /// </summary>
    private Paint measurementPaint;
    /// <summary>
    /// The paint that is applied to the measurement's unit of the view.
    /// </summary>
    private Paint unitPaint;

    public GridGaugeDeviceSensorView(Context context) : base(context){
    }


    //
    // The header will be 1/5
    // The content will be 3/5
    // the footer will be 1/5
    // 
    // aspect ratio 2/3
    //
    protected override void OnDraw(Canvas canvas) {
      // Draw header

    }

    private void Initialize() {
      // Create the typeface for the view.
      var tf = Typeface.CreateFromAsset(Context.Assets, TYPEFACE);

      // Create the header paint
      typePaint = new Paint();
      typePaint.SetTypeface(tf);
      typePaint.TextSize = CalculateTypeTextSize();
      typePaint.Color = Color.White;
      typePaint.TextAlign = Paint.Align.Left;


      // Create the measurement paint
      measurementPaint = new Paint();
      measurementPaint.SetTypeface(tf);
      measurementPaint.TextSize = CalculateMeasurementTextSize();
      measurementPaint.Color = Color.Black;
      measurementPaint.TextAlign = Paint.Align.Right;

      // Create the Unit paint
      unitPaint = new Paint();
      unitPaint.SetTypeface(tf);
      unitPaint.TextSize = CalculateUnitTextSize();
      unitPaint.Color = Color.Black;
      unitPaint.TextAlign = Paint.Align.Right;
    }

    /// <summary>
    /// Calculates the size of the gauge type text 
    /// </summary>
    /// <returns>The type text size.</returns>
    private float CalculateTypeTextSize() {
      
    }
  }
}
*/