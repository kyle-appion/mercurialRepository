namespace ION.Droid.Activity.Job {

  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;

  using Android.App;
  using Android.Content;
  using Android.OS;
  using Android.Support.Design.Widget;
  using Android.Runtime;
  using Android.Util;
  using Android.Views;
	using Android.Views.InputMethods;
  using Android.Widget;

  // Using ION
  using Core.Database;

  // Using ION.Droid
  using Fragments;

  public class EditJobFragment : IONFragment, IJobPresenter {

    private TextInputLayout nameLayout;
    private TextInputLayout customerLayout;
    private TextInputLayout dispatchLayout;
    private TextInputLayout purchaseNoLayout;
    private TextInputLayout notesLayout;

    private TextView name;
    private TextView customer;
    private TextView dispatch;
    private TextView purchaseNo;
    private TextView notes;

    public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
      var ret = inflater.Inflate(Resource.Layout.fragment_edit_job, container, false);

      nameLayout = ret.FindViewById<TextInputLayout>(Resource.Id.name);
      customerLayout = ret.FindViewById<TextInputLayout>(Resource.Id.customer_no);
      dispatchLayout = ret.FindViewById<TextInputLayout>(Resource.Id.dispatch_no);
      purchaseNoLayout = ret.FindViewById<TextInputLayout>(Resource.Id.purchase_no);
      notesLayout = ret.FindViewById<TextInputLayout>(Resource.Id.notes);

      name = nameLayout.FindViewById<TextInputEditText>(Resource.Id.text);
      customer = customerLayout.FindViewById<TextInputEditText>(Resource.Id.text);
      dispatch = dispatchLayout.FindViewById<TextInputEditText>(Resource.Id.text);
      purchaseNo = purchaseNoLayout.FindViewById<TextInputEditText>(Resource.Id.text);
      notes = notesLayout.FindViewById<TextInputEditText>(Resource.Id.text);

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

      return await ion.database.SaveAsync<JobRow>(job);
    }
  }
}

