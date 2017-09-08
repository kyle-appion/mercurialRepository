namespace ION.Core.IO {

  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Linq;
  using System.Reflection;

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

    /// <summary>
    /// Loads a list of the embedded resources that are in the project at the given path.
    /// </summary>
    /// <returns>The <see cref="System.Collections.Generic.List`1[[System.String]]"/>.</returns>
    /// <param name="ext">Path.</param>
    public static List<string> GetResourcesOfExtension(string ext) {
      return GetResourcesOfExtension(typeof(EmbeddedResource).GetTypeInfo().Assembly, ext);
    }

    /// <summary>
    /// Loads a list of the embedded resources that are in the project at the given path.
    /// </summary>
    /// <returns>The <see cref="System.Collections.Generic.List`1[[System.String]]"/>.</returns>
    /// <param name="assembly">Assembly.</param>
    /// <param name="ext">ext.</param>
    public static List<string> GetResourcesOfExtension(Assembly assembly, string ext) {
      var ret = new List<string>();

      var resourceNames = assembly.GetManifestResourceNames();
      var resourcePaths = resourceNames.Where(x => x.EndsWith(ext)).ToArray();

      ret.AddRange(resourcePaths);

      return ret;
    }
  }
}

