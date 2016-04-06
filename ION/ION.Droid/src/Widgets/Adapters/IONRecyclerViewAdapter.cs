namespace ION.Droid.Widgets.Adapters {

  using System;

  using Android.Support.V7.Widget;

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

    public IONRecyclerViewAdapter() {
      RegisterAdapterDataObserver(new InternalObserver(this));
    }

    public void NotifyChanged() {
      if (onDatasetChanged != null) {
        onDatasetChanged(this);
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
        base.OnChanged();
        ION.Core.Util.Log.D(this, "THE ADAPTER HAS CHANGED");
        adapter.NotifyChanged();
      }
    }
  }
}

