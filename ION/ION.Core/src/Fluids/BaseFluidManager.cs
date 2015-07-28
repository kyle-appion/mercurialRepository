using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

using ION.Core.App;
using ION.Core.Fluids.Parser;
using ION.Core.IO;
using ION.Core.Util;

namespace ION.Core.Fluids {
  /// <summary>
  /// A simple cross-platform implementation of a fluid manager.
  /// </summary>
  public class BaseFluidManager : IFluidManager {
    /// <summary>
    /// The name of the asset directory that fluids are contained.
    /// </summary>
    private const string PATH_FLUIDS = "fluids";
    /// <summary>
    /// The extension that is used to identify a fluid.
    /// </summary>
    private const string EXT_FLUID = ".dat";

    /// <summary>
    /// The backing ION context for the fluid manager.
    /// </summary>
    public IION ion { get; private set; }

    /// <summary>
    /// A lookup cache that will allow us to store commonly used fluids.
    /// </summary>
    private Dictionary<string, WeakReference> __cache = new Dictionary<string, WeakReference>();

    public BaseFluidManager(IION ion) {
      this.ion = ion;
    }

    // Overridden from IFluidManager
    public async Task<List<string>> GetAvailableFluidNamesAsync() {
      var dir = await GetRootDir();

      var ret = new List<string>();
      foreach (IFile file in await dir.GetFileListAsync()) {
        ret.Add(Regex.Replace(file.fullPath, "\\" + EXT_FLUID, ""));
      }

      return ret;
    }

    // Overridden from IFluidManager
    public async Task<Fluid> GetFluidAsync(string fluidName) {
      Fluid ret = null;

      if (__cache.ContainsKey(fluidName)) {
        var reference = __cache[fluidName];
        ret = (Fluid)reference.Target;
      }

      if (ret == null) {
        ret = await LoadFluidAsync(fluidName);
        __cache.Add(fluidName, new WeakReference(ret));
      }

      return ret;
    }

    // Overridden from IFluidManager
    public void MarkFluidAsPreferredAsync(string fluidName, bool preferred) {
      Log.D(this, "NOT marking '" + fluidName + "' as preferred");
    }

    // Overridden from IFluidManager
    public Task<List<Fluid>> GetPreferredFluidsAsync() {
      Log.D(this, "Return preferred fluids");
      return null;
    }

    /// <summary>
    /// Loads a fluid from the file system.
    /// </summary>
    /// <param name="fluidName"></param>
    /// <returns></returns>
    private async Task<Fluid> LoadFluidAsync(string fluidName) {
      fluidName = fluidName + EXT_FLUID;
      var dir = await GetRootDir();

      foreach (IFile f in await dir.GetFileListAsync()) {
        Log.D(this, f.fullPath);
      }

      if (!await dir.ContainsFileAsync(fluidName)) {
        throw new FileNotFoundException(dir.fullPath + fluidName);
      }

      var file = await dir.GetFileAsync(fluidName);

      return new BinaryFluidParser().ParseFluid(await file.OpenForReadingAsync());
    }

    /// <summary>
    /// Queries the root directory for the FluidManager.
    /// </summary>
    /// <returns></returns>
    private async Task<IFolder> GetRootDir() {
      var fm = ion.fileManager;
      var dir = await fm.GetAssetDirectoryAsync();
      return await dir.GetFolderAsync(PATH_FLUIDS);
    }
  }
}
