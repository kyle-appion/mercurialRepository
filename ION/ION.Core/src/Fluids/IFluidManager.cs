using System.Collections.Generic;
using System.Threading.Tasks;

using ION.Core.App;

namespace ION.Core.Fluids {
  /// <summary>
  /// Called when a fluid manager's fluid preferences change (ie. when a fluid
  /// is [un]marked preferred).
  /// </summary>
  public delegate void OnFluidPreferenceChanged(IFluidManager fluidManager, string fluidName);
  /// <summary>
  /// The contract for managing a collection of fluids.
  /// </summary>
  public interface IFluidManager : IIONManager {

    /// <summary>
    /// The event handler that will be notified when the preferred fluids list changes.
    /// </summary>
    event OnFluidPreferenceChanged onFluidPreferenceChanged;

    /// <summary>
    /// The last used fluid. Used for quick fluid defaults.
    /// </summary>
    /// <value>The last used fluid.</value>
    Fluid lastUsedFluid { get; }

    /// <summary>
    /// Queries the list of preferred fluids.
    /// </summary>
    /// <value>The preferred fluids.</value>
    List<string> preferredFluids { get; }

    /// <summary>
    /// Queries a list of the identifiers(name) of the fluids that are available
    /// from the fluid manager.
    /// </summary>
    /// <returns></returns>
    List<string> GetAvailableFluidNames();

    /// <summary>
    /// Queries a fluid with the given name.
    /// </summary>
    /// <param name="fluidName"></param>
    /// <returns></returns>
    Task<Fluid> GetFluidAsync(string fluidName);

    /// <summary>
    /// Queries whether or not the given fluid is preferred.
    /// </summary>
    /// <returns><c>true</c> if this instance is fluid preferred the specified fluidName; otherwise, <c>false</c>.</returns>
    /// <param name="fluidName">Fluid name.</param>
    bool IsFluidPreferred(string fluidName);

    /// <summary>
    /// Queries the ARGB8888 color for the given fluid. If the fluid does not
    /// exist, or does not have a color, then a default color will be returned.
    /// </summary>
    /// <returns>The fluid color.</returns>
    /// <param name="fluidName">Fluid name.</param>
    int GetFluidColor(string fluidName);

		/// <summary>
		/// Queries the safety of the fluid with the given name.
		/// </summary>
		/// <returns>The fluid safety.</returns>
		/// <param name="fluidName">Fluid name.</param>
		Fluid.ESafety GetFluidSafety(string fluidName);

    /// <summary>
    /// Marks whether or not the the named fluid is a preferred fluid. Preferred
    /// fluids are easier for the user to interact with and are generally
    /// preloaded.
    /// </summary>
    /// <param name="fluidName"></param>
    void MarkFluidAsPreferred(string fluidName, bool preferred=true);
  }
}
