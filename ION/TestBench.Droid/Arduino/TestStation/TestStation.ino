

#include <Arduino.h>

#include "Appion.h"
#include "BluetoothController.h"
#include "TestDriver.h"
#include "VctControlStepper.h"


#define DEBUG true
#define VERBOSE false
// Stepper 1 Pin layouts
#define PIN_STEPPER_1_DIR 46  // The pin that will output the stepper 1 motor direction
#define PIN_STEPPER_1_STEP 47 // The pin that will output the stepper 1 motor step
#define PIN_STEPPER_1_START_SWITCH 40 // The pin stepper 1's home detection switch
#define PIN_STEPPER_1_END_SWITCH 42   // The pin stepper 1's end detection switch
// Stepper 2s Pin layouts
#define PIN_STEPPER_2_DIR 44  // The pin that will output the stepper 2 motor direction
#define PIN_STEPPER_2_STEP 45 // The pin that will output the stepper 2 motor step
#define PIN_STEPPER_2_START_SWITCH 41 // The pin stepper 2's home detection switch
#define PIN_STEPPER_2_END_SWITCH 43   // The pin stepper 2's end detection switch

// Status LED
#define PIN_ERROR_LED 53          // The led that is used to indicate an error

#define BLUEFRUIT_SPI_SCK 52      // The pin to the bluefruit's SCK system clock
#define BLUEFRUIT_SPI_MISO 50     // The pin to the bluefruit's MISO pin
#define BLUEFRUIT_SPI_MOSI 51     // The pin to the bluefruit's MOSI pin
#define BLUEFRUIT_SPI_CS 49       // The pin to the bluefruit's CS pin
#define BLUEFRUIT_SPI_IRQ 48      // The pin to the bluefruit's IQR pin
#define BLUEFRUIT_SPI_RST 22      // The pin to the bluefruit's RST pin

#define BLUETOOTH_WRITE_INTERVAL 100000 // The interval between bluetooth writes
#define BLUETOOTH_READ_INTERVAL 500000 // The interval between bluetooth reads

#define IS_HIGH_CCW false         // Whether or not the high direction bit is a counter-clockwise rotation for the stepper

#define MASK_11 0x07ff
#define MASK_16 0xf800

BluetoothController* btController;
Vrc* vrc;
VctControlStepper* intake;        // The stepper controller that will control the intake valve.
VctControlStepper* exhaust;       // The stepper controller that will control the exhaust valve.
TestDriver* driver;
Timer timer;                      // The timer that is used to track time :)
ERigCommandState state;           // The current state of the rig.
u32 timeSinceLastRead;    // The time since the last bluetooth read was performed.
u32 timeSinceLastWrite;   // The time since the last bluetooth write was performed.
bool wasConnected;        // Was the test connected

void setup() {
  Serial.begin(115200);
  Serial.println("Setup");

  pinMode(PIN_ERROR_LED, OUTPUT);

  u32 testPoints[] = {
    500000,
    150000,
    50000,
    25000,
    10000,
    1700,
    1000,
    500,
    200,
  };

  btController = new BluetoothController(BLUEFRUIT_SPI_CS, BLUEFRUIT_SPI_IRQ, BLUEFRUIT_SPI_RST, "ION Test Rig");
  vrc = new Vrc();

  intake = new VctControlStepper(PIN_STEPPER_1_DIR, IS_HIGH_CCW, PIN_STEPPER_1_STEP, 5000, 10, PIN_STEPPER_1_START_SWITCH, PIN_STEPPER_1_END_SWITCH);
  exhaust = new VctControlStepper(PIN_STEPPER_2_DIR, IS_HIGH_CCW, PIN_STEPPER_2_STEP, 5000, 10, PIN_STEPPER_2_START_SWITCH, PIN_STEPPER_2_END_SWITCH);
  driver = new TestDriver(intake, exhaust, vrc, testPoints, sizeof(testPoints));

  btController->Begin(VERBOSE);
  driver->Reset();
  timer.GetDeltaTime();
  timeSinceLastWrite = 0;
  timeSinceLastRead = 0;
  wasConnected = false;
  Serial.println("Exited Setup");
}

