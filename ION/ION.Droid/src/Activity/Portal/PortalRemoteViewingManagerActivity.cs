namespace ION.Droid.Activity.Portal {

	using System;
	using System.Threading.Tasks;

	using Android.App;
	using Android.Content.PM;
	using Android.Graphics.Drawables;
	using Android.OS;
	using Android.Support.V4.Widget;
	using Android.Views;
	using Android.Widget;

	using Appion.Commons.Util;

	using ION.Droid.App;
	using ION.Droid.Views;
	using ION.Droid.Widgets.RecyclerViews;

	[Activity(Label="@string/portal", Theme="@style/TerminalActivityTheme", LaunchMode=Android.Content.PM.LaunchMode.SingleInstance, ScreenOrientation=ScreenOrientation.Portrait)]
	public class PortalRemoteViewingManagerActivity : IONActivity, SwipeRefreshLayout.IOnRefreshListener {

		private SwipeRefreshLayout refresh;
		private SwipeRecyclerView list;
		private Button button;

		private PortalRemoteViewingAdapter adapter;

		// Overridden from Activity
		protected override void OnCreate(Bundle savedInstanceState) {
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.activity_portal_remote_viewing_manager);

			ActionBar.SetDisplayHomeAsUpEnabled(true);
			ActionBar.SetIcon(GetColoredDrawable(Resource.Drawable.ic_cloud, Resource.Color.gray));

			refresh = FindViewById<SwipeRefreshLayout>(Resource.Id.refresh);
			list = FindViewById<SwipeRecyclerView>(Resource.Id.list);
			button = FindViewById<Button>(Resource.Id.button);
			button.Visibility = ViewStates.Invisible;
			button.Click += async (sender, e) => {
				var ri = ion as RemoteION;
				var i = adapter.IndexOfRecord(adapter.checkedRecord);

        if (ri != null && ri.connectionData.Equals(adapter.checkedRecord.data)) {
					await StartLocalIONAsync();
				} else {
					await StartRemoteIONAsync(adapter.checkedRecord.data);
				}

				ion.PostToMainDelayed(() => {
					adapter.NotifyDataSetChanged();
					var r = adapter[i] as PortalRemoteViewingRecord;
					if (r.isBeingDownloaded) {
						button.SetText(Resource.String.portal_remote_mode);
					} else {
						button.SetText(Resource.String.portal_local_mode);
					}
					if (ion is RemoteION) {
						ActionBar.SetBackgroundDrawable(new ColorDrawable(Resources.GetColor(Resource.Color.red)));
					} else {
						ActionBar.SetBackgroundDrawable(new ColorDrawable(Resources.GetColor(Android.Resource.Color.BackgroundDark)));
					}
				}, TimeSpan.FromMilliseconds(500));
			};

			adapter = new PortalRemoteViewingAdapter();
			adapter.emptyView = FindViewById(Resource.Id.empty);
			adapter.onRecordCheckChange = (obj) => {
				var ri = ion as RemoteION;
				var r = adapter[obj] as PortalRemoteViewingRecord;
				if (r.isChecked) {
          if (ri != null && ri.connectionData.id == r.data.id) {
						button.SetText(Resource.String.portal_local_mode);
					} else {
						button.SetText(Resource.String.portal_remote_mode);
					}
					button.Visibility = ViewStates.Visible;
				} else {
					button.Visibility = ViewStates.Invisible;
				}
			};

			refresh.SetOnRefreshListener(this);
			list.SetAdapter(adapter);
		}

		protected override void OnResume() {
			base.OnResume();
			GetContactsAsync();
      if (ion is RemoteION) {
        button.SetText(Resource.String.portal_local_mode);
      }
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
			if (ion.portal.isUploading) {
				tv.SetText(Resource.String.portal_stop_uploading);
			} else {
				tv.SetText(Resource.String.portal_upload_layout);
			}
			tv.SetOnClickListener(new ViewClickAction((view) => {
				ToggleUploading();
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

		public void OnRefresh() {
      Task.Factory.StartNew(GetContactsAsync);
		}

		private async Task GetContactsAsync() {
			refresh.Refreshing = true;

      var result = await ion.portal.RequestConnectionData();
			if (result.success) {
        adapter.SetContent(ion.portal.onlineConnections);
			} else {
				Log.E(this, result.message);
				Toast.MakeText(this, Resource.String.portal_error_failed_to_query_following, ToastLength.Long).Show();
			}

			await Task.Delay(TimeSpan.FromSeconds(1.5));

			refresh.Refreshing = false;
		}

		private void ToggleUploading() {
			if (ion.portal.isUploading) {
				ion.portal.EndAppStateUpload();
			} else {
				ion.portal.BeginAppStateUpload(ion);
			}
			InvalidateOptionsMenu();
			adapter.NotifyDataSetChanged();
		}
	}
}
