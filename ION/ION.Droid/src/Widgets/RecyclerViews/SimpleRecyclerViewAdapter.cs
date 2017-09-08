namespace ION.Droid.Widgets.RecyclerViews {

	using System;
	using System.Collections.Generic;

	using Android.Support.V7.Widget;
	using Android.Views;

	public abstract class SimpleRecyclerViewAdapter : RecyclerView.Adapter {

		// Overridden from RecyclerView.Adapter
		public override int ItemCount { get { return records.Count; } }

		/// <summary>
		/// Indexes the record at the given position.
		/// </summary>
		/// <param name="position">Position.</param>
		public IRecord this[int position] { get { return records[position]; } }

		/// <summary>
		/// The internal list of records that are present in the adapter.
		/// </summary>
		protected List<IRecord> records;


		public SimpleRecyclerViewAdapter() {
			records = new List<IRecord>();
		}

		// Overridden from SimpleRecyclerViewAdapter
		public override sealed RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType) {
			var ret = OnCreateSimpleViewHolder(parent, viewType);
			return ret;
		}

		// Overridden from SimpleRecyclerViewAdapter
		public override sealed void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position) {
			var svh = viewHolder as SimpleViewHolder;

			svh.record = records[position];

			OnBindSimpleViewHolder(svh, position);
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
		/// Swaps the two records.
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
		/// <summary>
		/// Removes all of the records from the recycleriew.
		/// </summary>
		public void ClearRecords() {
			records.Clear();
			NotifyDataSetChanged();
		}


		/// <summary>
		/// Called when the adapter needs to create a new view holder.
		/// </summary>
		/// <returns>The create simple view holder.</returns>
		/// <param name="parent">Parent.</param>
		/// <param name="viewType">View type.</param>
		protected abstract SimpleViewHolder OnCreateSimpleViewHolder(ViewGroup parent, int viewType);
		/// <summary>
		/// Called when the adapter is binding a record to the simple view holder. At the point of this call, the record
		/// has already been bound to the view holder. The only reason to override this method is if you wish to perform
		/// more operations after the bind.
		/// </summary>
		/// <param name="vh">Vh.</param>
		/// <param name="position">Position.</param>
		protected void OnBindSimpleViewHolder(SimpleViewHolder vh, int position) {
			vh.record = records[position];
			vh.OnBind();
		}


		/// <summary>
		/// A simple abstraction of the type of data that will exist in the adapter.
		/// </summary>
		public interface IRecord {
			int viewType { get; }
		}

		/// <summary>
		/// A simple implementation of a record that should meet almost all of the needs of a record.
		/// </summary>
		public class SimpleRecord<T> : IRecord {
			// Implemented from SimpleRecyclerViewAdapter.IRecord
			public int viewType { get; private set; }
			/// <summary>
			/// The data that the record holds.
			/// </summary>
			public T item;

			public SimpleRecord(T item, int viewType) {
				this.item = item;
				this.viewType = viewType;
			}
		}

		/// <summary>
		/// The base view holder that is present in the recycler view.
		/// </summary>
		public abstract class SimpleViewHolder : RecyclerView.ViewHolder {
			/// <summary>
			/// The record that is driving the content of this view holder.
			/// </summary>
			/// <value>The record.</value>
			public IRecord record { get; internal set; }
			/// <summary>
			/// Whether or not the view holder should be allowed to be dragged.
			/// </summary>
			/// <value><c>true</c> if is draggale; otherwise, <c>false</c>.</value>
			public abstract bool isDraggable { get; }
			/// <summary>
			/// Whether or not the view holder should be allowed to be swiped.
			/// </summary>
			/// <value><c>true</c> if is swipable; otherwise, <c>false</c>.</value>
			public abstract bool isSwipable { get; }

			public SimpleViewHolder(View parent) : base(parent) {
			}

			/// <summary>
			/// Called when the view holder should bind itself (update) to its content data record.
			/// </summary>
			public abstract void OnBind();

			/// <summary>
			/// Called when the view holder is being removed from the recycler view. This is where you should clean up any
			/// listeners.
			/// </summary>
			public virtual void OnUnbind() {
			}
		}

		/// <summary>
		/// A typed version of the SimpleViewHolder. This is the version that almost every subclass should use.
		/// </summary>
		public abstract class SimpleViewHolder<T> : SimpleViewHolder where T : IRecord {
			/// <summary>
			/// Gets the type cast record.
			/// </summary>
			/// <value>The item.</value>
			public T typeRecord { get { return (T)record; } }

			public SimpleViewHolder(View view) : base(view) {
			}

			public SimpleViewHolder(ViewGroup parent, int viewResource) : base(BuildContentView(parent, viewResource)) {
			}

			private static View BuildContentView(ViewGroup parent, int contentResource) {
				if (contentResource == 0) {
					throw new Exception("Cannot build " + typeof(SimpleViewHolder).Name + " without a content view");
				}

				return LayoutInflater.From(parent.Context).Inflate(contentResource, parent, true);
			}
		}


		/// <summary>
		/// The base view holder that is present in the recycler view.
		/// </summary>
		public abstract class SimpleSwipableViewHolder<T> : SimpleViewHolder<T> where T : IRecord {
			// Overridden from SimpleViewHolder
			public override bool isSwipable { get { return background == null; } }

			/// <summary>
			/// The content view.
			/// </summary>
			protected View content;
			/// <summary>
			/// The view that is exposed on swipe.
			/// </summary>
			protected View background;

			public SimpleSwipableViewHolder(ViewGroup parent, int contentResource, int backgroundResource=0) : base(BuildContentView(parent, contentResource, backgroundResource)) { 
				if (backgroundResource == 0) {
					content = ItemView;
				} else {
					content = ItemView.FindViewById(Resource.Id.content);
					background = ItemView.FindViewById(Resource.Id.background);
				}
      }

			private static View BuildContentView(ViewGroup parent, int contentResource, int backgroundResource) {
				if (contentResource == 0) {
					throw new Exception("Cannot build " + typeof(SimpleViewHolder).Name + " without a content view");
				}

				if (backgroundResource == 0) {
					return LayoutInflater.From(parent.Context).Inflate(contentResource, parent, true);
				} else {
					var root = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.list_item_simple_view_holder, parent, true);

					var contentContainer = root.FindViewById<ViewGroup>(Resource.Id.content);
					var backgroundContainer = root.FindViewById<ViewGroup>(Resource.Id.background);

					var content = LayoutInflater.From(parent.Context).Inflate(contentResource, contentContainer, true);
					var background = LayoutInflater.From(parent.Context).Inflate(backgroundResource, backgroundContainer, true);

					return root;
				}
			}
		}
	}
}
