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
		public bool isDataLogging;
    /// <summary>
    /// The battery level for the remote device.
    /// </summary>
    [JsonProperty("battery")]
    public int batteryLevel;
    /// <summary>
    /// Whether or not the wifi is enabled for the remote device.
    /// </summary>
    [JsonProperty("wifi")]
    public bool wifiStatus;
    /// <summary>
    /// The memory remaining on the remote device in bytes.
    /// </summary>
    [JsonProperty("memory")]
    public long remainingMemory;

    public RemoteStatus() {
    }

    public RemoteStatus(IION ion) {
      var pi = ion.GetPlatformInformation();

      isDataLogging = ion.dataLogManager.isRecording;
      batteryLevel = pi.batteryPercentage;
      wifiStatus = pi.wifiConnected;
      remainingMemory = pi.freeMemory;
    }
	}
}
