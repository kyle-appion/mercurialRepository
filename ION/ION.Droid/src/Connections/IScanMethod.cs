namespace ION.Droid.Connections {
  internal interface IScanMethod {
    /// <summary>
    /// Whether or not the scan method is currently performing a scan.
    /// </summary>
    /// <value><c>true</c> if is scanning; otherwise, <c>false</c>.</value>
    bool isScanning { get; }
    /// <summary>
    /// Starts a new scan instance. If we are already scanning, we will return true.
    /// </summary>
    /// <returns><c>true</c>, if scan was started, <c>false</c> otherwise.</returns>
    bool StartScan();
    /// <summary>
    /// Stops a current running scan.
    /// </summary>
    void StopScan();
  }
}
