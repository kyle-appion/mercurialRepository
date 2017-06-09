using ION.Droid.Dialog;
namespace ION.Droid.Activity.Portal {

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	using Android.App;
	using Android.Content;
	using Android.Content.PM;
	using Android.OS;
	using Android.Runtime;
	using Android.Support.Design.Widget;
	using Android.Support.V4.Widget;
	using Android.Support.V7.Widget;
	using Android.Views;
	using Android.Widget;

  using Appion.Commons.Util;

	using ION.CoreExtensions.Net.Portal;

	using ION.Droid.Views;
	using ION.Droid.Widgets.RecyclerViews;

	[Activity(Label = "@string/portal_access_code_manager", Theme = "@style/TerminalActivityTheme", LaunchMode=Android.Content.PM.LaunchMode.SingleTask, ScreenOrientation=ScreenOrientation.Portrait)]
	public class PortalAccessCodeManagerActivity : IONActivity, SwipeRefreshLayout.IOnRefreshListener {

		private EditText entry;
		private SwipeRefreshLayout swiper;
		private RecyclerView list;
		private Adapter adapter;

		protected override void OnCreate(Bundle savedInstanceState) {
			base.OnCreate(savedInstanceState);

			ActionBar.SetDisplayHomeAsUpEnabled(true);

      try {
        if (Build.VERSION.SdkInt > BuildVersionCodes.Lollipop) {
          SetContentView(Resource.Layout.activity_portal_access_code_manager);
        } else {
					SetContentView(Resource.Layout.activity_portal_access_code_manager_4_4);
				}
      } catch (Exception e) {
        Log.E(this, "Failed to set layout. Defaulting to old version", e);
        SetContentView(Resource.Layout.activity_portal_access_code_manager_4_4);
      }

			entry = FindViewById<EditText>(Resource.Id.entry);
			swiper = FindViewById<SwipeRefreshLayout>(Resource.Id.swiper);
			list = FindViewById<RecyclerView>(Resource.Id.list);

			FindViewById(Resource.Id.submit).Click += async (sender, e) => {
				await SubmitAccessCodeAsync(entry.Text);
			};

			FindViewById(Resource.Id.button).Click += async (sender, e) => {
				// TODO dialog
				var response = await ion.portal.RequestAccessCodeAsync();
				if (response.success) {
					await QueryPendingAccessCodesAsync();
				} else {
					Toast.MakeText(this, Resource.String.portal_error_access_code_too_many, ToastLength.Long).Show();
				}
			};

			list.SetAdapter(adapter = new Adapter());
			adapter.emptyView = FindViewById(Resource.Id.empty);
			adapter.deleteAction = OnDelete;
			adapter.onItemClicked += (position) => {
				var record = adapter[position] as AccessCodeRecord;
				if (record.data.acceptId != 0) {
					RequestConfirmAccess(position, record.data);
				}
			};
			swiper.SetOnRefreshListener(this);
		}

		protected override void OnResume() {
			base.OnResume();
			OnRefresh();
		}


		public override bool OnCreateOptionsMenu(IMenu menu) {
			base.OnCreateOptionsMenu(menu);

			MenuInflater.Inflate(Resource.Menu.refresh, menu);

			return true;
		}

		public override bool OnPrepareOptionsMenu(IMenu menu) {
			base.OnPrepareOptionsMenu(menu);

			var mi = menu.FindItem(Resource.Id.refresh);
			var tv = mi.ActionView as TextView;
			//			tv.Text = swiper.Refreshing ? GetString(Resource.String.refreshing) : GetString(Resource.String.refresh);
			mi.ActionView.SetOnClickListener(new ViewClickAction((view) => {
				OnRefresh();
			}));

			return true;
		}

		public override bool OnMenuItemSelected(int featureId, IMenuItem item) {
			switch (item.ItemId) {
				case Android.Resource.Id.Home:
					Finish();
					return true;
				default:
					return base.OnMenuItemSelected(featureId, item);
			}
		}

