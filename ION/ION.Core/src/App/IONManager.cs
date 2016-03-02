﻿using System;
using System.Threading.Tasks;

namespace ION.Core.App {
  public interface IIONManager : IDisposable {
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

