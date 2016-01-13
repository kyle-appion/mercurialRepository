namespace ION.Droid.Widgets.Adapters.Workbench {

  using System;
//  using System.Collections.Generic;
  using System.Collections.ObjectModel;

  using Android.App;
  using Android.Content;
  using Android.Content.Res;
  using Android.OS;
  using Android.Support.V7.Widget;
  using Android.Support.V7.Widget.Helper;
  using Android.Views;
  using Android.Widget;

  using ION.Core.App;
  using ION.Core.Content;
  using ION.Core.Sensors;
  using ION.Core.Util;

  using ION.Droid.Util;
  using ION.Droid.Views;
  using ION.Droid.Widgets.RecyclerViews;
  
  public class WorkbenchAdapter : RecyclerView.Adapter, IItemTouchHelperAdapter {

    // Overridden from RecyclerView.Adapter
    public override int ItemCount {
      get {
        return records.Count;
      }
    }

    /// <summary>
    /// The current ION instance.
    /// </summary>
    /// <value>The ion.</value>
    private IION ion { get; set; }
    /// <summary>
    /// The bitmap cache that will contain all of the bitmaps used in the adapter.
    /// </summary>
    /// <value>The cache.</value>
    private BitmapCache cache { get; set; }
    /// <summary>
    /// The drag listener that will be used to start the adapter dragging.
    /// </summary>
    private IOnStartDragListener dragListener { get; set; }
    /// <summary>
    /// The records that are contained within the adapter.
    /// </summary>
    private ObservableCollection<IRecord> records = new ObservableCollection<IRecord>();

    public WorkbenchAdapter(IION ion, Resources resources, IOnStartDragListener dragListener) {
      this.ion = ion;
      this.cache = new BitmapCache(resources);
      this.dragListener = dragListener;
    }

    // Overridden from RecyclerView.Adapter
    public override int GetItemViewType(int position) {
      return (int)records[position].viewType;
    }

    // Overridden from RecyclerView.Adapter
    public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType) {
      var li = LayoutInflater.From(parent.Context);

      switch ((ViewType)viewType) {
        case ViewType.Footer:
          return new FooterViewHolder(li.Inflate(Resource.Layout.list_item_add, parent, false));
        case ViewType.Viewer:
          return new ViewerViewHolder(this, cache, li.Inflate(Resource.Layout.list_item_large_viewer, parent, false));
        default:
          throw new Exception("Unknown view type: " + viewType);
      }
    }

    // Overridden from RecyclerView.Adapter
    public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position) {
      var viewType = GetItemViewType(position);

      switch ((ViewType)viewType) {
        case ViewType.Footer:
          var fr = records[position] as FooterRecord;

          if (fr != null) {
            (holder as FooterViewHolder)?.BindTo(fr);
          }

          break;
        case ViewType.Viewer:
          var vr = records[position] as ViewerRecord;

          if (vr != null) {
            (holder as ViewerViewHolder)?.BindTo(vr);
          }

          break;
        default:
          throw new Exception("Unknown view type: " + viewType);
      }

      holder.ItemView.SetOnTouchListener(new TouchListenerHelper(holder, dragListener));
    }

    // Overridden from RecyclerView.Adapter
    public override void OnViewDetachedFromWindow(Java.Lang.Object holder) {
      base.OnViewDetachedFromWindow(holder);

      (holder as WorkbenchViewHolder)?.Unbind();
    }

    /// <summary>
    /// Sets the workbench content that the adapter will display.
    /// </summary>
    /// <param name="workbench">Workbench.</param>
    public void SetWorkbench(Workbench workbench, Action footerAction) {
      records.Clear();

      records.Add(new FooterRecord(footerAction));

      NotifyDataSetChanged();
    }

    /// <summary>
    /// Adds a new sensor to the adapter in a pretty, animated fashion.
    /// </summary>
    /// <param name="sensor">Sensor.</param>
    public void AddSensor(Sensor sensor) {
      var pos = ItemCount - 1;
      records.Insert(pos, new ViewerRecord(new Manifold(sensor)));
      this.NotifyItemInserted(pos);
    }

    // Overridden from IItemTouchHelperAdapter
    public bool OnItemMove(int fromPosition, int toPosition) {
      var item = records[fromPosition];
      records.Move(fromPosition, toPosition);

      NotifyItemMoved(fromPosition, toPosition);

      return true;
    }

    // Overridden from IItemTouchHelperAdapter
    public void OnItemDismiss(int position) {
      records.RemoveAt(position);

      NotifyItemRemoved(position);
    }
  }

  /// <summary>
  /// The types of view holders that are present in the adapter.
  /// </summary>
  enum ViewType {
    Footer,
    Viewer,
    MeasurementSubview,
    FluidSubview,
  }

  /// <summary>
  /// The interface that represents a "record" item in the adapter. This interface is
  /// used to abstract the types of views in the adapter.
  /// </summary>
  interface IRecord {
    ViewType viewType { get; }
  }

  abstract class WorkbenchViewHolder : RecyclerView.ViewHolder, IItemTouchHelperViewHolder {
    public WorkbenchViewHolder(View view) : base(view) {
    }

    public abstract void BindTo(IRecord t);
    public abstract void Unbind();

    // Overridden from IITouchHelperViewHolder
    public virtual void OnItemSelected() {
    }

    // Overridden from IITouchHelperViewHolder
    public virtual void OnItemClear() {
    }
  }

  abstract class WorkbenchViewHolder<T> : WorkbenchViewHolder where T : IRecord {
    public WorkbenchViewHolder(View view) : base(view) {
    }

    public override void BindTo(IRecord t) {
      BindTo((T)t);
    }

    public abstract void BindTo(T t);
  }

  class FooterRecord : IRecord {
    public ViewType viewType { 
      get {
        return ViewType.Footer;
      }
    }

    public Action onClick;

    public FooterRecord(Action onClick) {
      this.onClick = onClick;
    }
  }

  class FooterViewHolder : WorkbenchViewHolder<FooterRecord> {
    public FooterRecord record;

    private Button add;

    public FooterViewHolder(View view) : base(view) {
      add = view.FindViewById<Button>(Resource.Id.add);
    }

    // Overridden from WorkbenchViewHolder
    public override void BindTo(FooterRecord record) {
      this.record = record;

      var c = ItemView.Context;
      add.Text = c.GetString(Resource.String.workbench_add_viewer);

      add.SetOnClickListener(new ViewClickAction((view) => {
        record.onClick();
      }));
    }

    // Overridden from WorkbenchViewHolder
    public override void Unbind() {
    }
  }
}

