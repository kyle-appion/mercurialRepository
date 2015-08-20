using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using ION.Core.IO;
using ION.Core.Util;

namespace ION.Core.IO.Preferences {
  /// <summary>
  /// A Preference is an entity that wraps the storage of common ION assets,
  /// such as application preferences or persistent application state keys, in
  /// an easy to use accessor.
  /// </summary>
  public class BasePreferences : IPreferences {
    /// <summary>
    /// The current version of the preference serialization.
    /// </summary>
    private const int CURRENT_VERSION = 1;

    /// <summary>
    /// The file that the preferences have been written out to.
    /// </summary>
    private readonly IFile __file;
    /// <summary>
    /// The content of the preferences.
    /// </summary>
    private readonly Dictionary<string, object> __content;


    // Creates a new BasePreferences object.
    public BasePreferences(IFile file) : this(file, new Dictionary<string, object>()) {
      // Nope
    }

    /// <summary>
    /// Creates a new preference wrapping the file.
    /// </summary>
    /// <param name="file"></param>
    private BasePreferences(IFile file, Dictionary<string, object> content) {
      __file = file;
      __content = content;
    }

    // Overridden from IPreference
    public void Dispose() {
      __content.Clear();
    }

    // Overridden from IPreference
    public bool GetBool(string key, bool defaultValue = false) {
      if (!__content.ContainsKey(key)) {
        return defaultValue;
      } else {
        return (bool)__content[key];
      }
    }

    // Overridden from IPreference
    public int GetInt(string key, int defaultValue = 0) {
      if (!__content.ContainsKey(key)) {
        return defaultValue;
      } else {
        return (int)__content[key];
      }
    }

    // Overridden from IPreference
    public long GetLong(string key, long defaultValue = 0) {
      if (!__content.ContainsKey(key)) {
        return defaultValue;
      } else {
        return (long)__content[key];
      }
    }

    // Overridden from IPreference
    public float GetFloat(string key, float defaultValue = 0) {
      if (!__content.ContainsKey(key)) {
        return defaultValue;
      } else {
        return (float)__content[key];
      }
    }

    // Overridden from IPreference
    public double GetDouble(string key, double defaultValue = 0) {
      if (!__content.ContainsKey(key)) {
        return defaultValue;
      } else {
        return (double)__content[key];
      }
    }

    // Overridden from IPreference
    public string GetString(string key, string defaultValue = "") {
      if (!__content.ContainsKey(key)) {
        return defaultValue;
      } else {
        return (string)__content[key];
      }
    }

    // Overridden from IPreferences
    public void SetBool(string key, bool value) {
      __content[key] = value;
    }

    // Overridden from IPreferences
    public void SetInt(string key, int value) {
      __content[key] = value;
    }

    // Overridden from IPreferences
    public void SetLong(string key, long value) {
      __content[key] = value;
    }

    // Overridden from IPreferences
    public void SetFloat(string key, float value) {
      __content[key] = value;
    }

    // Overridden from IPreferences
    public void SetDouble(string key, double value) {
      __content[key] = value;
    }

    // Overridden from IPreferences
    public void SetString(string key, string value) {
      __content[key] = value;
    }

    // Overridden from IPreferences
    public void Remove(string key) {
      __content.Remove(key);
    }

    // Overridden from IPreferences
    public Task<bool> Commit() {
      return Task.Run(async () => {
        try {
          using (BinaryWriter writer = new BinaryWriter(await __file.OpenForWritingAsync())) {
            // Write the preference serialization version
            writer.Write(CURRENT_VERSION);
            // Write the number of items that we will be persiting
            writer.Write(__content.Count);

            foreach (string key in __content.Keys) {
              object value = __content[key];

              if (value is bool) {
                writer.Write((int)Type.Bool);
                writer.Write((bool)value);
              } else if (value is int) {
                writer.Write((int)Type.I32);
                writer.Write((int)value);
              } else if (value is long) {
                writer.Write((int)Type.I64);
                writer.Write((long)value);
              } else if (value is float) {
                writer.Write((int)Type.F32);
                writer.Write((float)value);
              } else if (value is double) {
                writer.Write((int)Type.F64);
                writer.Write((double)value);
              } else if (value is string) {
                writer.Write((int)Type.String);
                var str = (string)value;
                writer.Write(str.Length);
                writer.Write(str.ToCharArray());
              } else {
                throw new IOException("Cannot commit preferences: type " + value.GetType().Name + " is not allowed");
              }
            }

            writer.Flush();
          }

          return true;
        } catch (Exception e) {
          Log.E(this, "Failed to persist preferences", e);
          return false;
        }
      });
    }

    /// <summary>
    /// Opens a BasePreferences object from the given file.
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    public static Task<BasePreferences> OpenAsync(IFile file) {
      return Task.Run(async () => {
        if (await file.GetSizeAsync() <= 0) {
          Log.D(typeof(BasePreferences).Name, "File returned size as empty. Creating new preference file");
          return new BasePreferences(file);
        } else {
          var content = new Dictionary<string, object>();

          using (var reader = new BinaryReader(await file.OpenForReadingAsync())) {
            // Read the preferene file version
            var version = reader.ReadInt32();
            if (CURRENT_VERSION != version) {
              throw new IOException("Cannot open preferences: invalid version " + version);
            }
            // Read how many items are stored in the preference file.
            var items = reader.ReadInt32();

            // Read all of the preferences
            for (int i = 0; i < items; i++) {
              // Read the preference key
              var key = new string(reader.ReadChars(reader.ReadInt32()));
              Type type = (Type)reader.ReadByte();

              // Read the value
              object value;
              switch (type) {
                case Type.Bool: {
                  value = reader.ReadBoolean();
                  break;
                }
                case Type.I32: {
                  value = reader.ReadInt32();
                  break;
                }
                case Type.I64: {
                  value = reader.ReadInt64();
                  break;
                }
                case Type.F32: {
                  value = (float)reader.ReadDouble();
                  break;
                }
                case Type.F64: {
                  value = reader.ReadDouble();
                  break;
                }
                case Type.String: {
                  value = new string(reader.ReadChars(reader.ReadInt32()));
                  break;
                }
                default: {
                  throw new IOException("Cannot read preferences: unknown type " + type);
                }
              }

              content.Add(key, value);
            }
          }

          return new BasePreferences(file, content);
        }
      });
    }

    /// <summary>
    /// Enumerates the types that are present within the 
    /// </summary>
    private enum Type {
      Bool,
      I32,
      I64,
      F32,
      F64,
      String,
    }
  }
}
