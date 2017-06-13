namespace ION.CoreExtensions {

	using Newtonsoft.Json;

	public class AnalyzerPositions {
		[JsonProperty("positions")]
		public int[] sensorPositions { get; set; }
		[JsonProperty("fluid")]
		public string fluid { get; set; }
	}
}
