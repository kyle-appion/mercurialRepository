namespace ION.Core.Devices.Sorters {

  using System;
  using System.Collections.Generic;

  public class DeviceSerialNumberSorter : IComparer<IDevice> {

    private SerialNumberSorter sorter = new SerialNumberSorter();

    /// <summary>
    /// Compares to.
    /// </summary>
    /// <returns>The to.</returns>
    /// <param name="d1">D1.</param>
    /// <param name="d2">D2.</param>
    public int Compare(IDevice d1, IDevice d2) {
      return sorter.Compare(d1.serialNumber, d2.serialNumber);
    }
  }
}

