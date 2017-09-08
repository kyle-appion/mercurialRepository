namespace ION.Droid.Views {

	using System;

	using Android.Views;

	public class ViewActionTouchListener : Java.Lang.Object, View.IOnTouchListener {
		public delegate bool OnTouchDelegate(View view, MotionEvent e);

		public OnTouchDelegate onTouch;


		public bool OnTouch(View v, MotionEvent e) {
			if (onTouch != null) {
				return onTouch(v, e);
			} else {
				return false;
			}
		}
	}
}
