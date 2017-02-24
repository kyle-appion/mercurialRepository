namespace ION.Droid.Activity.Job {

  using System.Threading.Tasks;

	using Android.Locations;
  using Android.OS;
  using Android.Support.Design.Widget;
  using Android.Views;
  using Android.Widget;

  // Using ION
  using Core.Database;

  // Using ION.Droid
  using Fragments;

  public class EditJobFragment : IONFragment, IJobPresenter {

    private TextInputEditText name;
		private TextInputEditText customer;
		private TextInputEditText dispatch;
		private TextInputEditText purchaseNo;
		private TextInputEditText notes;

		private TextInputEditText technician;
		private TextInputEditText system;
		private TextInputEditText address;

    public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
      var ret = inflater.Inflate(Resource.Layout.fragment_edit_job, container, false);

			name = ret.FindViewById<TextInputEditText>(Resource.Id.name);
			customer = ret.FindViewById<TextInputEditText>(Resource.Id.customer_no);
			dispatch = ret.FindViewById<TextInputEditText>(Resource.Id.dispatch_no);
			purchaseNo = ret.FindViewById<TextInputEditText>(Resource.Id.purchase_no);
			notes = ret.FindViewById<TextInputEditText>(Resource.Id.notes);

			technician = ret.FindViewById<TextInputEditText>(Resource.Id.job_technician_name);
			system = ret.FindViewById<TextInputEditText>(Resource.Id.job_system);
			address = ret.FindViewById<TextInputEditText>(Resource.Id.address);

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

