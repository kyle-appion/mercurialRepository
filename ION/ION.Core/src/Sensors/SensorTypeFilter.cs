namespace ION.Core.Sensors {
  
  using System;

	using Appion.Commons.Util;

  public abstract class SensorTypeFilter : IFilter<ESensorType[]> {
    /// <summary>
    /// The sorted sensor types that will be matched against.
    /// </summary>
    protected ESensorType[] filterTypes { get; private set; }

    /// <summary>
    /// Creates a new SensorTypeFilter that will match the given sensor types.
    /// </summary>
    /// <param name="flags">Flags.</param>
    public SensorTypeFilter(params ESensorType[] sensorTypes) {
//      this.filterTypes = Sort(sensorTypes);
      this.filterTypes = sensorTypes;
    }

    /// <summary>
    /// Queries whether or not the given item matches the filter.
    /// </summary>
    /// <param name="t">T.</param>
    /// <param name="sensorTypes">Sensor types.</param>
    public abstract bool Matches(ESensorType[] sensorTypes);

    /// <summary>
    /// Sorts the given sensor types by enum value.
    /// </summary>
    /// <param name="sensorTypes">Sensor types.</param>
    protected ESensorType[] Sort(ESensorType[] sensorTypes) {
      Array.Sort(sensorTypes);
      return sensorTypes;
    }
  }

  /// <summary>
  /// A SensorTypeFilter that will match sensor types to all of the sensor types given to the filter at creation.
  /// </summary>
  public class ExactSensorTypeFilter : SensorTypeFilter {
    public ExactSensorTypeFilter(params ESensorType[] sensorTypes) : base(sensorTypes) {
    }

    /// <summary>
    /// Queries whether or not the given item matches the filter.
    /// </summary>
    /// <param name="t">T.</param>
    /// <param name="sensorTypes">Sensor types.</param>
    public override bool Matches(ESensorType[] sensorTypes) {
      if (filterTypes.Length != sensorTypes.Length) {
        return false;
      }

      sensorTypes = Sort(sensorTypes);

      for (int i = 0; i < sensorTypes.Length; i++) {
        if (filterTypes[i] != sensorTypes[i]) {
          return false;
        }
      }

      return true;
    }
  }
}

