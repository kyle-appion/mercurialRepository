﻿namespace ION.Droid.Connections {
  
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

      switch (protocolVersion) {
        case EProtocolVersion.Classic:
          return new ClassicConnection(adapter.GetRemoteDevice(address));
        case EProtocolVersion.V1:
          goto case EProtocolVersion.V3;
        case EProtocolVersion.V2:
          goto case EProtocolVersion.V3;
        case EProtocolVersion.V3:
          var device = adapter.GetRemoteDevice(address);
          if (device == null) {
            throw new Exception("Cannot create connection: Failed to resolve connection's device.");
          } else {
            return new LeConnection(context, bluetoothManager, device);
          }
        default:
          throw new Exception("Cannot create connection for protocol version " + protocolVersion);
      }
    }
    
  }
}

