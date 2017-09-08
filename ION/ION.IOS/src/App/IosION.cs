using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using CoreFoundation;
using Foundation;
using UIKit;

using Appion.Commons.Util;

using ION.Core.App;
using ION.Core.Alarms;
using ION.Core.Alarms.Alerts;
using ION.Core.Content;
using ION.Core.Content.Parsers;
using ION.Core.Database;
using ION.Core.Devices;
using ION.Core.Fluids;
using ION.Core.IO;
using ION.Core.Location;
using ION.Core.Report.DataLogs;

using ION.CoreExtensions.Net.Portal;

using ION.IOS.Alarms.Alerts;
using ION.IOS.IO;
using ION.IOS.Location;
using ION.IOS.Connections;
using ION.IOS.Net;

namespace ION.IOS.App {
  public abstract class IosION : IION {
    /// <summary>
    /// Gets the displayed application name.
    /// </summary>
    /// <returns>The display name.</returns>
    private static string GetDisplayName() {
      var info = NSBundle.MainBundle.InfoDictionary;
      var appDisplayName = info.ObjectForKey(new NSString("CFBundleDisplayName"));
      return ((NSString)appDisplayName).ToString();
    }
    
    /// <summary>
    /// Gets the current application version.
    /// </summary>
    /// <returns>The version.</returns>
    private static string GetVersion() {
      var info = NSBundle.MainBundle.InfoDictionary;
      var version = info.ObjectForKey(new NSString("CFBundleVersion"));
      return ((NSString)version).ToString();
    }
    
    /// <summary>
    /// The file name for the primary workbench.
    /// </summary>
    public const string FILE_WORKBENCH = "primaryWorkbench.workbench";
    /// <summary>
    /// The file name for the primary analyzer.
    /// </summary>
    public const string FILE_ANALYZER = "primaryAnalyzer.analyzer";
    
    // Implemented from IION
    public event OnWorkbenchChanged onWorkbenchChanged;
    // Implemented from IION
    public event OnAnalyzerChanged onAnalyzerChanged;

    // Implemented from IION
    public string name { get { return GetDisplayName(); } }
    // Implemented from IION
    public string version { get { return GetVersion(); } }
    // Implemented from IION
    public IIONPreferences preferences { get { return settings; } }

    // Implemented From IION
    public IONDatabase database { get; protected set; }
    // Implemented From IION
    public IFileManager fileManager { get; protected set; }
    // Implemented From IION
    public IDeviceManager deviceManager { get; protected set; }
    // Implemented From IION
    public IAlarmManager alarmManager { get; protected set; }
    // Implemented From IION
    public IFluidManager fluidManager { get; protected set; }
    // Implemented From IION
    public ILocationManager locationManager { get; protected set; }
    // Implemented From IION
    public DataLogManager dataLogManager { get; protected set; }
    // Implemented from IION
    public virtual bool hasNetworkConnection { 
      get {
        return Reachability.IsReachableWithoutRequiringConnection(SystemConfiguration.NetworkReachabilityFlags.Reachable);  
      }
    }
    
    /// <summary>
    /// The interface that will allow the ion instance to communicate with the Appion web services.
    /// </summary>
    public IONPortalService portal { get; private set; }
    
    /// <summary>
    /// The mapping to the type secured settings.
    /// </summary>
    /// <value>The settings.</value>
    public AppPrefs settings { get { return AppPrefs.Get(); } }

    // Implemented From IION
    public virtual Analyzer currentAnalyzer { 
      get {
        return __analyzer;
      }
      set {
        __analyzer = value;
        if (onAnalyzerChanged != null) {
          onAnalyzerChanged(__analyzer);
        }
      }
    } Analyzer __analyzer;
    
    // Implemented From IION
    public virtual Workbench currentWorkbench {
      get {
        return __workbench;
      }
      set {
        __workbench = value;
        if (onWorkbenchChanged != null) {
          onWorkbenchChanged(__workbench);
        }
      }
    } Workbench __workbench;

