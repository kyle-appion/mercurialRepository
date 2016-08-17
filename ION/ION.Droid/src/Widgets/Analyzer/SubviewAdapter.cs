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
    public delegate void OnSensorPropertyClicked(Manifold manifold, ISensorProperty sensorProperty);

    public event OnSensorPropertyClicked onSensorPropertyClicked;
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
          return new MeasurementSubviewViewHolder(li.Inflate(Resource.Layout.subview_measurement_small, parent, false), cache);
        case EViewType.PTChartSubview:
          return new PTChartSubviewViewHolder(li.Inflate(Resource.Layout.subview_fluid_small, parent, false));
        case EViewType.SuperheatSubcoolSubview:
          return new SuperheatSubcoolSubviewViewHolder(li.Inflate(Resource.Layout.subview_fluid_small, parent, false));
        case EViewType.TimerSubview:
          return new TimerSubviewViewHolder(li.Inflate(Resource.Layout.subview_timer_small, parent, false), cache);
        case EViewType.RateOfChangeSubview:
          return new RateOfChangeSubviewViewHolder(li.Inflate(Resource.Layout.subview_measurement_small, parent, false), cache);
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
      var record = records[position] as SensorPropertyRecord;

      switch ((EViewType)viewType) {
        case EViewType.MeasurementSubview:
          var mr = records[position] as MeasurementRecord;
          (holder as MeasurementSubviewViewHolder)?.BindTo(mr);
          if (mr != null) {
            (holder as MeasurementSubviewViewHolder)?.BindTo(mr);
          }
          break;

        case EViewType.RateOfChangeSubview:
          var rr = record as RateOfChangeSubviewRecord;
          (holder as RateOfChangeSubviewViewHolder)?.BindTo(rr);
          break;

        case EViewType.TimerSubview:
          var tr = record as TimerSubviewRecord;
          (holder as TimerSubviewViewHolder)?.BindTo(tr);
          break;

        case EViewType.PTChartSubview:
          var pr = record as PTChartSubviewRecord;
          (holder as PTChartSubviewViewHolder)?.BindTo(pr);
          break;

        case EViewType.SuperheatSubcoolSubview:
          var shr = record as SuperheatSubcoolSubviewRecord;
          (holder as SuperheatSubcoolSubviewViewHolder)?.BindTo(shr);
          break;
      }

      holder.ItemView.SetOnClickListener(new ViewClickAction((v) => {
        var sp = records[position];
        if (sp != null && onSensorPropertyClicked != null) {
          onSensorPropertyClicked(manifold, record.sensorProperty);
        }
      }));
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

    /// <summary>
    /// Creates the record for the given sensor property.
    /// </summary>
    /// <returns>The sensor property record.</returns>
    /// <param name="sp">Sp.</param>
    private IRecord CreateRecordFor(ISensorProperty sp) {
      if (sp is PTChartSensorProperty) {
        return new PTChartSubviewRecord(sp as PTChartSensorProperty);
      } else if (sp is SuperheatSubcoolSensorProperty) {
        return new SuperheatSubcoolSubviewRecord(sp as SuperheatSubcoolSensorProperty);
      } else if (sp is TimerSensorProperty) {
        return new TimerSubviewRecord(sp as TimerSensorProperty);
      } else if (sp is RateOfChangeSensorProperty) {
        return new RateOfChangeSubviewRecord(sp as RateOfChangeSensorProperty);
      } else {
        return new MeasurementRecord(sp);
      }
    }
  }

  enum EViewType {
    MeasurementSubview,
    RateOfChangeSubview,
    PTChartSubview,
    SuperheatSubcoolSubview,
    TimerSubview,
  }

  interface IRecord {
    EViewType viewType { get; }
  }

  class SensorPropertyRecord : IRecord {
    public EViewType viewType { get; private set; }
    public ISensorProperty sensorProperty { get; private set; }

    public SensorPropertyRecord(EViewType viewType, ISensorProperty sensorProperty) {
      this.viewType = viewType;
      this.sensorProperty = sensorProperty;
    }
  }

  class SensorPropertyRecord<T> : SensorPropertyRecord where T : ISensorProperty {
    public T item;

    public SensorPropertyRecord(EViewType viewType, T item) : base(viewType, item) {
      this.item = item;
    }
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

    public override void BindTo(IRecord t) {
      BindTo((T)t);
    }

    public abstract void BindTo(T t);
  }
}

