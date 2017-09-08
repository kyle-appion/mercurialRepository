namespace ION.Droid.Animations {

	using System;

	using Android.Animation;

	public class AnimatorListenerActionAdapter : AnimatorListenerAdapter {

		public Action <Animator> onAnimationCancel;
		public Action <Animator> onAnimationEnd;
		public Action <Animator> onAnimationPause;
		public Action <Animator> onAnimationRepeat;
		public Action <Animator> onAnimationResume;
		public Action <Animator> onAnimationStart;

		public override void OnAnimationCancel(Animator animation) {
			base.OnAnimationCancel(animation);
			if (onAnimationCancel != null) {
				onAnimationCancel(animation);
			}
		}

		public override void OnAnimationEnd(Animator animation) {
			base.OnAnimationEnd(animation);
			if (onAnimationEnd != null) {
				onAnimationEnd(animation);
			}
		}

		public override void OnAnimationPause(Animator animation) {
			base.OnAnimationEnd(animation);
			if (onAnimationPause != null) {
				onAnimationPause(animation);
			}
		}

		public override void OnAnimationRepeat(Animator animation) {
			base.OnAnimationEnd(animation);
			if (onAnimationRepeat != null) {
				onAnimationRepeat(animation);
			}
		}

		public override void OnAnimationResume(Animator animation) {
			base.OnAnimationEnd(animation);
			if (onAnimationResume != null) {
				onAnimationResume(animation);
			}
		}

		public override void OnAnimationStart(Animator animation) {
			base.OnAnimationEnd(animation);
			if (onAnimationStart != null) {
				onAnimationStart(animation);
			}
		}
	}
}
