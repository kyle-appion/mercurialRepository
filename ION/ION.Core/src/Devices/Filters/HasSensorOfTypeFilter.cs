namespace ION.Core.Devices.Filters {
  
	using Appion.Commons.Util;

  using ION.Core.Devices;
  using ION.Core.Sensors;

  public class HasSensorOfTypeFilter : AbstractFilter<IDevice> {

    private ESensorType sensorType;

    public HasSensorOfTypeFilter(ESensorType sensorType) {
      this.sensorType = sensorType;
    }

    public override bool Matches(IDevice t) {
      var gd = t as GaugeDevice;

      if (gd != null) {
        foreach (var s in gd.sensors) {
          if (sensorType == s.type) {
            return true;
          }
        }

        return false;
      } else {
        return false;
      }
    }
  }
}