    // Implemented From IION
    public virtual IFolder screenshotReportFolder {
      get {
        var d = fileManager.GetApplicationExternalDirectory();
        d = d.GetFolder("reports", EFileAccessResponse.CreateIfMissing);
        d = d.GetFolder("screenshots", EFileAccessResponse.CreateIfMissing);
        return d;
      }
    }

    // Implemented From IION
    public virtual IFolder calibrationCertificateFolder {
      get {
        var d = fileManager.GetApplicationExternalDirectory();
        d = d.GetFolder("reports", EFileAccessResponse.CreateIfMissing);
        d = d.GetFolder("calibrationCertificates", EFileAccessResponse.CreateIfMissing);
        return d;
      }
    }
    
    /// <summary>
    /// The list of all the managers the are present in the ion instance.
    /// </summary>
    private List<IManager> _managers = new List<IManager>();
    
    /// <summary>
    /// Creates a new BaseION.
    /// </summary>
    /// <param name="name">Name.</param>
    public IosION(IONPortalService portal) {
      this.portal = portal;
    }
    
    // Implemented From IION
    public async Task<bool> InitAsync() {
      try {
        var _ = preferences.lastKnownAppVersion; // Set the current application version;
        
        OnCreateManagers();
        
				VerifyManagerOrThrow(fileManager);
        VerifyManagerOrThrow(database);
        VerifyManagerOrThrow(fluidManager);
        VerifyManagerOrThrow(locationManager);
        VerifyManagerOrThrow(alarmManager);
        VerifyManagerOrThrow(deviceManager);
        VerifyManagerOrThrow(dataLogManager);
        
        foreach (var m in _managers) {
          var managerName = m.GetType().Name;
          var result = await m.InitAsync();
          
          if (result == null) {
            throw new Exception("Manager {" + managerName + "} verification fail: failed to return result");
          } else if (!result.success) {
            throw new Exception("Manager {" + managerName + "} verification fail: init failed\n" + result.errorMessage);
          }
        }
        
        try {
          var w = await LoadWorkbenchAsync();
          currentWorkbench = w;
        } catch (Exception e) {
          Log.E(this, "Failed to load workbench", e);
        }
        
        try {
          var a = await LoadAnalyzerAsync();
          currentAnalyzer = a;
        } catch (Exception e) {
          Log.E(this, "Failed to load analyzer", e);
        }
        
        if (currentWorkbench == null) {
          currentWorkbench = new Workbench(this);
        }
        
        if (currentAnalyzer == null) {
          currentAnalyzer = new Analyzer(this);
        }
        
        foreach (var m in _managers) {
          m.PostInit();
        }
        
        if (!await OnPostInit()) {
          return false;
        }
        
        return true;
      } catch (Exception e) {
        Log.E(this, "Failed to initialize ion", e);
        return false;
      }
    }
    
    /// <summary>
    /// Called when the Ion instance has completed all necessary initialization and is ready to provide implementations
    /// time to initialize anything that may depend on previous managers.
    /// </summary>
    /// <returns>The post init.</returns>
    protected virtual Task<bool> OnPostInit() {
      return Task.FromResult(true);
    }
    
    /// <summary>
    /// Called by InitAsync when it is time for the implementation to create all of the managers.
    /// Note: do not initialize the manager, they will be initialized in InitAsync.
    /// </summary>
    /// <returns>The initialize managers async.</returns>
    protected abstract void OnCreateManagers();
    
    // Implemented from IION
    public virtual void Dispose() {
      var tmp = new List<IManager>();
      tmp.Reverse();

      foreach (var m in tmp) {
        try {
          m.Dispose();
        } catch (Exception e) {
          Log.E(this, "Failed to dispose of IIONManager: " + m.GetType().Name, e);
        }
      }
    }
    
    // Implemented from IION
    public void PostToMain(Action action) {
      NSOperationQueue.MainQueue.AddOperation(() => {
        try {
          action();
        } catch (Exception e) {
          Log.E(this, "Failed to execute action on main thread", e);
        }
      });
    }
    
