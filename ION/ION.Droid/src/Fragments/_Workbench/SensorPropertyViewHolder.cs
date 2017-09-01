namespace ION.Droid.Fragments._Workbench {

	using Android.Views;
	using Android.Widget;

	using ION.Core.Content;
	using ION.Core.Sensors.Properties;

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
					Bind();
					Invalidate();
				}
			}
		} SensorPropertyRecord __record;

		private View association;

		public SensorPropertyViewHolder(SwipeRecyclerView recyclerView, int foregroundLayout) : base(recyclerView, foregroundLayout, Resource.Layout.view_delete) {
			association = ItemView.FindViewById(Resource.Id.association);

			var button = background as Button;
			button.SetText(Resource.String.remove);
			button.SetOnClickListener(new ViewClickAction((view) => {
				sensorPropertyRecord.manifold.RemoveSensorProperty(sensorPropertyRecord.sensorProperty);
			}));
		}

		public virtual void Bind() {
			__record.sensorProperty.onSensorPropertyChanged += OnSensorPropertyChanged;
			sensorPropertyRecord.manifold.onManifoldEvent += OnManifoldEvent;
		}

		public override void Unbind() {
			base.Unbind();
			if (sensorPropertyRecord != null) {
				sensorPropertyRecord.sensorProperty.onSensorPropertyChanged -= OnSensorPropertyChanged;
				sensorPropertyRecord.manifold.onManifoldEvent -= OnManifoldEvent;
			}
		}

		public virtual void Invalidate() {
			InvalidateAssociation();
		}

		private void InvalidateAssociation() {
			if (association != null) {
				if (sensorPropertyRecord.manifold.IndexOfSensorProperty(sensorPropertyRecord.sensorProperty) >= sensorPropertyRecord.manifold.sensorPropertyCount - 1) {
					association.SetBackgroundResource(Resource.Drawable.ic_association);
				} else {
					association.SetBackgroundResource(Resource.Drawable.ic_association_linked);
				}
			}
		}

		protected virtual void OnSensorPropertyChanged(ISensorProperty sensorProperty) {
			Invalidate();
		}

		protected virtual void OnManifoldEvent(ManifoldEvent manifoldEvent) {
			switch (manifoldEvent.type) {
				case ManifoldEvent.EType.SensorPropertyAdded: {
					InvalidateAssociation();
					break;
				} // ManifoldEvent.EType.SensorPropertyAdded
				case ManifoldEvent.EType.SensorPropertyRemoved: {
					InvalidateAssociation();
					break;
				} // ManifoldEvent.EType.SensorPropertyAdded
				case ManifoldEvent.EType.SensorPropertySwapped: {
					InvalidateAssociation();
					break;
				} // ManifoldEvent.EType.SensorPropertySwapped
			}
		}
	}

	public class SensorPropertyViewHolder<T> : SensorPropertyViewHolder where T : ISensorProperty {
		public SensorPropertyRecord<T> record { get { return (SensorPropertyRecord<T>)sensorPropertyRecord; } } 

		public SensorPropertyViewHolder(SwipeRecyclerView recyclerView, int foregroundLayout) : base(recyclerView, foregroundLayout) {
		}
	}
}
