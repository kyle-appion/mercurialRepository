namespace ION.Droid.Activity.Job {

  using System;
  using System.Threading.Tasks;

  using Android.App;
	using Android.Locations;
  using Android.OS;
  using Android.Support.Design.Widget;
  using Android.Views;
  using Android.Widget;

  // Using ION
  using Core.Database;

  // Using ION.Droid
  using Fragments;
  using ION.Droid.Views;

  public class EditJobFragment : IONFragment, IJobPresenter {

    private EditText name;
		private EditText customer;
		private EditText dispatch;
		private EditText purchaseNo;
		private EditText notes;

		private EditText technician;
		private EditText system;
		private EditText address;
    private View getAddress;

    public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
			View ret;
			if (Build.VERSION.SdkInt < BuildVersionCodes.Lollipop) {
				ret = inflater.Inflate(Resource.Layout.fragment_edit_job_4_4, container, false);
			} else {
				ret = inflater.Inflate(Resource.Layout.fragment_edit_job, container, false);
			}

			name = ret.FindViewById<EditText>(Resource.Id.name);
			customer = ret.FindViewById<EditText>(Resource.Id.customer_no);
			dispatch = ret.FindViewById<EditText>(Resource.Id.dispatch_no);
			purchaseNo = ret.FindViewById<EditText>(Resource.Id.purchase_no);
			notes = ret.FindViewById<EditText>(Resource.Id.notes);

			technician = ret.FindViewById<EditText>(Resource.Id.job_technician_name);
			system = ret.FindViewById<EditText>(Resource.Id.job_system);
			address = ret.FindViewById<EditText>(Resource.Id.address);
      getAddress = ret.FindViewById<ImageView>(Resource.Id.icon);
      getAddress.Visibility = ViewStates.Gone;
      getAddress.SetOnClickListener(new ViewClickAction((v) => GetAddress()));

      return ret;
    }

    public override void OnActivityCreated(Bundle savedInstanceState) {
      base.OnActivityCreated(savedInstanceState);
    }

		public override void OnPause() {
			base.OnPause();
			HideKeyboard();
		}

    public void Present(JobRow job) {
      name.Text = job.jobName;
      customer.Text = job.customerNumber;
      dispatch.Text = job.dispatchNumber;
      purchaseNo.Text = job.poNumber;
      notes.Text = job.notes;

			technician.Text = job.techName;
			system.Text = job.systemType;
			address.Text = job.jobAddress;
    }

    public async Task<bool> SaveAsync(JobRow job) {
      job.jobName = name.Text;
      job.customerNumber = customer.Text;
      job.dispatchNumber = dispatch.Text;
      job.poNumber = purchaseNo.Text;
      job.notes = notes.Text;

			job.techName = technician.Text;
			job.systemType = system.Text;
			job.jobAddress = address.Text;

      return await ion.database.SaveAsync<JobRow>(job);
    }

    private async Task GetAddress() {
      var handler = new Handler();
      var dialog = new ProgressDialog(Activity);
      dialog.SetTitle(Resource.String.please_wait);
      dialog.SetMessage(GetString(Resource.String.location_determining_address));
      dialog.Show();

      var task = ion.locationManager.GetAddressFromLocationAsync(ion.locationManager.lastKnownLocation);
/*
    var done = false;
    var start = DateTime.Now;
    handler.Post(() => {
      if (!done) {

      }
    });
*/

      var ret = await task;
      dialog.Dismiss();
      address.Text = ret.address1 + " " + ret.address2;
    }

		private async Task<Address> PollGecode(string address) {
			var geo = new Geocoder(Activity);

			var addresses = await geo.GetFromLocationNameAsync(address, 1);
			if (addresses.Count > 0) {
				return addresses[0];
			} else {
				return null;
			}
		}
  }
}

