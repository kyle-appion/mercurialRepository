using ION.Droid.Views;
namespace ION.Droid.Activity.Job {

	using System;
	using System.Collections.Generic;
	using System.Linq;


	using Android.App;
	using Android.Support.V7.Widget;
	using Android.Views;
	using Android.Widget;

	using Appion.Commons.Util;

	using ION.Core.App;
	using ION.Core.Database;

	using Dialog;
	using ION.Droid.Widgets.RecyclerViews;

	public class SessionAdapter : RecordAdapter {

		public event EventHandler<SessionRecord> onSessionRowChecked;

		private IION ion;
		/// <summary>
		/// The job row that the sessions adapter is with regard to. Meaning, if a session is added to a job, this is the
		/// job that the sessions will be added to.
		/// </summary>
		/// <value>The job row.</value>
		public JobRow jobRow {
			get {
				return __jobRow;
			}
			set {
				__jobRow = value;
				foreach (var record in records) {
					var sr = record as SessionRecord;
					if (sr != null) {
						sr.job = value;
					}
				}
				NotifyDataSetChanged();
			}
		} JobRow __jobRow;

		/// <summary>
		/// Whether or not the adapter will allow a session record to be deleted.
		/// </summary>
		/// <value><c>true</c> if allow deleting; otherwise, <c>false</c>.</value>
		public bool allowDeleting { get; set; }

		public SessionAdapter(IION ion) {
			this.ion = ion;
			allowDeleting = true;
		}

		public override void OnAttachedToRecyclerView(RecyclerView recyclerView) {
			base.OnAttachedToRecyclerView(recyclerView);

			ion.database.onDatabaseEvent += OnDatabaseEvent;
			this.onItemClicked += OnItemClicked;
		}

		public override void OnDetachedFromRecyclerView(RecyclerView recyclerView) {
			base.OnDetachedFromRecyclerView(recyclerView);

			ion.database.onDatabaseEvent -= OnDatabaseEvent;
			this.onItemClicked -= OnItemClicked;
		}

		public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType) {
			switch ((EViewType)viewType) {
				case EViewType.Session:
					var ret = new SessionViewHolder(parent);
				return ret;
				default:
					throw new Exception("Cannot create view for " + (EViewType)viewType);
			}
		}

		private void OnItemClicked(int pos) {
			var r = records[pos] as SessionRecord;
			var vh = recyclerView.FindViewHolderForAdapterPosition(pos) as SessionViewHolder;
			r.isChecked = !r.isChecked;
			vh.check.Checked = r.isChecked;
			onSessionRowChecked(this, r);
		}

		private void RequestDeleteSession(SessionRecord record) {
			var context = recyclerView.Context;
			var adb = new IONAlertDialog(context);
			adb.SetTitle(Resource.String.report_delete_session);
			adb.SetMessage(Resource.String.report_delete_session_message);

			adb.SetNegativeButton(Resource.String.cancel, (sender, e) => {
				var d = sender as Dialog;
				if (d != null) {
					d.Cancel();
				}
			});
			adb.SetPositiveButton(Resource.String.delete, async (sender, e) => {
				var pd = new ProgressDialog(context);
				pd.SetTitle(Resource.String.please_wait);
				pd.SetMessage(context.GetString(Resource.String.deleting));
				pd.Indeterminate = true;
				pd.Show();

				// First we have to delete all the records
				var ion = AppState.context;
				var database = AppState.context.database;

				try {
					database.BeginTransaction();

					var results = database.Table<SensorMeasurementRow>().Delete(smr => smr.frn_SID == record.data._id);
					Log.D(this, "Deleted " + results + " sensor measurement rows");
					database.Commit();

					Log.D(this, "Deleted session: " + record.data._id + " = " + await AppState.context.database.DeleteAsync<SessionRow>(record.data));
					var index = records.IndexOf(record);
					records.RemoveAt(index);
					NotifyItemRemoved(index);
				} catch (Exception ex) {
					Log.E(this, "Failed to delete records", ex);
					database.Rollback();
					Toast.MakeText(context, Resource.String.error_failed_to_delete, ToastLength.Short).Show();
				} finally {
					pd.Dismiss();
				}
			});

			adb.Show();
		}

		public int IndexOfSession(SessionRow session) {
			for (int i = 0; i < ItemCount; i++) {
				var r = records[i] as SessionRecord;
				if (r?.data._id == session?._id) {
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

			foreach (var session in sessions) {
				session.job = jobRow;
				records.Add(session);
			}

			NotifyDataSetChanged();
		}

		public void SetSessions(IEnumerable<SessionRow> sessions) {
			var r = new List<SessionRecord>();

			foreach (var sr in sessions) {
				var table = ion.database.Table<SensorMeasurementRow>();
				var query = table.Where(smr => smr.frn_SID == sr._id)
				                 .GroupBy(smr => smr.serialNumber);

				var count = query.Count();

				r.Add(new SessionRecord(sr, count));
			}

			SetSessions(r);
		}

		private void OnDatabaseEvent(IONDatabase db, DatabaseEvent e) {
		}
	}
}

