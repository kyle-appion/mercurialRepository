namespace ION.Droid.Activity.Grid {

  using System;

  using Android.Content;
  using Android.Graphics;
  using Android.Support.V7.Widget;
  using Android.Support.V4.View;

  public class LinkDecorator : RecyclerView.ItemDecoration {
    /// <summary>
    /// The percentage of the screen recycler view width that is used as padding between two views - both vertically and
    /// horizontally.
    /// </summary>
    private const float PERCENT_PADDING = 0.035f;

    private Context context;
    private DeviceGridAdapter adapter;

    private int initializationWidth;
    private Paint arrowPaint;
    private Bitmap rightArrow;
    private Bitmap leftArrow;
    private readonly Rect drawRect = new Rect();

    public LinkDecorator(Context context, DeviceGridAdapter adapter) {
      this.context = context;
      this.adapter = adapter;
      initializationWidth = -1;
    }

    public override void GetItemOffsets(Rect outRect, Android.Views.View view, RecyclerView parent, RecyclerView.State state) {
      if (initializationWidth != parent.Width) {
        if (parent.MeasuredWidth <= 0) {
          return;
        }
        Init(parent.MeasuredWidth);
      }
      var position = parent.GetChildAdapterPosition(view);
      var colSize = adapter.colSize;
      var row = position / colSize;
      var col = position % colSize;
      var horizPadding = rightArrow.Width / 2;
      var vertPadding = rightArrow.Height;

      int l = horizPadding, t = 0, r = horizPadding, b = vertPadding;

      outRect.Set(l, t, r, b);
    }

    public override void OnDrawOver(Canvas c, RecyclerView parent, RecyclerView.State state) {
      for (int i = 0; i < state.ItemCount - 1; i++) {
        if (!adapter.AreIndicesLinked(i, i + 1)) {
          continue;
        }

				var view = parent.GetChildAt(i);
        parent.GetDecoratedBoundsWithMargins(view, drawRect);
				var position = parent.GetChildAdapterPosition(view);
				var colSize = adapter.colSize;
				var row = position / colSize;
				var col = position % colSize;

        var padding = initializationWidth / 8;

        var x = drawRect.Right - (initializationWidth / 2);
        var y = (drawRect.Top + drawRect.Bottom) / 2;

        c.DrawBitmap(rightArrow, x, y - initializationWidth - padding, null);
        c.DrawBitmap(leftArrow, x, y + padding, null);
      }
    }

    public void Release() {
			initializationWidth = -1;

      if (rightArrow != null) {
        rightArrow.Recycle();
      }

      if (leftArrow != null) {
        leftArrow.Recycle();
      }
    }

    private void Init(int width) {
      Release();

      arrowPaint = new Paint();
      arrowPaint.Color = context.Resources.GetColor(Resource.Color.gold);
      arrowPaint.SetStyle(Paint.Style.Fill);

      var s = (int)(width * PERCENT_PADDING);
			var path = new Path();
			path.MoveTo(0.0f * s, 0.2f * s);
			path.LineTo(0.5f * s, 0.2f * s);
			path.LineTo(0.8f * s, 0.5f * s);
			path.LineTo(0.5f * s, 0.8f * s);
			path.LineTo(0.0f * s, 0.8f * s);
			path.Close();

      rightArrow = Bitmap.CreateBitmap(s, s, Bitmap.Config.Argb8888);

			var canvas = new Canvas(rightArrow);
			canvas.DrawPath(path, arrowPaint);

      var m = new Matrix();
      m.PreScale(-1, 1);
      leftArrow = Bitmap.CreateBitmap(rightArrow, 0, 0, s, s, m, false);

      initializationWidth = s;
    }

  }
}
