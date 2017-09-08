namespace ION.Core.Devices.Sorters {

  using System;
  using System.Collections.Generic;

  public class SerialNumberSorter : IComparer<ISerialNumber> {

    /// <summary>
    /// Compares to.
    /// </summary>
    /// <returns>The to.</returns>
    /// <param name="s1">S1.</param>
    /// <param name="s2">S2.</param>
    public int Compare(ISerialNumber s1, ISerialNumber s2) {
      if (s1.deviceType == s2.deviceType) {
        return s1.deviceModel.CompareTo(s2.deviceModel);
      } else {
        return s1.deviceType.CompareTo(s2.deviceType);
      }
    }
  }
}

