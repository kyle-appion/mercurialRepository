
#pragma once
#ifndef PRESSURE_RIG_H
#define PRESSURE_RIG_H


// An enumeration of the units that are present in the fluke protocol.
enum EFlukeUnit {
  Fl_Psi = 0,
  Fl_Bar = 1,
  Fl_mBar = 2,
  Fl_Kg_cm_2 = 3,
  Fl_InH20_4C = 4,
  Fl_InH20_20C = 5,
  Fl_InH20_60F = 6,
  Fl_mH20_4C = 7,
  Fl_MH20_20C = 8,
  Fl_cm_H20_4C = 9,
  Fl_cm_H20_20C = 10,
  Fl_ft_H20_4C = 11,
  Fl_ft_H20_20C = 12,
  Fl_ft_H20_60F = 13,
  Fl_inhg_0C = 14,
  Fl_mmhg_0C = 15,
  Fl_kpal = 16,
  Fl_Far = 17,
  Fl_Cel = 18,
  // Error unit
  Fl_Missing = 255,
};

static const char* FlukeUnitToString(EFlukeUnit flukeUnit) {
  switch (flukeUnit) {
    case Fl_Psi:
      return "Psi";
    case Fl_Bar:
      return "Bar";
    case Fl_mBar:
      return "mBar";
    case Fl_Kg_cm_2:
      return "KG/CM2";
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

static bool FlukeUnitFromString(char* str, i32 strlen, EFlukeUnit* outUnit) {
//  Serial.print("Looking for unit for raw string: ");
//  Serial.write(str, strlen);
//  Serial.println();
  for (int i = 0; i < (int)Fl_Missing; i++) {
    EFlukeUnit u = (EFlukeUnit)i;
    if (strncmp(FlukeUnitToString(u), str, strlen) == 0) {
      *outUnit = u;
      return true;
    }
  }

  return false;
}

// Attempts to read a float from the buffer. We will return true if a float was parsed and false otherwise. The parsed
// float will be placed into the outResult. The max length is the max number of characters that will be read from the
// buffer.
static bool AssertF32(char* buffer, i32 maxLength, f32* outResult) {
//  Serial.print("Asserting: ");
//  Serial.write(buffer, maxLength);
//  Serial.println();

  // Guarantee that the buffer is a "c" string.
  int nl = maxLength + 1;
  char b[nl];
  memcpy(b, buffer, nl);

  char* t;
  *outResult = strtod(b, &t);
  return buffer != t;
}

#endif // PRESSURE_RIG_H
