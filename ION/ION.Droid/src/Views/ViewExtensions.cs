namespace ION.Droid.Views {

	using System;
	using System.IO;

	using Android.Graphics;
	using Android.Views;

	public static class ViewExtensions {

		/// <summary>
		/// Captures the view to bitmap.
		/// </summary>
		/// <returns>The view to bitmap.</returns>
		/// <param name="view">View.</param>
		public static Bitmap CaptureViewToBitmap(this View view) {
			var cacheEnabled = view.DrawingCacheEnabled;
			view.DrawingCacheEnabled = true;
			var b = Bitmap.CreateBitmap(view.GetDrawingCache(true));
			view.DrawingCacheEnabled = cacheEnabled;

			return b;
		}

		/// <summary>
		/// Renders the view to a Png of the same size.
		/// </summary>
		/// <returns>The png.</returns>
		/// <param name="view">View.</param>
		public static Stream ToPng(this View view) {
			var b = view.CaptureViewToBitmap();

			var ret = new MemoryStream();
			b.Compress(Bitmap.CompressFormat.Png, 100, ret);
			b.Recycle();
			b.Dispose();

			return ret;
		}
	}
}
