using System.Collections.Generic;
using System.Threading.Tasks;

namespace ION.Core.Fluids {
  /// <summary>
  /// The contract for managing a collection of fluids.
  /// </summary>
  public interface IFluidManager {
    /// <summary>
    /// Queries a list of the identifiers(name) of the fluids that are available
    /// from the fluid manager.
    /// </summary>
    /// <returns></returns>
    Task<List<string>> GetAvailableFluidNamesAsync();

    /// <summary>
    /// Queries a fluid with the given name.
    /// </summary>
    /// <param name="fluidName"></param>
    /// <returns></returns>
    Task<Fluid> GetFluidAsync(string fluidName);

    /// <summary>
    /// Marks whether or not the the named fluid is a preferred fluid. Preferred
    /// fluids are easier for the user to interact with and are generally
    /// preloaded.
    /// </summary>
    /// <param name="fluidName"></param>
    void MarkFluidAsPreferredAsync(string fluidName, bool preferred=true);

    /// <summary>
    /// Queries a list of the application's preferred fluids.
    /// </summary>
    /// <returns></returns>
    Task<List<Fluid>> GetPreferredFluidsAsync();
  }
}
