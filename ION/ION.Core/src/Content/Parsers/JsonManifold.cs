namespace ION.Core.Content.Parsers {

	using System;

	using Newtonsoft.Json;

	public class JsonManifold {

		/// <summary>
		/// The object that contains that primary sensor for the manifold.
		/// </summary>
		[JsonProperty("primarySensor")]
		public JsonSensor primarySensor;

		public JsonManifold() {
		}
	}
}
