namespace ION.Droid.Widgets.RecyclerViews {

	using System;
	using System.Collections.Generic;

	using Android.Graphics;
	using Android.OS;
	using Android.Support.V7.Widget;
	using Android.Support.V7.Widget.Helper;
	using Android.Views;
	using Android.Widget;

	using ION.Droid.Views;

	public abstract class RecordAdapter : RecyclerView.Adapter {
		/// <summary>
		/// The event that is notified when the data set changes.
		/// </summary>
		public event Action<RecordAdapter> onDataSetChanged;

		/// <summary>
		/// The recycler view that this adapter is working within.
		/// </summary>
		/// <value>The recycler view.</value>
		public RecyclerView recyclerView { get; private set; }
		/// <summary>
		/// The view that will be shown if the adapter is empty.
		/// </summary>
		/// <value>The empty view.</value>
		public View emptyView {
			get {
				return __emptyView;
			}
			set {
				__emptyView = value;
				if (__emptyView != null) {
					NotifyDataSetChanged();
				}
			}
		} View __emptyView;

		// Overridden from RecyclerView.Adapter
		public sealed override int ItemCount { get { return records.Count; } }
		/// <summary>
		/// Indexes a record from the adapter.
		/// </summary>
		/// <param name="index">Index.</param>
		public IRecord this[int index] { get { return records[index]; } }
		/// <summary>
		/// The records that make up the data content for the adapter.
		/// </summary>
		protected readonly List<IRecord> records = new List<IRecord>();

		public RecordAdapter() {
			RegisterAdapterDataObserver(new InternalObserver(this));
		}

		// Overridden from RecyclerView.Adapter
		public override void OnAttachedToRecyclerView(RecyclerView recyclerView) {
			base.OnAttachedToRecyclerView(recyclerView);
			this.recyclerView = recyclerView;
			if (recyclerView.GetLayoutManager() == null) {
				recyclerView.SetLayoutManager(new LinearLayoutManager(recyclerView.Context));
			}
		}

		// Overridden from RecyclerView.Adapter
		public override void OnDetachedFromRecyclerView(RecyclerView recyclerView) {
			base.OnDetachedFromRecyclerView(recyclerView);
			this.recyclerView = null;
		}

		// Overridden from RecyclerView.Adapter
		public override long GetItemId(int position) {
			return position;
		}

		/// <summary>
		/// Queries the index of the given record.
		/// </summary>
		/// <returns>The of record.</returns>
		/// <param name="record">Record.</param>
		public int IndexOfRecord(IRecord record) {
			return records.IndexOf(record);
		}

		/// <summary>
		/// Clears and set the current records for the adapter.
		/// </summary>
		/// <param name="records">Records.</param>
		public void Set(IEnumerable<IRecord> records) {
			this.records.Clear();
			this.records.AddRange(records);
			NotifyDataSetChanged();
		}

		/// <summary>
		/// Removes the given record from the adapter.
		/// </summary>
		/// <param name="index">Index.</param>
		public void RemoveRecord(int index) {
			this.records.RemoveAt(index);
			this.NotifyItemRemoved(index);
		}

		/// <summary>
		/// Swaps the records at the given indices.
		/// </summary>
		/// <param name="i1">I1.</param>
		/// <param name="i2">I2.</param>
		/// <param name="animate">If set to <c>true</c> animate.</param>
		public void SwapRecords(int i1, int i2, bool animate=true) {
			var tmp = records[i1];
			records[i1] = records[i2];
			records[i2] = tmp;

			if (animate) {
				NotifyItemMoved(i1, i2);
			}
		}

		internal void NotifyChanged() {
			if (onDataSetChanged != null) {
				onDataSetChanged(this);
			}

			if (emptyView != null) {
				if (ItemCount > 0) {
					emptyView.Visibility = ViewStates.Gone;
					recyclerView.Visibility = ViewStates.Visible;
				} else {
					emptyView.Visibility = ViewStates.Visible;
					recyclerView.Visibility = ViewStates.Gone;
				}
			}
		}

		/// <summary>
		/// The contract for a record that will live in the recycler view.
		/// </summary>
		public interface IRecord {
			/// <summary>
			/// Queries the view type of the record. This is used for identification and record switching.
			/// </summary>
			/// <value>The type of the view.</value>
			int viewType { get; }
		}

		/// <summary>
		/// The observer that is used to perform notification callbacks for the adapter.
		/// </summary>
		private class InternalObserver : RecyclerView.AdapterDataObserver {
			private RecordAdapter adapter { get; set; }

			public InternalObserver(RecordAdapter adapter) {
				this.adapter = adapter;
			}

			/// <summary>
			/// Raises the changed event.
			/// </summary>
			public override void OnChanged() {
				adapter.NotifyChanged();
			}

			/// <summary>
			/// Raises the item range changed event.
			/// </summary>
			/// <param name="positionStart">Position start.</param>
			/// <param name="itemCount">Item count.</param>
			public override void OnItemRangeChanged(int positionStart, int itemCount) {
				adapter.NotifyChanged();
			}

			/// <summary>
			/// Raises the item range changed event.
			/// </summary>
			/// <param name="positionStart">Position start.</param>
			/// <param name="itemCount">Item count.</param>
			/// <param name="payload">Payload.</param>
			public override void OnItemRangeChanged(int positionStart, int itemCount, Java.Lang.Object payload) {
				adapter.NotifyChanged();
			}

			/// <summary>
			/// Raises the item range inserted event.
			/// </summary>
			/// <param name="positionStart">Position start.</param>
			/// <param name="itemCount">Item count.</param>
			public override void OnItemRangeInserted(int positionStart, int itemCount) {
				adapter.NotifyChanged();
			}

			/// <summary>
			/// Raises the item range moved event.
			/// </summary>
			/// <param name="fromPosition">From position.</param>
			/// <param name="toPosition">To position.</param>
			/// <param name="itemCount">Item count.</param>
			public override void OnItemRangeMoved(int fromPosition, int toPosition, int itemCount) {
				adapter.NotifyChanged();
			}

			/// <summary>
			/// Raises the item range removed event.
			/// </summary>
			/// <param name="positionStart">Position start.</param>
			/// <param name="itemCount">Item count.</param>
			public override void OnItemRangeRemoved(int positionStart, int itemCount) {
				adapter.NotifyChanged();
			}
		}
	}
}
