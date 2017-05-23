namespace ION.Droid.Activity.Portal {

	using System;
	using System.Collections.Generic;

	using Android.App;
	using Android.Content.PM;
	using Android.OS;
	using Android.Support.V7.Widget;
	using Android.Views;
	using Android.Widget;

	using ION.CoreExtensions.Net.Portal;

	using ION.Droid.Views;
	using ION.Droid.Widgets.RecyclerViews;

	[Activity(Label = "@string/portal_view_connections", Theme = "@style/TerminalActivityTheme", LaunchMode=Android.Content.PM.LaunchMode.SingleTask, ScreenOrientation=ScreenOrientation.Portrait)]
	public class PortalViewConnectionsActivity : IONActivity {

		private RecyclerView followingList;
		private RecyclerView followersList;

		private Adapter followingAdapter;
		private Adapter followerAdapter;

		protected override void OnCreate(Bundle savedInstanceState) {
			base.OnCreate(savedInstanceState);

			ActionBar.SetDisplayHomeAsUpEnabled(true);

			SetContentView(Resource.Layout.activity_portal_view_connections);

			var following = FindViewById(Resource.Id.portal_following);
			followingList = following.FindViewById<RecyclerView>(Resource.Id.list);
			var followingEmpty = following.FindViewById(Resource.Id.empty);

			var followers = FindViewById(Resource.Id.portal_followers);
			followersList = followers.FindViewById<RecyclerView>(Resource.Id.list);
			var followersEmpty = followers.FindViewById(Resource.Id.empty);


			followingList.SetAdapter(followingAdapter = new Adapter());
			followingAdapter.emptyView = followingEmpty;
			followingAdapter.deleteAction = OnDeleteFollowing;

			followersList.SetAdapter(followerAdapter = new Adapter());
			followerAdapter.emptyView = followersEmpty;
			followerAdapter.deleteAction = OnDeleteFollower;
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

		private async void OnDeleteFollower(int index, ConnectionData data) {
			var pd = new ProgressDialog(this);
			pd.SetTitle(Resource.String.deleting);
			pd.SetMessage(GetString(Resource.String.please_wait));
			pd.SetCancelable(false);
			pd.Show();

			var response = await ion.portal.RequestRemoveFollowerAsync(data);

			pd.Dismiss();
		}

		private async void OnDeleteFollowing(int index, ConnectionData data) {
			var pd = new ProgressDialog(this);
			pd.SetTitle(Resource.String.deleting);
			pd.SetMessage(GetString(Resource.String.please_wait));
			pd.SetCancelable(false);
			pd.Show();

			var response = await ion.portal.RequestRemoveFollowingAsync(data);

			pd.Dismiss();
		}

		/// <summary>
		/// Refreshes the content of the activity.
		/// </summary>
		private async void OnRefresh() {
      var result = await ion.portal.RequestConnectionData();

      if (result.success) {
        var tmp = new List<ConnectionRecord>();
        foreach (var data in ion.portal.followingConnections) {
          tmp.Add(new ConnectionRecord(data));
        }
        followingAdapter.Set(tmp);

        tmp.Clear();
        foreach (var data in ion.portal.followerConnections) {
          tmp.Add(new ConnectionRecord(data));
        }
        followerAdapter.Set(tmp);
      } else {
        Error(GetString(Resource.String.portal_error_failed_to_query_following));
      }
		}

		private class Adapter : RecordAdapter {

			public Action<int, ConnectionData> deleteAction;

			public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType) {
				var rv = recyclerView as SwipeRecyclerView;
				var ret = new ConnectionViewHolder(rv);
				return ret;
			}
		}

		private class ConnectionRecord : RecordAdapter.Record<ConnectionData> {
			// Implemented from SwipableRecyclerViewAdapter.IRecord
			public override int viewType { get { return 1; } }

			public ConnectionRecord(ConnectionData data) : base(data) { }
		}

		private class ConnectionViewHolder : RecordAdapter.SwipeRecordViewHolder<ConnectionRecord> {
			private TextView text;

			public ConnectionViewHolder(SwipeRecyclerView rv) : base(rv, Resource.Layout.list_item_portal_connection, Resource.Layout.list_item_button) {
				text = foreground.FindViewById<TextView>(Resource.Id.text);
			}

			public override void Invalidate() {
				text.Text = "(" + record.data.email + ") " + record.data.displayName;
			}
		}
	}
}
