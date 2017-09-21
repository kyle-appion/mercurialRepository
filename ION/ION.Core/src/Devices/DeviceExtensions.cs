using System.Collections.Generic;
using System.Linq;

using ION.Core.App;
using ION.Core.Connections;

namespace ION.Core.Devices {

  public static class DeviceExtensions {
    public static void GeneralSort(this IEnumerable<GaugeDeviceSensor> enumerable) {
      enumerable.OrderBy(x => x.device.serialNumber.deviceModel)
        .ThenBy(x => x.device.serialNumber)
        .ThenBy(x => x.index);
    }

#if DEBUG
    public static IDevice CreateDebugDevice(IION ion, EDeviceModel deviceModel) {
      var sn = deviceModel.GenerateSerialNumber();

      var factory = ion.deviceManager.deviceFactory;
      var defn = factory.GetDeviceDefinition(sn);
      return defn.CreateDevice(ion,sn, new DebugConnection(sn.ToString(), "DebugAddress_" + sn.batchId), null);
    }
#endif
  }
}
