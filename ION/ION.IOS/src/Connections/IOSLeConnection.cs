namespace ION.IOS.Connections {
  
  using System;

  using CoreBluetooth;
  using Foundation;

  using ION.Core.Connections;

  public class IosLeConnection : BaseIOSConnection {
    /// <summary>
    /// The base UUID that identifies services, charactertistics and descriptors
    /// within the BluetoothGatt API.
    /// </summary>
    private const string BASE_UUID = "0000****-0000-1000-8000-00805f9b34fb";
    /// <summary>
    /// The Rx characteristic.
    /// </summary>
    private static readonly ServiceCharacteristicPair READ_CHARACTERISTIC = new ServiceCharacteristicPair("ffe0", "ffe4");
    /// <summary>
    /// The Tx characteristic.
    /// </summary>
    private static readonly ServiceCharacteristicPair WRITE_CHARACTERISTIC = new ServiceCharacteristicPair("ffe5", "ffe9");
    /// <summary>
    /// The characteristic for the name.
    /// </summary>
    private static readonly ServiceCharacteristicPair NAME_CHARACTERISTIC = new ServiceCharacteristicPair("ff90", "ff91");

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
    /// The characteristic that we will need to get the name of the device.
    /// </summary>
    /// <value>The name.</value>
    private CBCharacteristic nameCharacteristic { get; set; }


    /// <summary>
    /// Creates a new iOS connection wrapper.
    /// </summary>
    /// <param name="centeralManager">Centeral manager.</param>
    /// <param name="peripheral">Peripheral.</param>
    public IosLeConnection(IonCBCentralManagerDelegate centralManager, CBPeripheral peripheral) : base(centralManager, peripheral) {
      connectionState = EConnectionState.Disconnected;
      connectionTimeout = TimeSpan.FromMilliseconds(45 * 1000);
    }

    // Overridden from BaseIOSConnection
    public override void UpdatedCharacterteristicValue(CBPeripheral peripheral, CBCharacteristic characteristic, NSError error) {
      if (characteristic.Equals(nameCharacteristic)) {
        var bytes = nameCharacteristic?.Value?.ToArray();
        name = System.Text.Encoding.UTF8.GetString(bytes);
      } else if (characteristic.Equals(readCharacteristic)) {
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
            if (READ_CHARACTERISTIC.characteristic.Equals(characteristic.UUID)) {
              readCharacteristic = characteristic;
            } else if (WRITE_CHARACTERISTIC.characteristic.Equals(characteristic.UUID)) {
              writeCharacteristic = characteristic;
            } else if (NAME_CHARACTERISTIC.characteristic.Equals(characteristic.UUID)) {
              nameCharacteristic = characteristic;
            }
          }
        }
      }

      return readCharacteristic != null && writeCharacteristic != null && nameCharacteristic != null;
    }

    // Overridden from BaseIOSConnection
    protected override bool AreServicesValid() {
      return readCharacteristic != null && writeCharacteristic != null && nameCharacteristic != null;
    }

    internal class ServiceCharacteristicPair {
      public CBUUID service { get; set; }
      public CBUUID characteristic { get; set; }

      public ServiceCharacteristicPair(string service, string characteristic) {
        this.service = CBUUID.FromString(service);
        this.characteristic = CBUUID.FromString(characteristic);
      }
    }
  } // End IOSLeConnection
}