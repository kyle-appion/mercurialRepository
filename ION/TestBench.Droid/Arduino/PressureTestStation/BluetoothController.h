
#pragma once

#include "Adafruit_BLE.h"
#include "Adafruit_BluefruitLE_SPI.h"
#include "Appion.h"

class BluetoothController {
public:
  i32 service;
  i32 localReadCharacteristic;
  i32 localWriteCharacteristic;

  // The current error that the bluetooth module is suffering from. Null for no error.
  const char* errorMessage;

  BluetoothController(i8 pinCs, i8 pinIrq, i8 pinRst, const char* deviceName) {
    this->ada = new Adafruit_BluefruitLE_SPI(pinCs, pinIrq, pinRst);
    this->deviceName = deviceName;
    errorMessage = NULL;
  }

  /*
  Queries whether or not the bluetooth controller is connected.
  Note: this is blocking.
  */
  bool IsConnected() {
    return ada->isConnected();
  }

  // Starts the bluetooth adapter.
  bool Begin(bool verbose) {
    if (!ada->begin(verbose)) {
      PostBluetoothError("Failed to begin the bluetooth module");
      return false;
    }

    if (!ada->factoryReset()) {
      PostBluetoothError("Failed to factory reset the bluetooth module");
      return false;
    }

    delay(500);

    // Set the device's name
    ada->print("AT+GAPDEVNAME=");
    ada->println(deviceName);
    if (!ada->waitForOK()) {
      PostBluetoothError("Failed to set name");
      return false;
    }

    // Create the custom service
    if (!ada->sendCommandWithIntReply("AT+GATTADDSERVICE=UUID=0xFF00", &service)) {
      PostBluetoothError("Failed to create service");
      return false;
    }

    if (!ada->sendCommandWithIntReply("AT+GATTADDCHAR=UUID=0xFF01,PROPERTIES=0x0c,MIN_LEN=20,MAX_LEN=20", &localReadCharacteristic)) {
      PostBluetoothError("Failed to create read characteristic");
      return false;
    }

    if (!ada->sendCommandWithIntReply("AT+GATTADDCHAR=UUID=0xFF02,PROPERTIES=0x12,MIN_LEN=20,MAX_LEN=20", &localWriteCharacteristic)) {
      PostBluetoothError("Failed to create write characteristic");
      return false;
    }

    ada->reset();
    return true;
  }

  // Reads from the given characteristic.
  // Returns the number of characters read.
  u32 ReadFrom(u32 characteristic, u8* outBuffer, u32 length) {
    u32 lim = length * 3 - 1;

    ada->print("AT+GATTCHAR=");
    ada->println(characteristic);

    u32 read = ada->readline();

    lim = MIN((read + 1) / 3, length);

    for (int i = 0, k = 0; i < lim; i += 3) {
      u8 b = nibbleFromChar(ada->buffer[i]) << 4 | nibbleFromChar(ada->buffer[i + 1]);
      outBuffer[k++] = b;
    }

    return lim;
  }

  // Writes length bytes from the buffer to the given characteristic.
  bool WriteTo(u32 characteristic, u8* buffer, i32 length) {
    ada->print("AT+GATTCHAR=");
    ada->print(characteristic);
    ada->print(",");
    for (int i = 0; i < length - 1; i++) {
      ada->print(charFromNibble(buffer[i] >> 4));
      ada->print(charFromNibble(buffer[i] & 0xf));
      ada->print("-");
    }
    ada->print(charFromNibble(buffer[length - 1] >> 4));
    ada->print(charFromNibble(buffer[length - 1] & 0xf));
    ada->println();

    return ada->waitForOK();
  }

  // Clears the given characteristic (set all bytes to 0)
  void ClearCharacteristic(i32 characteristic) {
    u32 len = 20;
    u8 buffer[len];
    memset(buffer, 0, len);
    WriteTo(characteristic, buffer, len);
  }

  // Posts an error to the bluetooth module.
  void PostBluetoothError(const char* errorMessage) {
    this->errorMessage = errorMessage;
  }

  // Whether or not the bluetooth module has an error.
  bool HasError() {
    return errorMessage != NULL;
  }

  // Queries the error message from the bluetooth
  const char* GetErrorMessage() {
    return errorMessage;
  }

protected:
private:
  // The name for the device.
  const char* deviceName;
  // The adafrui adapter.
  Adafruit_BluefruitLE_SPI* ada;
};
