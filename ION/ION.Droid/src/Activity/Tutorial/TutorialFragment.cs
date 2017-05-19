namespace ION.Droid.Activity.Tutorial {

	using System;
	using System.Threading.Tasks;

	using Android.Animation;
	using Android.App;
	using Android.Content;
	using Android.Graphics;
	using Android.OS;
	using Android.Views;
	using Android.Widget;

	using ION.Droid.Animations;
	using ION.Droid.Util;

	public class TutorialFragment : Fragment {

		public TutorialPage page {
			get {
				return __page;
			}
			set {
				__page = value;

				Update();
			}
		} TutorialPage __page;

		public BitmapCache cache;

		private bool isCreated;
		private AbsoluteLayout abs;
		private View arrow;
		private View progress;

		private TextView titleView;
		private ImageView imageView;
		private TextView textView;

		private Handler handler = new Handler(Looper.MainLooper);

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
			var ret = inflater.Inflate(Resource.Layout.fragment_tutorial, container, false);

			abs = ret.FindViewById<AbsoluteLayout>(Resource.Id.view);
			arrow = ret.FindViewById(Resource.Id.button);
			progress = ret.FindViewById(Resource.Id.progress);

			titleView = ret.FindViewById<TextView>(Resource.Id.title);
			imageView = ret.FindViewById<ImageView>(Resource.Id.image);
			textView = ret.FindViewById<TextView>(Resource.Id.text);

			isCreated = true;
			return ret;
		}

		public override void OnAttach(Context context) {
			base.OnAttach(context);
		}

		public override void OnResume() {
			base.OnResume();

			handler.PostDelayed(() => {
				Update();
			}, 500);
		}

    public override void OnPause() {
      base.OnPause();
      cache.Clear();
    }

		private void Update() {
			if (isCreated && page != null) {
				titleView.SetText(page.titleResource);
				if (page.imageResource != 0) {
					LoadBitmap(page.imageResource);
				} else {
					arrow.Visibility = ViewStates.Gone;
					imageView.Visibility = ViewStates.Gone;
					progress.Visibility = ViewStates.Gone;
				}
//				imageView.SetImageBitmap(cache.GetBitmap(page.imageResource));
				textView.SetText(page.contentResource);
			}
		}

		private void LoadBitmap(int bitmap) {
			PulseView(arrow);
			imageView.SetImageBitmap(null);
			imageView.Visibility = ViewStates.Invisible;
			arrow.Visibility = ViewStates.Invisible;
			progress.Visibility = ViewStates.Visible;
			progress.Alpha = 1f;

			Task.Factory.StartNew(() => {
				cache.GetBitmap(bitmap);
			}).ContinueWith((t) => {
				handler.Post(() => {
					var a = Activity.Resources.GetAnimation(Resource.Animation.fade_in);
//					imageView.StartAnimation();
					imageView.SetImageBitmap(cache.GetBitmap(bitmap));
					FadeIn(500);
				});
			});
		}

		private void PulseView(View view) {
			view.ClearAnimation();

			var a = ObjectAnimator.OfPropertyValuesHolder(view, PropertyValuesHolder.OfFloat("scaleX", 0.8f), PropertyValuesHolder.OfFloat("scaleY", 0.8f));
			a.SetDuration(500);
			a.RepeatCount = ObjectAnimator.Infinite;
			a.RepeatMode = ValueAnimatorRepeatMode.Reverse;

			a.Start();
		}

		/// <summary>
		/// Fades the content view into the screen.
		/// </summary>
		private void FadeIn(long duration) {
			progress.Visibility = ViewStates.Visible;
			imageView.Visibility = ViewStates.Visible;
			imageView.Alpha = 0f;
			arrow.Visibility = ViewStates.Invisible;

			progress.Animate()
			        .Alpha(0f)
			        .SetDuration(duration)
			        .SetListener(new AnimatorListenerActionAdapter() {
								onAnimationEnd = (a) => {
									progress.Visibility = ViewStates.Invisible;
								},
							});

			imageView.Animate()
			         .Alpha(1f)
			         .SetDuration(duration)
			         .SetListener(new AnimatorListenerActionAdapter() {
								 onAnimationEnd = (a) => {
								   arrow.Visibility = ViewStates.Visible;
									 var lp = arrow.LayoutParameters as AbsoluteLayout.LayoutParams;
									 lp.X = (int)(imageView.Left + page.xPercent * imageView.Width - arrow.Width / 2);
									 lp.Y = (int)(imageView.Top + page.yPercent * imageView.Height);
									 arrow.RequestLayout();
								 },
							 });
		}
	}
}
