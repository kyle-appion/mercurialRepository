using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ION.Core.IO {
  /// <summary>
  /// A utitlity class that will load embedded resources.
  /// </summary>
  public class EmbeddedResource {

    /// <summary>
    /// Loads an embedded resource from this library's embedded resources.
    /// </summary>
    /// <param name="filename">Filename.</param>
    /// <returns>A readonly stream.</returns>
    public static Stream Load(string filename) {
      return Load(typeof(EmbeddedResource).GetTypeInfo().Assembly, filename);
    }

    /// <summary>
    /// Loads the resource with the given filename from the given assembly's embedded resources.
    /// </summary>
    /// <param name="assembly">Assembly.</param>
    /// <param name="filename">Filename.</param>
    /// <returns>A readonly stream.</returns>
    public static Stream Load(Assembly assembly, string filename) {
      var resourceNames = assembly.GetManifestResourceNames();
      var resourcePaths = resourceNames.Where(x => x.EndsWith(filename)).ToArray();

      if (!resourcePaths.Any()) {
        throw new IOException("Could not load resource of name " + filename + ": doesn't exist");
      }

      if (resourcePaths.Count() > 1) {
        throw new IOException("Could not load resource of name " + filename + ": too many files of name, resolution is ambiguous");
      }

      return assembly.GetManifestResourceStream(resourcePaths[0]);
    }
  }
}

