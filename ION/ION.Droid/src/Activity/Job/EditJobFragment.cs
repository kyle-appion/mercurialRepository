﻿namespace ION.Droid.Activity.Job {

  using System;
  using System.Threading.Tasks;

  using Android.App;
  using Android.Content;
	using Android.Locations;
  using Android.OS;
  using Android.Support.Design.Widget;
  using Android.Views;
  using Android.Widget;

  using Appion.Commons.Util;

  // Using ION
  using Core.Database;

  // Using ION.Droid
  using Dialog;
  using Fragments;
  using ION.Droid.Views;

  public class EditJobFragment : IONFragment, IJobPresenter {

    private Handler handler = new Handler();

    private EditText name;
		private EditText customer;
		private EditText dispatch;
		private EditText purchaseNo;
		private EditText notes;

		private EditText technician;
		private EditText system;
		private EditText addressView;
    private TextView coordinates;
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
			addressView = ret.FindViewById<EditText>(Resource.Id.address);
      coordinates = ret.FindViewById<TextView>(Resource.Id.coordinates);
      getAddress = ret.FindViewById<ImageView>(Resource.Id.icon);
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
			addressView.Text = job.jobAddress;
      coordinates.Text = job.jobLocation;
    }

    public async Task<bool> SaveAsync(JobRow job) {
      job.jobName = name.Text;
      job.customerNumber = customer.Text;
      job.dispatchNumber = dispatch.Text;
      job.poNumber = purchaseNo.Text;
      job.notes = notes.Text;

			job.techName = technician.Text;
			job.systemType = system.Text;
			job.jobAddress = addressView.Text;

      return await ion.database.SaveAsync<JobRow>(job);
    }

    private async Task GetAddress() {
      if (!ion.appPrefs._location.allowsGps) {
        var adb = new IONAlertDialog(Activity);
        adb.SetTitle(Resource.String.location_settings_not_enabled);
        adb.SetMessage(Resource.String.location_settings_enabled_required);

        adb.SetNegativeButton(Resource.String.cancel, (sender, e) => { });
				adb.SetPositiveButton(Resource.String.allow, (sender, e) => {
          ion.appPrefs._location.allowsGps = true;
        });

				adb.Show();
        return;
      }

			var dialog = new ProgressDialog(Activity);
			dialog.SetTitle(Resource.String.please_wait);
			dialog.SetMessage(GetString(Resource.String.location_determining_address));
			dialog.Show();

      var usAddress = addressView.Text.Trim();
      if (string.IsNullOrEmpty(usAddress)) {
        // Get current address based on coordinates
        var address = await PollGeocode();
				if (address == null) {
					Toast.MakeText(Activity, Resource.String.location_undetermined, ToastLength.Long).Show();
				} else {
					addressView.Text = address.GetAddressLine(0);
					coordinates.Text = address.Latitude + ", " + address.Longitude;
				}
      } else {
        // Get coordinates based on given address
        var address = await PollGeocode(usAddress);
        if (address == null) {
          Toast.MakeText(Activity, Resource.String.location_undetermined, ToastLength.Long).Show();
        } else {
          coordinates.Text = address.Latitude + ", " + address.Longitude;
        }
      }

      dialog.Dismiss();
    }

		private async Task<Address> PollGeocode(string address) {
			var geo = new Geocoder(Activity);

      var start = DateTime.Now;
      var task = geo.GetFromLocationNameAsync(address, 1);
      while (!task.IsCompleted && DateTime.Now - start <= TimeSpan.FromSeconds(5)) {
        await Task.Delay(100);
      }

      if (task.IsCompleted) {
        return task.Result[0];
      } else {
        return null;
      }
		}

    private async Task<Address> PollGeocode() {
      var loc = ion.locationManager.lastKnownLocation;

      var geo = new Geocoder(Activity);

			var start = DateTime.Now;
			var task = geo.GetFromLocationAsync(loc.latitude, loc.longitude, 1);
			while (!task.IsCompleted && DateTime.Now - start <= TimeSpan.FromSeconds(5)) {
				await Task.Delay(100);
			}

      if (task.IsCompleted) {
        return task.Result[0];
      } else {
        return null;
      }
    }
  }
}

