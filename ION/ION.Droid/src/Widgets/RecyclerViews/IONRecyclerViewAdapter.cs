namespace ION.Droid.Widgets.RecyclerViews {

  using Android.Support.V7.Widget;
	using Android.Support.V7.RecyclerView;
	using Android.Views;

  /// <summary>
  /// A recycler view adapter that provides features that are not default in a recycler view, such as swipe to delete
  /// and other touch/convenience interactions. 
  /// </summary>
  public abstract class IONRecyclerViewAdapter : RecyclerView.Adapter {
    /// <summary>
    /// The delegate that will be notified when the adapter's content changes.
    /// </summary>
    public delegate void OnDatasetChanged(IONRecyclerViewAdapter adapter);

    /// <summary>
    /// The event that will be notified when the ION RecyclerView's data set changes.
    /// </summary>
    public event OnDatasetChanged onDatasetChanged;

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

    public IONRecyclerViewAdapter() {
      RegisterAdapterDataObserver(new InternalObserver(this));
    }

		public override void OnAttachedToRecyclerView(RecyclerView recyclerView) {
			base.OnAttachedToRecyclerView(recyclerView);
			this.recyclerView = recyclerView;
			if (recyclerView.GetLayoutManager() == null) {
				recyclerView.SetLayoutManager(new LinearLayoutManager(recyclerView.Context));
			}
		}

		public override void OnDetachedFromRecyclerView(RecyclerView recyclerView) {
			base.OnDetachedFromRecyclerView(recyclerView);
			this.recyclerView = null;
		}

    public void NotifyChanged() {
      if (onDatasetChanged != null) {
        onDatasetChanged(this);
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

    private class InternalObserver : RecyclerView.AdapterDataObserver {
      private IONRecyclerViewAdapter adapter { get; set; }

      public InternalObserver(IONRecyclerViewAdapter adapter) {
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

