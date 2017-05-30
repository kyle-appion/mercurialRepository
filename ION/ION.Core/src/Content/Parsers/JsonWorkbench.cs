namespace ION.Core.Content.Parsers {

	using System;

	using Newtonsoft.Json;

	public class JsonWorkbench {

		[JsonProperty("manifolds")]
		public JsonManifold[] manifolds;

		public JsonWorkbench() {
		}

		public JsonWorkbench(Workbench workbench) {
			manifolds = new JsonManifold[workbench.count];

			for(int i = 0; i < manifolds.Length; i++) {
//				manifolds[i] = new JsonManifold(workbench[i]);
			}
		}
	}
}
