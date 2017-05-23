namespace ION.Core.Content.Parsers {

  using System.IO;

  using ION.Core.App;
  using ION.Core.Content;
	using ION.Core.Content.Parsers.ManifoldParsers;

  /// <summary>
  /// A ManifoldParser is not a parser in that it is not an IParser<T>. However, is does have versioning and does export
	/// sensors out in a binary format.
  /// </summary>
	public abstract class ManifoldParser {
		private const int VERSION = 2;

		private static ManifoldParser CURRENT_PARSER = new V2();

		public abstract int version { get; }
		public abstract void DoWriteManifold(IION ion, Manifold manifold, BinaryWriter writer);
		public abstract Manifold DoReadManifold(IION ion, BinaryReader reader);


		public static void WriteManifold(IION ion, Manifold manifold, BinaryWriter writer) {
			writer.Write(VERSION);
			CURRENT_PARSER.DoWriteManifold(ion, manifold, writer);
		}

		/// <summary>
		/// Attempts to read a manifold from the reader. If the manifold cannot or is not read from the reader, we will
		/// return null.
		/// Note: exceptions are NOT caught.
		/// </summary>
		/// <returns>The sensor.</returns>
		/// <param name="ion">Ion.</param>
		/// <param name="reader">Reader.</param>
		public static Manifold ReadManifold(IION ion, BinaryReader reader) {
			var v = reader.ReadInt32();
			switch (v) {
				case 1:
					return new V1().DoReadManifold(ion, reader);
				case 2:
					return new V2().DoReadManifold(ion, reader);
				default:
					return null;
			}
		}
  }
}

