namespace ION.CoreExtensions.Net.Portal {

	public class AccessCode {
		/// <summary>
		/// The id that is used to tell us if someone has requested access.
		/// </summary>
		/// <value>The identifier.</value>
		public int acceptId { get; private set; }
		public string code { get; private set; }
		public string user { get; private set; }

		public AccessCode(int id, string code, string user) {
			this.acceptId = id;
			this.code = code;
			this.user = user;
		}
	}
}
