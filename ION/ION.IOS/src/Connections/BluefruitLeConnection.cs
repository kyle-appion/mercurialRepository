namespace ION.IOS.Connections {
  
  using System;

  using CoreBluetooth;
  using Foundation;

  using ION.Core.Connections;
  using ION.Core.Util;

  public class BluefruitLeConnection : BaseIOSConnection {
//    private static readonly CBUUID READ_SERVICE = CBUUID.FromString("ffe0");
    private static readonly CBUUID READ_CHARACTERISTIC = CBUUID.FromString("ff01");
//    private static readonly CBUUID WRITE_SERVICE = CBUUID.FromString("ffe5");
    private static readonly CBUUID WRITE_CHARACTERISTIC = CBUUID.FromString("ff02");

    /// <summary>
    /// The characteristic that the connection is using to read data packets.
    /// </summary>
    private CBCharacteristic readCharacteristic;
    /// <summary>
    /// The characteristic that the connection is used to write data packets to.
    /// </summary>
    private CBCharacteristic writeCharacteristic;

    public BluefruitLeConnection(LeConnectionHelper connectionHelper, CBPeripheral peripheral) : base(connectionHelper, peripheral) {
    }

    /// <summary>
    /// Called when the connection successfully connects.
    /// </summary>
    protected override void OnConnected() {
      __nativeDevice.SetNotifyValue(true, readCharacteristic);
    }

    /// <summary>
    /// Updateds the characterteristic value.
    /// </summary>
    /// <param name="peripheral">Peripheral.</param>
    /// <param name="characteristic">Characteristic.</param>
    /// <param name="error">Error.</param>
    public override void UpdatedCharacterteristicValue(CBPeripheral peripheral, CBCharacteristic characteristic, NSError error) {
      if (READ_CHARACTERISTIC.Equals(characteristic.UUID)) {
        lastPacket = readCharacteristic.Value.ToArray();
      }
    }

    /// <summary>
    /// Writes the given packet out to the remote terminus.
    /// </summary>
    /// <returns></returns>
    /// <param name="packet">Packet.</param>
    public override bool Write(byte[] packet) {
      if (EConnectionState.Connected != connectionState) {
        return false;
      }

      NSData data = NSData.FromArray(packet);
      __nativeDevice.WriteValue(data, writeCharacteristic, CBCharacteristicWriteType.WithoutResponse);

//      Log.D(this, "Wrote: " + packet.ToByteString());

      return true;
    }

    /// <summary>
    /// Called by the BaseIONConnection when new characteristics are discovered. This is where you should attempt to
    /// find all the characteristics that are necessary for your communication.
    /// </summary>
    protected override void ValidateServices() {
      foreach (var service in __nativeDevice.Services) {
        if (service != null && service.Characteristics != null) {
          foreach (var characteristic in service.Characteristics) {
            if (READ_CHARACTERISTIC.Equals(characteristic.UUID)) {
              readCharacteristic = characteristic;
            } else if (WRITE_CHARACTERISTIC.Equals(characteristic.UUID)) {
              writeCharacteristic = characteristic;
            }
          }
        }
      }
    }

    /// <summary>
    /// Queries whether or not the services are valid.
    /// </summary>
    /// <returns><c>true</c>, if services valid was ared, <c>false</c> otherwise.</returns>
    protected override bool AreServicesValid() { 
      return readCharacteristic != null && writeCharacteristic != null;
    }
  }
}

