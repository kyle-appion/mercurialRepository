/*
The following is per christian 29 Dec 2016:

The pressure rig should be designed such that is is able to perform both calibration and nist procedures.

This requires that the rig have two steppers controllers that will isolate the test from the g5 and the exhaust. Further,
test and calibration target points are to be relative. The rig does not need to precisely reach a desired target point
as the only thing that matters is a test gauges proximity to the reference (fluke) at the target point.

That said, this program only needs to preform the following functions which will be identified as states:
  * pressure up
  * hold pressure
  * pressure down
  * shudown safely
*/



#include <string.h>

#include <SoftwareSerial.h>

#include "Appion.h"
#include "BluetoothController.h"
#include "PressureRig.h"
#include "VctControlStepper.h"

#define DEBUG true

#define IS_HIGH_CCW false     // The direction of the control stepper
// Input Stepper Pin layouts
#define PIN_SIN_DIR 46  // The input stepper's direction pin
#define PIN_SIN_STEP 47 // The input stepper's step pin (controls step pulses)
#define PIN_SIN_START_SWITCH 40 // The switch pin used to determine if the input stepper vct is open
#define PIN_SIN_END_SWITCH 42   // The switch pin used to determine if the input stepper vct is closed
// Output Stepper Pin layouts
#define PIN_SOUT_DIR 44  // The output stepper's direction pin
#define PIN_SOUT_STEP 45 // The output stepper's step pin (controls step pulses)
#define PIN_SOUT_START_SWITCH 41 // The switch pin used to determine if the output stepper vct is open
#define PIN_SOUT_END_SWITCH 43   // The switch pin used to determine if the output stepper vct is closed
// G5 Start Relay
#define PIN_G5_START_RELAY 39     // The pin that, when high, will start the G5
// Status LED
#define PIN_ERROR_LED 53          // The led that is used to indicate an error

#define BLUEFRUIT_SPI_SCK 52      // The pin to the bluefruit's SCK system clock
#define BLUEFRUIT_SPI_MISO 50     // The pin to the bluefruit's MISO pin
#define BLUEFRUIT_SPI_MOSI 51     // The pin to the bluefruit's MOSI pin
#define BLUEFRUIT_SPI_CS 49       // The pin to the bluefruit's CS pin
#define BLUEFRUIT_SPI_IRQ 48      // The pin to the bluefruit's IQR pin
#define BLUEFRUIT_SPI_RST 22      // The pin to the bluefruit's RST pin

#define BLUETOOTH_WRITE_INTERVAL 100 // The time in milliseconds between bluetooth writes.

#define FLUKE_BUFFER_LEN 128      // The length of the fluke input buffer.

#define SIN_OPEN 1 << 0     // Whether or not the input stepper is open
#define SIN_CLOSED 1 << 1   // Whether or not the input stepper is closed
#define SOUT_OPEN 1 << 2    // Whether or not the output stepper is open
#define SOUT_CLOSED 1 << 3  // Whether or not the output stepper is closed
#define CRASHED = 1 << 7    // Whether or not the rig has crashed

#define COM_SIN_OPEN 1 << 0
#define COM_SIN_CLOSED 1 << 1
#define COM_SOUT_OPEN 1 << 2
#define COM_SOUT_CLOSED 1 << 3
#define COM_G5_ON 1 << 4
#define COM_G5_OFF 1 << 5

//  The bluetooth controller that will wrap the general bluetooth communication objects.
BluetoothController* bt;
// The stepper that is used to block off input pressure to the rig.
VctControlStepper* inputStepper;
// The stepper that will vent the pressure within the rig.
VctControlStepper* outputStepper;

// The target pressure that the rig wants to be at.
f32 targetPressure;
// The last known pressure that the was received from the fluke gauge.
f32 flukePressure;
// The milliseconds since the last time bluetooth write.
u32 dtBtWrite;

// The buffer that will hold the fluke communication.
char flukeBuffer[FLUKE_BUFFER_LEN];
// The current index with the fluke buffer.
i32 fi;
// The number of responses we have pending from fluke command writes.
i32 pendingResponses;

void Crash() {
  Shutdown();

  u32 lastTime;
  while (true) {
    digitalWrite(PIN_G5_START_RELAY, 0);
    if (micros() - lastTime > 500) {
      digitalWrite(PIN_ERROR_LED, !digitalRead(PIN_ERROR_LED));
      lastTime = micros();
    }
    Step();
  }
}

void CrashWithWrite() {
  WriteBluetooth();
  Crash();
}

// Shuts the rig down.
void Shutdown() {
  bt->Disconnect();
  PowerG5(false);
  inputStepper->SetRPS(0.05);
  inputStepper->RotateToDegree(0);

  outputStepper->SetRPS(0.05);
  outputStepper->RotateToDegree(0);
}

void PowerG5(bool on) {
  if (on) {
    digitalWrite(PIN_G5_START_RELAY, 1);
    digitalWrite(PIN_ERROR_LED, 1);
  } else {
    digitalWrite(PIN_G5_START_RELAY, 0);
    digitalWrite(PIN_ERROR_LED, 0);
  }
}

u8 BuildStateByte() {
  u8 ret;

  if (inputStepper->IsVctAtEnd()) {
    ret |= SIN_CLOSED;
  }

  if (inputStepper->IsVctAtStart()) {
    ret |= SIN_OPEN;
  }

  if (outputStepper->IsVctAtEnd()) {
    ret |= SOUT_CLOSED;
  }

  if (outputStepper->IsVctAtStart()) {
    ret |= SOUT_OPEN;
  }

  return ret;
}


