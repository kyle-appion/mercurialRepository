namespace ION.CoreExtensions.Net.Portal {

	using System;
  using Newtonsoft.Json;

	/// <summary>
	/// A simple class that represents the high level information about a user.
	/// </summary>
	public class ConnectionData {
    [JsonProperty("ID")]
		public int id { get; set; }
    [JsonProperty("display")]
		public string displayName { get; set; }
    [JsonProperty("email")]
		public string email { get; set; }
    [JsonProperty("online")]
		public bool isUserOnline { get; set; }
    [JsonProperty("devicename")]
    public string deviceName { get; set; }
    [JsonProperty("deviceid")]
    public string deviceId { get; set; }
    [JsonProperty("layoutid")]
    public string layoutId { get; set; }
    
    [JsonConstructor]
    public ConnectionData() {
    }
	}
}
