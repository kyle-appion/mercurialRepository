namespace ION.Core.Fluids {

  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Text.RegularExpressions;
  using System.Threading.Tasks;

  using Appion.Commons.Util;

  using ION.Core.App;
  using ION.Core.Fluids.Parser;
  using ION.Core.IO;
  using ION.Core.IO.Preferences;

  /// <summary>
  /// A simple cross-platform implementation of a fluid manager.
  /// </summary>
  public class BaseFluidManager : IFluidManager {
    /// <summary>
    /// The asset file path for the refrigerant colors.
    /// </summary>
    private const string FLUID_COLORS_FILE = "refrigerantcolors.properties";
    /// <summary>
    /// The asset file path for the refrigerant safety.
    /// </summary>
    private const string FLUID_SAFETY_FILE = "refrigerantsafety.properties";
    /// <summary>
    /// The name of the asset directory that fluids are contained.
    /// </summary>
    private const string PATH_FLUIDS = "fluids";
    /// <summary>
    /// The extension that is used to identify a fluid.
    /// </summary>
    private const string EXT_FLUID = ".fluid";
    /// <summary>
    /// The absolute default fluid for the fluid manager.
    /// </summary>
    private const string DEFAULT_FLUID = "R22";
    /// <summary>
    /// The default preferred fluids.
    /// </summary>
    private static string[] DEFAULT_FLUIDS = new string[] { "R22", "R134a", "R407C", "R410A" };
    /// <summary>
    /// The default fluid color.
    /// </summary>
    private const int DEFAULT_FLUID_COLOR = unchecked((int)0xffd6d6d4);

    // Overridden from IFluidManager
    public event OnFluidPreferenceChanged onFluidPreferenceChanged;

    public bool isInitialized { get { return __isInitialized; } } bool __isInitialized;

    // Overridden from IFliudManager
    public Fluid lastUsedFluid { get; set; }

    // Overridden from IFluidManager
    public HashSet<string> preferredFluids {
      get {
        return new HashSet<string>(__preferredFluids);
      }
    } HashSet<string> __preferredFluids;
    /// <summary>
    /// The backing ION context for the fluid manager.
    /// </summary>
    public IION ion { get; private set; }
    /// <summary>
    /// The properties that contains the known fluid colors.
    /// </summary>
    private Properties fluidColors;
    /// <summary>
    /// The properties that contains the fluid safeties.
    /// </summary>
    private Properties fluidSafety;
    /// <summary>
    /// A lookup cache that will allow us to store commonly used fluids.
    /// </summary>
    private readonly Dictionary<string, WeakReference> __cache = new Dictionary<string, WeakReference>();

    public BaseFluidManager(IION ion) {
      this.ion = ion;
    }

    // Overridden from IFluidManager
    public async Task<InitializationResult> InitAsync() {
      try {
        var dir = ion.fileManager.GetApplicationInternalDirectory();
        var propStream = EmbeddedResource.Load(FLUID_COLORS_FILE);
        fluidColors = await Properties.FromStreamAsync(propStream);

        var fsStream = EmbeddedResource.Load(FLUID_SAFETY_FILE);
        fluidSafety = await Properties.FromStreamAsync(fsStream);

        var favoriteFluids = ion.preferences.fluid.favorites;
        if (favoriteFluids == null) {
          ion.preferences.fluid.favorites = favoriteFluids = DEFAULT_FLUIDS;
        }

        __preferredFluids = new HashSet<string>(favoriteFluids);

        var fluidName = ion.preferences.fluid.preferredFluid;
        if (fluidName == null) {
          fluidName = DEFAULT_FLUID;
        }

        await GetFluidAsync(fluidName);
        return new InitializationResult() { success = __isInitialized = true };
      } catch (Exception e) {
        Log.E(this, "Failed to init " + this, e);
        return new InitializationResult() {
          success = __isInitialized = false,
          errorMessage = "Failed to initialize fluid manager: " + e.Message
        };
      }
    }

    // Implemented for IManager
    public void PostInit() {
    }

    // Overridden from IFluidManager
    public void Dispose() {
      __cache.Clear();
      onFluidPreferenceChanged = null;
    }

    // Overridden from IFluidManager
    public HashSet<string> GetAvailableFluidNames() {
      var ret = new HashSet<string>();

      foreach (var filename in EmbeddedResource.GetResourcesOfExtension(EXT_FLUID)) {
        if (!FLUID_COLORS_FILE.Equals(filename)) {
          var fluidName = Regex.Replace(filename, "\\" + EXT_FLUID, "");
          ret.Add(fluidName);
        }
      }

      return ret;
    }

    // Overridden from IFluidManager
    public async Task<Fluid> GetFluidAsync(string fluidName) {
      Fluid ret = await LoadFluidAsync(fluidName);

      if (ret == null) {
        Log.D(this, "or not...");
        return null;
      }

      lastUsedFluid = ret;
      ion.preferences.fluid.preferredFluid = fluidName;

      return ret;
    }

    // Implemented from IFluidManager
    public async Task<Fluid> LoadFluidAsync(string fluidName) {
        Fluid ret = null;

        if (__cache.ContainsKey(fluidName)) {
          var reference = __cache[fluidName];
          if (!reference.IsAlive) {
            __cache.Remove(fluidName);
          } else {
            ret = reference.Target as Fluid;
          }
        }

        if (ret == null && HasFluid(fluidName)) {
          ret = new BinaryFluidParser().ParseFluid(EmbeddedResource.Load(fluidName + EXT_FLUID));
          ret.color = GetFluidColor(ret.name);
          ret.safety = GetFluidSafety(ret.name);
          __cache.Add(fluidName, new WeakReference(ret));
          return ret;
        } else {
          return ret;
        }
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
    public Fluid.ESafety GetFluidSafety(string fluidName) {
      var safety = fluidSafety[fluidName];
      if (safety == null) {
        return Fluid.ESafety.Unknown;
      } else {
        var ret = Fluid.ESafety.Unknown;
        Enum.TryParse(safety, out ret);
        return ret;
      }
    }

    // Overridden from IFluidManager
    public void MarkFluidAsPreferred(string fluidName, bool preferred) {
      if (fluidName == null || fluidName.Equals("")) {
        return;
      }

      if (preferred) {
				__preferredFluids.Add(fluidName);
      } else {
				__preferredFluids.Remove(fluidName);
			}

      ion.preferences.fluid.favorites = __preferredFluids.ToArray();

      if (onFluidPreferenceChanged != null) {
        onFluidPreferenceChanged(this, fluidName);
      }
    }

    /// <summary>
    /// Queries whether or not the given fluid is available for loading.
    /// </summary>
    /// <returns><c>true</c> if this instance has fluid the specified fluidName; otherwise, <c>false</c>.</returns>
    /// <param name="fluidName">Fluid name.</param>
    private bool HasFluid(string fluidName) {
      foreach (var fn in GetAvailableFluidNames()) {
        var exists = String.Compare(fn,fluidName,StringComparison.CurrentCultureIgnoreCase);
        if (exists.Equals(0)) { 
          return true;
        }
      }

      return false;
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
  }
}