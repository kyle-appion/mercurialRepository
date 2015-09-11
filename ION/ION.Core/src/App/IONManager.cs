using System;
using System.Threading.Tasks;

namespace ION.Core.App {
  public interface IIONManager : IDisposable {
    /// <summary>
    /// Initializes the IIONManager to the given IION.
    /// </summary>
    Task InitAsync();
  }
}

