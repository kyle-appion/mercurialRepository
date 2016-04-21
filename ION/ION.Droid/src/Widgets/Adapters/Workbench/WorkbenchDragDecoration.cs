namespace ION.Droid.Widgets.Adapters.Workbench {

  using System;

  using Android.Support.V7.Widget;
  using Android.Support.V7.Widget.Helper;
  using Android.Views;

  using ION.Core.Util;

  public class WorkbenchDragDecoration : ItemTouchHelper.SimpleCallback {

    /// <summary>
    /// The workbench adapter that we are decorating.
    /// </summary>
    private WorkbenchAdapter adapter;
    /// <summary>
    /// Whether or not the workbench has been expansion saved.
    /// </summary>
    private bool isSaved;

    public WorkbenchDragDecoration(WorkbenchAdapter adapter) : base(ItemTouchHelper.Up | ItemTouchHelper.Down, 0) {
     this.adapter = adapter;
    }

    /// <summary>
    /// Raises the move event.
    /// </summary>
    /// <param name="recyclerView">Recycler view.</param>
    /// <param name="viewHolder">View holder.</param>
    /// <param name="target">Target.</param>
    public override bool OnMove(RecyclerView recyclerView, RecyclerView.ViewHolder viewHolder, RecyclerView.ViewHolder target) {
      var sourceRecord = adapter.GetRecordAt(viewHolder.AdapterPosition);
      var targetRecord = adapter.GetRecordAt(target.AdapterPosition);

      if (sourceRecord is ManifoldRecord) {
        if (!isSaved) {
          adapter.SaveManifoldExpansionState();
          isSaved = true;
        }
      }

      if (sourceRecord is ManifoldRecord && targetRecord is ManifoldRecord) {
        return PerformManifoldMove(recyclerView, sourceRecord as ManifoldRecord, targetRecord as ManifoldRecord);
      } else if (sourceRecord is SensorPropertyRecord && targetRecord is SensorPropertyRecord) {
        return PerformSensorPropertyMove(recyclerView, sourceRecord as SensorPropertyRecord, targetRecord as SensorPropertyRecord);
      } else {
        return true;
      }
    }

    /// <summary>
    /// Raises the swiped event.
    /// </summary>
    /// <param name="viewHolder">View holder.</param>
    /// <param name="direction">Direction.</param>
    public override void OnSwiped(RecyclerView.ViewHolder viewHolder, int direction) {
    }

    /// <summary>
    /// Clears the view.
    /// </summary>
    /// <param name="recyclerView">Recycler view.</param>
    /// <param name="viewHolder">View holder.</param>
    public override void ClearView(RecyclerView recyclerView, RecyclerView.ViewHolder viewHolder) {
      base.ClearView(recyclerView, viewHolder);
      Log.D(this, "Clearing");
      if (isSaved) {
        adapter.RestoreManifoldExpansionState();
      }
      isSaved = false;
    }

    /// <summary>
    /// Performs a manifold record swap.
    /// </summary>
    /// <returns><c>true</c>, if manifold move was performed, <c>false</c> otherwise.</returns>
    /// <param name="recyclerView">Recycler view.</param>
    /// <param name="source">Source.</param>
    /// <param name="target">Target.</param>
    private bool PerformManifoldMove(RecyclerView recyclerView, ManifoldRecord source, ManifoldRecord target) {
      adapter.CollapseManifold(source.item);
      adapter.CollapseManifold(target.item);
      var workbench = adapter.workbench;
      workbench.Swap(workbench.IndexOf(source.item), workbench.IndexOf(target.item));

      return true;
    }

    /// <summary>
    /// Performs a sensor property swap.
    /// </summary>
    /// <returns><c>true</c>, if manifold move was performed, <c>false</c> otherwise.</returns>
    /// <param name="recyclerView">Recycler view.</param>
    /// <param name="source">Source.</param>
    /// <param name="target">Target.</param>
    private bool PerformSensorPropertyMove(RecyclerView recyclerView, SensorPropertyRecord source, SensorPropertyRecord target) {
      if (source.manifold.Equals(target.manifold)) {
        var m = source.manifold;
        var first = m.IndexOfSensorProperty(source.sensorProperty);
        var second = m.IndexOfSensorProperty(target.sensorProperty);
        m.SwapSensorProperties(first, second);
        return true;
      } else {
        return true;
      }
    }
  }
}

