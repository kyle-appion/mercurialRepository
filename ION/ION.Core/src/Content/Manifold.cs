namespace ION.Core.Content {

  using System;
  using System.Collections.Generic;

  using Appion.Commons.Measure;
  using Appion.Commons.Util;

  using ION.Core.App;
  using ION.Core.Devices;
  using ION.Core.Fluids;
  using ION.Core.Sensors;
  using ION.Core.Sensors.Properties;

  /// <summary>
  /// A class that represents a change event within a manifold. This class will reflect the
  /// type of change that occurred within the manifold.
  /// </summary>
  /// <example>
  /// {
  ///   ...
  ///
  ///   var mySensor = new Sensor(ESensorType.Pressure);
  ///   var m = new Manifold(mySensor);
  ///   m.onManifoldEvent += OnManifoldEvent;
  ///
  ///   ...
  /// }
  ///
  /// /// <summary>
  /// /// Called when the manifold changes.
  /// /// </summary>
  /// private void OnManifoldEvent(ManifoldEvent event, Manifold manifold) {
  /// }
  ///
  /// </example>
  public class ManifoldEvent {
    /// <summary>
    /// The type of the event.
    /// </summary>
    /// <value>The type.</value>
    public EType type { get; internal set; }
    /// <summary>
    /// The manifold that changed.
    /// </summary>
    /// <value>The manifold.</value>
    public Manifold manifold { get; internal set; }
    /// <summary>
    /// The index that a sensor property was removed from or added to. Index will be -1 when
    /// not used.
    /// </summary>
    /// <value>The index.</value>
    public int index { get; internal set; }
    /// <summary>
    /// The index that is used only for swap events. OtherIndex will be -1 when not used.
    /// </summary>
    /// <value>The index of the other.</value>
    public int otherIndex { get; internal set; }

    /// <summary>
    /// Creates a new manifold event.
    /// </summary>
    /// <param name="type">The type of event this is.</param>
    /// <param name="manifold">Manifold.</param>
    /// <param name="index">Index.</param>
    /// <param name="otherIndex">Other index.</param>
    public ManifoldEvent(EType type, Manifold manifold, int index, int otherIndex) {
      this.type = type;
      this.manifold = manifold;      
      this.index = index;
      this.otherIndex = otherIndex;
    }

    /// <summary>
    /// Enumerates the type of manifold events that are thrown.
    /// </summary>
    public enum EType {
      /// <summary>
      /// Used when either the primary or secondary sensor change, as well as the when the
      /// manifold's pt chart changes or when the secondary sensor changes.
      /// </summary>
      Invalidated,

      /// <summary>
      /// Used when a secondary sensor is added to the manifold. Note: is a secondary sensor
      /// already existed, the a SecondarySensorRemoved event will be thrown before this.
      /// </summary>
      SecondarySensorAdded,
      /// <summary>
      /// Used when the manifold's secondary sensor is removed.
      /// </summary>
      SecondarySensorRemoved,

      /// <summary>
      /// Used when a sensor property is added to the manifold.
      /// </summary>
      SensorPropertyAdded,
      /// <summary>
      /// Used when a sensor property is removed from the manifold.
      /// </summary>
      SensorPropertyRemoved,
      /// <summary>
      /// Used when the sensor property list is cleared.
      /// </summary>
      SensorPropertyCleared,
      /// <summary>
      /// Used when two sensor properties are swapped within the manifold.
      /// </summary>
      SensorPropertySwapped,
    }
  }

  /// <summary>
  /// A Manifold is an abstract representation of a collection of sensors that
  /// come together to create and "explain" a certain context. For example: a
  /// manifold consiting of a pressure and a temperature sensor create a context
  /// that allows for a Pressure/Temperature lookup chart and a Superheat/subcool
  /// reference measurement.
  /// </summary>
  /// <description>
  /// A Manifold should only and always be used when displaying a sensor that can
  /// interact with other sensors whose content needs to be retained, ie. Viewers.
  /// </description>
  public class Manifold : IDisposable {

    private static readonly IFilter<ESensorType[]> ALLOWED_SECONDARY_SENSORS = new OrFilterCollection<ESensorType[]>(
        new ExactSensorTypeFilter(ESensorType.Pressure),
        new ExactSensorTypeFilter(ESensorType.Pressure, ESensorType.Temperature),
        new ExactSensorTypeFilter(ESensorType.Temperature, ESensorType.Pressure),
        new ExactSensorTypeFilter(ESensorType.Temperature),
        new ExactSensorTypeFilter(ESensorType.Vacuum)
      );

    private const int VERSION = 1;

    /// <summary>
    /// The delegate that is call when a manifold'a context is changed. This
    /// can be as simple as a reading update or an entirely new context has
    /// been added to the manifold.
    /// </summary>
    /// <param name="manifold"></param>
    /// <param name="context"></param>
    [Obsolete("OnManifoldChanged is deprecated. Use OnManifoldEvent instead as this will be deleted in the future.")]
    public delegate void OnManifoldChanged(Manifold manifold);
    /// <summary>
    /// The delegate that is called when the manifold throws a new event.
    /// </summary>
    public delegate void OnManifoldEvent(ManifoldEvent manifoldEvent);

    /// <summary>
    /// The event handler that is notified when the manifold changes.
    /// </summary>
    [Obsolete("OnManifoldChanged is deprecated. Use OnManifoldEvent instead as this will be deleted in the future.")]
    public event OnManifoldChanged onManifoldChanged;
    /// <summary>
    /// The event handler that is notified when the manifold throws a new event.
    /// </summary>
    public event OnManifoldEvent onManifoldEvent;

    /// <summary>
    /// The primary sensor for the manifold.
    /// </summary>
    public Sensor primarySensor { get; private set; }

    /// <summary>
    /// The secondary sensor for the manifold.
    /// </summary>
    public Sensor secondarySensor {
      get {
        return __secondarySensor;
      }

      private set {
        if (__secondarySensor != null) {
					var s = __secondarySensor as GaugeDeviceSensor;
					if (s != null) {
						s.device.onDeviceEvent -= OnDeviceEvent;
					}
          __secondarySensor.onSensorStateChangedEvent -= OnManifoldSensorChanged;
          NotifyOfEvent(ManifoldEvent.EType.SecondarySensorRemoved);
        }

        __secondarySensor = value;
        if (__secondarySensor != null && __secondarySensor is GaugeDeviceSensor) {
          var s = __secondarySensor as GaugeDeviceSensor;
          if (s != null) {
            s.device.onDeviceEvent += OnDeviceEvent;
          }

          __secondarySensor.onSensorStateChangedEvent += OnManifoldSensorChanged;
          OnManifoldSensorChanged(__secondarySensor);
				  NotifyOfEvent(ManifoldEvent.EType.SecondarySensorAdded);
				} 

        //NotifyOfEvent(ManifoldEvent.EType.Invalidated);
      }
    } Sensor __secondarySensor;

    /// <summary>
    /// The target superheat/subcool for a sensor
    /// </summary>
    public double targetSHSC { get; set; }

    /// <summary>
    /// The sensor properties that are within the manifold.
    /// </summary>
    /// <value>The sensor properties.</value>
    public readonly List<ISensorProperty> sensorProperties = new List<ISensorProperty>();

    /// <summary>
    /// The number of sensor properties are held in the manifold.
    /// </summary>
    /// <value>The sensor property count.</value>
    public int sensorPropertyCount {
      get {
        return sensorProperties.Count;
      }
    }

    /// <summary>
    /// An indexer that will retrieve the sensor properties from the manifold.
    /// </summary>
    /// <param name="index">Index.</param>
    public ISensorProperty this[int index] {
      get {
        return sensorProperties[index];
      }
    }

    /// <summary>
    /// The pt chart that the manifold is expected to work with.
    /// </summary>
    public PTChart ptChart {
      get {
        if (__ptChart == null) {
          __ptChart = new PTChart(AppState.context.fluidManager.lastUsedFluid, Fluid.EState.Dew);
        }
        return __ptChart;
      }
      set {
        __ptChart = value;
        NotifyOfEvent(ManifoldEvent.EType.Invalidated);
      }
    } PTChart __ptChart;


    /// <summary>
    /// Creates a new manifold with the given sensor as the primary sensor.
    /// </summary>
    /// <param name="primarySensor">Primary sensor.</param>
    public Manifold(Sensor primarySensor) {
      this.primarySensor = primarySensor;
      //this.primarySensor.onSensorStateChangedEvent += OnManifoldSensorChanged;
      
			var s = primarySensor as GaugeDeviceSensor;
			if (s != null) {
				s.device.onDeviceEvent += OnDeviceEvent;
        s.onSensorStateChangedEvent += OnManifoldSensorChanged;
			}
    }

    /// <summary>
    /// Creates a new manifold with the given sensors as the primary and sencondary sensors.
    /// </summary>
    /// <param name="primarySensor">Primary sensor.</param>
    /// <param name="secondarySensor">Secondary sensor.</param>
    public Manifold(Sensor primarySensor, Sensor secondarySensor) : this(primarySensor) {
      this.secondarySensor = secondarySensor;
    }

    /// <summary>
    /// Returns a <see cref="System.String"/> that represents the current <see cref="ION.Core.Content.Manifold"/>.
    /// </summary>
    /// <returns>A <see cref="System.String"/> that represents the current <see cref="ION.Core.Content.Manifold"/>.</returns>
    public override string ToString() {
      return string.Format("[Manifold: primarySensor={0}, secondarySensor={1}, sensorPropertyCount={2}]", primarySensor, secondarySensor, sensorProperties);
    }

    // Overridden from IDispose
    public void Dispose() {
      primarySensor.onSensorStateChangedEvent -= OnManifoldSensorChanged;
      if (__secondarySensor != null) {
        __secondarySensor.onSensorStateChangedEvent -= OnManifoldSensorChanged;
      }

			var s = primarySensor as GaugeDeviceSensor;
			if (s != null) {
				s.device.onDeviceEvent -= OnDeviceEvent;
			}
    }

    /// <summary>
    /// Queries whether or not the manifold's primary or secondary sensor is the given sensor.
    /// </summary>
    /// <returns><c>true</c>, if sensor was containsed, <c>false</c> otherwise.</returns>
    /// <param name="sensor">Sensor.</param>
    public bool ContainsSensor(Sensor sensor) {
			return (primarySensor != null && primarySensor.Equals(sensor)) || (secondarySensor != null && secondarySensor.Equals(sensor));
    }

    /// <summary>
    /// Attempst to set the secondary sensor for the manifold.
    /// </summary>
    /// <returns><c>true</c>, if secondary sensor was set, <c>false</c> otherwise.</returns>
    /// <param name="sensor">Sensor.</param>
    public bool SetSecondarySensor(Sensor sensor) {
    	if(sensor == null){
				secondarySensor = null;
				return true;
			}
      if (!WillAcceptSecondarySensor(sensor)) {
        return false;
      }

      secondarySensor = sensor;

      return true;
    }

    /// <summary>
    /// Queries whether or not the manifold will accept the given sensor as a secondary sensor.
    /// </summary>
    /// <returns><c>true</c>, if accept secondary sensor was willed, <c>false</c> otherwise.</returns>
    /// <param name="sensor">Sensor.</param>
    public bool WillAcceptSecondarySensor(Sensor sensor) {
      if (sensor != null) {
        return ALLOWED_SECONDARY_SENSORS.Matches(new ESensorType[] { primarySensor.type, sensor.type });
      } else {
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
      } else {
				if (sensorProperty is SecondarySensorProperty) {
					sensorProperties.Insert(0, sensorProperty);
					NotifyOfEvent(ManifoldEvent.EType.SensorPropertyAdded, 0);

					return true;
				} else {
	        sensorProperties.Add(sensorProperty);
					NotifyOfEvent(ManifoldEvent.EType.SensorPropertyAdded, sensorProperties.Count - 1);
					
					return true;
				}
      }
    }

		/// <summary>
		/// Inserts the sensor property into the given index within the manifold.
		/// </summary>
		/// <returns><c>true</c>, if sensor property was inserted, <c>false</c> otherwise.</returns>
		/// <param name="">.</param>
		public bool InsertSensorProperty(ISensorProperty sensorProperty, int index) {
			if (HasSensorPropertyOfType(sensorProperty.GetType())) {
				return false;
			} else {
				sensorProperties.Insert(index, sensorProperty);
				NotifyOfEvent(ManifoldEvent.EType.SensorPropertyAdded, index);
				return true;
			}
		}

    /// <summary>
    /// Queries the index of the given sensor property.
    /// </summary>
    /// <returns>The of sensor property.</returns>
    /// <param name="sensorProperty">Sensor property.</param>
    public int IndexOfSensorProperty(ISensorProperty sensorProperty) {
      return sensorProperties.IndexOf(sensorProperty);
    }

    /// <summary>
    /// Swaps the sensor properties at the given indices.
    /// </summary>
    /// <param name="first">First.</param>
    /// <param name="second">Second.</param>
    public void SwapSensorProperties(int first, int second) {
      if (first == second) {
        return;
      }

      var tmp = sensorProperties[first];
      sensorProperties[first] = sensorProperties[second];
      sensorProperties[second] = tmp;
      NotifyOfEvent(ManifoldEvent.EType.SensorPropertySwapped, first, second);
    }

    /// <summary>
    /// Removes the given sensor property from the manifold.
    /// </summary>
    /// <param name="sensorProperty">Sensor property.</param>
    public void RemoveSensorProperty(ISensorProperty sensorProperty) {
      RemoveSensorPropertyAt(sensorProperties.IndexOf(sensorProperty));
    }

    /// <summary>
    /// Removes the sensor property at the given index.
    /// </summary>
    /// <param name="index">Index.</param>
    public void RemoveSensorPropertyAt(int index) {
      sensorProperties.RemoveAt(index);
      NotifyOfEvent(ManifoldEvent.EType.SensorPropertyRemoved, index);
    }

    /// <summary>
    /// Queries whether or not the manifold has a sensor property of the given type.
    /// </summary>
    /// <returns><c>true</c> if this instance has sensor property of type; otherwise, <c>false</c>.</returns>
    /// <typeparam name="T">The 1st type parameter.</typeparam>
    public bool HasSensorPropertyOfType(Type type) {
      foreach (var prop in sensorProperties) {
        if (prop.GetType().Equals(type)) {
          return true;
        }
      }

      return false;
    }

		/// <summary>
		/// Removes all of the sensor properties that are attached to this manifold.
		/// </summary>
		public void ClearSensorProperties() {
			sensorProperties.Clear();
			NotifyOfEvent(ManifoldEvent.EType.SensorPropertyCleared);
		}

		/// <summary>
		/// Returns the sensor property of given type.
		/// </summary>
		/// <returns>The sensor property of type.</returns>
		/// <param name="type">Type.</param>
		public ISensorProperty GetSensorPropertyOfType(Type type) {
			foreach (var prop in sensorProperties) {
				if (prop.GetType().Equals(type)) {
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
		public T GetSensorPropertyOfType<T>() where T : ISensorProperty {
			foreach (var prop in sensorProperties) {
				if (prop.GetType().Equals(typeof(T))) {
					return (T)prop;
				}
			}

			return default(T);
		}

		/// <summary>
		/// Called when a device throws a device event.
		/// </summary>
		/// <returns>The device event.</returns>
		/// <param name="de">De.</param>
		private void OnDeviceEvent(DeviceEvent de) {
      if (de.type == DeviceEvent.EType.NameChanged || de.type == DeviceEvent.EType.ConnectionChange) {
        NotifyOfEvent(ManifoldEvent.EType.Invalidated);
      } else if (de.type == DeviceEvent.EType.Deleted) {
        Log.E(this, "Not responding to device being deleted from workbench");
      }
		}

    /// <summary>
    /// Called when a manifold's sensor's reading changes.
    /// </summary>
    /// <param name="sensor"></param>
    /// <param name="reading"></param>
    private void OnManifoldSensorChanged(Sensor sensor) {
      NotifyOfEvent(ManifoldEvent.EType.Invalidated);
    }

    /// <summary>
    /// Notifies the onManifoldEvent handler of a new event.
    /// </summary>
    /// <param name="type">Type.</param>
    /// <param name="index">Index.</param>
    private void NotifyOfEvent(ManifoldEvent.EType type, int index = -1, int otherIndex = -1) {
      // TODO ahodder@appioninc.com: Delete this: deprecated.
      if (onManifoldChanged != null) {
        onManifoldChanged(this);
      }

      if (onManifoldEvent != null) {
        onManifoldEvent(new ManifoldEvent(type, this, index, otherIndex));
      }
    }
  }
}

