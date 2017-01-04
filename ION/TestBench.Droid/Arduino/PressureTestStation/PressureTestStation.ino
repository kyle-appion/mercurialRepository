#include <string.h>

#include <SoftwareSerial.h>

#include "Appion.h"
#include "ION_Fluke700G.h"

#define PIN_FK_TX 0
#define PIN_FK_RX 1

SoftwareSerial soft(PIN_FK_RX, PIN_FK_TX, true);

void setup() {
  Serial.begin(9600);
  soft.begin(9600);
  soft.setTimeout(10 * 1000);
}


void loop() {
  char buffer[128];
  int cnt = 0;
/*
  Serial.println("PRES_UNIT Bar");
  delay(1000);
  Serial.println("PRES_UNIT Psi");
  delay(1000);
  Serial.println("PRES_UNIT Kg/cm2");
  delay(1000);
  Serial.println("*IDN?");
  delay(1000);
*/
  if (Serial.available()) {
    cnt = Serial.readBytes(buffer, 128);
    Serial.write(buffer, cnt);
    Serial.flush();
    Serial.write("<< ");
    Serial.write(" {");
    Serial.print(cnt);
    Serial.write("} ");
    Serial.write(buffer, cnt);
    Serial.println();
  }
/*
  cnt = soft.readBytes(buffer, 128);
  if (cnt) {
    Serial.write(">> ");
    Serial.write(" {");
    Serial.print(cnt);
    Serial.write("} ");
    Serial.write(buffer, cnt);
    Serial.println();
    Serial.flush();
  }
*/
/*
  Serial.println("Writing VAL?");
  soft.println("VAL?\r");
  soft.flush();
  char buffer[32];
  int cnt = ReadLine1(buffer, 32);
  Serial.write(buffer, cnt);
  Serial.write("\r\n");
  delay(2500);
*/
/*
  char buffer[32];
  Serial.println("Reading line from input...");
  int cnt = ReadLine(buffer, 32);
  Serial.write("ACK: ");
  Serial.write(buffer, cnt);
  Serial.write("\r\n");
  soft.write(buffer, cnt);
//  soft.write("HC_OFF\r\n");
  soft.flush();
  cnt = ReadLine1(buffer, 32);
  if (cnt == 0) {
    Serial.println("Failed to read anything from the fluke");
  } else {
    Serial.print("Read ");
    Serial.write(buffer, cnt);
    Serial.println(" from Fluke");
  }
*/
}

/*
// Reads a line from Serial and places it into the buffer.
i32 ReadLine(char* buffer, i32 maxLen) {
  int i = 0;
  for (; i < maxLen; i++) {
    while (!Serial.available());
    buffer[i] = Serial.read();
    if (i > 0 && buffer[i - 1] == '\r' && buffer[i] == '\n') {
      i -= 1;
      break;
    }
  }

  return i;
}

// Reads a line from Serial and places it into the buffer.
i32 ReadLine1(char* buffer, i32 maxLen) {
  while (soft.available()) {
    Serial.print(soft.read());
  }
  Serial.println();
  return 0;
}
*/
