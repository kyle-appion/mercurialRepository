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

	using ION.CoreExtensions.Net.Portal;

	using ION.Droid.Views;
	using ION.Droid.Widgets.RecyclerViews;

	[Activity(Label = "@string/portal_access_code_manager", Theme = "@style/TerminalActivityTheme", LaunchMode=Android.Content.PM.LaunchMode.SingleTask, ScreenOrientation=ScreenOrientation.Portrait)]
	public class PortalAccessCodeManagerActivity : IONActivity, SwipeRefreshLayout.IOnRefreshListener {

		private TextInputEditText entry;
		private SwipeRefreshLayout swiper;
		private RecyclerView list;
		private Adapter adapter;

		protected override void OnCreate(Bundle savedInstanceState) {
			base.OnCreate(savedInstanceState);

			ActionBar.SetDisplayHomeAsUpEnabled(true);

			SetContentView(Resource.Layout.activity_portal_access_code_manager);

			entry = FindViewById<TextInputEditText>(Resource.Id.entry);
			swiper = FindViewById<SwipeRefreshLayout>(Resource.Id.swiper);
			list = FindViewById<RecyclerView>(Resource.Id.list);

			FindViewById(Resource.Id.submit).Click += async (sender, e) => {
				await SubmitAccessCodeAsync(entry.Text);
			};

			FindViewById(Resource.Id.button).Click += async (sender, e) => {
				// TODO dialog
				var response = await ion.portal.GenerateAccessCodeAsync();
				if (response.success) {
					await QueryPendingAccessCodesAsync();
				} else {
					Toast.MakeText(this, Resource.String.portal_error_access_code_too_many, ToastLength.Long).Show();
				}
			};

			list.SetAdapter(adapter = new Adapter());
			adapter.emptyView = FindViewById(Resource.Id.empty);
			adapter.deleteAction = OnDelete;
			adapter.onItemClicked += (adapter, position) => {
				var record = adapter[position] as AccessCodeRecord;
				if (record.code.acceptId != 0) {
					RequestConfirmAccess(position, record.code);
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

		private async void RequestConfirmAccess(int index, AccessCode code) {
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

				var response = await ion.portal.ConfirmAccessCodeAsync(code);
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

			var response = await ion.portal.DeleteAccessCodeAsync(code.code);
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

			var results = await ion.portal.QueryPendingAccessCodesAsync();

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

		private class Adapter : SwipableRecyclerViewAdapter {
			public Action<int, AccessCode> deleteAction;

			public override long GetItemId(int position) {
				return records[position].viewType;
			}

			public override SwipableViewHolder OnCreateSwipableViewHolder(ViewGroup parent, int viewType) {
				var ret = new AccessCodeViewHolder(parent);
				return ret;
			}

			public override void OnBindViewHolder(SwipableRecyclerViewAdapter.IRecord record, SwipableViewHolder vh, int position) {
				var cvh = ((AccessCodeViewHolder)vh);
				cvh.record = record as AccessCodeRecord;
				cvh.button.Text = recyclerView.Context.GetString(Resource.String.remove);
			}

			public override bool IsViewHolderSwipable(SwipableRecyclerViewAdapter.IRecord record, SwipableViewHolder viewHolder, int index) {
				return deleteAction != null;
			}

			public override Action GetViewHolderSwipeAction(int index) {
				return () => {
					if (deleteAction != null) {
						var record = records[index] as AccessCodeRecord;
						deleteAction(index, record.code);
					}
				};
			}
		}

		private class AccessCodeRecord : SwipableRecyclerViewAdapter.IRecord {
			// Implemented from SwipableRecyclerViewAdapter.IRecord
			public int viewType { get { return 1; } }

			public AccessCode code { get; private set; }

			public AccessCodeRecord(AccessCode code) {
				this.code = code;
			}
		}

		private class AccessCodeViewHolder : SwipableViewHolder<AccessCodeRecord> {
			private TextView text;

			public AccessCodeViewHolder(ViewGroup parent) : base(parent, Resource.Layout.list_item_portal_connection) {
				text = view.FindViewById<TextView>(Resource.Id.text);
			}

			public override void OnBindTo() {
				text.Text = "(" + t.code.code + ") " + t.code.user;
			}
		}
	}
}
