using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ION.Core.IO {
  /// <summary>
  /// Defines a representation of the location of a file/folder on a file system.
  /// </summary>
  public sealed class Path {
    /// <summary>
    /// Queries the parent for this path. Essentially cd ..
    /// </summary>
    public Path parent { get; private set; }
    /// <summary>
    /// The local name of the path. This is the immediate name of the file/folder.
    /// </summary>
    public string localName { get; private set; }
    /// <summary>
    /// Queries the absolute path location.
    /// </summary>
    public string absolutePath {
      get {
        Stack<string> stack = new Stack<string>();
        stack.Push(localName);

        var top = parent;
        while (top != null && top.parent != null) {
          stack.Push(top.localName);
          top = top.parent;
        }

        StringBuilder sb = new StringBuilder();
        while (stack.Count > 1) {
          sb.Append(stack.Pop()).Append(delimiter);
        }

        sb.Append(stack.Pop());

        return sb.ToString();
      }
    }
    /// <summary>
    /// Queries whether or not the path is relative.
    /// </summary>
    public bool isRelative { get; private set; }

    /// <summary>
    /// The delimiter between path parts.
    /// </summary>
    public string delimiter { get; private set; }

    /// <summary>
    /// Creates a new path that is considered the hierarchal root of a file
    /// system.
    /// </summary>
    /// <param name="localName"></param>
    /// <param name="delimiter"></param>
    public Path(string delimiter = "/") {
      this.localName = "";
      this.delimiter = delimiter;
      this.isRelative = false;
    }

    /// <summary>
    /// Creates a new Path that is hierarchically below the provided path.
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="localName"></param>
    public Path(Path parent, string localName) {
      this.parent = parent;
      this.localName = localName;
      this.delimiter = parent.delimiter;
      this.isRelative = parent.isRelative;
    }

    /// <summary>
    /// Creates a new path with this path as its parent.
    /// </summary>
    /// <param name="localName"></param>
    /// <returns></returns>
    public Path To(string localName) {
      return new Path(this, localName);
    }

    public override string ToString() {
      return "Path {" + absolutePath + "}";
    }

    /// <summary>
    /// Creates a new path from the given raw path.
    /// </summary>
    /// <param name="rawPath"></param>
    /// <param name="delimiter"></param>
    /// <returns></returns>
    public static Path FromString(string rawPath, string delimiter="/") {
      var parts = Regex.Split(rawPath, delimiter);

      // TODO ahodder@appioninc.com: Erm... This won't work on a win-suck machine with all dat c:\ bullshit
      Path path = new Path(delimiter);
      for (int i = 0; i < parts.Length - 2; i++) {
        path = new Path(path, parts[i]);
      }

      return new Path(path, parts[parts.Length - 1]);
    }
  }
}
