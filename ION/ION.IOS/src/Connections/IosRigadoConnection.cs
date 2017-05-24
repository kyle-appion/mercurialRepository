namespace ION.IOS.Connections {

  using System;

  using CoreBluetooth;
  using Foundation;

  using ION.Core.Connections;

  public class IosRigadoConnection : BaseIOSConnection {
    /// <summary>
    /// The base UUID that identifies services, charactertistics and descriptors
    /// within the BluetoothGatt API.
    /// </summary>
		private const string BASE_UUID = "6e40****-b5a3-f393-e0a9-e50e24dcca9e";
    /// <summary>
    /// The Rx characteristic.
    /// </summary>
		private static readonly CBUUID READ_CHARACTERISTIC = CBUUID.FromString("6e400003-b5a3-f393-e0a9-e50e24dcca9e");
    /// <summary>
    /// The Tx characteristic.
    /// </summary>
		private static readonly CBUUID WRITE_CHARACTERISTIC = CBUUID.FromString("6e400002-b5a3-f393-e0a9-e50e24dcca9e");

    /// <summary>
    /// A utility method used to create the UUIDs necessary for use of the
    /// BluetoothApi.
    /// </summary>
    /// <param name="content"></param>
    /// <returns></returns>
    private static CBUUID Inflate(string content) {
      return CBUUID.FromString(BASE_UUID.Replace("****", content));
    }

    /// <summary>
    /// The characteristic that we will read the device data from.
    /// </summary>
    private CBCharacteristic readCharacteristic { get; set; }
    /// <summary>
    /// The characteristic that we will write output packets to.
    /// </summary>
    private CBCharacteristic writeCharacteristic { get; set; }


    /// <summary>
    /// Creates a new iOS connection wrapper.
    /// </summary>
    /// <param name="centeralManager">Centeral manager.</param>
    /// <param name="peripheral">Peripheral.</param>
    public IosRigadoConnection(IonCBCentralManagerDelegate centralManager, CBPeripheral peripheral) : base(centralManager, peripheral) {
      connectionState = EConnectionState.Disconnected;
      connectionTimeout = TimeSpan.FromMilliseconds(45 * 1000);
    }

    // Overridden from BaseIOSConnection
    public override void UpdatedCharacterteristicValue(CBPeripheral peripheral, CBCharacteristic characteristic, NSError error) {
      if (characteristic.Equals(readCharacteristic)) {
        lastPacket = readCharacteristic.Value.ToArray();
      } else {
//          Log.D(this, "Received unknown characteristic value: " + args.Characteristic);
      }
    }
    // Overridden from BaseIOSConnection
    public override bool Write(byte[] packet) {
      if (EConnectionState.Connected != connectionState) {
        return false;
      }

      NSData data = NSData.FromArray(packet);
      device.WriteValue(data, writeCharacteristic, CBCharacteristicWriteType.WithoutResponse);

      return true;
    }

    // Overridden from BaseIOSConnection
    protected override void OnDisconnect() {
      readCharacteristic = null;
      writeCharacteristic = null;
    }

    // Overridden from BaseIOSConnection
    protected override bool ValidateServices() {
      readCharacteristic = null;
      writeCharacteristic = null;

      foreach (CBService service in device.Services) {
        if (service != null && service.Characteristics != null) {
          // Apparently services can be null after discovery?
          foreach (CBCharacteristic characteristic in service.Characteristics) {
            if (READ_CHARACTERISTIC.Equals(characteristic.UUID)) {
              readCharacteristic = characteristic;
              device.SetNotifyValue(true, readCharacteristic);
            } else if (WRITE_CHARACTERISTIC.Equals(characteristic.UUID)) {
              writeCharacteristic = characteristic;
            }
          }
        }
      }

      return readCharacteristic != null && writeCharacteristic != null;
    }

    // Overridden from BaseIOSConnection
    protected override bool AreServicesValid() {
      return readCharacteristic != null && writeCharacteristic != null;
    }

    internal class ServiceCharacteristicPair {
      public CBUUID service { get; set; }
      public CBUUID characteristic { get; set; }

      public ServiceCharacteristicPair(string service, string characteristic) {
        this.service = CBUUID.FromString(service);
        this.characteristic = CBUUID.FromString(characteristic);
      }
    }
  } // End IOSRigadoConnection
}