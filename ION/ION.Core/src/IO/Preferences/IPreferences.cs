using System;
using System.Threading.Tasks;

namespace ION.Core.IO.Preferences {
  /// <summary>
  /// An abstraction that defines common access to preferences in storage.
  /// </summary>
  public interface IPreferences : IDisposable {
    /// <summary>
    /// Queries a bool from the preferences.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    bool GetBool(string key, bool defaultValue = false);
    /// <summary>
    /// Queries a 32-bit signed integer from the preferences.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    int GetInt(string key, int defaultValue = 0);
    /// <summary>
    /// Queries a 64-bit signed integer from the preferences.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    long GetLong(string key, long defaultValue = 0);
    /// <summary>
    /// Queries a 32-bit floating point value from the preferences.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    float GetFloat(string key, float defaultValue = 0f);
    /// <summary>
    /// Queries a 64-bit double floating point value from the preferences.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    double GetDouble(string key, double defaultValue = 0);
    /// <summary>
    /// Queries a unicode string from the preference.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    string GetString(string key, string defaultValue = "");

    /// <summary>
    /// Sets a bool to preferences.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    void SetBool(string key, bool value);
    /// <summary>
    /// Sets a 32-bit integer to preferences.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    void SetInt(string key, int value);
    /// <summary>
    /// Sets a 64-bit integer to preferences.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    void SetLong(string key, long value);
    /// <summary>
    /// Sets a 32-bit float to preferences.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    void SetFloat(string key, float value);
    /// <summary>
    /// Sets a 64-bit float to preferences.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    void SetDouble(string key, double value);
    /// <summary>
    /// Sets a string to preferences.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    void SetString(string key, string value);

    /// <summary>
    /// Removes a preference value from the preferences.
    /// </summary>
    /// <param name="key"></param>
    void Remove(string key);

    /// <summary>
    /// Commits all changes to the preferences to storage.
    /// </summary>
    Task<bool> Commit();
  }
}
