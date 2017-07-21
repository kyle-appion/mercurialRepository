using System.Linq;

namespace ION.Core.Devices {
  
  using System.Collections.Generic;
  public static class DeviceExtensions {

    public static void GeneralSort(this IEnumerable<GaugeDeviceSensor> enumerable) {
      enumerable.OrderBy(x => x.device.serialNumber.deviceModel)
        .ThenBy(x => x.device.serialNumber)
        .ThenBy(x => x.index);
    }
  }
}