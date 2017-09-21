namespace ION.Core.Sensors
{

	using System;
	using System.Collections.Generic;

	using Newtonsoft.Json;

	using Appion.Commons.Measure;
	using Appion.Commons.Util;

	using ION.Core.App;
	using ION.Core.Fluids;
	using ION.Core.Sensors.Properties;

	/// <summary>
	/// A simple entity that is used to indicate events spawned by sensor actions.
	/// </summary>
	public class SensorEvent
	{
		/// <summary>
		/// The type describing the sensor event.
		/// </summary>
		public EType type { get; private set; }
		/// <summary>
		/// The sensor that caused the event.
		/// </summary>
		public Sensor sensor { get; private set; }
		/// <summary>
		/// The index of an affected sensor property. This will be -1 if the event does not describe a sensor property
		/// change.
		/// </summary>
		public int index { get; internal set; }
		/// <summary>
		/// The index that is used for the dest swap location for swapped sensor properties.
		/// </sumary>
		public int otherIndex { get; internal set; }

		public SensorEvent(EType type, Sensor sensor)
		{
			this.type = type;
			this.sensor = sensor;
		}

		public SensorEvent(EType type, Sensor sensor, int index) : this(type, sensor)
		{
			this.index = index;
		}

		public SensorEvent(EType type, Sensor sensor, int index, int otherIndex) : this(type, sensor, index)
		{
			this.otherIndex = otherIndex;
		}

		/// <summary>
		/// The enumeration of the events that a sensor can produce.
		/// </summary>
		public enum EType
		{
			/// <summary>
			/// Used when the sensor's measurements, unit or name change.
			/// </summary>
			Invalidated,

			/// <summary>
			/// Used when a sensor is linked to this sensor.
			/// </summary>
			LinkedSensorAdded,
			/// <summary>
			/// Used when the sensor's linked sensor is removed.
			/// </summary>
			LinkedSensorRemoved,

			/// <summary>
			/// The sensor's fluid state was changed.
			/// </summary>
			FluidStateChanged,

			/// <summary>
			/// Used when a sensor property is added to the sensor.
			/// </summary>
			SensorPropertyAdded,
			/// <summary>
			/// Used when a sensor property is removed from the sensor.
			/// </summary>
			SensorPropertyRemoved,
			/// <summary>
			/// Used when the sensor property list is cleared.
			/// </summary>
			SensorPropertyCleared,
			/// <summary>
			/// Used when two sensor properties are swapped within the sensor.
			/// </summary>
			SensorPropertySwapped,
		}
	}

	/// <summary>
	/// Enumerates the possible sensors.
	/// </summary>
	public enum ESensorType
	{
		Length,
		Humidity,
		Pressure,
		Temperature,
		Vacuum,
		Weight,
		Unknown,
	}

	/// <summary>
	/// A Sensor is an entity that reports on physical measurements.
	/// </summary>
	public abstract class Sensor
	{
		/// <summary>
		/// The delegate that will be notified when the sensor's state is changed.
		/// </summary>
		public delegate void OnSensorEvent(SensorEvent sensorEvent);

		/// <summary>
		/// The event that is used to spawn alerts that the sensor state changed.
		/// Note: events can be called from any thread an propogations through this event may not be on the main thread.
		/// </summary>
		public event OnSensorEvent onSensorEvent;

		/// <summary>
		/// The type of sensor this is. From this, the base conversion unit is
		/// derived as well as sensor uses.
		/// </summary>
		//public ESensorType type { get; private set; }
		public ESensorType type { get;  set; }
		/// <summary>
		/// Whether or not the sensor's reading is relative.
		/// </summary>
		public bool isRelative { get; private set; }
		/// <summary>
		/// Whether or not te sensor's reading is editable.
		/// </summary>
		/// <value><c>true</c> if is editable; otherwise, <c>false</c>.</value>
		public virtual bool isEditable
		{
			get
			{
				return false;
			}
		}

		/// <summary>
		/// The custom name for the specific sensor.
		/// </summary>
		public virtual string name
		{
			get
			{
				return _name;
			}
			set
			{
				_name = value;
				NotifyOfEvent(SensorEvent.EType.Invalidated);
			}
		}
		string _name;
		/// <summary>
		/// The unit that the sensor's measurement is quantitated in.
		/// </summary>
		public Unit unit
		{
			get
			{
				return measurement.unit;
			}
			set
			{
				measurement = measurement.ConvertTo(value);
			}
		}
		/// <summary>
		/// The measurement of the sensor.
		/// </summary>
		public Scalar measurement
		{
			get
			{
				return _measurement;
			}
			protected set
			{
				if (!value.unit.IsCompatible(unit))
				{
					throw new Exception("Attempted to set measurement, but the sensor unit " + unit + " is incompatible with " + value.unit + ".");
				}
				_measurement = value;
				NotifyOfEvent(SensorEvent.EType.Invalidated);
			}
		}
		Scalar _measurement;
		/// <summary>
		/// The maxumimum measurement that the sensor can accurately measure.
		/// </summary>
		public Scalar maxMeasurement { get; set; }
		/// <summary>
		/// The minumum measurement that the sensor can acurrately measure.
		/// </summary>
		public Scalar minMeasurement { get; set; }
		/// <summary>
		/// The measurement range for the sensor.
		/// </summary>
		/// <value>The range.</value>
		public ScalarSpan range
		{
			get
			{
				return maxMeasurement - minMeasurement;
			}
		}
		/// <summary>
		/// Whether or not the sensor is overloaded. The reading cannot be regarded as reliable if the measurement is overloaded.
		/// </summary>
		public bool isOverloaded
		{
			get
			{
				return maxMeasurement != null && measurement >= maxMeasurement ||
				  minMeasurement != null && measurement <= minMeasurement;
			}
		}
		/// <summary>
		/// The array of units that the sensor will support for unit changes.
		/// </summary>
		/// <value>The supported units.</value>
		public virtual Unit[] supportedUnits
		{
			get
			{
				return __supportedUnits;
			}
			protected set
			{
				foreach (var u in value)
				{
					if (!unit.IsCompatible(u))
					{
						throw new Exception("Cannot set units: quantity mismatch. Expected: " + unit.quantity + " received: " + u.quantity);
					}
				}

				__supportedUnits = value;
			}
		}
		Unit[] __supportedUnits;
    public double targetSHSC;
		/// <summary>
		/// Gets or sets the index of a sensor's location in the analyzer.
		/// </summary>
		/// <value>The analyzer slot.</value>
		[Obsolete("This needs to be deleted.")]
		public int analyzerSlot { get; set; }
		[Obsolete("This needs to be deleted.")]
		public int analyzerArea { get; set; }

		/// <summary>
		/// Gets the sensor that is linked to the this sensor.
		/// </summary>
		/// <value>The linked sensor.</value>
		public Sensor linkedSensor
		{
			get
			{
				return _linkedSensor;
			}
			private set
			{
				if (_linkedSensor != null)
				{
					NotifyOfEvent(SensorEvent.EType.LinkedSensorRemoved);
				}

				_linkedSensor = value;

				if (_linkedSensor != null)
				{
					NotifyOfEvent(SensorEvent.EType.LinkedSensorAdded);
				}
			}
		}
		Sensor _linkedSensor;

		/// <summary>
		/// The sensor properties that are within the manifold.
		/// </summary>
		/// <value>The sensor properties.</value>
		public readonly List<ISensorProperty> sensorProperties = new List<ISensorProperty>();

		/// <summary>
		/// The number of sensor properties are held in the manifold.
		/// </summary>
		/// <value>The sensor property count.</value>
		public int sensorPropertyCount
		{
			get
			{
				return sensorProperties.Count;
			}
		}

		/// <summary>
		/// An indexer that will retrieve the sensor properties from the manifold.
		/// </summary>
		/// <param name="index">Index.</param>
		public ISensorProperty this[int index]
		{
			get
			{
				return sensorProperties[index];
			}
		}

		/// <summary>
		/// The fluid state (system side) that the sensor is considered to be placed.
		/// </summary>
		/// <value>The state of the fluid.</value>
		public Fluid.EState fluidState
		{
			get
			{
				return _fluidState;
			}
			set
			{
				_fluidState = value;
				NotifyOfEvent(SensorEvent.EType.FluidStateChanged);
			}
		}
		Fluid.EState _fluidState;

		/// <summary>
		/// Creates a new sensor.
		/// </summary>
		/// <param name="sensorType">Sensor type.</param>
		/// <param name="isRelative">If set to <c>true</c> is relative.</param>
		protected Sensor(ESensorType sensorType, bool isRelative = true) :
		  this(sensorType, AppState.context.preferences.units.DefaultUnitFor(sensorType).OfScalar(0.0), isRelative)
		{
		}
		/// <summary>
		/// Creates a new sensor.
		/// </summary>
		/// <param name="sensorType">Sensor type.</param>
		/// <param name="initialMeasurement">Initial measurement.</param>
		/// <param name="isRelative">If set to <c>true</c> is relative.</param>
		protected Sensor(ESensorType sensorType, Scalar initialMeasurement, bool isRelative = true)
		{
			type = sensorType;
			_measurement = initialMeasurement;
			this.isRelative = isRelative;

			supportedUnits = new List<Unit>(SensorUtils.GetSensorTypeUnitMapping()[type]).ToArray();
		}

    protected Sensor(){
      
    }

		/// <summary>
		/// Attempst to set the secondary sensor for the manifold.
		/// </summary>
		/// <returns><c>true</c>, if secondary sensor was set, <c>false</c> otherwise.</returns>
		/// <param name="sensor">Sensor.</param>
		public bool SetLinkedSensor(Sensor sensor) {
			if (linkedSensor == sensor)	{
				// Safety check against infinite loops
				return true;
			}	else if (sensor == null){
				linkedSensor = null;
				return true;
			}	else if (!WillAcceptSecondarySensor(sensor))	{
				return false;
			}	else	{
				linkedSensor = sensor;
				sensor.linkedSensor = this;
				return true;
			}
		}

		/// <summary>
		/// Queries whether or not the manifold will accept the given sensor as a secondary sensor.
		/// </summary>
		/// <returns><c>true</c>, if accept secondary sensor was willed, <c>false</c> otherwise.</returns>
		/// <param name="sensor">Sensor.</param>
		public bool WillAcceptSecondarySensor(Sensor sensor) {
			if (sensor != null)	{
				return ALLOWED_SECONDARY_SENSORS.Matches(new ESensorType[] { type, sensor.type });
			}	else {
				return true;
			}
		}

		/// <summary>
		/// Adds the sensor property to the manifold if the manifold does not already have a
		/// sensor property of the given type.
		/// </summary>
		/// <param name="sensorProperty">Sensor property.</param>
		/// <returns>True if the property was added, false if the manifold already has the property.</returns>
		public bool AddSensorProperty(ISensorProperty sensorProperty) {
			if (HasSensorPropertyOfType(sensorProperty.GetType())) {
				return false;
			}	else {
				if (sensorProperty is SecondarySensorProperty) {
					sensorProperties.Insert(0, sensorProperty);
					NotifyOfEvent(new SensorEvent(SensorEvent.EType.LinkedSensorAdded, this, 0));
					return true;
				}	else {
					sensorProperties.Add(sensorProperty);
					NotifyOfEvent(SensorEvent.EType.LinkedSensorAdded);
					return true;
				}
			}
		}

		/// <summary>
		/// Inserts the sensor property into the given index within the manifold.
		/// </summary>
		/// <returns><c>true</c>, if sensor property was inserted, <c>false</c> otherwise.</returns>
		/// <param name="">.</param>
		public bool InsertSensorProperty(ISensorProperty sensorProperty, int index)	{
			if (HasSensorPropertyOfType(sensorProperty.GetType())) {
				return false;
			}	else {
				sensorProperties.Insert(index, sensorProperty);
				NotifyOfEvent(new SensorEvent(SensorEvent.EType.LinkedSensorAdded, this, index));
				return true;
			}
		}

		/// <summary>
		/// Queries the index of the given sensor property.
		/// </summary>
		/// <returns>The of sensor property.</returns>
		/// <param name="sensorProperty">Sensor property.</param>
		public int IndexOfSensorProperty(ISensorProperty sensorProperty)
		{
			return sensorProperties.IndexOf(sensorProperty);
		}

		/// <summary>
		/// Swaps the sensor properties at the given indices.
		/// </summary>
		/// <param name="first">First.</param>
		/// <param name="second">Second.</param>
		public void SwapSensorProperties(int first, int second)
		{
			if (first == second)
			{
				return;
			}

			var tmp = sensorProperties[first];
			sensorProperties[first] = sensorProperties[second];
			sensorProperties[second] = tmp;
			NotifyOfEvent(new SensorEvent(SensorEvent.EType.SensorPropertySwapped, this, first, second));
		}

		/// <summary>
		/// Removes the given sensor property from the manifold.
		/// </summary>
		/// <param name="sensorProperty">Sensor property.</param>
		public void RemoveSensorProperty(ISensorProperty sensorProperty)
		{
			RemoveSensorPropertyAt(sensorProperties.IndexOf(sensorProperty));
		}

		/// <summary>
		/// Removes the sensor property at the given index.
		/// </summary>
		/// <param name="index">Index.</param>
		public void RemoveSensorPropertyAt(int index)
		{
			sensorProperties.RemoveAt(index);
			NotifyOfEvent(new SensorEvent(SensorEvent.EType.SensorPropertyRemoved, this, index));
		}

		/// <summary>
		/// Queries whether or not the manifold has a sensor property of the given type.
		/// </summary>
		/// <returns><c>true</c> if this instance has sensor property of type; otherwise, <c>false</c>.</returns>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public bool HasSensorPropertyOfType(Type type)
		{
			foreach (var prop in sensorProperties)
			{
				if (prop.GetType().Equals(type))
				{
					return true;
				}
			}

			return false;
		}

		/// <summary>
		/// Removes all of the sensor properties that are attached to this manifold.
		/// </summary>
		public void ClearSensorProperties()
		{
			foreach (var sp in sensorProperties)
			{
				RemoveSensorProperty(sp);
			}
		}

		/// <summary>
		/// Returns the sensor property of given type.
		/// </summary>
		/// <returns>The sensor property of type.</returns>
		/// <param name="type">Type.</param>
		public ISensorProperty GetSensorPropertyOfType(Type type)
		{
			foreach (var prop in sensorProperties)
			{
				if (prop.GetType().Equals(type))
				{
					return prop;
				}
			}

			return null;
		}

		/// <summary>
		/// Returns the manifold's sensor property of the given type if it is present.
		/// </summary>
		/// <returns>The sensor property of type.</returns>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public T GetSensorPropertyOfType<T>() where T : ISensorProperty
		{
			foreach (var prop in sensorProperties)
			{
				if (prop.GetType().Equals(typeof(T)))
				{
					return (T)prop;
				}
			}

			return default(T);
		}

		public override string ToString()
		{
			return string.Format("[" + this.GetType().Name + ": type={0}, name={1}, unit={2}, measurement={3}]", type, name, unit, measurement);
		}

		/// <summary>
		/// Formats this sensor's measurement into a user friendly string.
		/// </summary>
		/// <returns>The formatted string.</returns>
		/// <param name="includeUnit">If set to <c>true</c> include unit.</param>
		public string ToFormattedString(bool includeUnit)
		{
			if (isOverloaded ||
				!(this is ManualSensor) && !(unit == Units.Vacuum.IN_HG || unit == Units.Pressure.IN_HG) &&
				(measurement.ConvertTo(maxMeasurement.unit).amount > maxMeasurement.amount ||
				 measurement.ConvertTo(minMeasurement.unit).amount < minMeasurement.amount))
			{
				return "OL";
			}
			else
			{
				return SensorUtils.ToFormattedString(measurement, includeUnit);
			}
		}

		/// <summary>
		/// Propogates a new sensor event to all listeners with EType.Invalidate.
		/// </summary>
		public void NotifyInvalidated()
		{
		}

		/// <summary>
		/// Progogates a new sensor event to all listeners.
		/// </summary>
		protected void NotifyOfEvent(SensorEvent sensorEvent)
		{
			if (onSensorEvent != null)
			{
				onSensorEvent(sensorEvent);
			}
		}

		/// <summary>
		/// Progogates a new sensor event to all listeners.
		/// </summary>
		protected void NotifyOfEvent(SensorEvent.EType type)
		{
			if (onSensorEvent != null)
			{
				onSensorEvent(new SensorEvent(type, this));
			}
		}

		private static readonly IFilter<ESensorType[]> ALLOWED_SECONDARY_SENSORS = new OrFilterCollection<ESensorType[]>(
		  new ExactSensorTypeFilter(ESensorType.Pressure),
		  new ExactSensorTypeFilter(ESensorType.Pressure, ESensorType.Temperature),
		  new ExactSensorTypeFilter(ESensorType.Temperature, ESensorType.Pressure),
		  new ExactSensorTypeFilter(ESensorType.Temperature),
		  new ExactSensorTypeFilter(ESensorType.Vacuum)
		);

	}
}