namespace ION.Core.Fluids {

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
    private const string FLUID_COLORS_FILE = "assets/refrigerantcolors.properties";
    /// <summary>
    /// The preference key that is used to retrieve the last used fluid for the fluid manager.
    /// </summary>
    private const string KEY_LAST_USED_FLUID = "last_used_fluid";
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
    /// The absolute default fluid for the fluid manager.
    /// </summary>
    private const string DEFAULT_FLUID = "R22";
    /// <summary>
    /// The default preferred fluids.
    /// </summary>
    private const string DEFAULT_FLUIDS = "R22,R134A,R407C,R410A";
    /// <summary>
    /// The default fluid color.
    /// </summary>
    private const int DEFAULT_FLUID_COLOR = unchecked((int)0xffd6d6d4);

    // Overridden from IFluidManager
    public event OnFluidPreferenceChanged onFluidPreferenceChanged;

    // Overridden from IFliudManager
    public Fluid lastUsedFluid { get; private set; }

    // Overridden from IFluidManager
    public List<string> preferredFluids {
      get {
        return new List<string>(__preferredFluids);
      }
      private set {
        __preferredFluids = value;
      }
    } List<string> __preferredFluids;
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
    private readonly Dictionary<string, WeakReference> __cache = new Dictionary<string, WeakReference>();

    public BaseFluidManager(IION ion) {
      this.ion = ion;
      preferredFluids = new List<string>();
    }

    // Overridden from IFluidManager
    public async Task<InitializationResult> InitAsync() {
      try {
        var dir = ion.fileManager.GetApplicationInternalDirectory();
        preferences = await BasePreferences.OpenAsync(dir.GetFile(PREFERENCE_FILE, EFileAccessResponse.CreateIfMissing));
        var assetsDir = ion.fileManager.GetAssetDirectory();
        var propsFile = assetsDir.GetFile(FLUID_COLORS_FILE, EFileAccessResponse.FailIfMissing);
        fluidColors = await Properties.FromFileAsync(propsFile);

        preferredFluids = new List<string>();
        var preferred = preferences.GetString(KEY_PREFERRED_FLUIDS, DEFAULT_FLUIDS);
        if (preferred != null) {
          var parts = preferred.Split(',');
          if (parts.Length > 0) {
            preferredFluids = parts.ToList();
          }
        }

        var fluidName = preferences.GetString(KEY_LAST_USED_FLUID, DEFAULT_FLUID);
        if (fluidName == null) {
          var fluidNames = GetAvailableFluidNames();
          if (fluidNames.Count > 0) {
            fluidName = fluidNames[0];
          }
        }
        await GetFluidAsync(fluidName);
        return new InitializationResult() { success = true };
      } catch (Exception e) {
        return new InitializationResult() {
          success = false,
          errorMessage = "Failed to initialize fluid manager: " + e.Message
        };
      }
    }

    // Overridden from IFluidManager
    public void Dispose() {
      __cache.Clear();
    }

    // Overridden from IFluidManager
    public List<string> GetAvailableFluidNames() {
      var dir = GetRootDir();

      var ret = new List<string>();
      foreach (IFile file in dir.GetFileList()) {
        var fluidName = Regex.Replace(file.name, "\\" + EXT_FLUID, "");
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

      if (ret == null && HasFluid(fluidName)) {
        ret = await LoadFluidAsync(fluidName);
        __cache.Add(fluidName, new WeakReference(ret));
      }

      lastUsedFluid = ret;
      preferences.SetString(KEY_LAST_USED_FLUID, fluidName);
      await preferences.Commit();

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
      if (fluidName == null || fluidName.Equals("")) {
        return;
      }

      if (preferred) {
        if (!__preferredFluids.Contains(fluidName)) {
          __preferredFluids.Add(fluidName);
        }
      } else {
        __preferredFluids.Remove(fluidName);
      }

      if (onFluidPreferenceChanged != null) {
        onFluidPreferenceChanged(this, fluidName);
      }

      CommitPreferredFluids();
    }

    /// <summary>
    /// Queries whether or not the given fluid is available for loading.
    /// </summary>
    /// <returns><c>true</c> if this instance has fluid the specified fluidName; otherwise, <c>false</c>.</returns>
    /// <param name="fluidName">Fluid name.</param>
    private bool HasFluid(string fluidName) {
      foreach (var fn in GetAvailableFluidNames()) {
        if (Regex.Replace(fn, "\\" + EXT_FLUID, "").Equals(fluidName)) {
          return true;
        }
      }

      return false;
    }

    /// <summary>
    /// Loads a fluid from the file system.
    /// </summary>
    /// <param name="fluidName"></param>
    /// <returns></returns>
    private async Task<Fluid> LoadFluidAsync(string fluidName) {
      fluidName = fluidName + EXT_FLUID;
      var dir = GetRootDir();

      if (!dir.ContainsFile(fluidName)) {
        throw new FileNotFoundException(dir.fullPath + fluidName);
      }

      var file = dir.GetFile(fluidName);

      var ret = new BinaryFluidParser().ParseFluid(file.OpenForReading());
      ret.color = GetFluidColor(ret.name);
      return ret;
    }

    /// <summary>
    /// Queries the root directory for the FluidManager.
    /// </summary>
    /// <returns></returns>
    private IFolder GetRootDir() {
      var fm = ion.fileManager;
      var dir = fm.GetAssetDirectory();
      return dir.GetFolder(PATH_FLUIDS);
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

        preferences.SetString(KEY_PREFERRED_FLUIDS, sb.ToString());
        preferences.Commit();
      }
    }
  }
}
