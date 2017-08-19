namespace ION.Droid.Activity.Portal {

	using System;
	using System.Linq;
	using System.Threading.Tasks;

	using Android.App;
	using Android.Content.PM;
	using Android.OS;
	using Android.Support.Design.Widget;
	using Android.Views;
	using Android.Widget;

	using ION.Droid.Dialog;

	[Activity(Label = "@string/portal_login", Theme = "@style/TerminalActivityTheme", /*LaunchMode=Android.Content.PM.LaunchMode.SingleTask,*/ ScreenOrientation=ScreenOrientation.Portrait)]
	public class PortalLoginActivity : IONActivity {

		private EditText username;
		private EditText password;
		private CheckBox rememberMe;

		protected override void OnCreate(Bundle state) {
			base.OnCreate(state);

			if (Build.VERSION.SdkInt < BuildVersionCodes.Lollipop) {
				SetContentView(Resource.Layout.activity_portal_login_4_4);
			} else {
				SetContentView(Resource.Layout.activity_portal_login);
			}

			ActionBar.SetDisplayHomeAsUpEnabled(true);
			ActionBar.SetIcon(GetColoredDrawable(Resource.Drawable.ic_cloud, Resource.Color.gray));

			username = FindViewById<EditText>(Resource.Id.login);
			password = FindViewById<EditText>(Resource.Id.password);
			rememberMe = FindViewById<CheckBox>(Resource.Id.checkbox);

			FindViewById(Resource.Id.button).Click += (sender, e) => { 
				SubmitLogin();
			};

			FindViewById(Resource.Id.forgot).Click += (sender, e) => {
				ResolveForgotPassword();
			};

			FindViewById(Resource.Id.register).Click += (sender, e) => {
				ResolveRegister();
			};
		}

		protected override void OnResume() {
			base.OnResume();

			if (prefs.portal.rememberMe) {
				username.Text = prefs.portal.username;
				password.Text = prefs.portal.password;
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

		private void ResolveForgotPassword() {
			var adb = new IONAlertDialog(this);
			adb.SetTitle(Resource.String.portal_password_reset);
			var view = LayoutInflater.From(this).Inflate(Resource.Layout.dialog_portal_password_reset, null, false);
			adb.SetView(view);
			adb.SetCancelable(true);

			var edit = view.FindViewById<TextInputEditText>(Resource.Id.password);

			var d = adb.Show();

			view.FindViewById(Resource.Id.cancel).Click += (sender, e) => {
				d.Dismiss();
			};

			view.FindViewById(Resource.Id.ok).Click += async (sender, e) => {
				if (!string.IsNullOrEmpty(edit.Text)) {
					var pd = new ProgressDialog(this);
					pd.SetTitle(Resource.String.working);
					pd.SetMessage(GetString(Resource.String.please_wait));
					pd.SetCancelable(false);
				  pd.Show();

					var response = await ion.portal.RequestResetUserPasswordAsync(edit.Text);
					if (response.success) {
						Toast.MakeText(this, Resource.String.portal_password_reset_sent, ToastLength.Long).Show();
						d.Dismiss();
					} else {
						Toast.MakeText(this, Resource.String.portal_error_failed_to_login, ToastLength.Long).Show();
					}

					pd.Dismiss();
				} else {
					Toast.MakeText(this, Resource.String.portal_error_email_invalid, ToastLength.Long).Show();
				}
			};
		}

		private void ResolveRegister() {
			var adb = new IONAlertDialog(this);
			adb.SetTitle(Resource.String.register);
			var view = LayoutInflater.From(this).Inflate(Resource.Layout.dialog_portal_register, null, false);
			adb.SetView(view);
			adb.SetCancelable(true);

			var email = view.FindViewById<TextInputEditText>(Resource.Id.email);
			var password = view.FindViewById<TextInputEditText>(Resource.Id.password);
			var passwordConfirm = view.FindViewById<TextInputEditText>(Resource.Id.passwordConfirm);
			var icon = view.FindViewById<ImageView>(Resource.Id.icon);
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

			icon.Click += (sender, e) => {
				var dialog = new IONAlertDialog(this);
				dialog.SetMessage(GetString(Resource.String.portal_error_password_invalid));
				dialog.SetCancelable(true);
				dialog.SetNegativeButton(Resource.String.cancel, (sender2, e2) => {});
				dialog.Show();
			};

			var d = adb.Show();

			view.FindViewById(Resource.Id.cancel).Click += (sender, e) => {
				d.Dismiss();
			};

			view.FindViewById(Resource.Id.register).Click += async (sender, e) => {
				if (string.IsNullOrEmpty(email.Text)) {
					Toast.MakeText(this, Resource.String.portal_error_email_invalid, ToastLength.Long).Show();
					return;
				}

				if (!password.Text.Equals(passwordConfirm.Text)) {
					Toast.MakeText(this, Resource.String.portal_error_passwords_dont_match, ToastLength.Long).Show();
					return;
				}

				if (!ion.portal.IsPasswordValid(password.Text)) {
					Toast.MakeText(this, Resource.String.portal_error_password_invalid, ToastLength.Long).Show();
					return;
				}

				if (password.Text.Equals(passwordConfirm.Text)) {
					var pd = new ProgressDialog(this);
					pd.SetTitle(Resource.String.working);
					pd.SetMessage(GetString(Resource.String.please_wait));
					pd.SetCancelable(false);
					pd.Show();

					var response = await ion.portal.RegisterUser(email.Text, password.Text);
					if (response.success) {
						Toast.MakeText(this, Resource.String.portal_password_reset_sent, ToastLength.Long).Show();
						this.username.Text = email.Text;
						this.password.Text = password.Text;
						d.Dismiss();
					} else {
						Toast.MakeText(this, response.message, ToastLength.Long).Show();
					}

					pd.Dismiss();
				} else {
					Toast.MakeText(this, Resource.String.portal_error_passwords_dont_match, ToastLength.Long).Show();
				}
			};
		}

		/// <summary>
		/// Submits the login stuff to the Portal server.
		/// </summary>
		private async void SubmitLogin() {
			if (rememberMe.Checked) {
				prefs.portal.rememberMe = rememberMe.Checked;
				prefs.portal.username = username.Text;
				prefs.portal.password = password.Text;
			} else {
				prefs.portal.rememberMe = false;
				prefs.portal.username = "";
				prefs.portal.password = "";
			}
			// TODO ahodder@appioninc.com: If the connection attempt times out then we need to display a valid error message
			var pd = new ProgressDialog(this);
			pd.SetTitle(Resource.String.portal_logging_in);
			pd.SetMessage(GetString(Resource.String.please_wait));
			pd.SetCancelable(false);
			pd.Indeterminate = true;
			pd.Show();

			var res = await ion.portal.RequestLoginAsync(username.Text, password.Text);

			if (res.success) {
				SetResult(Result.Ok);
				Finish();
			} else {
				Toast.MakeText(this, res.message, ToastLength.Long).Show();
			}

			pd.Dismiss();
		}
	}
}
