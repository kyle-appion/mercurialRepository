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
    /// The file name for the fluid manager's preferences.
    /// </summary>
    private const string PREFERENCE_FILE = "fluid_manager.preferences";
    /// <summary>
    /// The asset file path for the refrigerant colors.
    /// </summary>
    private const string FLUID_COLORS_FILE = "refrigerantcolors.properties";
		/// <summary>
		/// The asset file path for the refrigerant safety.
		/// </summary>
		private const string FLUID_SAFETY_FILE = "refrigerantsafety.properties";
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
    private const string EXT_FLUID = ".fluid";
    /// <summary>
    /// The absolute default fluid for the fluid manager.
    /// </summary>
    private const string DEFAULT_FLUID = "R22";
    /// <summary>
    /// The default preferred fluids.
    /// </summary>
    private const string DEFAULT_FLUIDS = "R22,R134a,R407C,R410A";
    /// <summary>
    /// The default fluid color.
    /// </summary>
    private const int DEFAULT_FLUID_COLOR = unchecked((int)0xffd6d6d4);

    // Overridden from IFluidManager
    public event OnFluidPreferenceChanged onFluidPreferenceChanged;

    public bool isInitialized { get { return __isInitialized; } } bool __isInitialized;

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
		/// The properties that contains the fluid safeties.
		/// </summary>
		private Properties fluidSafety;
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
//        var names = GetAvailableFluidNames();
        var dir = ion.fileManager.GetApplicationInternalDirectory();
        preferences = await BasePreferences.OpenAsync(dir.GetFile(PREFERENCE_FILE, EFileAccessResponse.CreateIfMissing));
        var propStream = EmbeddedResource.Load(FLUID_COLORS_FILE);
        fluidColors = await Properties.FromStreamAsync(propStream);

				var fsStream = EmbeddedResource.Load(FLUID_SAFETY_FILE);
				fluidSafety = await Properties.FromStreamAsync(fsStream);

        preferredFluids = new List<string>();
        var preferred = preferences.GetString(KEY_PREFERRED_FLUIDS, DEFAULT_FLUIDS);
        if (preferred != null) {
          var parts = preferred.Split(',');
          if (parts.Length > 0) {
            preferredFluids = parts.ToList();
          }
        }

				preferredFluids.Sort(new AlphabeticalStringComparer());

        var fluidName = preferences.GetString(KEY_LAST_USED_FLUID, DEFAULT_FLUID);
        if (fluidName == null) {
          var fluidNames = GetAvailableFluidNames();
          if (fluidNames.Count > 0) {
            fluidName = fluidNames[0];
          }
        }

				// TODO ahodder@appioninc.com: Fix initialize app when a fluid is missing
				// I didn't have time at the moment to fix this, so here is the trace from the moment. This will only occur when
				// the user has a fluid saved as the last used or in the perferred that was renamed or removed.
/*
System.IO.IOException: Could not load resource of name R601A.fluid: doesn't exist
E    at ION.Core.IO.EmbeddedResource.Load (System.Reflection.Assembly assembly, System.String filename) [0x0004e] in /Users/ahodder/Documents/code_space/ion_universe/ION/ION.Core/src/IO/EmbeddedResource.cs:34
E    at ION.Core.IO.EmbeddedResource.Load (System.String filename) [0x00016] in /Users/ahodder/Documents/code_space/ion_universe/ION/ION.Core/src/IO/EmbeddedResource.cs:20
E    at ION.Core.Fluids.BaseFluidManager.LoadFluidAsync (System.String fluidName) [0x00011] in /Users/ahodder/Documents/code_space/ion_universe/ION/ION.Core/src/Fluids/BaseFluidManager.cs:251
E    at ION.Core.Fluids.BaseFluidManager+<GetFluidAsync>c__async1.MoveNext () [0x000d0] in /Users/ahodder/Documents/code_space/ion_universe/ION/ION.Core/src/Fluids/BaseFluidManager.cs:173
E  --- End of stack trace from previous location where exception was thrown ---
E    at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw () [0x0000c] in /Users/builder/data/lanes/3819/96c7ba6c/source/mono/mcs/class/referencesource/mscorlib/system/runtime/exceptionservices/exceptionservicescommon.cs:143
E    at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess (System.Threading.Tasks.Task task) [0x00047] in /Users/builder/data/lanes/3819/96c7ba6c/source/mono/mcs/class/referencesource/mscorlib/system/runtime/compilerservices/TaskAwaiter.cs:187
E    at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification (System.Threading.Tasks.Task task) [0x0002e] in /Users/builder/data/lanes/3819/96c7ba6c/source/mono/mcs/class/referencesource/mscorlib/system/runtime/compilerservices/TaskAwaiter.cs:156
E    at System.Runtime.CompilerServices.TaskAwaiter.ValidateEnd (System.Threading.Tasks.Task task) [0x0000b] in /Users/builder/data/lanes/3819/96c7ba6c/source/mono/mcs/class/referencesource/mscorlib/system/runtime/compilerservices/TaskAwaiter.cs:128
E    at System.Runtime.CompilerServices.TaskAwaiter`1[TResult].GetResult () [0x00000] in /Users/builder/data/lanes/3819/96c7ba6c/source/mono/mcs/class/referencesource/mscorlib/system/runtime/compilerservices/TaskAwaiter.cs:357
E    at ION.Core.Fluids.BaseFluidManager+<InitAsync>c__async0.MoveNext () [0x0020b] in /Users/ahodder/Documents/code_space/ion_universe/ION/ION.Core/src/Fluids/BaseFluidManager.cs:126

*/
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

    // Overridden from IFluidManager
    public void Dispose() {
      __cache.Clear();
    }

    // Overridden from IFluidManager
    public List<string> GetAvailableFluidNames() {
      var ret = new List<string>();

      foreach (var filename in EmbeddedResource.GetResourcesOfExtension(EXT_FLUID)) {
        if (!FLUID_COLORS_FILE.Equals(filename)) {
          var fluidName = Regex.Replace(filename, "\\" + EXT_FLUID, "");
          ret.Add(fluidName);
        }
			}

			ret.Sort(new AlphabeticalStringComparer());

      return ret;
    }

    // Overridden from IFluidManager
    public async Task<Fluid> GetFluidAsync(string fluidName) {
      Fluid ret = null;

      if (__cache.ContainsKey(fluidName)) {
        var reference = __cache[fluidName];
        if (!reference.IsAlive) {
          Log.D(this, "But it was garbage collected so removing key");
          __cache.Remove(fluidName);
        } else {          
          ret = reference.Target as Fluid;
        }
      }

      if (ret == null && HasFluid(fluidName)) {
        ret = await LoadFluidAsync(fluidName);
        __cache.Add(fluidName, new WeakReference(ret));
      } else {
          Log.D(this, "or not...");
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
        var exists = String.Compare(fn,fluidName,StringComparison.CurrentCultureIgnoreCase);
        if (exists.Equals(0)) { 
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
    private Task<Fluid> LoadFluidAsync(string fluidName) {
      var ret = new BinaryFluidParser().ParseFluid(EmbeddedResource.Load(fluidName + EXT_FLUID));
      ret.color = GetFluidColor(ret.name);
			ret.safety = GetFluidSafety(ret.name);
      return Task.FromResult(ret);
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
