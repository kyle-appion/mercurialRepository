namespace ION.Droid.Widgets.Adapters.Job {

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	using Android.App;
	using Android.Content;
	using Android.OS;
	using Android.Runtime;
	using Android.Views;
	using Android.Widget;

	using ION.Core.App;
	using ION.Core.Database;
	using ION.Core.Util;

	using ION.Droid.Widgets.RecyclerViews;

	public class SessionAdapter : SwipableRecyclerViewAdapter {

		public JobRow jobRow {
			get {
				return __jobRow;
			}

			set {
				__jobRow = value;
				NotifyDataSetChanged();
			}
		} JobRow __jobRow;

		private IION ion;

		public SessionAdapter(IION ion) {
			this.ion = ion;
		}

		public override void OnAttachedToRecyclerView(Android.Support.V7.Widget.RecyclerView recyclerView) {
			base.OnAttachedToRecyclerView(recyclerView);

			ion.database.onDatabaseEvent += OnDatabaseEvent;
		}

		public override void OnDetachedFromRecyclerView(Android.Support.V7.Widget.RecyclerView recyclerView) {
			base.OnDetachedFromRecyclerView(recyclerView);

			ion.database.onDatabaseEvent -= OnDatabaseEvent;
		}

		public override SwipableViewHolder OnCreateSwipableViewHolder(ViewGroup parent, int viewType) {
			switch ((EViewType)viewType) {
				case EViewType.Session:
					return new SessionViewHolder(parent, Resource.Layout.list_item_session);
				default:
					throw new Exception("Cannot create view for " + (EViewType)viewType);
			}
		}

		public override bool IsViewHolderSwipable(SwipableRecyclerViewAdapter.IRecord record, SwipableViewHolder viewHolder, int index) {
			return false;
		}

		public override Action GetViewHolderSwipeAction(int index) {
			return null;
		}

		public int IndexOfSession(SessionRow session) {
			for (int i = 0; i < ItemCount; i++) {
				var r = records[i] as SessionRecord;
				if (r?.row._id == session?._id) {
					return i;
				}
			}

			return -1;
		}

		public List<SessionRecord> GetCheckedSessions() {
			var ret = new List<SessionRecord>();

			foreach (var r in records) {
				var sr = r as SessionRecord;
				if (sr != null && sr.isChecked) {
					ret.Add(sr);
				}
			}

			return ret;
		}

		public void SetSessions(IEnumerable<SessionRecord> sessions) {
			records.Clear();

			records.AddRange(sessions);

			NotifyDataSetChanged();
		}

		public void SetSessions(IEnumerable<SessionRow> sessions) {
			var r = new List<SessionRecord>();

			foreach (var sr in sessions) {
				r.Add(new SessionRecord(sr));
			}

			SetSessions(r);
		}

		private void OnDatabaseEvent(IONDatabase db, DatabaseEvent e) {
		}
	}
}

