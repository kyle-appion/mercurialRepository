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

	using L = Appion.Commons.Util.Log;

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
		public SensorPropertyRecord sensorPropertyRecord {
			get {
				return __record;
			}
			set {
				Unbind();
				__record = value;
				if (__record != null) {
					__record.sensorProperty.onSensorPropertyChanged += OnSensorPropertyChanged;
					Invalidate();
				}
			}
		} SensorPropertyRecord __record;

		private View association;
		private TextView button;

		public SensorPropertyViewHolder(SwipeRecyclerView recyclerView, int foregroundLayout) : base(recyclerView, foregroundLayout, Resource.Layout.list_item_button) {
			association = ItemView.FindViewById(Resource.Id.association);

			button = background as TextView;
			button.SetText(Resource.String.remove);
		}

		public override void Unbind() {
			base.Unbind();
			if (sensorPropertyRecord != null) {
				sensorPropertyRecord.sensorProperty.onSensorPropertyChanged -= OnSensorPropertyChanged;
			}
		}

		public virtual void Invalidate() {
			button.SetOnClickListener(new ViewClickAction((view) => {
				L.D(this, "Removing sensor property: " + sensorPropertyRecord.sensorProperty);
				sensorPropertyRecord.manifold.RemoveSensorProperty(sensorPropertyRecord.sensorProperty);
			}));

			if (association != null) {
				if (sensorPropertyRecord.manifold.IndexOfSensorProperty(sensorPropertyRecord.sensorProperty) >= sensorPropertyRecord.manifold.sensorPropertyCount - 1) {
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

	public class SensorPropertyViewHolder<T> : SensorPropertyViewHolder where T : ISensorProperty {
		public SensorPropertyRecord<T> record { get { return (SensorPropertyRecord<T>)sensorPropertyRecord; } } 

		public SensorPropertyViewHolder(SwipeRecyclerView recyclerView, int foregroundLayout) : base(recyclerView, foregroundLayout) {
		}
	}
}
