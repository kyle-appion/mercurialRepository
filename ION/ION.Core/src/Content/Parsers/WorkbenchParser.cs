namespace ION.Core.Content.Parsers {

	using System;
	using System.IO;

	using Appion.Commons.Util;

	using ION.Core.App;
	using ION.Core.IO;

  public class WorkbenchParser : IParser<Workbench> {
    // Overridden from IParser
    public int version { get { return 2; } }

    // Overridden from IParser
    public void WriteToStream(IION ion, Workbench workbench, Stream stream) {
      try {
        using (var writer = new BinaryWriter(stream)) {
          // Persist the version of the parser
          writer.Write(version);
          // Write how many manifolds are present in the workbench
          writer.Write(workbench.count);
					// Write all of the manifolds to the stream
          for (int i = 0; i < workbench.count; i++) {
						ManifoldParser.WriteManifold(ion, workbench[i], writer);
          }
        }
      } catch (Exception e) {
        Log.E(this, "Failed to write workbench to stream", e);
      }
    }

    // Overridden form IParser
    public Workbench ReadFromStream(IION ion, Stream stream) {
      try {
        using (var reader = new BinaryReader(stream)) {
          var v = reader.ReadInt32();

					if (version != v) {
						throw new IOException("Cannot read workbench from stream: expected version " + version + " received " + v);
					}

          // Read the number of manifold that were stored
          var len = reader.ReadInt32();

          var ret = new Workbench(ion);

          for (int i = 0; i < len; i++) {
						var manifold = ManifoldParser.ReadManifold(ion, reader);
            if (manifold != null) {
              try {
                ret.Add(manifold);
              } catch (Exception e) {
                Log.E(this, "Failed to read manifold", e);
              }
            }
          }

          return ret;
        }
      } catch (Exception e) {
        Log.E(this, "Failed to read workbench from stream", e);
        return null;
      }
    }
  }
}

