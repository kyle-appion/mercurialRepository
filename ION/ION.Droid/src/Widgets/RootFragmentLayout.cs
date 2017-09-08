namespace ION.Droid.Widgets {

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	using Android.App;
	using Android.Content;
	using Android.OS;
	using Android.Runtime;
	using Android.Util;
	using Android.Views;
	using Android.Widget;

	using Java.Interop;

	public class RootFragmentLayout : LinearLayout {
		
		public RootFragmentLayout(Context context) : base(context) {
		}

		public RootFragmentLayout(Context context, IAttributeSet attrs) : base(context, attrs) {
		}

		public RootFragmentLayout(Context context, IAttributeSet attrs, int defStyle) : base(context, attrs, defStyle) {
		}

		[Export]
		public float getXFraction() {
			if (Width == 0) {
				return 0;
			} else {
				return GetX() / Width;
			}
		}

		[Export]
		public void setXFraction(float fraction) {
			var w = Width;
			SetX(w > 0 ? fraction * w : w);
		}
	}
}

