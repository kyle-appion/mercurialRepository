namespace ION.Droid.Activity.Report {
	
	using System;

	using Android.App;
	using Android.OS;
	using Android.Views;

  using ION.Droid.Fragments;

	/// <summary>
	/// The fragment that is responsible for displaying the views related to selecting reports to graph and export or view.
	/// </summary>
	public class NewSavedReportFragment : IONFragment {

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
			var ret = inflater.Inflate(Resource.Layout.fragment_report_new_saved, container, false);



			return ret;
		}

		public override void OnActivityCreated(Bundle savedInstanceState) {
			base.OnActivityCreated(savedInstanceState);
		}
	}
}