    // Implemented from IION
    public void PostToMainDelayed(Action action, TimeSpan delay) {
      DispatchQueue.MainQueue.DispatchAfter(new DispatchTime(DispatchTime.Now, delay), action);
    }
    
    // Implemented from IION
    public virtual Task SaveWorkbenchAsync() {
//      return Task.Factory.StartNew(() => {
        var internalDirectory = fileManager.GetApplicationInternalDirectory();
        var file = internalDirectory.GetFile(FILE_WORKBENCH, EFileAccessResponse.CreateIfMissing);
        var parser = new WorkbenchParser();
        try {
          using (var s = file.OpenForWriting()) {
            parser.WriteToStream(this, currentWorkbench, s);
          }
        } catch (Exception e) {
          Log.E(this, "Failed to write workbench to file", e);
        }
//      });
        return Task.FromResult(false);
    }

    // Implemented from IION
    public virtual Task<Workbench> LoadWorkbenchAsync() {
      var internalDir = fileManager.GetApplicationInternalDirectory();
      if (internalDir.ContainsFile(FILE_WORKBENCH)) {
        var file = internalDir.GetFile(FILE_WORKBENCH);
        return LoadWorkbenchAsync(file);
      } else {
        return Task.FromResult(new Workbench(this));
      }
    }

    // Implemented from IION
    public virtual Task<Workbench> LoadWorkbenchAsync(IFile file) {
//      return Task.Factory.StartNew(() => {
        try {
          var parser = new WorkbenchParser();
          using (var s = file.OpenForReading()) {
            return Task.FromResult(parser.ReadFromStream(this, s));
          }
        } catch (Exception e) {
          Log.E(this, "Failed to load workbench. Defaulting to a new one.", e);
          return Task.FromResult(new Workbench(this));
        }
//      });
    }

    // Implemented from IION
    public virtual Task SaveAnalyzerAsync() {
//      return Task.Factory.StartNew(() => {
        var internalDirectory = fileManager.GetApplicationInternalDirectory();
        var file = internalDirectory.GetFile(FILE_ANALYZER, EFileAccessResponse.CreateIfMissing);
        var parser = new AnalyzerParser();
        try {
          using (var s = file.OpenForWriting()) {
            parser.WriteToStream(this, currentAnalyzer, s);
          }
        } catch (Exception e) {
          Log.E(this, "Failed to write analyzer to file", e);
        }
        return Task.FromResult(false);
//      });
    }

    // Implemented from IION
    public virtual Task<Analyzer> LoadAnalyzerAsync() {
      var internalDir = fileManager.GetApplicationInternalDirectory();
      if (internalDir.ContainsFile(FILE_ANALYZER)) {
        var file = internalDir.GetFile(FILE_ANALYZER);
        return LoadAnalyzerAsync(file);
      } else {
        return Task.FromResult(new Analyzer(this));
      }
    }

    // Implemented from IION
    public virtual Task<Analyzer> LoadAnalyzerAsync(IFile file) {
//      return Task.Factory.StartNew(() => {
        try {
          var parser = new AnalyzerParser();
          using (var s = file.OpenForReading()) {
            return Task.FromResult(parser.ReadFromStream(this, s));
          }
        } catch (Exception e) {
          Log.E(this, "Failed to load analyzer. Defaulting to a new one.", e);
          return Task.FromResult(new Analyzer(this));
        }
//      });
    }
    
    // Implemented from IION
    public virtual IAppDump CreateApplicationDump() {
      return new BaseAppDump(this);
    }
    // Implemented from IION
    public virtual IPlatformInfo GetPlatformInformation() {
      return new IOSPlatformInfo(this);
    }
    
    /// <summary>
    /// Verifies that the given manager is ready for use or throws an exception.
    /// </summary>
    /// <returns>The manager or throw.</returns>
    /// <param name="manager">Manager.</param>
    private void VerifyManagerOrThrow(IManager manager) {
      if (manager == null) {
        throw new Exception("Cannot verify manager: null");
      }

      var managerName = manager.GetType().Name;
      _managers.Add(manager);
    }
  }
}
