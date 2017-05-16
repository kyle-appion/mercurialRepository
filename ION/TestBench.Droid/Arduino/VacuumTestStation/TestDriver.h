
#pragma once

#include "Appion.h"
#include "BluetoothController.h"
#include "Timer.h"
#include "VctControlStepper.h"
#include "Vrc.h"

typedef enum ERigCommandState {
  ERCS_Idle = 1,
  ERCS_Reset = 2,
  ERCS_Test = 3,
};

class TestDriver {
public:
  /*
  Creates a new test driver with the given target points.
  */
  TestDriver(VctControlStepper* intake, VctControlStepper* exhaust, Vrc* vrc, u32* testPoints, u32 pointCount) {
    this->intake = intake;
    this->exhaust = exhaust;
    this->vrc = vrc;

    this->testPoints = testPoints;
    this->pointCount = pointCount;
    this->isReset = false;

    this->troc = 1;
    this->ttime = 1000;
  }
  /*
  Initializes the the driver.
  Note: this is blocking.
  */
  void Initialize();
  /*
  Resets the rig mid test.
  Note: this is blocking.
  */
  void Reset();
  /*
  Moves the test driver's stepper back by a degree.
  */
  void HaltAndBackUp();
  /*
  Performs the step logic for the test driver.
  */
  void Step();
  /*
  Queries the current command state of the driver.
  */
  ERigCommandState GetCommandState();
protected:
  /*
  Called at the end of the step method to perform the native rig step.
  */
  void SafeStep() {
    this->intake->SafeStep();
    this->exhaust->SafeStep();
  }

  /*
  Whether or not the driver is currently
  */
  bool isReset;
  /*
  Whether or not we are currently testing.
  */
  bool isTesting;
  /*
  The array of points themselves.
  */
  u32* testPoints;
  /*
  The number of points that are in the test.
  */
  u32 pointCount;
  /*
  The current index within the test points.
  */
  u32 pointIndex;
  /*
  The timer that is used to track time events.
  */
  Timer timer;
  /*
  The VRC controller that will poll the vrc measurements from the analog in.
  */
  Vrc* vrc;
  /*
  The stepepr for the intake stepper.
  */
  VctControlStepper* intake;
  /*
  The stepper for the the exhaust stepper.
  */
  VctControlStepper* exhaust;
  /*
  The last vrc measurement.
  */
  u32 lastMeas;
  /*
  The current ellapsed time.
  */
  u32 ellapsed;
  /*
  The target rate of change in millitorr for the test. This will be mulitplied by
  1000 when the test in in the high range.
  */
  f32 troc;
  /*
  The time that is used to base rate of change of calculation upon.
  */
  f32 ttime;
  /*
  The factor that is applied to the rate of change. If this multiplied by the
  target rate of change is less than the current rate of change, then we may
  need to scrub the if we passed a target point.
  */
  u32 resetFactor;
};
