namespace ION.Droid.Widgets.RecyclerView {

	using System;

	using Android.Graphics;
	using Android.Support.V7.Widget;


	public class SwipeDecoration : RecyclerView.ItemDecoration {
		public SwipeDecoration() {
		}

		public override void OnDrawOver(Canvas c, RecyclerView parent, RecyclerView.State state) {
			c.DrawColor(Color.Red);
		}
	}
}
