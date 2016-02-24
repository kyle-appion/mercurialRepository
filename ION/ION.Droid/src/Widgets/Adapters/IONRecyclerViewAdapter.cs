namespace ION.Droid.Widgets.Adapters {

  using System;

  using Android.Support.V7.Widget;

  /// <summary>
  /// A simple extension of the RecyclerView that will allow for the ion extensions to the class.
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
        adapter.NotifyChanged();
      }
    }
  }
}

