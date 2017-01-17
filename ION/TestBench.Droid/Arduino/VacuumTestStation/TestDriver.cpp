

#include "TestDriver.h"

/*
Hangs the program.
*/
void HangUntilImplemented(char* msg) {
  Serial.println(msg);
  Serial.println("The feature needs to be implemented");
  while (true) {
    delay(500);
  }
}

void TestDriver::Initialize() {
  Serial.println("Initialize");
  // Closes the exhaust and input.
  intake->SetRPS(0.5);
  intake->RotateToDegree(90);
/*
  exhaust->SetRPS(0.1);
  exhaust->RotateToDegree(90);

  while (!(intake->IsVctAtEnd() && exhaust->IsVctAtEnd())) {
    SafeStep();
  }
*/

  while (!intake->IsVctAtEnd()) {
    intake->SafeStep();
  }

  Serial.println("Initialize Complete");
}

void TestDriver::Reset() {
  if (isReset) {
    return;
  }
  Serial.println("Reset");

  intake->SetRPS(0.5);
  exhaust->SetRPS(0.05);

  intake->RotateByDegree(-360);
  exhaust->RotateByDegree(-360);

  while (!(intake->IsVctAtStart() && exhaust->IsVctAtStart())) {
    SafeStep();
  }

  intake->Reset();
  exhaust->Reset();

  Initialize();

  intake->SetRPS(0.5);
  intake->RotateToDegree(66.5);
  while (!(intake->IsSteppingComplete())) {
    intake->Step();
  }

  lastMeas = vrc->GetMeasurement();
  this->ellapsed = 0;

  pointIndex = 0;
  isReset = true;
  Serial.println("Reset complete");
}

ERigCommandState TestDriver::GetCommandState() {
  if (!isReset) {
    return ERCS_Reset;
  } else {
    return ERCS_Test;
  }
}

void TestDriver::HaltAndBackUp() {
  intake->Stop();
  intake->RotateByDegree(1);
  while (!intake->IsSteppingComplete()) {
    intake->SafeStep();
  }
}

void TestDriver::Step() {
  isReset = false;

  u32 meas = vrc->GetMeasurement();

  // If we are at the bottom of the test, just go full open
  if (meas < 500) {
    intake->RotateToDegree(0);
    if (!intake->IsSteppingComplete()) {
      intake->SafeStep();
    }
    return;
  }

  // Get the difference of measurement
  i32 dm = meas - lastMeas;
  ellapsed += timer.GetDeltaTime();
  u32 dt = ellapsed / 1000; // Convert from micros to millis
/*
  if (ttime = 0) {
    ttime = 1;
    Serial.println("Time was 0?");
  }
*/

  f32 c_dv_dt = (f32)dm / (f32)dt;
  f32 t_dv_dt = (vrc->isLow ? troc : troc * 1000) / ttime;
  u8 stepped = 0;

// dont calc dvdt until reading changes, expand from that time
  if (ABS(c_dv_dt) > t_dv_dt && !(ellapsed > (ttime * 1000) * 10)) {
    // If the current rate of change is greater than the target, we should wait
    // until is slows down.
  } else if (dm == 0 || dm < 0) {
    // If the vrc measurement has not changed
    if (ellapsed > (ttime * 1000)) {
      // If the current time minus the last time that the vrc changed is greater
      // than the target change rate
      // Then we should perform a single step
      intake->StepCw();
      ellapsed = 0;
      lastMeas = meas;
      stepped = 1;
    }
  }
/*
  Serial.print(stepped);Serial.print(",");
  Serial.print(intake->GetCurrentPositionAsDegrees());Serial.print(",");
  Serial.print(pointIndex);Serial.print(",");
  Serial.print(meas);Serial.print(",");
  Serial.print(lastMeas);Serial.print(",");
  Serial.print((i32)dm);Serial.print(",");
  Serial.print(dt);Serial.print(",");
  Serial.print(c_dv_dt);Serial.print(",");
  Serial.print(t_dv_dt);Serial.print(",");
  Serial.println();
*/
}
