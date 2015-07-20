using System;

using Foundation;
using UIKit;

using ION.Core.Measure;
using ION.Core.Sensors;

namespace ION.IOS {
  [Register("DeviceManagerSensorView")]
  public partial class DeviceManagerSensorView : UICollectionReusableView {
    /// <summary>
    /// Creates a new DeviceManagerSensorView from a nib.
    /// </summary>
    /// <returns>The from nib.</returns>
    public static DeviceManagerSensorView CreateFromNib(UIView parent) {
      return NSBundle.MainBundle.LoadNib(typeof(DeviceManagerSensorView).Name, parent, null).GetItem<DeviceManagerSensorView>(0);
    }

    /// <summary>
    /// The sensor that the view is displaying.
    /// </summary>
    /// <value>The sensor.</value>
    public Sensor sensor { get; private set; }

    public DeviceManagerSensorView(IntPtr handle) : base(handle) {
      // Nope
    }

    /// <summary>
    /// Applys the given sensor to the view. The view will automatically
    /// resolve itself to changes within the sensor. When the view dies, it
    /// will unregister itself from the sensor it you haven't already.
    /// </summary>
    /// <param name="sensor">Sensor.</param>
    public void Apply(Sensor sensor) {
      if (this.sensor != null) {
        this.sensor.readingChanged -= HandleReadingChanged;
      }

      this.sensor = sensor;

      if (this.sensor != null) {
        this.sensor.readingChanged += HandleReadingChanged;

        labelSensorType.Text = sensor.sensorType.GetTypeString();
      }
    }

    /// <summary>
    /// Handles the change of the sensor's reading.
    /// </summary>
    /// <param name="sensor">Sensor.</param>
    /// <param name="reading">Reading.</param>
    private void HandleReadingChanged(Sensor sensor, Scalar reading) {
      // TODO ahodder@appioninc.com: Localize
      labelSensorMeasurement.Text = reading.ToString();
    }
  }
}

