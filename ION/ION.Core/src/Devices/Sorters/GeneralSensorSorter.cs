using System.Collections.Generic;

namespace ION.Core.Devices.Sorters {
  /// <summary>
  /// A simple sorter that will sort GaugeDeviceSensors first by Model, then serial number and finally sensor index.
  /// If either of the sensors are null, then 0 is returned as it is likely that the null is used to indicates specific
  /// ordering.
  /// </summary>
  public class GeneralSensorSorter : IComparer<GaugeDeviceSensor> {
    public int Compare(GaugeDeviceSensor o1, GaugeDeviceSensor o2) {
      if (o1 == null || o2 == null) {
        return 0;
      }
      
      var ret = o1.device.serialNumber.deviceModel.CompareTo(o2.device.serialNumber.deviceModel);

      if (ret == 0) {
        return o1.device.serialNumber.CompareTo(o2.device.serialNumber);
      } else {
        return ret;
      }
    }
    
  }
}