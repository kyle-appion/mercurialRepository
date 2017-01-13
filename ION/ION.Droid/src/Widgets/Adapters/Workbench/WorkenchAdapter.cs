namespace ION.Droid.Widgets.Adapters.Workbench {

  using System;
  using System.Collections.Generic;

  using Android.Content.Res;
  using Android.Support.V7.Widget;
  using Android.Support.V7.Widget.Helper;
  using Android.Views;
  using Android.Widget;

	using Appion.Commons.Util;

  using ION.Core.App;
  using ION.Core.Content;
  using ION.Core.Sensors.Properties;

  using ION.Droid.Util;
  using ION.Droid.Views;
  using ION.Droid.Widgets.RecyclerViews;

  /// <summary>
  /// The adapter that will provide the views for use in the workbench fragment. After creating the adapter, if you want
  /// to rearrange the adapter, you will need to assign the proper listeners (ie. dragListener for dragging, and
  /// swipeListener for swipping).
  /// that you assign
  /// </summary>
	public class WorkbenchAdapter : SwipableRecyclerViewAdapter {

    public delegate void OnManifoldClicked(Manifold manifold);
    public delegate void OnSensorPropertyClicked(Manifold manifold, ISensorProperty sensorProperty);

    public event OnManifoldClicked onManifoldClicked;
    public event OnSensorPropertyClicked onSensorPropertyClicked;

    /// <summary>
    /// The workbench that the adapter is currently working with.
    /// </summary>
    /// <value>The workbench.</value>
    public Workbench workbench { get; internal set; }

    /// <summary>
    /// The current ION instance.
    /// </summary>
    /// <value>The ion.</value>
    private IION ion;
    /// <summary>
    /// The bitmap cache that will contain all of the bitmaps used in the adapter.
    /// </summary>
    /// <value>The cache.</value>
    private BitmapCache cache;
    /// <summary>
    /// The drag decoration.
    /// </summary>
//    private ItemTouchHelper dragDecoration;
    /// <summary>
    /// The stack of pending expands.
    /// </summary>
    private Stack<List<Tuple<Manifold, bool>>> expansionStack = new Stack<List<Tuple<Manifold, bool>>>();

    public WorkbenchAdapter(IION ion, Resources resources) {
      this.ion = ion;
      this.cache = new BitmapCache(resources);
//			dragDecoration = new ItemTouchHelper(new WorkbenchDragDecoration(this, swipes));
			touchHelperDecoration = new ItemTouchHelper(new WorkbenchDragDecoration(this));
    }

    /// <summary>
    /// Raises the attached to recycler view event.
    /// </summary>
    /// <param name="recyclerView">Recycler view.</param>
    public override void OnAttachedToRecyclerView(RecyclerView recyclerView) {
      base.OnAttachedToRecyclerView(recyclerView);
//      dragDecoration.AttachToRecyclerView(recyclerView);
    }

    /// <summary>
    /// Raises the detached from recycler view event.
    /// </summary>
    /// <param name="recyclerView">Recycler view.</param>
    public override void OnDetachedFromRecyclerView(RecyclerView recyclerView) {
      base.OnDetachedFromRecyclerView(recyclerView);
    }

    // Overridden from RecyclerView.Adapter
    public override int GetItemViewType(int position) {
      return (int)records[position].viewType;
    }

    // Overridden from RecyclerView.Adapter
    public override SwipableViewHolder OnCreateSwipableViewHolder(ViewGroup parent, int viewType) {
      switch ((EViewType)viewType) {
        case EViewType.Footer:
          return new FooterViewHolder(parent, Resource.Layout.list_item_add);
        case EViewType.Manifold:
          return new ManifoldViewHolder(parent, Resource.Layout.viewer_large, cache);
        case EViewType.Space:
          return new SpaceViewHolder(parent, Resource.Layout.list_item_space);
        case EViewType.MeasurementSubview:
          return new MeasurementSubviewViewHolder(parent, Resource.Layout.subview_measurement_large, cache);
        case EViewType.PTChartSubview:
          return new PTChartSubviewViewHolder(parent, Resource.Layout.subview_fluid_large);
        case EViewType.SuperheatSubcoolSubview:
          return new SuperheatSubcoolSubviewViewHolder(parent, Resource.Layout.subview_fluid_large);
				case EViewType.SecondarySensorSubview:
					return new SecondarySensorSubviewViewHolder(parent, Resource.Layout.subview_measurement_large);
        case EViewType.TimerSubview:
          return new TimerSubviewViewHolder(parent, Resource.Layout.subview_timer_large, cache);
        case EViewType.RateOfChangeSubview:
          return new RateOfChangeSubviewViewHolder(parent, Resource.Layout.subview_measurement_large, cache);
        default:
          throw new Exception("Unknown view type: " + viewType);
      }
    }

    /// <summary>
    /// Binds the given record to the view holder.
    /// </summary>
    /// <param name="record">Record.</param>
    /// <param name="vh">Vh.</param>
    /// <param name="position">Position.</param>
    /// <param name="holder">Holder.</param>
    public override void OnBindViewHolder(IRecord record, SwipableViewHolder holder, int position) {
      var viewType = record.viewType;

      if (record is SensorPropertyRecord) {
        BuildSensorPropertyViewHolder(holder, position);
        holder.button.Text = holder.button.Context.GetString(Resource.String.remove);
      } else {
        switch ((EViewType)viewType) {
          case EViewType.Footer:
            (holder as FooterViewHolder).record = record;
            break;
          case EViewType.Manifold:
            var template = (holder as ManifoldViewHolder).template;
            holder.button.Text = holder.button.Context.GetString(Resource.String.remove);

            var vr = records[position] as ManifoldRecord;

            if (vr != null) {
              (holder as ManifoldViewHolder).record = vr;
            }

            holder.ItemView.SetOnClickListener(new ViewClickAction((v) => {
              if (onManifoldClicked != null) {
                onManifoldClicked(vr.item);
              }
            }));

            if (template.toggle != null) {
              if (vr.item.sensorPropertyCount > 0) {
                template.arrow.Visibility = ViewStates.Visible;
                template.toggle.SetOnClickListener(new ViewClickAction((v) => {
                  ToggleManifold(vr.item);
                  template.MarkAsExpanded(IsManifoldExpanded(vr.item));
                }));
              } else {
                template.arrow.Visibility = ViewStates.Gone;
                template.toggle.SetOnClickListener(null);
              }
              template.MarkAsExpanded(IsManifoldExpanded(vr.item));
            }
            break;
          case EViewType.Space:
            var sr = records[position] as SpaceRecord;

            if (sr != null) {
              (holder as SpaceViewHolder).record = sr;
            }
            break;
          default:
            throw new Exception("Unknown view type: " + viewType);
        }
      }
    }

    /// <summary>
    /// Queries whether or not the given view holder is swipeable.
    /// </summary>
    /// <returns>true</returns>
    /// <c>false</c>
    /// <param name="viewHolder">View holder.</param>
    /// <param name="index">Index.</param>
    public override bool IsViewHolderSwipable(IRecord record, SwipableViewHolder viewHolder, int index) {
      return record is ManifoldRecord || record is SensorPropertyRecord;
    }

    /// <summary>
    /// Queries the action that is triggered when the swipe revealed button is clicked.
    /// </summary>
    /// <returns>The view holder swipe action.</returns>
    /// <param name="index">Index.</param>
    public override Action GetViewHolderSwipeAction(int index) {
			IRecord record = null;
			try {
      	record = records[index];
			} catch (Exception e) {
				Log.E(this, "Failed to get Swipe action for index: " + index, e);
#if DEBUG
				Toast.MakeText(recyclerView.Context, "Failed to get Swipe Action! See Logs", ToastLength.Long).Show();
#endif
				return null;
			}
      if (record is SensorPropertyRecord) {
        var spr = record as SensorPropertyRecord;
        var manifold = spr.manifold;
        return () => {
          manifold.RemoveSensorProperty(spr.sensorProperty);
        };
      } else if (record is ManifoldRecord) {
        var mr = record as ManifoldRecord;
        return () => {
          workbench.Remove(mr.item);
        };
      } else {
        return () => {
          Log.E(this, "GetViewHolderSwipeAction returned null action");
        };
      }
    }

    // Overridden from RecyclerView.Adapter
    public override void OnViewDetachedFromWindow(Java.Lang.Object holder) {
      base.OnViewDetachedFromWindow(holder);

      (holder as SwipableViewHolder)?.Unbind();
    }

		/// <summary>
		/// Updates a specific subview record.
		/// </summary>
		/// <param name="manifold">Manifold.</param>
		/// <param name="index">Index.</param>
		public void UpdateManifoldSubview(Manifold manifold, int index) {
			if (IsManifoldExpanded(manifold)) {
				var i = IndexOfManifold(manifold);
				NotifyItemChanged(i + index + 1);
			}
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
				var mr = new ManifoldRecord(m);
        records.Add(mr);
				records.Add(new SpaceRecord());

				if (m.sensorPropertyCount > 0) {
					ExpandManifold(m);
				}
      }

      records.Add(new FooterRecord(footerAction));

      NotifyDataSetChanged();
    }

    /// <summary>
    /// Raises the workbench event event.
    /// </summary>
    /// <param name="workbenchEvent">Workbench event.</param>
    public void OnWorkbenchEvent(WorkbenchEvent workbenchEvent) {
			try {
	      var manifold = workbenchEvent.manifold;
	      int startIndex = IndexOfManifold(manifold);
	      int manifoldSensorPropertyCount = manifold.sensorPropertyCount;

	      switch (workbenchEvent.type) {
	        case WorkbenchEvent.EType.Added:
	          if (startIndex < 0) {
	            var i = records.Count - 1;
	            records.Insert(i/*records.Count - 1*/, new ManifoldRecord(manifold));
	            records.Insert(i + 1, new SpaceRecord());
	            NotifyItemRangeInserted(i, 2);
	          } else {
	            records.Insert(startIndex, new SpaceRecord());
	            records.Insert(startIndex, new ManifoldRecord(manifold));
	            NotifyItemRangeInserted(startIndex, 2);
	          }
	          break;
	        case WorkbenchEvent.EType.ManifoldEvent:
	          OnManifoldEvent(workbenchEvent.manifoldEvent);
	          break;
	        case WorkbenchEvent.EType.Removed:
	          if (startIndex >= 0) {
	            records.RemoveAt(startIndex);
	            for (int i = 0; i < manifoldSensorPropertyCount; i++) {
	              records.RemoveAt(startIndex);
	            }
	            var count = 1 + manifoldSensorPropertyCount;
	            if (records[startIndex] is SpaceRecord) {
	              records.RemoveAt(startIndex);
	              count++;
	            }
	            NotifyItemRangeRemoved(startIndex, count);
	          }
	          break;
	        case WorkbenchEvent.EType.Swapped:
	          var sm = workbench[workbenchEvent.otherIndex];
	          var secondIndex = IndexOfManifold(sm);
	          var offset = manifoldSensorPropertyCount + 1;

	          var tmp = records[startIndex];
	          records[startIndex] = records[secondIndex];
	          records[secondIndex] = tmp;
	          NotifyItemMoved(startIndex, secondIndex);

	          break;
	        default:
	          throw new Exception("No case for workbenchtype: " + workbenchEvent.type);  
	      }
			} catch (Exception e) {
				NotifyDataSetChanged();
				Log.E(this, "Index exception occured while trying to remove stuff from the workbench", e);
#if DEBUG
				Toast.MakeText(recyclerView.Context, "AN AMAZING ERROR OCCURED", ToastLength.Long).Show();
#endif
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

					records.Insert(index, CreateSensorPropertyRecord(manifold, manifold[manifoldEvent.index]));

					NotifyItemChanged(manifoldIndex);
					NotifyItemInserted(index);
				break;

				case ManifoldEvent.EType.SensorPropertyRemoved:
					index = manifoldIndex + 1 + manifoldEvent.index;
					records.RemoveAt(index);
					NotifyItemChanged(manifoldIndex);
					NotifyItemRemoved(index);
				break;

				case ManifoldEvent.EType.SensorPropertySwapped:
					var from = manifoldIndex + 1 + manifoldEvent.index;
					var to = manifoldIndex + 1 + manifoldEvent.otherIndex;
					var tmp = records[from];

					records[from] = records[to];
					records[to] = tmp;

					NotifyItemMoved(from, to);

				break;
			}
		}

    /// <summary>
    /// Pushes a new manifold expanssion state, effectively saving whether or not a manifold is expanded.
    /// </summary>
    public void SaveManifoldExpansionState() {
      var tuples = new List<Tuple<Manifold, bool>>();
      foreach (var m in workbench.manifolds) {
        tuples.Add(new Tuple<Manifold, bool>(m, IsManifoldExpanded(m)));
        CollapseManifold(m, false);
      }
      expansionStack.Push(tuples);
    }

    /// <summary>
    /// Restores the manifold expanssion states.
    /// </summary>
    public void RestoreManifoldExpansionState() {
      var tuples = expansionStack.Pop();
      if (tuples != null) {
        foreach (var t in tuples) {
          if (t.Item2) {
            ExpandManifold(t.Item1);
          }
        }
      }
    }

    public void CollapseManifold(Manifold manifold, bool refresh=true) {
      var index = IndexOfManifold(manifold);
      var mr = records[index] as ManifoldRecord;

      if (mr != null) {
        if (mr.expanded) {
          var count = manifold.sensorPropertyCount;

          for (int i = count; i >= 1; i--) {
            records.RemoveAt(index + i);
          }

					if (refresh) {
	          NotifyItemChanged(index);
					}
          NotifyItemRangeRemoved(index + 1, count);
        }

        mr.expanded = false;
      }
    }

    public void ExpandManifold(Manifold manifold) {
      var index = IndexOfManifold(manifold);
      var mr = records[index] as ManifoldRecord;

      if (mr != null) {
        if (!mr.expanded) {
          var count = manifold.sensorPropertyCount;

          for (int i = 1; i <= count; i++) {
            records.Insert(index + i, CreateSensorPropertyRecord(manifold, manifold[i - 1]));
          }

          NotifyItemChanged(index);
          NotifyItemRangeInserted(index + 1, count);
        }

        mr.expanded = true;
      }
    }

    /// <summary>
    /// Queries whether or not the manifold is expanded.
    /// </summary>
    /// <returns><c>true</c> if this instance is manifold expanded the specified manifold; otherwise, <c>false</c>.</returns>
    /// <param name="manifold">Manifold.</param>
    public bool IsManifoldExpanded(Manifold manifold) {
      var index = IndexOfManifold(manifold);
      var mr = records[index] as ManifoldRecord;

      if (mr != null) {
        return mr.expanded;
      }

      return false;
    }

    public void ToggleManifold(Manifold manifold) {
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
          (holder as MeasurementSubviewViewHolder).record = mr;
          break;

        case EViewType.RateOfChangeSubview:
          var rr = record as RateOfChangeSubviewRecord;
          (holder as RateOfChangeSubviewViewHolder).record = rr;
          break;

        case EViewType.TimerSubview:
          var tr = record as TimerSubviewRecord;
          (holder as TimerSubviewViewHolder).record = tr;
          break;

        case EViewType.PTChartSubview:
          var pr = record as PTChartSubviewRecord;
          (holder as PTChartSubviewViewHolder).record = pr;
          break;

        case EViewType.SuperheatSubcoolSubview:
          var shr = record as SuperheatSubcoolSubviewRecord;
          (holder as SuperheatSubcoolSubviewViewHolder).record = shr;
          break;

				case EViewType.SecondarySensorSubview:
					var ssr = record as SecondarySensorSubviewRecord;
					(holder as SecondarySensorSubviewViewHolder).record = ssr;
					break;
      }

			holder.ItemView.SetOnClickListener(new ViewClickAction((v) => {
        var manifold = FindManifoldAtIndex(position);
        if (manifold != null && onSensorPropertyClicked != null) {
          onSensorPropertyClicked(manifold, record.sensorProperty);
        }
			}));
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
    private IRecord CreateSensorPropertyRecord(Manifold manifold, ISensorProperty sp) {
      SensorPropertyRecord ret = null;

      if (sp is PTChartSensorProperty) {
        ret = new PTChartSubviewRecord(sp as PTChartSensorProperty);
      } else if (sp is SuperheatSubcoolSensorProperty) {
        ret = new SuperheatSubcoolSubviewRecord(sp as SuperheatSubcoolSensorProperty);
      } else if (sp is TimerSensorProperty) {
        ret = new TimerSubviewRecord(sp as TimerSensorProperty);
      } else if (sp is RateOfChangeSensorProperty) {
        ret = new RateOfChangeSubviewRecord(sp as RateOfChangeSensorProperty);
			} else if (sp is SecondarySensorProperty) {
				ret = new SecondarySensorSubviewRecord(sp as SecondarySensorProperty);
      } else {
        ret = new MeasurementRecord(sp);
      }

      ret.manifold = manifold;
      return ret;
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
		SecondarySensorSubview,
    TimerSubview,
  }

  class WorkbenchRecord<T> : SwipableRecyclerViewAdapter.IRecord {
    public int viewType { get; private set; }
    public T item;

    public WorkbenchRecord(EViewType viewType, T item) {
      this.viewType = (int)viewType;
      this.item = item;
    }
  }

  class SensorPropertyRecord : SwipableRecyclerViewAdapter.IRecord {
    public int viewType { get; private set; }
    public Manifold manifold { get; set; }
    public ISensorProperty sensorProperty { get; private set; }

    public SensorPropertyRecord(EViewType viewType, ISensorProperty sensorProperty) {
      this.viewType = (int)viewType;
      this.sensorProperty = sensorProperty;
    }
  }

  class SensorPropertyRecord<T> : SensorPropertyRecord where T : ISensorProperty {
    public T item;

    public SensorPropertyRecord(EViewType viewType, T item) : base(viewType, item) {
      this.item = item;
    }
  }

  class SpaceRecord : SwipableRecyclerViewAdapter.IRecord {
    public int viewType {
      get {
        return (int)EViewType.Space;
      }
    }
  }

  class FooterRecord : SwipableRecyclerViewAdapter.IRecord {
    public int viewType { 
      get {
        return (int)EViewType.Footer;
      }
    }

    public Action onClick;

    public FooterRecord(Action onClick) {
      this.onClick = onClick;
    }
  }

  class SpaceViewHolder : SwipableViewHolder<SpaceRecord> {

    public SpaceViewHolder(ViewGroup parent, int viewResource) : base(parent, viewResource, false) {
    }
  }

  class FooterViewHolder : SwipableViewHolder<FooterRecord> {
    private View add;
    private TextView text;

    public FooterViewHolder(ViewGroup parent, int viewResource) : base(parent, viewResource) {
      add = view.FindViewById(Resource.Id.add);
      text = add.FindViewById<TextView>(Resource.Id.text);
    }

    // Overridden from WorkbenchViewHolder
    public override void OnBindTo() {
      var c = ItemView.Context;
      text.Text = c.GetString(Resource.String.workbench_add_viewer);

      add.SetOnClickListener(new ViewClickAction((view) => {
        t.onClick();
      }));
    }
  }
}

