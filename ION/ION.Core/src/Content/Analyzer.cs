namespace ION.Core.Content {

  using System;

  using ION.Core.Sensors;

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
    /// <param name="type">Type.</param>
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

    public AnalyzerEvent(EType type, int index) {
      this.type = type;
      this.index = index;
    }

    /// <summary>
    /// Creates a new Swap analyzer event.
    /// </summary>
    /// <param name="type">Type.</param>
    /// <param name="first">First.</param>
    /// <param name="second">Second.</param>
    public AnalyzerEvent(int first, int second) {
      this.type = EType.Swapped;
      this.index = first;
      this.otherIndex = second;
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
    }
  }

  /// <summary>
  /// An analyzer is an abstract representation of an HVAC system. It allows technicians to place sensors at various
  /// location around this absraction. The analyzer is heavily used in various reports, as a correctly lain out
  /// analyzer will describe an entire systems state.
  /// </summary>
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
    /// The number of sensors that the analyzer supports per side.
    /// </summary>
    /// <value>The sensors per side.</value>
    public int sensorsPerSide { get; private set; }

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
        }

        __lowSideManifold = value;

        if (__lowSideManifold != null) {
          __lowSideManifold.onManifoldEvent += OnManifoldEvent;
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
    /// The array of sensors that comprise the analyzer. Null indicates an empty slot.
    /// </summary>
    private Sensor[] sensors;

    /// <summary>
    /// Creates a new analyzer.
    /// </summary>
    /// <param name="sensorsPerSide">The number of sensors per side of the analyzer. This value must be an increment 
    /// of 2</param>
    public Analyzer(int sensorsPerSide=DEFAULT_SENSORS_PER_SIDE) {
      if (sensorsPerSide % 2 != 0) {
        throw new Exception("Cannot create new analyzer: sensors per side must be an increment of two. {is : " + sensorsPerSide + "}");
      }
      this.sensorsPerSide = sensorsPerSide;
      sensors = new Sensor[sensorsPerSide * 2];
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
        if (sensor.Equals(sensors[i])) {
          return i;
        }
      }

      // We don't need to check manifolds as they can only exist if their sensors are presnt in the sensor array.
      return -1;
    }

    /// <summary>
    /// Queries whether the sensor at the given index is attached to a manifold.
    /// </summary>
    /// <returns><c>true</c> if this instance is sensor index attached to manifold the specified index; otherwise, <c>false</c>.</returns>
    /// <param name="index">Index.</param>
    public bool IsSensorIndexAttachedToManifold(int index) {
      if (index >= 0 && index < sensorsPerSide) {
        return lowSideManifold != null && 
          (lowSideManifold.primarySensor.Equals(sensors[index]) || lowSideManifold.secondarySensor.Equals(sensors[index]));
      } else if (index >= sensorsPerSide && index < sensors.Length) {
        return highSideManifold != null && 
          (highSideManifold.primarySensor.Equals(sensors[index]) || highSideManifold.secondarySensor.Equals(sensors[index]));
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
      sensor.onSensorStateChangedEvent += OnSensorChangedEvent;

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

      sensors[index].onSensorStateChangedEvent -= OnSensorChangedEvent;
      sensors[index] = null;

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
    public bool SetPrimaryManifold(ESide side, Sensor sensor) {
      var index = IndexOfSensor(sensor);

      if (index < 0) {
        // The sensor is not in the analyzer.
        return false;
      }

      switch (side) {
        case ESide.Low:
          lowSideManifold = new Manifold(sensor);
          return true;
        case ESide.High:
          highSideManifold = new Manifold(sensor);
          return true;
        default:
          throw new Exception("Cannot set primary manifold: unknown side: " + side);
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

      return GetSideOfSensor(sensors[first], out firstSide) && GetSideOfSensor(sensors[second], out secondSide) && firstSide == secondSide;
    }

    /// <summary>
    /// Attempts to swap the sensors that the given first index and the given second index. If either of the two sensors
    /// are attached to the low of high manifold, then the operation will fail unless force is true. If force is true,
    /// the the swap will happen destuctively, meaning the viewer state may be clobbered to make the swap happen.
    /// </summary>
    /// <returns><c>true</c>, if sensors was swaped, <c>false</c> otherwise.</returns>
    /// <param name="first">First.</param>
    /// <param name="second">Second.</param>
    /// <param name="force">If set to <c>true</c> force.</param>
    public bool SwapSensors(int first, int second, bool force) {
      // The actual swap logic is this delegate.
      Action swap = delegate() {
        var f = sensors[first];
        var s = sensors[second];

        sensors[first] = f;
        sensors[second] = s;

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
      } else {
        if (firstIsInManifold) {
          ESide side;
          if (GetSideOfIndex(first, out side)) {
            SetPrimaryManifold(side, null);
          }
        }

        if (secondIsInManifold) {
          ESide side;
          if (GetSideOfIndex(second, out side)) {
            SetPrimaryManifold(side, null);
          }
        }
      }

      swap();
      return true;
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
    }

    /// <summary>
    /// Called when one of the analyzer's manifolds throws an event.
    /// </summary>
    /// <param name="manifoldEvent">Manifold event.</param>
    private void OnManifoldEvent(ManifoldEvent manifoldEvent) {

    } 

    /// <summary>
    /// An enumeration of the "sides" of the analyzer.
    /// </summary>
    public enum ESide {
      Low,
      High,
    }
  }
}

