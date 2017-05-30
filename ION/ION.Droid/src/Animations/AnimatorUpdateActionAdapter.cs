namespace ION.Droid.Animations {

	using System;

	using Android.Animation;

	public class AnimatorUpdateActionAdapter : Java.Lang.Object, ValueAnimator.IAnimatorUpdateListener {
		public Action<ValueAnimator> onAnimationUpdate;


		public void OnAnimationUpdate(ValueAnimator animator) {
			if (onAnimationUpdate != null) {
				onAnimationUpdate(animator);
			}
		}
	}
}
