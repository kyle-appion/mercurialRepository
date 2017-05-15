namespace ION.Core.Sensors {

  using System;
  using Appion.Commons.Measure;

  /// <summary>
  /// A sensor that reprents a manually entered sensor value.
  /// </summary>
  // TODO ahodder@appioninc.com: Is this really needed?
  public class ManualSensor : Sensor {
    /// <summary>
    /// Whether or not te sensor's reading is editable.
    /// </summary>
    /// <value>true</value>
    /// <c>false</c>
    public override bool isEditable {
      get {
        return true;
      }
    }

    public ManualSensor(ESensorType sensorType, Scalar manualScalar, bool isRelative=true) : base(sensorType, manualScalar, isRelative) {
    }

    public ManualSensor(ESensorType sensorType, bool isRelative=true) : base(sensorType, isRelative) {
    }
  }
}

