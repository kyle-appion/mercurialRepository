namespace ION.Droid.Fragments._Workbench {

	using System;
	using System.Collections.Generic;

	using Android.Support.V7.Widget;
	using Android.Support.V7.Widget.Helper;
	using Android.Views;

	using ION.Core.Content;
	using ION.Core.Sensors.Properties;

	using ION.Droid.Util;
	using ION.Droid.Views;
	using ION.Droid.Widgets.RecyclerViews;

	public class WorkbenchAdapter : RecordAdapter {

		public event Action<Manifold> onManifoldClicked;
		public event Action<Manifold, ISensorProperty> onSensorPropertyClicked;

		/// <summary>
		/// The workbench that the adapter is displaying.
		/// </summary>
		public Workbench workbench { get; private set; }

		private BitmapCache cache;
		private Action onAddViewer;
		private ItemTouchHelper dragger;
		private HashSet<ManifoldRecord> dragCollapsedManifoldRecords = new HashSet<ManifoldRecord>();

    public WorkbenchAdapter(Action onAddViewer, Workbench workbench, bool hideAdd) {
			this.onAddViewer = onAddViewer;
			this.workbench = workbench;
			dragger = new ItemTouchHelper(new Dragger(this));

			workbench.onWorkbenchEvent += OnWorkbenchEvent;
			foreach (var manifold in workbench.manifolds) {
				var mr = new ManifoldRecord(manifold);
				records.Add(mr);
				ExpandManifold(mr);
				records.Add(new SpaceRecord());
			}

      if (!hideAdd) {
        records.Add(new AddViewerRecord(onAddViewer));
      }
		}

		protected override void Dispose(bool disposing) {
			base.Dispose(disposing);
			workbench.onWorkbenchEvent -= OnWorkbenchEvent;
      cache.Clear();
		}

		public override void OnAttachedToRecyclerView(RecyclerView recyclerView) {
			base.OnAttachedToRecyclerView(recyclerView);
			if (cache == null) {
				cache = new BitmapCache(recyclerView.Context.Resources);
			}
			if (workbench.isEditable) {
				dragger.AttachToRecyclerView(recyclerView);
			}
		}

		public override void OnDetachedFromRecyclerView(RecyclerView recyclerView) {
			base.OnDetachedFromRecyclerView(recyclerView);
			if (workbench.isEditable) {
				dragger.AttachToRecyclerView(null);
			}

      for (int i = 0; i < ItemCount; i++) {
        var vh = recyclerView.FindViewHolderForAdapterPosition(i);
        if (vh is SwipeRecyclerView.ViewHolder) {
          var svh = vh as SwipeRecyclerView.ViewHolder;
          svh.Unbind();
        } else if (vh is RecordViewHolder) {
          var rvh = vh as RecordViewHolder;
          rvh.Unbind();
        }
      }
		}

		// Overridden from RecordAdapter
		public override int GetItemViewType(int position) {
			var r = records[position];
			return r.viewType;
		}

		// Overridden from RecordAdapter
		public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType) {
			var rv = recyclerView as SwipeRecyclerView;

			switch ((EViewType)viewType) {
				case EViewType.Space: {
					return new SpaceViewHolder(parent);
				} // EViewType.Space
				case EViewType.Add: {
					return new AddViewerViewHolder(parent);
				} // EViewType.Add
				case EViewType.Manifold: {
					return new ManifoldViewHolder(rv, cache, workbench);
				} // EViewType.Manifold

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

			if (holder is AddViewerViewHolder) {
				var avvh = holder as AddViewerViewHolder;
				avvh.record = r as AddViewerRecord;
			} else if (holder is ManifoldViewHolder) {
				var mvh = holder as ManifoldViewHolder;
				var mr = r as ManifoldRecord;

				mvh.foreground.SetOnClickListener(new ViewClickAction((view) => {
					NotifyManifoldClick(mr.manifold);
				}));

				mvh.onSerialNumberClicked = (obj) => {
					if (mr.manifold.sensorPropertyCount > 0) {
						DoToggleManifoldExpanded(mr);
					}
				};

				mvh.record = mr;
			} else if (holder is SensorPropertyViewHolder) {
				var spvh = holder as SensorPropertyViewHolder;
				spvh.sensorPropertyRecord = r as SensorPropertyRecord;

				spvh.foreground.SetOnClickListener(new ViewClickAction((view) => {
					NotifySensorPropertyClicked(spvh.sensorPropertyRecord.manifold, spvh.sensorPropertyRecord.sensorProperty);
				}));
			}
		}

		/// <summary>
		/// Queries the manifold index for the given manifold. If the manifold does not exist in the adapter, we will return
		/// -1.
		/// </summary>
		/// <returns>The index for manifold.</returns>
		/// <param name="manifold">Manifold.</param>
		public int AdapterIndexForManifold(Manifold manifold) {
			var ret = 0;

			foreach (var record in records) {
				var mr = record as ManifoldRecord;
				if (mr != null && mr.manifold.Equals(manifold)) {
					return ret;
				}

				ret++;
			}

			return -1;
		}

		/// <summary>
		/// Queries whether or not the adapter contains the given manifold.
		/// </summary>
		/// <param name="manifold">Manifold.</param>
		public bool Contains(Manifold manifold) {
			return AdapterIndexForManifold(manifold) > 0;
		}

		public void ExpandManifold(Manifold manifold) {
			var r = records[AdapterIndexForManifold(manifold)] as ManifoldRecord;
			if (r != null) {
				ExpandManifold(r);
			}
		}

		private void DoToggleManifoldExpanded(ManifoldRecord mr) {
			if (mr.isExpanded) {
				CollapseManifold(mr);
			} else {
				ExpandManifold(mr);
			}
		}

		private void CollapseManifold(ManifoldRecord mr, bool refresh=true) {
			var i = AdapterIndexForManifold(mr.manifold) + 1;

			if (mr.isExpanded) {
				var cnt = mr.manifold.sensorPropertyCount;
				records.RemoveRange(i, cnt);
				NotifyItemRangeRemoved(i, cnt);
			}

			if (refresh) {
				NotifyItemChanged(i - 1);
			}
			mr.isExpanded = false;
		}

		private void ExpandManifold(ManifoldRecord mr) {
			if (mr.isExpanded) {
				return;
			}

			var i = AdapterIndexForManifold(mr.manifold) + 1;

			if (!mr.isExpanded) {
				var cnt = mr.manifold.sensorPropertyCount;
				for (int j = cnt - 1; j >= 0; j--) {
					records.Insert(i, CreateSensorPropertyRecord(mr.manifold, mr.manifold[j]));
				}
				NotifyItemRangeInserted(i, cnt);
			}

			NotifyItemChanged(i - 1);
			mr.isExpanded = true;
		}

		/// <summary>
		/// Queries the adapter size of the given manifold (this size of the manifold and all expanded subviews.
		/// </summary>
		/// <returns>The size for manifold.</returns>
		/// <param name="manifold">Manifold.</param>
		private int AdapterSizeForManifold(ManifoldRecord mr) {
			return 1 + (mr.isExpanded ? mr.manifold.sensorPropertyCount : 0) + 1;
		}

		private IRecord CreateSensorPropertyRecord(Manifold manifold, ISensorProperty sp) {
			if (sp is PTChartSensorProperty) {
				var p = sp as PTChartSensorProperty;
				return new PTSensorPropertyRecord(manifold, p);
			} else if (sp is SuperheatSubcoolSensorProperty) {
				return new SHSCSensorPropertyRecord(manifold, sp as SuperheatSubcoolSensorProperty);
			} else if (sp is RateOfChangeSensorProperty) {
				return new ROCSensorPropertyRecord(manifold, sp as RateOfChangeSensorProperty);
			} else if (sp is TimerSensorProperty) {
				return new TimerSensorPropertyRecord(manifold, sp as TimerSensorProperty);
			} else if (sp is SecondarySensorProperty) {
				return new SecondarySensorPropertyRecord(manifold, sp as SecondarySensorProperty);
			} else {
				return new SimpleSensorPropertyRecord(manifold, sp);
			}
		}

		private void BeginDrag(RecyclerView.ViewHolder vh) {
			if (vh is ManifoldViewHolder) {
				for (int i = records.Count - 1; i  >= 0; i--) {
					var mr = records[i] as ManifoldRecord;
					if (mr != null) {
						CollapseManifold(mr, false);
						dragCollapsedManifoldRecords.Add(mr);
					}
				}
			}
		}

		private void EndDrag() {
			foreach (var mr in dragCollapsedManifoldRecords) {
				this.ExpandManifold(mr);
			}

			dragCollapsedManifoldRecords.Clear();
		}

		private bool IsPositionDraggable(RecyclerView.ViewHolder vh) {
			return vh is ManifoldViewHolder || vh is SensorPropertyViewHolder;
		}

		private bool IsSwapableWith(RecyclerView.ViewHolder vh1, RecyclerView.ViewHolder vh2) {
			if (vh1 == vh2) {
				return false;
			} else if (vh1 is ManifoldViewHolder && vh2 is ManifoldViewHolder) {
				return true;
			} else if (vh1 is SensorPropertyViewHolder && vh2 is SensorPropertyViewHolder) {
				var sr1 = records[vh1.AdapterPosition] as SensorPropertyRecord;
				var sr2 = records[vh2.AdapterPosition] as SensorPropertyRecord;
				return sr1.manifold.Equals(sr2.manifold);
			} else {
				return false;
			}
		}

		private void PerformSwap(RecyclerView.ViewHolder vh1, RecyclerView.ViewHolder vh2) {
			if (vh1 is ManifoldViewHolder && vh2 is ManifoldViewHolder) {
				var m1 = ((ManifoldViewHolder)vh1).record.manifold;
				var m2 = ((ManifoldViewHolder)vh2).record.manifold;
				workbench.Swap(workbench.IndexOf(m1), workbench.IndexOf(m2));
			} else if (vh1 is SensorPropertyViewHolder && vh2 is SensorPropertyViewHolder) {
				var spr1 = records[vh1.AdapterPosition] as SensorPropertyRecord;
				var spr2 = records[vh2.AdapterPosition] as SensorPropertyRecord;
				var m = spr1.manifold;

				m.SwapSensorProperties(m.IndexOfSensorProperty(spr1.sensorProperty), m.IndexOfSensorProperty(spr2.sensorProperty));
			}
		}

		private void OnWorkbenchEvent(WorkbenchEvent e) {
			switch (e.type) {
				case WorkbenchEvent.EType.Added:
					if (!Contains(e.manifold)) {
						var i = 0;
						var mr = new ManifoldRecord(e.manifold);
						if (e.index == 0) {
							records.Insert(i, new SpaceRecord());
							records.Insert(i, mr);
							NotifyItemRangeInserted(i, 2);
						} else {
							i = AdapterIndexForManifold(workbench[e.index - 1]);
							i += AdapterSizeForManifold(records[i] as ManifoldRecord);
							records.Insert(i, new SpaceRecord());
							records.Insert(i, mr);
							NotifyItemRangeInserted(i, 2);
						}

						if (mr.manifold.sensorPropertyCount > 0) {
							ExpandManifold(mr);
						}
					}
				break; // WorkbenchEvent.EType.Added
				case WorkbenchEvent.EType.ManifoldEvent: {
					OnManifoldEvent(e.manifoldEvent);
				} break; // WorkbenchEvent.EType.ManifoldEvent
				case WorkbenchEvent.EType.Removed: {
					var i = AdapterIndexForManifold(e.manifold);
					if (i != -1) {
						var cnt = AdapterSizeForManifold(records[i] as ManifoldRecord);
						records.RemoveRange(i, cnt);
						NotifyItemRangeRemoved(i, cnt);
					}
				} break; // WorkbenchEvent.EType.Removed
				case WorkbenchEvent.EType.Swapped: {
					SwapRecords(AdapterIndexForManifold(workbench[e.index]), AdapterIndexForManifold(workbench[e.otherIndex]));
				} break; // WorkbenchEvent.EType.Swapped
			}
		}

		private void OnManifoldEvent(ManifoldEvent e) {
			switch (e.type) {
				case ManifoldEvent.EType.SensorPropertyAdded: {
					var aifm = AdapterIndexForManifold(e.manifold);
					NotifyItemChanged(aifm);
					var mr = records[aifm] as ManifoldRecord;
					if (mr.manifold.sensorPropertyCount == 1) {
						mr.isExpanded = true;
					}
					if (mr.isExpanded) {
						var i = aifm + e.index + 1;
						var record = CreateSensorPropertyRecord(e.manifold, e.manifold[e.index]);
						records.Insert(i, record);
						NotifyItemInserted(i);
						if (mr.manifold.sensorPropertyCount > 1) {
								NotifyItemChanged(aifm + mr.manifold.sensorPropertyCount);
							} else {
								NotifyItemChanged(aifm - 1);
							}
						}
				} break; // ManifoldEvent.EType.SensorPropertyAdded
				case ManifoldEvent.EType.SensorPropertyRemoved: {
					var aifm = AdapterIndexForManifold(e.manifold);
					NotifyItemChanged(aifm);
					var i = aifm + e.index + 1;
					records.RemoveAt(i);
					NotifyItemRemoved(i);
					if (e.manifold.sensorPropertyCount > 1) {
						NotifyItemChanged(aifm + e.manifold.sensorPropertyCount);
					} else {
						NotifyItemChanged(aifm - 1);
					}
				} break; // ManifoldEvent.EType.SensorPropertyRemoved
				case ManifoldEvent.EType.SensorPropertySwapped: {
					var m = e.manifold;
					var mi = AdapterIndexForManifold(m) + 1;
					var i1 = mi + e.index;
					var i2 = mi + e.otherIndex;
					SwapRecords(i1, i2);
/*
					var vh1 = recyclerView.FindViewHolderForAdapterPosition(i1) as SensorPropertyViewHolder;
					var vh2 = recyclerView.FindViewHolderForAdapterPosition(i2) as SensorPropertyViewHolder;
					vh1.Invalidate();
					vh2.Invalidate();
*/
				} break; // ManifoldEvent.EType.SensorPropertySwapped
			}
		}

		private void NotifyManifoldClick(Manifold manifold) {
			if (onManifoldClicked != null) {
				onManifoldClicked(manifold);
			}
		}

		private void NotifySensorPropertyClicked(Manifold manifold, ISensorProperty sp) {
			if (onSensorPropertyClicked != null) {
				onSensorPropertyClicked(manifold, sp);
			}
		}

		public enum EViewType {
			Space,

			Add,
			Manifold,

			Secondary,
			ROC,
			Timer,
			SHSC,
			PT,
			Simple,
		}

		private class Dragger : ItemTouchHelper.Callback {
			private WorkbenchAdapter adapter;

			public Dragger(WorkbenchAdapter adapter) {
				this.adapter = adapter;
			}

			public override int GetMovementFlags(RecyclerView recyclerView, RecyclerView.ViewHolder viewHolder) {
				return MakeMovementFlags(ItemTouchHelper.Up | ItemTouchHelper.Down, 0);
			}

			public override void OnSelectedChanged(RecyclerView.ViewHolder viewHolder, int actionState) {
				adapter.BeginDrag(viewHolder);
			}

			public override bool OnMove(RecyclerView recyclerView, RecyclerView.ViewHolder viewHolder, RecyclerView.ViewHolder target) {
				if (adapter.IsSwapableWith(viewHolder, target)) {
					adapter.PerformSwap(viewHolder, target);
					return true;
				} else {
					return false;
				}
			}

			public override void ClearView(RecyclerView recyclerView, RecyclerView.ViewHolder viewHolder) {
				base.ClearView(recyclerView, viewHolder);
				adapter.EndDrag();
			}

			public override void OnSwiped(RecyclerView.ViewHolder viewHolder, int direction) {
			}
		}
	}
}
