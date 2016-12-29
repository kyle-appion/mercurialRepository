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

#ifndef MIN
#define MIN(a, b) (a < b ? a : b)
#endif

#ifndef I32_MAX
#define I32_MAX 2147483647
#endif

#ifndef I32_MIN
#define I32_MIN -2147483648
#endif

#ifndef F32_MAX
#define F32_MAX 3.4028235E+38
#endif

#ifndef F32_MIN
#define F32_MIN -3.4028235E+38
#endif

// Returns a nibble value from the given character.
static u8 nibbleFromChar(char character) {
  if (character == 0) {
    return 0;
  } else if ((character >= '0' && character <= '9')) {
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

// This function exactly performs a memcpy using the same parameters. Additionally, after the memcpy, the src is moved
// forward by count.
// This function is primarily used when pulling variable size entities out of a byte stream.
static void* memcpymov(void* dest, const void* src, size_t count) {
  void* ret = memcpy(dest, src, count);
  src += count;
  return ret;
}

// Similar to strtol in which we convert a series of characters to a long. However, this will break out of the string
// under two conditions: 1) a non numeric character is read, or 2) a number is read up to the number of characters
// provided in sz.
static i32 strntol(const char* str, size_t sz, char** end, int base) {
	/* Expect that digit representation of LONG_MAX/MIN
	 * not greater than this buffer */
	char buf[24];
	i32 ret;
	const char* beg = str;
	/* Catch up leading spaces */
	for (; beg && sz && *beg == ' '; beg++, sz--);
	if (!sz || sz >= sizeof(buf)) {
		if (end) {
			*end = (char*)str;
    }
		return 0;
	}
	memcpy(buf, beg, sz);
	buf[sz] = '\0';
	ret = strtol(buf, end, base);
	if (ret == I32_MIN || ret == I32_MAX) {
		return ret;
  }
	if (end) {
		*end = (char*)str + (*end - buf);
  }
	return ret;
}

// Similar to strtof in which we convert a series of characters to a float. However, this will break out of the string
// under two conditions: 1) a non numeric character is read, or 2) a number is read up to the number of characters
// provided in sz.
static f32 strntof(const char* str, i32 sz, char** end) {
  /* Expect that digit representation of LONG_MAX/MIN
   * not greater than this buffer */
  char buf[24];
  i32 ret;
  const char* beg = str;
  /* Catch up leading spaces */
  for (; beg && sz && *beg == ' '; beg++, sz--);
  if (!sz || sz >= sizeof(buf)) {
    if (end) {
      *end = (char*)str;
    }
    return 0;
  }
  memcpy(buf, beg, sz);
  buf[sz] = '\0';
  ret = strtod(buf, end);
  if (ret == F32_MIN || ret == F32_MAX) {
    return ret;
  }
  if (end) {
    *end = (char*)str + (*end - buf);
  }
  return ret;
}


#endif // APPION_H
