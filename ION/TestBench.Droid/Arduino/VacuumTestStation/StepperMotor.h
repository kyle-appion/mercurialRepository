/******************************************************************************
StepperMotor.c

This file will describe the implementation of an Stepper Motor hosted by an
Arduino platform.

Something to note is that most (all?) stepper motors can turn in half step
increments. Because of this, the fidelity of stepping is actually sub degree in
almost all cases.

TODO
  * work under consideration is to push all of the state of the motor into a
    small working unit. The would reduce size. The reason this hasn't been done
    is we don't really need to right now.
*******************************************************************************/

#pragma once

#include "Appion.h"


#define PI 3.14159265359f
#define PI2 6.28318530718f

class StepperMotor {
  public:
    // Initializes a new stepper motor.
    // The stepper motor will have its output pins set to the given pins and
    // will calculate the step metrics.
    // Parameters:
    //  stepPulse: The minimum time that is needed between pulses. This is
    //    enforced in stepMotor. (eg. if step motor is called too quickly, we
    //    will skip a step).
    StepperMotor(u32 pinDirection, bool isHighCcw, u32 pinStep, u32 stepsPerRotation, u32 stepPulse) {
      this->pinDirection = pinDirection;
      this->pinStep = pinStep;
      this->stepPulse = stepPulse;
      this->isHighCcw = isHighCcw;

      this->radiansPerHalfStep = PI2 / (stepsPerRotation * 2);
      this->halfStepsPerRotation = stepsPerRotation * 2;

      pinMode(pinDirection, OUTPUT);
      pinMode(pinStep, OUTPUT);

      this->Reset();
    }

    // Queries the stepper motors position in radian measure.
    inline f32 PositionToRadian() {
      return ((f32)position / (f32)halfStepsPerRotation) * PI2;
    }

    // Queries the stepper motor position using the given radian measure.
    inline i32 RadianToPosition(f32 radian) {
      return (i32)round((radian / PI2) * halfStepsPerRotation);
    }

    // Prints the state of the stepper motor.
    void PrintState() {
      if (Serial) {
        Serial.print("Pos: ");Serial.print(position);
        Serial.print(" TPos: ");Serial.print(targetPosition);
        Serial.print(" Dir: ");Serial.print(direction);
        Serial.print(" Delay: ");Serial.print(rotationalSpeed);
        Serial.println();
      }
    }

    // Resets the Stepper motor. Position = targetPosition = 0. If the stepper
    // motor was stopped, it will be allowed to run again.
    void Reset() {
      direction = 0;
      targetPosition = position = 0;
      stepState = 0;
      modulosDeltaTime = 0;
    }

    // Checks whether or not the stepper motor has completed stepping to its target.
    inline bool IsSteppingComplete() {
      return targetPosition == position;
    }

    // Queries the rotations per second that the stepper is set to.
    f32 GetRPS() {
      return rotationalSpeed * halfStepsPerRotation / 1e6f;
    }

    // Sets the rotational speed of the stepper motor. This is is rotations per
    // second.
    // Note: this will never exceed the step pulse of the stepper motor.
    void SetRPS(f32 rps) {
      rotationalSpeed = (u32)(1e6f / (rps * halfStepsPerRotation));
    }

    // Stops the stepper from turning. Effectively this sets the target position
    // equal to the current position.
    void Stop() {
      targetPosition = position;
    }

    // Steps the motor.
    // Note: this does not perform a single step. Instead, it updates the state of
    // the motor based on the given delta time (micro seconds).
    void Step() {
      if (IsSteppingComplete()) {
        return;
      }

      modulosDeltaTime = GetDeltaTime();

      if (modulosDeltaTime < stepPulse || modulosDeltaTime < rotationalSpeed) {
        return;
      } else {
        modulosDeltaTime = modulosDeltaTime % rotationalSpeed;
      }

      if (IsTurningCcw()) {
        position += 1;
        digitalWrite(pinDirection, isHighCcw ? 1 : 0);
      } else {
        position -= 1;
        digitalWrite(pinDirection, isHighCcw ? 0 : 1);
      }

      stepState = (stepState + 1) % 2;
      digitalWrite(pinStep, stepState);
    }

    // Performs a single step clockwise.
    void StepCw() {
      targetPosition -= 1;
      position -= 1;
      digitalWrite(pinDirection, isHighCcw ? 0 : 1);
      incrementStateState();
    }

