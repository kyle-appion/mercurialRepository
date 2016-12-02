﻿namespace ION.Core.Content {

	using System;
	using System.Collections.Generic;

	using ION.Core.App;
	using ION.Core.Fluids;
	using ION.Core.Sensors;
	using ION.Core.Util;

	/// <summary>
	/// A collection of data pertaining to a state change within the analyzer.
	/// </summary>
	public class AnalyzerEvent {
    /// <summary>
    /// The type of this event.
    /// </summary>
    /// <value>The type.</value>
    public EType type { get; private set; }
    /// <summary>
    /// The side of the analyzer that the event was thrown from.
    /// </summary>
    public Analyzer.ESide side { get; private set; }
    /// <summary>
    /// The index that is used for almost all sensor events. In the case of a swap event, this is the first sensor.
    /// </summary>
    /// <value>The index.</value>
    public int index { get; private set; }
    /// <summary>
    /// The second index used in swap events.
    /// </summary>
    /// <value>The index of the other.</value>
    public int otherIndex { get; private set; }
    /// <summary>
    /// The manifold event.
    /// </summary>
    /// <value>The manifold event.</value>
    public ManifoldEvent manifoldEvent { get; private set; }

    /// <summary>
    /// Creates a new analyzer event indicating that the event only affected a single sensor.
    /// </summary>
    /// <param name="type">Type.</param>
    /// <param name="index">Index.</param>
    public AnalyzerEvent(EType type, int index) {
      this.type = type;
      this.index = index;
    }

    /// <summary>
    /// Creates a new Swap analyzer event.
    /// </summary>
    /// <param name="first">First.</param>
    /// <param name="second">Second.</param>
    public AnalyzerEvent(int first, int second) {
      this.type = EType.Swapped;
      this.index = first;
      this.otherIndex = second;
    }

    /// <summary>
    /// Creates a new analyzer event indicating that the event affect one of the manifolds.
    /// </summary>
    /// <param name="type">Type.</param>
    /// <param name="side">Side.</param>
    public AnalyzerEvent(EType type, Analyzer.ESide side) {
      this.type = type;
      this.side = side;
    }

    /// <summary>
    /// Creates a new manifold event indicating that the analyzer received a new manifold event.
    /// </summary>
    /// <param name="manifoldEvent">Manifold event.</param>
    public AnalyzerEvent(ManifoldEvent manifoldEvent) {
			this.type = EType.ManifoldEvent;
      this.manifoldEvent = manifoldEvent;
    }

    /// <summary>
    /// The enumerations of the possible event notifications that the analyzer will throw.
    /// </summary>
    public enum EType {
      /// <summary>
      /// The type that is used to indicate that the analyzer swapped two sensors.
      /// </summary>
      Swapped,
      /// <summary>
      /// The type that is used to indicate that a sensor was added to the analyzer at index.
      /// </summary>
      Added,
      /// <summary>
      /// The type that is used to indicate that the analzer remove a sensor at index.
      /// </summary>
      Removed,
      /// <summary>
      /// The type that is used when a sensor event is thrown by a sensor within the analyzer. Provides the index of
      /// the triggering sensor.
      /// </summary>
      SensorChanged,
      /// <summary>
      /// Called when a manifold is added to the analyzer. This is paired with a side that the manifold was added to.
      /// </summary>
      ManifoldAdded,
      /// <summary>
      /// Called when a manifold is removed to the analyzer. This is paired with a side that the manifold was removed from.
      /// </summary>
      ManifoldRemoved,
      /// <summary>
      /// Called when a manifold event is thrown by a manifold within the analyzer. Provides the triggering manifold 
      /// event.
      /// </summary>
      ManifoldEvent,
    }
  }

  /// <summary>
  /// An analyzer is an abstract representation of an HVAC system. It allows technicians to place sensors at various
  /// location around this absraction. The analyzer is heavily used in various reports, as a correctly lain out
  /// analyzer will describe an entire systems state.
  /// </summary>
  /// <description>
  /// Sensors that are held within the analyzer are organized in an awkward fashion. There are techincally four sections
  /// to the analyzer. The high and low sides are most evident and first thought of. Additionally, there are the
  /// compressor and evaporator side. You can think of high low as right and left respectively, and compressor and
  /// evaporator as bottom and top respectively. With this in mind, the sensors are organized within the analyzer
  /// starting at the compressor side and extening into its respective side for half of the sensors per side value. The
  /// rest of the sensors are then placed into the evaporator side, again extending into their respective sides.
  /// </description>
  public class Analyzer {

    /// <summary>
    /// The default number of sensors per side of the analyzer.
    /// </summary>
    private const int DEFAULT_SENSORS_PER_SIDE = 4;

    /// <summary>
    /// The delegate that will be used to receiving analyzer events.
    /// </summary>
    public delegate void OnAnalyzerEvent(AnalyzerEvent analyzerEvent);

    /// <summary>
    /// The event handler that will throw new analyzer events.
    /// </summary>
    public OnAnalyzerEvent onAnalyzerEvent;

    /// <summary>
    /// Queries a sensor from the given index within the analyzer.
    /// </summary>
    /// <param name="index">Index.</param>
    public Sensor this[int index] {
      get {
        return sensors[index];
      }
    }

    /// <summary>
    /// Queries the maximum number of sensors that can be held in the analyzer.
    /// </summary>
    /// <value>The size of the analyzer.</value>
    public int analyzerSize {
      get {
        return sensors.Length;
      }
    }

		/// <summary>
		/// Queries the number of sensors that are in the analyzer.
		/// </summary>
		/// <value>The count.</value>
		public int count {
			get {
				return GetSensorsInSideCount(ESide.Low) + GetSensorsInSideCount(ESide.High);
			}
		}
    /// <summary>
    /// The number of sensors that the analyzer supports per side.
    /// </summary>
    /// <value>The sensors per side.</value>
    public int sensorsPerSide { get; private set; }
    
    /// <summary>
    /// Gets or sets the remote analyzer.
    /// </summary>
    /// <value>The remote analyzer.</value>
    public Analyzer storedAnalyzer {get; set;}

    /// <summary>
    /// The manifold that will represent the fore front low side sensors. Null indicates no manifold is present.
    /// </summary>
    public Manifold lowSideManifold {
      get {
        return __lowSideManifold;
      }
      private set {
        if (__lowSideManifold != null) {
          __lowSideManifold.onManifoldEvent -= OnManifoldEvent;
          NotifyOfAnalyzerEvent(new AnalyzerEvent(AnalyzerEvent.EType.ManifoldRemoved, ESide.Low));
        }

        __lowSideManifold = value;

        if (__lowSideManifold != null) {
          __lowSideManifold.onManifoldEvent += OnManifoldEvent;
          NotifyOfAnalyzerEvent(new AnalyzerEvent(AnalyzerEvent.EType.ManifoldAdded, ESide.Low));
        }
      }
    } Manifold __lowSideManifold;

    /// <summary>
    /// The manifold that will represent the forefront high side sensors.
    /// </summary>
    public Manifold highSideManifold {
      get {
        return __highSideManifold;
      }
      private set {
        if (__highSideManifold != null) {
          __highSideManifold.onManifoldEvent -= OnManifoldEvent;
        }

        __highSideManifold = value;

        if (__highSideManifold != null) {
          __highSideManifold.onManifoldEvent += OnManifoldEvent;
        }
      }
    } Manifold __highSideManifold;


    /// <summary>
    /// The ion context.
    /// </summary>
    /// <value>The point chart.</value>
    public IION ion { get; private set; }
    /// <summary>
    /// The array of sensors that comprise the analyzer. Null indicates an empty slot.
    /// </summary>
    private Sensor[] sensors;

    public List<Sensor> sensorList;
		public List<int> sensorPositions = new List<int>(){1,2,3,4,5,6,7,8};
		public List<int> revertPositions = new List<int>(){1,2,3,4,5,6,7,8};
		public List<string> lowSubviews = new List<string>();
		public List<string> highSubviews = new List<string>();
		public string lowAccessibility ="low";
		public string highAccessibility ="high";
    /// <summary>
    /// Creates a new analyzer.
    /// </summary>
    /// <param name="sensorsPerSide">The number of sensors per side of the analyzer. This value must be an increment 
    /// of 2</param>
    public Analyzer(IION ion, int sensorsPerSide=DEFAULT_SENSORS_PER_SIDE) {
      if (sensorsPerSide % 2 != 0) {
        throw new Exception("Cannot create new analyzer: sensors per side must be an increment of two. {is : " + sensorsPerSide + "}");
      }
      this.ion = ion;
      this.sensorsPerSide = sensorsPerSide;
      sensors = new Sensor[sensorsPerSide * 2];
      sensorList = new List<Sensor>();
    }

    /// <summary>
    /// Queries the list of sensor that are present in the analyzer.
    /// </summary>
    /// <returns>The sensors.</returns>
    public List<Sensor> GetSensors() {
			// TODO ahodder@appioninc: This is a hack that accomodates both android and ios for the split in how analyzer works
			// Because the compiler would not recognize the build commands __IOS__ and __ANDROID__, we had to do an even more
			// stupid work around: Check for content.
			var ret = new List<Sensor>();

			if (count > 0) {
				foreach (var sensor in sensors) {
					ret.Add(sensor);
				}

				Log.D(this, "The platform is android. Returning: " + ret.Count + " sensors");
			} else if (sensorList.Count > 0) {
				ret.AddRange(sensors);
				Log.D(this, "The platform is ios. Returning: " + ret.Count + " sensors");
			}

			return ret;
    }

    /// <summary>
    /// Queries whether or not the given sensor is present in the analyzer.
    /// </summary>
    /// <returns><c>true</c> if this instance has sensor the specified sensor; otherwise, <c>false</c>.</returns>
    /// <param name="sensor">Sensor.</param>
    public bool HasSensor(Sensor sensor) {
      return IndexOfSensor(sensor) >= 0;      
    }

    /// <summary>
    /// Queries whether or not the analyzer has a sensor at the given index.
    /// </summary>
    /// <returns><c>true</c> if this instance has sensor at the specified index; otherwise, <c>false</c>.</returns>
    /// <param name="index">Index.</param>
    public bool HasSensorAt(int index) {
      return sensors[index] != null;
    }

    /// <summary>
    /// Queries the index of the given sensor within the analyzer. If the sensor is not present, we will return -1.
    /// </summary>
    /// <returns>The of sensor.</returns>
    /// <param name="sensor">Sensor.</param>
    public int IndexOfSensor(Sensor sensor) {
      if (sensor == null) {
        return -1;
      }

      for (int i = 0; i < sensors.Length; i++) {
        if (sensor == sensors[i]) {//sensor.Equals(sensors[i])) {
          return i;
        }
      }

      // We don't need to check manifolds as they can only exist if their sensors are present in the sensor array.
      return -1;
    }

    /// <summary>
    /// Queries whether or not the given sensor is attached to the manifold.
    /// </summary>
    /// <returns><c>true</c> if this instance is sensor attached to manifold the specified sensor; otherwise, <c>false</c>.</returns>
    /// <param name="sensor">Sensor.</param>
    public bool IsSensorAttachedToManifold(Sensor sensor) {
      return IsSensorIndexAttachedToManifold(IndexOfSensor(sensor));
    }

    /// <summary>
    /// Queries whether the sensor at the given index is attached to a manifold.
    /// </summary>
    /// <returns><c>true</c> if this instance is sensor index attached to manifold the specified index; otherwise, <c>false</c>.</returns>
    /// <param name="index">Index.</param>
    public bool IsSensorIndexAttachedToManifold(int index) {
      if (index >= 0 && index < sensorsPerSide && lowSideManifold != null) {
        return lowSideManifold.ContainsSensor(sensors[index]);
      } else if (index >= sensorsPerSide && index < sensors.Length && highSideManifold != null) {
        return highSideManifold.ContainsSensor(sensors[index]);
      } else {
        return false;
      }
    }

    /// <summary>
    /// Queries the next empty sensor index of the given side of the analyzer. If the given side of the analyzer is
    /// full, then -1 will be returned.
    /// </summary>
    /// <returns>The empty sensor index.</returns>
    /// <param name="side">Side.</param>
    public int NextEmptySensorIndex(ESide side) {
      var end = (ESide.Low == side) ? 0 : sensorsPerSide;

      for (int i = 0; i < sensorsPerSide; i++) {
        if (sensors[i + end] == null) {
          return i + end;
        }
      }

      return -1;
    }

    /// <summary>
    /// Queries the number of sensors that are in the given side of the analyzer
    /// </summary>
    /// <returns>The sensors in side count.</returns>
    /// <param name="side">Side.</param>
    public int GetSensorsInSideCount(ESide side) {
      var i = (ESide.Low == side) ? 0 : sensorsPerSide;

      var ret = 0;
			for (int j = 0; j < sensorsPerSide; j++) {
        if (sensors[i++] != null) {
          ret++;
        }
      }

      return ret;
    }

		/// <summary>
		/// Queries whether or not a side of the analyzer is full.
		/// </summary>
		/// <returns><c>true</c>, if side full was ised, <c>false</c> otherwise.</returns>
		/// <param name="side">Side.</param>
		public bool IsSideFull(ESide side) {
			return NextEmptySensorIndex(side) < 0;
		}

    /// <summary>
    /// Queries whether or not a sensor can be added to the given side of the analyzer.
    /// </summary>
    /// <returns><c>true</c> if this instance can add sensor to side the specified side; otherwise, <c>false</c>.</returns>
    /// <param name="side">Side.</param>
    public bool CanAddSensorToSide(ESide side) {
      return NextEmptySensorIndex(side) >= 0;
    }

    /// <summary>
    /// Attempts to add the sensor to the given side of the analyzer.
    /// </summary>
    /// <returns><c>true</c>, if sensor to side was added, <c>false</c> otherwise.</returns>
    /// <param name="side">Side.</param>
    /// <param name="sensor">Sensor.</param>
    public bool AddSensorToSide(ESide side, Sensor sensor) {
			if (HasSensor(sensor)) {
				return false;
			}

      switch (side) {
        case ESide.Low:
          return AddSensorToRange(0, sensorsPerSide, sensor);
        case ESide.High:
          return AddSensorToRange(sensorsPerSide, sensors.Length, sensor);
        default:
          return false;  
      }
    }

    /// <summary>
    /// Queries the side of the given sensor. If the sensor is not present in the analyzer, we will return null and set
    /// side to ESide.Low.
    /// </summary>
    /// <returns><c>true</c>, if side of sensor was gotten, <c>false</c> otherwise.</returns>
    /// <param name="sensor">Sensor.</param>
    /// <param name="side">Side.</param>
    public bool GetSideOfSensor(Sensor sensor, out ESide side) {
      return GetSideOfIndex(IndexOfSensor(sensor), out side);
    }

		/// <summary>
		/// Queries whether or not the sensor is on the given side of the analyzer.
		/// </summary>
		/// <returns><c>true</c>, if sensor on side was ised, <c>false</c> otherwise.</returns>
		/// <param name="sensor">Sensor.</param>
		/// <param name="side">Side.</param>
		public bool IsSensorOnSide(Sensor sensor, ESide side) {
			if (!HasSensor(sensor)) {
				return false;
			}

			ESide actual;
			return GetSideOfSensor(sensor, out actual) && actual == side;
		}

    /// <summary>
    /// Queries the side of the given index. If the index is not a valid index within the analyzer, we will return null
    /// and set side to ESide.Low. 
    /// </summary>
    /// <returns><c>true</c>, if side of index was gotten, <c>false</c> otherwise.</returns>
    /// <param name="index">Index.</param>
    /// <param name="side">Side.</param>
    public bool GetSideOfIndex(int index, out ESide side) {
      if (index >= 0 && index < sensorsPerSide) {
        side = ESide.Low;
        return true;
      } else if (index >= sensorsPerSide && index < sensors.Length) {
        side = ESide.High;
        return true;
      } else {
        side = ESide.Low;
        return false;
      }
    }

    /// <summary>
    /// Attempts to place the given sensor in tho the given index slot. If a sensor is already present at the given
    /// index, it will be removed and replaced with the given sensor if force is true.
    /// </summary>
    /// <returns><c>true</c>, if sensor was put, <c>false</c> otherwise.</returns>
    /// <param name="index">Index.</param>
    /// <param name="sensor">Sensor.</param>
    public bool PutSensor(int index, Sensor sensor, bool force=false) {
      if (HasSensor(sensor) && !force) {
        return false;
      }

      RemoveSensor(index);

      sensors[index] = sensor;
      //sensor.onSensorStateChangedEvent += OnSensorChangedEvent;

      var ae = new AnalyzerEvent(AnalyzerEvent.EType.Added, index);
      NotifyOfAnalyzerEvent(ae);
      return true;
    }

    /// <summary>
    /// Removes the given sensor from the analyzer.
    /// </summary>
    /// <returns><c>true</c>, if a sensor was removed, <c>false</c> otherwise.</returns>
    /// <param name="sensor">Sensor.</param>
    public bool RemoveSensor(Sensor sensor) {
      return RemoveSensor(IndexOfSensor(sensor));
    }

    /// <summary>
    /// Removes the sensor at the given index.
    /// </summary>
    /// <returns><c>true</c>, if sensor was removed, <c>false</c> otherwise.</returns>
    /// <param name="index">Index.</param>
    public bool RemoveSensor(int index) {
      if (index < 0 || index >= sensors.Length || sensors[index] == null) {
        return false;
      }

      var sensor = sensors[index];
      sensors[index].onSensorStateChangedEvent -= OnSensorChangedEvent;
      sensors[index] = null;

			if (lowSideManifold != null) {
				if (lowSideManifold.primarySensor.Equals(sensor)) {
					RemoveManifold(ESide.Low);
				} else {
					lowSideManifold.SetSecondarySensor(null);
				}
			}

			if (highSideManifold != null) {
				if (highSideManifold.primarySensor.Equals(sensor)) {
					RemoveManifold(ESide.High);
				} else {
					highSideManifold.SetSecondarySensor(null);
				}
			}

      var ae = new AnalyzerEvent(AnalyzerEvent.EType.Removed, index);
      NotifyOfAnalyzerEvent(ae);

      return true;
    }

    /// <summary>
    /// Sets the new primary sensor for the manifold on the given side. If a manifold already exists, then it will
    /// be replaced by a new manifold.
    /// </summary>
    /// <returns><c>true</c>, if primary manifold was set, <c>false</c> otherwise.</returns>
    /// <param name="side">Side.</param>
    /// <param name="sensor">Sensor.</param>
    public bool SetManifold(ESide side, Sensor sensor) {
      var index = IndexOfSensor(sensor);

      if (index < 0) {
        // The sensor is not in the analyzer.
        return false;
      }

      switch (side) {
        case ESide.Low:
          RemoveManifold(ESide.Low);
          lowSideManifold = new Manifold(sensor);
          lowSideManifold.ptChart = PTChart.New(ion, Fluid.EState.Dew);
          NotifyOfAnalyzerEvent(new AnalyzerEvent(AnalyzerEvent.EType.ManifoldAdded, ESide.Low));
          return true;
        case ESide.High:
          RemoveManifold(ESide.High);
          highSideManifold = new Manifold(sensor);
          highSideManifold.ptChart = PTChart.New(ion, Fluid.EState.Bubble);
          NotifyOfAnalyzerEvent(new AnalyzerEvent(AnalyzerEvent.EType.ManifoldAdded, ESide.High));
          return true;
        default:
          throw new Exception("Cannot set primary manifold: unknown side: " + side);
      }
    }

		/// <summary>
		/// Sets the manifold for the given side. Is a manifold already exists, then it will be replaced by the new manifold.
		/// </summary>
		/// <returns><c>true</c>, if manifold was set, <c>false</c> otherwise.</returns>
		/// <param name="side">Side.</param>
		/// <param name="manifold">Manifold.</param>
		public bool SetManifold(ESide side, Manifold manifold) {
			switch (side) {
				case ESide.Low:
					RemoveManifold(ESide.Low);
					lowSideManifold = manifold;
					if (manifold.ptChart == null) {
						manifold.ptChart = PTChart.New(ion, Fluid.EState.Dew);
					}
					NotifyOfAnalyzerEvent(new AnalyzerEvent(AnalyzerEvent.EType.ManifoldAdded, ESide.Low));
					return true;
				case ESide.High:
					RemoveManifold(ESide.High);
					highSideManifold = manifold;
					if (manifold.ptChart == null) {
						manifold.ptChart = PTChart.New(ion, Fluid.EState.Bubble);
					}
					NotifyOfAnalyzerEvent(new AnalyzerEvent(AnalyzerEvent.EType.ManifoldAdded, ESide.High));
					return true;
				default:
					throw new Exception("Cannot set primary manifold: unknown side: " + side);
			}
		}

    /// <summary>
    /// Queries the and sets the value of side to the side of the given manifold. If the manifold is not present in the
    /// analyzer, we will return false and set the side to ESide.Low.
    /// </summary>
    /// <returns><c>true</c>, if side of manifold was gotten, <c>false</c> otherwise.</returns>
    /// <param name="manifold">Manifold.</param>
    /// <param name="side">Side.</param>
    public bool GetSideOfManifold(Manifold manifold, out ESide side) {
      if (lowSideManifold == manifold) {
        side = ESide.Low;
        return true;
      } else if (highSideManifold == manifold) {
        side = ESide.High;
        return true;
      } else {
        side = ESide.Low;
        return false;
      }
    }

    /// <summary>
    /// Queries whether or not the analyzer has a manifold on the given side.
    /// </summary>
    /// <returns><c>true</c> if this instance has manifold on side the specified ; otherwise, <c>false</c>.</returns>
    /// <param name="">.</param>
    public bool HasManifoldOnSide(ESide side) {
      switch (side) {
        case ESide.Low:
          return lowSideManifold != null;
        case ESide.High:
          return highSideManifold != null;
        default:
          return false;
      }
    }

    /// <summary>
    /// Attempts to remove the given manifold from the analyzer. Obviously, if the manifold is not in the analyzer, we
    /// will do nothing.
    /// </summary>
    /// <returns><c>true</c>, if manifold was removed, <c>false</c> otherwise.</returns>
    /// <param name="manifold">Manifold.</param>
    public void RemoveManifold(Manifold manifold) {
      if (lowSideManifold == manifold) {
        RemoveManifold(ESide.Low);
      } else if (highSideManifold == manifold) {
        RemoveManifold(ESide.High);
      }
    }

    /// <summary>
    /// Remove manifold at the given side form the analyzer.
    /// </summary>
    /// <returns><c>true</c>, if manifold was removed, <c>false</c> otherwise.</returns>
    /// <param name="side">Side.</param>
    public void RemoveManifold(ESide side) {
      switch (side) {
        case ESide.Low:
          lowSideManifold = null;
          NotifyOfAnalyzerEvent(new AnalyzerEvent(AnalyzerEvent.EType.ManifoldRemoved, ESide.Low));
          break;
        case ESide.High:
          highSideManifold = null;
          NotifyOfAnalyzerEvent(new AnalyzerEvent(AnalyzerEvent.EType.ManifoldRemoved, ESide.High));
          break;
      } 
    }

    /// <summary>
    /// Queries the manifold from the given side of the analyzer.
    /// </summary>
    /// <returns>The manifold from side.</returns>
    /// <param name="side">Side.</param>
    public Manifold GetManifoldFromSide(ESide side) {
      switch (side) {
        case ESide.Low:
          return lowSideManifold;
        case ESide.High:
          return highSideManifold;
        default:
          throw new Exception("Failed to get manifold from side: " + side);
      }
    }

    /// <summary>
    /// Queries the opposite side of the given side.
    /// </summary>
    /// <returns>The opposite side of.</returns>
    /// <param name="side">Side.</param>
    public ESide GetOppositeSideOf(ESide side) {
      switch (side) {
        case ESide.Low:
          return ESide.High;
        case ESide.High:
          return ESide.Low;
        default:
          throw new Exception("Cannot get opposite side for side: " + side);
      }
    }

    /// <summary>
    /// Queries whether or not the sensor at the two positions can successfully swap positions without breaking the
    /// integrity of the analyzer. Note: this method call does not mean you cannot swap, it simply means that if a
    /// swap is performed, the analyzer will destroy some previously set state.
    /// </summary>
    /// <example>
    /// For example, in the typical 8 slot analyzer, say a sensor is dragged from index 1 (lower left) to index 5
    /// (lower right). If one or both of these sensors are attached to the manifold, then a swap would mean that the
    /// sensors are now on the opposite side of the manifold. This is a break in the visual and logical integrity of the
    /// analyzer.
    /// </example>
    /// <returns><c>true</c> if this instance can sensors swap the specified first second; otherwise, <c>false</c>.</returns>
    /// <param name="first">First.</param>
    /// <param name="second">Second.</param>
    public bool CanSensorsSwapSafely(int first, int second) {
      ESide firstSide, secondSide;

      var sidesEqual = GetSideOfIndex(first, out firstSide) && GetSideOfIndex(second, out secondSide) && firstSide == secondSide;

      return sidesEqual || !(IsSensorIndexAttachedToManifold(first) || IsSensorIndexAttachedToManifold(second));
    }

    /// <summary>
    /// Attempts to swap the sensors that the given first index and the given second index. If either of the two sensors
    /// are attached to the low of high manifold, then the operation will fail unless force is true. If force is true,
    /// the the swap will happen destuctively, meaning the viewer state of the manifolds may be clobbered to make the
    /// swap happen.
    /// </summary>
    /// <returns><c>true</c>, if sensors was swaped, <c>false</c> otherwise.</returns>
    /// <param name="first">First.</param>
    /// <param name="second">Second.</param>
    /// <param name="force">If set to <c>true</c> force.</param>
		[Obsolete("This is essentially a 'God Function'.")]
		// TODO ahodder@appioninc.com: It is way too complicated and doesn't need to be this way. Make it either do the swap or not.
		// All the other things that this method does should be handled by the caller.
    public bool SwapSensors(int first, int second, bool force) {
      // The actual swap logic is this delegate.
      Action swap = delegate() {
        var f = sensors[first];
        var s = sensors[second];

        sensors[first] = s;
        sensors[second] = f;

        NotifyOfAnalyzerEvent(new AnalyzerEvent(first, second));
      };

      if (first < 0 || first > sensors.Length || second < 0 || second > sensors.Length) {
        return false;
      }

      if (CanSensorsSwapSafely(first, second)) {
        // Yay! We can simply swap the sensors, not big deal.
        swap();
        return true;
      }

      if (!force) {
        // Sadly, we can't force the swap. Bail.
        return false;
      }

      var firstIsInManifold = IsSensorIndexAttachedToManifold(first);
      var secondIsInManifold = IsSensorIndexAttachedToManifold(second);

      if (firstIsInManifold && secondIsInManifold) {
        // Great! Simply swap the manifolds and the two sensors.
        var low = lowSideManifold;
        var high = highSideManifold;

        lowSideManifold = high;
        highSideManifold = low;

				lowSideManifold.ptChart = PTChart.New(this.ion, Fluid.EState.Dew, lowSideManifold.ptChart.fluid);
				highSideManifold.ptChart = PTChart.New(this.ion, Fluid.EState.Bubble, highSideManifold.ptChart.fluid);

				lowSideManifold.SetSecondarySensor(null);
				highSideManifold.SetSecondarySensor(null);
      } else {
        if (firstIsInManifold) {
          ESide side;
          if (GetSideOfIndex(first, out side)) {
						var manifold = GetManifoldFromSide(side);
						if (manifold.primarySensor.Equals(sensors[first])) {
							RemoveManifold(side);
						} else {
							manifold.SetSecondarySensor(null);
						}
          }
        }

        if (secondIsInManifold) {
          ESide side;
          if (GetSideOfIndex(second, out side)) {
						var manifold = GetManifoldFromSide(side);
						if (manifold.primarySensor.Equals(sensors[second])) {
            	RemoveManifold(side);
						} else {
							manifold.SetSecondarySensor(null);
						}
          }
        }
      }

      swap();
      return true;
    }

		/// <summary>
		/// Attempts to remove the sensor from either of its manifolds.
		/// </summary>
		/// <param name="sensor">Sensor.</param>
		public void RemoveSensorFromManifold(Sensor sensor) {
			ESide side;
			if (!GetSideOfSensor(sensor, out side)) {
				return;
			}

			RemoveSensorFromManifold(side, sensor);
		}

		/// <summary>
		/// Attempts to remove the given sensor from the manifold on the given side of the analyzer.
		/// </summary>
		/// <param name="side">Side.</param>
		/// <param name="sensor">Sensor.</param>
		public void RemoveSensorFromManifold(ESide side, Sensor sensor) {
			var manifold = GetManifoldFromSide(side);
			if (manifold == null) {
				return;
			}

			if (manifold.primarySensor.Equals(sensor)) {
				RemoveManifold(side);
			} else if (manifold.secondarySensor != null && manifold.secondarySensor.Equals(sensor)) {
				manifold.SetSecondarySensor(null);
			}
		}

    /// <summary>
    /// Queries the fluid state based on the side of the analyzer.
    /// </summary>
    /// <returns>The as fluid state.</returns>
    /// <param name="side">Side.</param>
    public Fluid.EState SideAsFluidState(ESide side) {
      switch (side) {
        case ESide.Low:
          return Fluid.EState.Dew;
        case ESide.High:
          return Fluid.EState.Bubble;
        default:
          throw new Exception("Cannot get fluid state from side: " + side);
      }
    }

    /// <summary>
    /// Attempts to add the sensor to the into the given range within the analyzer.
    /// </summary>
    /// <returns><c>true</c>, if sensor to high side was added, <c>false</c> otherwise.</returns>
    /// <param name="sensor">Sensor.</param>
    private bool AddSensorToRange(int startInclusive, int endExclusive, Sensor sensor) {
      for (int i = startInclusive; i < endExclusive; i++) {
        if (sensors[i] == null) {
          sensors[i] = sensor;
          NotifyOfAnalyzerEvent(new AnalyzerEvent(AnalyzerEvent.EType.Added, i));
          return true;
        }
      }

      return false;
    }

    /// <summary>
    /// Notifies the onAnalyzerEvent handler that a new event should be thrown.
    /// </summary>
    /// <param name="analyzerEvent">Analyzer event.</param>
    private void NotifyOfAnalyzerEvent(AnalyzerEvent analyzerEvent) {
      if (onAnalyzerEvent != null) {
        onAnalyzerEvent(analyzerEvent);
      }
    }

    /// <summary>
    /// Called when a sensor in the analyzer throws an event.
    /// </summary>
    /// <param name="sensor">Sensor.</param>
    private void OnSensorChangedEvent(Sensor sensor) {
      NotifyOfAnalyzerEvent(new AnalyzerEvent(AnalyzerEvent.EType.SensorChanged, IndexOfSensor(sensor)));
    }

    /// <summary>
    /// Called when one of the analyzer's manifolds throws an event.
    /// </summary>
    /// <param name="manifoldEvent">Manifold event.</param>
    private void OnManifoldEvent(ManifoldEvent manifoldEvent) {
      NotifyOfAnalyzerEvent(new AnalyzerEvent(manifoldEvent));
    } 

    /// <summary>
    /// An enumeration of the "sides" of the analyzer.
    /// </summary>
    public enum ESide {
      Low,
      High,
    }
  }

	public static class AnalyzerESideExtensions {
		public static Analyzer.ESide Opposite(this Analyzer.ESide side) {
			switch (side) {
				case Analyzer.ESide.Low:
					return Analyzer.ESide.High;
				case Analyzer.ESide.High:
					return Analyzer.ESide.Low;
				default:
					return Analyzer.ESide.Low;
			}
		}
	}
}

 