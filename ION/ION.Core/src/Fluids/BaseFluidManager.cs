using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

using ION.Core.App;
using ION.Core.Fluids.Parser;
using ION.Core.IO;
using ION.Core.IO.Preferences;
using ION.Core.Util;

namespace ION.Core.Fluids {
  /// <summary>
  /// A simple cross-platform implementation of a fluid manager.
  /// </summary>
  public class BaseFluidManager : IFluidManager {
    /// <summary>
    /// The file name for the fluid manager's preferences.
    /// </summary>
    private const string PREFERENCE_FILE = "fluid_manager.preferences";
    /// <summary>
    /// The asset file path for the refrigerant colors.
    /// </summary>
    // TODO ahodder@appioninc.com: This was written just to get iOS working, and needs to be removed!
    // This is stupid and unsafe as it is not friendly for other platforms.
    private const string FLUID_COLORS_FILE = "assets/refrigerantcolors.properties";
    /// <summary>
    /// The key that is used to retrieve the preferred fluids from preferences.
    /// </summary>
    private const string KEY_PREFERRED_FLUIDS = "preferred_fluids";
    /// <summary>
    /// The name of the asset directory that fluids are contained.
    /// </summary>
    private const string PATH_FLUIDS = "fluids";
    /// <summary>
    /// The extension that is used to identify a fluid.
    /// </summary>
    private const string EXT_FLUID = ".dat";
    /// <summary>
    /// The default fluid color.
    /// </summary>
    private const int DEFAULT_FLUID_COLOR = unchecked((int)0xffd6d6d4);

    // Overridden from IFluidManager
    public event OnFluidPreferenceChanged onFluidPreferenceChanged;

    // Overridden from IFluidManager
    public List<string> preferredFluids { get; private set; }
    /// <summary>
    /// The backing ION context for the fluid manager.
    /// </summary>
    public IION ion { get; private set; }

    /// <summary>
    /// The preference for the fluid manager.
    /// </summary>
    private IPreferences preferences;
    /// <summary>
    /// The properties that contains the known fluid colors.
    /// </summary>
    private Properties fluidColors;
    /// <summary>
    /// A lookup cache that will allow us to store commonly used fluids.
    /// </summary>
    private Dictionary<string, WeakReference> __cache = new Dictionary<string, WeakReference>();

    public BaseFluidManager(IION ion) {
      this.ion = ion;
    }

    // Overridden from IFluidManager
    public async Task<List<string>> GetAvailableFluidNamesAsync() {
      Log.D(this, "Looking for all fluids");
      var dir = await GetRootDir();

      var ret = new List<string>();
      foreach (IFile file in await dir.GetFileListAsync()) {
        var fluidName = Regex.Replace(file.name, "\\" + EXT_FLUID, "");
        Log.D(this, "Found fluid: " + fluidName);
        ret.Add(fluidName);
      }

      return ret;
    }

    // Overridden from IFluidManager
    public async Task<Fluid> GetFluidAsync(string fluidName) {
      Fluid ret = null;

      if (__cache.ContainsKey(fluidName)) {
        var reference = __cache[fluidName];
        ret = reference.Target as Fluid;
      }

      if (ret == null) {
        ret = await LoadFluidAsync(fluidName);
        __cache.Add(fluidName, new WeakReference(ret));
      }

      return ret;
    }

    // Overridden from IFluidManager
    public bool IsFluidPreferred(string fluidName) {
      return preferredFluids.Contains(fluidName);
    }

    // Overridden from IFluidManager
    public int GetFluidColor(string fluidName) {
      var rawColor = fluidColors[fluidName.ToLower()];
      if (rawColor == null) {
        return DEFAULT_FLUID_COLOR;
      } else {
        try {
          // Ensure that alpha is 100%; the properties file doesn't need to declare the alpha
          return unchecked((int)(0xff000000) | int.Parse(rawColor, System.Globalization.NumberStyles.HexNumber));
        } catch (Exception e) {
          Log.E(this, "Fluid color properties contains an invalid value for key: " + fluidName, e);
          return DEFAULT_FLUID_COLOR;
        }
      }
    }

    // Overridden from IFluidManager
    public void MarkFluidAsPreferred(string fluidName, bool preferred) {
      if (preferred) {
        if (fluidName != null && !fluidName.Equals("") && !preferredFluids.Contains(fluidName)) {
          preferredFluids.Add(fluidName);
        }
      } else {
        if (preferredFluids.Contains(fluidName)) {
          preferredFluids.Remove(fluidName);
        }
      }

      if (onFluidPreferenceChanged != null) {
        onFluidPreferenceChanged(this, fluidName);
      }

      CommitPreferredFluids();
    }

    /// <summary>
    /// Initializes the BaseFluidManager.
    /// </summary>
    public async Task Init() {
      try {
        var dir = await ion.fileManager.GetApplicationInternalDirectoryAsync();
        preferences = await BasePreferences.OpenAsync(await dir.GetFileAsync(PREFERENCE_FILE, EFileAccessResponse.CreateIfMissing));
        var assetsDir = await ion.fileManager.GetAssetDirectoryAsync();
        var propsFile = await assetsDir.GetFileAsync(FLUID_COLORS_FILE, EFileAccessResponse.FailIfMissing);
        fluidColors = await Properties.FromFileAsync(propsFile);
        var preferred = preferences.GetString(KEY_PREFERRED_FLUIDS);
        preferredFluids = preferred.Split(',').ToList();
      } catch (Exception e) { 
        Log.E(this, "Failed to initialize fluid manager", e);
      }

//      await GetAvailableFluidNamesAsync();
    }

    /// <summary>
    /// Loads a fluid from the file system.
    /// </summary>
    /// <param name="fluidName"></param>
    /// <returns></returns>
    private async Task<Fluid> LoadFluidAsync(string fluidName) {
      fluidName = fluidName + EXT_FLUID;
      var dir = await GetRootDir();

      if (!await dir.ContainsFileAsync(fluidName)) {
        throw new FileNotFoundException(dir.fullPath + fluidName);
      }

      var file = await dir.GetFileAsync(fluidName);

      var ret = new BinaryFluidParser().ParseFluid(await file.OpenForReadingAsync());
      ret.color = GetFluidColor(ret.name);
      return ret;
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

    /// <summary>
    /// Commits the preferred fluids for the manager.
    /// </summary>
    private void CommitPreferredFluids() {
      if (preferredFluids.Count > 0) {
        var len = preferredFluids.Count;
        var sb = new StringBuilder();
        for (int i = 0; i < len - 1; i++) {
          sb.Append(preferredFluids[i]).Append(",");
        }
        sb.Append(preferredFluids[len - 1]);

        Log.D(this, "Saving preferred fluids as: " + sb);
        preferences.SetString(KEY_PREFERRED_FLUIDS, sb.ToString());
        preferences.Commit();
      }
    }
  }
}
