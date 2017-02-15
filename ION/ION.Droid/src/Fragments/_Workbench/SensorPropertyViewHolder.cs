namespace ION.Droid.Fragments._Workbench {

	using System;

	using Android.OS;
	using Android.Views;
	using Android.Widget;

	using ION.Core.Content;
	using ION.Core.Sensors;
	using ION.Core.Sensors.Properties;

	using ION.Droid.Util;
	using ION.Droid.Sensors.Properties;
	using ION.Droid.Widgets.RecyclerViews;
	using ION.Droid.Views;

	public abstract class SensorPropertyRecord : RecordAdapter.IRecord {
		// Implemented from RecordAdapter.IRecord
		public int viewType { get; private set; }

		public Manifold manifold;
		public ISensorProperty sensorProperty;

		protected SensorPropertyRecord(Manifold manifold, ISensorProperty sensorProperty, WorkbenchAdapter.EViewType viewType) {
			this.manifold = manifold;
			this.sensorProperty = sensorProperty;
			this.viewType = (int)viewType;
		}
	}

	public abstract class SensorPropertyRecord<T> : SensorPropertyRecord where T : ISensorProperty {
		public T sp { get { return (T)sensorProperty; } }

		protected SensorPropertyRecord(Manifold manifold, T t, WorkbenchAdapter.EViewType viewType) : base(manifold, t, viewType) {
		}
	}

	public abstract class SensorPropertyViewHolder : SwipeRecyclerView.ViewHolder {
		public SensorPropertyViewHolder(SwipeRecyclerView recyclerView, int foregroundLayout) : base(recyclerView, foregroundLayout, Resource.Layout.list_item_button) {
		}
	}

	public class SensorPropertyViewHolder<T> : SensorPropertyViewHolder where T : ISensorProperty {
		public SensorPropertyRecord<T> record {
			get {
				return __record;
			}
			set {
				Unbind();
				__record = value;
				if (__record != null) {
					__record.sp.onSensorPropertyChanged += OnSensorPropertyChanged;
					Invalidate();
				}
			}
		} SensorPropertyRecord<T> __record;

		private View association;

		public SensorPropertyViewHolder(SwipeRecyclerView recyclerView, int foregroundLayout) : base(recyclerView, foregroundLayout) {
			association = ItemView.FindViewById(Resource.Id.association);

			var button = background as TextView;
			button.SetText(Resource.String.remove);
			button.SetOnClickListener(new ViewClickAction((view) => {
				record.manifold.RemoveSensorProperty(record.sp);
			}));
		}

		public override void Unbind() {
			base.Unbind();
			if (record != null) {
				record.sp.onSensorPropertyChanged -= OnSensorPropertyChanged;
			}
		}

		public virtual void Invalidate() {
			if (association != null) {
				if (record.manifold.IndexOfSensorProperty(record.sp) >= record.manifold.sensorPropertyCount - 1) {
					association.SetBackgroundResource(Resource.Drawable.ic_association);
				} else {
					association.SetBackgroundResource(Resource.Drawable.ic_association_linked);
				}
			}
		}

		private void OnSensorPropertyChanged(ISensorProperty sensorProperty) {
			Invalidate();
		}
	}
}
