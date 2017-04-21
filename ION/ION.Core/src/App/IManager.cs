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
    /// Initializes the IIONManager to the given IION. This is where all of the initial loading of content and
    /// preparation for use will occur for the manager.
    /// Note: the manager will receive a call to PostInit one all managers have performed their InitAsync(), so delay
    /// any operations that depend on other managers or ion state until then.
    /// </summary>
    Task<InitializationResult> InitAsync();

    /// <summary>
    /// Called after the ION instance has initialized. This allows the manager to complete any further instantiation
    /// that depended on another manager being initialized.
    /// </summary>
    void PostInit();
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

