﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using Foundation;

using ION.Core.App;
using ION.Core.Content;
using ION.Core.Content.Parsers;
using ION.Core.Database;
using ION.Core.Devices;
using ION.Core.Fluids;
using ION.Core.IO;
using ION.Core.Util;

using ION.IOS.IO;
using ION.IOS.Devices;

namespace ION.IOS.App {
  /// <summary>
  /// The iOS ION implementation.
  /// </summary>
  public class IosION : IION {
    /// <summary>
    /// The file name for the primary workbench.
    /// </summary>
    public const string FILE_WORKBENCH = "primaryWorkbench.workbench";

    // Overridden from IION
    public IONDatabase database { get; set; }
    // Overridden from IION
    public ION.Core.IO.IFileManager fileManager { get; set; }
    // Overridden from IION
    public IDeviceManager deviceManager { get; set; }
    // Overridden from IION
    public IFluidManager fluidManager { get; set; }
    // Overridden from IION
    public Workbench currentWorkbench { get; set; }

    /// <summary>
    /// The list of managers that are present in the ion context.
    /// </summary>
    private readonly List<IIONManager> managers = new List<IIONManager>();

    public IosION() {
      // Order matters
      var path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ION.database");
      managers.Add(database = new IONDatabase(new SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS(), path, this));
      managers.Add(fileManager = new IosFileManager());
      managers.Add(deviceManager = new IOSDeviceManager(this));
      managers.Add(fluidManager = new BaseFluidManager(this));
    }

    // Overridden from IION
    public void Dispose() {
      if (deviceManager != null) {
        deviceManager.Dispose();
      }
    }

    // Overridden from IION
    public void PostToMain(Action action) {
      NSOperationQueue.MainQueue.AddOperation(() => {
        try {
          action();
        } catch (Exception e) {
          Log.E(this, "Failed to execute action on main thread", e);
        }
      });
    }

    // Overridden from IION
    public Task SaveWorkbenchAsync() {
      return Task.Factory.StartNew(() => {
        lock (this) {
          Log.D(this, "Saving workbench");
          var internalDir = fileManager.GetApplicationInternalDirectory();
          var file = internalDir.GetFile(FILE_WORKBENCH, EFileAccessResponse.CreateIfMissing);
          var wp = new WorkbenchParser();
          try {
            using (var stream = file.OpenForWriting()) {
              wp.WriteToStream(this, currentWorkbench, stream);
            }
            Log.D(this, "Workbench saved!");
          } catch (Exception e) {
            Log.E(this, "Failed to write workbench to file", e);
          }
        }
      });
    }

    /// <summary>
    /// Initializes the ION instance.
    /// </summary>
    public async Task Init() {
      try {
        Log.D(this, "Starting ION init");
        foreach (var im in managers) {
          Log.D(this, "Initializing " + im.GetType().Name);
          await im.InitAsync();
        }

        var internalDir = fileManager.GetApplicationInternalDirectory();
        if (internalDir.ContainsFile(FILE_WORKBENCH)) {
          var workbenchFile = internalDir.GetFile(FILE_WORKBENCH);
          currentWorkbench = await LoadWorkbenchAsync(workbenchFile);
        } else {
          currentWorkbench = new Workbench();
        }

        Log.D(this, "Ending ION init");
      } catch (Exception e) {
        Log.E(this, "Failed to init ION", e);
      }
    }

    private async Task<Workbench> LoadWorkbenchAsync(IFile file) {
      lock (this) {
        var wp = new WorkbenchParser();
        try {
          using (var stream = file.OpenForReading()) {
            return wp.ReadFromStream(this, stream); 
          }
        } catch (Exception e) {
          Log.E(this, "Failed to load workbench. Creating a new one.", e);
          return new Workbench();
        }
      }
    }
  } // End IosION
}