		public void OnRefresh() {
			QueryPendingAccessCodesAsync();
		}

		private void RequestConfirmAccess(int index, AccessCode code) {
			var adb = new IONAlertDialog(this);
			adb.SetTitle(Resource.String.portal_access_code_confirm);
			adb.SetMessage(Resource.String.portal_access_code_confirm_access);

			adb.SetNegativeButton(Resource.String.cancel, (sender, e) => {});
			adb.SetPositiveButton(Resource.String.ok, async (sender, e) => {
				var pd = new ProgressDialog(this);
				pd.SetTitle(Resource.String.portal_access_code_confirming);
				pd.SetMessage(GetString(Resource.String.please_wait));
				pd.SetCancelable(false);
				pd.Show();

				var response = await ion.portal.RequestConfirmAccessCodeAsync(code);
				if (response.success) {
					Toast.MakeText(this, Resource.String.portal_update_successful, ToastLength.Short).Show();
					adapter.RemoveRecord(index);
				} else {
					Toast.MakeText(this, Resource.String.portal_error_update_failed, ToastLength.Long).Show();
				}

				pd.Dismiss();
			});

			adb.Show();
		}

		private async void OnDelete(int index, AccessCode code) {
			var pd = new ProgressDialog(this);
			pd.SetTitle(Resource.String.portal_access_code_deleting);
			pd.SetMessage(GetString(Resource.String.please_wait));
			pd.SetCancelable(false);
			pd.Show();

      var response = await ion.portal.RequestDeleteAccessCodeAsync(code.code);
			if (response.success) {
				adapter.RemoveRecord(index);
			} else {
			}

			pd.Dismiss();
		}

		private async Task SubmitAccessCodeAsync(string code) {
			var pd = new ProgressDialog(this);
			pd.SetTitle(Resource.String.portal_access_submitting);
			pd.SetMessage(GetString(Resource.String.please_wait));
			pd.SetCancelable(false);
			pd.Indeterminate = true;
			pd.Show();

			var response = await ion.portal.SubmitAccessCodeAsync(code);

			if (response.success) {
			} else {
				// TODO ahodder@appioninc.com: localize
				Toast.MakeText(this, Resource.String.portal_error_access_code_submission_failed, ToastLength.Long).Show();
			}

			pd.Dismiss();
		}

		private async Task QueryPendingAccessCodesAsync() {
			swiper.Refreshing = true;
			InvalidateOptionsMenu();

			var results = await ion.portal.RequestPendingAccessCodesAsync();

			if (results.success) {
				var codes = new List<AccessCodeRecord>();
				foreach (var code in results.result) {
					codes.Add(new AccessCodeRecord(code));
				}
				adapter.Set(codes);
			} else {
				Toast.MakeText(this, Resource.String.portal_error_failed_to_query_access_codes, ToastLength.Short).Show();
			}

			swiper.Refreshing = false;
			InvalidateOptionsMenu();
		}

		private class Adapter : RecordAdapter {
			public Action<int, AccessCode> deleteAction;

			public override long GetItemId(int position) {
				return records[position].viewType;
			}

			public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType) {
				var rv = recyclerView as SwipeRecyclerView;
				var ret = new AccessCodeViewHolder(rv);
				return ret;
			}
		}

		private class AccessCodeRecord : RecordAdapter.Record<AccessCode> {
			// Implemented from SwipableRecyclerViewAdapter.IRecord
			public override int viewType { get { return 1; } }

			public AccessCodeRecord(AccessCode code) : base(code) { }
		}

    private class AccessCodeViewHolder : RecordAdapter.RecordViewHolder<AccessCodeRecord> {
			private TextView text;

      public AccessCodeViewHolder(SwipeRecyclerView rv) : base(rv, Resource.Layout.list_item_portal_connection) {
				text = ItemView.FindViewById<TextView>(Resource.Id.text);
			}

			public override void Invalidate() {
				text.Text = "(" + record.data.code + ") " + record.data.user;
			}
		}
	}
}
