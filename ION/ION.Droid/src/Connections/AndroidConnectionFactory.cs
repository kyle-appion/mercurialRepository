namespace ION.Droid.Connections {
  
  using System;

  using Android.Bluetooth;
  using Android.Content;

  using ION.Core.Connections;
  using ION.Core.Devices.Connections;
  using ION.Core.Devices.Protocols;

  public class AndroidConnectionFactory : IConnectionFactory {

    private Context context;
    private BluetoothManager bluetoothManager;

    public AndroidConnectionFactory(Context context, BluetoothManager bm) { 
      this.context = context;
      this.bluetoothManager = bm;
    }
    
    public IConnection CreateConnection(string address, EProtocolVersion protocolVersion) {
      var adapter = bluetoothManager.Adapter;

      var device = adapter.GetRemoteDevice(address);

      if (device == null) {
        throw new Exception("Cannot create connection: Failed to resolve connection's device.");
      }

      ION.Core.Util.Log.D(this, "Device: " + device.Name + " has a protocolversion of: " + protocolVersion);

      switch (protocolVersion) {
        case EProtocolVersion.Classic:
          return new ClassicConnection(device);
        case EProtocolVersion.V1:
          goto case EProtocolVersion.V3;
        case EProtocolVersion.V2:
          goto case EProtocolVersion.V3;
        case EProtocolVersion.V3:
          return new LeConnection(context, bluetoothManager, device);
        case EProtocolVersion.V4:
          return new RigadoConnection(context, bluetoothManager, device);
        default:
          throw new Exception("Cannot create connection for protocol version " + protocolVersion);
      }
    }
  }
}

