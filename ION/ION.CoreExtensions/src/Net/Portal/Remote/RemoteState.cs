namespace ION.CoreExtensions.Net.Portal {

	using System;

	using Newtonsoft.Json;

	public class RemoteState {
		[JsonProperty("log")]
		public string isDataLogging;
	}
}
