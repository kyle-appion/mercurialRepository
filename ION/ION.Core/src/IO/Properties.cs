namespace ION.Core.IO {

  using System.Collections.Generic;
  using System.IO;
  using System.Threading.Tasks;

  /// <summary>
  /// Properties is a class that wraps a Java-like properties file.
  /// </summary>
  public class Properties {

    public string this[string key] {
      get { 
        if (content.ContainsKey(key)) {
          return content[key];
        } else {
          return null;
        }
      }
    }

    /// <summary>
    /// The backing dicationary for the properties.
    /// </summary>
    /// <value>The content.</value>
    private Dictionary<string, string> content { get; set; }

    /// <summary>
    /// Creates a new properties object from the given source.
    /// </summary>
    /// <param name="source">Source.</param>
    public Properties(Dictionary<string, string> source) {
      content = new Dictionary<string, string>(source);
    }

    /// <summary>
    /// Attempts to load the properties from the given file. If the properties
    /// cannot be loaded, then we will throw an IOException.
    /// </summary>
    /// <returns>The file.</returns>
    /// <param name="file">File.</param>
    public async static Task<Properties> FromFileAsync(IFile file) {
      using (var reader = new System.IO.StreamReader(file.OpenForReading())) { 
        var dict = new Dictionary<string, string>();

        string line = null;
        var lineNumber = 1;
        while ((line = reader.ReadLine()) != null) {
          var parts = line.Split(':');
          if (parts.Length != 2) {
            throw new System.IO.IOException("Cannot load properties: invalid property definition at line " + lineNumber);
          }
          dict[parts[0].Trim()] = parts[1].Trim();
          lineNumber++;
        }

        return new Properties(dict);
      }
    }

    /// <summary>
    /// Attempts to load the properties from the given stream. If the properties
    /// cannot be loaded, then we will throw an IOException.
    /// </summary>
    /// <returns>The stream async.</returns>
    /// <param name="stream">Stream.</param>
    public async static Task<Properties> FromStreamAsync(Stream stream) {
      using (var reader = new StreamReader(stream)) {
        var dict = new Dictionary<string, string>();

        string line = null;
        var lineNumber = 1;
        while ((line = reader.ReadLine()) != null) {
          var parts = line.Split(':');
          if (parts.Length != 2) {
            throw new System.IO.IOException("Cannot load properties: invalid property definition at line " + lineNumber);
          }
          dict[parts[0].Trim()] = parts[1].Trim();
          lineNumber++;
        }

        return new Properties(dict);
      }
    }
  }
}

