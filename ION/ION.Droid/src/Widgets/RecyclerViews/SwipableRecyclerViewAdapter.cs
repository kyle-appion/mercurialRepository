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

  /// <summary>
  /// A recycler view adapter that provides features that are not default in a recycler view, such as swipe to delete
  /// and other touch/convenience interactions. 
  /// </summary>
	public abstract class SwipableRecyclerViewAdapter : IONRecyclerViewAdapter, SimpleDragSwipeHandler.ICallback {
    /// <summary>
    /// The delegate that will handle item clicks.
    /// </summary>
    public delegate void OnItemClicked(SwipableRecyclerViewAdapter adapter, int position);

    /// <summary>
    /// Occurs when an item is clicked.
    /// </summary>
    public event OnItemClicked onItemClicked;

    /// <summary>
    /// The number of records that are present in the adapter.
    /// </summary>
    /// <value>The item count.</value>
    public sealed override int ItemCount {
      get {
        return records.Count;
      }
    }

    /// <summary>
    /// An indexer that will return the record from the given index.
    /// </summary>
    /// <param name="index">Index.</param>
    public IRecord this[int index] {
      get {
        return records[index];
      }
    }

		// Implemented from SimpleDragSwipeHandler.ICallback
		public virtual bool allowDragging { get { return false; } }
		// Implemented from SimpleDragSwipeHandler.ICallback
		public virtual bool allowSwiping { get { return true; } }

    
    /// <summary>
    /// The delay that is applied when a swipe event is performed. After this delay, the swiped action will be commited.
    /// </summary>
    /// <value>The swipe confirm timeout.</value>
    public long swipeConfirmTimeout { get; set; }

    /// <summary>
    /// The records that the recycler view will display.
    /// </summary>
    protected readonly List<IRecord> records = new List<IRecord>();

    /// <summary>
    /// The item decorator that will draw the action button behind a swiped view.
    /// </summary>
		protected ItemTouchHelper touchHelperDecoration { get; set; }
    /// <summary>
    /// The item decorator that will resolve item swipes.
    /// </summary>
    private RecyclerView.ItemDecoration swipeDecoration;

    public SwipableRecyclerViewAdapter() : base() {
    }

    public override void OnAttachedToRecyclerView(RecyclerView recyclerView) {
      base.OnAttachedToRecyclerView(recyclerView);
			recyclerView.GetItemAnimator().ChangeDuration = 0;
			touchHelperDecoration = new ItemTouchHelper(new SimpleDragSwipeHandler(recyclerView, this));
      touchHelperDecoration.AttachToRecyclerView(recyclerView);
    }

    public override void OnDetachedFromRecyclerView(RecyclerView recyclerView) {
      base.OnDetachedFromRecyclerView(recyclerView);
			recyclerView.RemoveItemDecoration(touchHelperDecoration);
    }

		public override long GetItemId(int position) {
			return position;
		}

    public override sealed RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType) {
      var ret = OnCreateSwipableViewHolder(parent, viewType);
			ret.helper = this.touchHelperDecoration;

      ret.ItemView.Click += (sender, e) => {
        if (onItemClicked != null) {
          onItemClicked(this, ret.AdapterPosition);
        }
      };

      return ret;
    }

    /// <summary>
    /// Raises the bind view holder event.
    /// </summary>
    /// <param name="holder">Holder.</param>
    /// <param name="position">Position.</param>
    public override sealed void OnBindViewHolder(RecyclerView.ViewHolder holder, int position) {
      var record = records[position];
      var vh = holder as SwipableViewHolder;

      if (vh == null) {
        throw new Exception("IONRecyclerViewAdapter cannot accept viewholder's that do not extend IONRecyclerViewHolder");
      }

      OnBindViewHolder(record, vh, position);
/*
      if (pendingActions.ContainsKey(record)) {
        vh.ItemView.SetBackgroundColor(backgroundColor);
        vh.button.SetOnClickListener(new ViewClickAction((v) => {
          Action action = GetViewHolderSwipeAction(position);
          if (action != null) {
            action();
          }
					if (pendingActions.ContainsKey(record)) {
          	handler.RemoveCallbacks(pendingActions[record]);
						pendingActions.Remove(record);

					}
          NotifyItemChanged(position);
        }));

      } else {
        vh.ItemView.SetBackgroundColor(Color.Transparent);
        vh.ItemView.Visibility = ViewStates.Visible;
      }
*/
    }

		// Implemented from SimpleDragSwipeHandler.ICallback
		public virtual bool IsSwipable(int position) {
			return false;
		}

		// Implemented from SimpleDragSwipeHandler.ICallback
		public virtual bool IsDraggable(RecyclerView.ViewHolder viewHolder) {
			return false;
		}

		// Implemented from SimpleDragSwipeHandler.ICallback
		public virtual void StartingDrag(RecyclerView.ViewHolder viewHolder) {
		}

		// Implemented from SimpleDragSwipeHandler.ICallback
		public virtual void EndingDrag(RecyclerView.ViewHolder viewHolder) {
		}

		// Implemented from SimpleDragSwipeHandler.ICallback
		public virtual bool WillAcceptDrop(RecyclerView.ViewHolder viewHolder, RecyclerView.ViewHolder dropTarget) {
			return false;
		}

		// Implemented from SimpleDragSwipeHandler.ICallback
		public void OnRecordsSwapped(int i1, int i2) {
			SwapRecords(i1, i2, false);
		}

    /// <summary>
    /// Queries the record at the given index.
    /// </summary>
    /// <returns>The <see cref="SwipableRecyclerViewAdapter+IRecord"/>.</returns>
    /// <param name="index">Index.</param>
    public IRecord GetRecordAt(int index) {
      if (index >= 0 && index < records.Count) {
        return records[index];
      } else {
        return null;
      }
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

    /// <summary>
    /// Called when the adapter needs a new view holder.
    /// </summary>
    /// <param name="parent">Parent.</param>
    /// <param name="viewType">View type.</param>
    public abstract SwipableViewHolder OnCreateSwipableViewHolder(ViewGroup parent, int viewType);

    /// <summary>
    /// Binds the given record to the view holder.
    /// </summary>
    /// <param name="record">Record.</param>
    /// <param name="vh">Vh.</param>
    /// <param name="position">Position.</param>
    public virtual void OnBindViewHolder(IRecord record, SwipableViewHolder vh, int position) {
      vh.record = record;
    }

		public override int GetItemViewType(int position) {
			return (int)records[position].viewType;
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
  }

  /// <summary>
  /// The base view holder that will be used for the IONRecyclerViewAdapter.
  /// </summary>
  public class SwipableViewHolder : RecyclerView.ViewHolder {
    /// <summary>
    /// The record that is associated to the view holder.
    /// </summary>
    /// <value>The record.</value>
    public SwipableRecyclerViewAdapter.IRecord record {
      get {
        return __record;
      }
      set {
        if (__record != null) {
          Unbind();
        }

        __record = value;

        if (__record != null) {
          OnBindTo();
        }
      }
    } protected SwipableRecyclerViewAdapter.IRecord __record;

		public ItemTouchHelper helper;

    /// <summary>
    /// The inflated content view.
    /// </summary>
    protected View view;
    /// <summary>
    /// The internal content view that the inflated view resource lives.
    /// </summary>
    internal LinearLayout content;
    /// <summary>
    /// The button that is revealed when the view holder is swiped.
    /// </summary>
    internal Button button;

    private bool useSwipeLayout;

    public SwipableViewHolder(ViewGroup parent, int viewResource, bool useSwipeParent=true) : base(BuildContentView(parent, viewResource, useSwipeParent)) {
      useSwipeLayout = useSwipeParent;
      if (useSwipeParent) {
        content = ItemView.FindViewById<LinearLayout>(Resource.Id.content);
        view = LayoutInflater.From(parent.Context).Inflate(viewResource, content, true);
        button = ItemView.FindViewById<Button>(Resource.Id.button);
				content.SetOnLongClickListener(new ViewLongClickAction((view) => {;
					this.helper.StartDrag(this);
				}));
      }
    }

    public virtual void OnBindTo() {
    }

    public virtual void Unbind() {
    }


    private static View BuildContentView(ViewGroup parent, int viewResource, bool useSwipeParent) {
      if (useSwipeParent) {
        return LayoutInflater.From(parent.Context).Inflate(Resource.Layout.list_item_ion_recycler_view_holder, parent, false);
      } else {
        return LayoutInflater.From(parent.Context).Inflate(viewResource, parent, false);
      }
    }
  }

  public class SwipableViewHolder<T> : SwipableViewHolder where T : SwipableRecyclerViewAdapter.IRecord {
    /// <summary>
    /// The type converted record.
    /// </summary>
    /// <value>The t.</value>
    public T t {
      get {
        return (T)record;
      }
    }

    public SwipableViewHolder(ViewGroup parent, int viewResource, bool useSwipeParent=true) : base(parent, viewResource, useSwipeParent) {
    }
  }
}

