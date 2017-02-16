/******************************************************************************
VctControlStepper.h

This file provides a wrapper around a
******************************************************************************/

#pragma once

#include "Appion.h"
#include "StepperMotor.h"

// This data structure will wrap a Stepper motor and provide useful states and
// limits to the stepper.
class VctControlStepper : public StepperMotor {
  public:
    VctControlStepper(u32 pinDirection, bool isHighCcw, u32 pinStep, u32 stepsPerRotation, u32 stepPulse, u32 pinStartSwitch, u32 pinEndSwitch)
        : StepperMotor(pinDirection, isHighCcw, pinStep, stepsPerRotation, stepPulse) {
      this->pinStartSwitch = pinStartSwitch;
      this->pinEndSwitch = pinEndSwitch;
      pinMode(pinStartSwitch, INPUT_PULLUP);
      pinMode(pinEndSwitch, INPUT_PULLUP);
    }

    // Safely steps the stepper motor. Returns true if the step was made.
    bool SafeStep() {
      if (IsVctAtEnd() && IsTurningCcw()) {
        return false;
      } else if (IsVctAtStart() && !IsTurningCcw()) {
        return false;
      } else {
        Step();
        return true;
      }
    }

    // Queries whether or not the vct is pressing on the start switch.
    bool IsVctAtStart() {
      return !digitalRead(pinStartSwitch);
    }

    // Queries whether or not the vct is pressing on the end switch.
    bool IsVctAtEnd() {
      return !digitalRead(pinEndSwitch);
    }

  private:
    // The pin for the VctControlStepper's start switch.
    u32 pinStartSwitch;
    // The pin for the VctControlStepper's end switch.
    u32 pinEndSwitch;
};
