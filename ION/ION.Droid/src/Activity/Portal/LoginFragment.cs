namespace ION.Droid.Activity.Portal {

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;
	using System.Text;

	using Android.App;
	using Android.Content;
	using Android.OS;
	using Android.Runtime;
	using Android.Support.Design.Widget;
	using Android.Util;
	using Android.Views;
	using Android.Widget;

	public class LoginFragment : Fragment {

		private TextInputEditText login;
		private TextInputEditText password;
		private CheckBox rememberMe;

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
			var ret = inflater.Inflate(Resource.Layout.fragment_login, container, false);

			login = ret.FindViewById<TextInputEditText>(Resource.Id.login);
			password = ret.FindViewById<TextInputEditText>(Resource.Id.password);
			rememberMe = ret.FindViewById<CheckBox>(Resource.Id.checkbox);

			ret.FindViewById(Resource.Id.button).Click += (sender, e) => { 
				SubmitLogin();
			};

			return ret;
		}

		public override void OnCreate(Bundle savedInstanceState) {
			base.OnCreate(savedInstanceState);

			// Create your fragment here
		}

		/// <summary>
		/// Submits the login stuff to the Portal server.
		/// </summary>
		private void SubmitLogin() {
			var pd = new ProgressDialog(Activity);
			pd.SetTitle(Resource.String.portal_logging_in);
			pd.SetMessage(Resource.String.please_wait);
			pd.SetCancelable(false);
			pd.Indeterminate = true;
			pd.Show();

			Task.Factory.StartNew(() => {
			}).ContinueWith((t) => {
				pd.Dismiss();
			}, TaskScheduler.Default);
		}
	}
}
