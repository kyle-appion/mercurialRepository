namespace ION.Droid.Widgets.Analyzer {
  
  using System;

  using Android.Views;

  using ION.Core.Content;
  using ION.Core.Sensors.Properties;
	using ION.Core.Util;

  using ION.Droid.Util;
  using ION.Droid.Views;
  using ION.Droid.Widgets.RecyclerViews;

  public class SubviewAdapter : SwipableRecyclerViewAdapter {
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
					RefreshRecords();
          __manifold.onManifoldEvent += OnManifoldEvent;
        }
      }
    } Manifold __manifold;

    /// <summary>
    /// The bitmap cache that will contain all the flyweight bitmaps for the adapter's views.
    /// </summary>
    private BitmapCache cache;

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
		public override SwipableViewHolder OnCreateSwipableViewHolder(ViewGroup parent, int viewType) {
      switch ((EViewType)viewType) {
        case EViewType.MeasurementSubview:
          return new MeasurementSubviewViewHolder(parent, cache);
        case EViewType.PTChartSubview:
          return new PTChartSubviewViewHolder(parent);
        case EViewType.SuperheatSubcoolSubview:
				return new SuperheatSubcoolSubviewViewHolder(parent);
        case EViewType.TimerSubview:
          return new TimerSubviewViewHolder(parent, cache);
        case EViewType.RateOfChangeSubview:
          return new RateOfChangeSubviewViewHolder(parent, cache);
        default:
          throw new Exception("Unknown view type: " + viewType);
      }
    }

    /// <summary>
    /// Raises the bind view holder event.
    /// </summary>
    /// <param name="holder">Holder.</param>
    /// <param name="position">Position.</param>
    public override void OnBindViewHolder(IRecord record, SwipableViewHolder holder, int position) {
      var viewType = GetItemViewType(position);
			var r = record as SensorPropertyRecord;
			holder.button.SetText(Resource.String.remove);

      switch ((EViewType)viewType) {
        case EViewType.MeasurementSubview:
          var mr = records[position] as MeasurementRecord;
					(holder as MeasurementSubviewViewHolder).record = mr;
          break;

        case EViewType.RateOfChangeSubview:
          var rr = r as RateOfChangeSubviewRecord;
					(holder as RateOfChangeSubviewViewHolder).record = rr;
          break;

        case EViewType.TimerSubview:
          var tr = r as TimerSubviewRecord;
					(holder as TimerSubviewViewHolder).record = tr;
          break;

        case EViewType.PTChartSubview:
          var pr = r as PTChartSubviewRecord;
					(holder as PTChartSubviewViewHolder).record = pr;
          break;

        case EViewType.SuperheatSubcoolSubview:
          var shr = r as SuperheatSubcoolSubviewRecord;
					(holder as SuperheatSubcoolSubviewViewHolder).record = shr;
          break;
      }

      holder.ItemView.SetOnClickListener(new ViewClickAction((v) => {
        var sp = records[position];
        if (sp != null && onSensorPropertyClicked != null) {
          onSensorPropertyClicked(manifold, r.sensorProperty);
        }
      }));
    }

		public override bool IsViewHolderSwipable(SwipableRecyclerViewAdapter.IRecord record, SwipableViewHolder viewHolder, int index) {
			return true;
		}

		public override Action GetViewHolderSwipeAction(int index) {
			Log.D(this, "Before we returned an action, we had: " + manifold.sensorPropertyCount);
			return () => {
				manifold.RemoveSensorPropertyAt(index);
				Log.D(this, "The number of subviews in the manifold after remove: " + manifold.sensorPropertyCount);
			};
		}

    /// <summary>
    /// Raises the view detached from window event.
    /// </summary>
    /// <param name="holder">Holder.</param>
    public override void OnViewDetachedFromWindow(Java.Lang.Object holder) {
      base.OnViewDetachedFromWindow(holder);

      (holder as SwipableViewHolder)?.Unbind();
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
					this.NotifyItemRemoved(me.index);
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

	class SensorPropertyRecord : SwipableRecyclerViewAdapter.IRecord {
		public int viewType { get { return (int)__viewType; } } EViewType __viewType;
    public ISensorProperty sensorProperty { get; private set; }

    public SensorPropertyRecord(EViewType viewType, ISensorProperty sensorProperty) {
			__viewType = viewType;
      this.sensorProperty = sensorProperty;
    }
  }

  class SensorPropertyRecord<T> : SensorPropertyRecord where T : ISensorProperty {
    public T item;

    public SensorPropertyRecord(EViewType viewType, T item) : base(viewType, item) {
      this.item = item;
    }
  }
}

