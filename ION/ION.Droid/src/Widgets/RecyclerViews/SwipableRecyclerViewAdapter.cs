namespace ION.Droid.Widgets.RecyclerViews {

  using System;
  using System.Collections.Generic;


  using Android.Content;
  using Android.Graphics;
  using Android.Graphics.Drawables;
  using Android.OS;
  using Android.Support.V7.Widget;
  using Android.Util;
  using Android.Views;
  using Android.Widget;

  /// <summary>
  /// A recycler view adapter that provides features that are not default in a recycler view, such as swipe to delete
  /// and other touch/convenience interactions. 
  /// </summary>
  public abstract class SwipableRecyclerViewAdapter : RecyclerView.Adapter {
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
    public int swipeConfirmTimeout { get; set; }
    /// <summary>
    /// The color of the view that is swiped.
    /// </summary>
    /// <value>The color of the swipe background.</value>
    public Color swipeBackgroundColor { get; set; }

    /// <summary>
    /// The records that the recycler view will display.
    /// </summary>
    private List<IRecord> records = new List<IRecord>();
    /// <summary>
    /// The records that are pending removal from the recycler adapter.
    /// </summary>
    private HashSet<IRecord> recordsPendingRemoval = new HashSet<IRecord>();

    public SwipableRecyclerViewAdapter() {
      handler = new Handler();
      swipeBackgroundColor = Color.LightBlue;
    }

    /// <summary>
    /// Raises the attached to recycler view event.
    /// </summary>
    /// <param name="recyclerView">Recycler view.</param>
    public override void OnAttachedToRecyclerView(RecyclerView recyclerView) {
      base.OnAttachedToRecyclerView(recyclerView);
      this.recyclerView = recyclerView;
    }

    /// <summary>
    /// Raises the detached from recycler view event.
    /// </summary>
    /// <param name="recyclerView">Recycler view.</param>
    public override void OnDetachedFromRecyclerView(RecyclerView recyclerView) {
      base.OnDetachedFromRecyclerView(recyclerView);
      this.recyclerView = recyclerView;
    }

    /// <summary>
    /// Raises the bind view holder event.
    /// </summary>
    /// <param name="holder">Holder.</param>
    /// <param name="position">Position.</param>
    public override sealed void OnBindViewHolder(RecyclerView.ViewHolder holder, int position) {
      var record = records[position];
      var vh = holder as IONRecyclerViewHolder;

      if (vh == null) {
        vh = OnCreateViewHolder(record, position);
      }

      if (vh == null) {
        throw new Exception("IONRecyclerViewAdapter cannot accept viewholder's that do not extend IONRecyclerViewHolder");
      }

      if (recordsPendingRemoval.Contains(vh)) {
        vh.ItemView.SetBackgroundColor(swipeBackgroundColor);
      } else {
      }
    }

    /// <summary>
    /// Binds the given record to the view holder.
    /// </summary>
    /// <param name="record">Record.</param>
    /// <param name="vh">Vh.</param>
    /// <param name="position">Position.</param>
    public abstract void OnBindViewHolder(IRecord record, IONRecyclerViewHolder vh, int position);

    /// <summary>
    /// Queries whether or not the given record in the recycler view is pending removal.
    /// </summary>
    /// <returns><c>true</c> if this instance is pending removal; otherwise, <c>false</c>.</returns>
    public bool IsPendingRemoval(int position) {
      return recordsPendingRemoval.Contains(records[position]);
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
      int ViewType { get; }
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
  public class IONRecyclerViewHolder : RecyclerView.ViewHolder {
    protected SwipableRecyclerViewAdapter adapter { get; private set; }
    internal Button button;

    public IONRecyclerViewHolder(SwipableRecyclerViewAdapter adapter, View view) : base(view) {
      this.adapter = adapter;
      this.button = view.FindViewById<Button>(Resource.Id.button);
    }
  }

  /// <summary>
  /// The item decorator that will draw a color behind the view as it is swiped for removal.
  /// </summary>
  internal class ItemRemovedDecorator : RecyclerView.ItemDecoration {
    private Drawable color;
    private bool initialized;

    public ItemRemovedDecorator(int color) : this(new Color(color)) {
    }

    public ItemRemovedDecorator(Color color) {
      this.color = new ColorDrawable(color);
    }

    public override void OnDraw(Canvas canvas, RecyclerView parent, RecyclerView.State state) {
      if (parent.GetItemAnimator().IsRunning) {
        /* Because this decorator will fire when an item is swiped for removal, meaning the view is removed and the 
         * recycler view is animating the closure of void, we will need to account for the views being animated. Because
         * the IONRecyclerViewAdapter is meant as a linear list of records, we can assert that the views being animated
         * we be the view above and below the removed view.
         */

        View aboveView = null;
        View belowView = null;

        int left = 0, right = parent.Width, top = 0, bottom = 0;

        int childCount = parent.GetLayoutManager().ChildCount;
        for (int i = 0; i < childCount; i++) {
          var child = parent.GetLayoutManager().GetChildAt(i);

          if (child.TranslationY < 0) {
            aboveView = child;
          } else if (child.TranslationY > 0) {
            if (belowView == null) {
              belowView = child;
            }
          }
        }

        if (aboveView != null && belowView != null) {
          top = aboveView.Bottom + (int)aboveView.TranslationY;
          bottom = belowView.Top + (int)belowView.TranslationY;
        } else if (aboveView != null) {
          top = aboveView.Bottom + (int)aboveView.TranslationY;
          bottom = aboveView.Bottom;
        } else if (belowView != null) {
          top = belowView.Top;
          bottom = belowView.Top + (int)belowView.TranslationY;
        }

        color.SetBounds(left, top, right, bottom);
        color.Draw(canvas);

        base.OnDraw(canvas, parent, state);
      }
    }
  }
}

