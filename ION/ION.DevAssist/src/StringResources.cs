namespace ION.DevAssist {

	using System;
	using System.Collections.Generic;

	public class StringResources {
		/// <summary>
		/// The platform that the resource belong to.
		/// </summary>
		/// <value>The platform.</value>
		public EPlatform platform { get; private set; }
		/// <summary>
		/// Queries the section with the given key.
		/// </summary>
		/// <param name="key">Key.</param>
		public Section this[string key] { 
			get {
				Section s = null;

				if (sections.ContainsKey(key)) {
					s = sections[key];
				} else {
					sections[key] = s = new Section() { name = key };
				}

				return s;
			}
		}

		/// <summary>
		/// The dictionary of key-to-sections that make up the string resource pool.
		/// </summary>
		private Dictionary<string, Section> sections = new Dictionary<string, Section>();

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ION.DevAssist.StringResources"/> class.
		/// </summary>
		public StringResources(EPlatform platform) {
			this.platform = platform;
		}

		/// <summary>
		/// Adds a new value to the resources.
		/// </summary>
		/// <param name="key">Key.</param>
		public bool AddValue(string section, string key, string value) {
			var s = this[section];

			if (s.ContainsKey(key)) {
				return false;
			} else {
				s.resources[key] = value;
				return true;
			}
		}

		/// <summary>
		/// Queries whether or not the given section has the given key.
		/// </summary>
		/// <returns><c>true</c>, if key was hased, <c>false</c> otherwise.</returns>
		/// <param name="section">Section.</param>
		/// <param name="key">Key.</param>
		public bool HasKey(string section, string key) {
			return this[section].ContainsKey(key);
		}

		/// <summary>
		/// The wrapper class that will maintain a collection of string that are categorized.
		/// </summary>
		public class Section {
			public string name;
			public Dictionary<string, string> resources = new Dictionary<string, string>();

			public bool ContainsKey(string key) {
				if (key == null) {
					return false;
				}
				return resources.ContainsKey(key);
			}
		}

		[Flags]
		public enum EPlatform {
			None = 0,
			Android = 1 << 0,
			Ios = 1 << 1,
			Both = Android | Ios,
		}
	}
}
