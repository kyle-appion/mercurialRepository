using System;
using System.IO;
using System.Threading.Tasks;

using ION.Core.App;
using ION.Core.Alarms;
using ION.Core.Alarms.Alerts;
using ION.Core.Content;
using ION.Core.Database;
using ION.Core.Devices;
using ION.Core.Fluids;
using ION.Core.IO;
using ION.Core.Report.DataLogs;

using ION.CoreExtensions.Net.Portal;

using ION.IOS.Alarms.Alerts;
using ION.IOS.IO;
using ION.IOS.Location;

namespace ION.IOS.App {

  public class RemoteIosION : IosION {
  
    public event Action onRemoteUpdateEvent;
    
    /// <summary>
    /// The platform infor for the remote device.
    /// </summary>
    /// <value>The remote platform info.</value>
    public IPlatformInfo remotePlatformInfo { get; private set; }
  

    public RemoteIosION(IONPortalService portal) : base(portal) {
    }

    protected override void OnCreateManagers() {
      var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ION.database");
      
      database = new IONDatabase(new SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS(), path, this);
      fileManager = new IosFileManager();
      locationManager = new IosLocationManager(this);
      deviceManager = new RemoteDeviceManager(this);
      alarmManager = new BaseAlarmManager(this);
      dataLogManager = new DataLogManager(this);
      fluidManager = new BaseFluidManager(this);
      
      alarmManager.alertFactory = (IAlarmManager am, IAlarm alarm) => {
        return new CompoundAlarmAlert(alarm, new PopupWindowAlarmAlert(alarm), new VibrateAlarmAlert(alarm, this), new SoundAlarmAlert(alarm, this));
      };
    }
    
    public override Task<Analyzer> LoadAnalyzerAsync() {
      return Task.FromResult(new Analyzer(this));
    }
    
    public override Task<Analyzer> LoadAnalyzerAsync(IFile file) {
      return Task.FromResult(new Analyzer(this));
    }
    
    public override Task<Workbench> LoadWorkbenchAsync() {
      return Task.FromResult(new Workbench(this));
    }
    
    public override Task<Workbench> LoadWorkbenchAsync(IFile file) {
      return Task.FromResult(new Workbench(this));
    }
    
    public override Task SaveAnalyzerAsync() {
      return Task.FromResult(false);
    }
    
    public override Task SaveWorkbenchAsync() {
      return Task.FromResult(false);
    }
  }
}
