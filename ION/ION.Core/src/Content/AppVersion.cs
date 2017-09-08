namespace ION.Core.Content {

	using System;

	/// <summary>
	/// A parsed version value.
	/// </summary>
	public class AppVersion : IComparable<AppVersion> {

		public int major { get; private set; }
		public int minor { get; private set; }
		public int revision { get; private set; }


		public AppVersion(int major, int minor, int revision) {
			this.major = major;
			this.minor = minor;
			this.revision = revision;
		}

		public override bool Equals(object obj) {
			var av = obj as AppVersion;
			if (av != null) {
				return av.major == major && av.minor == minor && av.revision == revision;
			} else {
				return false;
			}
		}

		public override int GetHashCode() {
			return major.GetHashCode() ^ minor.GetHashCode() | revision.GetHashCode();
		}

		public override string ToString() {
			return string.Format("{0}.{1}.{2}", major, minor, revision);
		}

		// Implemented from IComparable
		public int CompareTo(AppVersion version) {
			var me = major * 1000000 + minor * 1000 + revision;
			var other = version.major * 1000000 + version.minor * 1000 + version.revision;
			return me - other;
		}

		public static AppVersion ParseOrThrow(string version) {
			Appion.Commons.Util.Log.D("AppVersion", "Parsing version: " + version);
			var parts = version.Split('.');
			if (parts.Length != 3) {
				//throw new Exception("Cannot parse what's new: invalid version {" + version + "}");
			}

			return new AppVersion(int.Parse(parts[0]), int.Parse(parts[1]), int.Parse(parts[2]));
		}
	}
}