// Parses a command from the given buffer. Note: the returned command is malloc'd onto the heap and thus needs to be
// freed.
// Parses the command that is written from the control tablet.
/*
Input protocol
Bytes | Type  | Description
------+-------+-----------------------------------------------------
 4    | f32   | The target pressure that the rig wants should be.
*/
void ResolveBluetoothInput() {
  u8 buffer[20];
  int cnt = bt->ReadFrom(bt->localReadCharacteristic, buffer, 20);
  if (cnt == 0 || buffer[0] == 0) {
    return;
  }

  memcpy(&targetPressure, buffer, sizeof(targetPressure));

  bt->ClearCharacteristic(bt->localReadCharacteristic);
}

/*
Output protocol
Bytes | Type  | Description
------+-------+-----------------------------------------------------
 1    | u8    | The current state of the rig
 4    | f32   | The current fluke measurement
 4    | f32   | The current input stepper angle
 4    | f32   | The current output angle
*/
// Write the current rig state to bluetooth.
void WriteBluetooth() {
  u8 buffer[20];
  u8* p = buffer;

  u8 state = BuildStateByte();
  f32 inputTheta = inputStepper->GetCurrentPositionAsDegrees();
  f32 outputTheta = outputStepper->GetCurrentPositionAsDegrees();

  // should be 13 bytes total
  memcpymov(p, &state, sizeof(state));
  memcpymov(p, &flukePressure, sizeof(flukePressure));
  memcpymov(p, &inputTheta, sizeof(inputTheta));
  memcpymov(p, &outputTheta, sizeof(outputTheta));

  bt->WriteTo(bt->localWriteCharacteristic, buffer, 20);
  dtBtWrite = millis() - dtBtWrite;
}

// Updates the fluke state.
void UpdateFlukeMeasurement() {
  if (unit != Fl_Psi) {
    Serial3.println("PRES_UNIT Psi");
  }
  int avail;

  if (pendingResponses == 0) {
    if (avail = Serial3.available()) {
      // Sink left over bytes.
      char buffer[MAX(64, avail)];
      Serial3.readBytes(buffer, avail);
    }
  }

  if (avail = Serial3.available()) {
    char buffer[MAX(64, avail)];
    int cnt = Serial3.readBytes(buffer, avail);
    if (cnt > 0) {
      memcpy(flukeBuffer + fi, buffer, cnt);
      fi += cnt;

      // Attempt to find the \r
      for (int i = 0; i < fi; i++) {
        if (flukeBuffer[i] == '\r') {
          if (!ResolveFlukeMeasurementBuffer(flukeBuffer, i)) {
            Serial.println("Failed to resolve measurement buffer");
          }
          memcpy(flukeBuffer, flukeBuffer + i + 1, fi - i - 1);
          fi = fi - i - 1;
          break;
        }
      }
    }
  }
}

// Resolves the current fluke buffer's contents.
bool ResolveFlukeMeasurementBuffer(char* buffer, i32 len) {
  Serial.print("Resolving: ");
  Serial.write(buffer, len);
  Serial.println();
  pendingResponses--;
  char b[len];
  int index = SplitByChar(buffer, len, b, ',');
  if (index == -1) {
    // The buffer is shot
    Serial.println("Failed to resolve measurement buffer: failed to split");
    return false;
  } else {
    f32 meas;

    if (!AssertF32(b, index, &meas)) {
      Serial.println("Failed to parse float");
      return false;
    } else if (!FlukeUnitFromString(buffer + index + 1, len - index - 1, &unit)) {
      Serial.println("Failed to parse unit");
      return false;
    } else if (unit != Fl_Psi) {
      Serial.println("Failed to resolve measurement: unit not psi");
    } else {
      flukePressure = meas;
      Serial.print("Resolve measurement: ");
      Serial.print(meas);
      Serial.print(" [");
      Serial.print(unit);
      Serial.println("]");
      return true;
    }
  }
}

void Step() {
  inputStepper->SafeStep();
  outputStepper->SafeStep();
}

/**********************************************************************************************************************
 * ARDUINO CONTROL CODE
 *********************************************************************************************************************/

void setup() {
  pinMode(PIN_ERROR_LED, OUTPUT);
  pinMode(PIN_G5_START_RELAY, OUTPUT);

  Serial.begin(9600);
  Serial3.begin(9600);

  bt = new BluetoothController(BLUEFRUIT_SPI_CS, BLUEFRUIT_SPI_IRQ, BLUEFRUIT_SPI_RST, "PB00Z0001");
  inputStepper = new VctControlStepper(PIN_SIN_DIR, IS_HIGH_CCW, PIN_SIN_STEP, 5000, 10, PIN_SIN_START_SWITCH, PIN_SIN_END_SWITCH);
  outputStepper = new VctControlStepper(PIN_SOUT_DIR, IS_HIGH_CCW, PIN_SOUT_STEP, 5000, 10, PIN_SOUT_START_SWITCH, PIN_SOUT_END_SWITCH);
}

void loop() {
  i32 start = millis();
  // Save our souls from an overpressure event
/*
  if (flukePressure > 950) {
    Shutdown();
    CrashWithWrite();
  }
*/

  UpdateFlukeMeasurement();
//  ResolveBluetoothInput();
  Step();

  if (millis() - dtBtWrite > BLUETOOTH_WRITE_INTERVAL) {
    if (pendingResponses == 0) {
      Serial3.println("VAL?");
      pendingResponses++;
    }
    dtBtWrite = millis();
//    WriteBluetooth();
  }
  i32 end = millis() - start;
  Serial.print("Loop iteration took: ");
  Serial.print(end);
  Serial.println("ms");
}
