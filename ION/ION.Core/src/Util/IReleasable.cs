using System;

namespace ION.Core.Util {
  /// <summary>
  /// An interface that is intended to release an object from event handlers.
  /// </summary>
  public interface IReleasable {
    void Release();
  }
}

