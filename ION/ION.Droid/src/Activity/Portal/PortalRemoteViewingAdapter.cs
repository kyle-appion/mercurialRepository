using ION.Core.App;
namespace ION.Droid.Activity.Portal {

	using System;
	using System.Collections.Generic;

	using Android.Support.V7.Widget;
	using Android.Views;
	using Android.Widget;

	using ION.CoreExtensions.Net.Portal;

	using ION.Droid.App;
	using ION.Droid.Widgets.RecyclerViews;

	public class PortalRemoteViewingAdapter : RecordAdapter {

		public Action<int> onRecordCheckChange;

		public PortalRemoteViewingRecord checkedRecord { get; private set; }

		private DividerItemDecoration decor;

		public PortalRemoteViewingAdapter() {
		}

		// Overridden from RecordAdapter
		public override void OnAttachedToRecyclerView(RecyclerView recyclerView) {
			base.OnAttachedToRecyclerView(recyclerView);

			if (decor == null) {
				decor = new DividerItemDecoration(recyclerView.Context, DividerItemDecoration.Vertical);
				recyclerView.AddItemDecoration(decor);
			}
		}

		// Overridden from RecordAdapter
		public override void OnDetachedFromRecyclerView(RecyclerView recyclerView) {
			base.OnDetachedFromRecyclerView(recyclerView);
			if (decor != null) {
				recyclerView.RemoveItemDecoration(decor);
			}
		}

		// Overridden from RecordAdapter
		public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType) {
			return new PortalRemoteViewingViewHolder(recyclerView as SwipeRecyclerView);
		}

		public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position) {
			var ion = AppState.context as RemoteION;
			var r = records[position] as PortalRemoteViewingRecord;

			if (ion != null) {
        r.isBeingDownloaded = r.data.id == ion.connectionData.id;
			} else {
				r.isBeingDownloaded = false;
			}
			base.OnBindViewHolder(holder, position);
		}

		protected override bool OnInterceptItemClicked(int position) {
			var record = records[position] as PortalRemoteViewingRecord;

			if (!record.data.isUserOnline) {
				Toast.MakeText(recyclerView.Context, Resource.String.portal_error_remote_access_offline, ToastLength.Long).Show();
				return true;
			}

			if (checkedRecord != null && record != checkedRecord) {
				checkedRecord.isChecked = false;
				NotifyItemChanged(IndexOfRecord(checkedRecord));
			}
			record.isChecked = !record.isChecked;
			checkedRecord = record;
			NotifyItemChanged(position);

			if (onRecordCheckChange != null) {
				onRecordCheckChange(position);
			}
			return true;
		}

		/// <summary>
		/// Sets the connection data as the content of the list.
		/// </summary>
		/// <param name="content">Content.</param>
		public void SetContent(IEnumerable<ConnectionData> content) {
			var ion = AppState.context as BaseAndroidION;
			var newRecords = new List<IRecord>();

			foreach (var item in content) {
        newRecords.Add(new PortalRemoteViewingRecord(item));
			}

			Set(newRecords);
		}
	}
}
