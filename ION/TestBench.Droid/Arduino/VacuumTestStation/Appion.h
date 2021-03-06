/******************************************************************************
Appion.h

This header file is used to provide commons program code that is used across
the appion c/c++ code respository.
******************************************************************************/

#pragma once
#ifndef APPION_H
#define APPION_H

#include <Arduino.h>

#define i8 int8_t
#define i16 int16_t
#define i32 int32_t
#define i64 int64_t

#define u8 uint8_t
#define u16 uint16_t
#define u32 uint32_t
#define u64 uint64_t

#define f32 float
#define f64 double

#ifndef ABS
#define ABS(x) ((x < 0) ? -x : x)
#endif


// Returns a nibble value from the given character.
static u8 nibbleFromChar(char character) {
  if ((character >= '0' && character <= '9')) {
    return (u8)(character - '0');
  } else if (character >= 'a' && character <= 'f') {
    return (u8)(character - 'a' + (char)10);
  } else if (character >= 'A' && character <= 'F') {
    return (u8)(character - 'A' + (char)10);
  } else {
    return (u8)255;
  }
}

// Gets the character nibble for the given byte.
static char charFromNibble(u8 theByte) {
  if (theByte >= 0 && theByte <= 9) {
    return (char)(theByte + '0');
  } else if (theByte >= 10 && theByte < 16) {
    return (char)(theByte - 10 + 'a');
  } else {
    return 'X';
  }
}

// Prints the given buffer.
static void printHexBuffer(u8* buffer, u32 bufferLength) {
  for (int i = 0; i < bufferLength; i++) {
    u8 b = (u8)buffer[i];
    u8 higher = b / 16;
    u8 lower = b % 16;
    Serial.print("0x");
    Serial.print(charFromNibble(higher));
    Serial.print(charFromNibble(lower));
    Serial.print(" ");
  }
  Serial.println("");
}

// Prints the given buffer.
static void printBuffer(char* buffer, u32 bufferLength) {
  for (int i = 0; i < bufferLength; i++) {
    Serial.print(buffer[i]);
    Serial.print(", ");
  }
  Serial.println("");
}
#endif // APPION_H
