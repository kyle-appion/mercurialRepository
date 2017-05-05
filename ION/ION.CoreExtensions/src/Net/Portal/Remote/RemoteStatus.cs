namespace ION.CoreExtensions.Net.Portal.Remote {

	using System;

	using Newtonsoft.Json;

  using ION.Core.App;

  /// <summary>
  /// A simple structure that represents more meta state for the remote device. Things like the device's state and 
  /// persistent application status' will go here.
  /// </summary>
	public class RemoteStatus {
    /// <summary>
    /// Whether or not the remote device is data logging.
    /// </summary>
		[JsonProperty("log")]
    private int __isDataLogging;
    public bool isDataLogging { get { return __isDataLogging == 1; } }
    /// <summary>
    /// The battery level for the remote device.
    /// </summary>
    [JsonProperty("battery")]
    public int batteryLevel;
    /// <summary>
    /// Whether or not the wifi is enabled for the remote device.
    /// </summary>
    [JsonProperty("wifi")]
    private int __wifiStatus;
    public bool wifiStatus { get { return __wifiStatus == 1; } }
    /// <summary>
    /// The memory remaining on the remote device in bytes.
    /// </summary>
    [JsonProperty("memory")]
    public double __remainingMemory;
    public long remainingMemory { get { return (long)__remainingMemory; } }

    public RemoteStatus() {
    }

    public RemoteStatus(IION ion) {
      var pi = ion.GetPlatformInformation();

      __isDataLogging = ion.dataLogManager.isRecording ? 1 : 0;
      batteryLevel = pi.batteryPercentage;
      __wifiStatus = pi.wifiConnected ? 1 : 0;
      __remainingMemory = pi.freeMemory;
    }
	}
}
