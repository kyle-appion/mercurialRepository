namespace ION.Core.App {

	using System;
	using System.Threading.Tasks;

  public interface IManager : IDisposable {
    /// <summary>
    /// Whether or not the manager is initialized.
    /// </summary>
    /// <value>The is initialized.</value>
    bool isInitialized { get; }

    /// <summary>
    /// Initializes the IIONManager to the given IION.
    /// </summary>
    Task<InitializationResult> InitAsync();
  }

  /// <summary>
  /// The class that wraps the initialization result of an IONManager.
  /// </summary>
  public class InitializationResult {
    /// <summary>
    /// True if the manager was initialized properly, false otherwise.
    /// </summary>
    /// <value><c>true</c> if success; otherwise, <c>false</c>.</value>
    public bool success { get; set; }
    /// <summary>
    /// If success is false, then the result will be accompanied with this string.
    /// </summary>
    /// <value>The error message.</value>
    public string errorMessage { get; set; }
  }
}

