namespace ION.Droid.Activity.Portal {

	using System;

	using Android.Widget;

	using ION.CoreExtensions.Net.Portal;

	using ION.Droid.Widgets.RecyclerViews;

	public class PortalRemoteViewingRecord : RecordAdapter.Record<ConnectionData> {
		// Overridden from RecordAdapter.Record
		public override int viewType { get { return (int)EViewType.RemoteViewingItem; } }

		public bool isChecked;
		public bool isBeingDownloaded;

		public PortalRemoteViewingRecord(ConnectionData data) : base(data) {
		}
	}

  public class PortalRemoteViewingViewHolder : RecordAdapter.RecordViewHolder<PortalRemoteViewingRecord> {
    private TextView account;
		private TextView name;
		private TextView status;
		private CheckBox check;

		public PortalRemoteViewingViewHolder(SwipeRecyclerView rv) : base(rv, Resource.Layout.list_portal_remote_view_item) {
			account = ItemView.FindViewById<TextView>(Resource.Id.account);
			name = ItemView.FindViewById<TextView>(Resource.Id.name);
			status = ItemView.FindViewById<TextView>(Resource.Id.status);
			check = ItemView.FindViewById<CheckBox>(Resource.Id.check);
			check.Enabled = false;
			check.Focusable = false;
/*
			var delete = background as TextView;
			delete.SetText(Resource.String.delete);
*/
		}

		public override void Invalidate() {
			var c = ItemView.Context;
			base.Invalidate();

			account.Text = record.data.displayName;
      name.Text = record.data.deviceName;

			if (record.isBeingDownloaded) {
				status.SetText(Resource.String.portal_viewing);
				status.SetTextColor(c.Resources.GetColor(Resource.Color.blue));
			} else if (record.data.isUserOnline) {
				status.SetText(Resource.String.portal_viewable);
				status.SetTextColor(c.Resources.GetColor(Resource.Color.green));
			} else {
				status.SetText(Resource.String.portal_offline);
				status.SetTextColor(c.Resources.GetColor(Resource.Color.red));
			}

			check.Checked = record.isChecked;
		}
	}
}
