/*
This file is used to define the operations and interfaces used in communication
with a Fluke700G high precision pressure gauge.

This file is written for the Arduino Mega 2560 platform.
*/
#include "Fluke700G.h"

#ifndef TIMEOUT
#define TIMEOUT 1000
#endif


const char* Fl_FlukeUnitToString(EFlukeUnit flukeUnit) {
  switch (flukeUnit) {
    case Fl_Psi:
      return "Psi";
    case Fl_Bar:
      return "Bar";
    case Fl_mBar:
      return "mBar";
    case Fl_Kg_cm_2:
      return "Kg/cm2";
    case Fl_InH20_4C:
      return "InH204C";
    case Fl_InH20_20C:
      return "InH2020C";
    case Fl_InH20_60F:
      return "InH2060F";
    case Fl_mH20_4C:
      return "mH204C";
    case Fl_MH20_20C:
      return "MH2020C";
    case Fl_cm_H20_4C:
      return "cmH204C";
    case Fl_cm_H20_20C:
      return "cmH2020C";
    case Fl_ft_H20_4C:
      return "ftH204C";
    case Fl_ft_H20_60F:
      return "ftH2060F";
    case Fl_inhg_0C:
      return "inhg0C";
    case Fl_mmhg_0C:
      return "mmhg0C";
    case Fl_kpal:
      return "kpal";
    case Fl_Far:
      return "Far";
    case Fl_Cel:
      return "Cel";
    default:
      return "MISSING";
  }
}

bool Fl_FlukeUnitFromString(char* str, i32 strlen, EFlukeUnit* outUnit) {
  for (int i = 0; i < (int)Fl_Missing; i++) {
    EFlukeUnit u = (EFlukeUnit)i;
    if (strncmp(Fl_FlukeUnitToString(u), str, strlen) == 0) {
      *outUnit = u;
      return true;
    }
  }

  return false;
}

EFlukeError Fluke700G::GetError() {
  Write("FAULT?");

  char buffer[64];
  int cnt = ReadLine(buffer, 64, TIMEOUT);
  if (cnt > 0) {
    char* t;
    i32 ret = strtol(buffer, &t, 10);
    if (buffer == t) {
      // Corrupt error packet.
      return Fl_Err_ParseError;
    } else {
      return (EFlukeError)ret;
    }
  } else {
    return Fl_Err_Timeout;
  }
}

void Fluke700G::ClearErrorQueue() {
  Write("*CLS");
}

void Fluke700G::Tare() {
  Write("TARE");
}

EFlukeError Fluke700G::GetTare(f32* outTare) {
  Write("TARE?");
  char buffer[64];
  int cnt = ReadLine(buffer, 64, TIMEOUT);
  if (cnt > 0) {
    f32 result;
    if (AssertF32(buffer, cnt, &result)) {
      *outTare = result;
      return Fl_Err_Ok;
    } else {
      return Fl_Err_ParseError;
    }
  } else {
    return Fl_Err_Timeout;
  }
}

void Fluke700G::SetPressureUnit(EFlukeUnit unit) {
  char buffer[64];
  sprintf(buffer, "PRES_UNIT %s", Fl_FlukeUnitToString(unit));
  Write(buffer);
}

EFlukeError Fluke700G::GetPressureUnit(EFlukeUnit* outUnit) {
  Write("PRES_UNIT?");
  char buffer[64];
  int cnt = ReadLine(buffer, 64, TIMEOUT);
  if (cnt > 0) {
    EFlukeUnit unit;
    if (Fl_FlukeUnitFromString(buffer, cnt, &unit)) {
      *outUnit = unit;
      return Fl_Err_Ok;
    } else {
      return Fl_Err_ParseError;
    }
  } else {
    return Fl_Err_Timeout;
  }
}

EFlukeError Fluke700G::GetPressure(f32* outMeasurement) {
  Write("VAL?");
  char buffer[64];
  int cnt = ReadLine(buffer, 64, TIMEOUT);
  if (cnt > 0) {
    f32 result;
    if (AssertF32(buffer, cnt, &result)) {
      *outMeasurement = result;
      return Fl_Err_Ok;
    } else {
      return Fl_Err_ParseError;
    }
  } else {
    return Fl_Err_Timeout;
  }
}

void Fluke700G::SetTemperatureUnit(EFlukeUnit unit) {
  char buffer[64];
  sprintf(buffer, "TEMP_UNIT %s", Fl_FlukeUnitToString(unit));
  Write(buffer);
}

EFlukeError Fluke700G::GetTemperatureUnit(EFlukeUnit* outUnit) {
  Write("TEMP_UNIT?");
  char buffer[64];
  int cnt = ReadLine(buffer, 64, TIMEOUT);
  if (cnt > 0) {
    EFlukeUnit unit;
    if (Fl_FlukeUnitFromString(buffer, cnt, &unit)) {
      *outUnit = unit;
      return Fl_Err_Ok;
    } else {
      return Fl_Err_ParseError;
    }
  } else {
    return Fl_Err_Timeout;
  }
}

EFlukeError Fluke700G::GetTemperature(f32* outTemp) {
  Write("TEMP?");
  char buffer[64];
  int cnt = ReadLine(buffer, 64, TIMEOUT);
  if (cnt > 0) {
    f32 result;
    if (AssertF32(buffer, cnt, &result)) {
      *outTemp = result;
      return Fl_Err_Ok;
    } else {
      return Fl_Err_ParseError;
    }
  } else {
    return Fl_Err_Timeout;
  }
}

void Fluke700G::Zero() {
  Write("ZERO_MEAS");
}

EFlukeError Fluke700G::GetZero(f32* outZero) {
  Write("ZERO_MEAS?");
  char buffer[64];
  int cnt = ReadLine(buffer, 64, TIMEOUT);
  if (cnt > 0) {
    f32 result;
    if (AssertF32(buffer, cnt, &result)) {
      *outZero = result;
      return Fl_Err_Ok;
    } else {
      return Fl_Err_ParseError;
    }
  } else {
    return Fl_Err_Timeout;
  }
}

void Fluke700G::ResetMinMax() {
  Write("MINMAX_RST");
}

EFlukeError Fluke700G::GetMin(f32* outMin) {
  Write("MIN?");
  char buffer[64];
  int cnt = ReadLine(buffer, 64, TIMEOUT);
  if (cnt > 0) {
    f32 result;
    if (AssertF32(buffer, cnt, &result)) {
      *outMin = result;
      return Fl_Err_Ok;
    } else {
      return Fl_Err_ParseError;
    }
  } else {
    return Fl_Err_Timeout;
  }
}

EFlukeError Fluke700G::GetMax(f32* outMax) {
  Write("MAX?");
  char buffer[64];
  int cnt = ReadLine(buffer, 64, TIMEOUT);
  if (cnt > 0) {
    f32 result;
    if (AssertF32(buffer, cnt, &result)) {
      *outMax = result;
      return Fl_Err_Ok;
    } else {
      return Fl_Err_ParseError;
    }
  } else {
    return Fl_Err_Timeout;
  }
}

void Fluke700G::SetOptions(EFlukeStateOptions options) {
}

EFlukeStateOptions Fluke700G::GetOptions() {
  return Fl_Opts_None;

}