    // Performs a single step counter-clockwise.
    void StepCcw() {
      targetPosition += 1;
      position += 1;
      digitalWrite(pinDirection, isHighCcw ? 1 : 0);
      incrementStateState();
    }

    // Performs a single state state increment and performs the digital write.
    inline void incrementStateState() {
      stepState = (stepState + 1) % 2;
      digitalWrite(pinStep, stepState);
    }

    // Queries whether or not the motor if turning counter clockwise.
    bool IsTurningCcw() {
      return targetPosition - position >= 0 ;
    }

    // Queries the current radian rotation from the stepper initial position that
    // the stepper currently is.
    f32 GetCurrentPositionAsRadians() {
      return (f32)position / (f32)halfStepsPerRotation * PI2;
    }

    // Queries the current degree rotation of the stepper motor.
    f32 GetCurrentPositionAsDegrees() {
      return (f32)position / (f32)halfStepsPerRotation * 360;
    }

    // Queries the target radian rotation that the stepper is moving to.
    f32 GetTargetPositionAsRadians() {
      return (f32)targetPosition / (f32)halfStepsPerRotation * PI2;
    }

    // Queries the target degree rotation that the stepper is moving to.
    f32 GetTargetPositionAsDegrees() {
      return (f32)targetPosition / (f32)halfStepsPerRotation * 360;
    }

    // Rotates the stepper motor to the given radian. If the target radian is less
    // than the current position, the direction will be changed.
    // Note: positive radians rotate the stepper counter clockwise
    // Note: this is based on the stepper's zero position.
    void RotateToRadian(f32 radian) {
      targetPosition = RadianToPosition(radian);
    }

    // Rotates the stepper motor to the given degree. If the target degree is less
    // than the current position, the direction will be changed.
    // Note: positive degrees rotate the stepper counter clockwise.
    // Note: This is based on the stepper's zero position.
    void RotateToDegree(f32 degree) {
      RotateToRadian(degree * PI / 180);
    }

    // Sets the stepper motor to start rorating by the given radian amount. The
    // sign of the radian will dictate the direction of the stepper.
    // Note: positive radians rotate the stepper counter clockwise
    // Note: the stepper motor will attempt to get as close to the given radian
    // as possible. This is dependant on the motor's steps-per-rotation.
    void RotateByRadian(f32 radian) {
      RotateToRadian(radian + PositionToRadian());
    }

    // Starts the stepper motor rotating by the givin degree count. The sign of
    // the degree will dictate the direction of the stepper.
    // Note: positive radians rotate the stepper counter clockwise.
    // Note: if the given degree does not line up with the stepper's natural
    // degree interval, the stepper will round down to the closest proper step
    // degree.
    void RotateByDegree(f32 degree) {
      RotateByRadian(degree * PI / 180);
    }
    // The rotational speed of the stepper motor.
    u32 rotationalSpeed;
  protected:
    // The target position for the stepper motor. This is calculated by calling
    // a rotate function.
    i32 targetPosition;

    // The adjustment to which direction the stepper motor considers CW and which
    // the stepper motor considers CCW.
    i32 directionMode;

  private:
    // The digital pin that is used in setting the stepper motors direction.
    u32 pinDirection;
    // The digital pin that is used in causing a step.
    u32 pinStep;
    // Whether or not a high signal on the direction pin correlated to a ccw
    // motor rotation.
    bool isHighCcw;

    // The state of the direction output pin. 0 is low 1 is high.
    u32 direction;
    // The number of radians per half step of the stepper motor
    f32 radiansPerHalfStep;
    // The total number of steps in one full circular rotation. A step is a toggle
    // from low to high to low.
    i32 halfStepsPerRotation;
    // The length in microseconds necessary to constitute a complete step.
    u32 stepPulse;
    // The current position of the stepper motor (in steps). This value ranges
    // [0, stepsPerRotation).
    i32 position;
    // The current step state (high or low).
    u8 stepState;
    // The last known micro time.
    u32 lastMicro;
    // The Remiaining delta time that is applied to the next step.
    u32 modulosDeltaTime;

    // Calculates the current delta time.
    u32 GetDeltaTime() {
      static u32 max = -1; // Used to manage a wrapping of micros.

      u32 now = micros();
      u32 dt = 0;

      if (now < lastMicro) {
        dt = max - lastMicro + now;
      } else {
        dt = now - lastMicro;
      }

      lastMicro = now;

      return dt + modulosDeltaTime;
    }
};
