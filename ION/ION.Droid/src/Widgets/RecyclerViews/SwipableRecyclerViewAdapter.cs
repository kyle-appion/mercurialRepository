namespace ION.Droid.Widgets.RecyclerViews {

  using System;
  using System.Collections.Generic;
  using System.Collections.ObjectModel;


  using Android.Content;
  using Android.Graphics;
  using Android.Graphics.Drawables;
  using Android.OS;
  using Android.Support.V7.Widget;
  using Android.Support.V7.Widget.Helper;
  using Android.Util;
  using Android.Views;
  using Android.Widget;

  using ION.Droid.Views;

  /// <summary>
  /// A recycler view adapter that provides features that are not default in a recycler view, such as swipe to delete
  /// and other touch/convenience interactions. 
  /// </summary>
  public abstract class SwipableRecyclerViewAdapter : RecyclerView.Adapter {
    /// <summary>
    /// The number of milliseconds that will ellapse before the swiped row will return.
    /// </summary>
    private const long PENDING_ACTION_DELAY = 2500;
    /// <summary>
    /// The delegate that will be notified when the adapter's content changes.
    /// </summary>
    public delegate void OnDatasetChanged(SwipableRecyclerViewAdapter adapter);

    /// <summary>
    /// The event that will be notified when the ION RecyclerView's data set changes.
    /// </summary>
    public event OnDatasetChanged onDatasetChanged;

    /// <summary>
    /// The number of records that are present in the adapter.
    /// </summary>
    /// <value>The item count.</value>
    public override int ItemCount {
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
    protected readonly ObservableCollection<IRecord> records = new ObservableCollection<IRecord>();
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

    public SwipableRecyclerViewAdapter() {
      handler = new Handler();
      touchHelperDecoration = new ItemTouchHelper(new SwipeDecorator(this, Color.Transparent));
      swipeDecoration = new SwipeAnimationDecorator(Color.Red);
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
//      recyclerView.AddItemDecoration(touchHelperDecoration);
      recyclerView.AddItemDecoration(swipeDecoration);
    }

    /// <summary>
    /// Raises the detached from recycler view event.
    /// </summary>
    /// <param name="recyclerView">Recycler view.</param>
    public override void OnDetachedFromRecyclerView(RecyclerView recyclerView) {
      base.OnDetachedFromRecyclerView(recyclerView);
      this.recyclerView = recyclerView;
//      recyclerView.RemoveItemDecoration(touchHelperDecoration);
      recyclerView.RemoveItemDecoration(swipeDecoration);
    }

    /// <summary>
    /// Raises the create view holder event.
    /// </summary>
    /// <param name="parent">Parent.</param>
    /// <param name="viewType">View type.</param>
    public override sealed RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType) {
      return OnCreateSwipableViewHolder(parent, viewType);
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
        vh.content.Visibility = ViewStates.Invisible;
        vh.button.Visibility = ViewStates.Visible;
        vh.button.SetOnClickListener(new ViewClickAction((v) => {
          Action action = GetViewHolderSwipeAction(position);
          if (action != null) {
            action();
          }
          handler.RemoveCallbacks(pendingActions[record]);
          pendingActions.Remove(record);
          NotifyItemChanged(position);
/*
          var action = pendingActions[record];
          pendingActions.Remove(record);
          if (action != null) {
            handler.RemoveCallbacks(action);
          }
          
          NotifyItemChanged(records.IndexOf(record));
*/
        }));
      } else {
        vh.ItemView.SetBackgroundColor(Color.Transparent);
        vh.ItemView.Visibility = ViewStates.Visible;
        vh.content.Visibility = ViewStates.Visible;
        vh.button.Visibility = ViewStates.Invisible;
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
    public abstract void OnBindViewHolder(IRecord record, SwipableViewHolder vh, int position);

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

/*
      var record = records[swipePosition];
      if (!pendingActions.ContainsKey(record)) {
        var vh = recyclerView.FindViewHolderForAdapterPosition(swipePosition) as SwipableViewHolder;
        var action = GetViewHolderSwipeAction(swipePosition);
        pendingActions.Add(record, action);
        NotifyItemChanged(swipePosition);
        handler.PostDelayed(action, swipeConfirmTimeout);
      }
*/
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
    /// Notifies the OnDataSetChanged event that the adapter changed.
    /// </summary>
    public void NotifyChanged() {
      if (onDatasetChanged != null) {
        onDatasetChanged(this);
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
    /// The observer that will be used by the adapter to propogate events to the OnDataSetChanged.
    /// </summary>
    private class InternalObserver : RecyclerView.AdapterDataObserver {
      private SwipableRecyclerViewAdapter adapter { get; set; }

      public InternalObserver(SwipableRecyclerViewAdapter adapter) {
        this.adapter = adapter;
      }

      /// <summary>
      /// Raises the changed event.
      /// </summary>
      public override void OnChanged() {
        base.OnChanged();
        adapter.NotifyChanged();
      }
    }
  }

  /// <summary>
  /// The base view holder that will be used for the IONRecyclerViewAdapter.
  /// </summary>
  public class SwipableViewHolder : RecyclerView.ViewHolder {
//    protected SwipableRecyclerViewAdapter adapter { get; internal set; }
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


    public SwipableViewHolder(ViewGroup parent, int viewResource) :
        base(LayoutInflater.From(parent.Context).Inflate(Resource.Layout.list_item_ion_recycler_view_holder, parent, false)) {
      content = ItemView.FindViewById<LinearLayout>(Resource.Id.content);
      view = LayoutInflater.From(parent.Context).Inflate(viewResource, content, true);
      button = ItemView.FindViewById<Button>(Resource.Id.button);
    }
  }
}

