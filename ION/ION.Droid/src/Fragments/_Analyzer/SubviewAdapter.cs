namespace ION.Droid.Fragments._Analyzer {
  
  using System;

	using Android.Support.V7.Widget;
	using Android.Support.V7.Widget.Helper;
  using Android.Views;

  using ION.Core.Content;
  using ION.Core.Sensors.Properties;

  using ION.Droid.Util;
  using ION.Droid.Views;
  using ION.Droid.Widgets.RecyclerViews;

  public class SubviewAdapter : RecordAdapter {
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
		private ItemTouchHelper dragger;

    public SubviewAdapter(BitmapCache cache) {
      this.cache = cache;
      manifold = null;
			dragger = new ItemTouchHelper(new Dragger(this));
    }

		public override void OnAttachedToRecyclerView(RecyclerView recyclerView) {
			base.OnAttachedToRecyclerView(recyclerView);
			if (cache == null) {
				cache = new BitmapCache(recyclerView.Context.Resources);
			}
			dragger.AttachToRecyclerView(recyclerView);
		}

		public override void OnDetachedFromRecyclerView(RecyclerView recyclerView) {
			base.OnDetachedFromRecyclerView(recyclerView);
			dragger.AttachToRecyclerView(null);
		}


    /// <summary>
    /// Gets the type of the item view.
    /// </summary>
    /// <returns>The item view type.</returns>
    /// <param name="position">Position.</param>
    public override int GetItemViewType(int position) {
      return (int)records[position].viewType;
    }

		// Overridden from RecordAdapter
		public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType) {
			var rv = recyclerView as SwipeRecyclerView;

			switch ((EViewType)viewType) {
				case EViewType.SHSC: {
						return new SHSCSensorPropertyViewHolder(rv);
				} // EViewType.SHSC
				case EViewType.ROC: {
					return new ROCSensorPropertyViewHolder(rv, cache);
				} // EViewType.ROC
				case EViewType.Timer: {
					return new TimerSensorPropertyViewHolder(rv, cache);
				} // EViewType.Timer
				case EViewType.Secondary: {
					return new SecondarySensorPropertyViewHolder(rv);
				} // EViewType.Secondary
				case EViewType.PT: {
					return new PTSensorPropertyViewHolder(rv);
				} // EViewType.PT
				case EViewType.Simple: {
					return new SimpleSensorPropertyViewHolder(rv, cache);
				} // EViewType.Simple
			}

			throw new Exception("Need a new view holder");
		}

		// Overridden from RecordAdapter
		public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position) {
			var r = records[position];

			holder.ItemView.SetOnLongClickListener(new ViewLongClickAction((view) => {;
				dragger.StartDrag(holder);
			}));

			if (holder is SensorPropertyViewHolder) {
				var spvh = holder as SensorPropertyViewHolder;
				spvh.sensorPropertyRecord = r as SensorPropertyRecord;

				spvh.foreground.SetOnClickListener(new ViewClickAction((view) => {
					if (onSensorPropertyClicked != null) {
						onSensorPropertyClicked(spvh.sensorPropertyRecord.manifold, spvh.sensorPropertyRecord.sensorProperty);
					}
				}));
			}
		}

    /// <summary>
    /// Refreshes the records that are contained within the adapter.
    /// </summary>
    public void RefreshRecords() {
      records.Clear();

			if (manifold != null) {
	      foreach (var sp in manifold.sensorProperties) {
					records.Add(CreateSensorPropertyRecord(manifold, sp));
	      }
			}

      NotifyDataSetChanged();
    }

    /// <summary>
    /// Called when a manifold event is thrown.
    /// </summary>
    /// <param name="me">Me.</param>
    private void OnManifoldEvent(ManifoldEvent me) {
      switch (me.type) {
        case ManifoldEvent.EType.SecondarySensorAdded:
        	goto case ManifoldEvent.EType.Invalidated;
        case ManifoldEvent.EType.SecondarySensorRemoved:
        	goto case ManifoldEvent.EType.Invalidated;
        case ManifoldEvent.EType.Invalidated:
          RefreshRecords();
          break;
        case ManifoldEvent.EType.SensorPropertyAdded:
					records.Add(CreateSensorPropertyRecord(manifold, manifold[me.index]));
          NotifyItemRangeInserted(me.index, 1);
          break;
        case ManifoldEvent.EType.SensorPropertyRemoved:
          records.RemoveAt(me.index);
					NotifyItemRemoved(me.index);
          break;
        case ManifoldEvent.EType.SensorPropertySwapped:
					var i1 = me.index;
					var i2 = me.otherIndex;
					SwapRecords(i1, i2);

					var vh1 = recyclerView.FindViewHolderForAdapterPosition(i1) as SensorPropertyViewHolder;
					var vh2 = recyclerView.FindViewHolderForAdapterPosition(i2) as SensorPropertyViewHolder;
					vh1.Invalidate();
					vh2.Invalidate();
          break;
				case ManifoldEvent.EType.SensorPropertyCleared:
					records.Clear();
					NotifyDataSetChanged();
					break;
      }
    }

		private IRecord CreateSensorPropertyRecord(Manifold m, ISensorProperty sp) {
			if (sp is PTChartSensorProperty) {
				var p = sp as PTChartSensorProperty;
				return new PTSensorPropertyRecord(m, p);
			} else if (sp is SuperheatSubcoolSensorProperty) {
				return new SHSCSensorPropertyRecord(m, sp as SuperheatSubcoolSensorProperty);
			} else if (sp is RateOfChangeSensorProperty) {
				return new ROCSensorPropertyRecord(m, sp as RateOfChangeSensorProperty);
			} else if (sp is TimerSensorProperty) {
				return new TimerSensorPropertyRecord(m, sp as TimerSensorProperty);
			} else if (sp is SecondarySensorProperty) {
				return new SecondarySensorPropertyRecord(m, sp as SecondarySensorProperty);
			} else {
				return new SimpleSensorPropertyRecord(m, sp);
			}
		}

		private void PerformSwap(int i1, int i2) {
			manifold.SwapSensorProperties(i1, i2);
		}

		public enum EViewType {
			Secondary,
			ROC,
			Timer,
			SHSC,
			PT,
			Simple,
		}

		private class Dragger : ItemTouchHelper.Callback {
			private SubviewAdapter adapter;

			public Dragger(SubviewAdapter adapter) {
				this.adapter = adapter;
			}

			public override int GetMovementFlags(RecyclerView recyclerView, RecyclerView.ViewHolder viewHolder) {
				return MakeMovementFlags(ItemTouchHelper.Up | ItemTouchHelper.Down, 0);
			}

			public override bool OnMove(RecyclerView recyclerView, RecyclerView.ViewHolder viewHolder, RecyclerView.ViewHolder target) {
				adapter.PerformSwap(viewHolder.AdapterPosition, target.AdapterPosition);
				return true;
			}

			public override void OnSwiped(RecyclerView.ViewHolder viewHolder, int direction) {
			}
		}
	}
}

