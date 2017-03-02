﻿namespace ION.Droid.Activity.Portal {

	using System;
	using System.Threading.Tasks;

	using Android.App;
	using Android.Content.PM;
	using Android.OS;
	using Android.Views;
	using Android.Widget;

	using Appion.Commons.Util;

	using ION.Droid.App;
	using ION.Droid.Views;
	using ION.Droid.Widgets.RecyclerViews;

	[Activity(Label="@string/portal", Theme="@style/TerminalActivityTheme", LaunchMode=Android.Content.PM.LaunchMode.SingleInstance, ScreenOrientation=ScreenOrientation.Portrait)]
	public class PortalRemoteViewingManagerActivity : IONActivity {

		private SwipeRecyclerView list;
		private Button button;

		private PortalRemoteViewingAdapter adapter;

		// Overridden from Activity
		protected override void OnCreate(Bundle savedInstanceState) {
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.activity_portal_remote_viewing_manager);

			ActionBar.SetDisplayHomeAsUpEnabled(true);
			ActionBar.SetIcon(GetColoredDrawable(Resource.Drawable.ic_cloud, Resource.Color.gray));

			list = FindViewById<SwipeRecyclerView>(Resource.Id.list);
			button = FindViewById<Button>(Resource.Id.button);
			button.Visibility = ViewStates.Invisible;
			button.Click += async (sender, e) => {
				var ri = ion as RemoteION;
				var i = adapter.IndexOfRecord(adapter.checkedRecord);

				if (ri != null && ri.userId.Equals(adapter.checkedRecord.data.id + "")) {
					await StartLocalIONAsync();
				} else {
					await StartRemoteIONAsync(adapter.checkedRecord.data.id + "");
				}

				ion.PostToMainDelayed(() => {
					adapter.NotifyDataSetChanged();
				}, TimeSpan.FromMilliseconds(500));
			};

			adapter = new PortalRemoteViewingAdapter();
			adapter.emptyView = FindViewById(Resource.Id.empty);
			adapter.onRecordCheckChange = (obj) => {
				var ri = ion as RemoteION;
				var r = adapter[obj] as PortalRemoteViewingRecord;
				if (r.isChecked) {
					if (ri != null && ri.userId.Equals(r.data.id + "")) {
						button.SetText(Resource.String.portal_local_mode);
					} else {
						button.SetText(Resource.String.portal_remote_mode);
					}
					button.Visibility = ViewStates.Visible;
				} else {
					button.Visibility = ViewStates.Invisible;
				}
			};

			list.SetAdapter(adapter);
		}

		protected override void OnResume() {
			base.OnResume();
			GetContacts();
		}

		public override bool OnCreateOptionsMenu(IMenu menu) {
			base.OnCreateOptionsMenu(menu);

			MenuInflater.Inflate(Resource.Menu.button, menu);

			return true;
		}

		public override bool OnPrepareOptionsMenu(IMenu menu) {
			base.OnPrepareOptionsMenu(menu);

			var item = menu.FindItem(Resource.Id.button);
			var tv = item.ActionView as TextView;
			tv.SetText(Resource.String.portal_upload_layout);
			tv.SetOnClickListener(new ViewClickAction((view) => {
/*
				if (ion is RemoteION) {
					if (await StartLocalIONAsync()) {
						tv.SetText(Resource.String.portal_remote_mode_start);
					}
				} else {
					if (await StartRemoteIONAsync()) {
						tv.SetText(Resource.String.portal_remote_mode_end);
					}
				}
*/
			}));

			return true;
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

		private async Task GetContacts() {
			var pd = new ProgressDialog(this);
			pd.Indeterminate = true;
			pd.SetTitle(Resource.String.loading);
			pd.SetMessage(GetString(Resource.String.please_wait));
			pd.Show();

			var result = await ion.portal.QueryFollowingAsync();
			if (result.success) {
				adapter.SetContent(result.result);
			} else {
				Log.E(this, result.message);
				Toast.MakeText(this, Resource.String.portal_error_failed_to_query_following, ToastLength.Long).Show();
			}

			pd.Dismiss();
		}
	}
}
