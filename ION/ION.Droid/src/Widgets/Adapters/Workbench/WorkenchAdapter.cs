﻿namespace ION.Droid.Widgets.Adapters.Workbench {

  using System;
  using System.Collections.ObjectModel;
  using System.Threading.Tasks;

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

  /// <summary>
  /// The adapter that will provide the views for use in the workbench fragment. After creating the adapter, if you want
  /// to rearrange the adapter, you will need to assign the proper listeners (ie. dragListener for dragging, and
  /// swipeListener for swipping).
  /// that you assign
  /// </summary>
  public class WorkbenchAdapter : RecyclerView.Adapter, IItemTouchHelperAdapter {

    public delegate void OnManifoldClicked(Manifold manifold);
    public delegate void OnSensorPropertyClicked(Manifold manifold, ISensorProperty sensorProperty);

    public event OnManifoldClicked onManifoldClicked;
    public event OnSensorPropertyClicked onSensorPropertyClicked;

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
    /// The workbench that the adapter is currently working with.
    /// </summary>
    /// <value>The workbench.</value>
    private Workbench workbench { get; set; }
    /// <summary>
    /// The records that are contained within the adapter.
    /// </summary>
    private ObservableCollection<IRecord> records = new ObservableCollection<IRecord>();

    public WorkbenchAdapter(IION ion, Resources resources) {
      this.ion = ion;
      this.cache = new BitmapCache(resources);
    }

    // Overridden from RecyclerView.Adapter
    public override int GetItemViewType(int position) {
      return (int)records[position].viewType;
    }

    // Overridden from RecyclerView.Adapter
    public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType) {
      var li = LayoutInflater.From(parent.Context);

      switch ((EViewType)viewType) {
        case EViewType.Footer:
          return new FooterViewHolder(li.Inflate(Resource.Layout.list_item_add, parent, false));
        case EViewType.Manifold:
          return new ManifoldViewHolder(li.Inflate(Resource.Layout.viewer_large, parent, false), cache);
        case EViewType.Space:
          return new SpaceViewHolder(li.Inflate(Resource.Layout.list_item_space, parent, false));
        case EViewType.MeasurementSubview:
          return new MeasurementSubviewViewHolder(li.Inflate(Resource.Layout.subview_measurement_large, parent, false), cache);
        case EViewType.PTChartSubview:
          return new PTChartSubviewViewHolder(li.Inflate(Resource.Layout.subview_fluid_large, parent, false));
        case EViewType.SuperheatSubcoolSubview:
          return new SuperheatSubcoolSubviewViewHolder(li.Inflate(Resource.Layout.subview_fluid_large, parent, false));
        case EViewType.TimerSubview:
          return new TimerSubviewViewHolder(li.Inflate(Resource.Layout.subview_timer_large, parent, false), cache);
        case EViewType.RateOfChangeSubview:
          return new RateOfChangeSubviewViewHolder(li.Inflate(Resource.Layout.subview_measurement_large, parent, false), cache);
        default:
          throw new Exception("Unknown view type: " + viewType);
      }
    }

    // Overridden from RecyclerView.Adapter
    public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position) {
      var viewType = GetItemViewType(position);
      var record = records[position];

      if (record is SensorPropertyRecord) {
        BuildSensorPropertyViewHolder(holder, position);
      } else {
        switch ((EViewType)viewType) {
          case EViewType.Footer:
            (holder as FooterViewHolder)?.BindTo(record as FooterRecord);
            break;

          case EViewType.Manifold:
            var vr = records[position] as ManifoldRecord;

            if (vr != null) {
              (holder as ManifoldViewHolder)?.BindTo(vr);
            }

            holder.ItemView.SetOnClickListener(new ViewClickAction((v) => {
              if (onManifoldClicked != null) {
                onManifoldClicked(vr.item);
              }
            }));

            break;

          case EViewType.Space:
            var sr = records[position] as SpaceRecord;

            if (sr != null) {
              (holder as SpaceViewHolder)?.BindTo(sr);  
            }

            break;

          default:
            throw new Exception("Unknown view type: " + viewType);
        }
        /*
        var touchHelper = new TouchListenerHelper(holder);
        touchHelper.dragStartListener = dragListener;
        touchHelper.swipeStartListener = swipeListener;
        holder.ItemView.SetOnTouchListener(touchHelper);
    */
      }
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
      if (this.workbench != null) {
        this.workbench.onWorkbenchEvent -= OnWorkbenchEvent;
      }

      this.workbench = workbench;
      this.workbench.onWorkbenchEvent += OnWorkbenchEvent;

      records.Clear();

      foreach (var m in workbench.manifolds) {
        records.Add(new ManifoldRecord(m));
      }

      records.Add(new FooterRecord(footerAction));

      NotifyDataSetChanged();
    }

    /// <summary>
    /// Raises the workbench event event.
    /// </summary>
    /// <param name="workbenchEvent">Workbench event.</param>
    public void OnWorkbenchEvent(WorkbenchEvent workbenchEvent) {
      var manifold = workbenchEvent.manifold;
      int startIndex = IndexOfManifold(manifold);
      int spc = manifold.sensorPropertyCount;

      switch (workbenchEvent.type) {
        case WorkbenchEvent.EType.Added:
          if (startIndex < 0) {
            records.Insert(records.Count - 1, new ManifoldRecord(manifold));
//            records.Insert(records.Count - 1, new SpaceRecord());
            NotifyItemRangeInserted(records.Count - 1, 2);
          } else {
            records.Insert(startIndex, new ManifoldRecord(manifold));
//            records.Insert(startIndex, new SpaceRecord());
            NotifyItemRangeInserted(startIndex, 2);
          }
          break;
        case WorkbenchEvent.EType.ManifoldEvent:
          OnManifoldEvent(workbenchEvent.manifoldEvent);
          break;
        case WorkbenchEvent.EType.Removed:
          if (startIndex >= 0) {
            records.RemoveAt(startIndex);
            for (int i = 0; i < spc; i++) {
              records.RemoveAt(startIndex);
            }
            NotifyItemRangeRemoved(startIndex, 1 + spc);
          }
          break;
        case WorkbenchEvent.EType.Swapped:
          var firstManifold = workbench[workbenchEvent.index];
          var firstIndex = IndexOfManifold(firstManifold);
          var fmr = records[firstIndex] as ManifoldRecord;
          var fwe = fmr.expanded;

          var secondManifold = workbench[workbenchEvent.otherIndex];
          var secondIndex = IndexOfManifold(secondManifold);
          var smr = records[secondIndex] as ManifoldRecord;
          var swe = smr.expanded;


          Task.Factory.StartNew(() => {
            CollapseManifold(firstManifold);
            CollapseManifold(secondManifold);

            Task.Delay(TimeSpan.FromMilliseconds(100)).Wait();
          }).ContinueWith((task) => {
            records.Move(firstIndex, secondIndex);
            NotifyItemMoved(firstIndex, secondIndex);

            Task.Delay(TimeSpan.FromMilliseconds(100)).Wait();
          }).ContinueWith((task) => {
            if (fwe) {
              ExpandManifold(firstManifold);
            }

            if (swe) {
              ExpandManifold(secondManifold);
            }
          });

          break;
        default:
          throw new Exception("No case for workbenchtype: " + workbenchEvent.type);  
      }
    }

    /// <summary>
    /// Raises the manifold event event.
    /// </summary>
    /// <param name="manifoldEvent">Manifold event.</param>
    public void OnManifoldEvent(ManifoldEvent manifoldEvent) {
      var manifold = manifoldEvent.manifold;
      var manifoldIndex = IndexOfManifold(manifold);
      int index = 0;

      switch (manifoldEvent.type) {
        case ManifoldEvent.EType.Invalidated:
          break;
        case ManifoldEvent.EType.SensorPropertyAdded:
          var mr = records[manifoldIndex] as ManifoldRecord;

          if (manifold.sensorPropertyCount <= 1 && !mr.expanded) {
            mr.expanded = true;
          }

          index = manifoldIndex + 1 + manifoldEvent.index;

          records.Insert(index, CreateSensorPropertyRecord(manifold[manifoldEvent.index]));

          NotifyItemInserted(index);
          break;

        case ManifoldEvent.EType.SensorPropertyRemoved:
          index = manifoldIndex + 1 + manifoldEvent.index;
          records.RemoveAt(index);
          NotifyItemRemoved(index);
          break;

        case ManifoldEvent.EType.SensorPropertySwapped:
          var from = manifoldIndex + 1 + manifoldEvent.index;
          var to = manifoldIndex + 1 + manifoldEvent.otherIndex;
          records.Move(from, to);

          NotifyItemMoved(from, to);
          break;
      }
    }

    /// <summary>
    /// Called when the given view holder is about to be dragged.
    /// </summary>
    /// <param name="vh">Vh.</param>
    public void OnDragStart(RecyclerView.ViewHolder vh) {
    }

    /// <summary>
    /// Raises the item move event.
    /// </summary>
    /// <param name="fromPosition">From position.</param>
    /// <param name="toPosition">To position.</param>
    public bool OnItemMove(int fromPosition, int toPosition) {
      var from = records[fromPosition];
      var to = records[toPosition];

      if (from is ManifoldRecord && to is ManifoldRecord) {
        var fvr = from as ManifoldRecord;
        var tvr = to as ManifoldRecord;

        workbench.Swap(workbench.IndexOf(fvr.item), workbench.IndexOf(tvr.item));

        return true;
      } else if (from is SensorPropertyRecord && to is SensorPropertyRecord) {
        var fp = FindManifoldAtIndex(fromPosition);
        var tp = FindManifoldAtIndex(toPosition);

        if (fp == tp) {
          var i = IndexOfManifold(fp);
          fp.SwapSensorProperties(fromPosition - i - 1, toPosition - i - 1);

          return true;
        } else {
          return false;
        }
      } else {
        return false;
      }
    }

    /// <summary>
    /// Used to determine whether or not the adapter allows an item dismissal at the given position.
    /// </summary>
    /// <param name="position">Position.</param>
    /// <param name="posiiton">Posiiton.</param>
    public bool AllowItemDismissalAt(int position) {
      var r = records[position];

      return r is ManifoldRecord || r is SensorPropertyRecord;
    }

    /// <summary>
    /// Raises the item dismiss event.
    /// </summary>
    /// <param name="position">Position.</param>
    public void OnItemDismiss(int position) {
      var r = records[position];

      if (r is ManifoldRecord) {
        var mr = r as ManifoldRecord;

        workbench.Remove(mr.item);
      } else if (r is SensorPropertyRecord) {
        var spr = r as SensorPropertyRecord;

        var manifold = FindManifoldAtIndex(position);
        manifold.RemoveSensorProperty(spr.sensorProperty);
      }
    }

    private void CollapseManifold(Manifold manifold) {
      var index = IndexOfManifold(manifold);
      var mr = records[index] as ManifoldRecord;

      if (mr != null) {
        if (mr.expanded) {
          var count = manifold.sensorPropertyCount;

          for (int i = count; i >= 1; i--) {
            records.RemoveAt(index + i);
          }

          NotifyItemRangeRemoved(index + 1, count);
        }

        mr.expanded = false;
      }
    }

    private void ExpandManifold(Manifold manifold) {
      var index = IndexOfManifold(manifold);
      var mr = records[index] as ManifoldRecord;

      if (mr != null) {
        if (!mr.expanded) {
          var count = manifold.sensorPropertyCount;

          for (int i = 1; i <= count; i++) {
            records.Insert(index + i, CreateSensorPropertyRecord(manifold[i - 1]));
          }

          NotifyItemRangeRemoved(index + 1, count);
        }

        mr.expanded = true;
      }
    }

    private void ToggleManifold(Manifold manifold) {
      var index = IndexOfManifold(manifold);
      var mr = records[index] as ManifoldRecord;

      if (mr != null) {
        if (mr.expanded) {
          CollapseManifold(manifold);
        } else {
          ExpandManifold(manifold);
        }
      }
    }

    private void BuildSensorPropertyViewHolder(RecyclerView.ViewHolder holder, int position) {
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

      holder.ItemView.Click += (sender, e) => {
        var manifold = FindManifoldAtIndex(position);
        if (manifold != null && onSensorPropertyClicked != null) {
          onSensorPropertyClicked(manifold, record.sensorProperty);
        }
      };
    }

    /// <summary>
    /// Queries the index of the viewer record whose manifold is equal to the
    /// given manifold or -1 if the manifold is not in the adapter.
    /// </summary>
    /// <returns>The of viewer record.</returns>
    /// <param name="manifold">Manifold.</param>
    private int IndexOfManifold(Manifold manifold) {
      for (int i = 0; i < records.Count; i++) {
        var viewerRecord = records[i] as ManifoldRecord;
        if (viewerRecord != null && viewerRecord.item.Equals(manifold)) {
          return i;
        }
      }

      return -1;
    }

    /// <summary>
    /// Attempts to find the manifold at the given index. If a manifold cannot be found using the given index, we will
    /// return null. To find a manifold, we will look at the contents of the adapter. If the index is a sensor property,
    /// we will walk backwards until we find a manifold. If the the index is a manifold, obviously we will return it.
    /// If the index is anything else, we will return null.
    /// </summary>
    /// <returns>The manifold at index.</returns>
    /// <param name="index">Index.</param>
    private Manifold FindManifoldAtIndex(int index) {
      if (index < 0) {
        return null;
      }

      var record = records[index];

      if (record is ManifoldRecord) {
        return ((ManifoldRecord)record).item;
      } else if (record is SensorPropertyRecord) {
        return FindManifoldAtIndex(index - 1);
      } else {
        return null;
      }
    }

    /// <summary>
    /// Creates the record for the given sensor property.
    /// </summary>
    /// <returns>The sensor property record.</returns>
    /// <param name="sp">Sp.</param>
    private IRecord CreateSensorPropertyRecord(ISensorProperty sp) {
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

  /// <summary>
  /// The types of view holders that are present in the adapter.
  /// </summary>
  enum EViewType {
    Footer,
    Manifold,
    Space,
    MeasurementSubview,
    RateOfChangeSubview,
    PTChartSubview,
    SuperheatSubcoolSubview,
    TimerSubview,
  }

  /// <summary>
  /// The interface that represents a "record" item in the adapter. This interface is
  /// used to abstract the types of views in the adapter.
  /// </summary>
  interface IRecord {
    EViewType viewType { get; }
  }

  class WorkbenchRecord<T> : IRecord {
    public EViewType viewType { get; private set; }
    public T item;

    public WorkbenchRecord(EViewType viewType, T item) {
      this.viewType = viewType;
      this.item = item;
    }
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

  class SpaceRecord : IRecord {
    public EViewType viewType {
      get {
        return EViewType.Space;
      }
    }
  }

  class FooterRecord : IRecord {
    public EViewType viewType { 
      get {
        return EViewType.Footer;
      }
    }

    public Action onClick;

    public FooterRecord(Action onClick) {
      this.onClick = onClick;
    }
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

  class SpaceViewHolder : WorkbenchViewHolder<SpaceRecord> {
    public SpaceViewHolder(View view) : base(view) {
    }

    /// <summary>
    /// Binds to.
    /// </summary>
    /// <param name="t">T.</param>
    public override void BindTo(SpaceRecord t) {
    }

    /// <summary>
    /// Unbind this instance.
    /// </summary>
    public override void Unbind() {
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