void loop() {
  if (btController->HasError()) {
    Serial.println("Crashing...");
    Crash(btController->GetErrorMessage());
  }

  if (!btController->IsConnected()) {
    if (wasConnected) {
      // We may have popped in here because of a crashed packet. Before scrubbing the test, lets try again.
      // TODO ahodder@appioninc.com: This check can cause massive failures if consistent
      // Because this can happen at any time (including mission critical points),
      // this check will need to be removed at some point.
      for (int i = 5; i >= 0; i--) {
        if (btController->IsConnected()) {
          // It fixed itself.
          return;
        }
      }
/*
      // Still not connected
      u32 ellapsed;
      // Wait up to 10 seconds for a reconnect
      ellapsed += timer.GetDeltaTime();

      while (ellapsed <= 1000 * 1000 * 10) {
        if (!btController->IsConnected()) {
          delay(10);
        } else {
          // We reconnected.
          return;
        }
      }

      state = ERCS_Idle;
      return; // This return will flip us into another iteration which will determine what happens
*/
    }
    state = ERCS_Idle;
    driver->Reset();
    ClearCharacteristic(btController->localReadCharacteristic);
    Serial.println("Not connected...");
    delay(1500);
  } else {
    wasConnected = true;
    if (state != ERCS_Test) {
      Serial.println("Not Testing...");
      driver->Reset();
      delay(500);
    } else {
      driver->Step();
    }

    u32 dt = timer.GetDeltaTime();

    timeSinceLastWrite += dt;
    timeSinceLastRead += dt;

    // If it is time to write, do it
    if (timeSinceLastWrite >= BLUETOOTH_WRITE_INTERVAL) {
      u32 len = 20;
      u8 buffer[len];
      u8 state = (u8)(driver->GetCommandState());

      u32 offset = 0;

      u32 vrcMeas = vrc->GetMeasurement();
      f32 cip = intake->GetCurrentPositionAsDegrees();
      memcpy(buffer + (offset), &state, sizeof(state));
      memcpy(buffer + (offset += sizeof(state)), &vrcMeas, sizeof(vrcMeas));
      memcpy(buffer + (offset += sizeof(vrcMeas)), &cip, sizeof(cip));
      if (!btController->WriteTo(btController->localWriteCharacteristic, buffer, len)) {
        Serial.println("Failed to write to write characteristic");
      }
      timeSinceLastWrite %= timeSinceLastWrite;
    }

    // If it is time to read, do it
    if (timeSinceLastRead >= BLUETOOTH_READ_INTERVAL) {
      u32 len = 20;
      u8 buffer[len];
      btController->ReadFrom(btController->localReadCharacteristic, buffer, len);
      ParseBufferCommand(buffer, len);
      timeSinceLastRead %= timeSinceLastRead;
    }
  }
}

void ClearCharacteristic(i32 characteristic) {
  u32 len = 20;
  u8 buffer[len];
  memset(buffer, 0, len);
  btController->WriteTo(characteristic, buffer, len);
}

void Crash(const char* msg) {
  Serial.println(msg);
  while (true) {
    digitalWrite(PIN_ERROR_LED, !digitalRead(PIN_ERROR_LED));
    delay(750);
  }
}

void ParseBufferCommand(u8* buffer, u32 len) {
  // TODO ahodder@appioninc.com: Clear the read characteristic upon parsing the buffer. This will prevent this method from being spammed.
  i8 command = 0;
  memcpy(&command, buffer, sizeof(command));

  switch (command) {
    case ERCS_Reset:
      driver->Reset();
      break;
    case ERCS_Test:
      if (!exhaust->IsVctAtEnd()) {
        Serial.println("Closing exhaust");
        exhaust->RotateToDegree(100);
        while (!exhaust->IsVctAtEnd()) {
          exhaust->SafeStep();
        }
      }
      state = ERCS_Test;
      break;
  }
}

// Attempts to shorten the the value down to two bytes while maintaining the most
// amount of precision.
u16 ConvertToShortSignificant(u32 value) {
/*
  11 bits used for sig figs
  5 bits used for exponent
*/
  u16 ret = 0;

  u8 e = 0;
  while (value % 10 == 0) {
    e++;
    value /= 10;
  }

  ret = (value & MASK_11) | ((e << 11) & MASK_16);
  return ret;
}
