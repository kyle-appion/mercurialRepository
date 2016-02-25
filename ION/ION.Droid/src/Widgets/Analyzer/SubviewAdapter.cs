namespace ION.Droid.Widgets.Analyzer {
  
  using System;
  using System.Collections.Generic;

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
  using ION.Core.Sensors.Properties;
  using ION.Core.Util;

  using ION.Droid.Util;
  using ION.Droid.Views;
  using ION.Droid.Widgets.RecyclerViews;
  using ION.Droid.Widgets.Templates;

  public class SubviewAdapter : RecyclerView.Adapter {
    /// <summary>
    /// The manifold whose sensor properties will be displayed.
    /// </summary>
    /// <value>The manifold.</value>
    public Manifold manifold {
      get {
        return __manifold;
      }
      set {
        if (__manifold != null) {
          __manifold.onManifoldEvent -= OnManifoldEvent;
        }

        __manifold = value;

        records.Clear();


        if (__manifold != null) {
          __manifold.onManifoldEvent += OnManifoldEvent;
          RefreshRecords();
        }
      }
    } Manifold __manifold;

    /// <summary>
    /// Gets the item count.
    /// </summary>
    /// <value>The item count.</value>
    public override int ItemCount {
      get {
        return records.Count;
      }
    }
    /// <summary>
    /// The bitmap cache that will contain all the flyweight bitmaps for the adapter's views.
    /// </summary>
    private BitmapCache cache;
    /// <summary>
    /// The list of records that will map to the subviews within the manifold.
    /// </summary>
    private List<IRecord> records = new List<IRecord>();

    public SubviewAdapter(BitmapCache cache) {
      this.cache = cache;
      manifold = null;
    }

    /// <summary>
    /// Gets the type of the item view.
    /// </summary>
    /// <returns>The item view type.</returns>
    /// <param name="position">Position.</param>
    public override int GetItemViewType(int position) {
      return (int)records[position].viewType;
    }

    /// <summary>
    /// Raises the create view holder event.
    /// </summary>
    /// <param name="parent">Parent.</param>
    /// <param name="viewType">View type.</param>
    public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType) {
      var li = LayoutInflater.From(parent.Context);

      switch ((EViewType)viewType) {
        case EViewType.MeasurementSubview:
          return new MeasurementViewHolder(li.Inflate(Resource.Layout.subview_measurement_small, parent, false), cache);
/*
        case EViewType.FluidSubview:
          return new FluidViewHolder(li.Inflate(Resource.Layout.list_item_small_fluid_subview, false), cache);
*/
        default:
          throw new Exception("Unknown view type: " + viewType);
      }
    }

    /// <summary>
    /// Raises the bind view holder event.
    /// </summary>
    /// <param name="holder">Holder.</param>
    /// <param name="position">Position.</param>
    public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position) {
      var viewType = GetItemViewType(position);

      switch ((EViewType)viewType) {
        case EViewType.MeasurementSubview:
          var mr = records[position] as MeasurementRecord;

          if (mr != null) {
            (holder as MeasurementViewHolder)?.BindTo(mr);
          }
          break;
/*
        case EViewType.FluidSubview:
          var fr = records[position] as FluidRecord;

          if (fr != null) {
            (holder as FluidViewHolder)?.BindTo(fr);
          }

          break;
*/
        default:
          throw new Exception("Unknown view type: " + viewType);
      }
    }

    /// <summary>
    /// Raises the view detached from window event.
    /// </summary>
    /// <param name="holder">Holder.</param>
    public override void OnViewDetachedFromWindow(Java.Lang.Object holder) {
      base.OnViewDetachedFromWindow(holder);

      (holder as SubviewAdapterViewHolder)?.Unbind();
    }

    /// <summary>
    /// Refreshes the records that are contained within the adapter.
    /// </summary>
    private void RefreshRecords() {
      records.Clear();

      foreach (var sp in manifold.sensorProperties) {
        records.Add(CreateRecordFor(sp));
      }

      NotifyDataSetChanged();
    }

    /// <summary>
    /// The record that represents a subview.
    /// </summary>
    /// <returns>The record for.</returns>
    /// <param name="sensorProperty">Sensor property.</param>
    private IRecord CreateRecordFor(ISensorProperty sensorProperty) {
      if (false) {
      } else {
        return new MeasurementRecord(sensorProperty);
      }
    }

    /// <summary>
    /// Called when a manifold event is thrown.
    /// </summary>
    /// <param name="me">Me.</param>
    private void OnManifoldEvent(ManifoldEvent me) {
      switch (me.type) {
        case ManifoldEvent.EType.Invalidated:
          RefreshRecords();
          break;
        case ManifoldEvent.EType.SensorPropertyAdded:
          records.Add(CreateRecordFor(manifold[me.index]));
          NotifyItemRangeInserted(me.index, 1);
          break;
        case ManifoldEvent.EType.SensorPropertyRemoved:
          records.RemoveAt(me.index);
          NotifyItemRangeRemoved(me.index, 1);
          break;
        case ManifoldEvent.EType.SensorPropertySwapped:
          break;
      }
    }

    internal enum EViewType {
      MeasurementSubview,
      FluidSubview,
    }
  }

  interface IRecord {
    SubviewAdapter.EViewType viewType { get; }
  }

  abstract class SubviewAdapterViewHolder : RecyclerView.ViewHolder {
    public SubviewAdapterViewHolder(View view) : base(view) {
    }

    public abstract void BindTo(IRecord t);
    public abstract void Unbind();
  }

  abstract class SubviewAdapterViewHolder<T> : SubviewAdapterViewHolder where T : IRecord {
    public SubviewAdapterViewHolder(View view) : base(view) {
    }

    public sealed override void BindTo(IRecord t) {
      BindTo((T)t);
    }

    public abstract void BindTo(T t);
  }

  class MeasurementRecord : IRecord {
    public SubviewAdapter.EViewType viewType {
      get {
        return SubviewAdapter.EViewType.MeasurementSubview;
      }
    }

    public ISensorProperty sensorProperty { get; private set; }

    public MeasurementRecord(ISensorProperty sensorProperty) {
      this.sensorProperty = sensorProperty;
    }
  }

  class MeasurementViewHolder : SubviewAdapterViewHolder<MeasurementRecord> {
    private MeasurementRecord record;
    private MeasurementSubviewTemplate template;

    public MeasurementViewHolder(View view, BitmapCache cache) : base(view) {
      template = new MeasurementSubviewTemplate(view, cache);
    }

    public override void BindTo(MeasurementRecord t) {
      template.Bind(t.sensorProperty);
    }

    public override void Unbind() {
      template.Unbind();
    }
  }
}

