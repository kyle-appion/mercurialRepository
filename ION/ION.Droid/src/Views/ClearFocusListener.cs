using Android.Graphics;
namespace ION.Droid.Views {

	using System;

	using Android.Content;
	using Android.Views;
	using Android.Views.InputMethods;

	public class ClearFocusListener : Java.Lang.Object, View.IOnTouchListener{
		private View[] views;

		public ClearFocusListener(params View[] views) {
			this.views = views;
		}

		public bool OnTouch(View view, MotionEvent e) {
			if (e.Action == MotionEventActions.Down) {
				foreach (var v in views) {
					var r = new Rect();
					v.GetGlobalVisibleRect(r);
					if (r.Contains((int)e.RawX, (int)e.RawY)) {
						v.ClearFocus();
						var imm = v.Context.GetSystemService(Context.InputService) as InputMethodManager;
						imm.HideSoftInputFromWindow(v.WindowToken, 0);
					}
				}
			}

			return false;
		}
	}
}
