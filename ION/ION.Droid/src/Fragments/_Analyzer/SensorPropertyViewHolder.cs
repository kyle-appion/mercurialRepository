namespace ION.Droid.Fragments._Analyzer {

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

		protected SensorPropertyRecord(Manifold manifold, ISensorProperty sensorProperty, SubviewAdapter.EViewType viewType) {
			this.manifold = manifold;
			this.sensorProperty = sensorProperty;
			this.viewType = (int)viewType;
		}
	}

	public abstract class SensorPropertyRecord<T> : SensorPropertyRecord where T : ISensorProperty {
		public T sp { get { return (T)sensorProperty; } }

		protected SensorPropertyRecord(Manifold manifold, T t, SubviewAdapter.EViewType viewType) : base(manifold, t, viewType) {
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
					__record.sensorProperty.onSensorPropertyChanged += OnSensorPropertyChanged;
					Invalidate();
				}
			}
		} SensorPropertyRecord __record;

		public SensorPropertyViewHolder(SwipeRecyclerView recyclerView, int foregroundLayout) : base(recyclerView, foregroundLayout, Resource.Layout.view_delete) {
			var button = background as Button;
			button.SetText(Resource.String.remove);
			button.SetOnClickListener(new ViewClickAction((view) => {
				L.D(this, "Removing sensor property: " + sensorPropertyRecord.sensorProperty);
				sensorPropertyRecord.manifold.RemoveSensorProperty(sensorPropertyRecord.sensorProperty);
			}));
		}

		public virtual void Bind() {
		}

		public override void Unbind() {
			base.Unbind();
			if (sensorPropertyRecord != null) {
				sensorPropertyRecord.sensorProperty.onSensorPropertyChanged -= OnSensorPropertyChanged;
			}
		}

		public virtual void Invalidate() {
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
