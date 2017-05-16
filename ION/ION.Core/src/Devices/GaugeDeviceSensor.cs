namespace ION.Core.Devices {

  using System;

  using Appion.Commons.Measure;
  using ION.Core.App;
  using ION.Core.Sensors;

  /// <summary>
  /// A Type of sensor that exists within a GaugeDevice.
  /// </summary>
  public class GaugeDeviceSensor : Sensor {
    /// <summary>
    /// Whether or not te sensor's reading is editable.
    /// </summary>
    /// <value>true</value>
    /// <c>false</c>
    public override bool isEditable {
      get {
        return false;
      }
    }
		// Overridden from Sensor
		public override string name {
			get {
				return device.name;
			}
			set {
				device.name = value;
				NotifySensorStateChanged();
			}
		}
    /// <summary>
    /// The device that this sensor belongs to.
    /// </summary>
    /// <value>The device.</value>
    public GaugeDevice device { get; internal set; }
    /// <summary>
    /// The 0-based index that the sensor is within the device.
    /// </summary>
    /// <value>The index.</value>
    public int index { get; internal set; }
    /// <summary>
    /// Whether or not the gauge device sensor is removable from its host device.
    /// </summary>
    /// <value><c>true</c> if removable; otherwise, <c>false</c>.</value>
    public bool removable { get; internal set; }
    /// <summary>
    /// Whether or not the gauge device sensor is removed. A removed GaugeDeviceSensor
    /// will not have its measurement updated.
    /// </summary>
    /// <value><c>true</c> if removed; otherwise, <c>false</c>.</value>
    public bool removed { get; internal set; }

    public GaugeDeviceSensor(GaugeDevice device, int index, ESensorType sensorType, bool relative = true)
      : base(sensorType, AppState.context.preferences.units.DefaultUnitFor(sensorType).OfScalar(0.0), relative) {
      this.device = device;
      this.index = index;
    }

    /// <summary>
    /// A hook that is to be called when a device receives a measurement packet.
    /// </summary>
    /// <param name="measurement">Measurement.</param>
    internal void SetMeasurement(Scalar measurement) {
      ForceSetMeasurement(measurement);
    }

		public override string ToString() {
			return string.Format("[GaugeDeviceSensor: isEditable={0}, device={1}, index={2}, removable={3}, removed={4}]", isEditable, device, index, removable, removed);
		}
  }
}

