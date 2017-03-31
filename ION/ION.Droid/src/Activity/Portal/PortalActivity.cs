using ION.Droid.Views;
namespace ION.Droid.Activity.Portal {

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	using Android.Animation;
	using Android.App;
	using Android.Content;
	using Android.Content.PM;
	using Android.OS;
	using Android.Runtime;
	using Android.Support.Design.Widget;
	using Android.Views;
	using Android.Widget;

	using ION.Droid.Dialog;

	/// <summary>
	/// This activity is the base activity for Appion portal interactions.
	/// </summary>
	[Activity(Label = "@string/portal", Theme = "@style/TerminalActivityTheme", LaunchMode=Android.Content.PM.LaunchMode.SingleTask, ScreenOrientation=ScreenOrientation.Portrait)]
	public class PortalActivity : IONActivity {

		private const int REQUEST_LOGIN = 1;

		private TextInputEditText displayName;
		private TextInputEditText email;
		private TextInputEditText password;
		private TextInputEditText passwordConfirm;

		protected override void OnCreate(Bundle savedInstanceState) {
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.activity_portal);

			ActionBar.SetDisplayHomeAsUpEnabled(true);
			ActionBar.SetIcon(GetColoredDrawable(Resource.Drawable.ic_cloud, Resource.Color.gray));

			// Setup home widgets
			var home = FindViewById(Resource.Id.home);
			FindViewById<Button>(Resource.Id.portal_session).Click += (sender, e) => {
				StartActivity(typeof(PortalSessionsViewActivity));
			};

			FindViewById<Button>(Resource.Id.portal_access_code).Click += (sender, e) => {
				StartActivity(typeof(PortalAccessCodeManagerActivity));
			};

			FindViewById<Button>(Resource.Id.portal_connections).Click += (sender, e) => {
				StartActivity(typeof(PortalViewConnectionsActivity));
			};

			FindViewById<Button>(Resource.Id.portal_web).Click += (sender, e) => {
				var i = new Intent(Intent.ActionView);
				i.SetData(Android.Net.Uri.Parse(ion.portal.loginPortalUrl));
				StartActivity(i);
			};

			home.FindViewById(Resource.Id.toggle).Click += (sender, args) => {
				AnimateToSettingsView();
			};

			// Setup settings Widgets
			var settings = FindViewById(Resource.Id.settings);
			displayName = settings.FindViewById<TextInputEditText>(Resource.Id.name);
			email = settings.FindViewById<TextInputEditText>(Resource.Id.email);
			password = settings.FindViewById<TextInputEditText>(Resource.Id.password);
			passwordConfirm = settings.FindViewById<TextInputEditText>(Resource.Id.passwordConfirm);
			var icon = settings.FindViewById<ImageView>(Resource.Id.icon);

			settings.FindViewById(Resource.Id.toggle).Click += (sender, args) => {
				AnimateToHomeView();
			};

			FindViewById(Resource.Id.update).Click += (sender, args) => {
				Update();
			};

			FindViewById(Resource.Id.logout).Click += (sender, args) => {
				LogoutUser();
			};

			password.TextChanged += (sender, e) => {
				if (password.Text.Equals(passwordConfirm.Text) && ion.portal.IsPasswordValid(password.Text)) {
					icon.SetColorFilter(Android.Graphics.Color.Green);
					icon.SetImageBitmap(cache.GetBitmap(Resource.Drawable.ic_check));
				} else {
					icon.SetColorFilter(Android.Graphics.Color.Red);
					icon.SetImageBitmap(cache.GetBitmap(Resource.Drawable.ic_x));
				}
			};

			passwordConfirm.TextChanged += (sender, e) => {
				if (password.Text.Equals(passwordConfirm.Text) && ion.portal.IsPasswordValid(password.Text)) {
					icon.SetColorFilter(Android.Graphics.Color.Green);
					icon.SetImageBitmap(cache.GetBitmap(Resource.Drawable.ic_check));
				} else {
					icon.SetColorFilter(Android.Graphics.Color.Red);
					icon.SetImageBitmap(cache.GetBitmap(Resource.Drawable.ic_x));
				}
			};

			icon.SetOnClickListener(new ViewClickAction((view) => {
				var dialog = new IONAlertDialog(this);
				dialog.SetMessage(GetString(Resource.String.portal_error_password_invalid));
				dialog.SetCancelable(true);
				dialog.SetNegativeButton(Resource.String.cancel, (sender2, e2) => {});
				dialog.Show();
			}));
		}

