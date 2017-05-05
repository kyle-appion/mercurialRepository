namespace Appion.Commons.Util {

	using System;

  /// <summary>
  /// An interface that is intended to release an object from event handlers.
  /// </summary>
  public interface IReleasable {
    void Release();
  }
}

