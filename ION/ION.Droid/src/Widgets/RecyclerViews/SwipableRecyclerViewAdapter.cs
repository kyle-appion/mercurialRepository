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
  public abstract class SwipableRecyclerViewAdapter : IONRecyclerViewAdapter {
    /// <summary>
    /// The delegate that will handle item clicks.
    /// </summary>
    public delegate void OnItemClicked(SwipableRecyclerViewAdapter adapter, int position);

    /// <summary>
    /// The number of milliseconds that will ellapse before the swiped row will return.
    /// </summary>
    private const long PENDING_ACTION_DELAY = 2500;

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

    /// <summary>
    /// The recycler view that this adapter is working within.
    /// </summary>
    /// <value>The recycler view.</value>
    public RecyclerView recyclerView { get; private set; }
    /// <summary>
    /// The handler that will post delayed actions to the main thread.
    /// </summary>
    public Handler handler { get; private set; }

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
    /// The records that have pending actions.
    /// </summary>
    private Dictionary<IRecord, Action> pendingActions = new Dictionary<IRecord, Action>();

    /// <summary>
    /// The item decorator that will draw the action button behind a swiped view.
    /// </summary>
    private ItemTouchHelper touchHelperDecoration;
    /// <summary>
    /// The item decorator that will resolve item swipes.
    /// </summary>
    private RecyclerView.ItemDecoration swipeDecoration;

    public SwipableRecyclerViewAdapter() : base() {
      handler = new Handler();
      touchHelperDecoration = new ItemTouchHelper(new SwipeDecorator(this, Color.Transparent));
      swipeDecoration = new SwipeAnimationDecorator(Color.Transparent);
      swipeConfirmTimeout = PENDING_ACTION_DELAY;
    }

    /// <summary>
    /// Raises the attached to recycler view event.
    /// </summary>
    /// <param name="recyclerView">Recycler view.</param>
    public override void OnAttachedToRecyclerView(RecyclerView recyclerView) {
      base.OnAttachedToRecyclerView(recyclerView);
      this.recyclerView = recyclerView;
      touchHelperDecoration.AttachToRecyclerView(recyclerView);
      recyclerView.AddItemDecoration(swipeDecoration);
      if (recyclerView.GetLayoutManager() == null) {
        recyclerView.SetLayoutManager(new LinearLayoutManager(recyclerView.Context));
      }
    }

    /// <summary>
    /// Raises the detached from recycler view event.
    /// </summary>
    /// <param name="recyclerView">Recycler view.</param>
    public override void OnDetachedFromRecyclerView(RecyclerView recyclerView) {
      base.OnDetachedFromRecyclerView(recyclerView);
      this.recyclerView = recyclerView;
      recyclerView.RemoveItemDecoration(swipeDecoration);
    }

    /// <summary>
    /// Raises the create view holder event.
    /// </summary>
    /// <param name="parent">Parent.</param>
    /// <param name="viewType">View type.</param>
    public override sealed RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType) {
      var ret = OnCreateSwipableViewHolder(parent, viewType);

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

      if (pendingActions.ContainsKey(record)) {
        vh.ItemView.SetBackgroundColor(Color.Red);
        vh.RevealButton();
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
        vh.HideButton();

      }
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
    /// Queries whether or not the given view holder is swipeable.
    /// </summary>
    /// <returns><c>true</c> if this instance is view holder swipable the specified viewHolder index; otherwise, <c>false</c>.</returns>
    /// <param name="viewHolder">View holder.</param>
    /// <param name="index">Index.</param>
    public abstract bool IsViewHolderSwipable(IRecord record, SwipableViewHolder viewHolder, int index);

    /// <summary>
    /// Queries the action that is triggered when the swipe revealed button is clicked.
    /// </summary>
    /// <returns>The view holder swipe action.</returns>
    /// <param name="index">Index.</param>
    public abstract Action GetViewHolderSwipeAction(int index);

    /// <summary>
    /// Queries whether or not the given record in the recycler view has a pending action.
    /// </summary>
    /// <returns><c>true</c> if this instance is pending removal; otherwise, <c>false</c>.</returns>
    public bool HasPendingAction(int position) {
      return pendingActions.ContainsKey(records[position]);
    }

    /// <summary>
    /// Performs a swipe action for the given position.
    /// </summary>
    /// <param name="swipePosition">Swipe position.</param>
    public void PerformSwipeAction(int swipePosition) {
      var record = records[swipePosition];
      Action action = () => {
        handler.RemoveCallbacks(pendingActions[record]);
        pendingActions.Remove(record);
        NotifyItemChanged(swipePosition);
      };
      pendingActions.Add(record, action);
      handler.PostDelayed(action, swipeConfirmTimeout);
      NotifyItemChanged(swipePosition);
    }

    /// <summary>
    /// Cancels an active swipe.
    /// </summary>
    /// <returns><c>true</c> if this instance cancel swipe action the specified swipePosition; otherwise, <c>false</c>.</returns>
    /// <param name="swipePosition">Swipe position.</param>
    public void CancelSwipeAction(int swipePosition) {
      var action = pendingActions[records[swipePosition]];
      if (action != null) {
        action();
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
      }
    }

    public virtual void OnBindTo() {
    }

    public virtual void Unbind() {
    }

    public void RevealButton() {
      if (useSwipeLayout) {
        content.Visibility = ViewStates.Invisible;
        button.Visibility = ViewStates.Visible;
      }
    }

    public void HideButton() {
      if (useSwipeLayout) {
        content.Visibility = ViewStates.Visible;
        button.Visibility = ViewStates.Invisible;
      }
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

