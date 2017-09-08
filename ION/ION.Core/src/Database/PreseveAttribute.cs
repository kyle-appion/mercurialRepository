namespace ION.Core.Database {
	
	using System;

	public sealed class PreserveAttribute : System.Attribute {
		public bool AllMembers;
		public bool Conditional;
	}
}

