/*
This header is used to declare the operations and interface used in communication
with a Fluke 700G high precision pressure gauge.

TODO ahodder@appioninc.com:
The following commands have not been addressed in the API below:
CAL_START
CAL_STORE
HC_OFF
HC_DFLT
HC_COMD_LIST
*/

#pragma once
#ifndef FLUKE700G_H
#define FLUKE700G_H

#include <string.h>

#include <SoftwareSerial.h>

#include "Appion.h"

// An enumeration of the units that are present in the fluke protocol.
enum EFlukeUnit {
  Fl_Psi,
  Fl_Bar,
  Fl_mBar,
  Fl_Kg_cm_2,
  Fl_InH20_4C,
  Fl_InH20_20C,
  Fl_InH20_60F,
  Fl_mH20_4C,
  Fl_MH20_20C,
  Fl_cm_H20_4C,
  Fl_cm_H20_20C,
  Fl_ft_H20_4C,
  Fl_ft_H20_20C,
  Fl_ft_H20_60F,
  Fl_inhg_0C,
  Fl_mmhg_0C,
  Fl_kpal,
  Fl_Far,
  Fl_Cel,
  // Error unit
  Fl_Missing,
};

// An enumeration of the errors that the fluke gauge will respond with.
enum EFlukeError {
  // Fluke Errors
  Fl_Err_Ok = 0,
  Fl_Err_NonNumber = 101,
  Fl_Err_TooManyDigits = 102,
  Fl_Err_InvalidParam = 103,
  Fl_Err_AboveRange = 105,
  Fl_Err_BelowRange = 106,
  Fl_Err_MissingParam = 108,
  Fl_Err_InvalidPressureUnit = 109,
  Fl_Err_UnknownCommand = 117,
  Fl_Err_InputOverflow = 120,
  Fl_Err_TooManyEntries = 121,
  Fl_Err_NoPressure = 122,
  // Custom Errors
  Fl_Err_Timeout = 200,   // Used when a readline fails.
  Fl_Err_ParseError = 201,
};

// An enumeration of binary state options that the fluke may be in.
enum EFlukeStateOptions {
  Fl_Opts_None = 0,
  Fl_Opts_Comp = 1 << 1,
  Fl_Opts_Si = 1 << 2,
  Fl_Opts_Auto_On = 1 << 3,
};

// Queries the serial string that is used in communication with the FlukeGauge.
const char* Fl_FlukeUnitToString(EFlukeUnit flukeUnit);

// Parses the EFlukeUnit from the given string. The parsed unit will be placed into the out unit.
// If the string is not a valid, we will return false;
bool Fl_FlukeUnitFromString(char* str, i32 strlen, EFlukeUnit* outUnit);

class Fluke700G {
private:
  SoftwareSerial* serial;

public:
  Fluke700G(u8 tx, u8 rx, bool invertLogic=true) {
    this->serial = new SoftwareSerial(tx, rx, invertLogic);
  }

  // Queries the next error from the fluke gauge.
  EFlukeError GetError();

  // Clears the Fluke700g's error queue.
  void ClearErrorQueue();

  // Queries the identification string for the Fluke700G. This includes the
  // manufacturer, model number and firmware version.
  char* GetIdentification();

  // Write the tare command to the Fluke700G.
  void Tare();

  // Queries the current tare value for the pressure gauge.
  EFlukeError GetTare(f32* outTare);

  // Attempts to set the pressure unit for the Fluke700G.
  void SetPressureUnit(EFlukeUnit unit);

  // Queries the current pressure unit that is assigned to the Fluke700G.
  EFlukeError GetPressureUnit(EFlukeUnit* outUnit);

  // Queries the current pressure from the Fluke700G in its current units.
  EFlukeError GetPressure(f32* outPres);

  // Attempts to set the temperature unit for the Fluke700G.
  void SetTemperatureUnit(EFlukeUnit unit);

  // Queries the current tempreature unit from Fluke700G.
  EFlukeError GetTemperatureUnit(EFlukeUnit* outUnit);

  // Queries the current temperature form the Fluke700G in its current units.
  EFlukeError GetTemperature(f32* outTemp);

  // Attempts to zero the Fluke700G.
  void Zero();

  // Queries the current zero offset for the Fluke700G.
  EFlukeError GetZero(f32* outZero);

  // Attempts to reset the min and max recorded values for the Fluke700G.
  void ResetMinMax();

  // Queries the min recorded pressure from the Fluke700G.
  EFlukeError GetMin(f32* outMin);

  // Queries the max recorded pressure from the Fluke700G.
  EFlukeError GetMax(f32* outMax);

  // Attempts to write all of the given options to the Fluke700G. If any of the
  // options fail to set, then an error will be returned.
  void SetOptions(EFlukeStateOptions options);

  // Queries the current state options for the Fluke700G.
  EFlukeStateOptions GetOptions();

  // Writes a line to the fluke gauge.
  void Write(char* msg) {
    this->serial->println(msg);
  }

  // Attempts to read a line from the fluke's serial device.
  // If the timeout is negative, then the readline will timeout in approximately 70 minutes.
  // Returns the number of characters read. If the Readline timed out during the read, then the returned value will be
  // inverted (ie. ~ret).
  i32 ReadLine(char* buffer, i32 maxRead, i32 timeout) {
    i32 ret = 0;

    while (timeout--) {
      while (serial->available()) {
        char c = serial->read();
        // End-of-line
        if (c == '\r' || c == '\n') {
          return ret;
        } else {
          buffer[ret++] = c;
        }
      }

      delay(1);
    }

    return ~ret;
  }

  // Attempts to read a float from the buffer. We will return true if a float was parsed and false otherwise. The parsed
  // float will be placed into the outResult. The max length is the max number of characters that will be read from the
  // buffer.
  bool AssertF32(char* buffer, i32 maxLength, f32* outResult) {
    char* t;
    *outResult = strntof(buffer, maxLength, &t);
    return buffer != t;
  }
/*
  // Attempts to read a measurement and attached unit from the fluke's serial device.
  // If the timeout is negative, then the readline will timeout in approximately 70 minutes.
  // Returns true if the measurement and unit were both read correctely. If the Readline timed out during the read or
  // either the measurement or unit were not successfully read, then we will return false.
  EFlukeError ReadMeasureUnitLine(i32 timeout, f32 outMeasurement, EFlukeUnit* outUnit) {
    char buffer[64];
    i32 cnt = ReadLine(buffer, 64, timeout);
    if (cnt > 0) {
      // The format is <FLOAT><COMMA><UNIT> with no separating whitespace
      char* t;
      f32 meas = strntof(buffer, &t);
      if (buffer == t) {
        // Failed to parse the measurement form the line
        return Fl_Err_ParseError;
      } else {
        if (Fl_FlukeUnitFromString(t + 1, cnt - (t - buffer) + 1, outUnit)) {
          return Fl_Err_Ok;
        } else {
          return Fl_Err_ParseError;
        }
      }
    } else {
      return Fl_Err_Timeout;
    }
  }
*/
};

#endif // FLUKE700G_H
