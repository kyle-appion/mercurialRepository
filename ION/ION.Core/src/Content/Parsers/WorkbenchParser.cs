using System;
using System.IO;

using ION.Core.App;
using ION.Core.IO;
using ION.Core.Util;

namespace ION.Core.Content.Parsers {
  public class WorkbenchParser : IParser<Workbench> {
    // Overridden from IParser
    public int version { get { return 1; } }

    // Overridden from IParser
    public void WriteToStream(IION ion, Workbench workbench, Stream stream) {
      try {
        var mp = new ManifoldParser();
        using (var writer = new BinaryWriter(stream)) {
          // Persist the version of the parser
          writer.Write(version);
          // Write how many manifolds are present in the workbench
          writer.Write(workbench.count);
          for (int i = 0; i < workbench.count; i++) {
            mp.WriteToStream(ion, workbench[i], stream);
          }
        }
      } catch (Exception e) {
        Log.E(this, "Failed to write workbench to stream", e);
      }
    }

    // Overridden form IParser
    public Workbench ReadFromStream(IION ion, Stream stream) {
      try {
        var mp = new ManifoldParser();

        using (var reader = new BinaryReader(stream)) {
          var version = reader.ReadInt32();

          // TODO ahodder@appioninc.com: Implement version checking
          // Throw or delegate on version difference
          if (this.version != version) { 
            throw new IOException("Cannot read workbench from stream: expected version " + this.version + " receved " + version);
          }

          // Read the number of manifold that were stored
          var len = reader.ReadInt32();

          var ret = new Workbench(ion);

          for (int i = 0; i < len; i++) {
            var manifold = mp.ReadFromStream(ion, stream);
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

