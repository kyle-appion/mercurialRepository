namespace ION.Droid.Dialog {
  
  using System.Collections.Generic;
  
  using Android.Content;
  
  using ION.Core.App;
  using ION.Core.Devices;
  using ION.Core.Devices.Sorters;
  using ION.Core.Sensors;
  
  using ION.Droid.Devices;
  
  public class GaugeDeviceSensorSelectDialog {
    public delegate void OnSensorSelected(GaugeDeviceSensor sensor);

    private Context _context;
    private List<GaugeDeviceSensor> _sensors;
    private OnSensorSelected _onSensorSelected;

    public GaugeDeviceSensorSelectDialog(Context context, IION ion, ESensorType sensorType, OnSensorSelected onSensorSelected) {
      _context = context;
      _onSensorSelected = onSensorSelected;

      _sensors = new List<GaugeDeviceSensor>();

      foreach (var device in ion.deviceManager.knownDevices) {
        var gd = device as GaugeDevice;
        if (gd != null) {
          foreach (var sensor in gd.sensors) {
            if (sensor.type == sensorType) {
              _sensors.Add(sensor);
            }
          }
        }
      }
      
      _sensors.Sort(new GeneralSensorSorter());
    }
    
    public GaugeDeviceSensorSelectDialog(Context context, IEnumerable<GaugeDeviceSensor> sensors, OnSensorSelected onSensorSelected) {
      _context = context;
      _sensors = new List<GaugeDeviceSensor>(sensors);
      _onSensorSelected = onSensorSelected;
    }

    public void Show() {
      var adb = new ListDialogBuilder(_context);
      adb.SetTitle(Resource.String.select_a_sensor);
      foreach (var sensor in _sensors) {
        adb.AddItem(string.Format("{0}: {1}", sensor.device.serialNumber.deviceModel.GetTypeString(), sensor.type.GetSensorTypeName()), () => {
          _onSensorSelected(sensor);
        });
      }
//      adb.SetNegativeButton(Resource.String.cancel, (sender, args) => { });
//      adb.SetNegativeButton(Resource.String.cancel, (sender, args) => { });

      adb.Show();
    }
  }
}