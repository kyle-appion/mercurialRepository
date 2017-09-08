
#pragma once

#include "Wire.h"
#include "Adafruit_ADS1015.h"

#include "Appion.h"

class Vrc {
  public:
    // https://learn.adafruit.com/adafruit-4-channel-adc-breakouts/assembly-and-wiring
    // Initializes a VRC structure.
    // Note: the VRC structure assumes that it has a ADS 1115 ADC connected to the
    // arduino's SCL and SDA pins. For a modern MEGA this is pin 20 and 21.
    // Additionally, the ADC assumes that the low measurement single ended analog
    // wire is connected to the ADC analog input pin A0 and the high measurement
    // single ended analog wire is connect to the ADC analog input pin A1.
    Vrc() {
      this->adc = new Adafruit_ADS1115;
      this->measurement = 0;

      adc->begin();
      adc->setGain(GAIN_TWO);
    }

    // Reads and calculates the current vrc measurement.
    u32 GetMeasurement() {
      f64 lowRaw = (f64)adc->readADC_SingleEnded(1);
      f64 highRaw = (f64)adc->readADC_SingleEnded(0);
      u32 low = (u32)(lowRaw / 32767.0 * 2048.0);
      u32 high = (u32)(highRaw / 32767.0 * 2048.0);
/*
      Serial.print("LowRaw: ");Serial.print(lowRaw);Serial.print(" Low: ");Serial.println(low);
      Serial.print("HighRaw: ");Serial.print(highRaw);Serial.print(" High: ");Serial.println(high);
*/
      if (lowRaw < highRaw || high <= 2) {
        measurement = low;
        isLow = true;
      } else {
        measurement = high * 1000;
        isLow = false;
      }

      return measurement;
    }

//  private:
    // The adc that is providing the analog inputs from the adc.
    Adafruit_ADS1115* adc;
    // The last known measurement of the vrc.
    u32 measurement;
    // Whether or not the vrc in measuring from the low input.
    bool isLow;
};