		protected override void OnResume() {
			base.OnResume();

			displayName.Text = ion.portal.displayName;
			email.Text = ion.portal.userEmail;

			if (!ion.portal.isLoggedIn) {
				var i = new Intent(this, typeof(PortalLoginActivity));
				StartActivityForResult(i, REQUEST_LOGIN);
			}
		}

		public override bool OnMenuItemSelected(int featureId, IMenuItem item) {
			switch (item.ItemId) {
				case Android.Resource.Id.Home:
					SetResult(Result.Canceled);
					Finish();
					return true;
				default:
					return base.OnMenuItemSelected(featureId, item);
			}
		}

		protected override void OnActivityResult(int requestCode, Result resultCode, Intent data) {
			switch (requestCode) {
				case REQUEST_LOGIN:
					if (resultCode != Result.Ok || !ion.portal.isLoggedIn) {
						Toast.MakeText(this, Resource.String.portal_error_failed_to_login, ToastLength.Long).Show();
						Finish();
					} else {
//						Toast.MakeText(this, string.Format(GetString(Resource.String.portal_welcome_name), ion.portal.displayName), ToastLength.Long).Show();
					}
					break;
				default:
					base.OnActivityResult(requestCode, resultCode, data);
					break;
			}
		}

		private async void LogoutUser() {
			var pd = new ProgressDialog(this);
			pd.SetTitle(Resource.String.portal_logging_out);
			pd.SetMessage(GetString(Resource.String.please_wait));
			pd.SetCancelable(false);
			pd.Show();

			await ion.portal.Logout();
			pd.Dismiss();

			Finish();
		}

		private async void Update() {
			if (!password.Text.Equals(passwordConfirm.Text)) {
				Toast.MakeText(this, Resource.String.portal_error_passwords_dont_match, ToastLength.Long).Show();
				return;
			}

			if (!ion.portal.IsPasswordValid(password.Text)) {
				Toast.MakeText(this, Resource.String.portal_error_password_invalid, ToastLength.Long).Show();
				return;
			}

			var pd = new ProgressDialog(this);
			pd.SetTitle(Resource.String.working);
			pd.SetMessage(GetString(Resource.String.please_wait));
			pd.SetCancelable(false);
			pd.Show();

			var response = await ion.portal.UpdatePassword(passwordConfirm.Text);
			if (response.success) {
				if (ion.preferences.portal.rememberMe) {
					ion.preferences.portal.password = password.Text;
				}
				password.Text = "";
				passwordConfirm.Text = "";
				Toast.MakeText(this, Resource.String.portal_update_successful, ToastLength.Long).Show();
			} else {
				Toast.MakeText(this, response.message, ToastLength.Long).Show();
			}

			pd.Dismiss();

		}

		private void AnimateToSettingsView() {
			FlipView(FindViewById(Resource.Id.content), Resource.Id.home, Resource.Id.settings);

		}

		private void AnimateToHomeView() {
			FlipView(FindViewById(Resource.Id.content), Resource.Id.settings, Resource.Id.home);
		}

		/// <summary>
		/// Flips a view, hiding one view and revealing another as the flip finished.
		/// </summary>
		/// <param name="view">View.</param>
		/// <param name="hideId">Hide identifier.</param>
		/// <param name="revealId">Reveal identifier.</param>
		private void FlipView(View view, int hideId, int revealId) {
			var fo = AnimatorInflater.LoadAnimator(this, Resource.Animation.card_flip_left_out);
			var fi = AnimatorInflater.LoadAnimator(this, Resource.Animation.card_flip_left_in);
			var fadeOut = AnimatorInflater.LoadAnimator(this, Resource.Animation.fade_out);
			var fadeIn = AnimatorInflater.LoadAnimator(this, Resource.Animation.fade_in);

			var hideView = view.FindViewById(hideId);
			var revealView = view.FindViewById(revealId);

			fo.SetTarget(view);
			fi.SetTarget(view);
			fadeOut.SetTarget(hideView);
			fadeIn.SetTarget(revealView);

			var animOut = new AnimatorSet();
			animOut.Play(fo);
			animOut.SetDuration(350);
			animOut.AnimationEnd += (obj, args) => {
				hideView.Visibility = ViewStates.Gone;
				revealView.Visibility = ViewStates.Visible;
			};

			var animIn = new AnimatorSet();
			animIn.Play(fi);
			animIn.SetDuration(350);

			var animationSet = new AnimatorSet();
			animationSet.AnimationEnd += (obj, args) => {
				hideView.Visibility = ViewStates.Gone;
				revealView.Visibility = ViewStates.Visible;
			};
			animationSet.Play(animOut)
			            .Before(animIn);
			animationSet.Start();
		}

	}
}
