/*
namespace ION.Droid.Connections {
  
  using System;

  using Android.Bluetooth;
  using Android.Content;

	using Appion.Commons.Util;

  using ION.Core.Connections;
  using ION.Core.Devices.Connections;
  using ION.Core.Devices.Protocols;

  public class AndroidConnectionFactory : IConnectionFactory {

    private Context context;
    private BluetoothManager bluetoothManager;

    public AndroidConnectionFactory(Context context) { 
			this.bluetoothManager = (BluetoothManager)context.GetSystemService(Context.BluetoothService);
    }
    
    public IConnection CreateConnection(string address, EProtocolVersion protocolVersion) {
      var adapter = bluetoothManager.Adapter;

      var device = adapter.GetRemoteDevice(address);

      if (device == null) {
        throw new Exception("Cannot create connection: Failed to resolve connection's device.");
      }

      switch (protocolVersion) {
        case EProtocolVersion.Classic:
          return new ClassicConnection(device);
        case EProtocolVersion.V1:
          return new LeConnection(context, bluetoothManager, device);
        case EProtocolVersion.V4:
          return new RigadoConnection(context, bluetoothManager, device);
        default:
          throw new Exception("Cannot create connection for protocol version " + protocolVersion);
      }
    }
  }
}

*/