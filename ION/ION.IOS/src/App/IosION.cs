using System;
using System.IO;

using Foundation;

using ION.Core.App;
using ION.Core.Content;
using ION.Core.Database;
using ION.Core.Devices;
using ION.Core.Fluids;
using ION.Core.Util;

using ION.IOS.IO;
using ION.IOS.Devices;

namespace ION.IOS.App {
  /// <summary>
  /// The iOS ION implementation.
  /// </summary>
  public class IosION : IION {
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

    public IosION() {
      fileManager = new IosFileManager();
      deviceManager = new IOSDeviceManager(this);
      var fm = new BaseFluidManager(this);
      fluidManager = fm;
      var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ION.database");
      database = new IONDatabase(new SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS(), path, this);

      deviceManager.Init();
      fm.Init();

      currentWorkbench = new Workbench();
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
  } // End IosION
}

